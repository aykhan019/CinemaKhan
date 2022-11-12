using CinemaPlus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaPlus.ViewModels.WindowsViewModel
{
    public class SuccessfulPaymentWindowViewModel : BaseViewModel
    {
        public RelayCommand GoBackCommand { get; set; }

        public SuccessfulPaymentWindowViewModel()
        {
            GoBackCommand = new RelayCommand((g) => 
            {
                App.ChildWindow.Close();
                App.ChildWindow = null;
                App.Rectangle.Visibility = Visibility.Hidden;
            });
        }
    }
}
