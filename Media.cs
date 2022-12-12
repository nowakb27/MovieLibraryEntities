using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MovieLibraryEntities.Models;

namespace MovieLibrary
{
    public class Media
    {
        public List<string> FormatMovieToString(List<Movie> movies, List<MovieGenre> movieGenresList, List<Genre> genresList)
        {
            List<string> MovieList = new List<string>();

            for (int i = 0; i < movies.Count; i++)
            {
                long id = movies[i].Id;
                string title = movies[i].Title;
                string year = movies[i].ReleaseDate.Year.ToString();
                List<MovieGenre> movieGenres = movieGenresList.Where(g => g.Movie == movies[i]).ToList();
                List<Genre> genres = new List<Genre>();

                for (int j = 0; j < movieGenres.Count; j++)
                {
                    var genresLoop = (genresList.Where(g => g.Id == movieGenres[j].Genre.Id).ToList());

                    for (int k = 0; k < genresLoop.Count; k++)
                    {
                        genres.Add(genresLoop[k]);
                    }
                }

                string line;
                string genre = "";

                if (title.Contains(","))
                {

                    if (title.Contains(year))
                    {
                        line = $@"ID: {id}, Title: ""{title}""";
                    }
                    else
                    {
                        line = $@"ID: {id}, Title: ""{title}"" ({year})";
                    }

                }
                else
                {
                    if (title.Contains(year))
                    {
                        line = $"ID: {id}, Title: {title}";
                    }
                    else
                    {
                        line = $"ID: {id}, Title: {title} ({year})";
                    }

                }

                for (int j = 0; j < genres.Count; j++)
                {
                    if (genres.Count != j + 1)
                    {
                        genre += genres[j].Name + "|";
                    }
                    else
                    {
                        genre += genres[j].Name;
                    }
                }
                MovieList.Add($"{line}, Genres: {genre}");
            }

            return MovieList;
        }

        public List<string> FormatUserToString(List<User> users, List<Occupation> occupations)
        {
            List<string> usersString = new List<string>();

            for (int i = 0; i < users.Count; i++)
            {
                List<Occupation> occupations1 = occupations.Where(o => o == users[i].Occupation).ToList();
                Occupation occupation = new Occupation();

                if (occupations1.Count == 1)
                {
                    occupation = occupations1[0];
                }

                string user = $"ID: {users[i].Id}, Occupation: {occupation.Name}, Age: {users[i].Age}, Gender: {users[i].Gender}, Zipcode: {users[i].ZipCode}";
                usersString.Add(user);
            }

            return usersString;
        }
    }
}