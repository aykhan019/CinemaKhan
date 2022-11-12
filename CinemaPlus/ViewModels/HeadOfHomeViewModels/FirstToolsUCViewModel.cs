using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Views.UserControls.HomePage;
using System;

namespace CinemaPlus.ViewModels
{
    public class FirstToolsUCViewModel : BaseViewModel
    {
        public RelayCommand AppStoreCommand { get; set; }
        public RelayCommand GooglePlayCommand { get; set; }
        public RelayCommand InRussianCommand { get; set; }
        public RelayCommand InAzerbaijaniCommand { get; set; }
        public RelayCommand InEnglishCommand { get; set; }
        public RelayCommand PlatiniumCommand { get; set; }
        public RelayCommand RulesCommand { get; set; }
        public RelayCommand ServicesCommand { get; set; }
        public RelayCommand AboutUsCommand { get; set; }

        public PlatiniumUC PlatiniumView { get; set; } = new PlatiniumUC();
        public ServicesUC ServicesView { get; set; } = new ServicesUC();
        public AboutUsUC AboutUSView { get; set; } = new AboutUsUC();
        public RulesUC RulesView{ get; set; } = new RulesUC();

        public FirstToolsUCViewModel()
        {
            PlatiniumView.PlatiniumEndingView.DataContext = App.EndingView.DataContext;
            ServicesView.ServicesEndingView.DataContext = App.EndingView.DataContext;
            AboutUSView.AboutUsEndingView.DataContext = App.EndingView.DataContext;

            PlatiniumCommand = new RelayCommand((p) => 
            {
                App.PageWrapPanel.Children.Clear();
                App.PageWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(PlatiniumView));
                App.IsInAdminSide = false;
                App.SideChangedCommands();
                App.Web.Source = new Uri($"https://www.youtube.com");
            });

            RulesCommand = new RelayCommand((r) =>
            {
                RulesView.RulesEndingView.DataContext = App.EndingView.DataContext;
                App.PageWrapPanel.Children.Clear();
                App.PageWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(RulesView));
                RulesView.RulesScroll.ScrollToTop();
                App.IsInAdminSide = false;
                App.SideChangedCommands();
                App.Web.Source = new Uri($"https://www.youtube.com");
            });

            ServicesCommand = new RelayCommand((s) =>
            {
                App.PageWrapPanel.Children.Clear();
                App.PageWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(ServicesView));
                ServicesView.ServiceScroll.ScrollToTop();
                App.IsInAdminSide = false;
                App.SideChangedCommands();
                App.Web.Source = new Uri($"https://www.youtube.com");
            });

            AboutUsCommand = new RelayCommand((a) => 
            {
                App.PageWrapPanel.Children.Clear();
                App.PageWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(AboutUSView));
                AboutUSView.AboutUsScroll.ScrollToTop();
                App.IsInAdminSide = false;
                App.SideChangedCommands();
                App.Web.Source = new Uri($"https://www.youtube.com");
            });

            AppStoreCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://apps.apple.com/us/app/cinemaplus/id1072140418";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
                App.Web.Source = new Uri($"https://www.youtube.com/results?search_query={App.SelectedMovie.Title.Replace(" ", "+")}+Trailer+{App.SelectedMovie.Year}");
            });

            GooglePlayCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://play.google.com/store/apps/details?id=com.promote.cinemaplus";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
                App.Web.Source = new Uri($"https://www.youtube.com/results?search_query={App.SelectedMovie.Title.Replace(" ", "+")}+Trailer+{App.SelectedMovie.Year}");

            });

            InAzerbaijaniCommand = new RelayCommand((a) => 
            {
                var destinationurl = "https://www.cinemaplus.az/az/";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
                App.Web.Source = new Uri($"https://www.youtube.com/results?search_query={App.SelectedMovie.Title.Replace(" ", "+")}+Trailer+{App.SelectedMovie.Year}");

            });

            InRussianCommand = new RelayCommand((a) =>
            {
                var destinationurl = "https://www.cinemaplus.az/ru/";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
                App.Web.Source = new Uri($"https://www.youtube.com/results?search_query={App.SelectedMovie.Title.Replace(" ", "+")}+Trailer+{App.SelectedMovie.Year}");
            });

            InEnglishCommand = new RelayCommand((a) =>
            {
                var destinationurl = "https://www.cinemaplus.az/en/";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
                App.Web.Source = new Uri($"https://www.youtube.com/results?search_query={App.SelectedMovie.Title.Replace(" ", "+")}+Trailer+{App.SelectedMovie.Year}");
            });
        }
    }
}
