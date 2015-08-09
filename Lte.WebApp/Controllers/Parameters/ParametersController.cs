using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lte.Evaluations.Service;
using Lte.Evaluations.ViewHelpers;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities;
using Lte.Parameters.Region.Abstract;

namespace Lte.WebApp.Controllers.Parameters
{
    public class ParametersController : Controller
    {
        private readonly ITownRepository townRepository;
        private readonly IENodebRepository eNodebRepository;
        private readonly ICellRepository cellRepository;
        private readonly IBtsRepository btsRepository;
        private readonly ICdmaCellRepository cdmaCellRepository;
        private readonly IRegionRepository regionRepository;
        private readonly IENodebPhotoRepository photoRepository;

        public ParametersController(
            ITownRepository townRepository,
            IENodebRepository eNodebRepository,
            ICellRepository cellRepository,
            IBtsRepository btsRepository,
            ICdmaCellRepository cdmaCellRepository,
            IRegionRepository regionRepository,
            IENodebPhotoRepository photoRepository)
        {
            this.townRepository = townRepository;
            this.eNodebRepository = eNodebRepository;
            this.cellRepository = cellRepository;
            this.btsRepository = btsRepository;
            this.cdmaCellRepository = cdmaCellRepository;
            this.regionRepository = regionRepository;
            this.photoRepository = photoRepository;
        }

        public ViewResult List(ParametersContainer container)
        {
            container.ImportTownENodebStats(townRepository, eNodebRepository, regionRepository);
            return View(container.TownENodebStats);
        }

        public ViewResult ENodebList(int townId, int page = 1)
        {
            ENodebListViewModel viewModel 
                = new ENodebListViewModel(eNodebRepository, townRepository, townId, page);
            return View(viewModel);
        }

        public ViewResult Query()
        {
            ENodebQueryViewModel viewModel = new ENodebQueryViewModel();
            viewModel.InitializeTownList(townRepository);
            return View(viewModel);
        }

        [HttpPost]
        public ViewResult Query(ENodebQueryViewModel viewModel)
        {
            ParametersContainer.QueryENodebs = viewModel.ENodebs = eNodebRepository.GetAllWithNames(townRepository,
                viewModel, viewModel.ENodebName, viewModel.Address);

            viewModel.InitializeTownList(townRepository, viewModel);
            if (viewModel.ENodebs != null)
            {
                return View(viewModel);
            } 
            
            viewModel = new ENodebQueryViewModel();
            viewModel.InitializeTownList(townRepository);
            return View(viewModel);
        }

