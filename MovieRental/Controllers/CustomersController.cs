using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRental.Models;

namespace MovieRental.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            // Get list of customers
            var customers = GetCustomers();
            return View(customers);

        }

        public ActionResult Details(int id)
        {
            // Customer Details page
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            // 404 error - When customer doesn't exist
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            // list of customers
            return new List<Customer>
            {
                new Customer {Id = 1, Name = "John Smith"},
                new Customer {Id = 2, Name = "Mary Williams"}
            };
        }
    }
}