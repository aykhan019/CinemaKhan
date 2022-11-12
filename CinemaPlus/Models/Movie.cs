using CinemaPlus.Helpers;
using CinemaPlus.Views.UserControls.MovieUC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CinemaPlus.Models
{
    public class Movie
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Year { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string ImdbRating { get; set; }
        public string Price { get; set; }   
        [JsonConverter(typeof(StringEnumConverter))]
        public Helper.Enums.Subtitles Subtitle { get; set; }
        public Session Session { get; set; }
        [JsonIgnore]
        public List<MovieDetailUC> Formats { get; set; } = new List<MovieDetailUC>();
        public List<int> BusySeats { get; set; } = new List<int>();

        // Price is set according to IMDb rating - increasing IMDb rating 10%
        // and casting it to int gives us the default price
        public void SetPrice()
        {
            if (ImdbRating != null)
            {
                if (ImdbRating != "N/A") // Not Assesses
                    Price = ((int)((double.Parse(ImdbRating) * (1.1)))).ToString() + ".00";
                else
                    Price = (6.00).ToString();

                // .....
                if (!Price.Contains(".00"))
                {
                    Price += ".00";
                }
            }
        }
    }
}
