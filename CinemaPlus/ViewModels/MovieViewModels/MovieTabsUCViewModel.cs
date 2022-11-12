using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.ViewModels.TabsViewModels;
using CinemaPlus.Views.UserControls;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieTabsUCViewModel : BaseViewModel
    {
        public RelayCommand SessionsCommand { get; set; }
        public RelayCommand DescriptionCommand { get; set; }
        public RelayCommand TrailerCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public Models.Movie Movie { get; set; }

        public MovieSchedulesUC MovieSchedulesView { get; set; }
        public MovieSchedulesUCViewModel MovieSchedulesViewModel { get; set; }

        public MovieDescriptionUC MovieDescriptionView { get; set; }
        public MovieDescriptionUCViewModel MovieDescriptionViewModel { get; set; }

        public MovieTrailerUC MovieTrailerView { get; set; }
        public MovieTrailerUCViewModel MovieTrailerViewModel { get; set; }


        public MovieTabsUCViewModel()
        {
            //Sessions View
            MovieSchedulesView = new MovieSchedulesUC();
            MovieSchedulesViewModel = new MovieSchedulesUCViewModel(MovieSchedulesView.OneMovieSchedulesWrapPanel, MovieSchedulesView.MovieSchedulesScroll);
            MovieSchedulesView.DataContext = MovieSchedulesViewModel;
            MovieSchedulesViewModel.TodayRB = MovieSchedulesView.TodayRB;
            MovieSchedulesViewModel.TomorrowRB = MovieSchedulesView.TomorrowRB;
            MovieSchedulesViewModel.DatesComboBox = MovieSchedulesView.datesCBox;
            MovieSchedulesViewModel.FilterAllSchedules();

            // Description View
            MovieDescriptionView = new MovieDescriptionUC();
            MovieDescriptionViewModel = new MovieDescriptionUCViewModel();
            MovieDescriptionView.DataContext = MovieDescriptionViewModel;
            MovieDescriptionViewModel.PosterImageSource = Helper.StringToImageSource(App.SelectedMovie.Poster);

            var m = App.SelectedMovie;
            for (int x = 0; x < m.Formats.Count; x++)
            {
                var format = new MovieDetailUC { DataContext = m.Formats[x].DataContext };
                if (x != 0) // 2d and subtitle
                {
                    format.Image.Stretch = Stretch.UniformToFill;
                    format.Image.Height = m.Formats[x].Image.Height;
                    format.Image.Width = m.Formats[x].Image.Width;
                }
                MovieDescriptionView.FormatsStackPanel.Children.Add(format);
            }

            // Trailer View
            MovieTrailerView = new MovieTrailerUC();
            MovieTrailerViewModel = new MovieTrailerUCViewModel();
            MovieTrailerView.DataContext = MovieTrailerViewModel;
            MovieTrailerViewModel.Web = MovieTrailerView.Web;
            App.Web = MovieTrailerViewModel.Web;
            MovieTrailerViewModel.Web.IsEnabled = false;
            MovieTrailerView.IsEnabled = false;

            App.AnotherMoviesView.HorizontalAlignment = HorizontalAlignment.Center;
            SessionsCommand = new RelayCommand((s) =>
            {
                MovieTrailerView.Web.Source = new Uri($"https://www.youtube.com/results?search_query={Movie.Title.Replace(" ", "+")}+Trailer+{Movie.Year}");
                App.OneMovieSchedulesWrapPanel = MovieSchedulesView.OneMovieSchedulesWrapPanel;
                if (App.MoviePage.Children.Count > 0)
                    App.MoviePage.Children.RemoveAt(0);
                App.MoviePage.Children.Add(Helper.RemoveElementFromItsParent(MovieSchedulesView));
                App.SelectedMovie = Movie;
                MovieSchedulesViewModel.FilterAllSchedules();
                MovieSchedulesView.MovieSchedulesScroll.ScrollToTop();
            });
            DescriptionCommand = new RelayCommand((d) =>
            {
                MovieTrailerView.Web.Source = new Uri($"https://www.youtube.com/results?search_query={Movie.Title.Replace(" ", "+")}+Trailer+{Movie.Year}");
                if (App.MoviePage.Children.Count > 0)
                    App.MoviePage.Children.RemoveAt(0);
                App.MoviePage.Children.Add(Helper.RemoveElementFromItsParent(MovieDescriptionView));
                App.SelectedMovie = Movie;
                if (MovieDescriptionView.EndOfDescription.Children.Count == 0)
                    MovieDescriptionView.EndOfDescription.Children.Add(Helper.RemoveElementFromItsParent(App.AnotherMoviesView));
                MovieDescriptionView.EndOfDescription.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                MovieDescriptionView.MovieDescriptionScroll.ScrollToTop();
            });

            TrailerCommand = new RelayCommand((t) =>
            {
                if (App.MoviePage.Children.Count > 0)
                    App.MoviePage.Children.RemoveAt(0);
                App.MoviePage.Children.Add(Helper.RemoveElementFromItsParent(MovieTrailerView));
                App.SelectedMovie = Movie;
                MovieTrailerView.Web.Source = new Uri($"https://www.youtube.com/results?search_query={Movie.Title.Replace(" ", "+")}+Trailer+{Movie.Year}");
                //MovieTrailerViewModel.Navigate($"http://gdata.youtube.com/feeds/api/videos/-/{Movie.Title.Replace(" ","-")}-trailer?max-results=1");
                //MovieTrailerViewModel.Navigate($"https://www.youtube.com/results?search_query={Movie.Title.Replace(" ", "+")}");
            });

            BackCommand = new RelayCommand((b) =>
            {
                MovieTrailerView.Web.Source = new Uri($"https://www.youtube.com/results?search_query={Movie.Title.Replace(" ", "+")}+Trailer+{Movie.Year}");
                if (App.PreviousPages[App.PreviousPages.Count - 1] is MovieTabsUC movieTabsUC)
                {
                    App.MoviePage = movieTabsUC.MovieTabPage;
                    var movieTabsviewModel = movieTabsUC.DataContext as MovieTabsUCViewModel;
                    App.SelectedMovie = movieTabsviewModel.Movie;
                    App.OneMovieSchedulesWrapPanel = movieTabsviewModel.MovieSchedulesView.OneMovieSchedulesWrapPanel;

                    //movieTabsviewModel.MovieSchedulesView.OneMovieSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                    if (movieTabsviewModel.MovieDescriptionView.EndOfDescription.Children.Count == 0)
                    {
                        movieTabsviewModel.MovieDescriptionView.EndOfDescription.Children.Add(Helper.RemoveElementFromItsParent(App.AnotherMoviesView));
                    }
                    movieTabsviewModel.MovieDescriptionView.EndOfDescription.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                }
                App.PageWrapPanel.Children.RemoveAt(0);
                App.PageWrapPanel.Children.Add(App.PreviousPages[App.PreviousPages.Count - 1]);
                App.PreviousPages.RemoveAt(App.PreviousPages.Count - 1);

                if (App.PreviousPages.Count == 1) // home page
                {
                    //App.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.AnotherMoviesView));
                    App.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                }
            });
        }
    }
}
