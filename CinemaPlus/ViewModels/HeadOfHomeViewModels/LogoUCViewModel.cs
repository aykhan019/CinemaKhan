using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.ViewModels.HomePageViewModels;
using CinemaPlus.Views.UserControls.HomePage;
using System;
using System.Windows.Input;

namespace CinemaPlus.ViewModels
{
    public class LogoUCViewModel : BaseViewModel
    {
        public RelayCommand LogoCommand { get; set; }

        private string logoImage;

        public string LogoImage
        {
            get { return logoImage; }
            set { logoImage = value; OnPropertyChanged(); }
        }

        private Cursor cursor;

        public Cursor Cursor
        {
            get { return cursor; }
            set { cursor = value; OnPropertyChanged(); }
        }


        public LogoUCViewModel()
        {
            cursor = Cursors.Hand;
            LogoImage = @"\Images\cinemaKhanLogo.png";
            LogoCommand = new RelayCommand((w) =>
            {
                if (!App.IsInAdminSide)
                {
                    if (App.PageWrapPanel.Children.Count != 0)
                    {
                        App.PageWrapPanel.Children.RemoveAt(0);
                        App.PageWrapPanel.Children.Add(App.PreviousPages[0]);
                        App.PreviousPages.RemoveRange(1, App.PreviousPages.Count - 1);
                        App.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                        var homePageView = App.PageWrapPanel.Children[0] as HomePageUC;
                        var homePageViewModel = homePageView.DataContext as HomePageUCViewModel;
                        homePageViewModel.TodayView.TodayUCScroll.ScrollToTop();
                        if (App.HomePageViewModel.TodayIsChecked)
                        {
                            App.HomePageViewModel.TodayView.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.WeAreBackView));
                            App.HomePageViewModel.TodayView.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                        }
                        else if (App.HomePageViewModel.ScheduleIsChecked)
                        {
                            App.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.WeAreBackView));    
                            App.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                        }
                        App.Web.Source = new Uri($"https://www.youtube.com");
                    }
                    App.IsInAdminSide = false;
                    App.SideChangedCommands();
                }
            });
        }
    }
}
