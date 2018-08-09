using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMartUI.Models
{
    public class LivestockViewModel : BaseViewModel
    {
        public int? CropTypeId { get; set; }

        public string Name { get; set; }

        public Livestock Livestock { get; set; }

        public string BreedNote { get; set; }

        public string Photopath { get; set; }

        public DateTime DateCreated { get; set; }

        public LivestockPrice PriceDetail { get; set; }

        public List<Livestock> AllLivestock { get; internal set; }

        public string Description { get; internal set; }

        public FarmLivestock farmLivestock { get; set; }
    }
}