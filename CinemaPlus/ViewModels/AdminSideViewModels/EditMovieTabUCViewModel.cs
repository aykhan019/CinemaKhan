using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.ViewModels.Movie;
using CinemaPlus.ViewModels.MovieViewModels;
using CinemaPlus.Views.UserControls.EndOfPage;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class EditMovieTabUCViewModel : BaseViewModel
    {
        public WrapPanel MoviesForEditWrapPanel { get; set; } = new WrapPanel();

        private List<string> languages;

        public List<string> Languages
        {
            get { return languages; }
            set { languages = value; OnPropertyChanged(); }
        }

        private List<string> cinemas;

        public List<string> Cinemas
        {
            get { return cinemas; }
            set { cinemas = value; OnPropertyChanged(); }
        }

        public RelayCommand CinemaChangedCommand { get; set; }
        public RelayCommand LanguageChangedCommand { get; set; }
        public RelayCommand EnglishOnlyCheckedCommand { get; set; }
        public RelayCommand EnglishOnlyUncheckedCommand { get; set; }

        public ScrollViewer EditMoviesTabScroll { get; set; }
        public ToggleButton EnglishTB { get; set; }

        public string SelectedLanguage { get; set; } = "All languages";
        public string SelectedCinema { get; set; } = "All Cinemas";

        public EditMovieTabUCViewModel()
        {
            languages = App.Languages;
            cinemas = Helper.GetCinemaNames();
            App.SelectedCinema = "All Cinemas";

            CinemaChangedCommand = new RelayCommand((cinemaName) =>
            {
                MoviesForEditWrapPanel.Children.Clear();
                SelectedCinema = cinemaName.ToString();
                FilterMovies();
            });

            LanguageChangedCommand = new RelayCommand((selectedLanguage) =>
            {
                MoviesForEditWrapPanel.Children.Clear();
                SelectedLanguage = selectedLanguage.ToString();
                if (SelectedLanguage != "English")
                {
                    EnglishTB.IsChecked = false;
                }
                FilterMovies();
            });

            EnglishOnlyCheckedCommand = new RelayCommand((LanguageComboBox) =>
            {
                var cb = LanguageComboBox as ComboBox;
                if (cb.Items.Contains("English"))
                {
                    cb.SelectedValue = "English";
                }
            });

            EnglishOnlyUncheckedCommand = new RelayCommand((LanguageComboBox) =>
            {
                var cb = LanguageComboBox as ComboBox;
                if (cb.SelectedValue.ToString() == "English")
                {
                    cb.SelectedValue = "All languages";
                }
            });
        }

        public void FilterMovies()
        {
            App.MoviesInMoviesForEditWrapPanel.Clear();
            MoviesForEditWrapPanel.Children.Clear();
            bool hasResult = false;
            if (SelectedCinema == "All Cinemas")
            {
                bool hasResultSupporter = false;
                foreach (var movie in App.DefaultMovies)
                {
                    hasResultSupporter = CreateMovieCell(movie);

                    if (hasResultSupporter)
                    {
                        hasResult = true;
                    }
                }
            }
            else
            {
                var cinema = App.Cinemas.Find((c) => c.Name == SelectedCinema);
                App.SelectedCinema = cinema.Name;
                bool hasResultSupporter = false;
                foreach (var hall in cinema.Halls)
                {
                    foreach (var movie in hall.HallMovies)
                    {
                        hasResultSupporter = CreateMovieCell(movie);

                        if (hasResultSupporter)
                        {
                            hasResult = true;
                        }
                    }
                }
            }

            if (!hasResult)
            {
                NoResultUC noResultUC = new NoResultUC();
                var noResultViewModel = new NoResultUCViewModel("No matching result found . . . ");
                noResultUC.DataContext = noResultViewModel;
                MoviesForEditWrapPanel.Children.Add(noResultUC);
            }
            EditMoviesTabScroll.ScrollToTop();
            //MoviesForEditWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.WeAreBackView));
            //MoviesForEditWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
        }

        //Returns what - returns if there was a cell created
        private bool CreateMovieCell(Models.Movie movie)
        {
            // to prevent repetition
            var M = App.MoviesInMoviesForEditWrapPanel.Find((m) => m.Title == movie.Title);
            if (M != null)
                return false;

            bool hasResult = false;
            if (SelectedLanguage == "All languages" || movie.Language == SelectedLanguage)
            {
                hasResult = true;
                var movieView = new MovieCellUC();
                Color color = ((SolidColorBrush)(App.ColorsDictionary["tenthColor"] as Brush)).Color;
                movieView.BackgroundColorAnimationOfTbInMouseOver.To = color;
                var movieViewModel = new MovieCellUCViewModel();
                movieViewModel.PosterImageSource = Helper.StringToImageSource(movie.Poster);
                movieView.DataContext = movieViewModel;
                movieViewModel.Movie = movie;
                if (movie.Formats.Count == 0)
                {
                    movie.Formats = new List<MovieDetailUC>();
                    MovieCellUCHelper.AddDetailsToMovie(movie);
                }
                for (int x = 0; x < movie.Formats.Count; x++)
                {
                    var format = new MovieDetailUC { DataContext = movie.Formats[x].DataContext };
                    if (x != 0) // 2d and subtitle
                    {
                        if (x == 1)
                            format.Image.Source = Helper.StringToImageSource($@"..\..\Images\2dDark.png");
                        if (x == 2)
                        {
                            if (movie.Subtitle == Helper.Enums.Subtitles.Azerbaijani)
                            {
                                format.Image.Source = Helper.StringToImageSource($@"..\..\Images\azSubDark.png");

                            }
                            else if (movie.Subtitle == Helper.Enums.Subtitles.Turkish)
                            {
                                format.Image.Source = Helper.StringToImageSource($@"..\..\Images\trSubDark.png");
                            }
                        }
                        format.Image.Stretch = Stretch.UniformToFill;
                        format.Image.Height = movie.Formats[x].Image.Height;
                        format.Image.Width = movie.Formats[x].Image.Width;
                    }
                    movieView.MovieDetailsStackPanel.Children.Add(format);
                }
                App.MoviesInMoviesForEditWrapPanel.Add(movie);
                MoviesForEditWrapPanel.Children.Add(movieView);
            }
            return hasResult;
        }
    }
}
