using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryEntities.Models;
using MovieLibraryEntities;

namespace MovieLibrary
{
class MovieLibrary
    {
        static void Main(string[] args)
        {
            Dependency dep = new Dependency();

            Services service = dep.GetInputOutputService();
            Media formatter = dep.GetFormatter();
            Search mediaSearch = dep.GetSearch();
            Database dbManager = dep.GetDatabase();

            string option;

            do
            {
                option = service.Startup();

                switch (option)
                {
                    case "1":
                        List<string> movies = formatter.FormatMovieToString(dbManager.ReadMedia(), dbManager.ReadMovieGenres(), dbManager.ReadGenres());

                        for (int i = 0; i < movies.Count; i++)
                        {
                            Console.WriteLine(movies[i]);
                        }
                        Console.WriteLine();

                        break;
                    case "2":
                        dbManager.WriteMedia(service.AddMovie(dbManager));
                        Console.WriteLine();

                        break;
                    case "3":

                        Console.WriteLine();
                        string searchString = service.SearchOption().ToUpper();
                        Console.WriteLine();

                        List<Movie> movies1 = dbManager.ReadMedia();
                        List<Movie> moviesSearched = mediaSearch.SearchMovies(movies1, searchString);

                        if (moviesSearched.Count > 0)
                        {
                            Console.WriteLine($"{moviesSearched.Count} RESULTS FOUND");

                            List<string> moviesString = formatter.FormatMovieToString(moviesSearched, dbManager.ReadMovieGenres(), dbManager.ReadGenres());

                            for (int i = 0; i < moviesString.Count; i++)
                            {
                                Console.WriteLine(moviesString[i]);
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("NO RESULTS FOUND");
                        }

                        Console.WriteLine();
                        break;
                    case "4":
                        service.UpdateMovie(mediaSearch, dbManager, formatter);
                        break;
                    case "5":
                        service.DeleteMovie(mediaSearch, dbManager, formatter);
                        break;
                    case "6":
                        service.AddUser(dbManager);
                        break;
                    case "7":
                        Console.WriteLine();
                        service.DisplayUsers(formatter.FormatUserToString(dbManager.ReadUsers(), dbManager.ReadOccupations()));
                        Console.WriteLine();
                        break;
                    case "8":
                        Console.WriteLine();
                        service.DisplayUsers(formatter.FormatUserToString(dbManager.ReadUsers(), dbManager.ReadOccupations()));
                        service.RateMovie(dbManager, mediaSearch, formatter);
                        break;
                }
            } while (option != "X");
        }
    }
}