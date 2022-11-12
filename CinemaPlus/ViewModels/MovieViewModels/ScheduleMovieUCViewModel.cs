using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.ViewModels.Main;
using CinemaPlus.Views.Main;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class ScheduleMovieUCViewModel : BaseViewModel
    {
        private Models.Movie movie;

        public Models.Movie Movie
        {
            get { return movie; }
            set { movie = value; OnPropertyChanged(); }
        }

        private string sessionTime;

        public string SessionTime
        {
            get { return sessionTime; }
            set { sessionTime = value; OnPropertyChanged(); }
        }

        private string cinema;

        public string Cinema
        {
            get { return cinema; }
            set { cinema = value; OnPropertyChanged(); }
        }

        private string hall;

        public string Hall
        {
            get { return hall; }
            set { hall = value; OnPropertyChanged(); }
        }

        private StackPanel formats;

        public StackPanel Formats
        {
            get { return formats; }
            set { formats = value; OnPropertyChanged(); }
        }

        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        public RelayCommand PlacesCommand { get; set; }
        public RelayCommand MovieClickCommand { get; set; }

        public ScheduleMovieUCViewModel()
        {
            PlacesCommand = new RelayCommand((p) =>
            {
                App.SelectedMovie = movie;
                if (movie.Formats == null || movie.Formats.Count == 0)
                {
                    movie.Formats = new List<MovieDetailUC>();
                    MovieCellUCHelper.AddDetailsToMovie(movie);
                }

                movie = App.Cinemas.Find((c) => c.Name == Cinema).Halls.Find((h) => h.HallName == Hall).HallMovies.Find((m) => m.Title == movie.Title);
                var selectSeatsView = new SelectSeatsWindow();
                var selectSeatsViewModel = new SelectSeatsWindowViewModel();
                selectSeatsView.DataContext = selectSeatsViewModel;
                selectSeatsViewModel.SeatsWP = selectSeatsView.SeatsWrapPanel;

                DateTime date = DateTime.Parse(Movie.Session.Date);
                DateTime time = DateTime.Parse(Movie.Session.Time);
                DateTime dtCombined = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

                selectSeatsViewModel.Session = dtCombined.ToString("MM.dd.yyyy, HH:mm");
                selectSeatsViewModel.ConfirmButton = selectSeatsView.ConfirmButton;
                selectSeatsViewModel.MovieName = movie.Title;
                selectSeatsViewModel.PriceOfTicket = double.Parse(movie.Price.Replace("₼"," ").Trim());
                selectSeatsViewModel.CinemaAndHall = cinema + ", " + hall;
                selectSeatsViewModel.DisableConfirmButton();
                selectSeatsView.FormatsStackPanel.Children.Clear();
                for (int x = 0; x < movie.Formats.Count; x++)
                {
                    var format = new MovieDetailUC { DataContext = movie.Formats[x].DataContext };
                    if (x != 0) // 2d and subtitle
                    {
                        format.Image.Stretch = Stretch.UniformToFill;
                        format.Image.Height = movie.Formats[x].Image.Height;
                        format.Image.Width = movie.Formats[x].Image.Width;
                    }
                    selectSeatsView.FormatsStackPanel.Children.Add(format);
                }

                App.ChildWindow = selectSeatsView;
                selectSeatsView.Owner = App.Current.MainWindow;
                selectSeatsView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                App.Rectangle.Visibility = Visibility.Visible;
                for (int x = 0; x < selectSeatsViewModel.SeatsWP.Children.Count; x++)
                {
                    if (movie.BusySeats.Contains(x))
                    {
                        var tb = selectSeatsViewModel.SeatsWP.Children[x] as ToggleButton;
                        tb.IsEnabled = false;
                        tb.Background = App.ColorsDictionary["fifthColor"] as Brush;
                        tb.Foreground = App.ColorsDictionary["firstColor"] as Brush;
                    }
                }
                App.SelectedHall = Hall;
                App.SelectedCinema = Cinema;
                selectSeatsView.ShowDialog();
            });

            MovieClickCommand = new RelayCommand((m) => 
            {
                MovieCellUCHelper.MovieClick(Movie, Helper.Enums.MovieTabs.DescriptionTab);
            });
        }
    }
}
