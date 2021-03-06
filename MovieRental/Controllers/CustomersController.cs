﻿using System.Linq;
using System.Web.Mvc;
using MovieRental.Models;
using System.Data.Entity;
using MovieRental.ViewModels;

namespace MovieRental.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList(); // instantiate list
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

// GET: Customers
        public ActionResult Index()
        {
            // Get list of customers
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);

        }

        public ActionResult Details(int id) //get customer details from DB with this ID
        {
            // Customer Details page
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            // 404 error - When customer doesn't exist
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }


        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0) 
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                // Map input to row in DB
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id) // get customer from DB with this ID
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)                             //Lamda Expression        
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel //this is the model behind view 
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList() //Initialize and get from DB
            };
            return View("CustomerForm", viewModel);//Pass viewModel to view
        }
    }
}