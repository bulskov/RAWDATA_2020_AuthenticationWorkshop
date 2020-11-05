using System;
using System.Collections.Generic;
using System.Linq;

namespace DataServiceLib
{
    public interface IDataService
    {
        IList<Movie> GetMovies(int userId);
        Movie GetMovie(int userId, string movieId);
        User GetUser(string username);
        User CreateUser(string name, string username);
    }

    public class DataService : IDataService
    {
        private List<Movie> _movies = Data.Movies;
        private List<User> _users = Data.Users;

        public IList<Movie> GetMovies(int userId)
        {
            if(_users.FirstOrDefault(x => x.Id == userId) == null)
                throw new ArgumentException("User not found");
            return _movies;
        }

        public Movie GetMovie(int userId, string movieId)
        {
            if (_users.FirstOrDefault(x => x.Id == userId) == null)
                throw new ArgumentException("User not found");
            return _movies.FirstOrDefault(x => x.Id == movieId);
        }

        public User GetUser(string username)
        {
            return _users.FirstOrDefault(x => x.Username == username);
        }

        public User CreateUser(string name, string username)
        {
            var user = new User
            {
                Id = _users.Max(x => x.Id) + 1,
                Name = name,
                Username = username
            };
            _users.Add(user);
            return user;
        }

    }
}
