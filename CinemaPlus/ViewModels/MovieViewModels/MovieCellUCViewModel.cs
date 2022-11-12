using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.AdminSideViewModels;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.ViewModels.Movie;
using CinemaPlus.ViewModels.WindowsViewModel;
using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.Views.UserControls.EndOfPage;
using CinemaPlus.Views.UserControls.MovieUC;
using CinemaPlus.Views.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieCellUCViewModel : BaseViewModel
    {
        public List<MovieDetailUC> MovieFormats { get; set; }

        private Models.Movie movie;

        public Models.Movie Movie
        {
            get { return movie; }
            set { movie = value; OnPropertyChanged(); }
        }

        private ImageSource posterImageSource;

        public ImageSource PosterImageSource
        {
            get { return posterImageSource; }
            set { posterImageSource = value; OnPropertyChanged(); }
        }

        public RelayCommand MovieClickCommand { get; set; }
        public RelayCommand SessionsCommand { get; set; }
        public StackPanel MovieDetailsStackPanel { get; internal set; }

        public MovieCellUCViewModel()
        {
            MovieClickCommand = new RelayCommand((m) =>
            {
                if (App.IsInAdminSide)
                {
                    App.SelectedMovieForEdit = Movie;
                    if (App.AdminSideEditSide)
                        CreateEditMovieWindow().ShowDialog();
                    else
                        CreateAddMovieWindow().ShowDialog();
                }
                else
                {
                    MovieCellUCHelper.MovieClick(Movie, Helper.Enums.MovieTabs.DescriptionTab);
                }
            });

            SessionsCommand = new RelayCommand((s) =>
            {
                if (App.IsInAdminSide)
                {
                    App.SelectedMovieForEdit = Movie;
                    var window = CreateEditMovieWindow();
                    App.EditMovieWindowViewModel.SessionsTabCommand.Execute(null);
                    App.EditMovieWindowViewModel.SessionsTabCheckedCommand.Execute(App.SessionTabImage);
                    Helper.ChangeImageSource(@"\Images\mainTabImage.png", window.MainTabImage);
                    window.SessionsRB.IsChecked = true;
                    window.ShowDialog();
                }
                else
                {
                    MovieCellUCHelper.MovieClick(Movie, Helper.Enums.MovieTabs.SessionsTab);
                }
            });
        }

        public EditMovieWindow CreateEditMovieWindow()
        {
            if (App.SelectedCinema == "All Cinemas")
            {
                foreach (var cinema in App.Cinemas)
                {
                    if (SetSelectedHallAndMovie(cinema))
                        break;
                }
            }
            else
            {
                var cinema = App.Cinemas.Find((c) => c.Name == App.SelectedCinema);
                foreach (var hall in cinema.Halls)
                {
                    if (SetSelectedHallAndMovie(cinema))
                        break;
                }
            }
            var editMovieWindow = new EditMovieWindow();
            App.SessionTabImage = editMovieWindow.SessionsTabImage;
            var editMovieWindowViewModel = new EditMovieWindowViewModel();
            editMovieWindow.DataContext = editMovieWindowViewModel;
            editMovieWindowViewModel.SelectedMovie = App.SelectedMovieForEdit;

            editMovieWindowViewModel.MainTabViewModel.Movie = App.SelectedMovieForEdit;
            editMovieWindowViewModel.MainTabViewModel.UpdateProperties();

            editMovieWindowViewModel.PlotTabViewModel.Plot = App.SelectedMovieForEdit.Plot;

            editMovieWindowViewModel.PosterTabViewModel.ImageSource = Helper.StringToImageSource($@"{App.SelectedMovieForEdit.Poster}");

            editMovieWindowViewModel.SessionsTabViewModel.MovieSchedulesToEditWrapPanel = editMovieWindowViewModel.SessionsTabView.MovieSchedulesToEditWrapPanel;
            editMovieWindowViewModel.SessionsTabViewModel.UpdateSessions();

            App.EditMovieWindowViewModel = editMovieWindowViewModel;
            App.EditMoviePageStackPanel = editMovieWindow.PageStackPanel;
            App.EditMoviePageStackPanel.Children.Add(editMovieWindowViewModel.MainTabView);

            App.ChildWindow = editMovieWindow;
            editMovieWindow.Owner = App.Current.MainWindow;
            editMovieWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            App.Rectangle.Visibility = Visibility.Visible;
            App.HasChanges = false;
            App.ChildWindowRectangle = editMovieWindow.ChildWindowRectangle;
            return editMovieWindow;
        }

        public AddMovieWindow CreateAddMovieWindow()
        {
            if (App.SelectedCinema == "All Cinemas")
            {
                foreach (var cinema in App.Cinemas)
                {
                    if (SetSelectedHallAndMovie(cinema))
                        break;
                }
            }
            else
            {
                var cinema = App.Cinemas.Find((c) => c.Name == App.SelectedCinema);
                foreach (var hall in cinema.Halls)
                {
                    if (SetSelectedHallAndMovie(cinema))
                        break;
                }
            }
            var addMovieWindow = new AddMovieWindow();
            var addMovieWindowViewModel = new AddMovieWindowViewModel();
            addMovieWindow.DataContext = addMovieWindowViewModel;
            addMovieWindowViewModel.SelectedMovie = App.SelectedMovieForEdit;

            addMovieWindowViewModel.MainTabViewModel.Movie = App.SelectedMovieForEdit;
            addMovieWindowViewModel.MainTabViewModel.UpdateProperties();

            addMovieWindowViewModel.PlotTabViewModel.Plot = App.SelectedMovieForEdit.Plot;

            addMovieWindowViewModel.PosterTabViewModel.ImageSource = Helper.StringToImageSource($@"{App.SelectedMovieForEdit.Poster}");

            addMovieWindowViewModel.SessionsTabViewModel.MovieSchedulesToEditWrapPanel = addMovieWindowViewModel.SessionsTabView.MovieSchedulesToEditWrapPanel;
            addMovieWindowViewModel.SessionsTabViewModel.UpdateSessions();

            App.AddMovieViewModel = addMovieWindowViewModel;
            App.AddMoviePageStackPanel = addMovieWindow.PageStackPanel;
            App.AddMoviePageStackPanel.Children.Add(addMovieWindowViewModel.MainTabView);

            App.ChildWindow = addMovieWindow;
            addMovieWindow.Owner = App.Current.MainWindow;
            addMovieWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            App.Rectangle.Visibility = Visibility.Visible;
            App.HasChanges = false;
            App.ChildWindowRectangle = addMovieWindow.ChildWindowRectangle;
            return addMovieWindow;
        }

        // returns true if selected
        private bool SetSelectedHallAndMovie(Cinema cinema)
        {
            foreach (var hall in cinema.Halls)
            {
                foreach (var _movie in hall.HallMovies)
                {
                    if (_movie.Title == movie.Title)
                    {
                        App.SelectedHall = hall.HallName;
                        App.SelectedMovieForEdit = _movie;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}