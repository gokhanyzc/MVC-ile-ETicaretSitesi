using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ETicaret.Identity
{
    public class IdentityInitializer : CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {

            //ADMİN ROLÜ
            if (!context.Roles.Any(i => i.Name == "admin")) //admin rolü yoksa
            {
                //Rol Oluşturulur.
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                //role oluşturulup manager aracılığı ile kaydedilir.
                var role = new ApplicationRole() { Name = "admin", Description = "admin rolü" };
                manager.Create(role); //role oluşturuldu.            
            }


            //USER ROLÜ
            if (!context.Roles.Any(i => i.Name == "user"))
            {

                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "user", Description = "user rolü" };
                manager.Create(role);
            }

            if (!context.Users.Any(i => i.Name == "gokhanyzc"))
            {

                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "gokhan", Surname = "yazici", Email = "gokhanyzc9797@gmail.com", UserName = "gokhanyzc" };
                manager.Create(user, "976134"); //kullanıcı oluşturuldu.  
                manager.AddToRole(user.Id, "admin");//hem kullanıcı hem admin.
                manager.AddToRole(user.Id, "user");
            }

            //ADMİN OLMAYAN USER 
            if (!context.Users.Any(i => i.Name == "hakanyzc"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "hakan", Surname = "yazici", Email = "hakan6197@gmail.com", UserName = "hakanyzc" };
                manager.Create(user, "123456"); //kullanıcı oluşturuldu.  
                manager.AddToRole(user.Id, "user"); //sadece user.
            }

            base.Seed(context);
        }

    }
}