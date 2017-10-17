using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

using ASE.Entities;
using ASE.Models;

[assembly: OwinStartupAttribute(typeof(ASE.Startup))]
namespace ASE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
            //createRolesandUsers();
        }
  
  
        // In this method we will create default User roles and Admin user for login    
    //    private void createRolesandUsers()    
    //    {    
    //        ApplicationDbContext context = new ApplicationDbContext();    
  
    //        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));    
    //        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));    
  
  
    //        // In Startup iam creating first Admin Role and creating a default Admin User     
    //        if (!roleManager.RoleExists("Admin"))    
    //        {    
  
    //            // first we create Admin rool    
    //            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();    
    //            role.Name = "Admin";    
    //            roleManager.Create(role);    
  
    //            //Here we create a Admin super user who will maintain the website                   
  
    //            var user = new ApplicationUser();    
    //            user.UserName = "michael";    
    //            user.Email = "michaeldkirkegaard@gmail.com";    
  
    //            string userPWD = "P@ssw0rd";    
  
    //            var chkUser = UserManager.Create(user, userPWD);    
  
    //            //Add default User to Role Admin    
    //            if (chkUser.Succeeded)    
    //            {    
    //                var result1 = UserManager.AddToRole(user.Id, "Admin");    
  
    //            }    
    //        }    
  
    //        // creating Creating Employee role     
    //        if (!roleManager.RoleExists(UserTypes.Candidate.ToString()))    
    //        {    
    //            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
    //            role.Name = UserTypes.Candidate.ToString();    
    //            roleManager.Create(role);    
  
    //        }    
    //    }
    }
}