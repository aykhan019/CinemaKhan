using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class EditMoviePlotTabUCViewModel : BaseViewModel
    {
        private string plot;

        public string Plot
        {
            get { return plot; }
            set { plot = value; OnPropertyChanged(); }
        }
    }
}
