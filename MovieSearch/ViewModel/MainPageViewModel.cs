using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieSearch.Common;
using MovieSearch.ViewModel;

namespace MovieSearch
{
    public class MainPageViewModel : BindableBase
    {
        /// <summary>
        /// The list of all movies.
        /// </summary>
        private List<Movie> allMovies;

        /// <summary>
        /// The list of the filtered movies.
        /// </summary>
        private List<Movie> movies;

        /// <summary>
        /// Stores the last search expression to enable a delay.
        /// </summary>
        private string lastSearch;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/>.
        /// </summary>
        public MainPageViewModel()
        {
            this.allMovies = new List<Movie>
            {
                new Movie { Title = "Matrix", Subtitle = "Lorem ipsum...", ImagePath = new Uri("http://lorempixel.com/250/250/nightlife/1") },
                new Movie { Title = "Der Pate", Subtitle = "Lorem ipsum...", ImagePath = new Uri("http://lorempixel.com/250/250/nightlife/2") },
                new Movie { Title = "The Dark Knight", Subtitle = "Lorem ipsum...", ImagePath = new Uri("http://lorempixel.com/250/250/nightlife/3") },
                new Movie { Title = "Pulp Fiction", Subtitle = "Lorem ipsum...", ImagePath = new Uri("http://lorempixel.com/250/250/nightlife/4") },
                new Movie { Title = "Fight Club", Subtitle = "Lorem ipsum...", ImagePath = new Uri("http://lorempixel.com/250/250/nightlife/5") },
            };

            this.Movies = this.allMovies;
            this.SearchOnTypingCommand = new DelegateCommand(this.FilterMoviesByTitle);
        }

        /// <summary>
        /// Gets or sets the command that is executed on search.
        /// </summary>
        public DelegateCommand SearchOnTypingCommand { get; set; }

        /// <summary>
        /// Gets or sets the list of movies to display
        /// </summary>
        public List<Movie> Movies
        {
            get { return this.movies; }
            set { this.SetProperty(ref this.movies, value); }
        }

        /// <summary>
        /// Filters the movies by their title. Adds a slight delay that the search does not trigger on every key press.
        /// </summary>
        /// <param name="parameter">The search expression</param>
        private async void FilterMoviesByTitle(object parameter)
        {
            string searchExpression = parameter.ToString();
            this.lastSearch = searchExpression;

            // Delay for search
            await Task.Delay(500);

            if (searchExpression == this.lastSearch)
            {
                if (string.IsNullOrWhiteSpace(searchExpression))
                {
                    this.Movies = this.allMovies;
                }
                else
                {
                    this.Movies = new List<Movie>(this.allMovies.Where(x => x.Title.IndexOf(searchExpression, System.StringComparison.CurrentCultureIgnoreCase) > -1));
                }
            }
        }
    }
}
