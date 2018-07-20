using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

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
        public ActionResult Index()
        {
            //var movies = _context.Movies.Include(m => m.Genre).ToList();
            if(User.IsInRole(RoleName.CanManageMovie))
            return View(/*movies*/"Index");
            else
            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovie)]
        public ViewResult New()
        {
            var genres = _context.Genre.ToList();

            var viewModel = new MovieFormViewModel
            {
                DateAdded = DateTime.Now,
                genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                //Movies = movie,
              
                genres = _context.Genre.ToList()
            };

            return View("MovieForm", viewModel);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    genres = _context.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }

             if (movie.Id == 0)
                {
                   
                    _context.Movies.Add(movie);
                }

                else
                {
                    var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                    movieInDb.Name = movie.Name;
                    movieInDb.ReleaseDate = movie.ReleaseDate;
                    movieInDb.GenreId = movie.GenreId;
                    movieInDb.DateAdded = movie.DateAdded;
                    movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.DateAdded = movie.DateAdded;
                }

                _context.SaveChanges();
            

            return RedirectToAction("Index", "Movies");
        }






        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Random
        //public ActionResult Random()
        //{

        //    var movie = new Movie() { Name = "Hangover!" };
        //    //var customers = new List<Customer>
        //    //{
        //    //    new Customer {Name = "Customer 1" },
        //    //    new Customer {Name = "Customer 2" }
        //    //};

        //    //var viewModel = new RandomMovieViewModel
        //    //{
        //    //    Movie = movie,
        //    //    Customers = customers
        //    //};

        //    return View(movie);


        //    //return Content("Hello World");
        //    //return HttpNotFound();
        //    //return new EmptyResult();

        //    // here index is action and home is controller and we can add an arrgument in it to after home by , new {page = 1, sortBy = "name"});
        //    //return RedirectToAction("Index", "Home");
        //}


        ////regex is just a function which unable regular experations
        ////google ASP.NET MVC attribute route constraints
        //[Route("movies/released/{year}/{month:regex(\\d{4}):range(1, 12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{

        //    return Content(year + "/" + month);
        //}

        ////public ActionResult Edit(int id)
        ////{
        ////    return Content("id=" + id);
        ////}

        //    //movies
        ////public ActionResult index(int? pageIndex, string sortBy)
        ////{
        ////    if (!pageIndex.HasValue)
        ////        pageIndex = 1;

        ////    if (String.IsNullOrWhiteSpace(sortBy))
        ////        sortBy = "Name";

        ////    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        ////}


    }
}