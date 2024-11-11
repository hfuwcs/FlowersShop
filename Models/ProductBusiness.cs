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
        static ProductBusiness instance;

        public static ProductBusiness Instance { get { 
                if (instance == null)
                    {
                    instance = new ProductBusiness();
                }
                return instance;
            }
            private set => instance = value; }

        public List<Product> GetData()
        {
            return db.Product.ToList();
        }
        public List<Product> SearchProduct(string txt_Search)
        {
            return db.Product.Where(d=>d.Name.Contains(txt_Search)).ToList();
        }
        public Product GetProduct(int id)
        {
            var product = db.Product
                .Include(p => p.Color) // Tải trước dữ liệu từ bảng liên quan
                .Include(p => p.Object)
                .Include(p => p.Occasion)
                .Include(p => p.Presentation)
                .FirstOrDefault(p => p.Product_ID == id);
            return product;
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