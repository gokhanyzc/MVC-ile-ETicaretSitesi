using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Entity
{
    //modelde herhangi bir değişiklik olduğu zaman veritabanını siler ve yeniden oluşturur.
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Category>()
            {
              new Category() { Name="KAMERA",Description="Kamera Ürünleri" },
              new Category() { Name="BİLGİSAYAR",Description="Bilgisayar Ürünleri" }
            };
            foreach (var item in kategoriler)
            {
                context.Categories.Add(item);
            }
            context.SaveChanges(); //kategorileri veritabanına ekleme işlemi

            var urunler = new List<Product>()
            {
                new Product() { Name="Canon",Description="EOS-700D",Price=2500,Stock=50,IsHome=true,CategoryId=1,Image="kamera1.jpg",IsApproved=true,IsFeatured=false },
                new Product() { Name="Asus",Description="i7 8.Nesil 5500u,1TB HD,8GB RAM",Price=4500,Stock=10,IsHome=false,CategoryId=2,Image="pc1.jpg",IsApproved=true,IsFeatured=true },
                new Product() { Name="Casper",Description="i5 6.Nesil 1TB HD,4GB RAM,NVDİA EKRAN KARTI",Price=2850,Stock=7,IsHome=true,CategoryId=2,Image="PC2.jpg",IsApproved=true,IsFeatured=false }
            };
            foreach (var item in urunler)
            {
                context.Products.Add(item);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}