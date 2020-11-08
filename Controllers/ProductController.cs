using Blazing_Shop.Models;
using Blazing_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Blazing_Shop.Controllers
{
    public class ProductController : Controller
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




    }
}

