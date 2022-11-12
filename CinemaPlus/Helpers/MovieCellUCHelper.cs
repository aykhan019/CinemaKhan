using CinemaPlus.Models;
using CinemaPlus.Services;
using CinemaPlus.ViewModels.Movie;
using CinemaPlus.ViewModels.MovieViewModels;
using CinemaPlus.Views.UserControls;
using CinemaPlus.Views.UserControls.MovieUC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CinemaPlus.Helpers.MovieCellUCHelpers
{
    public class MovieCellUCHelper
    {
        public static void AddMoviesToView(TodayUC todayView)
        {
            var all_movies = App.Movies;
            todayView.MoviesWrapPanel.Children.Clear();
            foreach (var movie in all_movies)
            {
                MovieCellUC movieCellView = new MovieCellUC();
                MovieCellUCViewModel movieCellViewModel = new MovieCellUCViewModel();
                movieCellView.DataContext = movieCellViewModel;
                movieCellViewModel.Movie = movie;
                if (movie.Formats.Count == 0)
                    MovieCellUCHelper.AddDetailsToMovie(movie);

                if (string.IsNullOrEmpty(movie.Poster))
                {
                    movie.Poster = $@"..\..\Images\noImage.jpg";
                }
                movieCellViewModel.PosterImageSource = Helper.StringToImageSource(movie.Poster);
                for (int x = 0; x < movie.Formats.Count; x++)
                {
                    var format = new MovieDetailUC { DataContext = movie.Formats[x].DataContext };
                    if (x != 0) // 2d and subtitle
                    {
                        format.Image.Stretch = Stretch.UniformToFill;
                        format.Image.Height = movie.Formats[x].Image.Height;
                        format.Image.Width = movie.Formats[x].Image.Width;
                    }
                    movieCellView.MovieDetailsStackPanel.Children.Add(Helper.RemoveElementFromItsParent(format));
                }

                todayView.MoviesWrapPanel.Children.Add(movieCellView);
            }
        }

        public static void AddDetailsToMovie(Movie movie)
        {
            var detail1 = GetLangaugeDetail(movie.Language, movie.Country);
            var detail2 = GetMovieFormat();
            var detail3 = GetSubtitle(movie.Subtitle);

            movie.Formats.Add(detail1);
            movie.Formats.Add(detail2);
            movie.Formats.Add(detail3);
        }

        private static MovieDetailUC GetMovieFormat()
        {
            MovieDetailUC movieDetailView = new MovieDetailUC();
            var movieDetailUCViewModel = new MovieDetailUCViewModel();
            movieDetailView.DataContext = movieDetailUCViewModel;
            movieDetailUCViewModel.Header = movieDetailView.Header;
            movieDetailView.Border.Background = App.ColorsDictionary["firstColor"] as Brush;
            if (App.IsInAdminSide)
                movieDetailUCViewModel.ImageSource = Helper.StringToImageSource($@"..\..\Images\2dDark.png");
            else
                movieDetailUCViewModel.ImageSource = Helper.StringToImageSource($@"..\..\Images\2d.png");

            movieDetailView.Image.Stretch = Stretch.UniformToFill;
            movieDetailView.Image.VerticalAlignment = VerticalAlignment.Center;
            movieDetailView.Image.Width += 12;
            movieDetailView.Image.Height += 12;
            movieDetailUCViewModel.ToolTipText = $"Movie format : 2D";
            return movieDetailView;
        }

        private static MovieDetailUC GetSubtitle(Helper.Enums.Subtitles subtitle)
        {
            MovieDetailUC movieDetailView = new MovieDetailUC();
            var movieDetailUCViewModel = new MovieDetailUCViewModel();
            movieDetailView.DataContext = movieDetailUCViewModel;

            movieDetailUCViewModel.Header = movieDetailView.Header;

            movieDetailView.Border.Background = App.ColorsDictionary["firstColor"] as Brush;
            if (subtitle == Helper.Enums.Subtitles.Azerbaijani)
            {
                if (App.IsInAdminSide)
                {
                    movieDetailUCViewModel.ImageSource = Helper.StringToImageSource($@"..\..\Images\azSubDark.png");
                    movieDetailUCViewModel.ToolTipText = $"Movie with Aze subtitles";
                }
                else
                {
                    movieDetailUCViewModel.ImageSource = Helper.StringToImageSource($@"..\..\Images\azsub.png");
                    movieDetailUCViewModel.ToolTipText = $"Movie with Aze subtitles";
                }
            }
            else if (subtitle == Helper.Enums.Subtitles.Turkish)
            {
                if (App.IsInAdminSide)
                {
                    movieDetailUCViewModel.ImageSource = Helper.StringToImageSource($@"..\..\Images\trSubDark.png");
                    movieDetailUCViewModel.ToolTipText = $"Movie with TR subtitles";
                }
                else
                {
                    movieDetailUCViewModel.ImageSource = Helper.StringToImageSource($@"..\..\Images\trsub.png");
                    movieDetailUCViewModel.ToolTipText = $"Movie with TR subtitles";
                }
            }
            movieDetailView.Image.Stretch = Stretch.UniformToFill;
            movieDetailView.Image.VerticalAlignment = VerticalAlignment.Center;
            movieDetailView.Image.Width += 12;
            movieDetailView.Image.Height += 12;

            return movieDetailView;
        }

        private static MovieDetailUC GetLangaugeDetail(string language, string country)
        {
            if (language.Contains(",")) // if language is more than 1
            {
                language = language.Split(',').ElementAt(0).Trim();
            }
            MovieDetailUC movieDetailView = new MovieDetailUC();
            var movieDetailUCViewModel = new MovieDetailUCViewModel();
            movieDetailView.DataContext = movieDetailUCViewModel;

            movieDetailUCViewModel.Header = movieDetailView.Header;

            if (country == "UK")
            {
                country = "United Kingdom";
            }
            string countryFlag;
            if (language == "N/A")
            {
                countryFlag = "https://earthflag.store/wp-content/uploads/2020/09/flag-300x187-2.png";
                movieDetailUCViewModel.ToolTipText = $"No language data";
            }
            else
            {
                countryFlag = GetFlagSource(country);
                movieDetailUCViewModel.ToolTipText = $"Movie language : {language}";
            }
            movieDetailUCViewModel.ImageSource = Helper.StringToImageSource(countryFlag);
            return movieDetailView;
        }

        private static IEnumerable<RegionInfo> Regions { get; set; } = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
        private static string GetFlagSource(string country)
        {
            if (country.Contains(",")) // if country name is more than 1
            {
                country = country.Split(',').ElementAt(0).Trim();
            }

            var countryInfo = Regions.FirstOrDefault(region => region.EnglishName.Contains(country));
            if (countryInfo == null)
            {
                return "https://earthflag.store/wp-content/uploads/2020/09/flag-300x187-2.png";
            }

            string url = "https://countryflagsapi.com/png/";
            string countryAbbrev = countryInfo.TwoLetterISORegionName;
            url += countryAbbrev;

            if (country == "United States" || country == "USA")
            {
                country = "united states of america";
            }
            string url2 = $@"https://cdn.countryflags.com/thumbs/{country.Replace(" ", "-").Trim().ToLower()}/flag-square-250.png";

            //if (Helper.RemoteFileExists(url))
            //{
            //    return url;
            //}
            //else if (Helper.RemoteFileExists(url2))
            //{
            return url2;
            //}
            //else
            //{
            //    return "https://earthflag.store/wp-content/uploads/2020/09/flag-300x187-2.png";
            //}

        }

        public static void MovieClick(Movie movie, Helper.Enums.MovieTabs Tab)
        {
            var anotherMoviesView = new AnotherMoviesUC();
            List<Models.Movie> anotherMoviesRandom;
            do
            {
                anotherMoviesRandom = Helper.GetRandomMovies(4);
            }
            while (anotherMoviesRandom.Contains(App.SelectedMovie));
            foreach (var m in anotherMoviesRandom)
            {
                var movieCellView = new MovieCellUC();
                var movieCellViewModel = new MovieCellUCViewModel();
                movieCellViewModel.PosterImageSource = Helper.StringToImageSource(m.Poster);
                movieCellView.DataContext = movieCellViewModel;
                movieCellViewModel.Movie = m;
                for (int x = 0; x < m.Formats.Count; x++)
                {
                    var format = new MovieDetailUC { DataContext = m.Formats[x].DataContext };
                    if (x != 0) // 2d and subtitle
                    {
                        format.Image.Stretch = Stretch.UniformToFill;
                        format.Image.Height = m.Formats[x].Image.Height;
                        format.Image.Width = m.Formats[x].Image.Width;
                    }
                    movieCellView.MovieDetailsStackPanel.Children.Add(format);
                }
                anotherMoviesView.AnotherMoviesWrapPanel.Children.Add(movieCellView);
            }
            App.AnotherMoviesView = anotherMoviesView;
            App.SelectedMovie = movie;
            var movieTabsView = new MovieTabsUC();
            App.MoviePage = movieTabsView.MovieTabPage;
            var movieTabsViewModel = new MovieTabsUCViewModel();
            movieTabsViewModel.Movie = movie;
            movieTabsView.DataContext = movieTabsViewModel;
            App.PreviousPages.Add(App.PageWrapPanel.Children[0]);
            App.PageWrapPanel.Children.RemoveAt(0);
            App.PageWrapPanel.Children.Add(movieTabsView);
            App.MoviePage = movieTabsView.MovieTabPage;
            App.MoviePage.Children.Add(Helper.RemoveElementFromItsParent(movieTabsViewModel.MovieSchedulesView));
            movieTabsView.SessionsRB.IsChecked = false;
            movieTabsView.TrailerRB.IsChecked = false;
            movieTabsView.DescriptionRB.IsChecked = false;
            if (Tab == Helper.Enums.MovieTabs.SessionsTab)
            {
                movieTabsView.SessionsRB.IsChecked = true;
                movieTabsViewModel.SessionsCommand.Execute(null);
            }
            else if (Tab == Helper.Enums.MovieTabs.DescriptionTab)
            {
                movieTabsView.DescriptionRB.IsChecked = true;
                movieTabsViewModel.DescriptionCommand.Execute(null);
            }
        }
    }
}
