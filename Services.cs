using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieLibraryEntities.Models;


namespace MovieLibrary
{
    public class Services
    {
        public string Startup()
        {
            Console.WriteLine("WELCOME TO BRANDON'S MOVIE LIBRARY!");
            Console.WriteLine("");
            Console.WriteLine("SELECT AN OPTION");
            Console.WriteLine("(1): DISPLAY A LIST OF MOVIES");
            Console.WriteLine("(2): ADD MOVIES TO THE LIBRARY");
            Console.WriteLine("(3): SEARCH FOR MOVIES");
            Console.WriteLine("(4): EDIT/UPDATE MOVIES");
            Console.WriteLine("(5): DELETE MOVIES FROM THE LIBRARY");
            Console.WriteLine("(6): ADD A NEW USER");
            Console.WriteLine("(7): DISPLAY A LIST OF USERS");
            Console.WriteLine("(8): RATE A MOVIE");
            Console.WriteLine("(X): EXIT THE PROGRAM");
            string option = Console.ReadLine().ToUpper();
            return option;
        }

        public string SearchOption()
        {
            Console.WriteLine("ENTER STRING TO SEARCH: ");
            string searchString = Console.ReadLine();
            return searchString;
        }

        public List<Movie> AddMovie(Database dbManager)
        {
            List<Movie> movieList = new List<Movie>();
            string title = "";
            do
            {
                List<MovieGenre> movieGenres = new List<MovieGenre>();
                Console.WriteLine("ENTER A MOVIE TITLE OR PRESS (X) TO SAVE AND EXIT: ");
                title = Console.ReadLine();
                if (title.ToUpper() == "X")
                {
                    break;
                }

                int year = 0;
                int month = 0;
                int day = 0;
                DateTime releaseDate = new DateTime();

                do
                {
                    Console.WriteLine("RELEASE DATE INFO");
                    Console.WriteLine("");
                    try
                    {
                        Console.WriteLine("ENTER RELEASE YEAR: ");
                        year = int.Parse(Console.ReadLine());
                        Console.WriteLine("ENTER RELEASE MONTH: ");
                        month = int.Parse(Console.ReadLine());
                        Console.WriteLine("ENTER RELEASE DAY: ");
                        day = int.Parse(Console.ReadLine());
                        releaseDate = new DateTime(year, month, day);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("INVALID DATE");
                        year = 0;
                        month = 0;
                        day = 0;
                    }

                } while (year == 0 || month == 0 || day == 0);

                Console.WriteLine("ADD A GENRE OR PRESS (X) TO EXIT: ");

                List<Genre> genres = dbManager.ReadGenres();
                string genreName;
                List<string> genreList = new List<string>();

                do
                {
                    genreName = Console.ReadLine();

                    if (genreName.ToUpper() == "X")
                    {
                        break;
                    }

                    if (genres.Where(g => g.Name.ToUpper() == genreName.ToUpper()).ToList().Count == 1)
                    {
                        genreList.Add(genreName);
                    }
                    else
                    {
                        Console.WriteLine("INVALID GENRE");
                    }

                } while (genreName.ToUpper() != "X");

                Movie movie = new Movie();
                movie.Title = title;
                movie.ReleaseDate = releaseDate;

                for (int i = 0; i < genreList.Count; i++)
                {
                    MovieGenre movieGenre = new MovieGenre();
                    movieGenre.Movie = movie;

                    var genres1 = genres.Where(g => g.Name.ToUpper() == genreList[i].ToUpper()).ToList();

                    if (genres1.Count == 1)
                    {
                        movieGenre.Genre = genres1[0];
                        movieGenres.Add(movieGenre);
                    }
                }

                movie.MovieGenres = movieGenres;
                movieList.Add(movie);

            } while (title.ToUpper() != "X");

            return movieList;
        }

