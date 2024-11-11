using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowersShop.Repository
{
    public class Product_Color
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
    }

}