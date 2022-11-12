using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.Models;
using CinemaPlus.ViewModels.MovieViewModels;
using CinemaPlus.ViewModels.WindowsViewModel;
using CinemaPlus.Views.UserControls.MovieUC;
using CinemaPlus.Views.Windows;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CinemaPlus.ViewModels.Main
{
    public class OrderWindowViewModel : BaseViewModel
    {
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand PayCommand { get; set; }

        public TextBlock EmailWarning { get; set; }
        public TextBlock PhoneWarning { get; set; }
        public TextBlock DigitsOfCardWarning { get; set; }

        public string TextOfDigitsOfCardTBox { get; set; } = String.Empty;
        public string TextOfEmailTBox { get; set; } = String.Empty;
        public string TextOfPhoneTBox { get; set; } = String.Empty;

        private int phoneNumbersStartingsIndex;

        public int PhoneNumbersStartingsIndex
        {
            get { return phoneNumbersStartingsIndex; }
            set { phoneNumbersStartingsIndex = value; OnPropertyChanged(); }
        }

        private List<string> phoneNumberStartings = new List<string>();

        public List<string> PhoneNumberStartings
        {
            get { return phoneNumberStartings; }
            set { phoneNumberStartings = value; OnPropertyChanged(); }
        }

        private int selectedIndexOfPaymentCb;

        public int SelectedIndexOfPaymentCb
        {
            get { return selectedIndexOfPaymentCb; }
            set { selectedIndexOfPaymentCb = value; OnPropertyChanged(); }
        }

        private List<string> paymentList = new List<string>();

        public List<string> PaymentList
        {
            get { return paymentList; }
            set { paymentList = value; OnPropertyChanged(); }
        }

        private static readonly Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public OrderWindowViewModel()
        {
            SelectedIndexOfPaymentCb = 0;
            PhoneNumbersStartingsIndex = 0;

            PhoneNumberStartings.Add("10");
            PhoneNumberStartings.Add("50");
            PhoneNumberStartings.Add("55");
            PhoneNumberStartings.Add("70");
            PhoneNumberStartings.Add("77");
            PhoneNumberStartings.Add("99");

            PaymentList.Add("Via Bank Card");
            PaymentList.Add("Via CineBonus Card");

            CloseCommand = new RelayCommand((c) =>
            {
                App.Rectangle.Visibility = Visibility.Hidden;
                App.ChildWindow.Close();
                App.ChildWindow = null;
            });

            PayCommand = new RelayCommand((p) =>
            {
                bool successful = true;
                if (TextOfDigitsOfCardTBox.Replace(" ", "").Trim().Length < 16)
                {
                    DigitsOfCardWarning.Foreground = System.Windows.Media.Brushes.Red;
                    successful = false;
                }
                else
                {
                    DigitsOfCardWarning.Foreground = System.Windows.Media.Brushes.Transparent;
                }

                if (TextOfPhoneTBox.Replace(" ", "").Trim().Length < 7)
                {
                    PhoneWarning.Foreground = System.Windows.Media.Brushes.Red;
                    successful = false;
                }
                else
                {
                    PhoneWarning.Foreground = System.Windows.Media.Brushes.Transparent;
                }

                if (!EmailRegex.IsMatch(TextOfEmailTBox))
                {
                    EmailWarning.Foreground = System.Windows.Media.Brushes.Red;
                    successful = false;
                }
                else
                {
                    EmailWarning.Foreground = System.Windows.Media.Brushes.Transparent;
                }

                if (successful)
                {
                    var ticketList = new List<MovieTicketUC>();
                    var tickets = new List<Ticket>();
                    int count = App.SelectedSeats.Count;
                    var hall = App.Cinemas.Find((c) => c.Name == App.SelectedCinema).Halls.Find((h) => h.HallName == App.SelectedHall);
                    var movie = App.Cinemas.Find((c) => c.Name == App.SelectedCinema).Halls.Find((h) => h.HallName == App.SelectedHall).HallMovies.Find((m) => m.Title == App.SelectedMovie.Title);
                    var date = movie.Session;
                    for (int x = 0; x < count; x++)
                    {
                        var movieTicketView = new MovieTicketUC();
                        var movieTicketViewModel = new MovieTicketUCViewModel();
                        movieTicketView.DataContext = movieTicketViewModel;
                        movieTicketViewModel.Movie = App.SelectedMovie;
                        //movieTicketView.CinemaTB.Text = App.SelectedCinema;
                        //movieTicketView.HallTB.Text = App.SelectedHall;
                        //int seat = App.SelectedSeats[x].SeatNo;
                        //int row = App.SelectedSeats[x].RowNo;
                        //movieTicketView.SeatNo.Text = seat.ToString();
                        //movieTicketView.RowNo.Text = row.ToString();
                        //movieTicketView.DateTB.Text = date.Date;
                        //movieTicketView.TimeTB.Text = date.Time;
                        //movieTicketView.PriceTB.Text = "₼" + App.SelectedMovie.Price;
                        movieTicketViewModel.Cinema = App.SelectedCinema;
                        movieTicketViewModel.Hall = App.SelectedHall;
                        int seat = App.SelectedSeats[x].SeatNo;
                        int row = App.SelectedSeats[x].RowNo;
                        movieTicketViewModel.SeatNo = seat.ToString();
                        movieTicketViewModel.RowNo = row.ToString();
                        movieTicketViewModel.Date = date.Date;
                        movieTicketViewModel.Time = DateTime.Parse(date.Time).ToShortTimeString();
                        movieTicketViewModel.Price = "₼" + App.SelectedMovie.Price;
                        movieTicketViewModel.ImageSource = Helper.StringToImageSource(movie.Poster);
                        ticketList.Add(movieTicketView);
                        movie.BusySeats.Add((row - 1) * 13 + (seat - 1));

                        var ticket = new Ticket()
                        {
                            MovieTitle = movie.Title,
                            MoviePoster = movie.Poster,
                            CinemaName = App.SelectedCinema,
                            HallName = App.SelectedHall,
                            Time = DateTime.Parse(date.Time).ToShortTimeString(),
                            Date = date.Date,
                            Price = "₼" + App.SelectedMovie.Price,
                            SeatNo = seat.ToString(),
                            RowNo = row.ToString(),
                            PhoneNumber = $@"+994 {PhoneNumberStartings[PhoneNumbersStartingsIndex]} {TextOfPhoneTBox.Substring(0, 3)} {TextOfPhoneTBox.Substring(3, 2)} {TextOfPhoneTBox.Substring(5, 2)}",
                            CardNumber = TextOfDigitsOfCardTBox,
                            Email = TextOfEmailTBox,
                            Payment = PaymentList[SelectedIndexOfPaymentCb],
                            PurchaseDate = DateTime.Now.ToString()
                        };
                        tickets.Add(ticket);
                    }

                    var allPurchasedTickets = JsonSerialization<Ticket>.Deserialize(@"~/../../../Files\purchasedTickets.json");
                    allPurchasedTickets.AddRange(tickets);
                    JsonSerialization<Ticket>.Serialize(allPurchasedTickets, @"~/../../../Files\purchasedTickets.json");

                    //hall.HallMovies.Remove(hall.HallMovies.Find((m) => m.Title == movie.Title));
                    //hall.HallMovies.Add(movie);
                    string filename = @"~/../../../Files/Halls\" + App.SelectedCinema.Replace(" ", string.Empty) + "+" + App.SelectedHall.Replace(" ", string.Empty) + ".json";
                    JsonSerialization<Models.Movie>.Serialize(hall.HallMovies, filename);
                    hall.HallMovies = JsonSerialization<Models.Movie>.Deserialize(filename);

                    foreach (var mm in hall.HallMovies)
                    {
                        if (mm.Formats == null)
                        {
                            mm.Formats = new List<MovieDetailUC>();
                            MovieCellUCHelper.AddDetailsToMovie(mm);
                        }
                    }

                    string messageSubject = "You successfully bought your ticket";
                    string message = string.Empty; // write something
                    if (count > 1)
                        messageSubject += "s!";
                    else
                        messageSubject += "!";
                    App.ChildWindow.Close();
                    var successView = new SuccessfulPaymentWindow();
                    var successViewModel = new SuccessfulPaymentWindowViewModel();
                    successView.DataContext = successViewModel;
                    App.ChildWindow = successView;
                    successView.Owner = App.Current.MainWindow;
                    successView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    App.ChildWindow.ShowDialog();
                    EmailHelper.SendEmail(TextOfEmailTBox, messageSubject, message, ticketList);
                }
            });
        }

        private static readonly Regex OnlyNumberRegex = new Regex("[0-9]+");
        public void IsAllowedInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return OnlyNumberRegex.IsMatch(text);
        }
    }
}
