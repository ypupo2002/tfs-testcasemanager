// <copyright file="TestCasesBatchDuplicateViewModel.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Documents;
    using System.Windows.Threading;
    using FirstFloor.ModernUI.Presentation;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Entities;

    /// <summary>
    /// Contains methods and properties related to the TestCasesBatchDuplicate View
    /// </summary>
    public class TestCasesBatchDuplicateViewModel : NotifyPropertyChanged
    { 
        /// <summary>
        /// Defines if the text in titles should be replaced
        /// </summary>
        private bool replaceInTitle;

        /// <summary>
        /// Defines if the text in the test steps in the current test case shared steps should be replaced
        /// </summary>
        private bool replaceSharedSteps;

        /// <summary>
        /// Defines if the text in the test steps in the current test case should be replaced
        /// </summary>
        private bool replaceInTestSteps;

        /// <summary>
        /// The test cases count after filtering
        /// </summary>
        private string testCasesCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesBatchDuplicateViewModel"/> class.
        /// </summary>
        public TestCasesBatchDuplicateViewModel()
        {
            this.InitializeInnerCollections();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = TestCaseManager.GetAllTestCasesInTestPlan();
            testCasesList.ForEach(t => this.ObservableTestCases.Add(t));
            this.InitializeInitialTestCaseCollection();
            this.InitializeTestSuiteList();
            this.TestCasesCount = this.ObservableTestCases.Count.ToString();
            this.ReplaceInTitles = true;
            this.ReplaceInTestSteps = true;
            this.ReplaceSharedSteps = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesBatchDuplicateViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">The old view model.</param>
        public TestCasesBatchDuplicateViewModel(TestCasesBatchDuplicateViewModel viewModel) : this()
        {
            this.InitialViewFilters = viewModel.InitialViewFilters;
            this.ReplaceInTitles = viewModel.ReplaceInTitles;
            this.ReplaceInTestSteps = viewModel.ReplaceInTestSteps;
            this.ReplaceSharedSteps = viewModel.ReplaceSharedSteps;
        }

        /// <summary>
        /// Gets or sets the observable test cases.
        /// </summary>
        /// <value>
        /// The observable test cases.
        /// </value>
        public ObservableCollection<TestCase> ObservableTestCases { get; set; }

        /// <summary>
        /// Gets or sets the observable text replace pairs.
        /// </summary>
        /// <value>
        /// The observable text replace pairs.
        /// </value>
        public ObservableCollection<TextReplacePair> ObservableTextReplacePairs { get; set; }

        /// <summary>
        /// Gets or sets the observable shared step unique identifier replace pairs.
        /// </summary>
        /// <value>
        /// The observable shared step unique identifier replace pairs.
        /// </value>
        public ObservableCollection<SharedStepIdReplacePair> ObservableSharedStepIdReplacePairs { get; set; }

        /// <summary>
        /// Gets or sets the initial test case collection.
        /// </summary>
        /// <value>
        /// The initial test case collection.
        /// </value>
        public ObservableCollection<TestCase> InitialTestCaseCollection { get; set; }

        /// <summary>
        /// Gets or sets the observable test suites used in the suite drop down.
        /// </summary>
        /// <value>
        /// The observable test suites.
        /// </value>
        public ObservableCollection<ITestSuiteBase> ObservableTestSuites { get; set; }

        /// <summary>
        /// Gets or sets the selected test cases.
        /// </summary>
        /// <value>
        /// The selected test cases.
        /// </value>
        public List<TestCase> SelectedTestCases { get; set; }

        /// <summary>
        /// Gets or sets the initial view filters.
        /// </summary>
        /// <value>
        /// The initial view filters.
        /// </value>
        public InitialViewFilters InitialViewFilters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [the text in the test case title should be replaced].
        /// </summary>
        /// <value>
        /// <c>true</c> if [the text in the test case title should be replaced]; otherwise, <c>false</c>.
        /// </value>
        public bool ReplaceInTitles
        {
            get
            {
                return this.replaceInTitle;
            }

            set
            {
                this.replaceInTitle = value;
                this.OnPropertyChanged("ReplaceInTitles");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [text in the test steps in the shared steps should be replaced].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [text in the test steps in the shared steps should be replaced]; otherwise, <c>false</c>.
        /// </value>
        public bool ReplaceSharedSteps
        {
            get
            {
                return this.replaceSharedSteps;
            }

            set
            {
                this.replaceSharedSteps = value;
                this.OnPropertyChanged("ReplaceSharedSteps");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [text in the test steps of the current test case should be replaced].
        /// </summary>
        /// <value>
        /// <c>true</c> if [text in the test steps of the current test case should be replaced]; otherwise, <c>false</c>.
        /// </value>
        public bool ReplaceInTestSteps
        {
            get
            {
                return this.replaceInTestSteps;
            }

            set
            {
                this.replaceInTestSteps = value;
                this.OnPropertyChanged("ReplaceInSteps");
            }
        }

        /// <summary>
        /// Gets or sets the test cases count after filtering is applied.
        /// </summary>
        /// <value>
        /// The test cases count.
        /// </value>
        public string TestCasesCount
        {
            get
            {
                return this.testCasesCount;
            }

            set
            {
                this.testCasesCount = value;
                this.OnPropertyChanged("TestCasesCount");
            }
        }

        /// <summary>
        /// Reinitializes the test cases.
        /// </summary>
        public void ReinitializeTestCases()
        {
            this.ObservableTestCases.Clear();
            foreach (var currentTestCase in this.InitialTestCaseCollection)
            {
                this.ObservableTestCases.Add(currentTestCase);
            }
        }

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        public void FilterTestCases()
        {
            this.ReinitializeTestCases();
            var filteredList = this.ObservableTestCases.Where(
                t => ((InitialViewFilters.IsTitleTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.TitleFilter)) ? t.ITestCase.Title.ToLower().Contains(this.InitialViewFilters.TitleFilter.ToLower()) : true)
                    && ((InitialViewFilters.IsSuiteTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.SuiteFilter)) ? t.ITestSuiteBase.Title.ToLower().Contains(this.InitialViewFilters.SuiteFilter.ToLower()) : true)).ToList();
            this.ObservableTestCases.Clear();
            filteredList.ForEach(x => this.ObservableTestCases.Add(x));
            this.TestCasesCount = filteredList.Count.ToString();
        }

        /// <summary>
        /// Initializes the test suite list.
        /// </summary>
        private void InitializeTestSuiteList()
        {
            List<ITestSuiteBase> testSuiteList = TestSuiteManager.GetAllTestSuitesInTestPlan();
            testSuiteList.ForEach(s => this.ObservableTestSuites.Add(s));
        }

        /// <summary>
        /// Initializes the initial test case collection.
        /// </summary>
        private void InitializeInitialTestCaseCollection()
        {
            this.InitialTestCaseCollection = new ObservableCollection<TestCase>();
            foreach (var currentTestCase in this.ObservableTestCases)
            {
                this.InitialTestCaseCollection.Add(currentTestCase);
            }
        }

        /// <summary>
        /// Initializes the inner collections.
        /// </summary>
        private void InitializeInnerCollections()
        {
            this.ObservableTestSuites = new ObservableCollection<ITestSuiteBase>();
            this.ObservableTextReplacePairs = new ObservableCollection<TextReplacePair>();
            this.ObservableTextReplacePairs.Add(new TextReplacePair());
            this.ObservableSharedStepIdReplacePairs = new ObservableCollection<SharedStepIdReplacePair>();
            this.ObservableSharedStepIdReplacePairs.Add(new SharedStepIdReplacePair());
            this.SelectedTestCases = new List<TestCase>();
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            this.InitialViewFilters = new InitialViewFilters();
        }
    }
}