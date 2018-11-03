using FarmMartDAL.Model;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmMartUI.Areas.Farmer.Models
{
    public class FarmCropViewModel : BaseViewModel
    {
        public int FarmId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a Crop")]
        public int CropId { get; set; }

        [Range(1,int.MaxValue, ErrorMessage ="Please select a Crop Type")]
        public int CropTypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a Crop Variety")]
        public int CropVarietyId { get; set; }

        [Required(ErrorMessage = "Please Due months")]
        public string Hectarage { get; set; }

        [Required(ErrorMessage = "Please enter yield per Hecter/Acre/Plot/Square meter")]
        public string YieldPerHectar { get; set; }

        public string CropVarietyNote { get; set; }

        public List<Crop> CropListItem { get; set; }

        public List<FarmCrop> FarmCropList { get; set; }

        public FarmCrop ThisCrop { get; set; }

        public int _FarmId { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> FarmDropDown { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> CropDropDown { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> CropVarietyDropDown { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> CropTypeDropDown { get; set; }
    }
}