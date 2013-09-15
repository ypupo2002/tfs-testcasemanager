// <copyright file="InitialViewFilters.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Entities
{
    using FirstFloor.ModernUI.Presentation;

    /// <summary>
    /// Contains search filters for the initial app view
    /// </summary>
    public class InitialViewFilters : NotifyPropertyChanged
    {
        /// <summary>
        /// The is title text set
        /// </summary>
        public bool IsTitleTextSet;

        /// <summary>
        /// The is suite text set
        /// </summary>
        public bool IsSuiteTextSet;

        /// <summary>
        /// The is unique identifier text set
        /// </summary>
        public bool IsIdTextSet;

        /// <summary>
        /// The title filter
        /// </summary>
        private string titleFilter;

        /// <summary>
        /// The suite filter
        /// </summary>
        private string suiteFilter;

        /// <summary>
        /// The unique identifier filter
        /// </summary>
        private string idFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialViewFilters"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="suite">The suite.</param>
        /// <param name="id">The unique identifier.</param>
        public InitialViewFilters(string title, string suite, string id)
        {
            this.TitleFilter = title;
            this.SuiteFilter = suite;
            this.IdFilter = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InitialViewFilters"/> class.
        /// </summary>
        public InitialViewFilters()
        {
            this.IdFilter = "ID";
            this.TitleFilter = "Title";
            this.SuiteFilter = "Suite";
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
                return this.titleFilter;
            }

            set
            {
                this.titleFilter = value;
                this.OnPropertyChanged("TitleFilter");
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
                return this.suiteFilter;
            }

            set
            {
                this.suiteFilter = value;
                this.OnPropertyChanged("SuiteFilter");
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
                return this.idFilter;
            }

            set
            {
                this.idFilter = value;
                this.OnPropertyChanged("IdFilter");
            }
        }
    }
}
