using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using FarmMartBLL.ServiceAPI;
using FarmMartUI.Models;
using FarmMartDAL.Model;
using FarmMartUI.helper;
using System.Collections.Generic;
using AutoMapper;
using System.IO;
using FarmMartUI.Areas.Farmer.Models;
using FarmMartBLL.Core;

namespace FarmMartUI.Areas.Farmer.Controllers
{
    
    public class FarmController : Controller
    {
        private readonly IRepositoryService<Farm> FarmService;
       
        private readonly IRepositoryService<Crop> CropService;
        private readonly IRepositoryService<State> StateService;
        private readonly IRepositoryService<LocalGovernment> LocalGovernmentService;
        private readonly IRepositoryService<Livestock> LivestockService;
        private readonly IRepositoryService<HarvestPeriod> HarvestMonthService;
        private readonly IRepositoryService<CropType> CropTypeService;
        private readonly IRepositoryService<CropVariety> CropVarietyService;
        private readonly IRepositoryService<AnimalGender> AnimalGenderService;
        private readonly IRepositoryService<LivestockType> LivestockTypeService;
        private readonly IRepositoryService<FarmSizeUnit> FarmSizeUnitService;


        ApplicationDbContext db = new ApplicationDbContext();
        public FarmController(IRepositoryService<Farm> farmService,IRepositoryService<Crop> cropService, IRepositoryService<State> stateService, IRepositoryService<LocalGovernment> localGovernmentService, IRepositoryService<Livestock> livestockService, IRepositoryService<HarvestPeriod> harvestMonthService, IRepositoryService<CropType> cropTypeService, IRepositoryService<CropVariety> cropVarietyService, IRepositoryService<AnimalGender> animalGenderService, IRepositoryService<LivestockType> livestockTypeService, IRepositoryService<FarmSizeUnit> farmSizeUnitService)
        {
            FarmService = farmService;
            CropService = cropService;
            StateService = stateService;
            LocalGovernmentService = localGovernmentService;
            LivestockService = livestockService;
            HarvestMonthService = harvestMonthService;
            CropTypeService = cropTypeService;
            CropVarietyService = cropVarietyService;
            AnimalGenderService = animalGenderService;
            LivestockTypeService = livestockTypeService;
            FarmSizeUnitService = farmSizeUnitService;
        }


