using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CinemaPlus.ViewModels.MovieViewModels
{
    public class MovieDescriptionUCViewModel : BaseViewModel
    {
        private Models.Movie movie;

        public Models.Movie Movie
        {
            get { return movie; }
            set { movie = value; }
        }

        private ImageSource posterImageSource;

        public ImageSource PosterImageSource
        {
            get { return posterImageSource; }
            set { posterImageSource = value; OnPropertyChanged(); }
        }


        public string Director { get; set; } = "Director";
        public string Writer { get; set; } = "Writer";
        public string Genre { get; set; } = "Genre";
        public string Country { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Writers { get; set; }
        public string Genres { get; set; }

        public MovieDescriptionUCViewModel()
        {
            movie = App.SelectedMovie;
            Actors = movie.Actors;
            if (movie.Actors.Length > 40)
            {
                var data = movie.Actors.Split(',');
                Actors = data[0] + ", " + data[1];
            }

            Plot = Movie.Plot;
            if (Movie.Plot.Length > 1025)
            {
                var data = Movie.Plot.Split('.');
                int count = 1;
                string text;
                do
                {
                    text = string.Empty;
                    for (int x = 0; x < data.Length - count; x++)
                    {
                        text += data[x] + ".";
                    }
                    if (text.Length < 1025)
                        break;
                    count++;

                } while (true);
                Plot = text;
            }

            Genres = Movie.Genre;
            if (Genres.Length > 40)
            {
                var data = Movie.Genre.Split(',');
                int count = 1;
                string text;
                do
                {
                    text = string.Empty;
                    for (int x = 0; x < data.Length - count; x++)
                    {
                        text += data[x] + ",";
                    }
                    if (text.Length < 40)
                        break;
                    count++;

                } while (true);
                Genres = text.Remove(text.Length - 1, 1);
            }

            Writers = movie.Writer;
            if (movie.Writer.Length > 40)
            {
                var data = movie.Actors.Split(',');
                Writers = data[0] + ", " + data[1];
            }

            if (movie.Director.Contains(","))
            {
                Director = "Directors";
            }
            if (movie.Writer.Contains(","))
            {
                Writer = "Writers";
            }
            if (movie.Genre.Contains(","))
            {
                Genre = "Genres";
            }
            Country = movie.Country.Split(',').ElementAt(0).Trim();
        }
    }
}
