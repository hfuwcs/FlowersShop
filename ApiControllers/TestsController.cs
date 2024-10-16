﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlowersShop.Repository;

namespace FlowersShop.ApiControllers
{
    public class TestsController : ApiController
    {
        private QL_BanHoaEntities db = new QL_BanHoaEntities();

        // GET: api/Tests
        public IQueryable<Test> GetTest()
        {
            return db.Test;
        }

        // GET: api/Tests/5
        [ResponseType(typeof(Test))]
        public IHttpActionResult GetTest(string search)
        {
            IList<Test> LstTest = db.Test.Where(t => t.NAME.Contains(search)).ToList();
            //Product product = db.Product.Find(id);
            if (LstTest == null)
            {
                return NotFound();
            }

            return Ok(LstTest);
        }

        // PUT: api/Tests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTest(int id, Test test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != test.ID)
            {
                return BadRequest();
            }

            db.Entry(test).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestExists(id))
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

        // POST: api/Tests
        [ResponseType(typeof(Test))]
        public IHttpActionResult PostTest(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Test temp = new Test { NAME = name };
            db.Test.Add(temp);
            db.SaveChanges();
            return Ok();
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (TestExists(test.ID))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return CreatedAtRoute("DefaultApi", new { id = test.ID }, test);
        }

        // DELETE: api/Tests/5
        [ResponseType(typeof(Test))]
        public IHttpActionResult DeleteTest(int id)
        {
            Test test = db.Test.Find(id);
            if (test == null)
            {
                return NotFound();
            }

            db.Test.Remove(test);
            db.SaveChanges();

            return Ok(test);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestExists(int id)
        {
            return db.Test.Count(e => e.ID == id) > 0;
        }
    }
}