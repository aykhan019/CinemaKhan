using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Models;
using CinemaPlus.Views.UserControls.AdminSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static CinemaPlus.ViewModels.AdminSideViewModels.EditMovieSeatsTabUCViewModel;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class AddMovieSessionUCViewModel : BaseViewModel
    {
        public RelayCommand BackCommand { get; set; }
        public RelayCommand CinemaChangedCommand { get; set; }
        public RelayCommand HallChangedCommand { get; set; }
        public RelayCommand DateChangedCommand { get; set; }
        public RelayCommand TimeChangedCommand { get; set; }
        public RelayCommand PriceChangedCommand { get; set; }
        public RelayCommand AddSessionCommand { get; set; }

        public List<string> AllCinemas { get; set; } = new List<string>();
        public List<string> AllHalls { get; set; } = new List<string>();
        public List<string> AllDates { get; set; } = new List<string>();
        public List<string> AllTimes { get; set; } = new List<string>();

        private string price = string.Empty;

        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); PriceChangedCommand.Execute(null); }
        }

        public MovieSessionUC MovieSessionView { get; set; } = new MovieSessionUC();
        public MovieSessionUCViewModel MovieSessionViewModel { get; set; } = new MovieSessionUCViewModel();

        EditMovieSessionsTabUCViewModel SessionsTabViewModel { get; set; }

        public AddMovieSessionUCViewModel(EditMovieSessionsTabUCViewModel _SessionsTabViewModel)
        {
            SessionsTabViewModel = _SessionsTabViewModel;
            MovieSessionViewModel.Cinema = "Select Cinema";
            MovieSessionViewModel.Hall = "Select Hall";
            MovieSessionViewModel.Date = "Select Date";
            MovieSessionViewModel.Time = "Select Time";
            MovieSessionViewModel.Price = "Type Price";

            foreach (var cinema in App.Cinemas)
            {
                AllCinemas.Add(cinema.Name);
                foreach (var hall in cinema.Halls)
                {
                    AllHalls.Add(hall.HallName);
                }
            }
            AllHalls = AllHalls.Distinct().ToList();
            AllDates = Helper.GetDates();
            AllTimes = Helper.GetAllTimes();

            BackCommand = new RelayCommand((b) =>
            {
                App.EditMoviePageStackPanel.Children.RemoveAt(0);
                App.EditMoviePageStackPanel.Children.Add(App.AdminSidePreviousPage);
                SessionsTabViewModel.UpdateSessions();
                App.AdminSidePreviousPage = null;
            });

            CinemaChangedCommand = new RelayCommand((selectedCinema) =>
            {
                MovieSessionViewModel.Cinema = selectedCinema.ToString();
            });

            HallChangedCommand = new RelayCommand((selectedHall) =>
            {
                MovieSessionViewModel.Hall = selectedHall.ToString();
            });

            DateChangedCommand = new RelayCommand((selectedDate) =>
            {
                MovieSessionViewModel.Date = selectedDate.ToString();
            });

            TimeChangedCommand = new RelayCommand((selectedTime) =>
            {
                MovieSessionViewModel.Time = selectedTime.ToString();
            });

            PriceChangedCommand = new RelayCommand((p) =>
            {
                if (Price.StartsWith("0"))
                {
                    do
                    {
                        Price = Price.Remove(0, 1);
                    } while (Price.StartsWith("0"));
                }

                MovieSessionViewModel.Price = Price.Trim() + ".00 ₼";
            });

            AddSessionCommand = new RelayCommand((a) =>
            {
                Notifier notifier = new Notifier(cfg =>
                {
                    cfg.PositionProvider = new WindowPositionProvider(
                        parentWindow: App.ChildWindow,
                        corner: Corner.TopLeft,
                        offsetX: 272,
                        offsetY: 717);

                    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                        notificationLifetime: TimeSpan.FromSeconds(3),
                        maximumNotificationCount: MaximumNotificationCount.FromCount(1));

                    cfg.Dispatcher = Application.Current.Dispatcher;
                });
                if (MovieSessionViewModel.Cinema == "Select Cinema")
                {
                    notifier.ShowError("Please, select cinema!");
                    return;
                }
                if (MovieSessionViewModel.Hall == "Select Hall")
                {
                    notifier.ShowError("Please, select hall!");
                    return;
                }
                if (MovieSessionViewModel.Date == "Select Date")
                {
                    notifier.ShowError("Please, select date!");
                    return;
                }
                if (MovieSessionViewModel.Time == "Select Time")
                {
                    notifier.ShowError("Please, select time!");
                    return;
                }
                if (MovieSessionViewModel.Price == "Type Price")
                {
                    notifier.ShowError("Please, type price!");
                    return;
                }
                var stringPrice = MovieSessionViewModel.Price.Replace("₼", String.Empty).Trim();
                var _price = double.Parse(stringPrice);
                if (_price == 0)
                {
                    notifier.ShowError("Price cannot be 0 or empty!");
                    return;
                }

                var session = new Session()
                {
                    Cinema = MovieSessionViewModel.Cinema,
                    Hall = MovieSessionViewModel.Hall,
                    Date = MovieSessionViewModel.Date.Replace("/","."),
                    Time = DateTime.Parse(MovieSessionViewModel.Time).ToLongTimeString().Replace(":00 "," ").Trim(),
                    Price = _price.ToString() + ".00 ₼",
                };

                var date = DateTime.Parse(session.Date + " " + session.Time);
                var detailedHall = new DetailedHall()
                {
                    BusySeats = new List<int>(),
                    Cinema = session.Cinema,
                    Date = date.ToShortDateString().Replace("/", ".") + ", " + date.ToLongTimeString().Replace(":00 ", " ").Trim(),
                    Hallname = session.Hall
                };
                //Date = _movie.Session.ToShortDateString().Replace("/", "."),
                //                Time = _movie.Session.ToLongTimeString().Replace(":00 ", " ").Trim(),
                App.BusySeatsOfMovieInDifferentHalls.Add(new List<int>());
                SessionsTabViewModel.Sessions.Add(session);
                App.EditMovieWindowViewModel.SeatsTabViewModel.HallsMovieExists.Add(detailedHall);
                //App.EditMovieWindowViewModel.SeatsTabViewModel.PlacesMovieExists.Add($"{detailedHall.Cinema}, {detailedHall.Hallname}");
                App.SeatsTabViewModel.PlacesMovieExists.Add($"{detailedHall.Cinema}, {detailedHall.Hallname}");
                App.SeatsTabViewModel.RefreshPlacesComboBox();
                App.SeatsTabViewModel.HallChangedCommand.Execute(App.SeatsTabViewModel.SelectedIndex);
                //App.SeatsTabViewModel.SelectedItem = App.SeatsTabViewModel.PlacesMovieExists[0];
                //App.EditMovieWindowViewModel.SeatsTabViewModel.UpdateBusySeatsOfMovieInDifferentHalls();
                if (App.EditMovieWindowViewModel.SeatsTabViewModel.PlacesMovieExists.Count == 1)
                {
                    App.EditMovieWindowViewModel.SeatsTabViewModel.SelectedIndex = 0;
                }
                App.HasChanges = true;
                notifier.ShowSuccess("Movie session was successfully added! Save Changes!");
                BackCommand.Execute(null);
            });
        }

        private static readonly Regex OnlyNumberRegex = new Regex("[0-9]+");
        public void IsAllowedInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return OnlyNumberRegex.IsMatch(text);
        }
    }
}
