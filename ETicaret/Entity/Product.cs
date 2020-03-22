using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public bool Slider { get; set; }
        public bool IsHome { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; } //yabancıl anahtar. (1-*) ilişkisi kuruldu.
        public virtual Category Category { get; set; } //ürünün bağlı olduğu kategoriyi alabilmek için.Her bir ürünün bir kategorisi olması için.


    }
}