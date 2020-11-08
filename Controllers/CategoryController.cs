using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Blazing_Shop.Controllers
{
    public class CategoryController : Controller
    {

        public ActionResult Index()
        {
            IEnumerable<Product> products;
            HttpResponseMessage response = GlobalVar.webApiClient.GetAsync("Product").Result;
            products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            return View(products);

        }

        public ActionResult Details(int id)
        {
            Product product;
            HttpResponseMessage response = GlobalVar.webApiClient.GetAsync("ProductApi?id=" + id.ToString()).Result;
            product = response.Content.ReadAsAsync<Product>().Result;
            return View(product);

        }


        public ActionResult Create()
        {
            HttpResponseMessage response1 = GlobalVar.webApiClient.GetAsync("Category").Result;

            var viewModel = new NewProductsViewModel
            {
                Categories = response1.Content.ReadAsAsync<IEnumerable<Category>>().Result
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product)
        {
            if (!ModelState.IsValid)
            {
                HttpResponseMessage response1 = GlobalVar.webApiClient.GetAsync("Category").Result;
                var viewModel = new NewProductsViewModel(product)
                {
                    Categories = response1.Content.ReadAsAsync<IEnumerable<Category>>().Result
                };
                return View("Create", viewModel);
            }
            if (product.Id == 0)
            {
                HttpResponseMessage response = GlobalVar.webApiClient.PostAsJsonAsync("Product", product).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVar.webApiClient.PutAsJsonAsync($"Product/{product.Id}", product).Result;
            }
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Edit(int id)
        {
            Product product;
            HttpResponseMessage response = GlobalVar.webApiClient.GetAsync($"Product/{id}").Result;
            product = response.Content.ReadAsAsync<Product>().Result;
            HttpResponseMessage response1 = GlobalVar.webApiClient.GetAsync("Category").Result;

            var viewModel = new NewProductsViewModel(product)
            {

                Categories = response1.Content.ReadAsAsync<IEnumerable<Category>>().Result
            };
            return View("Create", viewModel);
        }



        public ApplicationDbContext _context;
        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Category
        public ActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }

        public ActionResult Details(int id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult New()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category category)
        {
            if (category.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                var custInDB = _context.Categories.Single(c => c.Id == category.Id);
                custInDB.CName = category.CName;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        public ActionResult Edit(int id)
        {

            var updateCust = _context.Categories.SingleOrDefault(c => c.Id == id);
            return View("New", updateCust);

        }

        public ActionResult Popup()
        {

            ViewBag.Message = " ";

            return View();
        }

        public ActionResult Delete(int id)
        {

            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}