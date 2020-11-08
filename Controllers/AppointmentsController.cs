using Blazing_Shop.Models;
using Blazing_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Blazing_Shop.Controllers
{
    public class AppointmentsController : Controller
    {

        public ActionResult Index()
        {
            IEnumerable<Appointment> appointments;
            HttpResponseMessage response = GlobalVar.webApiClient.GetAsync("Appointments").Result;
            appointments = response.Content.ReadAsAsync<IEnumerable<Appointment>>().Result;
            return View(appointments);

        }

        public ActionResult Details(int id)
        {
            Appointment appointment;
            HttpResponseMessage response = GlobalVar.webApiClient.GetAsync("AppointmentApi?id=" + id.ToString()).Result;
            appointment = response.Content.ReadAsAsync<Appointment>().Result;
            return View(appointment);

        }


        public ActionResult Create()
        {
            HttpResponseMessage response1 = GlobalVar.webApiClient.GetAsync("Product").Result;

            var viewModel = new NewAppointmentViewModel
            {
                Products = response1.Content.ReadAsAsync<IEnumerable<Product>>().Result
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                HttpResponseMessage response1 = GlobalVar.webApiClient.GetAsync("Product").Result;
                var viewModel = new NewAppointmentViewModel(appointment)
                {
                    Products = response1.Content.ReadAsAsync<IEnumerable<Product>>().Result
                };
                return View("Create", viewModel);
            }
            if (appointment.Id == 0)
            {
                HttpResponseMessage response = GlobalVar.webApiClient.PostAsJsonAsync("Appointments", appointment).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVar.webApiClient.PutAsJsonAsync($"Appointments/{appointment.Id}", appointment).Result;
            }
            return RedirectToAction("Index", "Appointments");
        }

        public ActionResult Edit(int id)
        {
            Appointment appointment;
            HttpResponseMessage response = GlobalVar.webApiClient.GetAsync($"Appointments/{id}").Result;
            appointment = response.Content.ReadAsAsync<Appointment>().Result;
            HttpResponseMessage response1 = GlobalVar.webApiClient.GetAsync("Product").Result;

            var viewModel = new NewAppointmentViewModel(appointment)
            {

                Products = response1.Content.ReadAsAsync<IEnumerable<Product>>().Result
            };
            return View("Create", viewModel);
        }

    }
}