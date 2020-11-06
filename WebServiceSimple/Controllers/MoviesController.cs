using System;
using System.Linq;
using DataServiceLib;
using Microsoft.AspNetCore.Mvc;
using WebServiceSimple.Models;

namespace WebServiceSimple.Controllers
{
    [ApiController]
    [Route("api/v2/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IDataService _dataService;

        public MoviesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
           
            if (Program.CurrentUser == null)
            {
                return Unauthorized();
            }

            try
            {
                var movies = _dataService.GetMovies(Program.CurrentUser.Id);
                return Ok(movies.Select(CreateMovieDto));
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }

        [HttpGet("{movieId}", Name = nameof(GetMovie))]
        public IActionResult GetMovie(string movieId)
        {
            if (Program.CurrentUser == null)
            {
                return Unauthorized();
            }

            try
            {
                var movie = _dataService.GetMovie(Program.CurrentUser.Id, movieId);
                return Ok(CreateMovieDto(movie));
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }

        /**
         *
         * Helper
         *
         */

        private MovieDto CreateMovieDto(Movie movie)
        {
            return new MovieDto
            {
                Url = Url.Link(nameof(GetMovie), new { movieId = movie.Id}),
                Title = movie.Title,
                Type = movie.Type
            };
        }
    }
}
