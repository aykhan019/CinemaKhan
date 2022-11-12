using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.ViewModels.EndingViewModels;
using CinemaPlus.ViewModels.MovieViewModels;
using CinemaPlus.Views.UserControls.EndOfPage;
using CinemaPlus.Views.UserControls.HeadOfHome;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CinemaPlus.Models;

namespace CinemaPlus.ViewModels.TabsViewModels
{
    public class ScheduleUCViewModel : BaseViewModel
    {
        private List<string> languages;

        public List<string> Languages
        {
            get { return languages; }
            set { languages = value; OnPropertyChanged(); }
        }

        private List<string> cinemas;

        public List<string> Cinemas
        {
            get { return cinemas; }
            set { cinemas = value; OnPropertyChanged(); }
        }

        private List<string> dates;

        public List<string> Dates
        {
            get { return dates; }
            set { dates = value; OnPropertyChanged(); }
        }

        public RadioButton TodayRB { get; set; }
        public RadioButton TomorrowRB { get; set; }
        public ComboBox DatesComboBox { get; set; }
        public ScrollViewer ScheduleUCScroll { get; set; }
        public ScheduleHeadUC ScheduleHeadView { get; set; }
        public TextBlock DateTextBlock { get; set; }

        public RelayCommand SelectedDateChangedCommand { get; set; }
        public RelayCommand SelectedLanguageChangedCommand { get; set; }
        public RelayCommand SelectedCinemaChangedCommand { get; set; }
        public RelayCommand TodayIsCheckedCommand { get; set; }
        public RelayCommand TomorrowIsCheckedCommand { get; set; }

        public DateTime SelectedDate { get; set; }
        public string SelectedCinema { get; set; } = "All Cinemas";
        public string SelectedLanguage { get; set; } = "All languages";

