using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.Services;
using CinemaPlus.ViewModels.Movie;
using CinemaPlus.ViewModels.MovieViewModels;
using CinemaPlus.Views.UserControls.EndOfPage;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class AddMovieTabUCViewModel : BaseViewModel
    {
        public RelayCommand MouseEnterCommand { get; set; }
        public RelayCommand MouseLeaveCommand { get; set; }
        public RelayCommand IsFocusedCommand { get; set; }
        public RelayCommand IsNotFocusedCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand KeyDownCommand { get; set; }

        public WrapPanel SearchedMoviesWrapPanelAdminSide { get; internal set; }
        public RelayCommand SearchCommand { get; set; }

        public ScrollViewer SearchUCScrollViewer { get; set; }
        public TextBox SearchTb { get; set; }

        private readonly string DefaultText = "Search For Movie . . .";

        public AddMovieTabUCViewModel()
        {
            KeyDownCommand = new RelayCommand((key) =>
            {
                var _key = key as string;
                if (_key[key.ToString().Length - 1] == '\r')
                {
                    SearchCommand.Execute(null);
                }
            });

            MouseEnterCommand = new RelayCommand((m) =>
            {
                if (SearchTb.Text.Trim() == DefaultText)
                {
                    SearchTb.Text = String.Empty;
                }
            });

            MouseLeaveCommand = new RelayCommand((m) =>
            {
                if (SearchTb.Text.Trim() == String.Empty && SearchTb.IsFocused == false)
                {
                    SearchTb.Text = DefaultText;
                }
            });

            IsFocusedCommand = new RelayCommand((i) =>
            {
                SearchTb.Foreground = App.ColorsDictionary["fifthColor"] as System.Windows.Media.Brush;
            });

            IsNotFocusedCommand = new RelayCommand((i) =>
            {
                string text = SearchTb.Text.Trim();
                if (text == String.Empty || text == DefaultText)
                {
                    SearchTb.Foreground = App.ColorsDictionary["fourthColor"] as System.Windows.Media.Brush;
                    SearchTb.Text = DefaultText;
                }
            });

            SearchCommand = new RelayCommand((s) =>
            {
                string search = SearchTb.Text.Trim();
                if (search != string.Empty && search != DefaultText)
                {
                    SearchedMoviesWrapPanelAdminSide.Children.Clear();
                    var movies = MovieService.GetMoviesBySearch(search);
                    if (movies == null)
                    {
                        var noSearchResultView = new NoSearchResultUC();
                        SearchedMoviesWrapPanelAdminSide.Children.Add(noSearchResultView);
                    }
                    else
                    {
                        for (int x = movies.Count - 1; x >= 0; x--)
                        {
                            var movie = movies[x];
                            var movieView = new MovieCellUC();
                            Color color = ((SolidColorBrush)(App.ColorsDictionary["tenthColor"] as Brush)).Color;
                            movieView.BackgroundColorAnimationOfTbInMouseOver.To = color;
                            var movieViewModel = new MovieCellUCViewModel();
                            movieView.DataContext = movieViewModel;
                            movieViewModel.Movie = movie;
                            movieViewModel.PosterImageSource = Helper.StringToImageSource(movie.Poster);

                            if (movie.Formats.Count == 0)
                                MovieCellUCHelper.AddDetailsToMovie(movie);

                            for (int y = 0; y < movie.Formats.Count; y++)
                            {
                                var format = new MovieDetailUC { DataContext = movie.Formats[y].DataContext };
                                if (y != 0) // 2d and subtitle
                                {
                                    format.Image.Stretch = Stretch.UniformToFill;
                                    format.Image.Height = movie.Formats[y].Image.Height;
                                    format.Image.Width = movie.Formats[y].Image.Width;
                                }
                                movieView.MovieDetailsStackPanel.Children.Add(format);
                            }

                            SearchedMoviesWrapPanelAdminSide.Children.Add(movieView);
                        }
                    }
                    SearchUCScrollViewer.ScrollToTop();
                }
            });

            ClearCommand = new RelayCommand((c) =>
            {
                string text = SearchTb.Text.Trim();
                if (text != DefaultText && text != string.Empty)
                {
                    SearchTb.Text = string.Empty;
                }
            });
        }

        public void AddRandomMoviesToSearch()
        {
            SearchedMoviesWrapPanelAdminSide.Children.Clear();
            int movie_count = 16;
            List<Models.Movie> movies = Helper.GetRandomMoviesFromFile(movie_count);
            foreach (var m in movies)
            {
                var movieCellView = new MovieCellUC();
                Color color = ((SolidColorBrush)(App.ColorsDictionary["tenthColor"] as Brush)).Color;
                movieCellView.BackgroundColorAnimationOfTbInMouseOver.To = color;
                var movieViewModel = new MovieCellUCViewModel();
                movieViewModel.PosterImageSource = Helper.StringToImageSource(m.Poster);
                movieCellView.DataContext = movieViewModel;
                movieViewModel.Movie = m;
                var movieCellViewModel = new MovieCellUCViewModel()
                {
                    PosterImageSource = Helper.StringToImageSource(m.Poster),
                    Movie = m,
                };
                movieCellView.DataContext = movieCellViewModel;

                if (m.Formats.Count == 0)
                    MovieCellUCHelper.AddDetailsToMovie(m);

                for (int y = 0; y < m.Formats.Count; y++)
                {
                    var format = new MovieDetailUC { DataContext = m.Formats[y].DataContext };
                    if (y != 0) // 2d and subtitle
                    {
                        format.Image.Stretch = Stretch.UniformToFill;
                        format.Image.Height = m.Formats[y].Image.Height;
                        format.Image.Width = m.Formats[y].Image.Width;
                    }
                    movieCellView.MovieDetailsStackPanel.Children.Add(format);
                }

                SearchedMoviesWrapPanelAdminSide.Children.Add(movieCellView);
            }

        }
    }
}
