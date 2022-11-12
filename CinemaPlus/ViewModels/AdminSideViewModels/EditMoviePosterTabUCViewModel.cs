using CinemaPlus.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class EditMoviePosterTabUCViewModel : BaseViewModel
    {
        private ImageSource imageSource;

        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; OnPropertyChanged(); }
        }

        public RelayCommand ChooseFileCommand { get; set; }

        public EditMoviePosterTabUCViewModel()
        {
            ChooseFileCommand = new RelayCommand((c) => 
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    ImageSource = new BitmapImage(fileUri);
                    App.HasChanges = true;
                }
            });
        }

        public void DropEvent(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) != null)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                BitmapImage myBitmapImage = new BitmapImage(new Uri($@"{files[0]}", UriKind.Relative));
                myBitmapImage.CacheOption = BitmapCacheOption.Default;
                ImageSource = myBitmapImage;
                App.HasChanges = true;
            }
        }
    }
}
