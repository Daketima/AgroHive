using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Factory
{
    public class FarmerArea : FactoryBase
    {
        public FarmerArea ( string Area)
        {
            thisArea = RedirectToAction("Index", "Farm", new { area = Area});
        }
    }
}