using FarmMartBLL.Core;
using FarmMartDAL.Model;
using FarmMartUI.Areas.Farmer.Models;
using FarmMartUI.helper;
using FarmMartUI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Areas.Consumer.Controllers
{
    public class ConsumerController : SuperBaseController
    {
       
        readonly IRepositoryService<Address> AddressService;
        private readonly IRepositoryService<State> StateService;
        private readonly IRepositoryService<LocalGovernment> LocalGovermentService;

        public ConsumerController(IRepositoryService<Address> addressService, IRepositoryService<State> stateService, IRepositoryService<LocalGovernment> localGovermentService)
        {
            AddressService = addressService;
            StateService = stateService;
            LocalGovermentService = localGovermentService;
        }

        [HttpPost]
        public ActionResult GetLgasNew(int? id)
        {
            var lgas = LocalGovermentService.Get().Where(x => x.StateId == id.Value).ToList();
            lgas.Insert(0, new LocalGovernment { Id = 0, Name = "--Please Select--" });
            return Json(new SelectList(lgas, "Id", "Name"));
        }

        private IEnumerable<SelectListItem> GetLocalGovernmentEmpty(int? selectedId)
        {
            if (selectedId == null)

                selectedId = 0;

            var allLocalGovernment = new List<LocalGovernment>();

            return allLocalGovernment.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selectedId });
        }

        private IEnumerable<SelectListItem> GetLocalGovernment(int? selectedId)
        {
            if (!selectedId.HasValue)

                selectedId = 0;

            var allLocalGovernment = LocalGovermentService.Get().ToList();

            allLocalGovernment.Insert(0, new LocalGovernment { Id = 0, Name = "--Please Select--" });
            return allLocalGovernment.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selectedId });
        }

        private IEnumerable<SelectListItem> GetState(int? selectedId)
        {
            if (selectedId == null)

                selectedId = 0;

            var allState = StateService.Get().ToList();

            allState.Insert(0, new State { Id = 0, Name = "--Please Select--" });
            return allState.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selectedId });
        }
        // GET: Logistics/Consumer
        public ActionResult Index()
        {
            return View();
        }

        // GET: Logistics/Consumer/Details/5
        public ActionResult AddAddress()
        {
            string user = User.Identity.GetUserId();

            if (thisUser.AddressId != null)
            {
                return RedirectToAction("EditFarmAddress", new
                {
                    addressId = thisUser.AddressId
                });
            }

            var model = new AddressViewModel
            {
                LocalGovernmentAreaDropDown = GetLocalGovernmentEmpty(null),
                StateDropDown = GetState(null),
                ApplicationUserId = thisUser.Id
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddAddress(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var farmAddress = new Address
                {
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    StateId = model.StateId,
                    LocalGovermentId = model.LocalGovermentId,
                    DateCreated = DateTime.Now
                };

                var address = AddressService.Create(farmAddress);

                if (address != null)
                {
                    thisUserFarm = farm
                    thisUser.AddressId = address.Id;
                    PersonService.Update(thisUser);
                }
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Logistics/Consumer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logistics/Consumer/Create
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

        // GET: Logistics/Consumer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Logistics/Consumer/Edit/5
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

        // GET: Logistics/Consumer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Logistics/Consumer/Delete/5
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
