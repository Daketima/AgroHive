using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmMartUI.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public List<Farm> Farms { get; set; }
        public Farm ThisFarm { get; set; }
        
        public List<FarmCrop> FarmCrops { get; set; }
        public List<FarmLivestock> FarmLivestocks { get; set; }
        public List<Crop> CropList { get; set; }
       
        public List<Livestock> LivestockList { get;  set; }
        public List<CropPrice> CropPrice { get; set; }
        public List<LivestockPrice> LivestockPrice { get; set; }

    }

    public class ContactViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }

    }
}