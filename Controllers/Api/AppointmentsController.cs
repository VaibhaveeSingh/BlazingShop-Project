using Blazing_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blazing_Shop.Controllers.api
{
    public class AppointmentsController : ApiController
    {

        private ApplicationDbContext db;
        public AppointmentsController()
        {
            db = new ApplicationDbContext();
        }

        public IHttpActionResult GetAppointments()
        {
            var app = db.Appointments.Include(m => m.Product).ToList();

            if (app == null)
            {
                return NotFound();
            }
            return Ok(app);
        }

        public IHttpActionResult GetAppointment(int id)
        {
            var app = db.Appointments.Include(c => c.Product).SingleOrDefault(c => c.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return Ok(app);
        }

        //Post /api/appointmentapi
        [HttpPost]
        public IHttpActionResult CreateAppointment(Appointment app)
        {

            db.Appointments.Add(app);
            db.SaveChanges();
            return Ok(app);
        }

        //Put /api/customerapi/1
        [HttpPut]
        public IHttpActionResult UpdateAppointment(int id, Appointment app)
        {

            var appInDb = db.Appointments.Include(c => c.Product).SingleOrDefault(c => c.Id == id);
            if (appInDb == null)
            {

                return NotFound();
            }

            appInDb.PersonName = app.PersonName;
            appInDb.Email = app.Email;
            appInDb.PhoneNumber = app.PhoneNumber;
            appInDb.Date = app.Date;
            appInDb.PId = app.PId;

            db.SaveChanges();
            return Ok();
        }

        //Delete /api/appointmentapi/1

        public IHttpActionResult DeleteAppointment(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a Valid Appointment id");
            }
            var appInDb = db.Appointments.SingleOrDefault(c => c.Id == id);
            if (appInDb == null)
            {

                return NotFound();
            }

            db.Appointments.Remove(appInDb);
            db.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }

}