        public void UpdateMovie(Search mediaSearch, Database dbManager, Media formatter)
        {
            Console.WriteLine();

            List<MovieGenre> movieGenres = new List<MovieGenre>();

            Console.WriteLine("SEARCH FOR A MOVIE TO EDIT/UPDATE: ");
            string searchString1 = Console.ReadLine().ToUpper();
            List<Movie> moviesSearched1;
            moviesSearched1 = mediaSearch.SearchMovies(dbManager.ReadMedia(), searchString1);

            if (moviesSearched1.Count > 0)
            {
                Console.WriteLine($"{moviesSearched1.Count} RESULTS FOUND");
                Console.WriteLine("ENTER MOVIE ID FOR THE MOVIE YOU WOULD LIKE TO EDIT/UPDATE: ");

                List<string> moviesString = formatter.FormatMovieToString(moviesSearched1, dbManager.ReadMovieGenres(), dbManager.ReadGenres());

                for (int i = 0; i < moviesString.Count; i++)
                {
                    Console.WriteLine(moviesString[i]);
                }

                int Id = 0;

                try
                {
                    Id = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("INVALID INPUT");
                }

                List<Movie> movieList = moviesSearched1.Where(m => m.Id == Id).ToList();

                if (movieList.Count != 1)
                {
                    Console.WriteLine("ID DOES NOT EXIST");
                }
                else
                {
                    Movie movie = movieList[0];
                    Console.WriteLine("ENTER A MOVIE TITLE: ");
                    string title = Console.ReadLine();

                    int year = 0;
                    int month = 0;
                    int day = 0;
                    DateTime releaseDate = new DateTime();

                    do
                    {
                        Console.WriteLine("RELEASE DATE INFO");
                        Console.WriteLine("");
                        try
                        {
                            Console.WriteLine("ENTER RELEASE YEAR: ");
                            year = int.Parse(Console.ReadLine());
                            Console.WriteLine("ENTER RELEASE MONTH: ");
                            month = int.Parse(Console.ReadLine());
                            Console.WriteLine("ENTER RELEASE DAY: ");
                            day = int.Parse(Console.ReadLine());
                            releaseDate = new DateTime(year, month, day);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("INVALID DATE");
                            year = 0;
                            month = 0;
                            day = 0;
                        }

                    } while (year == 0 || month == 0 || day == 0);


                    Console.WriteLine("ADD A GENRE OR PRESS (X) TO EXIT: ");

                    List<Genre> genres = dbManager.ReadGenres();
                    string genreName;
                    List<string> genreList = new List<string>();

                    do
                    {
                        genreName = Console.ReadLine();

                        if (genreName.ToUpper() == "X")
                        {
                            break;
                        }

                        if (genres.Where(g => g.Name.ToUpper() == genreName.ToUpper()).ToList().Count == 1)
                        {
                            genreList.Add(genreName);
                        }
                        else
                        {
                            Console.WriteLine("INVALID GENRE");
                        }

                    } while (genreName.ToUpper() != "X");

                    movie.Title = title;
                    movie.ReleaseDate = releaseDate;

                    for (int i = 0; i < genreList.Count; i++)
                    {
                        MovieGenre movieGenre = new MovieGenre();
                        movieGenre.Movie = movie;

                        var genres1 = genres.Where(g => g.Name.ToUpper() == genreList[i].ToUpper()).ToList();

                        if (genres1.Count == 1)
                        {
                            movieGenre.Genre = genres1[0];
                            movieGenres.Add(movieGenre);
                        }
                    }

                    movie.MovieGenres = movieGenres;
                    dbManager.Update(movie);
                    Console.WriteLine("MOVIE HAS BEEN EDITED/UPDATED SUCCESSFULLY!");
                }
            }
            else
            {
                Console.WriteLine("NO RESULTS FOUND");
            }

            Console.WriteLine();
        }

