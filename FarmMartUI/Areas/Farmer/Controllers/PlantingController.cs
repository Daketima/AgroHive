using AutoMapper;
using FarmMartBLL.Core;
using FarmMartDAL.Model;
using FarmMartUI.Areas.Farmer.Models;
using FarmMartUI.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Areas.Farmer.Controllers
{
    public class PlantingController : SuperBaseController
    {
        private readonly IRepositoryService<FarmCrop> FarmCropService;
        private IRepositoryService<Planting> PlantingService;
        private IRepositoryService<HarvestPeriod> HarvestMonthService;

        public PlantingController(IRepositoryService<FarmCrop> farmCropService, IRepositoryService<Planting> plantingService, IRepositoryService<HarvestPeriod> harvestMonthService)
        {
            FarmCropService = farmCropService;
            PlantingService = plantingService;
            HarvestMonthService = harvestMonthService;
        }

        private IEnumerable<SelectListItem> GetCropDueMonth(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropDueMonth = HarvestMonthService.Get().ToList();
            allCropDueMonth.Insert(0, new HarvestPeriod { Id = 0, Name = "Please Select" });
            return allCropDueMonth.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        // GET: Farmer/Planting
        public ActionResult Index()
        {
            return View();
        }

        // GET: Farmer/Planting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Farmer/Planting/Create
        [HttpGet]
        public ActionResult PlantCrop(int? farmCropId)
        {
            PlantingViewModel model = null;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Planting, PlantingViewModel>();
            });

            IMapper iMapper = config.CreateMapper();

            model = iMapper.Map<Planting, PlantingViewModel>(new Planting{});
            model.FarmCropId = (int)farmCropId;
            model.HarvestPeriodDropDown = GetCropDueMonth(null);

            //return PartialView("_AddCropPlantingDialog", model);
            return View(model);
        }

        // POST: Farmer/Planting/Create
        [HttpPost]
        public ActionResult PlantCrop(PlantingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {

                    Planting existingplanting = PlantingService.GetById(model.Id);

                    existingplanting.DatePlanted = model.DatePlanted;
                    existingplanting.ExpectedHarvestDate = model.DatePlanted.AddMonths(model.MonthToGrowId);

                    PlantingService.Update(existingplanting);

                   return RedirectToAction("Index", "Crop");
                }

                Planting planting = new Planting
                {
                    DatePlanted = model.DatePlanted,
                    ExpectedHarvestDate = model.DatePlanted.AddMonths(model.MonthToGrowId),
                    FarmCropId = model.Id
                };

                planting = PlantingService.Create(planting);

                if (planting != null)
                {
                    FarmCrop updateFarmCrop = FarmCropService.GetById(model.FarmCropId);
                    updateFarmCrop.PlantingId = planting?.Id;

                    FarmCropService.Update(updateFarmCrop);
                }
                return RedirectToAction("Index", "Crop");
            }
            return RedirectToAction("Index", "Crop");
        }

        public ActionResult UdatePlanting(int? id)
        {
            PlantingViewModel model = null;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Planting, PlantingViewModel>();
            });

            IMapper iMapper = config.CreateMapper();

            Planting editPlanting = PlantingService.GetById(id);
            model = iMapper.Map<Planting, PlantingViewModel>(editPlanting);
            
            model.MonthToGrowId = editPlanting.ExpectedHarvestDate.Month - editPlanting.DatePlanted.Month;
            model.HarvestPeriodDropDown = GetCropDueMonth(model.MonthToGrowId);
            // return PartialView("_AddCropPlantingDialog", model);
            return View(model);
        }


        // POST: Farmer/Planting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Farmer/Planting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Farmer/Planting/Delete/5
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
    }
}
