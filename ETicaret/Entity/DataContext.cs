using ETicaret.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace ETicaret.Entity
{
    //bu class özelliği veritabanı üzerinden oturum işlemlerini gerçekleştirip , işlemlerin gerçekleştirilmesini sağlar.Crud işlemleri için.
    public class DataContext:DbContext
    {
        public DataContext():base("dataConnection")
        {
            Database.SetInitializer(new DataInitializer()); //DataInitializeri devreye sokmak için gerekli.
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}