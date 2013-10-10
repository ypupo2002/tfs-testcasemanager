// <copyright file="TestCasesBatchDuplicateViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Documents;
    using Microsoft.TeamFoundation.Framework.Client;
    using Microsoft.TeamFoundation.Framework.Common;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains methods and properties related to the TestCasesBatchDuplicate View
    /// </summary>
    public class TestCasesBatchDuplicateViewModel : BaseNotifyPropertyChanged
    { 
        /// <summary>
        /// The test cases count after filtering
        /// </summary>
        private string testCasesCount;

        /// <summary>
        /// The selected test case count
        /// </summary>
        private string selectedTestCasesCount;

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
            this.ReplaceContext = new ReplaceContext();
            this.ReplaceContext.SelectedSuite = this.ObservableTestSuites[0];
            if (this.ObservableTeamFoundationIdentityNames.Count > 0)
            {
                this.ReplaceContext.SelectedTeamFoundationIdentityName = this.ObservableTeamFoundationIdentityNames[0];
            }            
            this.SelectedTestCasesCount = "0";   
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesBatchDuplicateViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">The old view model.</param>
        public TestCasesBatchDuplicateViewModel(TestCasesBatchDuplicateViewModel viewModel) : this()
        {
            this.InitialViewFilters = viewModel.InitialViewFilters;
            this.ReplaceContext = viewModel.ReplaceContext;
            this.ReplaceContext.SelectedSuite = this.ObservableTestSuites[0];
            if (this.ObservableTeamFoundationIdentityNames.Count > 0)
            {
                this.ReplaceContext.SelectedTeamFoundationIdentityName = this.ObservableTeamFoundationIdentityNames[0];
            }
        }

        /// <summary>
        /// Gets or sets the replace context.
        /// </summary>
        /// <value>
        /// The replace context.
        /// </value>
        public ReplaceContext ReplaceContext { get; set; }

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
        /// Gets or sets the observable test suites used in the suite drop down.
        /// </summary>
        /// <value>
        /// The observable test suites.
        /// </value>
        public ObservableCollection<ITestSuiteBase> ObservableTestSuites { get; set; }

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
        public ObservableCollection<TeamFoundationIdentityName> ObservableTeamFoundationIdentityNames { get; set; }

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
                (t.ITestCase != null) &&
                (shouldSetTextFilter ? (t.ITestCase.Title.ToLower().Contains(titleFilter)) : true) &&
                (this.FilterTestCasesBySuite(shouldSetSuiteFilter, suiteFilter, t)) &&
                (shouldSetPriorityFilter ? t.Priority.ToString().ToLower().Contains(priorityFilter) : true) &&
                (shouldSetAssignedToFilter ? t.TeamFoundationIdentityName.DisplayName.ToLower().Contains(assignedToFilter) : true)
                ).ToList();
            this.ObservableTestCases.Clear();
            filteredList.ForEach(x => this.ObservableTestCases.Add(x));
            this.TestCasesCount = filteredList.Count.ToString();
        }

        /// <summary>
        /// Finds the and replace information test case.
        /// </summary>
        /// <returns></returns>
        public int FindAndReplaceInTestCase()
        {
            int replacedCount = 0;
            for (int i = 0; i < this.ReplaceContext.SelectedTestCases.Count; i++)
            {
                this.FindAndReplaceInTestCaseInternal(ReplaceContext.SelectedTestCases[i]);
                replacedCount++;
            }

            return replacedCount;
        }

        /// <summary>
        /// Duplicates the test case.
        /// </summary>
        /// <returns></returns>
        public int DuplicateTestCase()
        {
            int duplicatedCount = 0;
            foreach (TestCase currentSelectedTestCase in this.ReplaceContext.SelectedTestCases)
            {
                this.DuplicateTestCaseInternal(currentSelectedTestCase);
                duplicatedCount++;
            }

            return duplicatedCount;
        }

        /// <summary>
        /// Duplicates the test case.
        /// </summary>
        /// <param name="testCaseToBeDuplicated">The test case to be duplicated.</param>
        private void DuplicateTestCaseInternal(TestCase testCaseToBeDuplicated)
        {
            List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(testCaseToBeDuplicated.ITestCase.Actions.ToList());
            ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Create();
            TestCase currentTestCase = new TestCase(testCaseCore, testCaseToBeDuplicated.ITestSuiteBase);

            currentTestCase.ITestCase.Area = testCaseToBeDuplicated.ITestCase.Area;
            if (this.ReplaceContext.ReplaceInTitles)
            {
                this.ReplaceTestCaseTitle(currentTestCase);
            }
            else
            {
                currentTestCase.ITestCase.Title = testCaseToBeDuplicated.ITestCase.Title;
            }
            this.ChangeTestCasePriority(currentTestCase);
            this.ChangeTestCaseOwner(currentTestCase);
            currentTestCase.ITestCase.Priority = testCaseToBeDuplicated.ITestCase.Priority;
            this.ReplaceStepsInTestCase(currentTestCase, testSteps);

            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();

            this.AddTestCaseToSuite(currentTestCase);
        }

        /// <summary>
        /// Adds the test case automatic suite.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        private void AddTestCaseToSuite(TestCase currentTestCase)
        {
            var newSuite = TestSuiteManager.GetTestSuiteByName(this.ReplaceContext.SelectedSuite.Title);
            newSuite.AddTestCase(currentTestCase.ITestCase);
        }

        /// <summary>
        /// Finds the and replace information test case.
        /// </summary>
        /// <param name="testCaseToReplaceIn">The test case to replace in.</param>
        private void FindAndReplaceInTestCaseInternal(TestCase testCaseToReplaceIn)
        {
            TestCase currentTestCase = testCaseToReplaceIn;
            currentTestCase.ITestCase = ExecutionContext.TestManagementTeamProject.TestCases.Find(testCaseToReplaceIn.ITestCase.Id);
            List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(currentTestCase.ITestCase.Actions.ToList());
            this.ReplaceTestCaseTitle(currentTestCase);
            this.ChangeTestCasePriority(currentTestCase);
            this.ChangeTestCaseOwner(currentTestCase);
            this.ReplaceStepsInTestCase(currentTestCase,testSteps);

            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();
        }

        /// <summary>
        /// Replaces the test case title.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        private void ReplaceTestCaseTitle(TestCase currentTestCase)
        {
            if (this.ReplaceContext.ReplaceInTitles)
            {
                string newTitle = currentTestCase.ITestCase.Title.ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                currentTestCase.ITestCase.Title = newTitle;
            }
        }

        /// <summary>
        /// Changes the test case owner.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        private void ChangeTestCaseOwner(TestCase currentTestCase)
        {
            if (this.ReplaceContext.ChangeOwner && this.ReplaceContext.SelectedTeamFoundationIdentityName != null)
            {
                var identity = ExecutionContext.TestManagementTeamProject.TfsIdentityStore.FindByTeamFoundationId(this.ReplaceContext.SelectedTeamFoundationIdentityName.TeamFoundationId);
                currentTestCase.ITestCase.Owner = identity;
            }
        }

        /// <summary>
        /// Changes the test case priority.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        private void ChangeTestCasePriority(TestCase currentTestCase)
        {
            if (this.ReplaceContext.ChangePriorities)
            {
                currentTestCase.ITestCase.Priority = (int)this.ReplaceContext.SelectedPriority;
            }
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
            this.InitialViewFilters = new InitialViewFilters();
            this.ObservableTeamFoundationIdentityNames = new ObservableCollection<TeamFoundationIdentityName>();
        }


        /// <summary>
        /// Initializes the team foundation identity names.
        /// </summary>
        private void InitializeTeamFoundationIdentityNames()
        {
            this.ObservableTeamFoundationIdentityNames = new ObservableCollection<TeamFoundationIdentityName>();
            ExecutionContext.TestManagementTeamProject.TfsIdentityStore.Refresh();
            ITestManagementService testManagementService = (ITestManagementService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ITestManagementService));
            foreach (string currentProjectName in this.GetTeamProjectNamesUsingVcs())
            {
                var project = testManagementService.GetTeamProject(currentProjectName);
                foreach (var member in project.TfsIdentityStore.AllUserIdentities)
                {
                    if (member.TeamFoundationId != default(Guid) && !String.IsNullOrEmpty(member.DisplayName) && member.DisplayName.Contains(" ") && this.ObservableTeamFoundationIdentityNames.Where(x => x.TeamFoundationId == member.TeamFoundationId).ToArray().Count() == 0)
                    {
                        this.ObservableTeamFoundationIdentityNames.Add(new TeamFoundationIdentityName(member.TeamFoundationId, member.DisplayName));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the team project names using VCS.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetTeamProjectNamesUsingVcs()
        {
            ReadOnlyCollection<CatalogNode> projectNodes = ExecutionContext.TfsTeamProjectCollection.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.TeamProject }, false, CatalogQueryOptions.None);

            foreach (var tp in projectNodes)
            {
                yield return tp.Resource.DisplayName;
            }
        }

        /// <summary>
        /// Replaces the test steps information in specific test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="testSteps">The test steps.</param>
        private void ReplaceStepsInTestCase(TestCase testCase, List<TestStep> testSteps)
        {
            if (this.ReplaceContext.ReplaceSharedSteps || this.ReplaceContext.ReplaceInTestSteps)
            {
                testCase.ITestCase.Actions.Clear();
                List<Guid> addedSharedStepGuids = new List<Guid>();

                foreach (TestStep currentStep in testSteps)
                {
                    if (currentStep.IsShared && !addedSharedStepGuids.Contains(currentStep.TestStepGuid) && this.ReplaceContext.ReplaceSharedSteps)
                    {
                       
                        if (this.ReplaceContext.ReplaceSharedSteps)
                        {
                            List<int> newSharedStepIds = GetNewSharedStepIds(currentStep.SharedStepId, this.ReplaceContext.ObservableSharedStepIdReplacePairs);
                            foreach (int currentId in newSharedStepIds)
                            {
                                this.AddNewSharedStepInternal(testCase, addedSharedStepGuids, currentStep, currentId);
                            }
                        }
                        else
                        {
                            this.AddNewSharedStepInternal(testCase, addedSharedStepGuids, currentStep, currentStep.SharedStepId);
                        }
                    }
                    else if (!currentStep.IsShared)
                    {
                        ITestStep testStepCore = testCase.ITestCase.CreateTestStep();
                        if (this.ReplaceContext.ReplaceInTestSteps)
                        {
                            testStepCore.Title = currentStep.ActionTitle.ToString().ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                            testStepCore.ExpectedResult = currentStep.ActionExpectedResult.ToString().ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                        }
                        else
                        {
                            testStepCore.Title = currentStep.ActionTitle;
                            testStepCore.ExpectedResult = currentStep.ActionExpectedResult;
                        }
                        testCase.ITestCase.Actions.Add(testStepCore);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the new shared step internal.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="addedSharedStepGuids">The added shared step guids.</param>
        /// <param name="currentStep">The current step.</param>
        private void AddNewSharedStepInternal(TestCase testCase, List<Guid> addedSharedStepGuids, TestStep currentStep, int sharedStepId)
        {
            ISharedStep sharedStep = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(sharedStepId);
            ISharedStepReference sharedStepReferenceCore = testCase.ITestCase.CreateSharedStepReference();
            sharedStepReferenceCore.SharedStepId = sharedStep.Id;
            testCase.ITestCase.Actions.Add(sharedStepReferenceCore);
            addedSharedStepGuids.Add(currentStep.TestStepGuid);
        }

        /// <summary>
        /// Ares all shared step ids valid.
        /// </summary>
        /// <returns></returns>
        public bool AreAllSharedStepIdsValid()
        {
            bool result = true;
            string regexPattern = @"^[0-9,]$";
            foreach (SharedStepIdReplacePair currentSharedStepIdReplacePair in this.ReplaceContext.ObservableSharedStepIdReplacePairs)
            {
                try
                {
                    if (currentSharedStepIdReplacePair.OldSharedStepId == 0 || Regex.IsMatch(currentSharedStepIdReplacePair.NewSharedStepIds, regexPattern))
                    {
                        continue;
                    }
                    ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentSharedStepIdReplacePair.OldSharedStepId);
                    List<int> newSharedStepIds = GetNewSharedStepIdsFromString(currentSharedStepIdReplacePair.NewSharedStepIds);
                    foreach (int currentId in newSharedStepIds)
                    {
                        ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentId);
                    }                    
                }
                catch
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the new shared step unique identifier.
        /// </summary>
        /// <param name="currentSharedStepId">The current shared step unique identifier.</param>
        /// <param name="sharedStepIdReplacePairs">The shared steps replace pairs.</param>
        /// <returns>new shared step id</returns>
        private List<int> GetNewSharedStepIds(int currentSharedStepId, ICollection<SharedStepIdReplacePair> sharedStepIdReplacePairs)
        {
            string newSharedStepIds = String.Empty;
            foreach (SharedStepIdReplacePair currentPair in sharedStepIdReplacePairs)
            {
                if (currentSharedStepId.Equals(currentPair.OldSharedStepId))
                {
                    newSharedStepIds = currentPair.NewSharedStepIds;
                    break;
                }
            }
            List<int> sharedStepIds = this.GetNewSharedStepIdsFromString(newSharedStepIds);

            return sharedStepIds;
        }

        private List<int> GetNewSharedStepIdsFromString(string newSharedStepIds)
        {
            string[] sharedStepIdsStrs = newSharedStepIds.Split(',');
            List<int> sharedStepIds = new List<int>();
            foreach (var current in sharedStepIdsStrs)
            {
                sharedStepIds.Add(int.Parse(current));
            }

            return sharedStepIds;
        }

        /// <summary>
        /// Filters the test cases by suite.
        /// </summary>
        /// <param name="shouldSetSuiteFilter">if set to <c>true</c> [should set suite filter].</param>
        /// <param name="suiteFilter">The suite filter.</param>
        /// <param name="testCase">The test case.</param>
        /// <returns>should the test case be in</returns>
        private bool FilterTestCasesBySuite(bool shouldSetSuiteFilter, string suiteFilter, TestCase testCase)
        {
            if (!shouldSetSuiteFilter)
            {
                return true;
            }
            else if (testCase.ITestSuiteBase != null)
            {
                return testCase.ITestSuiteBase.Title.ToLower().Contains(suiteFilter);
            }
            else
            {
                return false;
            }
        }
    }
}