using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FlowersShop.Repository;

namespace FlowersShop.Models
{
    public class ProductBusiness
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public List<Product> GetData()
        {
            return db.Product.ToList();
        }
        public List<Product> SearchProduct(string txt_Search)
        {
            return db.Product.Where(d=>d.Name.Contains(txt_Search)).ToList();
        }
    }
}