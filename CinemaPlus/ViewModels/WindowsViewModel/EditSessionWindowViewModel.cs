using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.AdminSideViewModels;
using CinemaPlus.Views.UserControls.AdminSide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static CinemaPlus.ViewModels.AdminSideViewModels.EditMovieSeatsTabUCViewModel;

namespace CinemaPlus.ViewModels.WindowsViewModel
{
    public class EditSessionWindowViewModel : BaseViewModel
    {
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand DeleteSessionCommand { get; set; }

        public RelayCommand CinemaChangedCommand { get; set; }
        public RelayCommand HallChangedCommand { get; set; }
        public RelayCommand DateChangedCommand { get; set; }
        public RelayCommand TimeChangedCommand { get; set; }
        public RelayCommand PriceChangedCommand { get; set; }
        public RelayCommand EditSessionCommand { get; set; }

        public List<string> AllCinemas { get; set; } = new List<string>();
        public List<string> AllHalls { get; set; } = new List<string>();
        public List<string> AllDates { get; set; } = new List<string>();
        public List<string> AllTimes { get; set; } = new List<string>();

        private int cinemaSelectedIndex;

        public int CinemaSelectedIndex
        {
            get { return cinemaSelectedIndex; }
            set { cinemaSelectedIndex = value; OnPropertyChanged(); }
        }

        private int hallSelectedIndex;

        public int HallSelectedIndex
        {
            get { return hallSelectedIndex; }
            set { hallSelectedIndex = value; OnPropertyChanged(); }
        }

        private int dateSelectedIndex;

        public int DateSelectedIndex
        {
            get { return dateSelectedIndex; }
            set { dateSelectedIndex = value; OnPropertyChanged(); }
        }

        private int timeSelectedIndex;

        public int TimeSelectedIndex
        {
            get { return timeSelectedIndex; }
            set { timeSelectedIndex = value; OnPropertyChanged(); }
        }

