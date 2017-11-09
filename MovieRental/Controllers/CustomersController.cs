using System.Linq;
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
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View( viewModel);
        }

// GET: Customers
        public ActionResult Index()
        {
            // Get list of customers
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);

        }

        public ActionResult Details(int id)
        {
            // Customer Details page
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            // 404 error - When customer doesn't exist
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }


        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}