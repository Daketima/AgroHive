using FarmMartDAL.Model;
using FarmMartUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMartUI.Areas.Farmer.Models
{
    public class CropViewModel : BaseViewModel
    {
        public int? CropTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photopath { get; set; }
        public DateTime DateCreated { get; set; }
        public Nullable<int> CropId { get; set; }
        public string Note { get; set; }
        public string PhotoPath { get; set; }
        public List<Crop> AllCrop { get; set; }
        public List<CropVariety> AllCropVariety { get; set; }
    }
}