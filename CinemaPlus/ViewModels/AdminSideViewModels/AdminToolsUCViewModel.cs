using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.ViewModels.HomePageViewModels;
using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.Views.UserControls.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class AdminToolsUCViewModel : BaseViewModel
    {
        public RelayCommand BackToUserSideCommand { get; set; }
        public RelayCommand ViewPurchasedTicketsCommand { get; set; }
        public RelayCommand AdminHomeCommand { get; set; }

        private string adminName;

        public string AdminName
        {
            get { return adminName; }
            set { adminName = value; }
        }

        public PurchasedTicketsUC PurchasedTicketsView { get; set; } = new PurchasedTicketsUC();
        public PurchasedTicketsUCViewModel PurchasedTicketsViewModel { get; set; } = new PurchasedTicketsUCViewModel();

        public AdminToolsUCViewModel()
        {
            PurchasedTicketsView.DataContext = PurchasedTicketsViewModel;
            PurchasedTicketsViewModel.PurchasedTicketsWrapPanel = PurchasedTicketsView.PurchasedTicketsWrapPanel;
            PurchasedTicketsViewModel.CreateCellsForPurchasedTickets();

            BackToUserSideCommand = new RelayCommand((b) =>
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
                    App.Web.Source = new Uri($"https://www.youtube.com");
                    App.IsInAdminSide = false;
                    App.SideChangedCommands();
                }
            });

            ViewPurchasedTicketsCommand = new RelayCommand((v) =>
            {
                if (App.PageWrapPanel.Children.Count > 0)
                    App.PageWrapPanel.Children.RemoveAt(0);
                App.PageWrapPanel.Children.Add(PurchasedTicketsView);
            });

            AdminHomeCommand = new RelayCommand((a) =>
            {
                if (App.PageWrapPanel.Children.Count > 0)
                    App.PageWrapPanel.Children.RemoveAt(0);
                App.PageWrapPanel.Children.Add(App.AdminHomePage);
            });
        }
    }
}
