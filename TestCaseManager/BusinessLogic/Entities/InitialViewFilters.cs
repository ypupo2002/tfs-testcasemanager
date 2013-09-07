using FirstFloor.ModernUI.Presentation;

namespace TestCaseManagerApp.BusinessLogic.Entities
{
    /// <summary>
    /// Contains search filters for the initial app view
    /// </summary>
    public class InitialViewFilters : NotifyPropertyChanged
    {
        private string titleFilter;
        private string suiteFilter;
        private string idFilter;

        public InitialViewFilters(string title, string suite, string id)
        {
            TitleFilter = title;
            SuiteFilter = suite;
            IdFilter = id;
        }

        public InitialViewFilters()
        {
            IdFilter = "ID";
            TitleFilter = "Title";
            SuiteFilter = "Suite";
        }

        /// <summary>
        /// Gets or sets the title filter.
        /// </summary>
        /// <value>
        /// The title filter.
        /// </value>
        public string TitleFilter
        {
            get
            {
                return titleFilter;
            }
            set
            {
                titleFilter = value;
                OnPropertyChanged("TitleFilter");
            }
        }

        /// <summary>
        /// Gets or sets the suite filter.
        /// </summary>
        /// <value>
        /// The suite filter.
        /// </value>
        public string SuiteFilter
        {
            get
            {
                return suiteFilter;
            }
            set
            {
                suiteFilter = value;
                OnPropertyChanged("SuiteFilter");
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier filter.
        /// </summary>
        /// <value>
        /// The unique identifier filter.
        /// </value>
        public string IdFilter
        {
            get
            {
                return idFilter;
            }
            set
            {
                idFilter = value;
                OnPropertyChanged("IdFilter");
            }
        }
    }
}
