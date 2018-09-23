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

        public DateTime DatePlanted { get; set; }

        public DateTime ExpectedHarvestDate { get; set; }

        public IEnumerable<SelectListItem> HarvestPeriodDropDown { get; set; }

        public Planting PlantingDetail { get; set; }


    }
}