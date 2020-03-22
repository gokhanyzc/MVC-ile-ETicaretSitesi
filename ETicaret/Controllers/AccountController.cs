using ETicaret.Entity;
using ETicaret.Identity;
using ETicaret.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        public ActionResult Index()
        {
            var username = User.Identity.Name; //o anda login olan kullanıcının kullanıcı adı getirilir.
            //siparişleri username göre sorgulanır. (filtreleme)
            var orders = db.Orders.Where(i => i.Username == username).Select(i => new UserOrderModel
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Total = i.Total
            }).OrderByDescending(i => i.OrderDate).ToList();    //orders tablosundaki bilgileri userordermodel tablosunun içine paketlendi.
            return View(orders);
        } 

        public ActionResult Details(int id)
        {
            var entity = db.Orders.Where(i => i.Id == id).Select(i => new OrderDetailsModel()
            {
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.Total,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Username = i.Username,
                AdresBasligi = i.AdresBasligi,
                Adres = i.Adres,
                Sehir = i.Sehir,
                Semt = i.Semt,
                Mahalle = i.Mahalle,
                PostaKodu = i.PostaKodu,
                OrderLines = i.OrderLines.Select(x => new OrderLineModel()
                {
                    ProductId=x.ProductId,
                    ProductName=x.Product.Name,
                    Image=x.Product.Image,
                    Quantity=x.Quantity,
                    Price=x.Price,
                }).ToList()

            }).FirstOrDefault(); //çünkü sadece bir tane kayıt döndürür.
            return View(entity);
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.Username;
                user.Email = model.Email;
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded) //kayıt işlemi gerçekleşmişse
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı Oluşturma Hatası");
                }

            }
            return View(model);
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password); //kullanıcı ve şifreye göre kullanıcı arama
                if (user != null)
                {
                    var autManager = HttpContext.GetOwinContext().Authentication; //kullanıcıyı sisteme dahil et.eşleşirse eğer.
                    var identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");

                    //REMEMBER İÇİN GEREKLİ İŞLEMLER.
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe; //sisteme beni dahil et.sistemin devamlı açık olması sağlanır.
                    autManager.SignIn(authProperties, identityclaims); // kullanıcıyı sisteme dahil ettik.Giriş işlemi tamamlandı

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl); //kullanıcı giriş yapmadan izni olmayan sayfalara gitmek istediğinde login sayfasına yönlendirilir.
                                                    //giriş yaptıktan sonra istediği sayfaya yönlendirilir.
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok.");
                }
            }
            return View(model);
        }
        public ActionResult LogOut()
        {

            var autManager = HttpContext.GetOwinContext().Authentication; //çıkış yaptıktan sonra kullanıcıyı yok etme.
            autManager.SignOut(); //çıkış yapıldı.
            return RedirectToAction("Index", "Home");
        }
    }

}
