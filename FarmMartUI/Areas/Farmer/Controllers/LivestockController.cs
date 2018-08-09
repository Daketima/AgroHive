using FarmMartBLL.Core;
using FarmMartBLL.ServiceAPI;
using FarmMartDAL.Model;
using FarmMartUI.Areas.Farmer.Models;
using FarmMartUI.helper;
using FarmMartUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Areas.Farmer.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class LivestockController : SuperBaseController
    {
        private IRepositoryService<Livestock> LivestockService;
        private IRepositoryService<FarmLivestock> FarmLivestockService;
        private readonly IRepositoryService<LivestockBreed> LivestockBreedService;

        private IRepositoryService<LivestockPrice> LivestockPriceService;


        public LivestockController(IRepositoryService<Livestock> livestockService, IRepositoryService<FarmLivestock> farmLivestockService, IRepositoryService<LivestockPrice> livestockPriceService, IRepositoryService<LivestockBreed> farmLivestockBreedService)
        {
            LivestockService = livestockService;
            FarmLivestockService = farmLivestockService;
            LivestockBreedService = farmLivestockBreedService;
            LivestockPriceService = livestockPriceService;

        }

        private List<Livestock> GetLivestockList() => LivestockService.Get().ToList();

        public IEnumerable<SelectListItem> GetLivestockBreed(int? selected)
        {
            string userId = User.Identity.GetUserId();


            if (!selected.HasValue)
                selected = 0;

            var allFarms = LivestockBreedService.Get().ToList();
            allFarms.Insert(0, new LivestockBreed { Id = 0, Name = "Please Select" });
            return allFarms.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        public IEnumerable<SelectListItem> GetLivestockBreedEmpty(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allFarms = new List<LivestockBreed>();
            allFarms.Insert(0, new LivestockBreed { Id = 0, Name = "Please Select" });
            return allFarms.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        [HttpGet]
        public ActionResult GetLivestockBreedNew(int? LivestockId)
        {
            var allFarms = this.LivestockBreedService.Get().Where(x => x.LivestockId == LivestockId.Value).ToList();
            return Json(allFarms, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFarmLivestockList(int? farmId)
        {
            FarmLivestockViewModel model = new FarmLivestockViewModel
            {
                FarmLivestockList = FarmLivestockService.Get().Where(x => x.FarmId == farmId.Value && x.Farm.IsActive).ToList()
            };
            return PartialView("_FarmLivestockList", model);
        }

        // GET: Livestock
        public ActionResult Index(int? farmId)
        {
            var model = new FarmLivestockViewModel
            {
                FarmDropDown = base.GetMyFarm(null)
            };
            return View(model);
        }

        // GET: Livestock/Create
        public ActionResult AddFarmLivestock(int? farmId)
        {
            var model = new FarmLivestockViewModel
            {
                LivestockBreedList = LivestockBreedService.Get().ToList(),
                FarmId = farmId.Value

            };
            return View(model);
        }

        // GET: Livestock/Create

        public ActionResult AddLivestockToFarm(int farmId, int? BreedId)
        {

            var model = new FarmLivestockViewModel
            {
                FarmId = farmId,
                AnimalGenderDropDown = base.GetMyAnimalGender(null),
                LivestockBreedId = BreedId.Value
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddLivestockToFarm(FarmLivestockViewModel model)
        {
            FarmLivestock checkFarmLivestock = LivestockAlreadyAdded(model.LivestockBreedId, model.FarmId);

            if (checkFarmLivestock != null)
            {
                model.AnimalGenderDropDown = GetMyAnimalGender(model.GenderId);

                return RedirectToAction("AddFarmLivestock", new { farmId = model.FarmId, BreedId = model.LivestockBreedId });
            }

            if (ModelState.IsValid)
            {
                var newFarmLivestock = new FarmLivestock
                {
                    LivestockBreedId = model.LivestockBreedId,
                    FarmId = model.FarmId,
                    GenderId = model.GenderId,
                    Population = model.Population,
                    QuantityAvailable = model.QuantityAvailable,
                    Weight = model.Weight,
                    HitMarketDate = model.HitMarketDate,
                    IsActive = true,
                    DateCreated = DateTime.Now
                };

                FarmLivestockService.Create(newFarmLivestock);
                return RedirectToAction("AddFarmLivestock", new { farmId = model.FarmId, BreedId = model.LivestockBreedId });
            }
            model.AnimalGenderDropDown = GetMyAnimalGender(model.GenderId);
            return View(model);

            //Local method to get a ducplicate livestock already added to 
            // a particular farm
            FarmLivestock LivestockAlreadyAdded(int breedId, int farmId) => FarmLivestockService.Get().Where(x => x.LivestockBreedId == breedId && x.FarmId == farmId).FirstOrDefault();
        }

        // GET: Crop/Details/5
        public ActionResult Details(int? farmLivestockId)
        {
            //var livestockPrice = LivestockPriceService.Get().Where(x => x.FarmLivestockId.Equals(farmLivestockId));

            //var model = new HomeViewModel
            //{
            //    LivestockPrice = livestockPrice.ToList()
            //};
            return View();
        }

        public ActionResult AddLivestockPrice(int? farmLivestockId, int? farmId)
        {
            var model = new LivestockPriceViewModel
            {
                FarmLivestockId = farmLivestockId.Value,
                FarmId = farmId.Value,
                MeasurementDropDown = base.GetMeasurement(null)
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

                LivestockPrice livestockPrice = LivestockPriceService.Create(pricing);

                if (livestockPrice != null)
                {
                    FarmLivestock updateFarmLivestock = FarmLivestockService.Get().Where(x => x.FarmId == model.FarmId).FirstOrDefault();
                    updateFarmLivestock.LivestockPriceId = livestockPrice?.Id;
                }
                return RedirectToAction("Index", "Livestock");
            }
            return RedirectToAction("Index", "Livestock");
        }

        //private void SaveLivestock(int farmId, int[] LivestockId)
        //{
        //    if (!LivestockId.Any())
        //        return;

        //    foreach (var cid in LivestockId)
        //    {
        //        SaveToFarmLivestock(farmId, cid);
        //    }
        //}

        //private static string GetConnectionString()
        //{
        //    return System.Configuration.ConfigurationManager.ConnectionStrings["RemoteClient"].ConnectionString;
        //}

        //private void SaveToFarmLivestock(int farmId, int LivestockId)
        //{
        //    using (System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(GetConnectionString()))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SaveLivestock", myConnection))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            myConnection.Open();

        //            cmd.Parameters.AddWithValue("@FarmId", farmId);
        //            cmd.Parameters.AddWithValue("@LivestockId", LivestockId);
        //            try
        //            {
        //                cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //                var msg = ex.Message;
        //            }
        //        }
        //    }
        //}


    }
}
