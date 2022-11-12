using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;

namespace CinemaPlus.Services
{
    public class MovieService
    {
        public static dynamic Data { get; set; }
        public static dynamic SingleData { get; set; }
        public static List<Movie> GetMoviesBySearch(string movie, bool returnOneMovie = false)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=d5254632&s={movie}&plot=full").Result;
            var str = response.Content.ReadAsStringAsync().Result;  
            Data = JsonConvert.DeserializeObject(str);

            if (Data.Response == "True")
            {
                var movies = new List<Movie>();
                for (int i = 0; i < Data.Search.Count; i++)
                {
                    response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=d5254632&t={Data.Search[i].Title}&plot=full").Result;
                    str = response.Content.ReadAsStringAsync().Result;
                    SingleData = JsonConvert.DeserializeObject(str);
                    var mymovie = new Movie
                    {
                        Title = GetValue(SingleData.Title),
                        Year = GetValue(SingleData.Year),
                        Released = GetValue(SingleData.Released),
                        Runtime = GetValue(SingleData.Runtime),
                        Genre = GetValue(SingleData.Genre),
                        Director = GetValue(SingleData.Director),
                        Writer = GetValue(SingleData.Writer),
                        Actors = GetValue(SingleData.Actors),
                        Plot = GetValue(SingleData.Plot),
                        Language = GetValue(SingleData.Language),
                        Country = GetValue(SingleData.Country),
                        Awards = GetValue(SingleData.Awards),
                        ImdbRating = GetValue(SingleData.imdbRating),
                    };

                    if (SingleData.Poster == "N/A" || SingleData.Poster == null)
                        mymovie.Poster = @"\Images\noImage.jpg";
                    else
                        mymovie.Poster = SingleData.Poster;

                    mymovie.SetPrice();
                    if (mymovie.Formats.Count == 0)
                        MovieCellUCHelper.AddDetailsToMovie(mymovie);
                    if (!movies.Exists((m) => m.Title == mymovie.Title) && mymovie.Title != "N/A")
                        movies.Add(mymovie);

                    mymovie.Language = mymovie.Language.Split(',').ElementAt(0);
                    mymovie.Country = mymovie.Country.Split(',').ElementAt(0);

                    if (returnOneMovie)
                    {
                        if (movies.Count > 0)
                            return movies;
                    }
                }

                //filter movies by Imdb rating
                Helper.SortMoviesByImdbRating(movies);
                return movies;
            }
            else
            {
                return null;
            }
        }

        public static string GetValue(dynamic value)
        {
            if (value == null)
                return "N/A";

            return value;
        }
    }
}
