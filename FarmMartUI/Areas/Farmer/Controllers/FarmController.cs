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
       
        private readonly IRepositoryService<CropVariety> CropService;
        private readonly IRepositoryService<State> StateService;
        private readonly IRepositoryService<LocalGovernment> LocalGovernmentService;
        private readonly IRepositoryService<Livestock> LivestockService;
        ApplicationDbContext db = new ApplicationDbContext();
        public FarmController(IRepositoryService<Farm> farmService,IRepositoryService<CropVariety> cropService, IRepositoryService<State> stateService, IRepositoryService<LocalGovernment> localGovernmentService, IRepositoryService<Livestock> livestockService)
        {
            FarmService = farmService;
            CropService = cropService;
            StateService = stateService;
            LocalGovernmentService = localGovernmentService;
            LivestockService = livestockService;
        }


        private List<CropVariety> GetCropList()
        {
            return CropService.Get().ToList();
        }

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
                        LivestockListItem = LivestockService.Get().ToList()
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
                        CropListItem = GetCropList()
                    },
                    FarmLivestock = new FarmLivestockViewModel
                    {
                        LivestockListItem = LivestockService.Get().ToList()
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
                EmailAddress = thisUser.Email
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
                        IsVerified = false
                    };

                    newFarm = FarmService.Create(newFarm);

                    if (newFarm != null)
                    {
                        SaveCropFarm(newFarm);
                    }
                    return RedirectToAction("AddFarmAddress", "FarmAddress", new { farmId = newFarm.Id });
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: Farm/Edit/5
        public ActionResult EditFarm(int? farmId)
        {
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

            var farm = FarmService.GetById(farmId.Value);

            var model = iMapper.Map<Farm, FarmViewModel>(farm);
            

            //var model = new FarmViewModel {
            //    Id = farm.Id,
            //    FarmName = farm.FarmName,
            //    EmailAddress = farm.Person.EmailAddress,
                
            //};
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
