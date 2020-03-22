using ETicaret.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class OrderDetailsModel
    {
        //ÜRÜN BİLGİLERİ
        public int OrderId { get; set; }
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
        public virtual List<OrderLineModel> OrderLines { get; set; } //OrderLine ile ilişkilendirme
    }
    public class OrderLineModel
    {
        public int ProductId { get; set; } //product tablosu ile ilişkili
        public string ProductName { get; set; }     
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }


    }
}