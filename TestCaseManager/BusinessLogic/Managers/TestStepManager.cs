// <copyright file="TestStepManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Managers
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
        public static List<TestStep> GetTestStepsFromTestActions(ICollection<ITestAction> testActions)
        {
            List<TestStep> testSteps = new List<TestStep>();           

            foreach (var currentAction in testActions)
            {
                if (currentAction is ITestStep)
                {
                    Guid testStepGuid = Guid.NewGuid();
                    testSteps.Add(new TestStep(false, string.Empty, testStepGuid, currentAction as ITestStep));
                }
                else if (currentAction is ISharedStepReference)
                {
                    ISharedStepReference currentSharedStepReference = currentAction as ISharedStepReference;
                    ISharedStep currentSharedStep = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentSharedStepReference.SharedStepId);
                    testSteps.AddRange(TestStepManager.GetAllTestStepsInSharedStep(currentSharedStep));
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
        /// <returns>list of all test steps in the specified shared step</returns>
        public static List<TestStep> GetAllTestStepsInSharedStep(ISharedStep currentSharedStep, bool includeSharedStep = true)
        {
            List<TestStep> testSteps = new List<TestStep>();
            Guid sharedStepUniqueGuid = Guid.NewGuid();
            if (currentSharedStep != null && currentSharedStep.Actions != null)
            {                
                foreach (var currentSharedStepAction in currentSharedStep.Actions)
                {
                    if(includeSharedStep)
                    {
                        testSteps.Add(new TestStep(true, currentSharedStep.Title, sharedStepUniqueGuid, currentSharedStepAction as ITestStep, currentSharedStep.Id));
                    }
                    else
                    {
                        testSteps.Add(new TestStep(false, currentSharedStep.Title, Guid.NewGuid(), currentSharedStepAction as ITestStep));
                    }                   
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
                sb.AppendLine(string.Format("{0}   {1}", currentTestStep.ActionTitle, currentTestStep.ActionExpectedResult));
                sb.AppendLine(new string('*', 20));
            }

            string result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Creates the new test step.
        /// </summary>
        /// <param name="testBase">The test case.</param>
        /// <param name="stepTitle">The step title.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <param name="testStepGuid">The unique identifier.</param>
        /// <returns>the test step object</returns>
        public static TestStep CreateNewTestStep(ITestBase testBase, string stepTitle, string expectedResult, Guid testStepGuid)
        {
            ITestStep testStepCore = testBase.CreateTestStep();
            testStepCore.ExpectedResult = expectedResult;
            testStepCore.Title = stepTitle;
            if (testStepGuid == default(Guid))
            {
                testStepGuid = Guid.NewGuid();
            }

            TestStep testStepToInsert = new TestStep(false, string.Empty, testStepGuid, testStepCore);

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

            sharedStepCore.Save();
            AddTestStepsToSharedStep(sharedStepCore, Guid.NewGuid(), selectedTestSteps, sharedStepTitle);
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
        private static void AddTestStepsToSharedStep(ISharedStep sharedStepCore, Guid sharedStepGuid, List<TestStep> selectedTestSteps, string sharedStepTitle)
        {
            foreach (TestStep currentTestStep in selectedTestSteps)
            {
                ITestStep testStepCore = sharedStepCore.CreateTestStep();
                testStepCore.ExpectedResult = currentTestStep.ActionExpectedResult;
                testStepCore.Title = currentTestStep.ActionTitle;
                sharedStepCore.Actions.Add(testStepCore);
                currentTestStep.TestStepGuid = sharedStepGuid;
                currentTestStep.Title = sharedStepTitle;
                currentTestStep.IsShared = true;
                currentTestStep.SharedStepId = sharedStepCore.Id;
            }
        }
    }
}