        private string price = string.Empty;

        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); PriceChangedCommand?.Execute(null); }
        }

        public MovieSessionUC MovieSessionView { get; set; } = new MovieSessionUC();
        public MovieSessionUCViewModel MovieSessionViewModel { get; set; } = new MovieSessionUCViewModel();

        private string headline;

        public string Headline
        {
            get { return headline; }
            set { headline = value; OnPropertyChanged(); }
        }

        public int IndexOfSession { get; set; }
        public List<Session> Sessions { get; set; }

        public bool MadeChange { get; set; }

        EditMovieSessionsTabUCViewModel EditMovieSessionsTabViewModel { get; set; }

        public EditSessionWindowViewModel(Session session)
        {
            EditMovieWindow view = App.ChildWindow as EditMovieWindow;
            EditMovieWindowViewModel viewModel = view.DataContext as EditMovieWindowViewModel;
            EditMovieSessionsTabViewModel = viewModel.SessionsTabViewModel;
            Sessions = EditMovieSessionsTabViewModel.Sessions;

            for (int x = 0; x < Sessions.Count; x++)
            {
                var s = Sessions[x];
                if (s.Equals(session))
                {
                    IndexOfSession = x;
                    break;
                }
            }

            MovieSessionViewModel.Cinema = session.Cinema;
            MovieSessionViewModel.Hall = session.Hall;
            MovieSessionViewModel.Date = session.Date;
            MovieSessionViewModel.Time = DateTime.Parse(session.Time).ToShortTimeString().Replace("/", ".").Replace(":00 ", " ").Trim();
            //MovieSessionViewModel.Time = session.Time;
            MovieSessionViewModel.Price = session.Price;

            Price = MovieSessionViewModel.Price.Replace(".00 ₼", " ").Trim();
            foreach (var cinema in App.Cinemas)
            {
                AllCinemas.Add(cinema.Name);
                foreach (var hall in cinema.Halls)
                {
                    AllHalls.Add(hall.HallName);
                }
            }
            AllDates = Helper.GetDates();
            AllTimes = Helper.GetAllTimes();

            CinemaSelectedIndex = AllCinemas.IndexOf(MovieSessionViewModel.Cinema);
            HallSelectedIndex = AllHalls.IndexOf(MovieSessionViewModel.Hall);
            DateSelectedIndex = AllDates.IndexOf(MovieSessionViewModel.Date.Replace("/", "."));
            TimeSelectedIndex = AllTimes.IndexOf(MovieSessionViewModel.Time);

            DeleteSessionCommand = new RelayCommand((d) =>
            {
                string currentCinema = AllCinemas[CinemaSelectedIndex];
                string currentHall = AllHalls[HallSelectedIndex];
                string currentDate = DateTime.Parse(AllDates[DateSelectedIndex]).ToShortDateString().Replace("/", ".") +
                              ", " + DateTime.Parse(AllTimes[TimeSelectedIndex]).ToLongTimeString().Replace(":00 ", " ").Trim();

                int index = App.SeatsTabViewModel.HallsMovieExists.FindIndex((h) => h.Cinema == currentCinema && h.Hallname == currentHall && h.Date == currentDate);
                App.SeatsTabViewModel.HallsMovieExists.RemoveAt(index);
                //App.SeatsTabViewModel.PlacesMovieExists.RemoveAt(index);
                //App.SeatsTabViewModel.PlacesMovieExists.Remove(App.SeatsTabViewModel.PlacesMovieExists[index]);
                //App.SeatsTabViewModel.PlacesMovieExists.RemoveAt(index);
                //var list = App.SeatsTabViewModel.PlacesMovieExists.ToList();
                //list.RemoveAt(index);
                //App.SeatsTabViewModel.PlacesMovieExists.Clear();
                //App.SeatsTabViewModel.PlacesMovieExists.Clear();
                //App.SeatsTabViewModel.PlacesMovieExists = new ObservableCollection<string>(list);
                App.SeatsTabViewModel.PlacesMovieExists.RemoveAt(index);
                App.SeatsTabViewModel.RefreshPlacesComboBox();
                App.SeatsTabViewModel.BusySeatsOfMovieInDifferentHalls.RemoveAt(index);
                App.SeatsTabViewModel.HallChangedCommand.Execute(App.SeatsTabViewModel.SelectedIndex);
                //App.SeatsTabViewModel.SelectedItem = App.SeatsTabViewModel.PlacesMovieExists[0];
                //App.SeatsTabViewModel.PlacesMovieExists.Remove<string>((p) => App.SeatsTabViewModel.PlacesMovieExists.IndexOf(p) == index);
                //App.SeatsTabViewModel.PlacesMovieExists.Remove<string>((p) => App.SeatsTabViewModel.PlacesMovieExists.IndexOf(p) == index);
                int index2 = App.SessionsTabViewModel.Sessions.FindIndex((s) => s.Cinema == currentCinema && s.Hall == currentHall && s.Date == currentDate.Split(',').ElementAt(0).Trim() && s.Time == s.Time);
                App.SessionsTabViewModel.Sessions.RemoveAt(index2);

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
                notifier.ShowSuccess("Session was deleted. To save the changes click \"Save Changes\" button. ");
                App.HasChanges = true;
                App.ChildWindow2.Close();
                App.ChildWindowRectangle.Visibility = Visibility.Hidden;
                App.ChildWindow2 = null;
                //App.SessionsTabViewModel.InitializeSessions();
                App.SessionsTabViewModel.UpdateSessions();
                App.SeatsTabViewModel.SelectedIndex = 0;
            });

            CloseCommand = new RelayCommand((c) =>
            {
                App.ChildWindow2.Close();
                App.ChildWindow2 = null;
                App.ChildWindowRectangle.Visibility = Visibility.Hidden;
            });

            CinemaChangedCommand = new RelayCommand((selectedCinema) =>
            {
                MadeChange = true;
                App.HasChanges = true;
                MovieSessionViewModel.Cinema = selectedCinema.ToString();
            });

            HallChangedCommand = new RelayCommand((selectedHall) =>
            {
                MadeChange = true;
                App.HasChanges = true;
                MovieSessionViewModel.Hall = selectedHall.ToString();
            });

            DateChangedCommand = new RelayCommand((selectedDate) =>
            {
                MadeChange = true;
                App.HasChanges = true;
                MovieSessionViewModel.Date = selectedDate.ToString();
            });

            TimeChangedCommand = new RelayCommand((selectedTime) =>
            {
                MadeChange = true;
                App.HasChanges = true;
                MovieSessionViewModel.Time = selectedTime.ToString();
            });

            PriceChangedCommand = new RelayCommand((p) =>
            {
                MadeChange = true;
                if (Price.StartsWith("0"))
                {
                    do
                    {
                        Price = Price.Remove(0, 1);
                    } while (Price.StartsWith("0"));
                }

                MovieSessionViewModel.Price = Price.Trim() + ".00 ₼";
            });

            EditSessionCommand = new RelayCommand((e) =>
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
                var _session = new Session()
                {
                    Cinema = MovieSessionViewModel.Cinema,
                    Hall = MovieSessionViewModel.Hall,
                    Date = MovieSessionViewModel.Date,
                    Time = DateTime.Parse(MovieSessionViewModel.Time).ToShortTimeString(),
                    Price = MovieSessionViewModel.Price
                };

                EditMovieSessionsTabViewModel.Sessions[IndexOfSession] = _session;
                EditMovieSessionsTabViewModel.UpdateSessions();
                App.SeatsTabViewModel.RefreshPlacesComboBox();
                App.SeatsTabViewModel.RefreshPlacesComboBox();
                App.HasChanges = true;
                App.ChildWindow2.Close();
                if (MadeChange)
                    notifier.ShowSuccess("Movie session was succesfully edited!");
                else
                    notifier.ShowInformation("No changes were done");
                App.ChildWindowRectangle.Visibility = Visibility.Hidden;
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
