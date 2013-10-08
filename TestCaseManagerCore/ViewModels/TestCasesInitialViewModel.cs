// <copyright file="TestCasesInitialViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Forms;
    using FirstFloor.ModernUI.Windows.Controls;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Enums;
    using TestCaseManagerCore.BusinessLogic.Managers;
    using TestCaseManagerCore.Templates;

    /// <summary>
    /// Contains methods and properties related to the TestCasesInitial View
    /// </summary>
    public class TestCasesInitialViewModel : BaseNotifyPropertyChanged
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
        /// The selected test cases count
        /// </summary>
        private string selectedTestCasesCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialViewModel"/> class.
        /// </summary>
        public TestCasesInitialViewModel()
        {
            this.InitialViewFilters = new InitialViewFilters();
            ObservableCollection<Suite> subSuites = TestSuiteManager.GetAllSuites(ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites);

            // Load last selected suite in the treeview in order to selected it again
            this.selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            List<TestCase> suiteTestCaseCollection = new List<TestCase>();
            if (this.selectedSuiteId != -1)
            {
                try
                {
                    suiteTestCaseCollection = TestCaseManager.GetAllTestCaseFromSuite(this.selectedSuiteId);
                }
                catch(NullReferenceException ex)
                {
                    // No such suite in current test project
                    // TODO: log the exception
                    if (subSuites.Count > 0)
                    {
                        suiteTestCaseCollection = TestCaseManager.GetAllTestCaseFromSuite(subSuites[0].Id);
                        this.selectedSuiteId = subSuites[0].Id;
                    }
                    else
                    {                    
                        suiteTestCaseCollection = TestCaseManager.GetAllTestCasesInTestPlan();
                    }
                }
            }
            else
            {
                suiteTestCaseCollection = TestCaseManager.GetAllTestCasesInTestPlan();
            }      
            
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            this.InitialTestCaseCollection = new ObservableCollection<TestCase>();
            suiteTestCaseCollection.ForEach(t => this.ObservableTestCases.Add(t));
            this.InitializeInitialTestCaseCollection(this.ObservableTestCases);
            this.TestCasesCount = this.ObservableTestCases.Count.ToString();
         
            this.Suites = new ObservableCollection<Suite>();

            // Add a master node which will represnt all test cases in the plan. If selected all test cases will be displayed.
            Suite masterSuite = new Suite("ALL", -1, subSuites);
            masterSuite.IsNodeExpanded = true;
            masterSuite.IsSelected = false;
            masterSuite.IsCopyEnabled = false;
            masterSuite.IsRenameEnabled = false;
            masterSuite.IsCutEnabled = false;
            masterSuite.IsRemoveEnabled = false;
            this.Suites.Add(masterSuite);
            TestSuiteManager.SetParentToAllChildrenSuites(masterSuite.SubSuites, masterSuite);
            this.SelectPreviouslySelectedSuite(this.Suites, this.selectedSuiteId);
            this.IsAfterInitialize = true;
            this.SelectedTestCasesCount = "0";
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
        /// Gets or sets a value indicating whether [is after initialize]. It will be true only right after the contructor is called. Is used to determine if the Initial filter Reset should be called.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is after initialize]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAfterInitialize { get; set; }

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
                this.NotifyPropertyChanged();
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
        /// Resets the initial filters. If its comming from another page the filter is saved but on next suite selected it will be reset.
        /// </summary>
        public void ResetInitialFilters()
        {
            if (this.IsAfterInitialize)
            {
                this.IsAfterInitialize = false;
            }
            else
            {
                this.InitialViewFilters.Reset();
            }
        }

        /// <summary>
        /// Exports the test cases.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void ExportTestCases(string fileName)
        {
            List<TestCaseFull> fullTestCases = this.GetAllFullTestCasesForObservableTestCases();
            foreach (TestCaseFull currentFullTestCase in fullTestCases)
            {
                TestStepManager.UpdateGenericSharedSteps(currentFullTestCase.TestSteps);
            }
            
            HtmlTestCaseExportTemplate htmlTestCaseExportTemplate = new HtmlTestCaseExportTemplate();
            htmlTestCaseExportTemplate.Session = new Dictionary<string, object>();
            htmlTestCaseExportTemplate.Session.Add("FullTestCases", fullTestCases);
            htmlTestCaseExportTemplate.Initialize();
            string currentSvcFileContent = htmlTestCaseExportTemplate.TransformText();
            StreamWriter writer = new StreamWriter(fileName);
            using (writer)
            {
                writer.WriteLine(currentSvcFileContent);
            }
        }      

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        public void FilterTestCases()
        {
            bool shouldSetIdFilter = InitialViewFilters.IsIdTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.IdFilter);
            string idFilter = this.InitialViewFilters.IdFilter;
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
                (shouldSetIdFilter ? (t.ITestCase.Id.ToString().Contains(idFilter)) : true) &&
                (shouldSetTextFilter ? (t.ITestCase.Title.ToLower().Contains(titleFilter)) : true) &&
                (this.FilterTestCasesBySuite(shouldSetSuiteFilter, suiteFilter, t)) &&
                (shouldSetPriorityFilter ? t.Priority.ToString().ToLower().Contains(priorityFilter) : true) &&
                (t.TeamFoundationIdentityName != null && shouldSetAssignedToFilter ? t.TeamFoundationIdentityName.DisplayName.ToLower().Contains(assignedToFilter) : true) &&
                (!this.HideAutomated.Equals(t.ITestCase.IsAutomated) || !this.HideAutomated)
                ).ToList();
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
        /// Removes the test case from suite.
        /// </summary>
        /// <param name="testCaseToBeRemoved">The test case to be removed.</param>
        public void RemoveTestCaseFromSuite(TestCase testCaseToBeRemoved)
        {
            TestSuiteManager.RemoveTestCase(testCaseToBeRemoved);            
            this.ObservableTestCases.Remove(testCaseToBeRemoved);
            this.InitialTestCaseCollection.Remove(testCaseToBeRemoved);
        }

        /// <summary>
        /// Renames the suite information observable collection.
        /// </summary>
        /// <param name="suites">The suites.</param>
        /// <param name="selectedSuiteId">The selected suite unique identifier.</param>
        /// <param name="newTitle">The new title.</param>
        public void RenameSuiteInObservableCollection(ObservableCollection<Suite> suites, int selectedSuiteId, string newTitle)
        {
            foreach (Suite currentSuite in suites)
            {
                if (currentSuite.Id.Equals(selectedSuiteId))
                {
                    currentSuite.Title = newTitle;
                    return;
                }
                if (currentSuite.SubSuites != null && currentSuite.SubSuites.Count > 0)
                {
                    this.RenameSuiteInObservableCollection(currentSuite.SubSuites, selectedSuiteId, newTitle);
                }
            }
        }

        /// <summary>
        /// Gets the suite by unique identifier.
        /// </summary>
        /// <param name="suites">The suites.</param>
        /// <param name="selectedSuiteId">The selected suite unique identifier.</param>
        /// <returns>the suite</returns>
        public Suite GetSuiteById(ObservableCollection<Suite> suites, int? selectedSuiteId)
        {
            Suite result = null;
            foreach (Suite currentSuite in suites)
            {
                if (currentSuite.Id.Equals(selectedSuiteId))
                {
                    result = currentSuite;
                    break;
                }
                if (currentSuite.SubSuites != null && currentSuite.SubSuites.Count > 0)
                {
                    result = this.GetSuiteById(currentSuite.SubSuites, selectedSuiteId);
                    if (result != null)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Adds the child suite to observable collection.
        /// </summary>
        /// <param name="suites">The suites.</param>
        /// <param name="selectedSuiteId">The selected suite unique identifier.</param>
        /// <param name="newSuiteId">The new suite unique identifier.</param>
        public void AddChildSuiteObservableCollection(ObservableCollection<Suite> suites, int selectedSuiteId, int newSuiteId)
        {
            foreach (Suite currentSuite in suites)
            {
                if (currentSuite.Id.Equals(selectedSuiteId))
                {
                    ITestSuiteBase newSuiteCore = ExecutionContext.TestManagementTeamProject.TestSuites.Find(newSuiteId);
                    Suite newSuite = new Suite(newSuiteCore.Title, newSuiteCore.Id, null, currentSuite);
                    newSuite.IsSelected = true;
                    if (currentSuite.SubSuites == null)
                    {
                        currentSuite.SubSuites = new ObservableCollection<Suite>();
                    }
                    currentSuite.SubSuites.Add(newSuite);
                    currentSuite.IsSelected = false;
                    currentSuite.IsNodeExpanded = true;
                    return;
                }
                if (currentSuite.SubSuites != null && currentSuite.SubSuites.Count > 0)
                {
                    this.AddChildSuiteObservableCollection(currentSuite.SubSuites, selectedSuiteId, newSuiteId);
                }
            }
        }

        /// <summary>
        /// Pastes the suite to parent suite.
        /// </summary>
        /// <param name="parentSuite">The parent suite.</param>
        /// <param name="clipboardSuite">The clipboard suite.</param>
        public void CopyPasteSuiteToParentSuite(Suite parentSuite, Suite clipboardSuite)
        {
            try
            {
                TestSuiteManager.PasteSuiteToParent(parentSuite.Id, clipboardSuite.Id, ClipBoardCommand.Copy);
            }
            catch (TestManagementValidationException ex)
            {
                if (ex.Message.Equals("This item has already been added. A test suite cannot contain a duplicate test case or test suite."))
                {
                    ModernDialog.ShowMessage(ex.Message, "Warrning!", MessageBoxButton.OK);
                    return;

                    // TODO: Log the exception
                }
            }
            Suite suiteToBePasted = (Suite)clipboardSuite.Clone();
            suiteToBePasted.Parent = parentSuite;
            parentSuite.SubSuites.Add(suiteToBePasted);
            parentSuite.IsSelected = true;
            parentSuite.IsNodeExpanded = true;
        }

        /// <summary>
        /// Cuts the paste suite to parent suite.
        /// </summary>
        /// <param name="parentSuite">The parent suite.</param>
        /// <param name="clipboardSuite">The clipboard suite.</param>
        public void CutPasteSuiteToParentSuite(Suite parentSuite, Suite clipboardSuite)
        {
            TestSuiteManager.PasteSuiteToParent(parentSuite.Id, clipboardSuite.Id, ClipBoardCommand.Cut);
            if (clipboardSuite.Parent != null)
            {
                this.DeleteSuiteObservableCollection(this.Suites, clipboardSuite.Id);
            }
          
            Suite suiteToBePasted = (Suite)clipboardSuite.Clone();
            suiteToBePasted.Parent = parentSuite;
            parentSuite.SubSuites.Add(suiteToBePasted);
            parentSuite.IsSelected = true;
            parentSuite.IsNodeExpanded = true;
            System.Windows.Clipboard.Clear();
        }

        /// <summary>
        /// Deletes the suite from the suite observable collection.
        /// </summary>
        /// <param name="suites">The suites.</param>
        /// <param name="suiteForDeleteId">The suite for delete unique identifier.</param>
        public void DeleteSuiteObservableCollection(ObservableCollection<Suite> suites, int suiteForDeleteId)
        {
            Suite[] suitesCopy = new Suite[suites.Count];
            suites.CopyTo(suitesCopy, 0);

            for (int i = 0; i < suitesCopy.Length; i++)
            {
                if (suitesCopy[i].Id.Equals(suiteForDeleteId) && suitesCopy[i].Parent != null)
                {
                    suitesCopy[i].Parent.SubSuites.Remove(suitesCopy[i]);
                }
                if (suitesCopy[i].SubSuites != null && suitesCopy[i].SubSuites.Count > 0)
                {
                    this.DeleteSuiteObservableCollection(suitesCopy[i].SubSuites, suiteForDeleteId);
                }
            }
        }

        /// <summary>
        /// Adds the test cases automatic observable collection.
        /// </summary>
        /// <param name="list">The list.</param>
        public void AddTestCasesToObservableCollection(Suite suiteToPasteIn, int? parentSuiteId)
        {
            suiteToPasteIn.IsSelected = true;
            if (parentSuiteId != null)
            {
                Suite parentSuite = this.GetSuiteById(this.Suites, parentSuiteId);
                parentSuite.IsSelected = false;
            }
           
            List<TestCase> testCasesList = TestCaseManager.GetAllTestCaseFromSuite(suiteToPasteIn.Id);
            this.ObservableTestCases.Clear();
            testCasesList.ForEach(t => this.ObservableTestCases.Add(t));
            this.InitializeInitialTestCaseCollection(this.ObservableTestCases);
        }

        /// <summary>
        /// Gets all full test cases for observable test cases.
        /// </summary>
        /// <returns></returns>
        private List<TestCaseFull> GetAllFullTestCasesForObservableTestCases()
        {
            List<TestCaseFull> fullTestCases = new List<TestCaseFull>();
            foreach (TestCase currentTestCase in this.ObservableTestCases)
            {
                List<TestStep> currentTestSteps = TestStepManager.GetTestStepsFromTestActions(currentTestCase.ITestCase.Actions);
                fullTestCases.Add(new TestCaseFull(currentTestCase, currentTestSteps));
            }

            return fullTestCases;
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