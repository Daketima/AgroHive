using FarmMartUI.helper;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Areas.Farmer.Models
{
    public class PriceViewModel : BaseViewModel
    {
        public int? FarmCropId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a measurement")]
        public int MeasurementId { get; set; }

        [Required(ErrorMessage = "Unit price for measurement selected is required")]
        public decimal UnitPrice { get; set; }

        public IEnumerable<SelectListItem> MeasurementDropDown { get; set; }

    }
}