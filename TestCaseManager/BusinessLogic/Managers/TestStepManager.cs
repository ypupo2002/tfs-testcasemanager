// <copyright file="TestStepManager.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Entities;
    using TestCaseManagerApp.BusinessLogic.Enums;
    using TestCaseManagerApp.BusinessLogic.Managers;

    /// <summary>
    /// Contains helper methods for working with TestStep entities
    /// </summary>
    public static class TestStepManager
    {
        /// <summary>
        /// Gets the test steps from test actions.
        /// </summary>
        /// <param name="testActions">The test actions.</param>
        /// <param name="alreadyAddedSharedSteps">The already added shared steps.</param>
        /// <param name="sharedSteps">The shared steps.</param>
        /// <returns>list of all test steps</returns>
        public static List<TestStep> GetTestStepsFromTestActions(List<ITestAction> testActions, Dictionary<int, string> alreadyAddedSharedSteps = null, List<SharedStep> sharedSteps = null)
        {
            if (alreadyAddedSharedSteps == null)
            {
                alreadyAddedSharedSteps = new Dictionary<int, string>();
            }

            List<TestStep> testSteps = new List<TestStep>();           

            foreach (var currentAction in testActions)
            {
                if (currentAction is ITestStep)
                {
                    string stepGuid = Guid.NewGuid().ToString();
                    testSteps.Add(new TestStep(false, currentAction as ITestStep, string.Empty, stepGuid));
                }
                else if (currentAction is ISharedStepReference)
                {
                    ISharedStepReference currentSharedStepReference = currentAction as ISharedStepReference;
                    ISharedStep currentSharedStep = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentSharedStepReference.SharedStepId);
                    string sharedStepGuid = GetSharedStepGuid(alreadyAddedSharedSteps, currentSharedStep);

                    //if (sharedSteps != null)
                    //{
                    //    sharedSteps.Add(new SharedStep(currentSharedStep));
                    //}

                    testSteps.AddRange(TestStepManager.GetAllTestStepsInSharedStep(currentSharedStep, sharedStepGuid));
                }
            }

            return testSteps;
        }

        /// <summary>
        /// Copies the automatic clipboard.
        /// </summary>
        /// <param name="isCopy">if set to <c>true</c> [copy].</param>
        /// <param name="testSteps">The test steps.</param>
        public static void CopyToClipboardTestSteps(bool isCopy, List<TestStep> testSteps)
        {
            ClipBoardCommand clipBoardCommand = isCopy ? ClipBoardCommand.Copy : ClipBoardCommand.Cut;
            ClipBoardTestStep clipBoardTestStep = new ClipBoardTestStep(testSteps, clipBoardCommand);
            ClipBoardManager<ClipBoardTestStep>.CopyToClipboard(clipBoardTestStep);
        }

        /// <summary>
        /// Gets from clipboard the test steps
        /// </summary>
        /// <returns>the retrieved test steps</returns>
        public static ClipBoardTestStep GetFromClipboardTestSteps()
        {
            ClipBoardTestStep clipBoardTestStep = ClipBoardManager<ClipBoardTestStep>.GetFromClipboard();

            if (clipBoardTestStep != null)
            {
                return clipBoardTestStep;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the shared step unique identifier.
        /// </summary>
        /// <param name="alreadyAddedSharedSteps">The already added shared steps.</param>
        /// <param name="currentSharedStep">The current shared step.</param>
        /// <returns>the shared step unique identifier</returns>
        public static string GetSharedStepGuid(Dictionary<int, string> alreadyAddedSharedSteps, ISharedStep currentSharedStep)
        {
            string sharedStepUniqueGuid = Guid.NewGuid().ToString();

            return sharedStepUniqueGuid;
        }

        /// <summary>
        /// Determines whether [is there shared step selected] from [the specified selected test steps].
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>is there shared step selected</returns>
        public static bool IsThereSharedStepSelected(List<TestStep> selectedTestSteps)
        {
            bool isThereSharedStepSelected = false;
            foreach (TestStep currentStep in selectedTestSteps)
            {
                if (currentStep.IsShared)
                {
                    isThereSharedStepSelected = true;
                    break;
                }
            }

            return isThereSharedStepSelected;
        }

        /// <summary>
        /// Gets the test steps from shared step.
        /// </summary>
        /// <param name="currentSharedStep">The current shared step.</param>
        /// <param name="sharedStepUniqueGuid">The shared step unique unique identifier.</param>
        /// <returns>list of all test steps in the specified shared step</returns>
        public static List<TestStep> GetAllTestStepsInSharedStep(ISharedStep currentSharedStep, string sharedStepUniqueGuid = "")
        {
            List<TestStep> testSteps = new List<TestStep>();
            if (currentSharedStep != null && currentSharedStep.Actions != null)
            {                
                foreach (var currentSharedStepAction in currentSharedStep.Actions)
                {
                    testSteps.Add(new TestStep(true, currentSharedStepAction as ITestStep, currentSharedStep.Title, currentSharedStep.Id, sharedStepUniqueGuid));
                }
            }

            return testSteps;
        }

        /// <summary>
        /// Generates the test steps text - action + expected result.
        /// </summary>
        /// <param name="testSteps">The test steps.</param>
        /// <returns>test steps text</returns>
        public static string GenerateTestStepsText(List<TestStep> testSteps)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TestStep currentTestStep in testSteps)
            {
                sb.AppendLine(string.Format("{0}   {1}", currentTestStep.ITestStep.Title.ToPlainText(), currentTestStep.ITestStep.ExpectedResult.ToPlainText()));
                sb.AppendLine(new string('*', 20));
            }

            string result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Creates the new test step.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="stepTitle">The step title.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>the test step object</returns>
        public static TestStep CreateNewTestStep(TestCase testCase, string stepTitle, string expectedResult, string guid = "")
        {
            ITestStep testStepCore = testCase.ITestCase.CreateTestStep();
            testStepCore.ExpectedResult = expectedResult;
            testStepCore.Title = stepTitle;
            if (string.IsNullOrEmpty(guid))
            {
                guid = Guid.NewGuid().ToString();
            }

            TestStep testStepToInsert = new TestStep(false, testStepCore, string.Empty, guid);

            return testStepToInsert;
        }

        /// <summary>
        /// Creates the new shared step.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="sharedStepTitle">The shared step title.</param>
        /// <param name="stepTitle">The step title.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <returns>the shared step core object</returns>
        public static ISharedStep CreateNewSharedStep(TestCase testCase, string sharedStepTitle, string stepTitle, string expectedResult)
        {
            ISharedStepReference sharedStepReferenceCore = testCase.ITestCase.CreateSharedStepReference();
            ISharedStep sharedStepCore = ExecutionContext.TestManagementTeamProject.SharedSteps.Create();
            sharedStepReferenceCore.SharedStepId = sharedStepCore.Id;
            sharedStepCore.Title = sharedStepTitle;
            ITestStep testStepCore = sharedStepCore.CreateTestStep();
            testStepCore.ExpectedResult = expectedResult;
            testStepCore.Title = stepTitle;
            sharedStepCore.Actions.Add(testStepCore);

            return sharedStepCore;
        }

        /// <summary>
        /// Creates the new shared step.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="sharedStepTitle">The shared step title.</param>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>the shared step core object</returns>
        public static ISharedStep CreateNewSharedStep(this TestCase testCase, string sharedStepTitle, List<TestStep> selectedTestSteps)
        {
            ISharedStep sharedStepCore = ExecutionContext.TestManagementTeamProject.SharedSteps.Create();
            sharedStepCore.Title = sharedStepTitle;
            string sharedStepGuid = Guid.NewGuid().ToString();

            sharedStepCore.Save();
            AddTestStepsToSharedStep(sharedStepCore, sharedStepGuid, selectedTestSteps, sharedStepTitle);
            sharedStepCore.Save();

            return sharedStepCore;
        }

        /// <summary>
        /// Gets all shared step core objects.
        /// </summary>
        /// <returns>list of all shared step core objects</returns>
        public static List<ISharedStep> GetAllSharedSteps()
        {
            return ExecutionContext.TestManagementTeamProject.SharedSteps.Query("select * from WorkItems where [System.TeamProject] = @project and [System.WorkItemType] = 'Shared Steps'").ToList();
        }

        /// <summary>
        /// Adds the test steps to shared steps actions.
        /// </summary>
        /// <param name="sharedStepCore">The core shared step object.</param>
        /// <param name="sharedStepGuid">The shared step unique identifier.</param>
        /// <param name="selectedTestSteps">The test steps to add.</param>
        /// <param name="sharedStepTitle">The shared step title.</param>
        private static void AddTestStepsToSharedStep(ISharedStep sharedStepCore, string sharedStepGuid, List<TestStep> selectedTestSteps, string sharedStepTitle)
        {
            foreach (TestStep currentTestStep in selectedTestSteps)
            {
                ITestStep testStepCore = sharedStepCore.CreateTestStep();
                testStepCore.ExpectedResult = currentTestStep.ITestStep.ExpectedResult;
                testStepCore.Title = currentTestStep.ITestStep.Title;
                sharedStepCore.Actions.Add(testStepCore);
                currentTestStep.StepGuid = sharedStepGuid;
                currentTestStep.Title = sharedStepTitle;
                currentTestStep.IsShared = true;
                currentTestStep.SharedStepId = sharedStepCore.Id;
            }
        }
    }
}
