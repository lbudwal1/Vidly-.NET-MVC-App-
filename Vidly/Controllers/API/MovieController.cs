using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class MovieController : ApiController
    {
        private ApplicationDbContext _context;

        public MovieController()
        {
            _context = new ApplicationDbContext();
        }


        //return list of movie
        //GET : /api/movie
        public IEnumerable<MovieDto> GetMovies()
        {
            //return _context.Customers.ToList();
            return _context.Movies.Include(c => c.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);
        }


        //this is for single movie
        //GET /api/movie/1
        //public Movie GetMovie(int id)
        public IHttpActionResult GetMovies(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                //    throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            //return customer;
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movie
        [HttpPost]
        // If we write PostMovie on the place of CreateMovie then we do not need to Add [HttpPost]
        //public MovieDto CreateMovie(MovieDto movieDto)
        //IHttpActionResult is similar to ActionResult in MVC
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            }
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();


            //return movieDto;
            //Uri is Unified Resource Identifier eg - /api/movie/10
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);

        }

        //PUT /api /movie/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

                if (movieInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {

                    //in (movieDto is source object and movieInDb is taget object)
                    // in last method i do not add second argument and i create variable because it creates new object and return it 
                    //But in this one we already have existing object 
                    //we have two ways to write this code
                    //first
                    //Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

                    //second
                    Mapper.Map(movieDto, movieInDb);

                    //old one
                    //movieInDb.Name = movieDto.Name;
                    //movieInDb.ReleaseDate = movieDto.ReleaseDate;
                    //movieInDb.DateAdded = movieDto.DateAdded;
                    //movieInDb.NumberInStock = movieDto.NumberInStock;
                    //movieInDb.GenreId = movieDto.GenreId;

                    _context.SaveChanges();
                }
            }
        }

        [HttpDelete]
        //DELETE /api/movie/1
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                _context.Movies.Remove(movieInDb);
                _context.SaveChanges();
            }
        }
    }
}
