using FarmMartBLL.ServiceAPI;
using FarmMartDAL.Model;
using FarmMartUI.helper;
using FarmMartUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Controllers
{
    /// <summary>
    /// This is an example controller using the AdminLTE NuGet package's CSHTML templates, CSS, and JavaScript
    /// You can delete these, or use them as handy references when building your own applications
    /// </summary>
    [Authorize]
    public class AdminLteController : SuperBaseController
    {
        /// <summary>
        /// The home page of the AdminLTE demo dashboard, recreated in this new system
        /// </summary>
        /// <returns></returns>
        private FarmService farmService;
        private CropService cropService;
        ApplicationDbContext db = new ApplicationDbContext();

        public AdminLteController()
        {
            farmService = new FarmService();
            cropService = new CropService();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && farmService != null)
            {
                farmService.Dispose();
                farmService = null;
            }

            if (disposing && cropService != null)
            {
                cropService.Dispose();
                cropService = null;
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The color page of the AdminLTE demo, demonstrating the AdminLte.Color enum and supporting methods
        /// </summary>
        /// <returns></returns>
        public ActionResult Colors()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            HomeViewModel model = null;

            if (User.IsInRole("Admin"))
            {
                model = new HomeViewModel
                {
                    Farms = farmService.Get().Where(x => x.IsActive).ToList()
                };
            }
            else
            {
                string userId = User.Identity.GetUserId();
                ApplicationUser thisUser = db.Users.FirstOrDefault(x => x.Id == userId);

                model = new HomeViewModel
                {
                    Farms = farmService.Get().Where(x => x.ApplicationUserId == userId).ToList()
                };

                if (User.IsInRole("Farmer"))
                {
                    return RedirectToAction("Index", "Farm", new { area = "Farmer" });
                }
                if (User.IsInRole("Logistics"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Logistics" });
                }
                if (User.IsInRole("Research Institute"))
                {
                    return RedirectToAction("Index", "Home", new { area = "ResearchInstitute" });
                }
            }
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult ProfileDropdown()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser thisPerson = db.Users.FirstOrDefault(x => x.Id == userId);

            var person = new HomeViewModel
            {
                ThisUser = thisPerson
            };
            return PartialView("_UserProfileDropdown", person);
        }

        //public ActionResult UpdateProfile()
        //{
        //    string userId = User.Identity.GetUserId();
        //    ApplicationUser thisPerson = db.Users.FirstOrDefault(x => x.Id == userId);

        //    var model = new ProfileViewModel
        //    {
        //        ApplicationUserId = thisPerson.Id
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult UpdateProfile(ProfileViewModel model)
        //{
        //    var personToUpdate = personService.GetById(model.ApplicationUserId);
        //    personToUpdate.CompanyName = model.CompanyName;
        //    personService.Update(personToUpdate);

        //    return Redirect(Request.UrlReferrer.ToString());
        //}


    }
}



