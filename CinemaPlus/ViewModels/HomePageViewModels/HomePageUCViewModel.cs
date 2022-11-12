using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.ViewModels.TabsViewModels;
using CinemaPlus.Views.UserControls;
using CinemaPlus.Views.UserControls.HeadOfHome;
using CinemaPlus.Views.UserControls.HomePage;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CinemaPlus.ViewModels.HomePageViewModels
{
    public class HomePageUCViewModel : BaseViewModel
    {
        public RelayCommand TodayCommand { get; set; }
        public RelayCommand ScheduleCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }

        // Tabs
        public TodayUC TodayView { get; set; }
        public TodayUCViewModel TodayViewModel { get; set; }
        public ScheduleUC ScheduleView { get; set; }
        public ScheduleUCViewModel ScheduleViewModel { get; set; }
        public SearchUC SearchView { get; set; }
        public SearchUCViewModel SearchViewModel { get; set; }

        private bool todayIsChecked = true;

        public bool TodayIsChecked
        {
            get { return todayIsChecked ; }
            set { todayIsChecked = value; OnPropertyChanged(); }
        }

        private bool scheduleIsChecked = false;

        public bool ScheduleIsChecked
        {
            get { return scheduleIsChecked; }
            set { scheduleIsChecked = value; OnPropertyChanged(); }
        }

        private bool searchIsChecked = false;

        public bool SearchIsChecked
        {
            get { return searchIsChecked; }
            set { searchIsChecked = value; OnPropertyChanged(); }
        }

        public HomePageUCViewModel()
        {
            // TodayView
            TodayView = new TodayUC();
            TodayViewModel = new TodayUCViewModel(TodayView.MoviesWrapPanel);
            TodayView.DataContext = TodayViewModel;
            TodayViewModel.MoviesWrapPanel = TodayView.MoviesWrapPanel;
            TodayViewModel.TodayUCScroll = TodayView.TodayUCScroll;
            TodayViewModel.EnglishTB = TodayView.EnglishTB;

            // ScheduleView
            ScheduleView = new ScheduleUC();
            ScheduleViewModel = new ScheduleUCViewModel(ScheduleView.MoviesSchedulesWrapPanel, ScheduleView.ScheduleUCScroll);
            ScheduleView.DataContext = ScheduleViewModel;
            ScheduleViewModel.TodayRB = ScheduleView.TodayRB;
            ScheduleViewModel.TomorrowRB = ScheduleView.TomorrowRB;
            ScheduleViewModel.DatesComboBox = ScheduleView.datesCBox;
            ScheduleViewModel.FilterAllSchedules();
            //App.HomePageViewModel.TodayView.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.WeAreBackView));
            //App.HomePageViewModel.TodayView.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));

            // SearchView
            SearchView = new SearchUC();
            SearchViewModel = new SearchUCViewModel();
            SearchView.DataContext = SearchViewModel;
            SearchViewModel.SearchTb = SearchView.SearchTB;
            SearchViewModel.SearchUCScrollViewer = SearchView.SearchUCScroll;
            SearchViewModel.SearchedMoviesWrapPanel = SearchView.SearchedMoviesWrapPanel;
            SearchViewModel.AddRandomMoviesToSearch();

            TodayCommand = new RelayCommand((e) =>
            {
                TodayIsChecked = true;
                ScheduleIsChecked = false;
                SearchIsChecked = false;

                App.HomePage.Children.RemoveAt(0);
                App.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.WeAreBackView));
                App.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                App.HomePage.Children.Add(TodayView);
                TodayView.TodayUCScroll.ScrollToTop();
            });

            ScheduleCommand = new RelayCommand((e) =>
            {
                TodayIsChecked = false;
                ScheduleIsChecked = true;
                SearchIsChecked = false;

                App.HomePage.Children.RemoveAt(0);
                //ScheduleViewModel.FilterAllSchedules();
                ScheduleView.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.WeAreBackView));
                ScheduleView.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                //App.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.WeAreBackView));
                //App.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                App.HomePage.Children.Add(ScheduleView);
                ScheduleView.ScheduleUCScroll.ScrollToTop();
            });

            SearchCommand = new RelayCommand((e) =>
            {
                TodayIsChecked = false;
                ScheduleIsChecked = false;
                SearchIsChecked = true;

                App.HomePage.Children.RemoveAt(0);
                App.HomePage.Children.Add(SearchView);
                SearchView.SearchUCScroll.ScrollToTop();
            });
        }
    }
}

