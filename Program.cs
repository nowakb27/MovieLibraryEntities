using Microsoft.EntityFrameworkCore;
using MovieLibraryEntities.Context;
using MovieLibraryEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibraryEntities
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string INPUT;
            do
            {
                Console.WriteLine("(1): SEARCH/VIEW MOVIES");
                Console.WriteLine("(2): ADD A MOVIE");
                Console.WriteLine("(3): UPDATE A MOVIE");
                Console.WriteLine("(4): DELETE A MOVIE");
                Console.WriteLine("(5): EXIT");
                    INPUT = Convert.ToString(Console.ReadLine());

                switch (INPUT)
                {
                    case "1":  
                        {
                            using (var db = new MovieContext())
                            {
                                Console.WriteLine("(1): SEARCH MOVIE TITLE");
                                Console.WriteLine("(2): VIEW ALL MOVIES");
                                string INPUT2 = Console.ReadLine();
                                switch (INPUT2)
                                {
                                    case "1": 
                                        {
                                            Console.WriteLine("ENTER MOVIE TITLE: ");
                                            var MOVIETITLE = Console.ReadLine();
                                            bool NULL = string.IsNullOrEmpty(MOVIETITLE);

                                            if (NULL == false)
                                            {
                                                var SHOWMOVIE = db.Movies.Include(x => x.MovieGenres).ThenInclude(x => x.Genre).Where(x => x.Title.ToUpper().Contains(MOVIETITLE.ToUpper()));

                                                if (SHOWMOVIE is not null)
                                                {
                                                    foreach (var movie in SHOWMOVIE)
                                                    {
                                                        Console.WriteLine($"Movie {movie.Id}: {movie.Title}, Release Date: {movie.ReleaseDate}");
                                                        foreach (var genre in movie.MovieGenres)
                                                        {
                                                            Console.WriteLine($"\tGenre {genre.Genre.Id}: {genre.Genre.Name}");
                                                        }
                                                        Console.WriteLine();
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("TITLE DOES NOT EXIST\n");
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR\n");
                                                break;
                                            }
                                            break;
                                        }
                                    case "2": 
                                        {
                                            Console.WriteLine("MOVIE LIST: ");
                                            foreach (var MOVIE in db.Movies)
                                            {
                                                Console.WriteLine($"Movie {MOVIE.Id}: {MOVIE.Title}, Release Date: {MOVIE.ReleaseDate}");
                                            }
                                            Console.WriteLine();
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("ERROR\n");
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case "2":  
                        {
                            using (var db = new MovieContext())
                            {
                                Console.WriteLine("ENTER MOVIE TITLE: ");
                                var INPUT3 = Console.ReadLine();
                                bool NULL2 = string.IsNullOrEmpty(INPUT3);

                                if (NULL2 == false)
                                {
                                    Console.WriteLine("ENTER RELEASE DATE OF MOVIE (DD/MM/YYYY): ");
                                    DateTime DATE;
                                    if (DateTime.TryParse(Console.ReadLine(), out DATE))
                                    {

                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR\n");
                                        break;
                                    }

                                    var MOVIES = new Movie()
                                    {
                                        Title = INPUT3,
                                        ReleaseDate = DATE
                                    };
                                    db.Movies.Add(MOVIES);
                                    db.SaveChanges();

                                    var ADDMOVIE = db.Movies.Where(x => x.Title == INPUT3).FirstOrDefault();
                                    Console.WriteLine($"Movie {ADDMOVIE.Id}: {ADDMOVIE.Title}, Release Date: {ADDMOVIE.ReleaseDate}");
                                }
                                else
                                {
                                    Console.WriteLine("ERROR\n");
                                    break;
                                }
                            }
                            break;
                        }
                    case "3":  
                        {
                            using (var db = new MovieContext())
                            {
                                Console.WriteLine("ENTER EXISTING MOVIE TITLE: ");
                                var INPUT4 = Console.ReadLine();

                                var UPDATE = db.Movies.FirstOrDefault(x => x.Title.ToUpper().Contains(INPUT4.ToUpper()));
                                Console.WriteLine($"Movie {UPDATE.Id}: {UPDATE.Title}, Release Date: {UPDATE.ReleaseDate}\n");

                                Console.WriteLine("ENTER NEW MOVIE TITLE: ");
                                var UPDATE2 = Console.ReadLine();
                                bool NULL3 = string.IsNullOrEmpty(UPDATE2);

                                if (NULL3 == false)
                                {
                                    Console.WriteLine("ENTER NEW RELEASE DATE OF MOVIE (DD/MM/YYYY): ");
                                    DateTime UPDATE3;
                                    if (DateTime.TryParse(Console.ReadLine(), out UPDATE3))
                                    {

                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR\n");
                                        break;
                                    }

                                    var UPDATE4 = db.Movies.FirstOrDefault(x => x.Title.ToUpper().Contains(INPUT4.ToUpper()));

                                    UPDATE4.Title = UPDATE2;
                                    UPDATE4.ReleaseDate = UPDATE3;

                                    db.Movies.Update(UPDATE4);
                                    db.SaveChanges();

                                    Console.WriteLine($"Movie {UPDATE4.Id}: {UPDATE4.Title}, Release Date: {UPDATE4.ReleaseDate}\n");
                                }
                                else
                                {
                                    Console.WriteLine("ERROR\n");
                                    break;
                                }
                            }
                            break;
                        }
                    case "4":  
                        {
                            using (var db = new MovieContext())
                            {
                                string BOOLEAN;
                                do
                                {
                                    Console.WriteLine("ENTER MOVIE TITLE: ");
                                    var INPUT5 = Console.ReadLine();
                                    bool NULL4 = string.IsNullOrEmpty(INPUT5);

                                    if (NULL4 == false)
                                    {
                                        var DELETE = db.Movies.FirstOrDefault(x => x.Title.ToUpper().Contains(INPUT5.ToUpper()));
                                        if (DELETE is not null)
                                        {
                                            Console.WriteLine($"Movie {DELETE.Id}: {DELETE.Title}, Release Date: {DELETE.ReleaseDate}");

                                            Console.WriteLine("\nWOULD YOU LIKE TO DELETE THIS MOVIE?");
                                            Console.WriteLine("\n(1): YES");
                                            Console.WriteLine("\n(2): NO");
                                            BOOLEAN = Convert.ToString(Console.ReadLine());
                                            switch (BOOLEAN)
                                            {
                                                case "1":
                                                    {
                                                        db.Movies.Remove(DELETE);
                                                        db.SaveChanges();

                                                        Console.WriteLine($"Movie {DELETE.Id}: {DELETE.Title}, Release Date: {DELETE.ReleaseDate}");
                                                        break;
                                                    }
                                                case "2":
                                                    {
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        Console.WriteLine("ERROR\n");
                                                        break;
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("TITLE DOES NOT EXIST\n");
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR\n");
                                        break;
                                    }
                                } while (BOOLEAN == "2");
                            }
                            break;
                        }
                    case "5":
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR\n");
                            break;
                        }
                }

            } while (INPUT != "5");
        }
    }
}
