using CinemaPlus.Commands;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.WindowsViewModel;
using CinemaPlus.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class MovieSessionUCViewModel : BaseViewModel
    {
        public RelayCommand EditSessionCommand { get; set; }

        private string cinema;

        public string Cinema
        {
            get { return cinema; }
            set { cinema = value; OnPropertyChanged(); }
        }

        private string hall;

        public string Hall
        {
            get { return hall; }
            set { hall = value; OnPropertyChanged(); }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }

        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; OnPropertyChanged(); }
        }

        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        public MovieSessionUCViewModel()
        {
            EditSessionCommand = new RelayCommand((e) =>
            {
                var editSessionWindow = new EditSessionWindow();
                var session = new Session()
                {
                     Cinema = Cinema,
                     Hall = Hall,
                     Date = Date,
                     Time = DateTime.Parse(Time).ToShortTimeString(),
                     Price = Price
                };
                var editSessionWindowViewModel = new EditSessionWindowViewModel(session);
                editSessionWindow.DataContext = editSessionWindowViewModel;
                editSessionWindow.Owner = App.ChildWindow;
                editSessionWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                App.ChildWindow2 = editSessionWindow;
                App.ChildWindowRectangle.Visibility = Visibility.Visible;
                editSessionWindow.ShowDialog(); 
            }); 
        }
    }
}
