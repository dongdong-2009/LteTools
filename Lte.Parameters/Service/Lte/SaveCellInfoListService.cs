using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Lte.Domain.TypeDefs;
using Lte.Domain.Regular;
using Lte.Parameters.Abstract;
using Lte.Parameters.Concrete;
using Lte.Parameters.Entities;
using Lte.Parameters.Kpi.Service;
using Lte.Parameters.Service.Public;

namespace Lte.Parameters.Service.Lte
{
    public abstract class SaveCellInfoListService
    {
        protected readonly ICellRepository _repository;

        protected SaveCellInfoListService(ICellRepository repository)
        {
            _repository = repository;
        }

        public abstract void Save(ParametersDumpInfrastructure infrastructure, IParametersDumpResults results);
    }

    public class QuickSaveCellInfoListService : SaveCellInfoListService
    {
        private readonly IEnumerable<CellExcel> _cellInfoList;
        private readonly CellBaseRepository _baseRepository;
        private readonly ENodebBaseRepository _baseENodebRepository;

        public QuickSaveCellInfoListService(ICellRepository repository,
            IEnumerable<CellExcel> cellInfoList, IENodebRepository eNodebRepository)
            : base(repository)
        {
            _cellInfoList = cellInfoList;
            _baseRepository = new CellBaseRepository(repository);
            _baseENodebRepository = new ENodebBaseRepository(eNodebRepository);
        }

        public override void Save(ParametersDumpInfrastructure infrastructure, IParametersDumpResults results)
        {
            infrastructure.CellsInserted = 0;
            foreach (CellExcel info in _cellInfoList)
            {
                ByENodebBaseQuickSaveOneCellService service = 
                    new ByENodebBaseQuickSaveOneCellService(_repository, _baseRepository, info, _baseENodebRepository);
                if (service.Save())
                {
                    _baseRepository.ImportNewCellInfo(info);
                    infrastructure.CellsInserted++;
                    results.NewCells = infrastructure.CellsInserted;
                }
            }
        }
    }

    public class UpdateConsideredSaveCellInfoListService : SaveCellInfoListService
    {
        private readonly IEnumerable<CellExcel> _validInfos;
        private readonly bool _updateExisted;
        private readonly bool _updatePci;

        public UpdateConsideredSaveCellInfoListService(ICellRepository repository,
            IEnumerable<CellExcel> cellInfos, IENodebRepository eNodebRepository,
            bool updateExisted = false, bool updatePci = false)
            : base(repository)
        {
            _updateExisted = updateExisted;
            _updatePci = updatePci;
            IEnumerable<CellExcel> distinctInfos
                = cellInfos.Distinct(p => new { p.ENodebId, p.SectorId, p.Frequency });
            _validInfos
                = from d in distinctInfos
                  join e in eNodebRepository.GetAll()
                  on d.ENodebId equals e.ENodebId
                  select d;
        }

        public override void Save(ParametersDumpInfrastructure infrastructure, IParametersDumpResults results)
        {
            var updateCells
                = from v in _validInfos
                  join c in _repository.GetAll()
                  on new { v.ENodebId, v.SectorId, v.Frequency }
                  equals new { c.ENodebId, c.SectorId, c.Frequency }
                  select new { Info = v, Data = c };
            infrastructure.CellsUpdated = 0;
            infrastructure.NeighborPciUpdated = 0;
            if (_updateExisted && _updatePci)
            {
                foreach (var cell in updateCells.Where(x=>x.Data.Pci!=x.Info.Pci))
                {
                    cell.Data.Pci = cell.Info.Pci;
                    _repository.Update(cell.Data);
                    infrastructure.CellsUpdated++;
                    infrastructure.NeighborPciUpdated++;
                    results.UpdateCells = infrastructure.CellsUpdated;
                    results.UpdatePcis = infrastructure.NeighborPciUpdated;
                }

                infrastructure.NeighborPciUpdated = SaveLteCellRelationService.UpdateNeighborPci(_validInfos);
            }
            IEnumerable<Cell> insertInfos = _validInfos.Except(updateCells.Select(x => x.Info)).Select(x =>
            {
                Cell cell = new Cell();
                cell.Import(x);
                return cell;
            }).ToList();
            _repository.AddCells(insertInfos);
            infrastructure.CellsInserted = insertInfos.Count();
            results.NewCells = infrastructure.CellsInserted;
        }
    }

    public class SaveLteCellRelationService
    {
        private readonly ILteNeighborCellRepository _repository;

        public SaveLteCellRelationService(ILteNeighborCellRepository repository)
        {
            _repository = repository;
        }

        public void Save(IEnumerable<LteCellRelationCsv> infos)
        {
            foreach (LteCellRelationCsv info in infos)
            {
                string[] fields = info.NeighborRelation.Split(':');
                int eNodebId = fields[3].ConvertToInt(0);
                byte sectorId = fields[4].ConvertToByte(0);
                if (eNodebId < 10000) continue;
                LteNeighborCell nCell = _repository.NeighborCells.FirstOrDefault(x =>
                    x.CellId == info.ENodebId && x.SectorId == info.SectorId
                    && x.NearestCellId == eNodebId && x.NearestSectorId == sectorId);
                if (nCell != null) continue;
                nCell = new LteNeighborCell
                {
                    CellId = info.ENodebId,
                    SectorId = info.SectorId,
                    NearestCellId = eNodebId,
                    NearestSectorId = sectorId
                };
                _repository.AddOneCell(nCell);
                if (eNodebId%1000 == sectorId)
                {
                    _repository.SaveChanges();
                }
            }
            _repository.SaveChanges();
        }

        public static int UpdateNeighborPci(IEnumerable<CellExcel> cells)
        {
            SqlConnection conn = new SqlConnection(
                "Data Source=WIN-E7U0ZAGEQAQ;Initial Catalog=ouyanghui_practise;User ID=ouyanghui;Password=123456");
            conn.Open();
            int count = 0;
            using (SqlCommand cmd = new SqlCommand("sp_UpdateNearestPci", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@eNodebId",
                    SqlDbType = SqlDbType.Int,
                    Value = 0
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@sectorId",
                    SqlDbType = SqlDbType.TinyInt,
                    Value = 0
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@pci",
                    SqlDbType = SqlDbType.SmallInt,
                    Value = 0
                });
                foreach (CellExcel cell in cells)
                {
                    cmd.Parameters[0].Value = cell.ENodebId;
                    cmd.Parameters[1].Value = cell.SectorId;
                    cmd.Parameters[2].Value = cell.Pci;
                    count += cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
            return count;
        }
    }
}
