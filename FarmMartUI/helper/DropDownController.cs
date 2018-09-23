using FarmMartBLL.Core;
using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.helper
{
    public class DropDownController : Controller
    {
        //private readonly IRepositoryService<CropVariety> CropVarietyService;
        //private readonly IRepositoryService<Crop> CropService;
        //private readonly IRepositoryService<CropType> CropTypeService;
        
        // GET: DropDown
        public ActionResult Index()
        {
            return View();
        }

        // GET: DropDown/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DropDown/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DropDown/Create
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

        // GET: DropDown/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DropDown/Edit/5
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

        // GET: DropDown/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DropDown/Delete/5
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
