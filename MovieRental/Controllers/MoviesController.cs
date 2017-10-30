using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRental.Models;
using MovieRental.ViewModels;

namespace MovieRental.Controllers
{
    public class MoviesController : Controller
    {
        // get list of movies
        public ViewResult Index()
        {
            var movies = GetMovies();
            return View(movies);
        }

        // list of movies
        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie {Id = 1, Name = "Shrek"},
                new Movie {Id = 2, Name = "Wall-e"}
            };
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