﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Lte.Domain.Geo.Abstract;
using Lte.Domain.Regular;
using Lte.Evaluations.Kpi;
using Lte.Evaluations.Rutrace.Record;
using Lte.Evaluations.Service;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Region.Abstract;
using Lte.Parameters.Region.Entities;

namespace Lte.Evaluations.ViewHelpers
{
    public class ENodebListViewModel : IPagingListViewModel<ENodeb>
    {
        public IEnumerable<ENodeb> Items { get; set; }

        public IEnumerable<ENodeb> QueryItems
        {
            get { return ParametersContainer.QueryENodebs; }
        }

        public PagingInfo PagingInfo { get; set; }

        private static int _townId = -1;

        public int TownId
        {
            get { return _townId; }
        }

        public double CenterLongtitute 
        {
            get { return QueryItems.Select(x => x.Longtitute).Average(); }
        }

        public double CenterLattitute 
        {
            get { return QueryItems.Select(x => x.Lattitute).Average(); }
        }

        private static string _infoTitle;

        public string InfoTitle 
        { 
            get { return _infoTitle; }
        }

        public ENodebListViewModel()
        {
        }

        public ENodebListViewModel(IENodebRepository eNodebRepository, ITownRepository townRepository,
            int townId, int page, int pageSize = 10)
        {
            if (townId != _townId)
            {
                ParametersContainer.QueryENodebs 
                    = eNodebRepository.GetAll().Where(x => x.TownId == townId).OrderBy(p => p.Id).ToList();
                _townId = townId;
                Town town = townRepository.Towns.FirstOrDefault(x => x.Id == TownId);
                _infoTitle = (town == null) ? "" :
                    town.CityName + town.DistrictName + town.TownName + "基站列表";
            }
            
            this.SetItems(page, pageSize);
        }
    }

    public class ENodebDetailsViewModel
    {
        public ENodeb ENodeb { get; private set; }

        public IEnumerable<Cell> Cells { get; private set; }

        public CdmaBts Bts { get; private set; }

        public IEnumerable<CdmaCell> CdmaCells { get; private set; }

        public IEnumerable<ENodebPhoto> Photos { get; private set; }

        public void Import(int eNodebId,
            IENodebRepository eNodebRepository, ICellRepository cellRepository, IBtsRepository btsRepository,
            ICdmaCellRepository cdmaCellRepository, IENodebPhotoRepository photoRepository)
        {
            ENodeb eNodeb = eNodebRepository.GetAll().FirstOrDefault(x => x.ENodebId == eNodebId);
            if (eNodeb != null)
            {
                ENodeb = eNodeb;
                Cells = cellRepository.Cells.Where(x => x.ENodebId == eNodebId).ToList();
            }
            CdmaBts bts = btsRepository.Btss.FirstOrDefault(x => x.ENodebId == eNodebId);
            if (bts != null)
            {
                int btsId = bts.BtsId;
                Bts = bts;
                CdmaCells = cdmaCellRepository.Cells.Where(x => x.BtsId == btsId).ToList();
            }
            Photos = photoRepository.Photos.Where(x => x.ENodebId == eNodebId).ToList();
        }
    }

    public class ENodebQueryViewModel : ITown, ITownDefViewModel
    {
        [Display(Name = "城市：")]
        public string CityName { get; set; }

        [Display(Name = "区域：")]
        public string DistrictName { get; set; }

        [Display(Name = "镇区：")]
        public string TownName { get; set; }

        [Display(Name = "基站名称（模糊匹配）：")]
        public string ENodebName { get; set; }

        [Display(Name = "基站地址（模糊匹配）：")]
        public string Address { get; set; }

        public IEnumerable<ENodeb> ENodebs { get; set; }

        public List<SelectListItem> CityList { get; set; }

        public List<SelectListItem> DistrictList { get; set; }

        public List<SelectListItem> TownList { get; set; }

        public double CenterLongtitute
        {
            get { return ENodebs==null?113: ENodebs.Average(x => x.BaiduLongtitute); }
        }

        public double CenterLattitute
        {
            get { return ENodebs==null?23: ENodebs.Average(x => x.BaiduLattitute); }
        }

        public void InitializeTownList(ITownRepository townRepository, ITown town = null)
        {
            if (town != null)
            {
                this.Initialize(townRepository.Towns, town);
            }
            else
            {
                this.Initialize(townRepository.Towns);
            }
        }
    }

    public class MrInterferenceViewModel : ENodebQueryViewModel, IDateSpanViewModel
    {
        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public List<MrsCellDateView> MrsCells { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public void UpdateStats(List<MrsCellDateView> stats, IEnumerable<ENodebBase> eNodebBases, 
            int pageSize, int page = 1)
        {
            if (stats == null)
                stats = new List<MrsCellDateView>();
            MrsCells = stats.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = stats.Count()
            };
        }

    }

    public class EvaluationViewModel : ENodebQueryViewModel
    {
        [Display(Name = "PDSCH/PDCCH负荷（%）"),
        Range(0, 100, ErrorMessage = "输入参数超出范围！必须在0到100之间"), UIHint("Number"), Required]
        public double TrafficLoad { get; set; }

        [Display(Name = "仿真栅格边长（米）"),
        Range(20, 1000, ErrorMessage = "输入参数超出范围！必须在20到1000之间"), UIHint("Number"), Required]
        public double DistanceInMeter { get; set; }

        [Display(Name = "最大小区覆盖距离（米）"),
        Range(3000, 20000, ErrorMessage = "输入参数超出范围！必须在3000到20000之间"), UIHint("Number"), Required]
        public double CellCoverage { get; set; }

        public PagingInfo PagingInfo { get; set; }

    }

    public class RutraceImportModel : ENodebQueryViewModel
    {
        [Display(Name = "查看TOPN指标个数：")]
        public int TopNum { get; set; }

        public List<RuInterferenceStat> Stats { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public void UpdateStats(List<RuInterferenceStat> stats, int pageSize, int page = 1)
        {
            Stats = stats.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = stats.Count()
            };
            TopNum = Convert.ToInt32(TopNumChoices.ElementAt(1).Value);
        }

        public static IEnumerable<SelectListItem> TopNumChoices
        {
            get
            {
                return new List<SelectListItem> {
                    new SelectListItem { Selected = true, Text="10", Value="10" },
                    new SelectListItem { Text="20", Value="20" },
                    new SelectListItem { Text="50", Value="50" },
                    new SelectListItem { Text="100", Value="100" },
                    new SelectListItem { Text="200", Value="200" }
                };
            }
        }
    }
}
