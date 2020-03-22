using ETicaret.Entity;
using ETicaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{

    public class CartController : Controller
    {
        DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);
            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }
        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"]; //session içinde kart var.
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart; //session sayfalar arası bilgi taşıma işlevi görür.
            }
            return cart;
        }
        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();
            if (cart.Cartlines.Count == 0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde Ürün Bulunmamaktadır..");
            }
            if (ModelState.IsValid)
            {
                //veritabanına kaydet.
                SaveOrder(cart, entity);
                cart.Clear(); //daha sonra temizle
                return View("Completed");
            }
            else
            {
                return View(entity); //hata varsa karşısına daha önceden doldurduğu bilgiler çıkar.
            }

        }
        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
            //Veritabanına kayıt eklenebilir.           
            var order = new Order();
            //ORDER
            order.OrderNumber = "A" + (new Random()).Next(1111, 9999).ToString(); //A ile bailayan 4 haneli bir sayı.
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Bekleniyor;
            order.Username = User.Identity.Name; //sisteme kayıtlı olan kullanıcı adı
            order.AdresBasligi = entity.AdresBasligi;
            order.Adres = entity.Adres;
            order.Sehir = entity.Sehir;
            order.Semt = entity.Semt;
            order.Mahalle = entity.Mahalle;
            order.PostaKodu = entity.PostaKodu;

            //ORDERLİNES
            order.OrderLines = new List<OrderLine>();
            foreach (var pr in cart.Cartlines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price = pr.Quantity * pr.Product.Price;
                orderline.ProductId = pr.Product.Id;
                order.OrderLines.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();

        }
    }
}