using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.ViewModels.HomePageViewModels;
using CinemaPlus.Views.UserControls.HomePage;
using System;

namespace CinemaPlus.ViewModels
{
    public class SecondToolsUCViewModel : BaseViewModel
    {
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand CampaignCommand { get; set; }
        public RelayCommand TariffsCommand { get; set; }
        public RelayCommand CineBonusCommand { get; set; }
        public RelayCommand FaqCommand { get; set; }

        public TariffsUC TariffsView { get; set; } = new TariffsUC();
        public CineBonusUC CineBonusView { get; set; } = new CineBonusUC();
        public FaqUC FaqView { get; set; } = new FaqUC();

        public SecondToolsUCViewModel()
        {
            TariffsView.TariffsEndingView.DataContext = App.EndingView.DataContext;
            CineBonusView.CineBonusEndingView.DataContext = App.EndingView.DataContext;

            HomeCommand = new RelayCommand((h) => 
            {
                if (App.PreviousPages.Count != 0)
                {
                    App.PageWrapPanel.Children.RemoveAt(0);
                    App.PageWrapPanel.Children.Add(App.PreviousPages[0]);
                    App.PreviousPages.RemoveRange(1, App.PreviousPages.Count - 1);
                    App.MoviesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
                    var homePageView = App.PageWrapPanel.Children[0] as HomePageUC;
                    var homePageViewModel = homePageView.DataContext as HomePageUCViewModel;
                    homePageViewModel.TodayView.TodayUCScroll.ScrollToTop();
                    //homePageViewModel.TodayViewModel.FilterMovies();
                    homePageViewModel.TodayCommand.Execute(null);
                    App.Web.Source = new Uri($"https://www.youtube.com");
                }
                App.IsInAdminSide = false;
                App.SideChangedCommands();
            });

            TariffsCommand = new RelayCommand((t) => 
            {
                App.PageWrapPanel.Children.Clear();
                App.PageWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(TariffsView));
                TariffsView.TariffsScroll.ScrollToTop();
                App.Web.Source = new Uri($"https://www.youtube.com");
                App.IsInAdminSide = false;
                App.SideChangedCommands();
            });

            FaqCommand = new RelayCommand((f) =>
            {
                FaqView.FaqEndingView.DataContext = App.EndingView.DataContext;
                App.PageWrapPanel.Children.Clear();
                App.PageWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(FaqView));
                FaqView.FaqScroll.ScrollToTop();
                App.Web.Source = new Uri($"https://www.youtube.com");
                App.IsInAdminSide = false;
                App.SideChangedCommands();
            });

            CineBonusCommand = new RelayCommand((c) => 
            {
                App.PageWrapPanel.Children.Clear();
                App.PageWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(CineBonusView));
                App.Web.Source = new Uri($"https://www.youtube.com");
                App.IsInAdminSide = false;
                //App.SideChangedCommands();
            });
        }
    }
}
