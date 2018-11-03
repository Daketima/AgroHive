using FarmMartDAL.Model;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmMartUI.Areas.Farmer.Models
{
    public class FarmLivestockViewModel : BaseViewModel
    {
       
        public int FarmId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select livestock type")]
        public int LivestockId { get; set; }

        public DateTime DateCreated { get; set; }

        public List<Livestock> LivestockListItem { get; set; }

        public List<FarmLivestock> MyFarmLivestock { get; set; }

        public FarmLivestock ThisLivestock { get; set; }

        public int _FarmId { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Please select livestock")]
        //public int LivestockTypeId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select gender")]
        public int GenderId { get; set; }

        public string Population { get; set; }

        public string Other { get; set; }

        public string QuantityAvailable { get; set; }

        [Required(ErrorMessage = "Specify weight")]
        public string Weight { get; set; }

        public DateTime HitMarketDate {get;set; }

        public bool IsActive { get; set; }
       
        public IEnumerable<System.Web.Mvc.SelectListItem> FarmDropDown { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> AnimalGenderDropDown { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> LivestockBreedDropdown { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> LivestockTypeDropDown { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> LivestockDropDown { get; set; }

        public string Breed { get; set; }
    }
}