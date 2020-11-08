using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Blazing_Shop.Controllers.api
{
    public class ProductController : ApiController
    {

        private ApplicationDbContext db;
        public ProductController()
        {
            db = new ApplicationDbContext();
        }

        //Get /api/appointmentapi
        public IHttpActionResult GetProducts()
        {
            var prod =db.Products.Include(c => c.Category).ToList();
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);

        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Products.Include(c => c.Category).SingleOrDefault(c => c.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(product);
        }

        //Post /api/appointmentapi
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateProduct(Product product)
        {
            
            db.Products.Add(product);
            db.SaveChanges();
            return Ok(product);
        }

        //Put /api/customerapi/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var prodInDb = db.Products.Include(c => c.Category).SingleOrDefault(c => c.Id == id);
            if (prodInDb == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            prodInDb.Name = product.Name;
            prodInDb.Price = product.Price;
            prodInDb.ShadeColour = product.ShadeColour;
            prodInDb.Image = product.Image;
            prodInDb.CID = product.CID;
            

            db.SaveChanges();
            return Ok();

        }

        //Delete /api/appointmentapi/1

        public IHttpActionResult DeleteProduct(int id)
        {

            var prodInDb = db.Products.SingleOrDefault(c => c.Id == id);
            if (prodInDb == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            db.Products.Remove(prodInDb);
            db.SaveChanges();
            return Ok();

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

       
    }
}
