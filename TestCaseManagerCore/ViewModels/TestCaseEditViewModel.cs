// <copyright file="TestCaseEditViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Xml;
    using Microsoft.TeamFoundation.Server;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains methods and properties related to the TestCaseEdit View
    /// </summary>
    public class TestCaseEditViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The shared step search default text
        /// </summary>
        public const string SharedStepSearchDefaultText = "Search in Shared";

        /// <summary>
        /// The action default text
        /// </summary>
        public const string ActionDefaultText = "Action";

        /// <summary>
        /// The expected result default text
        /// </summary>
        public const string ExpectedResultDefaultText = "Expected Result";

        /// <summary>
        /// The is action text set
        /// </summary>
        public bool IsActionTextSet;

        /// <summary>
        /// The is expected result text set
        /// </summary>
        public bool IsExpectedResultTextSet;

        /// <summary>
        /// The is shared step search text set
        /// </summary>
        public bool IsSharedStepSearchTextSet;

        /// <summary>
        /// The page title
        /// </summary>
        private string pageTitle;

        /// <summary>
        /// Gets or sets the initial shared step collection.
        /// </summary>
        /// <value>
        /// The initial shared step collection.
        /// </value>
        private ObservableCollection<SharedStep> initialSharedStepCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEditViewModel"/> class.
        /// </summary>
        /// <param name="editViewContext">The edit view context.</param>
        public TestCaseEditViewModel(EditViewContext editViewContext)
        {
            this.EditViewContext = editViewContext;
            this.Areas = this.GetProjectAreas();
            this.ObservableTestSteps = new ObservableCollection<TestStep>();

            if (!this.EditViewContext.IsSharedStep)
            {
                this.ShowTestCaseSpecificFields = true;
                ITestSuiteBase testSuiteBaseCore = TestSuiteManager.GetTestSuiteById(this.EditViewContext.TestSuiteId);
                if (this.EditViewContext.CreateNew && !this.EditViewContext.Duplicate)
                {
                    ITestCase newTestCase = ExecutionContext.TestManagementTeamProject.TestCases.Create();
                    this.TestCase = new TestCase(newTestCase, testSuiteBaseCore);
                }
                else
                {
                    ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Find(this.EditViewContext.TestCaseId);
                    this.TestCase = new TestCase(testCaseCore, testSuiteBaseCore);
                }
                this.ObservableSharedSteps = new ObservableCollection<SharedStep>();
                this.InitializeInitialSharedStepCollection();                
                this.InitializeObservableSharedSteps();
                this.InitializeTestCaseTestStepsFromITestCaseActions();
                this.EditViewContext.ComesFromSharedStep = false;
                this.AssociatedAutomation = this.TestCase.ITestCase.GetAssociatedAutomation();
                this.TestBase = this.TestCase;
            }
            else
            {
                if (this.EditViewContext.CreateNew && !this.EditViewContext.Duplicate)
                {
                    ISharedStep currentSharedStepCore = ExecutionContext.TestManagementTeamProject.SharedSteps.Create();
                    this.SharedStep = new SharedStep(currentSharedStepCore);
                }
                else
                {
                    SharedStep currentSharedStep = SharedStepManager.GetSharedStepById(this.EditViewContext.SharedStepId);
                    this.SharedStep = currentSharedStep;
                }

                List<TestStep> innerTestSteps = TestStepManager.GetAllTestStepsInSharedStep(this.SharedStep.ISharedStep, false);
                this.AddTestStepsToObservableCollection(innerTestSteps);
                //this.TestCaseIdLabel = this.SharedStep.ISharedStep.Id.ToString();
                this.ShowTestCaseSpecificFields = false;
                this.TestBase = this.SharedStep;
            }
            this.InitializeIdLabelFromTestBase(this.EditViewContext.CreateNew, this.EditViewContext.Duplicate);
            this.InitializePageTitle();
        }      

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        /// <value>
        /// The test case.
        /// </value>
        public TestCase TestCase { get; set; }

        /// <summary>
        /// Gets or sets the shared step.
        /// </summary>
        /// <value>
        /// The shared step.
        /// </value>
        public SharedStep SharedStep { get; set; }

        /// <summary>
        /// Gets or sets the edit view context.
        /// </summary>
        /// <value>
        /// The edit view context.
        /// </value>
        public EditViewContext EditViewContext { get; set; }

        /// <summary>
        /// Gets or sets the test base.
        /// </summary>
        /// <value>
        /// The test base.
        /// </value>
        public TestBase TestBase { get; set; }

        /// <summary>
        /// Gets or sets the test case unique identifier label.
        /// </summary>
        /// <value>
        /// The test case unique identifier label.
        /// </value>
        public string TestCaseIdLabel { get; set; }

        /// <summary>
        /// Gets or sets the observable test steps.
        /// </summary>
        /// <value>
        /// The observable test steps.
        /// </value>
        public ObservableCollection<TestStep> ObservableTestSteps { get; set; }

        /// <summary>
        /// Gets or sets the test case test steps. Used to preserve the current state of the test case test steps if the user edit shared step.
        /// </summary>
        /// <value>
        /// The test case test steps.
        /// </value>
        public List<TestStep> TestCaseTestSteps { get; set; }

        /// <summary>
        /// Gets or sets the observable shared steps.
        /// </summary>
        /// <value>
        /// The observable shared steps.
        /// </value>
        public ObservableCollection<SharedStep> ObservableSharedSteps { get; set; }

        /// <summary>
        /// Gets or sets the test suite list.
        /// </summary>
        /// <value>
        /// The test suite list.
        /// </value>
        public List<ITestSuiteBase> TestSuiteList { get; set; }

        /// <summary>
        /// Gets or sets the associated automation.
        /// </summary>
        /// <value>
        /// The associated automation.
        /// </value>
        public AssociatedAutomation AssociatedAutomation { get; set; }

        /// <summary>
        /// Gets or sets the areas.
        /// </summary>
        /// <value>
        /// The areas.
        /// </value>
        public List<string> Areas { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show test case specific fields].
        /// </summary>
        /// <value>
        /// <c>true</c> if [show test case specific fields]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowTestCaseSpecificFields { get; set; }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string PageTitle
        {
            get
            {
                return this.pageTitle;
            }

            set
            {
                this.pageTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes the page title.
        /// </summary>
        private void InitializePageTitle()
        {
            if (!this.EditViewContext.IsSharedStep && this.EditViewContext.CreateNew && !this.EditViewContext.Duplicate)
            {
                this.PageTitle = "Create New";
            }
            else if (!this.EditViewContext.IsSharedStep && this.EditViewContext.CreateNew && this.EditViewContext.Duplicate)
            {
                this.PageTitle = "Duplicate";
            }
            else if (this.EditViewContext.IsSharedStep && this.EditViewContext.CreateNew && !this.EditViewContext.Duplicate)
            {
                this.PageTitle = "Create New Shared Step";
            }
            else if (this.EditViewContext.IsSharedStep && this.EditViewContext.CreateNew && this.EditViewContext.Duplicate)
            {
                this.PageTitle = "Duplicate Shared Step";
            }
            else if (this.EditViewContext.IsSharedStep)
            {
                this.PageTitle = "Edit Shared Step";
            }
            else
            {
                this.PageTitle = "Edit";
            }
        }

        /// <summary>
        /// Copies the current test steps automatic copy.
        /// </summary>
        public void CopyCurrentTestStepsToCopy()
        {
            this.TestCaseTestSteps = new List<TestStep>();
            foreach (TestStep currentTestStep in this.ObservableTestSteps)
            {
                this.TestCaseTestSteps.Add(currentTestStep);
            }
        }

        /// <summary>
        /// Initializes the test steps from copy.
        /// </summary>
        public void InitializeTestStepsFromCopy()
        {
            foreach (TestStep currentTestStep in this.TestCaseTestSteps)
            {
                this.ObservableTestSteps.Add(currentTestStep);
            }
        }

        /// <summary>
        /// Reinitializes the shared step collection.
        /// </summary>
        public void ReinitializeSharedStepCollection()
        {
            this.ObservableSharedSteps.Clear();
            foreach (var item in this.initialSharedStepCollection)
            {
                this.ObservableSharedSteps.Add(item);
            }
        }      

        /// <summary>
        /// Gets the step title.
        /// </summary>
        /// <param name="stepTitle">The step title.</param>
        /// <returns>the test step title</returns>
        public string GetStepTitle(string stepTitle)
        {
            string finalStepTitle = stepTitle;
            if (finalStepTitle.Equals(ActionDefaultText))
            {
                finalStepTitle = string.Empty;
            }

            return finalStepTitle;
        }

        /// <summary>
        /// Gets the expected result.
        /// </summary>
        /// <param name="expectedResult">The expected result.</param>
        /// <returns>the expected result</returns>
        public string GetExpectedResult(string expectedResult)
        {
            string finalExpectedResult = expectedResult;
            if (finalExpectedResult.Equals(ExpectedResultDefaultText))
            {
                finalExpectedResult = string.Empty;
            }

            return finalExpectedResult;
        }

        /// <summary>
        /// Deletes the cut test steps.
        /// </summary>
        /// <param name="cutTestSteps">The cut test steps.</param>
        public void DeleteCutTestSteps(List<TestStep> cutTestSteps)
        {          
            foreach (TestStep currentTestStepToBeRemoved in cutTestSteps)
            {
                for (int i = 0; i < this.ObservableTestSteps.Count; i++)
                {
                    int index = i - 1;

                    // If the step to be removed is initial we set -2 for index in order the Insert Operation to be completed successfully. 
                    // If index == -2 the insert function will insert the test step to the beginning of the collection
                    if (index == -1)
                    {
                        index--;
                    }
                    if (this.ObservableTestSteps[i].TestStepGuid.Equals(currentTestStepToBeRemoved.TestStepGuid))
                    {
                        this.RemoveTestStepFromObservableCollection(ObservableTestSteps[i], index);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Marks the initial steps to be removed.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>the list of test steps to be removed</returns>
        public List<TestStepFull> MarkInitialStepsToBeRemoved(IList<TestStep> selectedTestSteps)
        {
            List<TestStepFull> testStepsToBeRemoved = new List<TestStepFull>();
            foreach (TestStep currentStepToBeRemoved in selectedTestSteps)
            {
                for (int i = 0; i < this.ObservableTestSteps.Count; i++)
                {
                    if (this.ObservableTestSteps[i].TestStepGuid.Equals(currentStepToBeRemoved.TestStepGuid))
                    {
                        int index = i - 1;

                        // If the step to be removed is initial we set -2 for index in order the Insert Operation to be completed successfully. 
                        // If index == -2 the insert function will insert the test step to the beginning of the collection
                        if (index == -1)
                        {
                            index--;
                        }
                        TestStepFull currentTestStep = new TestStepFull(this.ObservableTestSteps[i], index);
                        if (!testStepsToBeRemoved.Contains(currentTestStep))
                        {
                            testStepsToBeRemoved.Add(currentTestStep);
                        }                       
                    }
                }               
            }

            return testStepsToBeRemoved;
        }

        /// <summary>
        /// Inserts the test step information to the test case.
        /// </summary>
        /// <param name="testStepToInsert">The test step to be insert.</param>
        /// <param name="selectedIndex">Index of the selected test step.</param>
        public void InsertTestStepInTestCase(TestStep testStepToInsert, int selectedIndex)
        {
            // If you delete first step and call redo operation, the step should be inserted at the beginning
            if (selectedIndex == -2)
            {
                this.ObservableTestSteps.Insert(0, testStepToInsert);
            }
            else if (selectedIndex != -1)
            {
                this.ObservableTestSteps.Insert(selectedIndex + 1, testStepToInsert);
            }
            else
            {
                this.ObservableTestSteps.Add(testStepToInsert);                 
            }
            UndoRedoManager.Instance().Push((r, i) => this.RemoveTestStepFromObservableCollection(r, i), testStepToInsert, selectedIndex);
        }

        /// <summary>
        /// Removes the test step from test steps observable collection.
        /// </summary>
        /// <param name="testStepToBeRemoved">The test step to be removed.</param>
        /// <param name="selectedIndex">Index of the selected.</param>
        public void RemoveTestStepFromObservableCollection(TestStep testStepToBeRemoved, int selectedIndex)
        {
            this.ObservableTestSteps.Remove(testStepToBeRemoved);
            UndoRedoManager.Instance().Push((r, i) => this.InsertTestStepInTestCase(r, i), testStepToBeRemoved, selectedIndex, "remove Test Step");
        }

        /// <summary>
        /// Creates the new test step collection after move up.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedCount">The count of the selected steps.</param>
        public void CreateNewTestStepCollectionAfterMoveUp(int startIndex, int selectedCount)
        {
            List<TestStep> newCollection = new List<TestStep>();
            for (int i = 0; i < startIndex - 1; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            for (int i = startIndex; i < startIndex + selectedCount; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }
            for (int i = startIndex - 1; i < startIndex; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            for (int i = startIndex + selectedCount; i < this.ObservableTestSteps.Count; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            this.ObservableTestSteps.Clear();
            newCollection.ForEach(x => this.ObservableTestSteps.Add(x));
            UndoRedoManager.Instance().Push((si, c) => this.CreateNewTestStepCollectionAfterMoveDown(si, c), startIndex - 1, selectedCount, "Move up selected test steps");
        }

        /// <summary>
        /// Creates the new test step collection after move down.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedCount">The count of the selected test steps.</param>
        public void CreateNewTestStepCollectionAfterMoveDown(int startIndex, int selectedCount)
        {
            List<TestStep> newCollection = new List<TestStep>();
            for (int i = 0; i < startIndex; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }
            newCollection.Add(this.ObservableTestSteps[startIndex + selectedCount]);
            for (int i = startIndex; i < startIndex + selectedCount; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            for (int i = startIndex + selectedCount + 1; i < this.ObservableTestSteps.Count; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            this.ObservableTestSteps.Clear();
            newCollection.ForEach(x => this.ObservableTestSteps.Add(x));
            UndoRedoManager.Instance().Push((si, c) => this.CreateNewTestStepCollectionAfterMoveUp(si, c), startIndex + 1, selectedCount, "Move down selected test steps");
        }

        /// <summary>
        /// Inserts the new shared step.
        /// </summary>
        /// <param name="currentSharedStep">The current shared step.</param>
        /// <param name="selectedIndex">Index of the selected test step.</param>
        public void InsertSharedStep(SharedStep currentSharedStep, int selectedIndex)
        {
            List<TestStep> innerTestSteps = TestStepManager.GetAllTestStepsInSharedStep(currentSharedStep.ISharedStep);
          
            int j = 0;
            for (int i = selectedIndex; i < innerTestSteps.Count + selectedIndex; i++)
            {
                this.InsertTestStepInTestCase(innerTestSteps[j], i);
                j++;
            }
        }

        /// <summary>
        /// Deletes the test step.
        /// </summary>
        /// <param name="testStepsToBeRemoved">The test steps to be removed.</param>
        public void RemoveTestSteps(List<TestStepFull> testStepsToBeRemoved)
        {
            using (new UndoTransaction("Delete all selected test steps", false))
            {
                for (int i = testStepsToBeRemoved.Count - 1; i >= 0; i--)
                {
                    this.RemoveTestStepFromObservableCollection(testStepsToBeRemoved[i], testStepsToBeRemoved[i].Index);
                }
            }           
        }

        /// <summary>
        /// Initializes the unique identifier label.
        /// </summary>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        private void InitializeIdLabelFromTestBase(bool createNew, bool duplicate)
        {
            if (duplicate || createNew)
            {
                this.TestCaseIdLabel = "*";
            }
            else
            {
                this.TestCaseIdLabel = this.TestBase.Id.ToString();
            }
        }

        /// <summary>
        /// Initializes the initial shared step collection.
        /// </summary>
        private void InitializeInitialSharedStepCollection()
        {
            this.initialSharedStepCollection = new ObservableCollection<SharedStep>();
            foreach (var currentSharedStep in this.ObservableSharedSteps)
            {
                this.initialSharedStepCollection.Add(currentSharedStep);
            }
        }

        /// <summary>
        /// Gets the project areas.
        /// </summary>
        /// <returns>list of areas names as string list</returns>
        private List<string> GetProjectAreas()
        {
            List<string> areas = new List<string>();
            ICommonStructureService css = (ICommonStructureService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ICommonStructureService));
            ProjectInfo projectInfo = css.GetProjectFromName(ExecutionContext.Preferences.TestProjectName);
            NodeInfo[] nodes = css.ListStructures(projectInfo.Uri);
            XmlElement areaTree = css.GetNodesXml(new string[] { nodes[0].Uri }, true);
            areas.Clear();
            XmlNode areaNodes = areaTree.ChildNodes[0];
            this.CreateAreasList(areaNodes, areas);

            return areas;
        }

        /// <summary>
        /// Initializes the test case test steps from attribute test case actions.
        /// </summary>
        private void InitializeTestCaseTestStepsFromITestCaseActions()
        {
            List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(this.TestCase.ITestCase.Actions);
            this.AddTestStepsToObservableCollection(testSteps);
        }

        /// <summary>
        /// Adds the test steps automatic observable collection.
        /// </summary>
        /// <param name="testSteps">The test steps.</param>
        private void AddTestStepsToObservableCollection(List<TestStep> testSteps)
        {
            foreach (var currentTestStep in testSteps)
            {
                this.ObservableTestSteps.Add(currentTestStep);
            }
        }

        /// <summary>
        /// Initializes the observable shared steps.
        /// </summary>
        private void InitializeObservableSharedSteps()
        {
            List<ISharedStep> sharedStepList = TestStepManager.GetAllSharedSteps();

            sharedStepList.ForEach(s =>
            {
                this.ObservableSharedSteps.Add(new SharedStep(s));
            });
        }

        /// <summary>
        /// Creates the areas list.
        /// </summary>
        /// <param name="xmlNode">The XML node.</param>
        /// <param name="areas">The areas.</param>
        private void CreateAreasList(XmlNode xmlNode, List<string> areas)
        {
            this.GetAreasSingleNode(xmlNode, areas);
            this.GetAreasNodes(xmlNode.ChildNodes, areas);
        }

        /// <summary>
        /// Gets the areas nodes.
        /// </summary>
        /// <param name="nodeList">The node list.</param>
        /// <param name="areas">The areas.</param>
        private void GetAreasNodes(XmlNodeList nodeList, List<string> areas)
        {
            foreach (XmlNode currentNode in nodeList)
            {
                this.GetAreasSingleNode(currentNode, areas);
            }
        }

        /// <summary>
        /// Gets the areas single node.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        /// <param name="areas">The areas.</param>
        private void GetAreasSingleNode(XmlNode currentNode, List<string> areas)
        {
            if (currentNode.Attributes.Count > 0)
            {
                string path = currentNode.Attributes["Path"].Value.TrimStart('\\').Replace("\\Area", string.Empty);
                if (!areas.Contains(path))
                {
                    areas.Add(path);
                }
            }
            if (currentNode.ChildNodes.Count != 0)
            {
                this.GetAreasNodes(currentNode.ChildNodes, areas);
            }
        }     
    }
}