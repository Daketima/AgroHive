using System.Web.Mvc;

namespace FarmMartUI.Areas.Farmer
{
    public class FarmerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Farmer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Farmer_default",
                "Farmer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "FarmMartUI.Areas.Farmer.Controllers" }
            );
        }
    }
}