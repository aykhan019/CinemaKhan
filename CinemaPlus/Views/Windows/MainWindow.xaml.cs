using CinemaPlus.ViewModels.WindowsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaPlus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Rectangle = Rectangle;
            App.LogoGrid = LogoGrid;
            App.FirstTools = FirstTools;
            App.SecondTools = SecondTools;
            App.PageWrapPanel = PageWrapPanel;
            App.AdminTools = AdminTools;
            this.DataContext = new MainWindowViewModel();
        }
    }
}
    