using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ETicaret.Entity;

namespace ETicaret.Models
{
    public class Cart
    {
        //private olması o kartın özel olduğunu belirtir.
        private List<Cartline> _cartLines = new List<Cartline>(); //classa özel private oluşturuldu.Her satırı toplayacak kart oluşturuldu.
        public List<Cartline> Cartlines //private tanımlıyı dışarı açabilmek için public olarak tanım yaptık.
        {
            get { return _cartLines; }
        }

        //EKLEME
        public void AddProduct(Product product, int quantity)
        {
            //gelen eleman gerçekten var mı diye sorgulama yapar.
            var line = _cartLines.FirstOrDefault(i => i.Product.Id == product.Id); //içerideki id dışarıdan gelen id ye eşit mi diye kontrol eder.

            //_cartline içerisinde yoksa eklememiz gerekiyor.
            if (line == null)
            {
                _cartLines.Add(new Cartline() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity; //dışarıdan girilen sayı kadar adedi arttırılır.
            }
        }

        //SİLME
        public void DeleteProduct(Product product)
        {
            _cartLines.RemoveAll(i => i.Product.Id == product.Id); //silinmek istenilen elemanı gönderir.

        }

        //Herbir ürünün adedine göre hesaplama yapıp değer gönderme.
        public double Total()
        {
            return _cartLines.Sum(i => i.Product.Price * i.Quantity);
        }
        public void Clear()
        {
            _cartLines.Clear(); //Tüm elemanların hepsi temizlenmek istendiğinde
        }
    }
}
public class Cartline //product cinsinden eleman alacak.
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

}
