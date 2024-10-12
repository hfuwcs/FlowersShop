using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowersShop.Models
{
    public class CategoryBusiness
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public List<Category> GetCategory()
        {
            return db.Category.ToList();
        }
    }
}