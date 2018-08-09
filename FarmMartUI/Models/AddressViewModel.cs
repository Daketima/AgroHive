using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmMartUI.Models
{
    public class AddressViewModel : BaseViewModel
    {
        public int FarmId { get; set; }

        [Required]
        public string FullAddress { get; set; }

        [Required]
        public string LandMark { get; set; }
        
        public int StateId { get; set; }
       
        public int LocalGovermentId { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> StateDropDown { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> LocalGovernmentAreaDropDown { get; set; }
    }
}