
using FarmMartBLL.Core;
using FarmMartBLL.ServiceAPI;
using FarmMartDAL.Model;
using FarmMartUI.helper;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Controllers
{
    public class HomeController : SuperBaseController
    {
        private readonly IRepositoryService<FarmCrop> FarmCropService;
        private readonly IRepositoryService<Farm> FarmService;
        private readonly IRepositoryService<Crop> CropVarietyService;
        private readonly IRepositoryService<Livestock> LivestockService;
        private readonly IRepositoryService<FarmLivestock> FarmLivestockService;

       

        public HomeController(IRepositoryService<FarmCrop> farmCropService, IRepositoryService<Farm> farmService,  IRepositoryService<Crop> cropVarietyService, IRepositoryService<Livestock> livestockService, IRepositoryService<FarmLivestock> farmLivestockService)
        {
            FarmCropService = farmCropService;
            FarmService = farmService;
            CropVarietyService = cropVarietyService;
            LivestockService = livestockService;
            FarmLivestockService = farmLivestockService;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                Farms = FarmService.Get().Where(x => x.IsActive).ToList()

            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var model = new ContactViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                base.SendFeedBack(model.Email, model.Subject, model.Message);
            }
            return View();
        }



        // GET: Crop/Details/5
        public ActionResult Details(int? id)
        {
            var farm = FarmService.GetById(id.Value);

            var model = new HomeViewModel
            {
                ThisFarm = farm,
                FarmCrops = farm.FarmCrop.ToList(),
                FarmLivestocks = farm.FarmLivestock.ToList()
            };
            return View(model);
        }

        public ActionResult Search(string searchWord, int? cropId, int? livestockId)
        {
            var farmCrop = new List<FarmCrop>();
            var farmLivestock = new List<FarmLivestock>();
            var cropList = new List<Crop>();
            var livestockList = new List<Livestock>();
            var farms = new List<Farm>();

            if (!string.IsNullOrEmpty(searchWord))
            {
                farmCrop = FarmCropService.Get().Where(x => x.CropVariety.Name.ToUpper().Contains(searchWord.ToUpper()) && x.IsActive).ToList();

                if (farmCrop.Any())
                {
                    cropList = CropVarietyService.Get().ToList();
                }
                //if (!farmCrop.Any())
                //{
                //    farmLivestock = FarmLivestockService.Get().Where(x => x.IsActive && x.LivestockBreed.Livestock.Name.ToUpper().Contains(searchWord.ToUpper()) || x.Farm.FarmName.ToUpper().Contains(searchWord.ToUpper())).ToList();

                //    livestockList = LivestockService.Get().ToList();
                //}
            }

            if (cropId.HasValue)
            {
                farmCrop = FarmCropService.Get().Where(x => x.CropVarietyId == cropId.Value && x.IsActive).ToList();
            }

            //if (livestockId.HasValue)
            //{
            //    farmLivestock = FarmLivestockService.Get().Where(x => x.LivestockBreedId.Equals(livestockId.Value) && x.IsActive).ToList();
            //    livestockList = LivestockService.Get().ToList();
            //}

            if (farmCrop.Any())
            {
                farms = farmCrop.Select(x => x.Farm).Where(x => x.IsActive).ToList();
                farms.AddRange(SearchByWord(searchWord));
                cropList = CropVarietyService.Get().ToList();
            }
            if (farmLivestock.Any())
            {
                farms = farmLivestock.Select(x => x.Farm).Where(x => x.IsActive).ToList();
                farms.AddRange(SearchByWord(searchWord));
            }

            var model = new HomeViewModel
            {
                Farms = farms,
                CropList = cropList,
                LivestockList = livestockList
            };
            return View(model);
        }

        private List<Farm> SearchByWord(string searchWord)
        {
            var suggestedFarm = new List<Farm>();
            if (!string.IsNullOrEmpty(searchWord))
            {
              suggestedFarm = FarmService.Get().Where(x => x.FarmName.ToUpper().Contains(searchWord.ToUpper()) && x.IsActive).ToList();
            }
            return suggestedFarm;
        }

        
    }
}