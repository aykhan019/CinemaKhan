using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.Views.UserControls.EndOfPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class EditMovieSessionsTabUCViewModel : BaseViewModel
    {
        public WrapPanel MovieSchedulesToEditWrapPanel { get; set; } = new WrapPanel();

        public AddMovieSessionUC AddMovieSessionView { get; set; }
        public AddMovieSessionUCViewModel AddMovieSessionViewModel { get; set; }

        public RelayCommand AddSessionCommand { get; set; }

        public List<Session> Sessions { get; set; } = new List<Session>();

        public EditMovieSessionsTabUCViewModel()
        {
            InitializeSessions();

            AddSessionCommand = new RelayCommand((a) =>
            {
                AddMovieSessionView = new AddMovieSessionUC();
                AddMovieSessionViewModel = new AddMovieSessionUCViewModel(this);
                AddMovieSessionView.DataContext = AddMovieSessionViewModel;
                AddMovieSessionViewModel.MovieSessionView = AddMovieSessionView.SessionUC;
                App.AdminSidePreviousPage = App.EditMoviePageStackPanel.Children[0];
                App.EditMoviePageStackPanel.Children.RemoveAt(0);
                App.EditMoviePageStackPanel.Children.Add(AddMovieSessionView);
            });
        }

        public void InitializeSessions()
        {
            Sessions.Clear();
            foreach (var cinema in App.Cinemas)
            {
                foreach (var hall in cinema.Halls)
                {
                    foreach (var _movie in hall.HallMovies)
                    {
                        if (_movie.Title == App.SelectedMovieForEdit.Title)
                        {
                            var session = new Session()
                            {
                                Cinema = cinema.Name,
                                Hall = hall.HallName,
                                Date = DateTime.Parse(_movie.Session.Date).ToShortDateString().Replace("/","."),
                                Time = (DateTime.Parse(_movie.Session.Time).ToShortTimeString().Replace(":00 ", " ").Trim()),
                                Price = _movie.Price + " ₼"
                            };
                            Sessions.Add(session);
                        }
                    }
                }
            }
        }

        public void UpdateSessions()
        {
            MovieSchedulesToEditWrapPanel.Children.Clear();

            if (Sessions.Count == 0)
            {
                var noResultUC = new NoResultUC();
                var noResultViewModel = new NoResultUCViewModel("There is no session yet . . . ");
                noResultUC.DataContext = noResultViewModel;
                noResultUC.Width = 984;
                MovieSchedulesToEditWrapPanel.Children.Add(noResultUC);
                return;
            }
            foreach (var session in Sessions)
            {
                var movieSessionView = new MovieSessionUC();
                var movieSessionViewModel = new MovieSessionUCViewModel();
                movieSessionView.DataContext = movieSessionViewModel;
                movieSessionViewModel.Cinema = session.Cinema;
                movieSessionViewModel.Hall = session.Hall;
                movieSessionViewModel.Date = DateTime.Parse(session.Date).ToShortDateString().Replace("/", ".");
                movieSessionViewModel.Time = (DateTime.Parse(session.Time).ToShortTimeString().Replace(":00 ", " ").Trim());
                movieSessionViewModel.Price = session.Price;
                MovieSchedulesToEditWrapPanel.Children.Add(movieSessionView);
                if (App.AdminSideAddSide)
                {
                    movieSessionView.EditButton.Visibility = Visibility.Collapsed;
                    movieSessionView.EditButton.IsEnabled = false;
                    movieSessionView.border.Visibility = Visibility.Collapsed;
                    //movieSessionView.Margin = new Thickness(100, 0, 0, 0);
                    movieSessionView.Width += 200;
                }
            }
        }
    }
}
