using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowersShop.Repository
{
    public class ProductDTO
    {
        public int Product_ID { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}