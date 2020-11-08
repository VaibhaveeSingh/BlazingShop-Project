using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Blazing_Shop.Controllers.api
{
    public class CategoryController : ApiController
    {
        private ApplicationDbContext _context;
        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetCategories()
        {
            var cat= _context.Categories.ToList();
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }

        public IHttpActionResult GetCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(category);
        }

        [HttpPost]
        public IHttpActionResult CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok( category);
        }


        public IHttpActionResult UpdateCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var categoryInDb = _context.Categories.SingleOrDefault(c =>c.Id == id);

            if (categoryInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            categoryInDb.CName = category.CName;
          
            _context.SaveChanges();
            return Ok();


        }


        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategor(int id)
        {
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Categories.Remove(categoryInDb);
            _context.SaveChanges();
            return Ok();

        }


         protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
