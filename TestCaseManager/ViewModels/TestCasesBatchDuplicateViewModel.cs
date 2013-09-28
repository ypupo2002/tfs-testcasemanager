// <copyright file="TestCasesBatchDuplicateViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
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
using TestCaseManagerApp.BusinessLogic.Enums;

    /// <summary>
    /// Contains methods and properties related to the TestCasesBatchDuplicate View
    /// </summary>
    public class TestCasesBatchDuplicateViewModel : BaseNotifyPropertyChanged
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
        /// The change owner
        /// </summary>
        private bool changeOwner;

        /// <summary>
        /// The change priorities
        /// </summary>
        private bool changePriorities;

        /// <summary>
        /// The test cases count after filtering
        /// </summary>
        private string testCasesCount;

        /// <summary>
        /// The selected test case count
        /// </summary>
        private string selectedTestCasesCount;

        /// <summary>
        /// The selected priority
        /// </summary>
        private Priority selectedPriority;

        /// <summary>
        /// The selected team foundation identity name
        /// </summary>
        private TeamFoundationIdentityName selectedTeamFoundationIdentityName;


        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesBatchDuplicateViewModel"/> class.
        /// </summary>
        public TestCasesBatchDuplicateViewModel()
        {
            this.InitializeInnerCollections();
            this.InitializeTestCases();
            this.InitializeInitialTestCaseCollection();
            this.InitializeTestSuiteList();
            this.InitializeTeamFoundationIdentityNames();
            this.TestCasesCount = this.ObservableTestCases.Count.ToString();
            this.ReplaceInTitles = true;
            this.ReplaceInTestSteps = true;
            this.ReplaceSharedSteps = true;
            this.ChangeOwner = true;
            this.changePriorities = true;
            this.SelectedTestCasesCount = "0";   
        }

        /// <summary>
        /// Initializes the test cases.
        /// </summary>
        public void InitializeTestCases()
        {
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = TestCaseManager.GetAllTestCasesInTestPlan();
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            testCasesList.ForEach(t => this.ObservableTestCases.Add(t));
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
            this.ChangeOwner = viewModel.ChangeOwner;
            this.changePriorities = viewModel.changePriorities;
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
        /// Gets or sets the team foundation identity names.
        /// </summary>
        /// <value>
        /// The team foundation identity names.
        /// </value>
        public ObservableCollection<TeamFoundationIdentityName> TeamFoundationIdentityNames { get; set; }

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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [change priorities].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [change priorities]; otherwise, <c>false</c>.
        /// </value>
        public bool ChangePriorities
        {
            get
            {
                return this.changePriorities;
            }

            set
            {
                this.changePriorities = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [change owner].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [change owner]; otherwise, <c>false</c>.
        /// </value>
        public bool ChangeOwner
        {
            get
            {
                return this.changeOwner;
            }

            set
            {
                this.changeOwner = value;
                this.NotifyPropertyChanged();
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
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected test cases count.
        /// </summary>
        /// <value>
        /// The selected test cases count.
        /// </value>
        public string SelectedTestCasesCount
        {
            get
            {
                return this.selectedTestCasesCount;
            }

            set
            {
                this.selectedTestCasesCount = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public Priority Priority
        {
            get
            {
                return this.selectedPriority;
            }

            set
            {
                this.selectedPriority = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the name of the selected team foundation identity.
        /// </summary>
        /// <value>
        /// The name of the selected team foundation identity.
        /// </value>
        public TeamFoundationIdentityName SelectedTeamFoundationIdentityName
        {
            get
            {
                return this.selectedTeamFoundationIdentityName;
            }

            set
            {
                this.selectedTeamFoundationIdentityName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        public void FilterTestCases()
        {
            bool shouldSetTextFilter = InitialViewFilters.IsTitleTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.TitleFilter);
            string titleFilter = this.InitialViewFilters.TitleFilter.ToLower();
            bool shouldSetSuiteFilter = InitialViewFilters.IsSuiteTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.SuiteFilter);
            string suiteFilter = this.InitialViewFilters.SuiteFilter.ToLower();
            bool shouldSetPriorityFilter = InitialViewFilters.IsPriorityTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.PriorityFilter);
            string priorityFilter = this.InitialViewFilters.PriorityFilter.ToLower();
            bool shouldSetAssignedToFilter = InitialViewFilters.IsAssignedToTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.AssignedToFilter);
            string assignedToFilter = this.InitialViewFilters.AssignedToFilter.ToLower();

            var filteredList = this.InitialTestCaseCollection.Where(t =>
                (shouldSetTextFilter ? (t.ITestCase.Title.ToLower().Contains(titleFilter)) : true) &&
                (shouldSetSuiteFilter ? t.ITestSuiteBase.Title.ToLower().Contains(suiteFilter) : true) &&
                (shouldSetPriorityFilter ? t.Priority.ToString().ToLower().Contains(priorityFilter) : true) &&
                (shouldSetAssignedToFilter ? t.OwnerName.ToLower().Contains(assignedToFilter) : true)
                ).ToList();
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
            this.InitialViewFilters = new InitialViewFilters();
            this.TeamFoundationIdentityNames = new ObservableCollection<TeamFoundationIdentityName>();
        }


        /// <summary>
        /// Initializes the team foundation identity names.
        /// </summary>
        private void InitializeTeamFoundationIdentityNames()
        {
            var allUserIdentityNames = ExecutionContext.TestManagementTeamProject.TfsIdentityStore.AllUserIdentityNames;
            foreach (TeamFoundationIdentityName currentName in allUserIdentityNames)
            {
                this.TeamFoundationIdentityNames.Add(currentName);
            }
        }
    }
}