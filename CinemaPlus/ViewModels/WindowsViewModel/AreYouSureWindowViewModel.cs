using CinemaPlus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaPlus.ViewModels.WindowsViewModel
{
    public class AreYouSureWindowViewModel : BaseViewModel
    {
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public AreYouSureWindowViewModel()
        {
            CloseCommand = new RelayCommand((c) => 
            {
                App.ChildWindow2.DialogResult = false;
                App.ChildWindow2.Close();
                App.ChildWindowRectangle.Visibility = Visibility.Hidden;
            });

            DeleteCommand = new RelayCommand((d) => 
            {
                App.ChildWindowRectangle.Visibility = Visibility.Hidden;
                App.ChildWindow2.DialogResult = true;
                App.ChildWindow2.Close();
            });
        }
    }
}
