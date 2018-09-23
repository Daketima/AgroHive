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
    public class PricingController : SuperBaseController
    {
        private IRepositoryService<CropPrice> CropPriceService;
        private IRepositoryService<FarmCrop> FarmCropService;

        public PricingController(IRepositoryService<CropPrice> cropPriceService, IRepositoryService<FarmCrop> farmCropService)
        {
            CropPriceService = cropPriceService;
            FarmCropService = farmCropService;
        }
        // GET: Farmer/Pricing
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: Farmer/Pricing/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Farmer/Pricing/Create
        public ActionResult AddCropPrice(int? farmCropId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CropPrice, PriceViewModel>();
            });
            PriceViewModel model = null;
            IMapper iMapper = config.CreateMapper();

            //if (Id.HasValue && Id.Value > 0)
            //{
            //    CropPrice updateCropPrice = CropPriceService.GetById(Id.Value);
            //    model = iMapper.Map<CropPrice, PriceViewModel>(updateCropPrice);
            //    model.MeasurementDropDown = base.GetMeasurement(updateCropPrice.Id);

            //    return PartialView(model);
            //}

            model = iMapper.Map<CropPrice, PriceViewModel>(new CropPrice());
            model.FarmCropId = farmCropId.Value;
            model.MeasurementDropDown = base.GetMeasurement(null);
            return PartialView("_AddCropPricing", model);
        }

        // POST: Farmer/Pricing/Create
        [HttpPost]
        public ActionResult AddCropPrice(PriceViewModel model)
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

        // GET: Farmer/Pricing/Edit/5
        public ActionResult EditPrice(int id)
        {
            return View();
        }

        // POST: Farmer/Pricing/Edit/5
        [HttpPost]
        public ActionResult Edit(PriceViewModel model)
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

        // GET: Farmer/Pricing/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Farmer/Pricing/Delete/5
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
