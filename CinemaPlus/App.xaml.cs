using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels;
using CinemaPlus.ViewModels.AdminSideViewModels;
using CinemaPlus.ViewModels.HomePageViewModels;
using CinemaPlus.ViewModels.WindowsViewModel;
using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.Views.UserControls.EndOfPage;
using CinemaPlus.Views.UserControls.Home;
using CinemaPlus.Views.UserControls.MovieUC;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CinemaPlus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool AdminSideAddSide { get; internal set; }
        public static EditMovieSessionsTabUCViewModel SessionsTabViewModel { get; internal set; }
        public static ResourceDictionary ColorsDictionary { get; set; } = (ResourceDictionary)LoadComponent(new Uri("/CinemaPlus;component/Dictionaries/Colors.xaml", UriKind.Relative));
        public static ResourceDictionary ButtonStylesDictionary { get; set; } = (ResourceDictionary)LoadComponent(new Uri("/CinemaPlus;component/Dictionaries/ButtonStyles.xaml", UriKind.Relative));
        public static Window ChildWindow { get; set; }
        public static Window ChildWindow2 { get; set; }
        public static List<Cinema> Cinemas { get; set; } = new List<Cinema>();
        public static List<Movie> Movies { get; internal set; } = new List<Movie>(); /*= JsonSerialization<Movie>.Deserialize(@"~/../../../Files/Defaults\defaultMovies.json");*/
        public static List<List<int>> BusySeatsOfMovieInDifferentHalls { get; internal set; }
        public static List<Movie> DefaultMovies { get; internal set; } = JsonSerialization<Movie>.Deserialize(@"~/../../../Files/Defaults\defaultMovies.json");
        public static List<string> Languages { get; internal set; }
        public static WrapPanel PageWrapPanel { get; set; }
        public static WrapPanel MoviesWrapPanel { get; set; }
        public static List<Movie> MoviesInMoviesForEditWrapPanel { get; set; } = new List<Movie>();
        public static WrapPanel MoviesSchedulesWrapPanel { get; set; }
        public static WrapPanel OneMovieSchedulesWrapPanel { get; internal set; }
        public static StackPanel HomePage { get; set; }
        public static StackPanel MoviePage { get; internal set; }
        public static Grid LogoGrid { get; set; }
        public static Grid FirstTools { get; set; }
        public static Grid SecondTools { get; set; }
        public static WeAreBackUC WeAreBackView { get; internal set; }
        public static EndingUC EndingView { get; internal set; }
        public static Movie SelectedMovie { get; internal set; } = null;
        public static List<UIElement> PreviousPages { get; internal set; } = new List<UIElement>();
        public static AnotherMoviesUC AnotherMoviesView { get; internal set; }
        public static Rectangle Rectangle { get; internal set; }
        public static Rectangle ChildWindowRectangle { get; internal set; }
        public static List<Seat> SelectedSeats { get; internal set; }
        public static string SelectedHall { get; internal set; }
        public static string SelectedCinema { get; internal set; } = "All Cinemas";
        public static FirstToolsUCViewModel FirstToolsViewModel { get; internal set; }
        public static SecondToolsUCViewModel SecondToolsViewModel { get; internal set; }
        public static WebView2 Web { get; internal set; } = new WebView2();
        public static List<Admin> Admins { get; internal set; } = Helper.GetAdminsFromFile();
        public static Admin CurrentAdmin { get; set; }
        public static StackPanel AdminPage { get; internal set; }
        public static bool IsInAdminSide { get; set; } = false;
        public static Movie SelectedMovieForEdit { get; internal set; }
        public static bool HasChanges { get; set; } = false;
        public static StackPanel EditMoviePageStackPanel { get; internal set; }
        public static UIElement AdminSidePreviousPage { get; internal set; }
        public static EditMovieWindowViewModel EditMovieWindowViewModel { get; internal set; }
        public static HomePageUCViewModel HomePageViewModel { get; internal set; }
        public static List<Movie> AllMoviesFromAllHalls { get; internal set; }
        public static LogoUCViewModel LogoUCViewModel { get; internal set; }
        public static Image SessionTabImage { get; internal set; }
        public static Rectangle Headline { get; internal set; }
        public static Grid AdminTools { get; internal set; }
        public static AdminHomePageUC AdminHomePage { get; internal set; }
        public static EditMovieSeatsTabUCViewModel SeatsTabViewModel { get; internal set; }
        public static bool AdminSideEditSide { get; internal set; }
        public static StackPanel AddMoviePageStackPanel { get; internal set; }
        public static AddMovieWindowViewModel AddMovieViewModel { get; internal set; }
        public static string Flag { get; internal set; }
        public static Image EditMovieViewMainTabImage { get; internal set; }

        public static void SideChangedCommands()
        {
            if (IsInAdminSide)
            {
                App.LogoUCViewModel.LogoImage = @"\Images\cinemaKhanLogoDark.png";
                SolidColorBrush MyBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#383838");
                SolidColorBrush MyBrush2 = (SolidColorBrush)new BrushConverter().ConvertFrom("#808080"); // 
                Application.Current.Resources["secondColor"] = MyBrush;
                Application.Current.Resources["thirdColor"] = MyBrush2;

                App.FirstTools.Children.RemoveAt(0);
                App.SecondTools.Children.RemoveAt(0);

                var adminToolsView = new AdminToolsUC();
                var adminToolsViewModel = new AdminToolsUCViewModel();
                adminToolsView.DataContext = adminToolsViewModel;
                adminToolsViewModel.AdminName = App.CurrentAdmin.Username;

                App.AdminTools.Children.Add(adminToolsView);

                App.LogoUCViewModel.Cursor = Cursors.Arrow;
            }
            else
            {
                App.LogoUCViewModel.LogoImage = @"\Images\cinemaKhanLogo.png";
                SolidColorBrush MyBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#1D429A");
                SolidColorBrush MyBrush2 = (SolidColorBrush)new BrushConverter().ConvertFrom("#00ACEC");
                Application.Current.Resources["secondColor"] = MyBrush;
                Application.Current.Resources["thirdColor"] = MyBrush2;

                var firstToolsView = new FirstToolsUC();
                var firstToolsViewModel = new FirstToolsUCViewModel();
                App.FirstToolsViewModel = firstToolsViewModel;
                firstToolsView.DataContext = firstToolsViewModel;
                if (App.FirstTools.Children.Count > 0)
                    App.FirstTools.Children.RemoveAt(0);
                App.FirstTools.Children.Add(firstToolsView);

                var secondToolsView = new SecondToolsUC();
                var secondToolsViewModel = new SecondToolsUCViewModel();
                App.SecondToolsViewModel = secondToolsViewModel;
                secondToolsView.DataContext = secondToolsViewModel;
                if (App.SecondTools.Children.Count > 0)
                    App.SecondTools.Children.RemoveAt(0);
                App.SecondTools.Children.Add(secondToolsView);

                if (App.AdminTools.Children.Count > 0)
                    App.AdminTools.Children.RemoveAt(0);

                App.LogoUCViewModel.Cursor = Cursors.Hand;
            }
        }
    }
}
