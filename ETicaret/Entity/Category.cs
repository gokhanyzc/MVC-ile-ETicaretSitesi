using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; } //aralarındaki ilişkiyi belirtir.Bir kategorinin birden fazla ürünü olabilmesi için list şeklinde tutulur.

    }
}