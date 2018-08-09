using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Areas.Logistics.Controllers
{
    public class HomeController : Controller
    {
        // GET: Logistics/Home
        public ActionResult Index()
        {
            var model = new HomeViewModel
            {

            };
            return View(model);
        }

        // GET: Logistics/Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Logistics/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logistics/Home/Create
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

        // GET: Logistics/Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Logistics/Home/Edit/5
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

        // GET: Logistics/Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Logistics/Home/Delete/5
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
