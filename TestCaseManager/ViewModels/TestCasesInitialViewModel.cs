// <copyright file="TestCasesInitialViewModel.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Threading;
    using FirstFloor.ModernUI.Presentation;
    using TestCaseManagerApp.BusinessLogic.Entities;

    /// <summary>
    /// Contains methods and properties related to the TestCasesInitial View
    /// </summary>
    public class TestCasesInitialViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// Indicating wheter the automated test cases should be displayed in the test cases grid
        /// </summary>
        private bool hideAutomated;

        /// <summary>
        /// The test cases count after filtering
        /// </summary>
        private string testCasesCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialViewModel"/> class.
        /// </summary>
        public TestCasesInitialViewModel()
        {
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = TestCaseManager.GetAllTestCasesInTestPlan();
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            testCasesList.ForEach(t => this.ObservableTestCases.Add(t));
            this.InitialViewFilters = new InitialViewFilters();
            this.InitializeInitialTestCaseCollection();
            this.TestCasesCount = this.ObservableTestCases.Count.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">The old view model.</param>
        public TestCasesInitialViewModel(TestCasesInitialViewModel viewModel) : this()
        {
            this.InitialViewFilters = viewModel.InitialViewFilters;
            this.HideAutomated = viewModel.HideAutomated;
        }

        /// <summary>
        /// Gets or sets the observable test cases.
        /// </summary>
        /// <value>
        /// The observable test cases.
        /// </value>
        public ObservableCollection<TestCase> ObservableTestCases { get; set; }

        /// <summary>
        /// Gets or sets the initial test case collection.
        /// </summary>
        /// <value>
        /// The initial test case collection.
        /// </value>
        public ObservableCollection<TestCase> InitialTestCaseCollection { get; set; }

        /// <summary>
        /// Gets or sets the initial view filters.
        /// </summary>
        /// <value>
        /// The initial view filters.
        /// </value>
        public InitialViewFilters InitialViewFilters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [the automated test cases should be displayed in the test cases grid].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [the automated test cases should be displayed in the test cases grid]; otherwise, <c>false</c>.
        /// </value>
        public bool HideAutomated
        {
            get
            {
                return this.hideAutomated;
            }

            set
            {
                this.hideAutomated = value;
                this.OnPropertyChanged("HideAutomated");
            }
        }

        /// <summary>
        /// Gets or sets the test cases count after filtering.
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
        /// Filters the test cases.
        /// </summary>
        public void FilterTestCases()
        {
            this.ReinitializeTestCases();

            var filteredList = this.ObservableTestCases.Where(
                t => (t.ITestCase != null)
                    && ((this.InitialViewFilters.IsTitleTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.TitleFilter)) ? t.ITestCase.Title.ToLower().Contains(this.InitialViewFilters.TitleFilter.ToLower()) : true)
                    && ((this.InitialViewFilters.IsSuiteTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.SuiteFilter)) ? t.ITestSuiteBase.Title.ToLower().Contains(this.InitialViewFilters.SuiteFilter.ToLower()) : true)
                    && ((this.InitialViewFilters.IsIdTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.IdFilter)) ? t.ITestCase.Id.ToString().Contains(this.InitialViewFilters.IdFilter) : true)
                    && (!this.HideAutomated.Equals(t.ITestCase.IsAutomated) || !this.HideAutomated)).ToList();
            this.ObservableTestCases.Clear();
            filteredList.ForEach(x => this.ObservableTestCases.Add(x));

            this.TestCasesCount = filteredList.Count.ToString();
        }

        /// <summary>
        /// Refreshes the test cases.
        /// </summary>
        public void RefreshTestCases()
        {
            this.ObservableTestCases.Clear();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = TestCaseManager.GetAllTestCasesInTestPlan();
            testCasesList.ForEach(t => this.ObservableTestCases.Add(t));
        }

        /// <summary>
        /// Reinitializes the test cases.
        /// </summary>
        public void ReinitializeTestCases()
        {
            this.ObservableTestCases.Clear();
            foreach (var item in this.InitialTestCaseCollection)
            {
                this.ObservableTestCases.Add(item);
            }
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
    }
}