using System.Web.Mvc;

namespace FarmMartUI.Areas.ResearchInstitute
{
    public class ResearchInstituteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ResearchInstitute";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ResearchInstitute_default",
                "ResearchInstitute/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}