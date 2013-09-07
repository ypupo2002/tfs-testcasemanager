using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp.ViewModels
{
    public class TestCaseEditViewModel
    {
        public const string SharedStepSearchDefaultText = "Search in Shared";
        public const string ActionDefaultText = "Action";
        public const string ExpectedResultDefaultText = "Expected Result";
        public bool ActionFlag;
        public bool ExpectedResultFlag;
        public bool SharedStepSearchFlag;
        public Dictionary<int, string> AlreadyAddedSharedSteps { get; set; }
        public TestCase TestCase { get; set; }
        public List<ITestAction> TestActions { get; set; }
        public ObservableCollection<TestStep> ObservableTestSteps { get; set; }
        public ObservableCollection<SharedStep> ObservableSharedSteps { get; set; }
        private ObservableCollection<SharedStep> InitialSharedStepCollection { get; set; }
        public List<ITestSuiteBase> TestSuiteList { get; set; }
        public AssociatedAutomation AssociatedAutomation { get; set; }
        public List<int> Priorities { get; set; }
        public List<bool> IsAutomatedValues { get; set; }
        public List<string> Areas { get; set; }
        public bool CreateNew { get; set; }
        public bool Duplicate { get; set; }
        public bool IsAlreadyCreated { get; set; }
        public bool ComesFromAssociatedAutomation { get; set; }

        public TestCaseEditViewModel(int testCaseId, int suiteId, bool createNew, bool duplicate)
        {
            Areas = GetProjectAreas();
            IsAutomatedValues = new List<bool>() { true, false };
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            TestSuiteList = ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites.GetSuites();
            
            CreateNew = createNew;
            Duplicate = duplicate;
            TestActions = new List<ITestAction>();
            ObservableTestSteps = new ObservableCollection<TestStep>();
            ObservableSharedSteps = new ObservableCollection<SharedStep>();
           
            Priorities = new List<int>() { 1, 2, 3, 4 };
            AlreadyAddedSharedSteps = new Dictionary<int, string>();
            ITestSuiteBase iTestSuiteBase = null;
            if (suiteId != 0)
            {
                iTestSuiteBase = ExecutionContext.TeamProject.TestSuites.Find(suiteId);
            }
            else
            {
                iTestSuiteBase = ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites.GetSuites().FirstOrDefault();
            }
            
            if (CreateNew && !Duplicate)
            {
                ITestCase newTestCase = ExecutionContext.TeamProject.TestCases.Create();
                if (Duplicate)
                {
                    InitializeTestCaseWithExisting();
                }
                else
                {
                    TestCase = new TestCase(newTestCase, null);
                }
            }
            else
            {
                ITestCase iTestCase = ExecutionContext.TeamProject.TestCases.Find(testCaseId);
                TestCase = new TestCase(iTestCase, iTestSuiteBase);               
            }
            InitializeTestCaseWithExisting();
            InitializeInitialSharedStepCollection();
            this.AssociatedAutomation = TestCase.ITestCase.GetAssociatedAutomation();
            UpdateObservableTestSteps(ObservableTestSteps.ToList());
        }

        private void InitializeInitialSharedStepCollection()
        {
            InitialSharedStepCollection = new ObservableCollection<SharedStep>();
            foreach (var cSharedStep in ObservableSharedSteps)
            {
                InitialSharedStepCollection.Add(cSharedStep);
            }
        }

        public void ReinitializeSharedStepCollection()
        {
            ObservableSharedSteps.Clear();
            foreach (var item in InitialSharedStepCollection)
            {
                ObservableSharedSteps.Add(item);
            }
        }

        public List<string> GetProjectAreas()
        {
            List<string> areas = new List<string>();
            ICommonStructureService css = (ICommonStructureService)ExecutionContext.Tfs.GetService(typeof(ICommonStructureService));
            //Gets Area/Iteration base Project
            ProjectInfo projectInfo = css.GetProjectFromName(ExecutionContext.Preferences.TestProjectName);
            NodeInfo[] nodes = css.ListStructures(projectInfo.Uri);
            XmlElement areaTree = css.GetNodesXml(new string[] { nodes[0].Uri }, true);
            areas.Clear();
            XmlNode areaNodes = areaTree.ChildNodes[0];
            MakeList(areaNodes, areas);

            return areas;
        }

        public void UpdateObservableTestSteps(List<TestStep> selectedTestSteps)
        {
            foreach (TestStep currentSelectedStep in selectedTestSteps)
            {
                for (int i = 0; i < ObservableTestSteps.Count; i++)
                {
                    if (currentSelectedStep.ITestStep.Id.Equals(ObservableTestSteps[i].ITestStep.Id))
                    {
                        ObservableTestSteps[i] = currentSelectedStep;
                        string title = ObservableTestSteps[i].ITestStep.Title.ToPlainText();
                    }
                }  
            }
        }

        public string GetStepTitle(string stepTitle)
        {
            string finalStepTitle = stepTitle;
            if (finalStepTitle.Equals(ActionDefaultText))
            {
                finalStepTitle = String.Empty;
            }

            return finalStepTitle;
        }

        public string GetExpectedResult(string expectedResult)
        {
            string finalExpectedResult = expectedResult;
            if (finalExpectedResult.Equals(ExpectedResultDefaultText))
            {
                finalExpectedResult = String.Empty;
            }

            return finalExpectedResult;
        }

        private void MakeList(XmlNode xmlNode, List<string> areas)
        {
            GetAreasSingleNode(xmlNode, areas);
            GetAreasNodes(xmlNode.ChildNodes, areas);
        }     

        private void GetAreasNodes(XmlNodeList nodeList, List<string> areas)
        {
            foreach (XmlNode currentNode in nodeList)
            {
                GetAreasSingleNode(currentNode, areas);
            }
        }

        private void GetAreasSingleNode(XmlNode currentNode, List<string> areas)
        {
            if (currentNode.Attributes.Count > 0)
            {
                string path = currentNode.Attributes["Path"].Value.TrimStart('\\').Replace("\\Area", "");
                if (!areas.Contains(path))
                {
                    areas.Add(path);
                }
            }
            if (currentNode.ChildNodes.Count != 0)
            {
                GetAreasNodes(currentNode.ChildNodes, areas);
            }
        }

        private void InitializeTestCaseWithExisting()
        {
            TestCase.ITestCase.Actions.ToList().ForEach(x => TestActions.Add(x));
            List<ISharedStep> sharedStepList = TestStepManager.GetSharedTestSteps();

            sharedStepList.ForEach(s =>
            {
                ObservableSharedSteps.Add(new SharedStep(s));
            });
            List<SharedStep> testCaseSharedStepsList = new List<SharedStep>();
            TestActions.GetTestSteps(AlreadyAddedSharedSteps, testCaseSharedStepsList).ForEach(x => ObservableTestSteps.Add(x));
        }

        public void DeleteAllMarkedStepsForRemoval(List<TestStep> testStepsToBeRemoved)
        {
            foreach (TestStep currentTestStepToBeRemoved in testStepsToBeRemoved)
            {
                ObservableTestSteps.Remove(currentTestStepToBeRemoved);
            }
        }

        public List<TestStep> MarkStepsToBeRemoved(IList<TestStep> selectedTestSteps)
        {
            // All initial steps are marked for deletion (Login To Telerik) after that we trace them in all shared steps across the test case actions
            List<TestStep> testStepsToBeRemoved = MarkInitialStepsToBeRemoved(selectedTestSteps);
            List<TestStep> allStepsToBeRemoved = new List<TestStep>();
            foreach (TestStep currentStepToBeRemoved in testStepsToBeRemoved)
            {
                foreach (TestStep currentTestStep in ObservableTestSteps)
                {
                    if (currentTestStep.ITestStep.Id.Equals(currentStepToBeRemoved.ITestStep.Id))
                    {
                        allStepsToBeRemoved.Add(currentTestStep);
                    }
                }
            }
            return allStepsToBeRemoved;
        }

        public List<TestStep> MarkInitialStepsToBeRemoved(IList<TestStep> selectedTestSteps)
        {
            List<TestStep> testStepsToBeRemoved = new List<TestStep>();
            foreach (TestStep currentStepToBeRemoved in selectedTestSteps)
            {
                testStepsToBeRemoved.Add(currentStepToBeRemoved);
            }

            return testStepsToBeRemoved;
        }

        public void InsertInSharedStep(SharedStep selectedSharedStep, string stepText, string expectedResult)
        {
            //int actionsInSharedStep = selectedSharedStep.ISharedStep.Actions.Count;
            string sharedStepGuid = ObservableTestSteps.Where(x => x.Title.Equals(selectedSharedStep.ISharedStep.Title)).FirstOrDefault().StepGuid;

            TestStep testStepToInsert = TestCase.CreateNewTestStep(stepText, expectedResult);
            testStepToInsert.IsShared = true;
            testStepToInsert.StepGuid = sharedStepGuid;
            bool shouldInsert = true;
            for (int i = ObservableTestSteps.Count - 1; i >= 0; i--)
            {
                if (ObservableTestSteps[i].StepGuid.Equals(sharedStepGuid) && shouldInsert)
                {
                    ObservableTestSteps.Insert(i + 1, testStepToInsert);
                    shouldInsert = false;
                }
                else if (ObservableTestSteps[i].StepGuid.Equals(sharedStepGuid) && !shouldInsert)
                {
                    continue;
                }
                else
                {
                    shouldInsert = true;
                }
            }
        }

        public void InsertTestStep(TestStep testStepToInsert, int selectedIndex)
        {
           
            if (selectedIndex != -1)
            {
                ObservableTestSteps.Insert(selectedIndex + 1, testStepToInsert);
                //TestCaseEditViewModel.TestCase.ITestCase.Actions.Insert(selectedIndex + 1, testStepToInsert as ITestAction);
            }
            else
            {
                ObservableTestSteps.Add(testStepToInsert);
                //TestCaseEditViewModel.TestCase.ITestCase.Actions.Add(testStepToInsert as ITestAction);
            }
        }

        public void CreateNewTestStepCollectionAfterMoveUp(int startIndex, int count)
        {
            List<TestStep> newCollection = new List<TestStep>();
            for (int i = 0; i < startIndex - 1; i++)
            {
                newCollection.Add(ObservableTestSteps[i]);
            }

            for (int i = startIndex; i < startIndex + count; i++)
            {
                newCollection.Add(ObservableTestSteps[i]);
            }
            for (int i = startIndex - 1; i < startIndex; i++)
            {
                newCollection.Add(ObservableTestSteps[i]);
            }

            for (int i = startIndex + count; i < ObservableTestSteps.Count; i++)
            {
                newCollection.Add(ObservableTestSteps[i]);
            }

            ObservableTestSteps.Clear();
            newCollection.ForEach(x => ObservableTestSteps.Add(x));   
        }

        public void CreateNewTestStepCollectionAfterMoveDown(int startIndex, int count)
        {
            List<TestStep> newCollection = new List<TestStep>();
            for (int i = 0; i < startIndex; i++)
            {
                newCollection.Add(ObservableTestSteps[i]);
            }
            newCollection.Add(ObservableTestSteps[startIndex + count]);
            for (int i = startIndex; i < startIndex + count; i++)
            {
                newCollection.Add(ObservableTestSteps[i]);
            }

            for (int i = startIndex + count + 1; i < ObservableTestSteps.Count; i++)
            {
                newCollection.Add(ObservableTestSteps[i]);
            }

            ObservableTestSteps.Clear();
            newCollection.ForEach(x => ObservableTestSteps.Add(x));
        }

        public void InsertSharedStep(SharedStep currentSharedStep, int selectedIndex)
        {
            string guid =  TestStepManager.GetSharedStepGuid(AlreadyAddedSharedSteps, currentSharedStep.ISharedStep);
            List<TestStep> innerTestSteps = currentSharedStep.ISharedStep.GetInnerTestSteps(guid);
          
            int j = 0;
            for (int i = selectedIndex; i < innerTestSteps.Count + selectedIndex; i++)
            {
                ObservableTestSteps.Insert(i, innerTestSteps[j++]);
            }
        }

        public void DeleteStep(List<TestStep> testStepsToBeRemoved)
        {
            foreach (TestStep currentStepToBeRemoved in testStepsToBeRemoved)
            {
                ObservableTestSteps.Remove(currentStepToBeRemoved);
                if (!currentStepToBeRemoved.IsShared)
                {
                    //TestCaseEditViewModel.TestCase.ITestCase.Actions.Remove(currentStepToBeRemoved as ITestAction);
                }
                else
                {
                    ISharedStep currentSharedStep = ExecutionContext.TeamProject.SharedSteps.Find(currentStepToBeRemoved.SharedStepId);
                    //TestCaseEditViewModel.TestCase.ITestCase.Actions.Remove(currentSharedStep as ITestAction);
                    foreach (ITestStep currentInnerTestStep in currentSharedStep.Actions)
                    {
                        if (ObservableTestSteps.Where(x => x.ITestStep.Title.Equals(currentInnerTestStep.Title)).ToList().Count > 0)
                        {
                            ObservableTestSteps.Remove(ObservableTestSteps.First(x => x.ITestStep.Title.Equals(currentInnerTestStep.Title)));
                        }
                    }
                }
            }
        }
    }
}
