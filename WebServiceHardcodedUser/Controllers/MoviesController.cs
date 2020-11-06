using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib;
using Microsoft.AspNetCore.Mvc;
using WebServiceHardcodedUser.Models;

namespace WebServiceHardcodedUser.Controllers
{
    [ApiController]
    [Route("api/v1/movies")]
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
                var movies = _dataService.GetMovies(Program.CurrentUserId);
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
                var movie = _dataService.GetMovie(Program.CurrentUserId, movieId);
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
