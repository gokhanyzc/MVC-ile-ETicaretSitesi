using ETicaret.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Models
{
    public class UserOrderModel
    {
        //ÜRÜN BİLGİLERİ
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; } //sipariş durumu

    }
}