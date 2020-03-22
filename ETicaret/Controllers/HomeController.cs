using ETicaret.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret.Models;

namespace ETicaret.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            //product model elemanlarını çagırabilme.
            //veritabanından gelen işleri paketleme yaptık.
            var urun = db.Products.Where(i => i.IsHome && i.IsApproved).Select(i => new ProductModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description.Length > 25 ? i.Description.Substring(0, 20) + "..." : i.Description,
                Price = i.Price,
                Stock = i.Stock,
                Image = i.Image,
                CategoryId = i.CategoryId

            }).ToList();
           

            return View(urun);
        }

        //Slider düzenleme
        public PartialViewResult Slider()
        {
            return PartialView(db.Products.Where(x => x.Slider && x.IsApproved).Take(3).ToList());
        }


        //popüler ve onaylı ürünleri getirme
        public ActionResult FeaturedProductList()
        {
            return PartialView(db.Products.Where(x=>x.IsFeatured && x.IsApproved).Take(5).ToList());
        }
        //Filtreleme
        public ActionResult ProductList(int id)
        {
            return View(db.Products.Where(i => i.CategoryId == id).ToList());
        }
        public ActionResult Search(string q)
        {
            var p = db.Products.Where(i => i.IsApproved == true);
            if (!string.IsNullOrEmpty(q))
            {
                p = p.Where(i => i.Name.Contains(q) || i.Description.Contains(q));
            }
            return View(p.ToList());
        }

        public ActionResult ProductDetails(int id)
        {

            return View(db.Products.Where(i=>i.Id == id).FirstOrDefault()); //geriye dönecek değer bir tane.            

        }
        public ActionResult Product()
        {
            var uruns = db.Products.Where(i => i.IsApproved).Select(i => new ProductModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description.Length > 25 ? i.Description.Substring(0, 20) + "..." : i.Description,
                Price = i.Price,
                Stock = i.Stock,
                Image = i.Image,
                CategoryId = i.CategoryId


            }).ToList();

            return View(uruns);
        }

    }
}