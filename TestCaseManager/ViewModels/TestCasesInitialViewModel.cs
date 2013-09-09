using System.Collections.Generic;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.Presentation;
using System.Linq;
using System.Windows.Threading;
using System;
using TestCaseManagerApp.BusinessLogic.Entities;

namespace TestCaseManagerApp.ViewModels
{
    public class TestCasesInitialViewModel: NotifyPropertyChanged
    {
        public bool isIdFilterSet;
        public bool isTitleFilterSet;
        public bool isSuiteFilterSet;

        private bool hideAutomated;
        private string testCasesCount;

        public TestCasesInitialViewModel()
        {
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = TestCaseManager.GetAllTestCasesInTestPlan();
            ObservableTestCases = new ObservableCollection<TestCase>();            
            testCasesList.ForEach(t => ObservableTestCases.Add(t));
            InitialViewFilters = new InitialViewFilters();
            InitializeInitialTestCaseCollection();
            TestCasesCount = ObservableTestCases.Count.ToString();
        }

        public TestCasesInitialViewModel(TestCasesInitialViewModel viewModel) : this()
        {
            InitialViewFilters = viewModel.InitialViewFilters;
            isIdFilterSet = viewModel.isIdFilterSet;
            isTitleFilterSet = viewModel.isTitleFilterSet;
            isSuiteFilterSet = viewModel.isSuiteFilterSet;
            HideAutomated = viewModel.HideAutomated;
        }

        public ObservableCollection<TestCase> ObservableTestCases { get; set; }
        public ObservableCollection<TestCase> InitialTestCaseCollection { get; set; }
        public InitialViewFilters InitialViewFilters { get; set; }

        public bool HideAutomated
        {
            get
            {
                return hideAutomated;
            }
            set
            {
                hideAutomated = value;
                OnPropertyChanged("HideAutomated");
            }
        }       

        public string TestCasesCount
        {
            get
            {
                return testCasesCount;
            }
            set
            {
                testCasesCount = value;
                OnPropertyChanged("TestCasesCount");
            }
        }

        public void FilterTestCases()
        {
            ReinitializeTestCases();

            var filteredList = ObservableTestCases
                .Where(t => (t.ITestCase != null)
                    && ((isTitleFilterSet && !String.IsNullOrEmpty(InitialViewFilters.TitleFilter)) ? t.ITestCase.Title.ToLower().Contains(InitialViewFilters.TitleFilter.ToLower()) : true)
                    && ((isSuiteFilterSet && !String.IsNullOrEmpty(InitialViewFilters.SuiteFilter)) ? t.ITestSuiteBase.Title.ToLower().Contains(InitialViewFilters.SuiteFilter.ToLower()) : true)
                    && ((isIdFilterSet && !String.IsNullOrEmpty(InitialViewFilters.IdFilter)) ? t.ITestCase.Id.ToString().Contains(InitialViewFilters.IdFilter) : true)
                    && (!HideAutomated.Equals(t.ITestCase.IsAutomated) || !HideAutomated)).ToList();
            ObservableTestCases.Clear();
            filteredList.ForEach(x => ObservableTestCases.Add(x));

            TestCasesCount = filteredList.Count.ToString();
        }  

        public void RefreshTestCases()
        {
            ObservableTestCases.Clear();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = TestCaseManager.GetAllTestCasesInTestPlan();
            testCasesList.ForEach(t => ObservableTestCases.Add(t));
        }

        public void ReinitializeTestCases()
        {
            ObservableTestCases.Clear();
            foreach (var item in InitialTestCaseCollection)
            {
                ObservableTestCases.Add(item);
            }
        }

        private void InitializeInitialTestCaseCollection()
        {
            InitialTestCaseCollection = new ObservableCollection<TestCase>();
            foreach (var cTestCase in ObservableTestCases)
            {
                InitialTestCaseCollection.Add(cTestCase);
            }
        }
    }
}