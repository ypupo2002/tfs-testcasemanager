// <copyright file="TestCaseManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Entities;
    using TestCaseManagerApp.BusinessLogic.Enums;
    using TestCaseManagerApp.BusinessLogic.Managers;

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
        public static List<TestCase> GetAllTestCasesFromSuiteCollection(ITestSuiteCollection suiteEntries, List<int> alreadyCheckedSuitesIds = null)
        {
            if (alreadyCheckedSuitesIds == null)
            {
                alreadyCheckedSuitesIds = new List<int>();
            }
            List<TestCase> testCases = new List<TestCase>();
            
            foreach (ITestSuiteBase currentSuite in suiteEntries)
            {
                if (currentSuite != null && !alreadyCheckedSuitesIds.Contains(currentSuite.Id))
                {
                    alreadyCheckedSuitesIds.Add(currentSuite.Id);
                    currentSuite.Refresh();
                    foreach (var currentTestCase in currentSuite.TestCases)
                    {
                        TestCase testCaseToAdd = new TestCase(currentTestCase.TestCase, currentSuite);
                        if (!testCases.Contains(testCaseToAdd))
                        {
                            testCases.Add(testCaseToAdd);
                        }                        
                    }

                    if (currentSuite.TestSuiteType == TestSuiteType.StaticTestSuite)
                    {
                        IStaticTestSuite staticTestSuite = currentSuite as IStaticTestSuite;
                        if (staticTestSuite != null && (staticTestSuite.SubSuites.Count > 0))
                        {
                            List<TestCase> testCasesInternal = GetAllTestCasesFromSuiteCollection(staticTestSuite.SubSuites, alreadyCheckedSuitesIds);
                            foreach (var currentTestCase in testCasesInternal)
                            {
                                if (!testCases.Contains(currentTestCase))
                                {
                                    testCases.Add(currentTestCase);
                                }   
                            }
                        }
                    }
                }
            }
            return testCases;
        }

        /// <summary>
        /// Gets all test case from suite.
        /// </summary>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <returns>list of all test cases in the list</returns>
        public static List<TestCase> GetAllTestCaseFromSuite(int suiteId)
        {
            List<TestCase> testCases = new List<TestCase>();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ITestSuiteBase currentSuite = ExecutionContext.Preferences.TestPlan.Project.TestSuites.Find(suiteId);
            currentSuite.Refresh();
            foreach (var currentTestCase in currentSuite.TestCases)
            {
                TestCase testCaseToAdd = new TestCase(currentTestCase.TestCase, currentSuite);
                if (!testCases.Contains(testCaseToAdd))
                {
                    testCases.Add(testCaseToAdd);
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
        public static List<TestCase> GetAllTestCasesInTestPlan(bool includeSuites = true)
        {
            ExecutionContext.Preferences.TestPlan.Refresh();
            List<TestCase> testCasesList;
            if (includeSuites)
            {
                testCasesList = GetAllTestCasesFromSuiteCollection(ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites);
                AddTestCasesWithoutSuites(testCasesList);
            }
            else
            {
                testCasesList = new List<TestCase>();
                string queryText = "select [System.Id], [System.Title] from WorkItems where [System.WorkItemType] = 'Test Case'";
                IEnumerable<ITestCase> allTestCases = ExecutionContext.TestManagementTeamProject.TestCases.InPlans(queryText, true);
                foreach (var currentTestCase in allTestCases)
                {
                    TestCase testCaseToAdd = new TestCase(currentTestCase, currentTestCase.TestSuiteEntry.ParentTestSuite);
                    if (!testCasesList.Contains(testCaseToAdd))
                    {
                        testCasesList.Add(testCaseToAdd);
                    }
                }
            }

            return testCasesList;
        }      

        /// <summary>
        /// Saves the specified test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="createNew">should be saved as new test case.</param>
        /// <param name="newSuiteTitle">The new suite title.</param>
        /// <param name="testSteps">The test steps.</param>
        /// <returns>the saved test case</returns>
        public static TestCase Save(this TestCase testCase, bool createNew, string newSuiteTitle, List<TestStep> testSteps)
        {
            TestCase currentTestCase = testCase;
            if (createNew)
            {
                ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Create();
                currentTestCase = new TestCase(testCaseCore, testCase.ITestSuiteBase);
            }
            currentTestCase.ITestCase.Area = testCase.Area;
            currentTestCase.ITestCase.Title = testCase.Title;
            currentTestCase.ITestCase.Priority = (int)testCase.Priority;
            currentTestCase.ITestCase.Actions.Clear();
            currentTestCase.ITestCase.Owner = ExecutionContext.TestManagementTeamProject.TfsIdentityStore.FindByTeamFoundationId(testCase.TeamFoundationId);
            List<Guid> addedSharedStepGuids = new List<Guid>();
            foreach (TestStep currentStep in testSteps)
            {
                if (currentStep.IsShared && !addedSharedStepGuids.Contains(currentStep.TestStepGuid))
                {
                    ISharedStep sharedStepCore = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentStep.SharedStepId);
                    ISharedStepReference sharedStepReferenceCore = currentTestCase.ITestCase.CreateSharedStepReference();
                    sharedStepReferenceCore.SharedStepId = sharedStepCore.Id;
                    currentTestCase.ITestCase.Actions.Add(sharedStepReferenceCore);
                    addedSharedStepGuids.Add(currentStep.TestStepGuid);
                }
                else if (!currentStep.IsShared)
                {
                    ITestStep testStepCore = currentTestCase.ITestCase.CreateTestStep();
                    testStepCore.Title = currentStep.ActionTitle;
                    testStepCore.ExpectedResult = currentStep.ActionExpectedResult;
                    currentTestCase.ITestCase.Actions.Add(testStepCore);
                }
            }
            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();

            SetTestCaseSuite(newSuiteTitle, currentTestCase);

            return currentTestCase;
        }
    
        /// <summary>
        /// Copies the automatic clipboard.
        /// </summary>
        /// <param name="isCopy">if set to <c>true</c> [copy].</param>
        /// <param name="testCases">The test cases.</param>
        public static void CopyToClipboardTestCases(bool isCopy, List<TestCase> testCases)
        {
            ClipBoardCommand clipBoardCommand = isCopy ? ClipBoardCommand.Copy : ClipBoardCommand.Cut;
            ClipBoardTestCase clipBoardTestCase = new ClipBoardTestCase(testCases, clipBoardCommand);
            ClipBoardManager<ClipBoardTestCase>.CopyToClipboard(clipBoardTestCase);
            SerializationManager.IsSerializable(clipBoardTestCase);
        }

        /// <summary>
        /// Gets from clipboard the test cases
        /// </summary>
        /// <returns>the retrieved test cases</returns>
        public static ClipBoardTestCase GetFromClipboardTestCases()
        {
            ClipBoardTestCase clipBoardTestCase = ClipBoardManager<ClipBoardTestCase>.GetFromClipboard();

            if (clipBoardTestCase != null)
            {
                return clipBoardTestCase;
            }
            else
            {
                return null;
            }            
        }

        /// <summary>
        /// Gets the test case core object by unique identifier.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <returns></returns>
        public static ITestCase GetTestCaseCoreObjectById(int testCaseId)
        {
            ITestCase testCaseCore = null;
            try
            {
                testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Find(testCaseId);
            }
            catch
            {
            }

            return testCaseCore;
        }

        /// <summary>
        /// Adds the test cases without suites.
        /// </summary>
        /// <param name="testCasesList">The test cases list.</param>
        private static void AddTestCasesWithoutSuites(List<TestCase> testCasesList)
        {
            string queryText = "select [System.Id], [System.Title] from WorkItems where [System.WorkItemType] = 'Test Case'";
            IEnumerable<ITestCase> allTestCases = ExecutionContext.TestManagementTeamProject.TestCases.InPlans(queryText, true);
            foreach (var currentTestCase in allTestCases)
            {
                TestCase testCaseToAdd = new TestCase(currentTestCase, null);
                if (!testCasesList.Contains(testCaseToAdd))
                {
                    testCasesList.Add(testCaseToAdd);
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
    }
}
