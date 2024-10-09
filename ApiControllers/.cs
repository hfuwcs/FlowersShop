using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace FlowersShop.ApiControllers
{
    public class ProductController : ApiController
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public List<Product> Get()
        {
            List<Product> products = db.Product.ToList();
            return products;
        }
    }
}
