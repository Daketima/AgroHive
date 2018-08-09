using FarmMartUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FarmMartUI.Startup))]
namespace FarmMartUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Admin"
                };

                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website				

                var user = new ApplicationUser
                {
                    UserName = "admin@farmerMart.com",
                    Email = "admin@farmerMart.com"
                };

                string userPWD = "@Password1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Farmer"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Farmer"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Buyer"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Buyer"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Logistics"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Logistics"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Research Institute"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Research Institute"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Agro Processors"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Agro Processors"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Financial institutions"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Financial institutions"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Cooperatives"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Cooperatives"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Federal Government"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Federal Government"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Federal Government"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Federal Government"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("State Government"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "State Government"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Non-Governmental Organisation (Local)"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Non-Governmental Organisation (Local)"
                };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Non-Governmental Organisation (International)"))
            {
                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Non-Governmental Organisation (International)"
                };
                roleManager.Create(role);
            }
        }
    }
}
