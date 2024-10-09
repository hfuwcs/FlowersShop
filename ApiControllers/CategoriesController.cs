using FlowersShop.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Runtime.Serialization;


namespace FlowersShop.ApiControllers
{
    [KnownType(typeof(FlowersShop.Repository.Category))]
    public class CategoriesController : ApiController
    {
        QL_BanHoaEntities db = new QL_BanHoaEntities();
        [HttpGet]
        public IHttpActionResult Get()
        {
            IList<Category> categories = db.Category.ToList();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories); 
        }
    }
}
