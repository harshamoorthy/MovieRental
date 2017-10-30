using System.Linq;
using System.Web.Mvc;
using MovieRental.Models;

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



// GET: Customers
        public ActionResult Index()
        {
            // Get list of customers
            var customers = _context.Customers.ToList();
            return View(customers);

        }

        public ActionResult Details(int id)
        {
            // Customer Details page
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            // 404 error - When customer doesn't exist
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        
    }
}