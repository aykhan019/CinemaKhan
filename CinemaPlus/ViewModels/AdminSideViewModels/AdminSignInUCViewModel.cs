using CinemaPlus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ToastNotifications;
using System.Windows.Input;
using CinemaPlus.Views.Main;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using CinemaPlus.Models;
using System.Diagnostics;
using CinemaPlus.Views.UserControls.AdminSide;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class AdminSignInUCViewModel : BaseViewModel
    {
        public RelayCommand SignInCommand { get; set; }

        // //   
        public string UserName { get; set; } = string.Empty;
        // //
        public PasswordBox PasswordBox { get; internal set; }

        public TextBlock UsernameWarningTB { get; set; }
        public TextBlock PasswordWarningTB { get; set; }

        public AdminSignInUCViewModel()
        {
            SignInCommand = new RelayCommand((s) =>
            {
                bool _return = false;
                if (UserName.Trim() == String.Empty)
                {
                    UsernameWarningTB.Foreground = Brushes.Red;
                    _return = true;
                }
                else
                {
                    UsernameWarningTB.Foreground = Brushes.Transparent;
                }

                if (PasswordBox.Password.Trim() == String.Empty)
                {
                    PasswordWarningTB.Foreground = Brushes.Red;
                    _return = true;
                }
                else
                {
                    PasswordWarningTB.Foreground = Brushes.Transparent;
                }

                if (_return)
                    return;

                foreach (var admin in App.Admins)
                {
                    if (admin.Username == UserName)
                    {
                        if (admin.Password == PasswordBox.Password)
                        {
                            // welcome
                            App.IsInAdminSide = true;
                            var adminHomePageView = new AdminHomePageUC();
                            var adminHomePageViewModel = new AdminHomePageUCViewModel();
                            adminHomePageView.DataContext = adminHomePageViewModel;
                            App.CurrentAdmin = admin;
                            App.AdminPage = adminHomePageView.AdminPage;
                            App.AdminPage.Children.Add(adminHomePageViewModel.EditMovieTabView);
                            App.AdminHomePage = adminHomePageView;
                            App.PageWrapPanel.Children.RemoveAt(0);
                            App.PageWrapPanel.Children.Add(adminHomePageView);
                            App.SideChangedCommands();
                            return;
                        }
                    }
                }
                Notifier notifier = new Notifier(cfg =>
                {
                    cfg.PositionProvider = new WindowPositionProvider(
                        parentWindow: App.Current.MainWindow,
                        corner: Corner.TopLeft,
                        offsetX: 545,
                        offsetY: 305); // 506,284 - the corner

                    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                        notificationLifetime: TimeSpan.FromSeconds(3),
                        maximumNotificationCount: MaximumNotificationCount.FromCount(1));

                    cfg.Dispatcher = Application.Current.Dispatcher;
                });
                notifier.ShowError("Admin was not found!");
            });
        }
    }
}
