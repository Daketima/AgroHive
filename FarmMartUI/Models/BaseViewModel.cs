using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMartUI.Models
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ThisUser { get; set; }
        public ApplicationUser PersonFarm { get; set; }
     
    }
}