        public void DeleteMovie(Search mediaSearch, Database dbManager, Media formatter)
        {
            Console.WriteLine();
            Console.WriteLine("SEARCH FOR A MOVIE TO DELETE: ");
            string searchString2 = Console.ReadLine().ToUpper();
            List<Movie> moviesSearched2;
            moviesSearched2 = mediaSearch.SearchMovies(dbManager.ReadMedia(), searchString2);

            if (moviesSearched2.Count > 0)
            {
                Console.WriteLine($"{moviesSearched2.Count} RESULTS FOUND");
                Console.WriteLine("ENTER MOVIE ID FOR THE MOVIE YOU WOULD LIKE TO DELETE: ");

                List<string> moviesString = formatter.FormatMovieToString(moviesSearched2, dbManager.ReadMovieGenres(), dbManager.ReadGenres());

                for (int i = 0; i < moviesString.Count; i++)
                {
                    Console.WriteLine(moviesString[i]);
                }

                int Id = 0;

                try
                {
                    Id = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("INVALID INPUT");
                }

                List<Movie> movieList = moviesSearched2.Where(m => m.Id == Id).ToList();

                if (movieList.Count != 1)
                {
                    Console.WriteLine("ID DOES NOT EXIST");
                }
                else
                {
                    Movie deleteMovie = movieList[0];
                    dbManager.Delete(deleteMovie);
                    Console.WriteLine("MOVIE HAS BEEN DELETED SUCCESSFULLY!");
                }

            }
            else
            {
                Console.WriteLine("NO RESULTS FOUND");
            }

            Console.WriteLine();
        }

        public void AddUser(Database dbManager)
        {
            int age;
            List<User> users = new List<User>();
            do
            {
                User user = new User();
                Console.WriteLine("ENTER THE AGE OF USER OR TYPE (0) TO EXIT: ");
                age = -1;
                string gender = "";
                string zip = "0";
                string occupationName = "";

                do
                {
                    try
                    {
                        age = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("INVALID AGE");
                        age = -1;
                    }

                    if (age < 0)
                    {
                        if (age != -1)
                        {
                            Console.WriteLine("INVALID AGE");
                            age = -1;
                        }
                    }

                } while (age == -1);

                if (age != 0)
                {
                    do
                    {
                        Console.WriteLine("ENTER THE GENDER OF USER - (M): MALE (F): FEMALE: ");
                        gender = Console.ReadLine().ToUpper();

                        if (gender != "M" && gender != "F")
                        {
                            Console.WriteLine("INVALID GENDER");
                        }

                    } while (gender != "M" && gender != "F");

                    do
                    {
                        Console.WriteLine("ENTER THE ZIP CODE OF USER: ");
                        zip = Console.ReadLine();
                        Exception? exception = null;
                        bool isFiveChars = false;

                        try
                        {
                            int zipInt = int.Parse(zip);
                        }
                        catch (Exception e)
                        {
                            exception = e;
                        }

                        if (zip.Length == 5)
                        {
                            isFiveChars = true;
                        }

                        if (!isFiveChars || exception != null)
                        {
                            zip = "0";
                            Console.WriteLine("INVALID ZIP CODE");
                        }

                    } while (zip == "0");

                    Console.WriteLine("ENTER THE OCCUPATION OF USER: ");
                    occupationName = Console.ReadLine();

                    List<Occupation> occupations = dbManager.ReadOccupations();
                    List<string> occupationList = new List<string>();
                    Occupation occupation = new Occupation();

                    for (int i = 0; i < occupations.Count; i++)
                    {
                        occupationList.Add(occupations[i].Name);
                    }

                    if (occupationList.Contains(occupationName))
                    {
                        List<Occupation> occupations1 = occupations.Where(o => o.Name == occupationName).ToList();

                        if (occupations1.Count == 1)
                        {
                            occupation = occupations1[0];
                        }
                    }
                    else
                    {
                        occupation.Name = occupationName;
                    }

                    user.Occupation = occupation;
                    user.Age = age;
                    user.Gender = gender;
                    user.ZipCode = zip;
                    users.Add(user);

                    Console.WriteLine();
                    Console.WriteLine($"USER AGE: {age}");
                    Console.WriteLine($"USER GENDER: {gender}");
                    Console.WriteLine($"USER ZIPCODE: {zip}");
                    Console.WriteLine($"USER OCCUPATION: {occupation.Name}");
                    Console.WriteLine();

                }

            } while (age != 0);

            dbManager.AddUser(users);
        }

