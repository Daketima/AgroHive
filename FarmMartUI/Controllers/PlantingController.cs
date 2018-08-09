using FarmMartBLL.ServiceAPI;
using FarmMartDAL.Model;
using FarmMartUI.Areas.Farmer.Models;
using FarmMartUI.helper;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Controllers
{
    
    public class PlantingController : SuperBaseController
    {
        private PlantingService _plantingService;
        private PlantingPeriodService _harvestPeriodService;

        public PlantingController()
        {
            _plantingService = new PlantingService();
            _harvestPeriodService = new PlantingPeriodService();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _plantingService != null)
            {
                _plantingService.Dispose();
                _plantingService = null;
            }

            if (disposing && _harvestPeriodService != null)
            {
                _harvestPeriodService.Dispose();
                _harvestPeriodService = null;
            }
        }


        private IEnumerable<SelectListItem> GetCropDueMonths(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allMeasurement = _harvestPeriodService.Get().ToList();
            allMeasurement.Insert(0, new HarvestPeriod { Id = 0, Name = "Please Select" });
            return allMeasurement.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }


        // GET: Planting
        public ActionResult Index()
        {
            return View();
        }

        // GET: Planting/Details/5
        public ActionResult Details(int farmCropId)
        {
            PlantingViewModel model = new PlantingViewModel
            {
                PlantingDetail = _plantingService.Get().Where(x => x.FarmCropId == farmCropId).ToList()
        };
            
            return View(model);
        }

        // GET: Planting/Create
        public ActionResult PlantCrop(int? farmCropId)
        {
            var model = new PlantingViewModel
            {
                FarmCropId = farmCropId.Value,
                HarvestPeriodDropDown = GetCropDueMonths(null)
            };

            return View(model);
        }

        // POST: Planting/Create
        [HttpPost]
        public ActionResult PlantCrop(PlantingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var planting = new Planting
                {
                    FarmCropId = model.FarmCropId,
                    DatePlanted = model.DatePlanted,
                    ExpectedHarvestDate = model.DatePlanted.AddMonths(model.MonthToGrowId)
                };

                _plantingService.Create(planting);

                return RedirectToAction("Index", "FarmCrop");
            }
            return RedirectToAction("Index", "FarmCrop");
        }

        // GET: Planting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Planting/Edit/5
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

        // GET: Planting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Planting/Delete/5
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