        public ScheduleUCViewModel(WrapPanel panel, ScrollViewer scroll)
        {
            App.MoviesSchedulesWrapPanel = panel;
            ScheduleUCScroll = scroll;
            DateTextBlock = new TextBlock()
            {
                FontSize = 20,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(500, 20, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = 265,
                FontWeight = FontWeights.Medium,
                Foreground = App.ColorsDictionary["seventhColor"] as Brush,
                Background = Brushes.Transparent
            };
            DateTextBlock.Text = $"Today ({DateTime.Today.ToShortDateString()})";
            DateTextBlock.Text = DateTextBlock.Text.Replace("/", ".");
            languages = App.Languages;
            cinemas = Helper.GetCinemaNames();
            dates = Helper.GetDates();
            ScheduleHeadView = new ScheduleHeadUC();
            //FilterAllSchedules();
            SelectedDateChangedCommand = new RelayCommand((selectedDate) =>
            {
                var sDate = selectedDate.ToString();
                DateTime dDate = DateTime.Parse(sDate, CultureInfo.InvariantCulture);
                if (dDate.Date == DateTime.Today.Date)
                {
                    DateTextBlock.Text = sDate;
                    TodayRB.IsChecked = true;
                    TodayIsCheckedCommand.Execute(TodayRB);
                }
                else if (dDate.Date == DateTime.Today.AddDays(1).Date)
                {
                    DateTextBlock.Text = sDate;
                    TomorrowRB.IsChecked = true;
                    TomorrowIsCheckedCommand.Execute(TomorrowRB);
                }
                else
                {
                    DateTextBlock.Text = sDate;
                    TomorrowRB.IsChecked = false;
                    TodayRB.IsChecked = false;
                }
                FilterAllSchedules();
            });

            TodayIsCheckedCommand = new RelayCommand((t) =>
            {
                DateTextBlock.Text = $"Today ({(DateTime.Today.ToShortDateString())})".Replace("/", ".");
                DatesComboBox.SelectedIndex = 0;
            });

            TomorrowIsCheckedCommand = new RelayCommand((t) =>
            {
                DateTextBlock.Text = $"Tomorrow ({(DateTime.Today.AddDays(1).ToShortDateString())})".Replace("/", ".");
                DatesComboBox.SelectedIndex = 1;
            });

            SelectedLanguageChangedCommand = new RelayCommand((selectedLanguage) =>
            {
                SelectedLanguage = selectedLanguage.ToString();
                FilterAllSchedules();
            });

            SelectedCinemaChangedCommand = new RelayCommand((selectedCinema) =>
            {
                SelectedCinema = selectedCinema.ToString();
                FilterAllSchedules();
            });
        }

        public void FilterAllSchedules()
        {
            App.MoviesSchedulesWrapPanel.Children.Clear();
            App.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(DateTextBlock));
            App.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(ScheduleHeadView));
            if (DateTextBlock.Text.Contains("("))
            {
                string dt = DateTextBlock.Text.Split('(')[1].Replace(")", " ").Trim();
                SelectedDate = DateTime.Parse(dt);
            }
            else
            {
                SelectedDate = DateTime.Parse(DateTextBlock.Text);
            }   
            bool hasResult = false;
            if (SelectedCinema == "All Cinemas")
            {
                bool hasResultSupport = false;
                foreach (var cinema in App.Cinemas)
                {
                    hasResultSupport = AddScheduleToSchedules(cinema);

                    if (hasResultSupport)
                    {
                        hasResult = true;
                    }
                }
            }
            else
            {
                var cinema = App.Cinemas.Find((e) => e.Name == SelectedCinema);
                hasResult = AddScheduleToSchedules(cinema);
            }
            if (!hasResult)
            {
                NoResultUC noResultUC = new NoResultUC();
                var noResultViewModel = new NoResultUCViewModel("No matching result found . . . ");
                noResultUC.DataContext = noResultViewModel;
                App.MoviesSchedulesWrapPanel.Children.Add(noResultUC);
            }
            //ScheduleUCScroll.ScrollToTop();
            App.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.WeAreBackView));
            App.MoviesSchedulesWrapPanel.Children.Add(Helper.RemoveElementFromItsParent(App.EndingView));
        }

        //Returns true if there is a movie subject to conditions
        public bool AddScheduleToSchedules(Cinema cinema)
        {
            bool hasResult = false;
            foreach (var hall in cinema.Halls)
            {
                foreach (var movie in hall.HallMovies)
                {
                    if ((movie.Language == SelectedLanguage || SelectedLanguage == "All languages") && (DateTime.Parse(movie.Session.Date).Date == SelectedDate.Date))
                    {
                        hasResult = true;
                        CreateSchedule(movie, hall.HallName, cinema.Name);
                    }
                }
            }
            return hasResult;
        }

        public static void CreateSchedule(Models.Movie movie, string hallname, string cinemaName)
        {
            var scheduleMovieView = new ScheduleMovieUC();
            var scheduleMovieViewModel = new ScheduleMovieUCViewModel();
            scheduleMovieView.DataContext = scheduleMovieViewModel;
            scheduleMovieViewModel.Formats = scheduleMovieView.DetailsStackPanel;
            scheduleMovieViewModel.Movie = movie;
            scheduleMovieViewModel.Hall = hallname;
            scheduleMovieViewModel.Cinema = cinemaName;
            scheduleMovieViewModel.SessionTime = DateTime.Parse(movie.Session.Time).ToShortTimeString().Trim();
            scheduleMovieViewModel.Price = movie.Price;
            if (!scheduleMovieViewModel.Price.Contains("₼"))
            {
                scheduleMovieViewModel.Price += " ₼";
            }
            if (movie.Formats.Count == 0)
            {
                movie.Formats = new List<MovieDetailUC>();
                MovieCellUCHelper.AddDetailsToMovie(movie);
            }
            for (int x = 0; x < movie.Formats.Count; x++)
            {
                var format = new MovieDetailUC { DataContext = movie.Formats[x].DataContext };
                if (x != 0) // 2d and subtitle
                {
                    if (x == 1)
                        format.Image.Source = Helper.StringToImageSource($@"..\..\Images\2d.png");
                    format.Image.Stretch = Stretch.UniformToFill;
                    format.Image.Height = movie.Formats[x].Image.Height;
                    format.Image.Width = movie.Formats[x].Image.Width;
                }
                scheduleMovieView.DetailsStackPanel.Children.Add(format);
            }
            App.MoviesSchedulesWrapPanel.Children.Add(scheduleMovieView);
        }
    }
}
