using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieTicketUCViewModel : BaseViewModel
    {
        public Models.Movie Movie { get; set; }

        private string cinema;

        public string Cinema
        {
            get { return cinema; }
            set { cinema = value; OnPropertyChanged();}
        }

        private string hall;

        public string Hall
        {
            get { return hall; }
            set { hall = value; OnPropertyChanged(); }
        }

        private string seatNo;

        public string SeatNo
        {
            get { return seatNo; }
            set { seatNo = value; OnPropertyChanged(); }
        }

        private string rowNo;

        public string RowNo
        {
            get { return rowNo; }
            set { rowNo = value; OnPropertyChanged(); }
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

        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; OnPropertyChanged(); }
        }

        public MovieTicketUCViewModel()
        {

        }
    }
}