        private IEnumerable<SelectListItem> GetFarmSizeUnit(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allFarmSizeUnit = FarmSizeUnitService.Get();
            allFarmSizeUnit.Insert(0, new FarmSizeUnit { Id = 0, Name = "Please Select" });
            return allFarmSizeUnit.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCrop(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCrop = CropService.Get().ToList();
            allCrop.Insert(0, new Crop { Id = 0, Name = "Please Select" });
            return allCrop.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCropType(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropType = CropTypeService.Get().ToList();
            allCropType.Insert(0, new CropType { Id = 0, Name = "Please Select" });
            return allCropType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCropVariety(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropVariety = CropVarietyService.Get().ToList();
            allCropVariety.Insert(0, new CropVariety { Id = 0, Name = "Please Select" });
            return allCropVariety.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCropEmpty(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropType = new List<Crop>();
            allCropType.Insert(0, new Crop { Id = 0, Name = "Please Select" });
            return allCropType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetCropVarietyEmpty(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allCropType = new List<CropVariety>();
            allCropType.Insert(0, new CropVariety { Id = 0, Name = "Please Select" });
            return allCropType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetAnimalGender(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allAnimalGender = AnimalGenderService.Get();
            allAnimalGender.Insert(0, new AnimalGender { Id = 0, Name = "Please Select" });
            return allAnimalGender.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        [HttpGet]
        public ActionResult GetCropNew(int cropTypeId)
        {
            List<Crop> allCropType = CropService.Get().Where(x => x.CropTypeId == cropTypeId).ToList();
            allCropType.Insert(0, new Crop { Id = 0, Name = "Please Select" });
            return Json(new SelectList(allCropType, "Id", "Name"));
        }

        public ActionResult GetCropVarietyNew(int? cropId)
        {
            List<CropVariety> allCropVariety = CropVarietyService.Get().Where(x => x.CropId == cropId).ToList();
            allCropVariety.Insert(0, new CropVariety { Id = 0, Name = "Please Select" });
            return Json(new SelectList(allCropVariety, "Id", "Name"));
        }

        public IEnumerable<SelectListItem> GetLivestockType(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allLivestockType = LivestockTypeService.Get().ToList();
            allLivestockType.Insert(0, new LivestockType { Id = 0, Name = "Please Select" });
            return allLivestockType.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetLivestockEmpty(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allLivestock = new List<Livestock>();
            allLivestock.Insert(0, new Livestock { Id = 0, Name = "Please Select" });
            return allLivestock.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        private IEnumerable<SelectListItem> GetLivestock(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allLivestock = LivestockService.Get().ToList();
            allLivestock.Insert(0, new Livestock { Id = 0, Name = "Please Select" });
            return allLivestock.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        [HttpPost]
        public ActionResult GetLivestockNew(int livestockTypeId)
        {
            List<Livestock> allCropType = LivestockService.Get().Where(x => x.LivestockTypeId == livestockTypeId).ToList();
            allCropType.Insert(0, new Livestock { Id = 0, Name = "Please Select" });
            return Json(new SelectList(allCropType, "Id", "Name"));
        }

       

        private List<Crop> GetCropList() => CropService.Get().ToList();

        public void SaveCropFarm(Farm farm)
        {
            if (Request.Files.Count > 0)
            {
                int fileCount = Request.Files.Count;

                for (int i = 0; i < fileCount; i++)
                {
                    var file = Request.Files[i];

                    if (file.ContentLength == 0)
                        break;


                    var fileName = farm.Id.ToString() + "_" + i.ToString() + "_" + Path.GetFileName(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/FarmProfile"), fileName);

                    try
                    {
                        file.SaveAs(path);
                        farm.PhotoPath = fileName;
                        FarmService.Update(farm);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        // GET: Farm


        public ActionResult Index()
        {
            FarmViewModel model;

            if (User.IsInRole("Admin"))
            {
                model = new FarmViewModel
                {
                    MyFarm = FarmService.Get().ToList(),

                    FarmCrop = new FarmCropViewModel
                    {
                        CropListItem = GetCropList()
                    },
                    FarmLivestock = new FarmLivestockViewModel
                    {
                        LivestockListItem = LivestockService.Get().ToList(),
                        AnimalGenderDropDown = GetAnimalGender(null)
                    }
                };
            }
            else
            {
                string userId = User.Identity.GetUserId();
                

                var thisUserFarm = FarmService.Get().Where(x => x.ApplicationUserId == userId && x.IsActive).ToList();

                model = new FarmViewModel
                {
                    ThisUser = db.Users.FirstOrDefault(x => x.Id == userId),
                    MyFarm = thisUserFarm,
                    FarmCrop = new FarmCropViewModel
                    {
                        CropListItem = GetCropList(),
                        CropDropDown = GetCropEmpty(null),
                        CropVarietyDropDown = GetCropVarietyEmpty(null),
                        CropTypeDropDown = GetCropType(null)
                    },
                    FarmLivestock = new FarmLivestockViewModel
                    {
                        AnimalGenderDropDown = GetAnimalGender(null),
                        LivestockTypeDropDown = GetLivestockType(null),
                        LivestockDropDown = GetLivestockEmpty(null)
                    }
                };
            }
            return View(model);
        }

        // GET: Farm/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Farm/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser thisUser = db.Users.FirstOrDefault(x => x.Id == userId);
            var thisUserFarm = FarmService.Get().Where(x => x.ApplicationUserId == userId).ToList();

            var model = new FarmViewModel
            {
                ApplicationUserId = userId,
                EmailAddress = thisUser.Email,
                FarmSizeUnitDropDown = GetFarmSizeUnit(null)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FarmViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newFarm = new Farm
                    {
                        ApplicationUserId = model.ApplicationUserId,
                        FarmName = model.FarmName,
                        EmailAddress = model.EmailAddress,
                        PhotoPath = "fm.jpg",
                        Size = model.Size,
                        IsActive = true,
                        DateCreated = DateTime.Now,
                        IsVerified = false,
                        FarmSideUnitId = model.FarmSizeUnitId
                    };

                    newFarm = FarmService.Create(newFarm);

                    if (newFarm != null)
                    {
                        SaveCropFarm(newFarm);
                    }
                    return RedirectToAction("Add", "Address", new { farmId = newFarm.Id });
                }
                catch
                {
                    model.FarmSizeUnitDropDown = GetFarmSizeUnit(null);
                    return View();
                }
            }
            model.FarmSizeUnitDropDown = GetFarmSizeUnit(null);
            return View();
        }

        // GET: Farm/Edit/5
        public ActionResult EditFarm(int? farmId)
        {
            FarmViewModel model = null;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Farm, FarmViewModel>()
                .ForMember( x => x.FarmCrop, opt => opt.Ignore())
                .ForMember(x => x.FarmLivestock, opt => opt.Ignore())
                .ForMember(x => x.PersonFarm, opt => opt.Ignore())
                .ForMember(x => x.ThisUser, opt => opt.Ignore())
                .ForMember(x => x.MyFarm, opt => opt.Ignore());
            });

            IMapper iMapper = config.CreateMapper();

            Farm updateFarm = FarmService.GetById(farmId.Value);

            model = iMapper.Map<Farm, FarmViewModel>(updateFarm);
            model.FarmSizeUnitDropDown = GetFarmSizeUnit(updateFarm.FarmSideUnitId.HasValue == true ? updateFarm.FarmSideUnitId.Value : 1);

            return View(model);
        }

        // POST: Farm/Edit/5
        [HttpPost]
        public ActionResult EditFarm(FarmViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here

                var farm = FarmService.GetById(model.Id);

                farm.FarmName = model.FarmName;
                farm.IsVerified = model.IsVerified;
                farm.PhotoPath = model.PhotoPath;
                farm.IsActive = farm.IsActive;
                farm.FarmSideUnitId = model.FarmSizeUnitId;
                farm.Size = model.Size;

                FarmService.Update(farm);

                SaveCropFarm(farm);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Farm/Delete/5
        public ActionResult Verify(int? farmId)
        {
            var farm = FarmService.GetById(farmId.Value);
            farm.IsVerified = true;

            FarmService.Update(farm);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteFarm(int? farmId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Farm, FarmViewModel>()
                .ForMember(x => x.FarmCrop, opt => opt.Ignore())
                .ForMember(x => x.FarmLivestock, opt => opt.Ignore())
                .ForMember(x => x.PersonFarm, opt => opt.Ignore());
            });

            IMapper iMapper = config.CreateMapper();

            var farm = FarmService.GetById(farmId.Value);

            var model = iMapper.Map<Farm, FarmViewModel>(farm);

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteFarm(FarmViewModel model)
        {
            var deleteFarm = FarmService.GetById(model.Id);
            deleteFarm.IsActive = false;

            FarmService.Update(deleteFarm);

            return RedirectToAction("Index");
        }
    }
}
