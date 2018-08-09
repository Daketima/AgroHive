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
        private IRepositoryService<Planting> PlantingService;
        private IRepositoryService<CropPrice> CropPriceService;
        private IRepositoryService<HarvestPeriod> HarvestMonthService;


        public CropController(IRepositoryService<CropVariety> cropVarietyService,
            IRepositoryService<FarmCrop> farmCropService, IRepositoryService<Planting> plantingService, IRepositoryService<CropPrice> cropPriceService, IRepositoryService<HarvestPeriod> harvestMonthService)
        {
            CropVarietyService = cropVarietyService;
            FarmCropService = farmCropService;
            PlantingService = plantingService;
            CropPriceService = cropPriceService;
            HarvestMonthService = harvestMonthService;
        }

        private IEnumerable<SelectListItem> GetCropDueMonths(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allMeasurement = HarvestMonthService.Get().ToList();
            allMeasurement.Insert(0, new HarvestPeriod { Id = 0, Name = "Please Select" });
            return allMeasurement.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private List<CropVariety> GetCropList()
        {
            return CropVarietyService.Get().ToList();
        }

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
            var model = new FarmCropViewModel
            {
                FarmDropDown = base.GetMyFarm(null)
            };
            return View(model);
        }


        public ActionResult CropHarvest(int farmCropId)
        {
            Planting _cropharvest = null;
            var all = PlantingService.Get().Where(x => x.FarmCropId.Value == farmCropId).ToList();

            if (all.Any())
            {
                _cropharvest = all.LastOrDefault();
            }

            var model = new FarmCropViewModel
            {
                Harvest = _cropharvest
            };

            //return PartialView("_CropHarvest", model);
            return View(model);
        }



        // GET: Crop/Create
        public ActionResult AddFarmCrop(int? farmId)
        {
            var model = new FarmCropViewModel
            {
                CropListItem = GetCropList(),
                FarmId = farmId.Value
            };
            return View(model);
        }

        // POST: Crop/Create
        [HttpPost]
        public ActionResult AddCropToFarm(int CropVarietyId, int FarmId)
        {
            //SaveCrop(model.FarmId, model.CropId);

            var farmCrop = new FarmCrop
            {
                FarmId = FarmId,
                CropVarietyId = CropVarietyId,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            FarmCropService.Create(farmCrop);

            return View("AddFarmCrop", new { farmId = FarmId });
        }

        // GET: Crop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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

        public ActionResult AddCropPrice(int? Id, int? FarmCropId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CropPrice, CropPriceViewModel>();
            });
            CropPriceViewModel model = null;
            IMapper iMapper = config.CreateMapper();

            if (Id.HasValue && Id.Value > 0)
            {
                CropPrice updateCropPrice = CropPriceService.GetById(Id.Value);
                model = iMapper.Map<CropPrice, CropPriceViewModel>(updateCropPrice);
                model.MeasurementDropDown = base.GetMeasurement(updateCropPrice.Id);

                return View(model);
            }

            model = iMapper.Map<CropPrice, CropPriceViewModel>(new CropPrice());
            model.FarmCropId = FarmCropId.Value;
            model.MeasurementDropDown = base.GetMeasurement(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCropPrice(CropPriceViewModel model)
        {
            if (ModelState.IsValid)
            {
                CropPrice cropPrice = new CropPrice
                {
                    MeasurementId = model.MeasurementId,
                    UnitPrice = model.UnitPrice,
                    DateCreated = DateTime.Now
                };

                cropPrice = CropPriceService.Create(cropPrice);

                if (cropPrice != null)
                {
                    FarmCrop updateFarmCrop = FarmCropService.GetById(model.FarmCropId);
                    updateFarmCrop.CropPriceId = cropPrice?.Id;

                    FarmCropService.Update(updateFarmCrop);
                }
                return RedirectToAction("Index", "Crop");
            }
            model.MeasurementDropDown = base.GetMeasurement(model.MeasurementId);
            return View(model);
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


        public ActionResult PlantCrop(int? farmCropId, int? id)
        {
            PlantingViewModel model = null;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Planting, PlantingViewModel>();
            });

            IMapper iMapper = config.CreateMapper();

            if (id.HasValue && id.Value > 0)
            {
                Planting editPlanting = PlantingService.GetById(id.Value);
                model = Mapper.Map<Planting, PlantingViewModel>(editPlanting);
                model.HarvestPeriodDropDown = GetCropDueMonths(null);
                return View(model);
            }

            model = iMapper.Map<Planting, PlantingViewModel>(new Planting());
            model.FarmCropId = farmCropId;
            model.HarvestPeriodDropDown = GetCropDueMonths(null);

            return View(model);
        }

        [HttpPost]
        public ActionResult PlantCrop(PlantingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var plantCrop = new Planting
                {
                    FarmCropId = model.FarmCropId,
                    Hectarage = model.Hectarage,
                    YieldPerHectar = model.YieldPerHectar,
                    DatePlanted = model.DatePlanted,
                    ExpectedHarvestDate = model.DatePlanted.AddMonths(model.MonthToGrowId)
                };

                PlantingService.Create(plantCrop);

                return RedirectToAction("Index");
            }
            return View(model);
        }


    }
}
