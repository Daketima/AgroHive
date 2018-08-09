using FarmMartDAL.Model;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Areas.Farmer.Models
{
    public class PlantingViewModel : BaseViewModel
    {

        public int? FarmCropId { get; set; }

        [Required(ErrorMessage = "Please Due months")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select due month")]
        public int MonthToGrowId { get; set; }

        public System.DateTime DatePlanted { get; set; }

        public System.DateTime ExpectedHarvestDate { get; set; }

        public IEnumerable<SelectListItem> HarvestPeriodDropDown { get; set; }

        [Required(ErrorMessage = "Please Due months")]
        public string Hectarage { get; set; }

        [Required(ErrorMessage = "Please enter yield per Hecter/Acre/Plot/Square meter")]
        public string YieldPerHectar { get; set; }

        public string Note { get; set; }

        public List<Planting> PlantingDetail { get; set; }


    }
}