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
using System.Web.Mvc;
using FlowersShop.Repository;

namespace FlowersShop.ApiControllers
{
    public class ProductsController : ApiController
    {
        private QL_BanHoaEntities db = new QL_BanHoaEntities();

        // GET: api/Products
        // GET Method: Lấy tất cả Sản phẩm
        public IHttpActionResult GetProduct()
        {
            IList<Product> products = db.Product.ToList();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }



        // GET: api/Products/ProductName
        // GET Method: Tìm Sản phẩm bằng tên
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(string ProductName)
        {
            IList<Product> products = db.Product.Where(p=>p.Name.Contains(ProductName)).ToList();
            //Product product = db.Product.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
        //POST: api/Products/product
        //POST Method: Thêm mới sản phẩm
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return Ok();
            }
            return BadRequest("Invalid data.");
        }


        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Product_ID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Products/5
        // DELETE Method: Xóa sản phẩm
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            db.Product.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Product.Count(e => e.Product_ID == id) > 0;
        }
    }
}