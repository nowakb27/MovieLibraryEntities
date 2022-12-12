using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using MovieLibraryEntities.Migrations;

namespace MovieLibrary
{
    public class Database
    {
        private readonly MovieContext context = new MovieContext();

        public List<Movie> ReadMedia()
        {
            List<Movie> movies = new List<Movie>();
            movies = context.Movies.ToList();
            return movies;
        }

        public List<MovieGenre> ReadMovieGenres()
        {
            List<MovieGenre> movieGenres = new List<MovieGenre>();
            movieGenres = context.MovieGenres.ToList();
            return movieGenres;
        }

        public List<Genre> ReadGenres()
        {
            List<Genre> genres = new List<Genre>();
            genres = context.Genres.ToList();
            return genres;
        }

        public void WriteMedia(List<Movie> movies)
        {
            for (int i = 0; i < movies.Count; i++)
            {
                context.Movies.Add(movies[i]);
            }

            context.SaveChanges();
        }

        public void Update(Movie movie)
        {
            context.Movies.Update(movie);
            context.SaveChanges();
        }

        public void Delete(Movie movie)
        {
            context.Movies.Remove(movie);
            context.SaveChanges();
        }

        public void AddUser(List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                context.Users.Add(users[i]);
            }

            context.SaveChanges();
        }

        public List<User> ReadUsers()
        {
            List<User> users = context.Users.ToList();
            return users;
        }

        public List<Occupation> ReadOccupations()
        {
            List<Occupation> occupations = context.Occupations.ToList();
            return occupations;
        }

        public void AddUserMovie(UserMovie userMovie)
        {
            context.UserMovies.Add(userMovie);
            context.SaveChanges();
        }

        public List<UserMovie> ReadUserMovies()
        {
            List<UserMovie> userMovies = context.UserMovies.ToList();
            return userMovies;
        }

    }
}