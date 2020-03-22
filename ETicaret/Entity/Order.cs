using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Entity
{
    public class Order
    {
        //ÜRÜN BİLGİLERİ
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; } //sipariş durumu

        //SİPARİŞ BİLGİLERİ
        public string Username { get; set; }
        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public string PostaKodu { get; set; }


        //sipariş girildiği zaman siparişin gittiği adres bilgileride order tablosunda saklanması gerekir.
        public virtual List<OrderLine> OrderLines { get; set; } //OrderLine ile ilişkilendirme
    }
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; } //Yabancıl Anahtar
        public Order Order { get; set; } //herbir orderline tablosundan ordera gidilir.
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; } //ürün tablosu ile ilişkilendirme(yabancıl anahtar)
        public virtual Product Product { get; set; }

    }
}