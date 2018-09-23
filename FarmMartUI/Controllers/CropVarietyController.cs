using FarmMartBLL.Core;
using FarmMartDAL.Model;
using FarmMartUI.Areas.Farmer.Models;
using FarmMartUI.helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Controllers
{
    public class CropVarietyController : SuperBaseController
    {
        private IRepositoryService<CropVariety> CropVarietyService;

        public CropVarietyController(IRepositoryService<CropVariety> cropVarietyService) => CropVarietyService = cropVarietyService;

        // GET: Farmer/CropVariety
        public ActionResult Index()
        {
            var model = new CropViewModel
            {
                AllCropVariety = CropVarietyService.Get().ToList()
            };
            return View(model);
        }

        public ActionResult AddACropVariety()
        {
            var model = new CropViewModel
            {

            };
            return View(model);
        }

        // POST: Crop/Create
        [HttpPost]
        public ActionResult AddACropVariety(CropViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    var cropVarity = new CropVariety
                    {
                        Name = model.Name,
                        PhotoPath = "upload.png",
                        Note = model.Description,
                        DateCreated = DateTime.Now,

                    };
                    cropVarity = CropVarietyService.Create(cropVarity);

                    if (cropVarity != null)
                    {
                       base.SaveCropImage(cropVarity);
                    }

                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Index", "FarmCrop");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Farmer/CropVariety/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Farmer/CropVariety/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Farmer/CropVariety/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Farmer/CropVariety/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Farmer/CropVariety/Edit/5
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

        // GET: Farmer/CropVariety/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Farmer/CropVariety/Delete/5
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
