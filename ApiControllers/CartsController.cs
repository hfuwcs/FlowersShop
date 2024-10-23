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
using FlowersShop.Repository;

namespace FlowersShop.ApiControllers
{
    public class CartsController : ApiController
    {
        private QL_BanHoaEntities db = new QL_BanHoaEntities();

        // GET: api/Carts
        // GET Method: Lấy tất cả giỏ hàng - Không cần thiết, viết cho có =))
        public IHttpActionResult GetCart()
        {
            IList<Cart> carts = db.Cart.ToList();
            if (carts==null)
            {
                return NotFound();
            }
            return Ok(carts);
        }

        // GET: api/Carts/Cart ID
        // GET Method: Lấy giỏ hàng của người dùng cụ thể bằng ID
        [ResponseType(typeof(Cart))]
        public IHttpActionResult GetCarts(int Cid)
        {
            IList<Cart> carts = db.Cart.Where(c=>c.Cart_ID==Cid).ToList();
            if (carts == null)
            {
                return NotFound();
            }

            return Ok(carts);
        }

        // PUT: api/Carts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCart(int id, Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cart.Cart_ID)
            {
                return BadRequest();
            }

            db.Entry(cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        [ResponseType(typeof(Cart))]
        public IHttpActionResult PostCart(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cart.Add(cart);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cart.Cart_ID }, cart);
        }

        // DELETE: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult DeleteCart(int id)
        {
            Cart cart = db.Cart.Find(id);
            if (cart == null)
            {
                return NotFound();
            }

            db.Cart.Remove(cart);
            db.SaveChanges();

            return Ok(cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartExists(int id)
        {
            return db.Cart.Count(e => e.Cart_ID == id) > 0;
        }
    }
}