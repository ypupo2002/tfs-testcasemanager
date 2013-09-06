using FirstFloor.ModernUI.Presentation;

namespace TestCaseManagerApp.BusinessLogic.Entities
{
    public class InitialViewFilters : NotifyPropertyChanged
    {
        private string titleFilter;
        private string suiteFilter;
        private string idFilter;

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
    }
}
