using CinemaPlus.Commands;
using CinemaPlus.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CinemaPlus.ViewModels.AdminSideViewModels
{
    public class EditMovieMainTabUCViewModel : BaseViewModel
    {
        public RelayCommand UpCommand { get; set; }
        public RelayCommand DownCommand { get; set; }
        public RelayCommand IMDbUpCommand { get; set; }
        public RelayCommand IMDbDownCommand { get; set; }

        private Models.Movie movie;

        public Models.Movie Movie
        {
            get { return movie; }
            set { movie = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string year = string.Empty;

        public string Year
        {
            get { return year; }
            set { year = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string released;

        public string Released
        {
            get { return released; }
            set { released = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string genre;

        public string Genre
        {
            get { return genre; }
            set { genre = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string director;

        public string Director
        {
            get { return director; }
            set { director = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string writer;

        public string Writer
        {
            get { return writer; }
            set { writer = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string actors;

        public string Actors
        {
            get { return actors; }
            set { actors = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string awards;

        public string Awards
        {
            get { return awards; }
            set { awards = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string runtime;

        public string Runtime
        {
            get { return runtime; }
            set { runtime = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string imdbRating;

        public string ImdbRating
        {
            get { return imdbRating; }
            set { imdbRating = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private bool azerbaijaniIsChecked;

        public bool AzerbaijaniIsChecked
        {
            get { return azerbaijaniIsChecked; }
            set { azerbaijaniIsChecked = value; OnPropertyChanged(); 
                if (value == true)
                    SelectedSubtitle = Helper.Enums.Subtitles.Azerbaijani;
                App.HasChanges = true; }
        }

        private bool turkishIsChecked;

        public bool TurkishIsChecked
        {
            get { return turkishIsChecked; }
            set { turkishIsChecked = value; OnPropertyChanged();
                if (value == true)
                    SelectedSubtitle = Helper.Enums.Subtitles.Turkish;
                App.HasChanges = true; }
        }

        private Helper.Enums.Subtitles selectedSubtitle;

        public  Helper.Enums.Subtitles SelectedSubtitle
        {
            get { return selectedSubtitle; }
            set { selectedSubtitle = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private List<string> countries = new List<string>();

        public List<string> Countries
        {
            get { return countries; }
            set { countries = value; OnPropertyChanged(); }
        }

        private List<string> languages = new List<string>();

        public List<string> Languages
        {
            get { return languages; }
            set { languages = value; OnPropertyChanged(); }
        }

        private int countriesCBSelectedIndex;

        public int CountriesCBSelectedIndex
        {
            get { return countriesCBSelectedIndex; }
            set { countriesCBSelectedIndex = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private int languagesCBSelectedIndex;

        public int LanguagesCBSelectedIndex
        {
            get { return languagesCBSelectedIndex; }
            set { languagesCBSelectedIndex = value; OnPropertyChanged(); App.HasChanges = true; }
        }

        private static CultureInfo[] CultureInfos { get; set; } = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        public EditMovieMainTabUCViewModel()
        {
            foreach (CultureInfo culture in CultureInfos)
            {
                RegionInfo region = new RegionInfo(culture.LCID);

                if (!(Countries.Contains(region.EnglishName)))
                {
                    Countries.Add(region.EnglishName);
                }

                string language = culture.EnglishName;
                if (language.Contains("("))
                    language = language.Remove(language.IndexOf("("), language.IndexOf(")") - language.IndexOf("(") + 1).Trim();
                if (!Languages.Contains(language))
                    languages.Add(language);
            }
            Countries.Sort();
            Languages.Sort();

            UpCommand = new RelayCommand((runtimeTextBox) =>
            {
                var textBox = runtimeTextBox as TextBox;

                if (textBox.Text.Trim() == "N/A" || textBox.Text.Trim() == String.Empty)
                {
                    textBox.Text = "0";
                }

                int value = (int)double.Parse(textBox.Text.Trim());
                if (textBox.Text.Contains(".00"))
                    textBox.Text = (value + 1).ToString() + ".00";
                else
                    textBox.Text = (value + 1).ToString();
            });

            DownCommand = new RelayCommand((runtimeTextBox) =>
            {
                var textBox = runtimeTextBox as TextBox;

                if (textBox.Text.Trim() == "N/A" || textBox.Text.Trim() == String.Empty)
                {
                    textBox.Text = "0";
                }

                int value = (int)double.Parse(textBox.Text.Trim());
                if (value > 1)
                {
                    if (textBox.Text.Contains(".00"))
                        textBox.Text = (value - 1).ToString() + ".00";
                    else
                        textBox.Text = (value - 1).ToString();
                }
            });

            IMDbUpCommand = new RelayCommand((imdbTextBox) =>
            {
                var textBox = imdbTextBox as TextBox;

                if (textBox.Text == "N/A")
                {
                    textBox.Text = "0";
                }

                var value = double.Parse(textBox.Text);
                if (value + 0.1 <= 10)
                {
                    textBox.Text = (value + 0.1).ToString();
                    if (!textBox.Text.Contains("."))
                    {
                        textBox.Text += ".0";
                    }
                }
            });

            IMDbDownCommand = new RelayCommand((imdbTextBox) =>
            {
                var textBox = imdbTextBox as TextBox;

                if (textBox.Text == "N/A")
                {
                    textBox.Text = "0";
                }

                var value = double.Parse(textBox.Text);
                if (value > 1)
                {

                    textBox.Text = (value - 0.1).ToString();

                    if (!textBox.Text.Contains("."))
                    {
                        textBox.Text += ".0";
                    }
                }
            });
        }

        public void UpdateProperties()
        {
            if (App.HasChanges)
            {
                App.HasChanges = false;
                Title = movie.Title;
                Year = movie.Year;
                Released = movie.Released;
                Genre = movie.Genre;
                Director = movie.Director;
                Writer = movie.Writer;
                Actors = movie.Actors;
                Awards = movie.Awards;
                CountriesCBSelectedIndex = Countries.IndexOf(movie.Country);
                LanguagesCBSelectedIndex = Languages.IndexOf(movie.Language);
                Runtime = movie.Runtime.Replace("min", "").Trim();
                ImdbRating = movie.ImdbRating;
                Price = movie.Price;
                if (movie.Subtitle == Helper.Enums.Subtitles.Azerbaijani) // subtitle
                {
                    AzerbaijaniIsChecked = true;
                    TurkishIsChecked = false;
                }
                else if (movie.Subtitle == Helper.Enums.Subtitles.Turkish)
                {
                    AzerbaijaniIsChecked = false;
                    TurkishIsChecked = true;
                }
            }
        }

        private static readonly Regex OnlyNumberRegex = new Regex("[0-9]+");
        public void IsAllowedInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return OnlyNumberRegex.IsMatch(text);
        }
    }
}