        public void DisplayUsers(List<string> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine(users[i]);
            }
        }

        public void RateMovie(Database dbManager, Search mediaSearch, Media formatter)
        {
            int userId = -1;
            int movieId = -1;
            User user = new User();
            Movie movie = new Movie();
            int Rating = 0;


            do
            {
                Console.WriteLine("ENTER THE ID OF USER: ");

                try
                {
                    userId = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("INVALID INPUT");
                }

                List<User> users = dbManager.ReadUsers().Where(u => u.Id == userId).ToList();

                if (users.Count == 1)
                {
                    user = users[0];
                }
                else
                {
                    Console.WriteLine("INVALID INPUT");
                    userId = -1;
                }

            } while (userId == -1);

            do
            {
                string searchString = "";
                Console.WriteLine("SEARCH A MOVIE TO RATE: ");
                searchString = Console.ReadLine().ToUpper();

                List<Movie> movies = mediaSearch.SearchMovies(dbManager.ReadMedia(), searchString);

                if (movies.Count > 0)
                {
                    Console.WriteLine($"{movies.Count} RESULTS FOUND");
                    Console.WriteLine("ENTER MOVIE ID FOR THE MOVIE YOU WOULD LIKE TO RATE: ");

                    List<string> moviesString = formatter.FormatMovieToString(movies, dbManager.ReadMovieGenres(), dbManager.ReadGenres());

                    for (int i = 0; i < moviesString.Count; i++)
                    {
                        Console.WriteLine(moviesString[i]);
                    }

                    try
                    {
                        movieId = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("INVALID INPUT");
                    }

                    List<Movie> movieList = movies.Where(m => m.Id == movieId).ToList();

                    if (movieList.Count != 1)
                    {
                        Console.WriteLine("ID DOES NOT EXIST");
                        movieId = -1;
                    }
                    else
                    {
                        movie = movieList[0];
                    }

                }
                else
                {
                    Console.WriteLine("NO RESULTS FOUND");
                    movieId = -1;
                }

            } while (movieId == -1);

            do
            {
                Console.WriteLine($"ENTER A MOVIE RATING - (1-5): {movie.Title}");

                try
                {
                    Rating = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("INVALID INPUT");
                }

                if (Rating > 5 || Rating < 1)
                {
                    Console.WriteLine("INVALID RATING");
                    Rating = 0;
                }

            } while (Rating == 0);

            UserMovie userMovie = new UserMovie();
            userMovie.Movie = movie;
            userMovie.Rating = Rating;
            userMovie.User = user;
            userMovie.RatedAt = DateTime.Now;
            dbManager.AddUserMovie(userMovie);

            List<Movie> movie1 = new List<Movie>();
            movie1.Add(movie);
            List<String> movieString = formatter.FormatMovieToString(movie1, dbManager.ReadMovieGenres(), dbManager.ReadGenres());
            string displayString = "";

            if (movieString.Count == 1)
            {
                displayString = movieString[0];
            }

            List<User> user1 = new List<User>();
            user1.Add(user);
            List<string> userString = formatter.FormatUserToString(user1, dbManager.ReadOccupations());
            string userDisplay = "";

            if (userString.Count == 1)
            {
                userDisplay = userString[0];
            }

            Console.WriteLine("MOVIE HAS BEEN RATED SUCCESSFULLY!");
            Console.WriteLine();
            Console.WriteLine("MOVIE DETAILS");
            Console.WriteLine($"{displayString}, RATING: {Rating}");
            Console.WriteLine();
            Console.WriteLine("USER DETAILS");
            Console.WriteLine(userDisplay);

        }

    }
}