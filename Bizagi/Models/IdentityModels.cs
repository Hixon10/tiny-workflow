using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bizagi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    /// <summary>
    /// http://www.typecastexception.com/post/2013/11/11/Extending-Identity-Accounts-and-Implementing-Role-Based-Authentication-in-ASPNET-MVC-5.aspx
    /// </summary>
    public class IdentityManager
    {
//        public bool RoleExists(string name)
//        {
        //            var rm = new RoleManager<ApplicationRole>(
//                new RoleStore<IdentityRole>(new ApplicationDbContext()));
//
//            return rm.RoleExists(name);
//        }
//
//        public bool CreateRole(string name)
//        {
        //            var rm = new RoleManager<ApplicationRole>(
//                new RoleStore<IdentityRole>(new ApplicationDbContext()));
//
        //            var idResult = rm.Create(new ApplicationRole(name));
//
//            return idResult.Succeeded;
//        }
//
//        public bool CreateUser(ApplicationUser user, string password)
//        {
//            var um = new UserManager<ApplicationUser>(
//                new UserStore<ApplicationUser>(new ApplicationDbContext()));
//
//            var idResult = um.Create(user, password);
//
//            return idResult.Succeeded;
//        }
//
//        public bool AddUserToRole(string userId, string roleName)
//        {
//            var um = new UserManager<ApplicationUser>(
//                new UserStore<ApplicationUser>(new ApplicationDbContext()));
//
//            var idResult = um.AddToRole(userId, roleName);
//
//            return idResult.Succeeded;
//        }
//
//        public void ClearUserRoles(string userId)
//        {
//            var um = new UserManager<ApplicationUser>(
//
//                new UserStore<ApplicationUser>(new ApplicationDbContext()));
//
//            var user = um.FindById(userId);
//
//            var currentRoles = new List<IdentityUserRole>();
//
//            currentRoles.AddRange(user.Roles);
//
//            foreach (var role in currentRoles)
//            {
//                um.RemoveFromRole(userId, role.Role.Name);
//            }
//        }
    }
}