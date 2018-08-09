

using FarmMartDAL.Model;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmMartUI.Areas.Farmer.Models
{
    public class FarmViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Farm name is required")]
        public string FarmName { get; set; }

        public string EmailAddress { get; set; }

        public string PhotoPath { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }

        public string Size { get; set; }

        public List<Farm> MyFarm { get; internal set; }

        public FarmCropViewModel FarmCrop { get; set; }

        public FarmLivestockViewModel FarmLivestock { get; set; }

        public bool IsVerified { get; set; }

        
    }
}