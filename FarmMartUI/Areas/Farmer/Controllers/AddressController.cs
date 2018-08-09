using FarmMartUI.helper;
using FarmMartBLL.ServiceAPI;
using FarmMartDAL.Model;
using FarmMartUI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

using System.Collections.Generic;
using AutoMapper;
using FarmMartBLL.Core;
using FarmMartUI.Areas.Farmer.Models;

namespace FarmMartUI.Areas.Farmer.Controllers
{
    [Authorize]
    public class AddressController : SuperBaseController
    {
        private readonly IRepositoryService<Address> AddressService;
        private readonly IRepositoryService<Farm> FarmService;
        private readonly IRepositoryService<State>  StateService;
        private readonly IRepositoryService<LocalGovernment> LocalGovermentService;

        public AddressController(IRepositoryService<Address> addressService, IRepositoryService<Farm> farmService, IRepositoryService<State> stateService, IRepositoryService<LocalGovernment> localGovermentService)
        {
            AddressService = addressService;
            FarmService = farmService;
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

        // GET: FarmAddress
        public ActionResult Index()
        {
            return View();
        }

        // GET: FarmAddress/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FarmAddress/Create
        public ActionResult AddFarmAddress(int? farmId)
        {
            var farm = FarmService.GetById(farmId.Value);

            if (farm.Address != null)
            {
                return RedirectToAction("EditFarmAddress", new {
                    addressId = farm.Address.Id
                });
            }

            var model = new AddressViewModel
            {
                FarmId = farmId.Value,
                LocalGovernmentAreaDropDown = GetLocalGovernmentEmpty(null),
                StateDropDown = GetState(null)
            };
            return View(model);
        }

        // POST: FarmAddress/Create
        [HttpPost]
        public ActionResult AddFarmAddress(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var farmAddress = new Address
                {
                    FullAddress = model.FullAddress,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    StateId = model.StateId,
                    LandMark = model.LandMark,
                    LocalGovermentId = model.LocalGovermentId,
                    DateCreated = DateTime.Now
                };

                var address = AddressService.Create(farmAddress);

                if (address != null)
                {
                    UpdateFarm(model.FarmId, address.Id);
                }
            }

            return RedirectToAction("Index", "Farm");
        }

        // GET: FarmAddress/Edit/5
        public ActionResult EditFarmAddress(int? addressId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressViewModel>();
            });

            IMapper iMapper = config.CreateMapper();

            var _address = AddressService.GetById(addressId.Value);

            var model = iMapper.Map<Address, AddressViewModel>(_address);
            model.LocalGovernmentAreaDropDown = GetLocalGovernment(_address.LocalGovermentId);
            model.StateDropDown = GetState(_address.StateId);

            return View("EditFarmAddress", model);
        }

        // POST: FarmAddress/Edit/5
        [HttpPost]
        public ActionResult EditFarmAddress(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var addrress = AddressService.GetById(model.Id);

                addrress.FullAddress = model.FullAddress;
                addrress.Longitude = model.Longitude;
                addrress.Latitude = model.Latitude;
                addrress.StateId = model.StateId;
                addrress.LocalGovermentId = model.LocalGovermentId;
                addrress.DateCreated = addrress.DateCreated;

                AddressService.Update(addrress);
            }
            return RedirectToAction("Index", "Farm");
        }

        // GET: FarmAddress/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FarmAddress/Delete/5
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

        private void UpdateFarm(int farmId, int AddressId)
        {
            var farm = FarmService.GetById(farmId);
            farm.AddressId = AddressId;
            FarmService.Update(farm);
        }
    }
}
