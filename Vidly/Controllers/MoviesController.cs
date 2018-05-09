using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
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

        //        private IEnumerable<Movie> getMovies()
        //        {
        //            return new List<Movie>
        //            {
        //                new Movie(){Id = 1, Name = "Shrek"},
        //                new Movie(){Id = 2, Name = "Wall-e"}
        //            };
        //        }

        // GET: Movies
        //        public ActionResult Random()
        //        {
        //            var movie = new Movie() {Name = "Shrek!"};
        //            var customers = new List<Customer>
        //            {
        //                new Customer() {Name = "Customer 1"},
        //                new Customer() {Name = "Customer 2"}
        //            };
        //
        //            var viewModel = new RandomMovieViewModel
        //            {
        //                Movie = movie,
        //                Customers = customers
        //            };
        //
        //
        //            return View(viewModel);
        //            return Content("Hello world!");
        //            return HttpNotFound();
        //            return new EmptyResult();

        //            return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
        //        }

        public ViewResult New()
        {
            var GenreList = _context.Genre.ToList();

            var viewModel = new MovieFormViewModel()
            {
                Genre = GenreList
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genre = _context.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);

            }
            else
            {
                var MoviesIndb = _context.Movies.Single(x => x.Id == movie.Id);
                MoviesIndb.Name = movie.Name;
                MoviesIndb.GenreId = movie.GenreId;
                MoviesIndb.NumberInStock = movie.NumberInStock;
                MoviesIndb.ReleaseDate = movie.ReleaseDate;
            }

            
            _context.SaveChanges();

            
            return RedirectToAction("Index", "Movies");
        }


        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel(movie)
            {
                Genre = _context.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }

        // movies
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(x => x.Genre).ToList();

            return View(movies);
        }

        //details
        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(x => x.Genre).SingleOrDefault(x => x.Id == id);

            if (movies == null)
                return HttpNotFound();
            

            return View(movies);
        }

        [Route("movies/released/{year}/{month:regex(\\{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}