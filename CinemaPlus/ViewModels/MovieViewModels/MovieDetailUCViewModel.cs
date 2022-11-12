using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using CinemaPlus.Views.UserControls.MovieUC;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieDetailUCViewModel : BaseViewModel
    {
        public RelayCommand IsMouseOverCommand { get; set; }
        public RelayCommand MouseLeaveCommand { get; set; }

        private ImageSource imageSource = null;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; OnPropertyChanged(); }
        }

        private string toolTipText = String.Empty;

        public string ToolTipText
        {
            get { return toolTipText; }
            set { toolTipText = value; OnPropertyChanged(); }
        }

        public PopupUC Header { get; internal set; }

        public MovieDetailUCViewModel()
        {
            //ImageSource = Helper.StringToImageSource($@"..\..\Images\noImage.jpg");

            IsMouseOverCommand = new RelayCommand((_popup) =>
            {
                var popup = _popup as Popup;
                popup.PlacementTarget = popup;
                popup.Placement = PlacementMode.Bottom;
                popup.IsOpen = true;
                (popup.Child as PopupUC).PopupText.Text = ToolTipText;
            });

            MouseLeaveCommand = new RelayCommand((_popup) =>
            {
                var popup = _popup as Popup;
                popup.Visibility = Visibility.Hidden;
                popup.IsOpen = false;
            });
        }
    }
}
