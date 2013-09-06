using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using FirstFloor.ModernUI.Presentation;
using System.Linq;
using System.Windows.Documents;
using TestCaseManagerApp.BusinessLogic.Entities;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp.ViewModels
{
    public class TestCasesBatchDuplicateViewModel: NotifyPropertyChanged
    { 
        public bool TitleFlag;
        public bool SuiteFlag;

        private readonly Dispatcher dispatcher;
        private bool replaceInTitle;
        private bool replaceSharedSteps;
        private bool replaceInSteps;
        private string testCasesCount;

        public TestCasesBatchDuplicateViewModel()
        {
            InitializeInnerCollections();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites.GetSuiteEntries();
            testCasesList.ForEach(t => ObservableTestCases.Add(t));
            InitializeInitialTestCaseCollection();
            InitializeTestSuiteList();
            ReplaceInTitles = true;
            ReplaceInSteps = true;
            ReplaceSharedSteps = true;
        }

        public TestCasesBatchDuplicateViewModel(TestCasesBatchDuplicateViewModel viewModel)
        {
            InitializeInnerCollections();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites.GetSuiteEntries();
            testCasesList.ForEach(t => ObservableTestCases.Add(t));
            InitializeInitialTestCaseCollection();
            InitializeTestSuiteList();
            TestCasesCount = ObservableTestCases.Count.ToString();
            InitialViewFilters = viewModel.InitialViewFilters;
            TitleFlag = viewModel.TitleFlag;
            SuiteFlag = viewModel.SuiteFlag;
        }

        public ObservableCollection<TestCase> ObservableTestCases { get; set; }
        public ObservableCollection<TextReplacePair> ObservableTextReplacePairs { get; set; }
        public ObservableCollection<SharedStepIdReplacePair> ObservableSharedStepIdReplacePairs { get; set; }
        public ObservableCollection<TestCase> InitialTestCaseCollection { get; set; }
        public ObservableCollection<ITestSuiteBase> ObservableTestSuites { get; set; }
        public List<TestCase> SelectedTestCases { get; set; }
        public InitialViewFilters InitialViewFilters { get; set; }

        public bool ReplaceInTitles
        {
            get
            {
                return replaceInTitle;
            }
            set
            {
                replaceInTitle = value;
                OnPropertyChanged("ReplaceInTitles");
            }
        }

        public bool ReplaceSharedSteps
        {
            get
            {
                return replaceSharedSteps;
            }
            set
            {
                replaceSharedSteps = value;
                OnPropertyChanged("ReplaceSharedSteps");
            }
        }

        public bool ReplaceInSteps
        {
            get
            {
                return replaceInSteps;
            }
            set
            {
                replaceInSteps = value;
                OnPropertyChanged("ReplaceInSteps");
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

        private void InitializeTestSuiteList()
        {
            List<ITestSuiteBase> testSuiteList = ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites.GetSuites();
            testSuiteList.ForEach(s => ObservableTestSuites.Add(s));
        }       

        private void InitializeInitialTestCaseCollection()
        {
            InitialTestCaseCollection = new ObservableCollection<TestCase>();
            foreach (var cTestCase in ObservableTestCases)
            {
                InitialTestCaseCollection.Add(cTestCase);
            }
        }

        private void InitializeInnerCollections()
        {
            ObservableTestSuites = new ObservableCollection<ITestSuiteBase>();
            ObservableTextReplacePairs = new ObservableCollection<TextReplacePair>();
            ObservableTextReplacePairs.Add(new TextReplacePair());
            ObservableSharedStepIdReplacePairs = new ObservableCollection<SharedStepIdReplacePair>();
            ObservableSharedStepIdReplacePairs.Add(new SharedStepIdReplacePair());
            SelectedTestCases = new List<TestCase>();
            ObservableTestCases = new ObservableCollection<TestCase>();
            InitialViewFilters = new InitialViewFilters();
        }

        //public void ReinitializeTestCases()
        //{
        //    List<TestCase> newTestCases = TestCaseManager.GetAllTestCases();
        //    ObservableTestCases.Clear();
        //    newTestCases.ForEach(x => ObservableTestCases.Add(x));
        //}

        public void ReinitializeTestCases()
        {
            ObservableTestCases.Clear();
            foreach (var item in InitialTestCaseCollection)
            {
                ObservableTestCases.Add(item);
            }
        }

        public void FilterTestCases()
        {
            ReinitializeTestCases();
            var filteredList = ObservableTestCases
                .Where(t => ((TitleFlag && !String.IsNullOrEmpty(InitialViewFilters.TitleFilter)) ? t.ITestCase.Title.ToLower().Contains(InitialViewFilters.TitleFilter.ToLower()) : true)
                    && ((SuiteFlag && !String.IsNullOrEmpty(InitialViewFilters.SuiteFilter)) ? t.ITestSuiteBase.Title.ToLower().Contains(InitialViewFilters.SuiteFilter.ToLower()) : true)).ToList();
            ObservableTestCases.Clear();
            filteredList.ForEach(x => ObservableTestCases.Add(x));
            TestCasesCount = filteredList.Count.ToString();
        }
    }
}

