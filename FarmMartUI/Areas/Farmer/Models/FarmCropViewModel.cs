using FarmMartDAL.Model;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMartUI.Areas.Farmer.Models
{
    public class FarmCropViewModel : BaseViewModel
    {
        public int FarmId { get; set; }
        public int CropId { get; set; }
        public int CropTypeId { get; set; }
        public int CropVarietyId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<CropVariety> CropListItem { get; set; }
        public List<FarmCrop> FarmCropList { get; set; }
        public FarmCrop ThisCrop { get; set; }
        public int _FarmId { get; set; }
        public Planting Harvest { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FarmDropDown { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> CropDropDown { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> CropVarietyDropDown { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> CropTypeDropDown { get; set; }
    }
}