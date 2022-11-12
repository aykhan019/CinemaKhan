using CinemaPlus.Commands;
    using CinemaPlus.Views.UserControls.AdminSide;
using CinemaPlus.ViewModels.AdminSideViewModels;
using CinemaPlus.Helpers;

namespace CinemaPlus.ViewModels.EndingViewModels
{
    public class EndingUCViewModel : BaseViewModel
    {
        public RelayCommand SiteCommand { get; set; }
        public RelayCommand FacebookCommand { get; set; }
        public RelayCommand TelegramCommand { get; set; }
        public RelayCommand YoutubeCommand { get; set; }
        public RelayCommand InstagramCommand { get; set; }
        public RelayCommand AppStoreCommand { get; set; }
        public RelayCommand GooglePlayCommand { get; set; }
        public RelayCommand VipClubCommand { get; set; }
        public RelayCommand AboutUsCommand { get; set; }
        public RelayCommand ServicesCommand { get; set; }
        public RelayCommand RulesCommand { get; set; }
        public RelayCommand FaqCommand { get; set; }
        public RelayCommand TariffsCommand { get; set; }
        public RelayCommand CineBonusCommand { get; set; }
        public RelayCommand AdminSideCommand { get; set; }

        public AdminSignInUC AdminSignInView { get; set; } = new AdminSignInUC();
        public AdminSignInUCViewModel AdminSignInViewModel { get; set; } = new AdminSignInUCViewModel();

        public EndingUCViewModel()
        {
            AdminSignInViewModel.UsernameWarningTB = AdminSignInView.UsernameWarningTB;
            AdminSignInViewModel.PasswordWarningTB = AdminSignInView.PasswordWarningTB;
            AdminSignInViewModel.PasswordBox = AdminSignInView.PasswordBox;

            AboutUsCommand = new RelayCommand((a) =>
            {
                App.FirstToolsViewModel.AboutUsCommand.Execute(null);
            });

            ServicesCommand = new RelayCommand((s) =>
            {
                App.FirstToolsViewModel.ServicesCommand.Execute(null);
            });

            RulesCommand = new RelayCommand((r) =>
            {
                App.FirstToolsViewModel.RulesCommand.Execute(null);
            });

            FaqCommand = new RelayCommand((f) =>
            {
                App.SecondToolsViewModel.FaqCommand.Execute(null);
            });

            TariffsCommand = new RelayCommand((f) =>
            {
                App.SecondToolsViewModel.TariffsCommand.Execute(null);
            });

            CineBonusCommand = new RelayCommand((f) =>
            {
                App.SecondToolsViewModel.CineBonusCommand.Execute(null);
            });

            AdminSideCommand = new RelayCommand((a) => 
            {
                AdminSignInView.DataContext = AdminSignInViewModel;
                App.PageWrapPanel.Children.Clear();
                App.PageWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(AdminSignInView));
            });

            SiteCommand = new RelayCommand((s) =>
            {
                var destinationurl = "https://www.cinemaplus.az/en/";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            });

            FacebookCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://www.facebook.com/CinemaPlusAz/";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            });

            TelegramCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://t.me/CinemaPlusAz";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            });

            YoutubeCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://www.youtube.com/user/The28Cinema";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            });

            InstagramCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://www.instagram.com/cinemaplusaz/";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            });

            AppStoreCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://apps.apple.com/us/app/cinemaplus/id1072140418";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            });

            GooglePlayCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://play.google.com/store/apps/details?id=com.promote.cinemaplus";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            });

            VipClubCommand = new RelayCommand((f) =>
            {
                var destinationurl = "https://vipclubazerbaijan.com/";
                var sInfo = new System.Diagnostics.ProcessStartInfo(destinationurl)
                {
                    UseShellExecute = true,
                };
                System.Diagnostics.Process.Start(sInfo);
            });
        }
    }
}
