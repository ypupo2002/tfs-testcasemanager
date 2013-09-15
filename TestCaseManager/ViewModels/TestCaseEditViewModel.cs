// <copyright file="TestCaseEditViewModel.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml;
    using Microsoft.TeamFoundation.Server;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Contains methods and properties related to the TestCaseEdit View
    /// </summary>
    public class TestCaseEditViewModel
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
        /// Gets or sets the initial shared step collection.
        /// </summary>
        /// <value>
        /// The initial shared step collection.
        /// </value>
        private ObservableCollection<SharedStep> initialSharedStepCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEditViewModel"/> class.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public TestCaseEditViewModel(int testCaseId, int suiteId, bool createNew, bool duplicate)
        {
            this.Areas = this.GetProjectAreas();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            this.TestSuiteList = TestSuiteManager.GetAllTestSuitesInTestPlan();

            this.CreateNew = createNew;
            this.Duplicate = duplicate;
            this.TestActions = new List<ITestAction>();
            this.ObservableTestSteps = new ObservableCollection<TestStep>();
            this.ObservableSharedSteps = new ObservableCollection<SharedStep>();

            this.Priorities = new List<int>() { 1, 2, 3, 4 };
            this.AlreadyAddedSharedSteps = new Dictionary<int, string>();
            ITestSuiteBase testSuiteBaseCore = TestSuiteManager.GetTestSuiteById(suiteId);

            if (this.CreateNew && !this.Duplicate)
            {
                ITestCase newTestCase = ExecutionContext.TestManagementTeamProject.TestCases.Create();
                if (this.Duplicate)
                {
                    this.InitializeTestCaseWithExisting();
                }
                else
                {
                    this.TestCase = new TestCase(newTestCase, null);
                }
            }
            else
            {
                ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Find(testCaseId);
                this.TestCase = new TestCase(testCaseCore, testSuiteBaseCore);               
            }
            this.InitializeTestCaseWithExisting();
            this.InitializeInitialSharedStepCollection();
            this.AssociatedAutomation = this.TestCase.ITestCase.GetAssociatedAutomation();
            this.UpdateObservableTestSteps(this.ObservableTestSteps.ToList());
        }

        /// <summary>
        /// Gets or sets the already added shared steps.
        /// </summary>
        /// <value>
        /// The already added shared steps.
        /// </value>
        public Dictionary<int, string> AlreadyAddedSharedSteps { get; set; }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        /// <value>
        /// The test case.
        /// </value>
        public TestCase TestCase { get; set; }

        /// <summary>
        /// Gets or sets the test actions.
        /// </summary>
        /// <value>
        /// The test actions.
        /// </value>
        public List<ITestAction> TestActions { get; set; }

        /// <summary>
        /// Gets or sets the observable test steps.
        /// </summary>
        /// <value>
        /// The observable test steps.
        /// </value>
        public ObservableCollection<TestStep> ObservableTestSteps { get; set; }

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
        /// Gets or sets the priorities.
        /// </summary>
        /// <value>
        /// The priorities.
        /// </value>
        public List<int> Priorities { get; set; }

        /// <summary>
        /// Gets or sets the areas.
        /// </summary>
        /// <value>
        /// The areas.
        /// </value>
        public List<string> Areas { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [create new].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [create new]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateNew { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [duplicate].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [duplicate]; otherwise, <c>false</c>.
        /// </value>
        public bool Duplicate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is already created].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is already created]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAlreadyCreated { get; set; }

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
        /// Gets the project areas.
        /// </summary>
        /// <returns>list of areas names as string list</returns>
        public List<string> GetProjectAreas()
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
        /// Updates the observable test steps.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        public void UpdateObservableTestSteps(List<TestStep> selectedTestSteps)
        {
            foreach (TestStep currentSelectedStep in selectedTestSteps)
            {
                for (int i = 0; i < this.ObservableTestSteps.Count; i++)
                {
                    if (currentSelectedStep.ITestStep.Id.Equals(this.ObservableTestSteps[i].ITestStep.Id))
                    {
                        this.ObservableTestSteps[i] = currentSelectedStep;
                        string title = this.ObservableTestSteps[i].ITestStep.Title.ToPlainText();
                    }
                }  
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
        /// Deletes all marked steps for removal.
        /// </summary>
        /// <param name="testStepsToBeRemoved">The test steps automatic be removed.</param>
        public void DeleteAllMarkedStepsForRemoval(List<TestStep> testStepsToBeRemoved)
        {
            foreach (TestStep currentTestStepToBeRemoved in testStepsToBeRemoved)
            {
                this.ObservableTestSteps.Remove(currentTestStepToBeRemoved);
            }
        }

        /// <summary>
        /// Marks the steps to be removed.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>the list of test steps to be removed</returns>
        public List<TestStep> MarkStepsToBeRemoved(IList<TestStep> selectedTestSteps)
        {
            // All initial steps are marked for deletion (Login To Telerik) after that we trace them in all shared steps across the test case actions
            List<TestStep> testStepsToBeRemoved = this.MarkInitialStepsToBeRemoved(selectedTestSteps);
            List<TestStep> allStepsToBeRemoved = new List<TestStep>();
            foreach (TestStep currentStepToBeRemoved in testStepsToBeRemoved)
            {
                foreach (TestStep currentTestStep in this.ObservableTestSteps)
                {
                    if (currentTestStep.ITestStep.Id.Equals(currentStepToBeRemoved.ITestStep.Id))
                    {
                        allStepsToBeRemoved.Add(currentTestStep);
                    }
                }
            }
            return allStepsToBeRemoved;
        }

        /// <summary>
        /// Marks the initial steps to be removed.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>the list of test steps to be removed</returns>
        public List<TestStep> MarkInitialStepsToBeRemoved(IList<TestStep> selectedTestSteps)
        {
            List<TestStep> testStepsToBeRemoved = new List<TestStep>();
            foreach (TestStep currentStepToBeRemoved in selectedTestSteps)
            {
                testStepsToBeRemoved.Add(currentStepToBeRemoved);
            }

            return testStepsToBeRemoved;
        }

        /// <summary>
        /// Inserts the test step(title + expected result) information in shared step.
        /// </summary>
        /// <param name="selectedSharedStep">The selected shared step.</param>
        /// <param name="stepText">The step text.</param>
        /// <param name="expectedResult">The expected result.</param>
        public void InsertTestStepInSharedStep(SharedStep selectedSharedStep, string stepText, string expectedResult)
        {
            string sharedStepGuid = this.ObservableTestSteps.Where(x => x.Title.Equals(selectedSharedStep.ISharedStep.Title)).FirstOrDefault().StepGuid;

            TestStep testStepToInsert = TestStepManager.CreateNewTestStep(TestCase, stepText, expectedResult);
            testStepToInsert.IsShared = true;
            testStepToInsert.StepGuid = sharedStepGuid;
            bool shouldInsert = true;
            for (int i = this.ObservableTestSteps.Count - 1; i >= 0; i--)
            {
                if (this.ObservableTestSteps[i].StepGuid.Equals(sharedStepGuid) && shouldInsert)
                {
                    this.ObservableTestSteps.Insert(i + 1, testStepToInsert);
                    shouldInsert = false;
                }
                else if (this.ObservableTestSteps[i].StepGuid.Equals(sharedStepGuid) && !shouldInsert)
                {
                    continue;
                }
                else
                {
                    shouldInsert = true;
                }
            }
        }

        /// <summary>
        /// Inserts the test step information to the test case.
        /// </summary>
        /// <param name="testStepToInsert">The test step to be insert.</param>
        /// <param name="selectedIndex">Index of the selected test step.</param>
        public void InsertTestStepInTestCase(TestStep testStepToInsert, int selectedIndex)
        {
            if (selectedIndex != -1)
            {
                this.ObservableTestSteps.Insert(selectedIndex + 1, testStepToInsert);
            }
            else
            {
                this.ObservableTestSteps.Add(testStepToInsert);
            }
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
        }

        /// <summary>
        /// Inserts the new shared step.
        /// </summary>
        /// <param name="currentSharedStep">The current shared step.</param>
        /// <param name="selectedIndex">Index of the selected test step.</param>
        public void InsertSharedStep(SharedStep currentSharedStep, int selectedIndex)
        {
            string guid = TestStepManager.GetSharedStepGuid(this.AlreadyAddedSharedSteps, currentSharedStep.ISharedStep);
            List<TestStep> innerTestSteps = TestStepManager.GetAllTestStepsInSharedStep(currentSharedStep.ISharedStep, guid);
          
            int j = 0;
            for (int i = selectedIndex; i < innerTestSteps.Count + selectedIndex; i++)
            {
                this.ObservableTestSteps.Insert(i, innerTestSteps[j++]);
            }
        }

        /// <summary>
        /// Deletes the test step.
        /// </summary>
        /// <param name="testStepsToBeRemoved">The test steps to be removed.</param>
        public void RemoveTestSteps(List<TestStep> testStepsToBeRemoved)
        {
            foreach (TestStep currentStepToBeRemoved in testStepsToBeRemoved)
            {
                this.ObservableTestSteps.Remove(currentStepToBeRemoved);
                if (!currentStepToBeRemoved.IsShared)
                {
                    // TestCaseEditViewModel.TestCase.ITestCase.Actions.Remove(currentStepToBeRemoved as ITestAction);
                }
                else
                {
                    ISharedStep currentSharedStep = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentStepToBeRemoved.SharedStepId);

                    // TestCaseEditViewModel.TestCase.ITestCase.Actions.Remove(currentSharedStep as ITestAction);
                    foreach (ITestStep currentInnerTestStep in currentSharedStep.Actions)
                    {
                        if (this.ObservableTestSteps.Where(x => x.ITestStep.Title.Equals(currentInnerTestStep.Title)).ToList().Count > 0)
                        {
                            this.ObservableTestSteps.Remove(this.ObservableTestSteps.First(x => x.ITestStep.Title.Equals(currentInnerTestStep.Title)));
                        }
                    }
                }
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

        /// <summary>
        /// Initializes the test case with existing one.
        /// </summary>
        private void InitializeTestCaseWithExisting()
        {
            this.TestCase.ITestCase.Actions.ToList().ForEach(x => this.TestActions.Add(x));
            List<ISharedStep> sharedStepList = TestStepManager.GetAllSharedSteps();

            sharedStepList.ForEach(s =>
            {
                this.ObservableSharedSteps.Add(new SharedStep(s));
            });
            List<SharedStep> testCaseSharedStepsList = new List<SharedStep>();
            TestStepManager.GetTestStepsFromTestActions(this.TestActions, this.AlreadyAddedSharedSteps, testCaseSharedStepsList).ForEach(x => this.ObservableTestSteps.Add(x));
        }
    }
}
