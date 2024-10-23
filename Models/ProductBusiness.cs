using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FlowersShop.Repository;
using Microsoft.Ajax.Utilities;

namespace FlowersShop.Models
{
    public class ProductBusiness
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();

        //private int ProductState(Product product)
        //{
        //    if(product.Name.IsNullOrWhiteSpace())
        //}
        public List<Product> GetData()
        {
            return db.Product.ToList();
        }
        public List<Product> SearchProduct(string txt_Search)
        {
            return db.Product.Where(d=>d.Name.Contains(txt_Search)).ToList();
        }
        public bool AddProduct(Product product)
        {
            if (product.Product_ID != 0)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}