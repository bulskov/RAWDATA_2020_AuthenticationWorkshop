using System;
using System.Linq;
using DataServiceLib;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceToken.Controllers
{
    [ApiController]
    [Route("api/v3/movies")]
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
            try
            {
                var movies = _dataService.GetMovies(-1);
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
            try
            {
                var movie = _dataService.GetMovie(-1, movieId);
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
                Url = Url.Link(nameof(GetMovie), new { Id = movie.Id}),
                Title = movie.Title,
                Type = movie.Type
            };
        }
    }
}
