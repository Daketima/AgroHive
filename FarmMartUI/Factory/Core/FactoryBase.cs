using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.Factory
{
    public class FactoryBase : Controller
    {
        protected ActionResult thisArea;

        public ActionResult RedirectToArea(string Area)
        {
            return thisArea;
        }
    }
}