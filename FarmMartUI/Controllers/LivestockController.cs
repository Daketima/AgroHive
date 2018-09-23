using AutoMapper;
using FarmMartBLL.Core;
using FarmMartBLL.ServiceAPI;
using FarmMartDAL.Model;
using FarmMartUI.Areas.Farmer.Models;
using FarmMartUI.helper;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Controllers
{
    
    public class LivestockController : SuperBaseController
    {
        private IRepositoryService<Livestock> LivestockService;
        private IRepositoryService<LivestockPrice> LivestockPriceService;
        private IRepositoryService<FarmLivestock> FarmLivestockService;
        private IRepositoryService<Measurement> MeasurementService;

        public LivestockController(IRepositoryService<Livestock> livestockService,
        IRepositoryService<LivestockPrice> livestockPriceService,
        IRepositoryService<FarmLivestock> farmLivestockService,
        IRepositoryService<Measurement> measurementService)
        {
            LivestockService = livestockService;
            LivestockPriceService = livestockPriceService;
            FarmLivestockService = farmLivestockService;
            MeasurementService = measurementService;
        }



        public void SaveLivestockImage(Livestock Livestock)
        {
            if (Request.Files.Count > 0)
            {
                int fileCount = Request.Files.Count;

                for (int i = 0; i < fileCount; i++)
                {
                    var file = Request.Files[i];

                    if (file.ContentLength == 0)
                        break;


                    var fileName = Livestock.Id.ToString() + "_" + i.ToString() + "_" + Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/LivestockProfile"), fileName);

                    try
                    {
                        file.SaveAs(path);
                        Livestock.PhotoPath = fileName;
                        LivestockService.Update(Livestock);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        //private IEnumerable<SelectListItem> GetMeasurement(int? selected)
        //{
        //    if (!selected.HasValue)
        //        selected = 0;

        //    var allMeasurement = MeasurementService.Get().ToList();
        //    allMeasurement.Insert(0, new Measurement { Id = 0, Name = "Please Select" });
        //    return allMeasurement.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        //}

        [HttpGet]
        public ActionResult GetFarmLivestockList(int? farmId)
        {
            var model = new FarmLivestockViewModel
            {
                MyFarmLivestock = FarmLivestockService.Get().Where(x => x.FarmId == farmId.Value).ToList()
            };
            return PartialView("_FarmLivestockList", model);
        }

        private List<Livestock> GetLivestockList()
        {
            return LivestockService.Get().ToList();
        }

        // GET: Livestock
        public ActionResult Index()
        {
            var model = new LivestockViewModel
            {
                AllLivestock = LivestockService.Get().ToList()
            };
            return View(model);
        }

        // GET: Livestock/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Livestock/Create
        public ActionResult AddLivestock()
        {
            var model = new LivestockViewModel
            {

            };
            return View(model);
        }

        // POST: Livestock/Create
        [HttpPost]
        public ActionResult AddLivestock(LivestockViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    var Livestock = new Livestock
                    {
                        Name = model.Name,
                        PhotoPath = "upload.png",
                        Description = model.Description,
                        DateCreated = DateTime.Now,

                    };
                    Livestock = LivestockService.Create(Livestock);

                    if (Livestock != null)
                    {
                        SaveLivestockImage(Livestock);
                    }

                    return RedirectToAction("Index", "Farm");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Livestock/Edit/5
        public ActionResult EditLivestock(int LivestockId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Livestock, LivestockViewModel>();
            });

            IMapper iMapper = config.CreateMapper();

            var Livestock = LivestockService.GetById(LivestockId);

            var model = iMapper.Map<Livestock, LivestockViewModel>(Livestock);

            return View(model);
        }

        // POST: Livestock/Edit/5
        [HttpPost]
        public ActionResult EditLivestock(LivestockViewModel model)
        {
            try
            {
                var Livestock = LivestockService.GetById(model.Id);
                Livestock.Name = model.Name;
                Livestock.Description = model.Description;
                Livestock.PhotoPath = "fm.jpg";

                LivestockService.Update(Livestock);

                SaveLivestockImage(Livestock);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Livestock/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Livestock/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Livestock/Add livestock to farm
        //public ActionResult AddFarmLivestock(int? farmId)
        //{
        //    var model = new LivestockViewModel
        //    {
        //        LivestockListItem = GetLivestockList()
        //    };
        //    return PartialView("_AddLivestockToFarmDialog", model);
        //}

        // GET: Pricing/Create

        public ActionResult AddLivestockPrice(int? farmLivestockId, int? farmId)
        {
            var model = new LivestockPriceViewModel
            {
                FarmLivestockId = farmLivestockId.Value,
                FarmId = farmId.Value,
                MeasurementDropDown = GetMeasurement(null)
            };
            return View(model);
        }

        // POST: Pricing/Create
        [HttpPost]
        public ActionResult AddLivestockPrice(LivestockPriceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pricing = new LivestockPrice
                {
                    MeasurementId = model.MeasurementId,
                    UnitPrice = model.UnitPrice,
                    DateCreated = DateTime.Now
                };

                LivestockPriceService.Create(pricing);

                return RedirectToAction("Index", "FarmLivestock");
            }
            return RedirectToAction("Index", "FarmLivestock");
        }

        public ActionResult PriceDetail(int? farmLivestockId)
        {
            FarmLivestock fLivestock = FarmLivestockService.GetById(farmLivestockId);

            LivestockViewModel model = new LivestockViewModel
            {
                Photopath = fLivestock.LivestockBreed.PhotoPath,
                Name = fLivestock.LivestockBreed.Name,
                BreedNote = fLivestock.LivestockBreed.Note,
                PriceDetail = fLivestock.LivestockPrice,
                farmLivestock = fLivestock
            };
            return View(model);
        }


    }
}
