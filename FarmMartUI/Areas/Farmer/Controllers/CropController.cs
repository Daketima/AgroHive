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
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Areas.Farmer.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class CropController : SuperBaseController
    {
        private IRepositoryService<CropVariety> CropVarietyService;
        private IRepositoryService<FarmCrop> FarmCropService;
        private IRepositoryService<Crop> CropService;
        private IRepositoryService<CropType> CropTypeService;



        public CropController(IRepositoryService<CropVariety> cropVarietyService,
            IRepositoryService<FarmCrop> farmCropService, IRepositoryService<Planting> plantingService,  IRepositoryService<HarvestPeriod> harvestMonthService, IRepositoryService<Crop> cropService, IRepositoryService<CropType> cropTypeService)
        {
            CropVarietyService = cropVarietyService;
            FarmCropService = farmCropService;
            CropService = cropService;
            CropTypeService = cropTypeService;

        }

        private IEnumerable<SelectListItem> GetCrop(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCrop = CropService.Get().ToList();
            allCrop.Insert(0, new Crop { Id = 0, Name = "--Please Select--" });
            return allCrop.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCropType(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropType = CropTypeService.Get().OrderBy(x => x.Name).ToList();
            allCropType.Insert(0, new CropType { Id = 0, Name = "--Please Select--" });
            return allCropType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCropVariety(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropVariety = CropVarietyService.Get().ToList();
            allCropVariety.Insert(0, new CropVariety { Id = 0, Name = "Please Select" });
            return allCropVariety.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCropEmpty(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropType = new List<Crop>();
            allCropType.Insert(0, new Crop { Id = 0, Name = "Please Select" });
            return allCropType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCropVarietyEmpty(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropType = new List<CropVariety>();
            allCropType.Insert(0, new CropVariety { Id = 0, Name = "Please Select" });
            return allCropType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        [HttpPost]
        public ActionResult GetCropNew(int cropTypeId)
        {
            List<Crop> allCropType = CropService.Get().Where(x => x.CropTypeId == cropTypeId).OrderBy(x => x.Name).ToList();
            allCropType.Insert(0, new Crop { Id = 0, Name = "Please Select" });
            return Json(new SelectList(allCropType, "Id", "Name"));
        }

        [HttpPost]
        public ActionResult GetCropVarietyNew(int? cropId)
        {
            List<CropVariety> allCropVariety = CropVarietyService.Get().Where(x => x.CropId == cropId).OrderBy(x => x.Name).ToList();
            allCropVariety.Insert(0, new CropVariety { Id = 0, Name = "Please Select" });
            return Json(new SelectList(allCropVariety, "Id", "Name"));
        }

        private List<Crop> GetCropList() => CropService.Get().ToList();

        [HttpGet]
        public ActionResult GetFarmCropList(int? farmId)
        {
            var model = new FarmCropViewModel
            {
                FarmCropList = FarmCropService.Get().Where(x => x.FarmId == farmId.Value && x.Farm.IsActive).ToList(),
                FarmId = farmId.Value
            };
            return PartialView("_FarmCropList", model);
        }

        // GET: Crop
        public ActionResult Index(int? farmId)
        {
            string userId = User.Identity.GetUserId();

            var model = new FarmCropViewModel
            {
                FarmCropList = FarmCropService.Get().Where(
                    x => x.Farm.ApplicationUserId == userId && x.IsActive
                    ).ToList(),
                //FarmDropDown = base.GetMyFarm(null)
                CropDropDown = GetCrop(null),
                CropTypeDropDown = GetCropType(null),
                CropVarietyDropDown = GetCropVariety(null)
            };
            return View(model);
        }

        //public ActionResult CropHarvest(int farmCropId)
        //{
        //    Planting _cropharvest = null;
        //    var all = PlantingService.Get().Where(x => x.FarmCropId.Value == farmCropId).ToList();

        //    if (all.Any())
        //    {
        //        _cropharvest = all.LastOrDefault();
        //    }

        //    var model = new FarmCropViewModel
        //    {
        //        Harvest = _cropharvest
        //    };

        //    //return PartialView("_CropHarvest", model);
        //    return View(model);
        //}



        // GET: Crop/Create
        public ActionResult Add(int? farmId)
        {
            var model = new FarmCropViewModel
            {
                CropTypeDropDown = GetCropType(null),
                CropDropDown = GetCropEmpty(null),
                CropVarietyDropDown = GetCropVariety(null),
                FarmId = farmId.Value
            };
            return View(model);
        }

        // POST: Crop/Create
        [HttpPost]
        public ActionResult Add(FarmCropViewModel model)
        {
            //SaveCrop(model.FarmId, model.CropId);

            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    FarmCrop farmCropToUpdate = FarmCropService.GetById(model.Id);
                    farmCropToUpdate.Hectarage = model.Hectarage;
                    farmCropToUpdate.YieldPerHectar = model.YieldPerHectar;
                    farmCropToUpdate.CropVarietyNote = model.CropVarietyNote;
                    farmCropToUpdate.IsActive = true;

                    FarmCropService.Update(farmCropToUpdate);

                    return RedirectToAction("Index");
                }

                var farmCrop = new FarmCrop
                {
                    FarmId = model.FarmId,
                    CropVarietyId = model.CropVarietyId,
                    Hectarage = model.Hectarage,
                    YieldPerHectar = model.YieldPerHectar,
                    CropVarietyNote = model.CropVarietyNote,
                    IsActive = true
                };
                FarmCropService.Create(farmCrop);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Crop/Edit/5
        public PartialViewResult EditCrop(int FarmCropId)
        {
            FarmCrop farmCrop = FarmCropService.GetById(FarmCropId);

            FarmCropViewModel model = new FarmCropViewModel
            {
                Id = farmCrop.Id,
                CropId = farmCrop.CropVariety.Crop.Id,
                CropVarietyId = farmCrop.CropVariety.Crop.Id,
                CropTypeId = (int)farmCrop.CropVariety.Crop.CropTypeId,
                CropDropDown = GetCrop(farmCrop.CropVariety.Crop.Id),
                CropVarietyDropDown = GetCropVariety(farmCrop.CropVarietyId),
                CropTypeDropDown = GetCropType(farmCrop.CropVariety.Crop.CropTypeId)
            };
            return PartialView("_AddCropToFarmDialog", model);
        }


        public ActionResult Details(int? farmCropId)
        {
            //var cropPrice = CropPriceService.Get().Where(x => x.FarmCropId.Equals(farmCropId));

            //var model = new HomeViewModel
            //{
            //    CropPrice = cropPrice.ToList()
            //};
            return View();
        }

       


        //private void SaveCrop(int farmId, int[] CropId)
        //{
        //    if (!CropId.Any())
        //        return;

        //    foreach (var cid in CropId)
        //    {
        //        SaveToFarmCrop(farmId, cid);
        //    }
        //}

        //private static string GetConnectionString()
        //{
        //    return ConfigurationManager.ConnectionStrings["RemoteClient"].ConnectionString;
        //}

        //private void SaveToFarmCrop(int farmId, int CropId)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(GetConnectionString()))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SaveCrops", myConnection))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            myConnection.Open();

        //            cmd.Parameters.AddWithValue("@FarmId", farmId);
        //            cmd.Parameters.AddWithValue("@CropId", CropId);
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
