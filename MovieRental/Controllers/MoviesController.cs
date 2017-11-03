using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MovieRental.Models;
using MovieRental.ViewModels;

namespace MovieRental.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // get list of movies
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        // list of movies
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

           
            if (movie == null)
              return HttpNotFound();
             return View(movie);
        }

        // GET: Movies/Random

        public ActionResult Random()
        {
            // Display list of customers rented a movie
            var movie = new Movie() { Name = "Bahubali" };
            var customers = new List<Customer>
            {
                new Customer{Name = "Customer 1"},
                new Customer{Name = "Customer 2"}
            };


            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers

            };



            return View(viewModel);
        }
    }
}