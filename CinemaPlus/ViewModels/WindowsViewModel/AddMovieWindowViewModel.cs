using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.ViewModels.AdminSideViewModels;
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
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace CinemaPlus.ViewModels.WindowsViewModel
{
    public class AddMovieWindowViewModel : BaseViewModel
    {
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand AddMovieCommand { get; set; }

        public RelayCommand MainTabCheckedCommand { get; set; }
        public RelayCommand MainTabUncheckedCommand { get; set; }
        public RelayCommand PlotTabCheckedCommand { get; set; }
        public RelayCommand PlotTabUncheckedCommand { get; set; }
        public RelayCommand PosterTabCheckedCommand { get; set; }
        public RelayCommand PosterTabUncheckedCommand { get; set; }
        public RelayCommand SeatsTabCheckedCommand { get; set; }
        public RelayCommand SeatsTabUncheckedCommand { get; set; }
        public RelayCommand SessionsTabCheckedCommand { get; set; }
        public RelayCommand SessionsTabUncheckedCommand { get; set; }

        public RelayCommand MainTabCommand { get; set; }
        public RelayCommand PlotTabCommand { get; set; }
        public RelayCommand PosterTabCommand { get; set; }
        public RelayCommand SeatsTabCommand { get; set; }
        public RelayCommand SessionsTabCommand { get; set; }

        public Models.Movie SelectedMovie { get; set; }

        private string headline;

        public string Headline
        {
            get { return headline; }
            set { headline = value; OnPropertyChanged(); }
        }

        public EditMovieMainTabUC MainTabView { get; set; } = new EditMovieMainTabUC();
        public EditMovieMainTabUCViewModel MainTabViewModel { get; set; } = new EditMovieMainTabUCViewModel();

        public EditMoviePlotTabUC PlotTabView { get; set; } = new EditMoviePlotTabUC();
        public EditMoviePlotTabUCViewModel PlotTabViewModel { get; set; } = new EditMoviePlotTabUCViewModel();

        public EditMoviePosterTabUC PosterTabView { get; set; } = new EditMoviePosterTabUC();
        public EditMoviePosterTabUCViewModel PosterTabViewModel { get; set; } = new EditMoviePosterTabUCViewModel();

        public EditMovieSeatsTabUC SeatsTabView { get; set; } = new EditMovieSeatsTabUC();
        public EditMovieSeatsTabUCViewModel SeatsTabViewModel { get; set; } = new EditMovieSeatsTabUCViewModel();

        public EditMovieSessionsTabUC SessionsTabView { get; set; } = new EditMovieSessionsTabUC();
        public EditMovieSessionsTabUCViewModel SessionsTabViewModel { get; set; } = new EditMovieSessionsTabUCViewModel();

        public AddMovieWindowViewModel()
        {
            Headline = $"Add • {App.SelectedMovieForEdit.Title}";

            MainTabView.DataContext = MainTabViewModel;

            foreach (var item in MainTabView.Canvas.Children)
            {
                if (item is TextBox tb)
                {
                    tb.IsEnabled = false;
                }
                if (item is Button bt)
                {
                    bt.IsEnabled = false;
                }
                if (item is RadioButton rb)
                {
                    rb.IsEnabled = false;
                }
                if (item is ComboBox cbox)
                {
                    cbox.IsEnabled = false;
                }
            }

            //foreach (var item in MainTabView.canvas2.Children)
            //{
            //    if (item is Button bt)
            //    {
            //        bt.IsEnabled = false;
            //    }
            //    if (item is Image im)
            //    {
            //        im.Source = null;
            //    }
            //}

            //foreach (var item in MainTabView.canvas3.Children)
            //{
            //    if (item is Button bt)
            //    {
            //        bt.IsEnabled = false;
            //    }
            //    if (item is Image im)
            //    {
            //        im.Source = null;
            //    }
            //}

            //foreach (var item in MainTabView.canvas4.Children)
            //{
            //    if (item is Button bt)
            //    {
            //        bt.IsEnabled = false;
            //    }
            //    if (item is Image im)
            //    {
            //        im.Source = null;
            //    }
            //}
            MainTabView.image1.Source = null;
            MainTabView.image2.Source = null;
            MainTabView.image3.Source = null;
            MainTabView.image4.Source = null;
            MainTabView.image5.Source = null;
            MainTabView.image6.Source = null;

            //MainTabView.downButton1.Visibility = Visibility.Collapsed;

            MainTabView.downButton1.Background = Brushes.White;
            MainTabView.downButton2.Background = Brushes.White;
            MainTabView.downButton3.Background = Brushes.White;
            MainTabView.upButton1.Background = Brushes.White;
            MainTabView.upButton2.Background = Brushes.White;
            MainTabView.upButton3.Background = Brushes.White;
            MainTabView.downButton1.IsEnabled = false;
            MainTabView.downButton2.IsEnabled = false;
            MainTabView.downButton3.IsEnabled = false;
            MainTabView.upButton1.IsEnabled = false;
            MainTabView.upButton2.IsEnabled = false;
            MainTabView.upButton3.IsEnabled = false;
            MainTabView.border1.Background = Brushes.White;
            MainTabView.border1.BorderThickness = new Thickness(0, 1, 1, 1);
            MainTabView.border2.Background = Brushes.White;
            MainTabView.border2.BorderThickness = new Thickness(0, 1, 1, 1);
            MainTabView.border3.Background = Brushes.White;
            MainTabView.border3.BorderThickness = new Thickness(0, 1, 1, 1);
            MainTabView.rb1.IsEnabled = false;
            MainTabView.rb2.IsEnabled = false;

            PlotTabView.DataContext = PlotTabViewModel;
            PlotTabView.PlotTB.IsReadOnly = true;

            PosterTabView.DataContext = PosterTabViewModel;
            PosterTabView.Canvas.Children.Remove(PosterTabView.Image);
            PosterTabView.Canvas.Children.Remove(PosterTabView.ChooseFileButton);
            PosterTabView.Canvas.Children.Remove(PosterTabView.DragDropRectangle);
            PosterTabView.Canvas.Children.Remove(PosterTabView.DragTextBlock);
            var rectangle = (PosterTabView.Canvas.Children[0] as Rectangle);
            rectangle.Margin = new Thickness(270, -25, 0, 0);
            rectangle.Height += 78;
            rectangle.Width += 62;

            SeatsTabView.DataContext = SeatsTabViewModel;
            SeatsTabViewModel.SeatsWrapPanel = SeatsTabView.SeatsWrapPanel;
            SeatsTabViewModel.UpdateBusySeatsOfMovieInDifferentHalls();
            SeatsTabViewModel.PlacesCB = SeatsTabView.PlacesCB;
            SeatsTabViewModel.RefreshPlacesComboBox();
            App.SeatsTabViewModel = SeatsTabViewModel;
            foreach (var item in SeatsTabView.SeatsWrapPanel.Children)
            {
                if (item is ToggleButton tb)
                {
                    tb.IsEnabled = false;
                }
            }
            //SeatsTabView.canvas.Children.Remove(SeatsTabView.CheckAllButton);
            //SeatsTabView.canvas.Children.Remove(SeatsTabView.UnCheckAllButton);
            //SeatsTabView.CheckAllButton.Background = Brushes.Transparent;
            //SeatsTabView.CheckAllButton.Foreground = Brushes.Transparent;
            //SeatsTabView.CheckAllButton.IsEnabled = false;
            //SeatsTabView.UnCheckAllButton.Background = Brushes.Transparent;
            //SeatsTabView.UnCheckAllButton.Foreground = Brushes.Transparent;
            //SeatsTabView.UnCheckAllButton.IsEnabled = false;
            SeatsTabView.CheckAllButton.Visibility = Visibility.Collapsed;
            SeatsTabView.UnCheckAllButton.Visibility = Visibility.Collapsed;

            SessionsTabView.SessionsGrid.Children.Remove(SessionsTabView.AddSessionButton);
            SessionsTabView.DataContext = SessionsTabViewModel;
            foreach (var item in SessionsTabView.MovieSchedulesToEditWrapPanel.Children)
            {
                var sessionView = item as MovieSessionUC;
                sessionView.EditButton.Visibility = Visibility.Collapsed;
            }

            App.SessionsTabViewModel = SessionsTabViewModel;

            CloseCommand = new RelayCommand((c) =>
            {
                App.Rectangle.Visibility = Visibility.Hidden;
                App.ChildWindow.Close();
                App.ChildWindow = null;
            });

            #region RadioButtonCommands
            MainTabCheckedCommand = new RelayCommand((mainTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\mainTabImageWhite.png", mainTabImage as Image);
            });

            MainTabUncheckedCommand = new RelayCommand((mainTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\mainTabImage.png", mainTabImage as Image);
            });

            PlotTabCheckedCommand = new RelayCommand((plotTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\plotTabImageWhite.png", plotTabImage as Image);
            });

            PlotTabUncheckedCommand = new RelayCommand((plotTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\plotTabImage.png", plotTabImage as Image);
            });

            PosterTabCheckedCommand = new RelayCommand((posterTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\posterTabImageWhite.png", posterTabImage as Image);
            });

            PosterTabUncheckedCommand = new RelayCommand((posterTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\posterTabImage.png", posterTabImage as Image);
            });

            SeatsTabCheckedCommand = new RelayCommand((plotTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\seatsTabImageWhite.png", plotTabImage as Image);
            });

            SeatsTabUncheckedCommand = new RelayCommand((plotTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\seatsTabImage.png", plotTabImage as Image);
            });

            SessionsTabCheckedCommand = new RelayCommand((sessionsTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\sessionsTabImageWhite.png", sessionsTabImage as Image);
            });

            SessionsTabUncheckedCommand = new RelayCommand((sessionsTabImage) =>
            {
                Helper.ChangeImageSource(@"\Images\sessionsTabImage.png", sessionsTabImage as Image);
            });
            #endregion

            MainTabCommand = new RelayCommand((m) =>
            {
                App.AddMoviePageStackPanel.Children.RemoveAt(0);
                App.AddMoviePageStackPanel.Children.Add(MainTabView);
            });

            PlotTabCommand = new RelayCommand((p) =>
            {
                App.AddMoviePageStackPanel.Children.RemoveAt(0);
                App.AddMoviePageStackPanel.Children.Add(PlotTabView);
            });

            PosterTabCommand = new RelayCommand((p) =>
            {
                App.AddMoviePageStackPanel.Children.RemoveAt(0);
                App.AddMoviePageStackPanel.Children.Add(PosterTabView);
            });

            SeatsTabCommand = new RelayCommand((p) =>
            {
                App.AddMoviePageStackPanel.Children.RemoveAt(0);
                if (SessionsTabViewModel.Sessions.Count == 0)
                {
                    var noResultUC = new NoResultUC();
                    var noResultViewModel = new NoResultUCViewModel("There is no session yet . . . ");
                    noResultUC.DataContext = noResultViewModel;
                    noResultUC.Width = 984;
                    App.AddMoviePageStackPanel.Children.Add(noResultUC);
                    return;
                }
                App.AddMoviePageStackPanel.Children.Add(SeatsTabView);
            });

            SessionsTabCommand = new RelayCommand((s) =>
            {
                App.AddMoviePageStackPanel.Children.RemoveAt(0);
                App.AddMoviePageStackPanel.Children.Add(SessionsTabView);
            });


            AddMovieCommand = new RelayCommand((a) =>
            {
                Notifier notifier = new Notifier(cfg =>
                {
                    cfg.PositionProvider = new WindowPositionProvider(
                        parentWindow: App.ChildWindow,
                        corner: Corner.TopLeft,
                        offsetX: 272,
                        offsetY: 710);

                    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                        notificationLifetime: TimeSpan.FromSeconds(3),
                        maximumNotificationCount: MaximumNotificationCount.FromCount(1));

                    cfg.Dispatcher = Application.Current.Dispatcher;
                });
                if (SelectedMovie.Session != null)
                {
                    notifier.ShowWarning("This movie is already available for preview. If movie does not have a session, add a session for clients to see it!");
                    return;
                }

                foreach (var movie in App.DefaultMovies)
                {
                    if (movie.Title == SelectedMovie.Title)
                    {
                        notifier.ShowWarning("This movie is already available for preview. If movie does not have a session, add a session for clients to see it!");
                        return;
                    }
                }
                notifier = new Notifier(cfg =>
                {
                    cfg.PositionProvider = new WindowPositionProvider(
                        parentWindow: App.Current.MainWindow,
                        corner: Corner.TopLeft,
                        offsetX: 10,
                        offsetY: 10);

                    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                        notificationLifetime: TimeSpan.FromSeconds(3),
                        maximumNotificationCount: MaximumNotificationCount.FromCount(1));

                    cfg.Dispatcher = Application.Current.Dispatcher;
                });

                var defaultMovies = JsonSerialization<Models.Movie>.Deserialize(@"~/../../../Files/Defaults\defaultMovies.json");
                var session = SelectedMovie.Session;
                SelectedMovie.Session = null;
                defaultMovies.Add(SelectedMovie);
                App.DefaultMovies.Add(SelectedMovie);
                App.Movies.Add(SelectedMovie);
                JsonSerialization<Models.Movie>.Serialize(defaultMovies, @"~/../../../Files/Defaults\defaultMovies.json");
                SelectedMovie.Session = session;
                CloseCommand.Execute(null);
                ((App.PageWrapPanel.Children[0] as AdminHomePageUC).DataContext as AdminHomePageUCViewModel).EditMovieTabViewModel.FilterMovies();
                notifier.ShowSuccess("The movie was successfully added to the preview. Don't forget to add a session for clients to see it!");
            });
        }
    }
}