        public ActionResult ENodebEdit(int eNodebId)
        {
            ENodebDetailsViewModel viewModel = new ENodebDetailsViewModel();
            viewModel.Import(eNodebId, eNodebRepository, cellRepository, btsRepository, cdmaCellRepository,
                photoRepository);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateENodebInfo(ENodeb item)
        {
            ENodeb eNodeb = eNodebRepository.GetAll().FirstOrDefault(x => x.ENodebId == item.ENodebId);
            if (eNodeb == null) return View("ENodebEdit", new ENodebDetailsViewModel());
            eNodeb.Address = item.Address;
            eNodeb.Name = item.Name;
            eNodeb.Factory = item.Factory;
            ENodebDetailsViewModel viewModel = new ENodebDetailsViewModel();
            viewModel.Import(eNodeb.ENodebId, eNodebRepository, cellRepository, btsRepository, cdmaCellRepository,
                photoRepository);
            return View("ENodebEdit",viewModel);
        }

        [HttpPost]
        public ActionResult UpdateBtsInfo(CdmaBts item)
        {
            CdmaBts bts = btsRepository.GetAll().FirstOrDefault(x => x.ENodebId == item.ENodebId);
            if (bts == null) return View("ENodebEdit", new ENodebDetailsViewModel());
            bts.Address = item.Address;
            bts.Name = item.Name;
            btsRepository.Update(bts);
            ENodebDetailsViewModel viewModel = new ENodebDetailsViewModel();
            viewModel.Import(bts.ENodebId, eNodebRepository, cellRepository, btsRepository, cdmaCellRepository,
                photoRepository);
            return View("ENodebEdit", viewModel);
        }

        [HttpPost]
        public ActionResult UpdateImage()
        {
            int eNodebId = int.Parse(Request["ENodeb.ENodebId"]);
            string name = Request["ENodeb.Name"];
            if (Request.Files["btsImage"] != null && !string.IsNullOrEmpty(Request.Files["btsImage"].FileName))
            {
                HttpImporter btsImporter = new ImageFileImporter(Request.Files["btsImage"], name);
                ENodebPhoto btsPhoto = photoRepository.Photos.FirstOrDefault(
                    x => x.ENodebId == eNodebId && x.SectorId == 255 && x.Angle == -1);
                if (btsPhoto == null)
                {
                    btsPhoto = new ENodebPhoto
                    {
                        ENodebId = eNodebId,
                        SectorId = 255,
                        Angle = -1,
                        Path = btsImporter.FilePath
                    };
                    photoRepository.AddOnePhoto(btsPhoto);
                }
                photoRepository.SaveChanges();
            }
            IEnumerable<Cell> cells = cellRepository.GetAll().Where(x => x.ENodebId == eNodebId).ToList();
            foreach (Cell cell in cells)
            {
                HttpPostedFileBase file = Request.Files["cellImage-" + cell.SectorId];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    HttpImporter cellImporter=new ImageFileImporter(file,name);
                    byte sectorId = cell.SectorId;
                    ENodebPhoto cellPhoto = photoRepository.Photos.FirstOrDefault(
                        x => x.ENodebId == eNodebId && x.SectorId == sectorId && x.Angle == -1);
                    if (cellPhoto == null)
                    {
                        cellPhoto = new ENodebPhoto
                        {
                            ENodebId = eNodebId,
                            SectorId = sectorId,
                            Angle = -1,
                            Path = cellImporter.FilePath
                        };
                        photoRepository.AddOnePhoto(cellPhoto);
                    }
                    photoRepository.SaveChanges();
                }
            }
            ENodebDetailsViewModel viewModel = new ENodebDetailsViewModel();
            viewModel.Import(eNodebId, eNodebRepository, cellRepository, btsRepository, cdmaCellRepository,
                photoRepository);
            return View("ENodebEdit", viewModel);
        }

        public ViewResult CellList(int eNodebId)
        {
            ENodebDetailsViewModel viewModel = new ENodebDetailsViewModel();
            viewModel.Import(eNodebId, eNodebRepository, cellRepository, btsRepository, cdmaCellRepository,
                photoRepository);
            return View(viewModel);
        }

        public JsonResult GetDistrictENodebsStat(ParametersContainer container, string cityName)
        {
            if (container.TownENodebStats == null)
            { container.ImportTownENodebStats(townRepository, eNodebRepository, regionRepository); }

            return Json(container.GetENodebsByDistrict(cityName).Select(
                x => new { D = x.Key, N = x.Value }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTownENodebsStat(ParametersContainer container,
            string cityName, string districtName)
        {
            if (container.TownENodebStats == null)
            { container.ImportTownENodebStats(townRepository, eNodebRepository, regionRepository); }

            return Json(container.GetENodebsByTown(cityName, districtName).Select(
                x => new { T = x.Key, N = x.Value }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetENodebImage(int eNodebId)
        {
            ENodebPhoto photo =
                photoRepository.Photos.FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == 255 && x.Angle == -1);
            string path = (photo == null) ? "" : photo.Path;
            return File(path, "image/jpg");
        }

        public ActionResult GetCellImage(int eNodebId, byte sectorId)
        {
            ENodebPhoto photo =
                photoRepository.Photos.FirstOrDefault(x => x.ENodebId == eNodebId && x.SectorId == sectorId && x.Angle == -1);
            string path = (photo == null) ? "" : photo.Path;
            return File(path, "image/jpg");
        }
    }
}
