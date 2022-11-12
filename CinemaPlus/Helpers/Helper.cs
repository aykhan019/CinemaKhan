using CinemaPlus.Helpers.MovieCellUCHelpers;
using CinemaPlus.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Net;

namespace CinemaPlus.Helpers
{
    public class Helper
    {
        public class Enums
        {
            public enum MovieTabs
            {
                SessionsTab,
                DescriptionTab,
                TrailerTab
            };

            public enum Subtitles
            {
                Azerbaijani,
                Turkish
            }
        }

        ///
        /// Checks the file exists or not.
        ///
        /// The URL of the remote file.
        /// True : If the file exits, False if file not exists
        public static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        public static ImageSource StringToImageSource(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                if (source.Contains("/Images/"))
                {
                    if (!source.Contains(@"..\..\"))
                    {
                        source = @"..\..\" + source;
                    }
                }
            }
            else
            {
                source = $@"..\..\Images\noImage.jpg";
            }

            if (source == @"\Images\noImage.jpg")
            {
                source = @"..\..\Images\noImage.jpg";
            }

            //if (source == @"\Images\noImage.jpg")
            //{
            //    source = @"..\..\Images\noImage.jpg";
            //}


            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(source, UriKind.RelativeOrAbsolute);
            bi3.CacheOption = BitmapCacheOption.OnLoad;
            bi3.EndInit();
            return bi3;
        }

        public static void ChangeImageSource(string imagePath, Image image)
        {
            Uri imageUri = new Uri(imagePath, UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            image.Source = imageBitmap;
        }

        public static List<string> GetLanguagesFromDefaultMovies()
        {
            List<string> languages = new List<string>();
            languages.Add("All languages");
            foreach (var movie in App.Movies)
            {
                languages.Add(movie.Language.Split(',').ElementAt(0));
            }

            return languages.Distinct().ToList();
        }

        public static List<Movie> SortMoviesAsIWant(List<Movie> movies)
        {
            var desiredMovieSequence = new List<Movie>();
            List<int> indexes = new List<int>();

            int index = movies.FindIndex((m) => m.Id == "fedd6bbd-e655-400f-983a-d5e1bf24f6e4");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "8999287c-b3c0-4acf-b216-b94546906cbd");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "c8e3dd58-b6ff-4fb0-895c-889193031923");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "55713341-29ec-461d-9427-92b7b3f58d78");
            Foo(index, indexes, desiredMovieSequence, movies);

