using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarmMartDAL.Model;
using FarmMartUI.Models;

namespace FarmMartUI.Areas.Farmer.Models
{
    public class LivestockViewModel : BaseViewModel
    {
        public int? CropTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photopath { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Livestock> AllLivestock { get; internal set; }
    }
}