using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.ViewModels.EndingViewModels
{
    public class NoResultUCViewModel :BaseViewModel
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged();}
        }

        public NoResultUCViewModel(string _text)
        {
            Text = _text;
        }
    }
}
