using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class EditMovieSeatsTabUCViewModel : BaseViewModel
    {
        public RelayCommand HallChangedCommand { get; set; }
        public RelayCommand SeatSelectedCommand { get; set; }
        public RelayCommand CheckAllCommand { get; set; }
        public RelayCommand UncheckAllCommand { get; set; }

        public WrapPanel SeatsWrapPanel { get; internal set; }

        public List<DetailedHall> HallsMovieExists { get; set; } = new List<DetailedHall>();

        //public ObservableCollection<string> PlacesMovieExists = new ObservableCollection<string>();

        public List<string> PlacesMovieExists { get; set; }

        //public ObservableCollection<string> PlacesMovieExists
        //{
        //    get { return placesMoviesExists; }
        //    set { placesMoviesExists =  value; }
        //}

        public List<List<int>> BusySeatsOfMovieInDifferentHalls { get; set; } = new List<List<int>>();

        private int selectedIndex = 0;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; OnPropertyChanged(); }
        }

        private string selectedItem;

        public string SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        public ComboBox PlacesCB { get; set; }

        public void RefreshPlacesComboBox()
        {
            PlacesCB.Items.Clear();
            foreach (var item in PlacesMovieExists)
            {
                PlacesCB.Items.Add(item);
            }
            if (HallsMovieExists.Count > 0)
            {
                var detailedHall = HallsMovieExists[0];
                SelectedItem = $"{detailedHall.Cinema}, {detailedHall.Hallname}, {detailedHall.Date}";
            }
            SelectedIndex = 0;
        }

        public EditMovieSeatsTabUCViewModel()
        {
            HallsMovieExists = Helper.GetHallsMovieExists(App.SelectedMovieForEdit.Title);
            PlacesMovieExists = new List<string>();
            foreach (var dH in HallsMovieExists)
            {
                PlacesMovieExists.Add($"{dH.Cinema}, {dH.Hallname}");
            }

            if (HallsMovieExists.Count > 0)
            {
                var detailedHall = HallsMovieExists[0];
                SelectedItem = $"{detailedHall.Cinema}, {detailedHall.Hallname}, {detailedHall.Date}";
            }
            App.BusySeatsOfMovieInDifferentHalls = BusySeatsOfMovieInDifferentHalls;

            HallChangedCommand = new RelayCommand((_selectedIndex) =>
            {
                int index = int.Parse(_selectedIndex.ToString());
                if (index != -1 && HallsMovieExists.Count > 0)
                {
                    var _detailedHall = HallsMovieExists[index];
                    SelectedItem = $"{_detailedHall.Cinema}, {_detailedHall.Hallname}, {_detailedHall.Date}";
                    SetBusySeats(BusySeatsOfMovieInDifferentHalls[index]);
                }
            });

            SeatSelectedCommand = new RelayCommand((toggleButton) =>
            {
                App.HasChanges = true;
                var seat = toggleButton as ToggleButton;
                seat.IsEnabled = true;
                int index = SeatsWrapPanel.Children.IndexOf(seat);
                if (seat.IsChecked is false)
                {
                    BusySeatsOfMovieInDifferentHalls[SelectedIndex].Remove(index);
                }
                else
                {
                    BusySeatsOfMovieInDifferentHalls[SelectedIndex].Add(index);
                }
            });

            CheckAllCommand = new RelayCommand((c) =>
            {
                App.HasChanges = true;
                OperationInAllSeats(true);
            });

            UncheckAllCommand = new RelayCommand((c) =>
            {
                App.HasChanges = true;
                OperationInAllSeats(false);
            });
        }


        public void OperationInAllSeats(bool checkAll)
        {
            int length = SeatsWrapPanel.Children.Count;
            BusySeatsOfMovieInDifferentHalls[SelectedIndex].Clear();
            for (int x = 0; x < length; x++)
            {
                var seat = SeatsWrapPanel.Children[x] as ToggleButton;
                seat.IsEnabled = true;
                seat.IsChecked = checkAll;
                if (checkAll && !BusySeatsOfMovieInDifferentHalls[SelectedIndex].Contains(x))
                    BusySeatsOfMovieInDifferentHalls[SelectedIndex].Add(x);
            }
        }

        public void UpdateBusySeatsOfMovieInDifferentHalls()
        {
            BusySeatsOfMovieInDifferentHalls.Clear();

            foreach (var cinema in App.Cinemas)
            {
                foreach (var hall in cinema.Halls)
                {
                    if (HallsMovieExists.Any((h) => h.Hallname == hall.HallName))
                    {
                        foreach (var _movie in hall.HallMovies)
                        {
                            if (_movie.Title == App.SelectedMovieForEdit.Title)
                            {
                                if (_movie.Session.Cinema == cinema.Name && _movie.Session.Hall == hall.HallName)
                                {
                                    var list = new List<int>();
                                    list.AddRange(_movie.BusySeats);
                                    BusySeatsOfMovieInDifferentHalls.Add(list);
                                    //SetBusySeats(list);
                                }
                            }
                        }
                    }
                }
            }
            if (BusySeatsOfMovieInDifferentHalls.Count != 0)
                SetBusySeats(BusySeatsOfMovieInDifferentHalls[SelectedIndex]);
        }

        public void SetBusySeats(List<int> seats)
        {
            int length = SeatsWrapPanel.Children.Count;
            for (int x = 0; x < length; x++)
            {
                var seat = SeatsWrapPanel.Children[x] as ToggleButton;
                seat.IsEnabled = true;
                if (seats.Contains(x))
                    seat.IsChecked = true;
                else
                    seat.IsChecked = false; // when we change the hall, all seats have to be unchecked

                if (App.AdminSideAddSide)
                    seat.IsEnabled = false;
            }
        }
    }
}
