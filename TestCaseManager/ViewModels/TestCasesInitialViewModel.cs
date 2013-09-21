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
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Entities;

    /// <summary>
    /// Contains methods and properties related to the TestCasesInitial View
    /// </summary>
    public class TestCasesInitialViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// The selected suite unique identifier
        /// </summary>
        private int selectedSuiteId;

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
            this.InitializeFilters();

            // Load last selected suite in the treeview in order to selected it again
            this.selectedSuiteId = RegistryManager.GetSelectedSuiteIdFilter();
            List<TestCase> suiteTestCaseCollection = new List<TestCase>();
            if (this.selectedSuiteId != -1)
            {
                suiteTestCaseCollection = TestCaseManager.GetAllTestCaseFromSuite(this.selectedSuiteId);
            }
            else
            {
                suiteTestCaseCollection = TestCaseManager.GetAllTestCasesInTestPlan();
            }      
            
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            this.InitialTestCaseCollection = new ObservableCollection<TestCase>();
            suiteTestCaseCollection.ForEach(t => this.ObservableTestCases.Add(t));
            this.InitializeInitialTestCaseCollection(this.ObservableTestCases);
            this.FilterTestCases();         
            this.TestCasesCount = this.ObservableTestCases.Count.ToString();
            ObservableCollection<Suite> subSuites = TestSuiteManager.GetAllSuites(ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites);
            this.Suites = new ObservableCollection<Suite>();

            // Add a master node which will represnt all test cases in the plan. If selected all test cases will be displayed.
            Suite masterSuite = new Suite("ALL", -1, subSuites);
            masterSuite.IsNodeExpanded = true;
            masterSuite.IsSelected = false;
            this.Suites.Add(masterSuite);
            this.SelectPreviouslySelectedSuite(this.Suites, this.selectedSuiteId);
        }      

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">The old view model.</param>
        public TestCasesInitialViewModel(TestCasesInitialViewModel viewModel) : this()
        {
            this.InitialViewFilters = viewModel.InitialViewFilters;
            this.HideAutomated = viewModel.HideAutomated;
            this.UpdateSuites(viewModel.Suites, this.Suites);            
        }

        /// <summary>
        /// Gets or sets the suites.
        /// </summary>
        /// <value>
        /// The suites.
        /// </value>
        public ObservableCollection<Suite> Suites { get; set; }

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
        /// <param name="testCases">The test cases.</param>
        public void InitializeInitialTestCaseCollection(ICollection<TestCase> testCases)
        {
            this.InitialTestCaseCollection.Clear();
            foreach (var currentTestCase in testCases)
            {
                this.InitialTestCaseCollection.Add(currentTestCase);
            }
        }

        /// <summary>
        /// Determines whether [is there subnode selected] [the specified suites].
        /// </summary>
        /// <param name="suites">The suites.</param>
        /// <returns>is there subnode selected</returns>
        public bool IsThereSubnodeSelected(ObservableCollection<Suite> suites)
        {
            bool result = false;
            foreach (Suite currentSuite in suites)
            {                
                if (currentSuite.IsSelected && currentSuite.Id != -1)
                {
                    result = true;
                    break;
                }
                if (currentSuite.SubSuites != null && currentSuite.SubSuites.Count > 0)
                {
                    result = this.IsThereSubnodeSelected(currentSuite.SubSuites);
                    if (result)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Initializes the filters.
        /// </summary>
        private void InitializeFilters()
        {
            this.InitialViewFilters = new InitialViewFilters();
            this.InitialViewFilters.SuiteFilter = RegistryManager.GetSuiteFilter();
            if (this.InitialViewFilters.SuiteFilter != string.Empty)
            {
                this.InitialViewFilters.IsSuiteTextSet = true;
            }
        }

        /// <summary>
        /// Updates the suites.
        /// </summary>
        /// <param name="oldSuites">The old suites.</param>
        /// <param name="newSuites">The new suites.</param>
        private void UpdateSuites(ObservableCollection<Suite> oldSuites, ObservableCollection<Suite> newSuites)
        {
            foreach (Suite currentOldSuite in oldSuites)
            {
                foreach (Suite currentSuite in newSuites)
                {
                    if (currentOldSuite.Id.Equals(currentSuite.Id))
                    {
                        currentSuite.IsNodeExpanded = currentOldSuite.IsNodeExpanded;
                        currentSuite.IsSelected = currentOldSuite.IsSelected;       
                    }
                    if (currentSuite.SubSuites != null && currentSuite.SubSuites.Count > 0 && currentOldSuite.SubSuites != null && currentOldSuite.SubSuites.Count > 0)
                    {
                        this.UpdateSuites(currentOldSuite.SubSuites, currentSuite.SubSuites);
                    }
                }
            }
        }

        /// <summary>
        /// Selects the previously selected suite.
        /// </summary>
        /// <param name="suites">The suites.</param>
        /// <param name="selectedSuiteId">The selected suite unique identifier.</param>
        private void SelectPreviouslySelectedSuite(ObservableCollection<Suite> suites, int selectedSuiteId)
        {
            foreach (Suite currentSuite in suites)
            {
                if (currentSuite.Id.Equals(selectedSuiteId))
                {
                    currentSuite.IsSelected = true;
                    this.ExpandParent(currentSuite);
                    return;
                }
                if (currentSuite.SubSuites != null && currentSuite.SubSuites.Count > 0)
                {
                    this.SelectPreviouslySelectedSuite(currentSuite.SubSuites, selectedSuiteId);
                }
            }
        }

        /// <summary>
        /// Expands the parent.
        /// </summary>
        /// <param name="currentSuite">The current suite.</param>
        private void ExpandParent(Suite currentSuite)
        {
            if (currentSuite.Parent != null)
            {
                currentSuite.Parent.IsNodeExpanded = true;
                this.ExpandParent(currentSuite.Parent);
            }
        }
    }
}