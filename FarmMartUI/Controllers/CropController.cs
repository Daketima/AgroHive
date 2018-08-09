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
    [Authorize]
    public class CropController : SuperBaseController
    {
        
        private IRepositoryService<Crop> CropService;

        public CropController(IRepositoryService<Crop> cropService) => CropService = cropService;

        // GET: Crop
        public ActionResult Index()
        {
            var model = new CropViewModel
            {
                AllCrop = CropService.Get().ToList()
            };
            return View(model);
        }

        // GET: Crop/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Crop/Create
        public ActionResult AddCrop()
        {
            var model = new CropViewModel
            {

            };
            return View(model);
        }

        // POST: Crop/Create
        [HttpPost]
        public ActionResult AddCrop(CropViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    var crop = new Crop
                    {
                        Name = model.Name,
                        
                        Description = model.Description,
                        DateCreated = DateTime.Now,

                    };
                    crop = CropService.Create(crop);

                    //if (crop != null)
                    //{
                    //    SaveCropImage(crop);
                    //}

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

        // GET: Crop/Edit/5
        public ActionResult EditCrop(int cropId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Crop, CropViewModel>();
            });

            IMapper iMapper = config.CreateMapper();

            var crop = CropService.GetById(cropId);

            var model = iMapper.Map<Crop, CropViewModel>(crop);

            return View(model);
        }

        // POST: Crop/Edit/5
        [HttpPost]
        public ActionResult EditCrop(CropViewModel model)
        {
            try
            {
                var crop = CropService.GetById(model.Id);
                crop.Name = model.Name;
                crop.Description = model.Description;
                CropService.Update(crop);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Crop/Delete/5
        public ActionResult DeleteCrop(int id)
        {
            return View();
        }

        // POST: Crop/Delete/5
        [HttpPost]
        public ActionResult DeleteCrop(int id, FormCollection collection)
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
