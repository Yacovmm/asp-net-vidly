using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private IEnumerable<Movie> getMovies()
        {
            return new List<Movie>
            {
                new Movie(){Id = 1, Name = "Shrek"},
                new Movie(){Id = 2, Name = "Wall-e"}
            };
        }

        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shrek!"};
            var customers = new List<Customer>
            {
                new Customer() {Name = "Customer 1"},
                new Customer() {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };


            return View(viewModel);
//            return Content("Hello world!");
//            return HttpNotFound();
//            return new EmptyResult();
            
//            return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
        }

        public ActionResult Edit(int id)
        {
            return Content("id= " + id);
        }

        // movies
        public ViewResult Index()
        {
            var movies = getMovies();

            return View(movies);
        }

        [Route("movies/released/{year}/{month:regex(\\{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}