            index = movies.FindIndex((m) => m.Id == "9432b611-0194-40b0-8044-5788df10d20b");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "ff1d56dc-d9fd-45aa-8536-4ff05cf46bd1");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "76673269-f1d0-4f57-a3ca-248f01a80799");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "0f0b4706-899b-4248-836f-86727898a06a");
            Foo(index, indexes, desiredMovieSequence, movies);

            index = movies.FindIndex((m) => m.Id == "f1ae4eed-10c7-46dc-90d6-b8087ce3aed3");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "2dab65b8-dc6e-464f-9816-d43ee8f1a8bc");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "b4822912-7d36-4bb8-88b6-a4df84195e2e");
            Foo(index, indexes, desiredMovieSequence, movies);
            index = movies.FindIndex((m) => m.Id == "e01472f0-f4e5-4205-9e5c-966b9cc5c040");
            Foo(index, indexes, desiredMovieSequence, movies);

            for (int x = 0; x < movies.Count; x++)
            {
                if (!indexes.Contains(x))
                    desiredMovieSequence.Add(movies[x]);
            }
            return desiredMovieSequence;
        }

        private static void Foo(int index, List<int> indexes, List<Movie> desiredMovieSequence, List<Movie> movies)
        {
            if (index != -1)
            {
                desiredMovieSequence.Add(movies[index]);
                indexes.Add(index);
            }
        }

        readonly static Random random = new Random();
        #region Random Helpers
        private static readonly int RATIO_CHANCE_0 = 30;
        private static readonly int RATIO_CHANCE_1 = 25;
        private static readonly int RATIO_CHANCE_2 = 15;
        private static readonly int RATIO_CHANCE_3 = 10;
        private static readonly int RATIO_CHANCE_4 = 7;
        private static readonly int RATIO_CHANCE_5 = 7;
        private static readonly int RATIO_CHANCE_6 = 6;
        private static readonly int RATIO_TOTAL = RATIO_CHANCE_0
                                                + RATIO_CHANCE_1
                                                + RATIO_CHANCE_2
                                                + RATIO_CHANCE_3
                                                + RATIO_CHANCE_4
                                                + RATIO_CHANCE_5
                                                + RATIO_CHANCE_6;

        public static DateTime GetRandomDate() // within week
        {
            int dayRandom;
            int x = random.Next(0, RATIO_TOTAL);
            if ((x -= RATIO_CHANCE_0) < 0)
            {
                dayRandom = 0;
            }
            else if ((x -= RATIO_CHANCE_1) < 0)
            {
                dayRandom = 1;
            }
            else if ((x -= RATIO_CHANCE_2) < 0)
            {
                dayRandom = 2;
            }
            else if ((x -= RATIO_CHANCE_3) < 0)
            {
                dayRandom = 3;
            }
            else if ((x -= RATIO_CHANCE_4) < 0)
            {
                dayRandom = 4;
            }
            else if ((x -= RATIO_CHANCE_5) < 0)
            {
                dayRandom = 5;
            }
            else if ((x -= RATIO_CHANCE_6) < 0)
            {
                dayRandom = 6;
            }
            else
            {
                dayRandom = 0;
            }
            int hourRandom = random.Next(18, 24);
            DateTime date = DateTime.Today.AddDays(dayRandom);
            date = date.AddHours(hourRandom);
            int minuteRandom;
            do
            {
                minuteRandom = random.Next(0, 60);
            } while (minuteRandom % 5 != 0);
            date = date.AddMinutes(minuteRandom);
            return date;
        }

        public class RandomDateTime
        {
            public DateTime start;
            public Random gen;
            public int range;

            public RandomDateTime()
            {
                start = DateTime.Today;
                gen = new Random();
                range = 7;
            }

            public DateTime Next()
            {
                int minutes;

                do
                {
                    minutes = gen.Next(0, 60);
                } while (minutes % 5 != 0);

                int dayRandom;
                int x = random.Next(0, RATIO_TOTAL);
                if ((x -= RATIO_CHANCE_0) < 0)
                {
                    dayRandom = 0;
                }
                else if ((x -= RATIO_CHANCE_1) < 0)
                {
                    dayRandom = 1;
                }
                else if ((x -= RATIO_CHANCE_2) < 0)
                {
                    dayRandom = 2;
                }
                else if ((x -= RATIO_CHANCE_3) < 0)
                {
                    dayRandom = 3;
                }
                else if ((x -= RATIO_CHANCE_4) < 0)
                {
                    dayRandom = 4;
                }
                else if ((x -= RATIO_CHANCE_5) < 0)
                {
                    dayRandom = 5;
                }
                else if ((x -= RATIO_CHANCE_6) < 0)
                {
                    dayRandom = 6;
                }
                else
                {
                    dayRandom = 0;
                }

                return start.AddDays(dayRandom).AddHours(gen.Next(13, 24)).AddMinutes(minutes);
            }
        }

        public static bool Foo(DateTime date, List<Models.Movie> movies)
        {
            foreach (var m in movies)
            {
                DateTime d = DateTime.Parse(m.Session.Date);
                DateTime time = DateTime.Parse(m.Session.Time);
                DateTime dtCombined = new DateTime(d.Year, d.Month, d.Day, time.Hour, time.Minute, time.Second);
                if (dtCombined.Date == date.Date)
                {
                    int MinutesOfMovie;

                    if (m.Runtime == "N/A")
                        MinutesOfMovie = 95;
                    else
                        MinutesOfMovie = int.Parse(m.Runtime.Replace("min", " ").Trim());

                    var result = Math.Abs(dtCombined.Subtract(date).TotalMinutes);
                    if (result < MinutesOfMovie)
                        return true;
                }
            }
            return false;
        }
        internal static RandomDateTime rdt = new RandomDateTime();

        public static void AssignRandomly(string filename, Hall hall, string cinemaName)
        {
            int movieCount = random.Next(7, 13);
            var movies = GetRandomMovies(movieCount);
            List<Movie> moviesList = new List<Movie>();
            foreach (var movie in movies)
            {
                var m = new Movie()
                {
                    Actors = movie.Actors,
                    Awards = movie.Awards,
                    Country = movie.Country,
                    Formats = movie.Formats,
                    Director = movie.Director,
                    Genre = movie.Genre,
                    Id = movie.Id,
                    ImdbRating = movie.ImdbRating,
                    Language = movie.Language,
                    Plot = movie.Plot,
                    Poster = movie.Poster,
                    Price = movie.Price,
                    Released = movie.Released,
                    Runtime = movie.Runtime,
                    Title = movie.Title,
                    Writer = movie.Writer,
                    Year = movie.Year,
                };

                m.SetPrice();
                //var timespan = (DateTime.Today.Date - dDate.Date).Days;
                m.Session = new Session()
                {
                    Cinema = cinemaName,
                    Hall = hall.HallName,
                    Price = m.Price,
                };
                //m.Session.Date = DateTime.Parse(m.Session.Date).AddDays(timespan).ToString();

                DateTime date;
                do
                {
                    date = rdt.Next();


                } while (Foo(date, moviesList));
                m.Session.Date = date.Date.ToString();
                m.Session.Time = date.TimeOfDay.ToString();


                int no = random.Next(0, 100);
                if (no % 2 == 0)
                {
                    m.Subtitle = Enums.Subtitles.Azerbaijani;
                }
                else if (no % 2 == 1)
                {
                    m.Subtitle = Enums.Subtitles.Turkish;
                }

                int busyPlaceCount = random.Next(8, 60);
                List<int> list = new List<int>();
                for (int x = 0; x < busyPlaceCount; x++)
                {
                    int number;
                    do
                    {
                        number = random.Next(0, 91);
                    } while (list.Contains(number));
                    list.Add(number);
                }

                moviesList.Add(m);
                m.BusySeats = list;
            }
            hall.HallMovies = moviesList;
            JsonSerialization<Models.Movie>.Serialize(moviesList, filename);
        }

        //private void RandomInitializeCinemas()
        //{
        // Deniz mall
        //App.Cinemas[0].Name = "Deniz mall";
        //App.Cinemas[0].Halls.Add(new Hall() { HallName = "BirKart Dolby" });
        //App.Cinemas[0].Halls.Add(new Hall() { HallName = "PAŞA Həyat" });
        //App.Cinemas[0].Halls.Add(new Hall() { HallName = "Access Bank" });

        //// Gənclik Mall
        //App.Cinemas[1].Name = "Gənclik Mall";
        //App.Cinemas[1].Halls.Add(new Hall() { HallName = "Platinum" });
        //App.Cinemas[1].Halls.Add(new Hall() { HallName = "Hall 6" });

        //// 28 Mall
        //App.Cinemas[2].Name = "28 Mall";
        //App.Cinemas[2].Halls.Add(new Hall() { HallName = "PAŞA Sığorta" });
        //App.Cinemas[2].Halls.Add(new Hall() { HallName = "Berqa" });
        //App.Cinemas[2].Halls.Add(new Hall() { HallName = "InvestAZ" });

        //// Azerbaijan Cinema
        //App.Cinemas[3].Name = "Azerbaijan Cinema";
        //App.Cinemas[3].Halls.Add(new Hall() { HallName = "Coffee `Njoy" });
        //App.Cinemas[3].Halls.Add(new Hall() { HallName = "Unicapital" });

        //// Amburan Mall
        //App.Cinemas[4].Name = "Amburan Mall";
        //App.Cinemas[4].Halls.Add(new Hall() { HallName = "Birkart" });

        //// Sumgayit
        //App.Cinemas[5].Name = "Sumgayit";
        //App.Cinemas[5].Halls.Add(new Hall() { HallName = "Neo Kart" });
        //App.Cinemas[5].Halls.Add(new Hall() { HallName = "Access Bank" });

        //// Khamsa Park
        //App.Cinemas[6].Name = "Khamsa Park";
        //App.Cinemas[6].Halls.Add(new Hall() { HallName = "Hall 3" });
        //App.Cinemas[6].Halls.Add(new Hall() { HallName = "Tam Kart (Atmos)" });

        //// Ganja Mall
        //App.Cinemas[7].Name = "Ganja Mall";
        //App.Cinemas[7].Halls.Add(new Hall() { HallName = "Hall 4" });

        //// Nakhchivan
        //App.Cinemas[8].Name = "Nakhchivan";
        //App.Cinemas[8].Halls.Add(new Hall() { HallName = "Hall 7" });

        //// Shamakhi
        //App.Cinemas[9].Name = "Shamakhi";
        //App.Cinemas[9].Halls.Add(new Hall() { HallName = "Emiland VIP" });
        //JsonSerialization<Cinema>.Serialize(App.Cinemas, @"~/../../../Files/Defaults\defaultCinemas.json");
        //}
        #endregion

        public static void InitializeCinemas()
        {
            App.Cinemas.Clear();
            App.Cinemas = JsonSerialization<Cinema>.Deserialize(@"~/../../../Files/Defaults\defaultCinemas.json");
            ReadMoviesFromFiles();
            App.Languages = GetLanguagesFromDefaultMovies();
        }

        public static void ReadMoviesFromFiles()
        {
            var date = DateTime.Today;
            List<Movie> movies = new List<Movie>();
            foreach (var cinema in App.Cinemas)
            {
                foreach (var hall in cinema.Halls)
                {
                    string filename = @"~/../../../Files/Halls\" + cinema.Name.Replace(" ", string.Empty) + "+" + hall.HallName.Replace(" ", string.Empty) + ".json";
                    //AssignRandomly(filename, hall, cinema.Name);
                    hall.HallMovies = JsonSerialization<Models.Movie>.Deserialize(filename);
                    foreach (var movie in hall.HallMovies)
                    {
                        //Onumuzdeki 1 hefte ucun nezerde tutulub
                        var timespan = (DateTime.Today.Date - date.Date).Days;
                        movie.Session.Date = DateTime.Parse(movie.Session.Date).AddDays(timespan).ToString();
                        movies.Add(movie);
                    }
                }
            }
            App.AllMoviesFromAllHalls = movies;
            movies = movies.Distinct(new MovieTitleComparer()).ToList();
            App.Movies = SortMoviesAsIWant(movies);
        }

        public class MovieTitleComparer : IEqualityComparer<Movie>
        {
            public bool Equals(Movie x, Movie y)
            {
                return x.Title == y.Title;
            }

            public int GetHashCode(Movie obj)
            {
                return base.GetHashCode();
            }
        }

        public static void RemoveMoviesFromFiles(string title)
        {
            foreach (var cinema in App.Cinemas)
            {
                foreach (var hall in cinema.Halls)
                {
                    if (hall.HallMovies.Find((m) => m.Title == title) != null)
                    {
                        string filename = @"~/../../../Files/Halls\" + cinema.Name.Replace(" ", string.Empty) + "+" + hall.HallName.Replace(" ", string.Empty) + ".json";
                        List<Models.Movie> hallMovies = JsonSerialization<Models.Movie>.Deserialize(filename);
                        hallMovies.RemoveAll(x => x.Title == title);
                        JsonSerialization<Models.Movie>.Serialize(hallMovies, filename);
                    }
                }
            }
        }

        public static List<Movie> GetRandomMoviesFromFile(int count) // for admin side - in admin side there are random movies that are not in the app
        {
            var randomNewMovies = new List<Movie>();
            try
            {
                var indexes = new List<int>();
                var movies = JsonSerialization<Models.Movie>.Deserialize(@"~/../../../Files/Defaults\RandomMoviesToAdd.json");
                for (int x = 0; x < count; x++)
                {
                    int index;
                    do
                    {
                        index = random.Next(0, movies.Count);
                    } while (indexes.Contains(index));

                    randomNewMovies.Add(movies.ElementAt(index));
                    indexes.Add(index);
                }
                return randomNewMovies;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Movie> GetRandomMovies(int count) // for user side - random movies that are in the app
        {
            var randomMovies = new List<Movie>();
            var indexes = new List<int>();

            List<Movie> movies;
            if (App.Movies.Count != 0)
                movies = App.Movies;
            else
                movies = App.DefaultMovies;

            for (int x = 0; x < count; x++)
            {
                int index;
                do
                {
                    index = random.Next(0, movies.Count);
                } while (indexes.Contains(index) && movies.ElementAt(index) != App.SelectedMovie);

                randomMovies.Add(movies.ElementAt(index));
                indexes.Add(index);
            }
            return randomMovies;
        }

        public static void SortMoviesByImdbRating(List<Movie> movies)
        {
            var movieListWithRatings = movies.Where((m) => m.ImdbRating != "N/A").ToArray();
            var moviesWithNoRatings = movies.Where((m) => m.ImdbRating == "N/A").ToArray();
            Array.Sort(movieListWithRatings, new MovieComparer());

            movies.Clear();
            movies.AddRange(moviesWithNoRatings);
            movies.AddRange(movieListWithRatings);
        }

        class MovieComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                return (new CaseInsensitiveComparer()).Compare(double.Parse(((Movie)x).ImdbRating),
                       double.Parse(((Movie)y).ImdbRating));
            }
        }

        public static List<string> GetCinemaNames()
        {
            var names = new List<string>();
            names.Add("All Cinemas");
            foreach (var cinema in App.Cinemas)
            {
                names.Add(cinema.Name);
            }
            return names;
        }

        public static List<string> GetDates()
        {
            var dates = new List<string>();
            DateTime today = DateTime.Today;

            for (int x = 0; x < 7; x++) // 7 days, a week
            {
                var stringDate = today.ToShortDateString();
                stringDate = stringDate.Replace("/", ".");
                dates.Add(stringDate);
                today = today.AddDays(1);
            }

            return dates;
        }

        public static List<string> GetAllTimes()
        {
            var times = new List<string>();
            DateTime today = DateTime.Today;
            today = today.AddHours(13);

            for (int x = 0; x < 11; x++)
            {
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 5}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 10}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 15}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 20}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 25}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 30}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 35}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 40}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 45}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 50}").ToShortTimeString().Replace(":00 ", " ").Trim());
                times.Add(DateTime.Parse($"{today.Hour}:{today.Minute + 55}").ToShortTimeString().Replace(":00 ", " ").Trim());
                today = today.AddHours(1);
            }

            return times;
        }

        public static FrameworkElement RemoveElementFromItsParent(FrameworkElement el)
        {
            if (el?.Parent == null)
                return el;
            var panel = el.Parent as Panel;
            if (panel != null)
            {
                panel.Children.Remove(el);
                return el;
            }
            var decorator = el.Parent as Decorator;
            if (decorator != null)
            {
                decorator.Child = null;
                return el;
            }
            var contentPresenter = el.Parent as ContentPresenter;
            if (contentPresenter != null)
            {
                contentPresenter.Content = null;
                return el;
            }
            var contentControl = el.Parent as ContentControl;
            if (contentControl != null)
                contentControl.Content = null;
            return el;
        }

        public static List<Admin> GetAdminsFromFile()
        {
            try
            {
                var admins = new List<Admin>();
                var fileText = File.ReadAllText(@"~/../../../Files/Defaults\Admins.txt");
                var strings = fileText.Split('\n');
                foreach (var str in strings)
                {
                    var data = str.Split(',');
                    var admin = new Admin(data[0].Trim(), data[1].Trim());
                    admins.Add(admin);
                }
                return admins;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // returns List of hall names in which movie exists
        public static List<DetailedHall> GetHallsMovieExists(string movie_title)
        {
            var halls = new List<DetailedHall>();
            foreach (var cinema in App.Cinemas)
            {
                foreach (var hall in cinema.Halls)
                {
                    foreach (var _movie in hall.HallMovies)
                    {
                        if (_movie.Title == movie_title)
                        {
                            var _hall = new DetailedHall()
                            {
                                BusySeats = _movie.BusySeats,
                                Cinema = cinema.Name,
                                Date = DateTime.Parse(_movie.Session.Date.Split(' ').ElementAt(0)).ToShortDateString().Replace("/", ".") + ", " + DateTime.Parse(_movie.Session.Time).ToLongTimeString().Replace(":00 ", " ").Trim(),
                                Hallname = hall.HallName
                            };
                            halls.Add(_hall);
                        }
                    }
                }
            }
            return halls;
        }
    }

}
