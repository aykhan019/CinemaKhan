using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieTrailerUCViewModel : BaseViewModel        
    {
        public WebView2 Web { get; set; } = new WebView2();

        public void Navigate(string video)
        {
            Web.NavigateToString(video);
        }
    }
}
