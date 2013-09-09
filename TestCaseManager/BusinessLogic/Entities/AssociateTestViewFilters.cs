using FirstFloor.ModernUI.Presentation;

namespace TestCaseManagerApp.BusinessLogic.Entities
{
    /// <summary>
    /// Contains search filters for the associate automation view
    /// </summary>
    public class AssociateTestViewFilters : NotifyPropertyChanged
    {
        /// <summary>
        /// The is full name filter set
        /// </summary>
        public bool isFullNameFilterSet;
        /// <summary>
        /// The is class name filter
        /// </summary>
        public bool isClassNameFilterSet;
        /// <summary>
        /// The full name filter
        /// </summary>
        private string fullNameFilter;
        /// <summary>
        /// The class name filter
        /// </summary>
        private string classNameFilter;

        public AssociateTestViewFilters(string fullName, string className)
        {
            FullNameFilter = fullName;
            ClassNameFilter = className;
        }

        public AssociateTestViewFilters()
        {
            FullNameFilter = "Full Name";
            ClassNameFilter = "Class Name";
        }

        /// <summary>
        /// Gets or sets the full name filter.
        /// </summary>
        /// <value>
        /// The full name filter.
        /// </value>
        public string FullNameFilter
        {
            get
            {
                return fullNameFilter;
            }
            set
            {
                fullNameFilter = value;
                OnPropertyChanged("FullNameFilter");
            }
        }
        /// <summary>
        /// Gets or sets the class name filter.
        /// </summary>
        /// <value>
        /// The class name filter.
        /// </value>
        public string ClassNameFilter
        {
            get
            {
                return classNameFilter;
            }
            set
            {
                classNameFilter = value;
                OnPropertyChanged("ClassNameFilter");
            }
        }
    }
}
