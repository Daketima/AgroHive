using AutoMapper;
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
        private readonly IRepositoryService<LivestockPrice> LivestockPriceService;
        private IRepositoryService<LivestockType> LivestockTypeService;
        private IRepositoryService<AnimalGender> GenderService;

        public LivestockController(IRepositoryService<Livestock> livestockService, IRepositoryService<FarmLivestock> farmLivestockService, IRepositoryService<LivestockPrice> livestockPriceService, IRepositoryService<LivestockType> livestockTypeService, IRepositoryService<AnimalGender> genderService)
        {
            LivestockService = livestockService;
            FarmLivestockService = farmLivestockService;
            LivestockPriceService = livestockPriceService;
            LivestockTypeService = livestockTypeService;
            GenderService = genderService;
        }

        private List<Livestock> GetLivestockList() => LivestockService.Get().ToList();



        public IEnumerable<SelectListItem> GetLivestockType(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allLivestockType = LivestockTypeService.Get().ToList();
            allLivestockType.Insert(0, new LivestockType { Id = 0, Name = "Please Select" });
            return allLivestockType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetLivestockEmpty(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allLivestock = new List<Livestock>();
            allLivestock.Insert(0, new Livestock { Id = 0, Name = "Please Select" });
            return allLivestock.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetLivestock(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allLivestock = LivestockService.Get().ToList();
            allLivestock.Insert(0, new Livestock { Id = 0, Name = "Please Select" });
            return allLivestock.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        [HttpPost]
        public ActionResult GetLivestockNew(int livestockTypeId)
        {
            List<Livestock> allCropType = LivestockService.Get().Where(x => x.LivestockTypeId == livestockTypeId).ToList();
            allCropType.Insert(0, new Livestock { Id = 0, Name = "Please Select" });
            return Json(new SelectList(allCropType, "Id", "Name"));
        }

        private IEnumerable<SelectListItem> GetAnimalGender(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allAnimalGender = GenderService.Get().ToList();
            allAnimalGender.Insert(0, new AnimalGender { Id = 0, Name = "Please Select" });
            return allAnimalGender.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        [HttpGet]
        public ActionResult GetFarmLivestockList(int? farmId)
        {
            FarmLivestockViewModel model = new FarmLivestockViewModel
            {
                MyFarmLivestock = FarmLivestockService.Get().Where(x => x.FarmId == farmId.Value && x.Farm.IsActive).ToList()
            };
            return PartialView("_FarmLivestockList", model);
        }

        // GET: Livestock
        public ActionResult Index(int? farmId)
        {
            string userId = User.Identity.GetUserId();

            FarmLivestockViewModel model = new FarmLivestockViewModel
            {
                MyFarmLivestock = FarmLivestockService.Get().Where(
                    x => x.Farm.ApplicationUserId == userId && x.IsActive
                    ).ToList()
            };
            return View(model);
        }

        // GET: Livestock/Create
        public ActionResult AddFarmLivestock(int? farmId)
        {
            var model = new FarmLivestockViewModel
            {

                FarmId = farmId.Value,
                AnimalGenderDropDown = GetMyAnimalGender(null),
               /// LivestockTypeDropDown = GetLivestockType(null),
                //LivestockDropDown = GetLivestockEmpty(null)
                LivestockDropDown = GetLivestock(null)
            };
            return View(model);
        }

        //// GET: Livestock/Create
        //public ActionResult AddLivestockToFarm(int farmId, int? BreedId)
        //{
        //    var model = new FarmLivestockViewModel
        //    {
        //        FarmId = farmId,
        //        AnimalGenderDropDown = base.GetMyAnimalGender(null),
        //        LivestockBreedId = BreedId.Value
        //    };
        //    return View(model);
        //}

        [HttpPost]
        public ActionResult AddLivestockToFarm(FarmLivestockViewModel model)
        {

            if (ModelState.IsValid)
            {
                if(model.Id > 0)
                {
                    FarmLivestock updateDetail = FarmLivestockService.GetById(model.Id);

                    updateDetail.Breed = model.Breed;
                    updateDetail.LivestockId = model.LivestockId;
                    updateDetail.Population = model.Population;
                    updateDetail.QuantityAvailable = model.QuantityAvailable;
                    updateDetail.Weight = model.Weight;
                    updateDetail.GenderId = model.GenderId;
                    updateDetail.HitMarketDate = model.HitMarketDate;
                    updateDetail.Other = model.Other;
                    FarmLivestockService.Update(updateDetail);

                    return RedirectToAction("Index", new { farmid = model.FarmId });
                }
                var newFarmLivestock = new FarmLivestock
                {
                    Breed = model.Breed,
                    FarmId = model.FarmId,
                    GenderId = model.GenderId,
                    Population = model.Population,
                    QuantityAvailable = model.QuantityAvailable,
                    Weight = model.Weight,
                    HitMarketDate = model.HitMarketDate,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    Other = model.Other,
                    LivestockId = model.LivestockId,
                    IsAvailable = false
                    
                };

                FarmLivestockService.Create(newFarmLivestock);
                return RedirectToAction("Index", new { farmid = model.FarmId});
            }
            model.AnimalGenderDropDown = GetMyAnimalGender(model.GenderId);
            return View(model);
        }


        public ActionResult UpdateDetail(int id)
        {
            FarmLivestockViewModel model = null;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FarmLivestock, FarmLivestockViewModel>()
                .ReverseMap();
            });

            IMapper iMapper = config.CreateMapper();

            FarmLivestock thisLivestock = FarmLivestockService.GetById(id);

            model = iMapper.Map<FarmLivestock, FarmLivestockViewModel>(thisLivestock);
            //model.LivestockTypeId = thisLivestock.Livestock.LivestockTypeId.Value;
            //model.LivestockTypeDropDown = GetLivestockType(model.LivestockTypeId);
            model.LivestockDropDown = GetLivestock(thisLivestock.LivestockId);
            model.AnimalGenderDropDown = GetAnimalGender(thisLivestock.GenderId);
            return View("AddFarmLivestock", model);
        }


        // GET: Crop/Details/5
        public JsonResult Delete(int id)
        {
            FarmLivestock deleteFarmlivestock = FarmLivestockService.GetById(id);
            deleteFarmlivestock.IsActive = false;

            FarmLivestockService.Update(deleteFarmlivestock);
            
            return Json(new { Message = "Deleted"},JsonRequestBehavior.AllowGet);
        }

        public JsonResult Discontinue(int id)
        {
            FarmLivestock discontinueFarmlivestock = FarmLivestockService.GetById(id);
            discontinueFarmlivestock.IsAvailable = false;

            FarmLivestockService.Update(discontinueFarmlivestock);


            return Json(new { Message = "Discontinued" }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult AddLivestockPrice(int? farmLivestockId, int? farmId)
        //{
        //    var model = new LivestockPriceViewModel
        //    {
        //        FarmLivestockId = farmLivestockId.Value,
        //        FarmId = farmId.Value,
        //        MeasurementDropDown = base.GetMeasurement(null)
        //    };
        //    return View(model);
        //}



        // POST: Pricing/Create
        //[HttpPost]
        //public ActionResult AddLivestockPrice(LivestockPriceViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var pricing = new LivestockPrice
        //        {
        //            MeasurementId = model.MeasurementId,
        //            UnitPrice = model.UnitPrice,
        //            DateCreated = DateTime.Now
        //        };

        //        LivestockPrice livestockPrice = LivestockPriceService.Create(pricing);

        //        if (livestockPrice != null)
        //        {
        //            FarmLivestock updateFarmLivestock = FarmLivestockService.Get().Where(x => x.FarmId == model.FarmId).FirstOrDefault();
        //            updateFarmLivestock.LivestockPriceId = livestockPrice?.Id;
        //        }
        //        return RedirectToAction("Index", "Livestock");
        //    }
        //    return RedirectToAction("Index", "Livestock");
        //}

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
