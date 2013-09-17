// <copyright file="TestCaseManager.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Entities;

    /// <summary>
    /// Contains helper methods for working with TestCase entities
    /// </summary>
    public static class TestCaseManager
    {
        /// <summary>
        /// Gets all test cases from all test suites.d
        /// </summary>
        /// <param name="suiteEntries">The test suite collection.</param>
        /// <returns>list with all test cases</returns>
        public static List<TestCase> GetAllTestCasesFromSuiteCollection(ITestSuiteCollection suiteEntries)
        {
            List<TestCase> testCases = new List<TestCase>();
            foreach (ITestSuiteBase currentSuite in suiteEntries)
            {
                if (currentSuite != null)
                {
                    currentSuite.Refresh();
                    foreach (var tc in currentSuite.TestCases)
                    {
                        testCases.Add(new TestCase(tc.TestCase, currentSuite));
                    }

                    if (currentSuite.TestSuiteType == TestSuiteType.StaticTestSuite)
                    {
                        IStaticTestSuite staticTestSuite = currentSuite as IStaticTestSuite;
                        if (staticTestSuite != null && (staticTestSuite.SubSuites.Count > 0))
                        {
                            testCases.AddRange(GetAllTestCasesFromSuiteCollection(staticTestSuite.SubSuites));
                        }
                    }
                }
            }
            return testCases;
        }

        /// <summary>
        /// Gets the associated automation.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <returns>the associated automation</returns>
        public static AssociatedAutomation GetAssociatedAutomation(this ITestCase testCase)
        {
            AssociatedAutomation associatedAutomation;
            if (testCase.Implementation == null)
            {
                associatedAutomation = new AssociatedAutomation();
            }
            else
            {
                associatedAutomation = new AssociatedAutomation(testCase.Implementation.ToString());
            }

            return associatedAutomation;
        }

        /// <summary>
        /// Sets the associated automation of specific test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="testForAssociation">The test to be associated with.</param>
        /// <param name="testType">Type of the test.</param>
        public static void SetAssociatedAutomation(this ITestCase testCase, Test testForAssociation, string testType)
        {
            try
            {
                ITmiTestImplementation imp = ExecutionContext.TestManagementTeamProject.CreateTmiTestImplementation(testForAssociation.FullName, testType, testForAssociation.ClassName, testForAssociation.MethodId);
                testCase.Implementation = imp;
            }
            catch (NullReferenceException)
            {
                // TODO: add exception handling
            }
        }

        /// <summary>
        /// Gets all test cases in current test plan.
        /// </summary>
        /// <returns>list of all test cases</returns>
        public static List<TestCase> GetAllTestCasesInTestPlan()
        {
            ExecutionContext.Preferences.TestPlan.Refresh();
            List<TestCase> testCasesList = GetAllTestCasesFromSuiteCollection(ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites);

            return testCasesList;
        }

        /// <summary>
        /// Saves the specified test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="createNew">should be saved as new test case.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="newSuiteTitle">The new suite title.</param>
        /// <param name="testSteps">The test steps.</param>
        /// <returns>the saved test case</returns>
        public static TestCase Save(this TestCase testCase, bool createNew, int priority, string newSuiteTitle, List<TestStep> testSteps)
        {
            TestCase currentTestCase = testCase;
            if (createNew)
            {
                ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Create();
                currentTestCase = new TestCase(testCaseCore, testCase.ITestSuiteBase);
            }
            currentTestCase.ITestCase.Area = testCase.ITestCase.Area;
            currentTestCase.ITestCase.Title = testCase.ITestCase.Title;
            currentTestCase.ITestCase.Priority = priority;
            currentTestCase.ITestCase.Actions.Clear();
            List<string> addedSharedStepGuids = new List<string>();
            foreach (TestStep currentStep in testSteps)
            {
                if (currentStep.IsShared && !addedSharedStepGuids.Contains(currentStep.StepGuid))
                {
                    ISharedStep sharedStepCore = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentStep.SharedStepId);

                    // currentTestCase.ITestCase.Actions.Add(sharedStep as ITestAction);
                    ISharedStepReference sharedStepReferenceCore = currentTestCase.ITestCase.CreateSharedStepReference();
                    sharedStepReferenceCore.SharedStepId = sharedStepCore.Id;
                    currentTestCase.ITestCase.Actions.Add(sharedStepReferenceCore);
                    addedSharedStepGuids.Add(currentStep.StepGuid);
                }
                else if (!currentStep.IsShared)
                {
                    ITestStep testStepCore = currentTestCase.ITestCase.CreateTestStep();
                    testStepCore.Title = currentStep.ITestStep.Title;
                    testStepCore.ExpectedResult = currentStep.ITestStep.ExpectedResult;
                    currentTestCase.ITestCase.Actions.Add(testStepCore);
                }
            }
            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();
            TestSuiteManager.RemoveTestCase(currentTestCase.ITestCase);

            SetTestCaseSuite(newSuiteTitle, currentTestCase);

            return currentTestCase;
        }

        /// <summary>
        /// Duplicates the test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="textReplacePairs">The text replace pairs.</param>
        /// <param name="sharedStepIdReplacePairs">The shared step replace pairs.</param>
        /// <param name="newSuiteTitle">The new suite title.</param>
        /// <param name="replaceInTitles">if set to <c>true</c> [replace information titles].</param>
        /// <param name="replaceSharedSteps">if set to <c>true</c> [replace shared steps].</param>
        /// <param name="replaceInSteps">if set to <c>true</c> [replace information steps].</param>
        public static void DuplicateTestCase(this TestCase testCase, List<TextReplacePair> textReplacePairs, List<SharedStepIdReplacePair> sharedStepIdReplacePairs, string newSuiteTitle, bool replaceInTitles, bool replaceSharedSteps, bool replaceInSteps)
        {
            List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(testCase.ITestCase.Actions.ToList());
            ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Create();
            TestCase currentTestCase = new TestCase(testCaseCore, testCase.ITestSuiteBase);

            currentTestCase.ITestCase.Area = testCase.ITestCase.Area;
            if (replaceInTitles)
            {
                currentTestCase.ITestCase.Title = testCase.ITestCase.Title.ReplaceAll(textReplacePairs);
            }
            else
            {
                currentTestCase.ITestCase.Title = testCase.ITestCase.Title;
            }
            currentTestCase.ITestCase.Priority = testCase.ITestCase.Priority;
            ReplaceStepsInTestCase(currentTestCase, textReplacePairs, sharedStepIdReplacePairs, testSteps, replaceSharedSteps, replaceInSteps);
            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();

            var newSuite = TestSuiteManager.GetTestSuiteByName(newSuiteTitle);
            newSuite.AddTestCase(currentTestCase.ITestCase);
        }

        /// <summary>
        /// Finds the and replace information test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="textReplacePairs">The text replace pairs.</param>
        /// <param name="sharedStepIdReplacePairs">The shared step replace pairs.</param>
        /// <param name="replaceInTitles">if set to <c>true</c> [replace information titles].</param>
        /// <param name="replaceSharedSteps">if set to <c>true</c> [replace shared steps].</param>
        /// <param name="replaceInSteps">if set to <c>true</c> [replace information steps].</param>
        public static void FindAndReplaceInTestCase(this TestCase testCase, List<TextReplacePair> textReplacePairs, List<SharedStepIdReplacePair> sharedStepIdReplacePairs, bool replaceInTitles, bool replaceSharedSteps, bool replaceInSteps)
        {
            TestCase currentTestCase = testCase;
            currentTestCase.ITestCase = ExecutionContext.TestManagementTeamProject.TestCases.Find(testCase.ITestCase.Id);
            List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(currentTestCase.ITestCase.Actions.ToList());
            if (replaceInTitles)
            {
                string newTitle = currentTestCase.ITestCase.Title.ReplaceAll(textReplacePairs);
                currentTestCase.ITestCase.Title = newTitle;
            }
            ReplaceStepsInTestCase(currentTestCase, textReplacePairs, sharedStepIdReplacePairs, testSteps, replaceSharedSteps, replaceInSteps);

            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();
        }

        /// <summary>
        /// Replaces the test steps information in specific test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="textReplacePairs">The text replace pairs.</param>
        /// <param name="sharedStepReplacePairs">The shared step replace pairs.</param>
        /// <param name="testSteps">The test steps.</param>
        /// <param name="replaceSharedSteps">if set to <c>true</c> [replace shared steps].</param>
        /// <param name="replaceInSteps">if set to <c>true</c> [replace information steps].</param>
        private static void ReplaceStepsInTestCase(TestCase testCase, List<TextReplacePair> textReplacePairs, List<SharedStepIdReplacePair> sharedStepReplacePairs, List<TestStep> testSteps, bool replaceSharedSteps, bool replaceInSteps)
        {
            if (replaceSharedSteps || replaceInSteps)
            {
                testCase.ITestCase.Actions.Clear();
                List<string> addedSharedStepGuids = new List<string>();

                foreach (TestStep currentStep in testSteps)
                {
                    if (currentStep.IsShared && !addedSharedStepGuids.Contains(currentStep.StepGuid) && replaceSharedSteps)
                    {
                        int newSharedStepId = GetNewSharedStepId(currentStep.SharedStepId, sharedStepReplacePairs);
                        if (!replaceSharedSteps)
                        {
                            newSharedStepId = currentStep.SharedStepId;
                        }
                        ISharedStep sharedStep = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(newSharedStepId);
                        ISharedStepReference sharedStepReferenceCore = testCase.ITestCase.CreateSharedStepReference();
                        sharedStepReferenceCore.SharedStepId = sharedStep.Id;
                        testCase.ITestCase.Actions.Add(sharedStepReferenceCore);
                        addedSharedStepGuids.Add(currentStep.StepGuid);
                    }
                    else if (!currentStep.IsShared)
                    {
                        ITestStep testStepCore = testCase.ITestCase.CreateTestStep();
                        if (replaceInSteps)
                        {
                            testStepCore.Title = currentStep.ITestStep.Title.ToString().ReplaceAll(textReplacePairs);
                            testStepCore.ExpectedResult = currentStep.ITestStep.ExpectedResult.ToString().ReplaceAll(textReplacePairs);
                        }
                        else
                        {
                            testStepCore.Title = currentStep.ITestStep.Title;
                            testStepCore.ExpectedResult = currentStep.ITestStep.ExpectedResult;
                        }
                        testCase.ITestCase.Actions.Add(testStepCore);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the test case suite.
        /// </summary>
        /// <param name="newSuiteTitle">The new suite title.</param>
        /// <param name="testCase">The test case.</param>
        private static void SetTestCaseSuite(string newSuiteTitle, TestCase testCase)
        {
            var newSuite = TestSuiteManager.GetTestSuiteByName(newSuiteTitle);
            newSuite.AddTestCase(testCase.ITestCase);
            testCase.ITestSuiteBase = newSuite;
        }

        /// <summary>
        /// Gets the new shared step unique identifier.
        /// </summary>
        /// <param name="currentSharedStepId">The current shared step unique identifier.</param>
        /// <param name="sharedStepIdReplacePairs">The shared steps replace pairs.</param>
        /// <returns>new shared step id</returns>
        private static int GetNewSharedStepId(int currentSharedStepId, List<SharedStepIdReplacePair> sharedStepIdReplacePairs)
        {
            int newSharedStepId = currentSharedStepId;
            foreach (SharedStepIdReplacePair currentPair in sharedStepIdReplacePairs)
            {
                if (currentSharedStepId.Equals(currentPair.OldSharedStepId))
                {
                    newSharedStepId = currentPair.NewSharedStepId;
                    break;
                }
            }

            return newSharedStepId;
        }
    }
}
