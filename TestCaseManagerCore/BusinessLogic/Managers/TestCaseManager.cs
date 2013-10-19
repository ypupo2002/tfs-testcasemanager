// <copyright file="TestCaseManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Enums;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains helper methods for working with TestCase entities
    /// </summary>
    public static class TestCaseManager
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        /// Gets the most recent test case result.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <returns></returns>
        public static string GetMostRecentTestCaseResult(int testCaseId)
        {
            var testPoints = ExecutionContext.Preferences.TestPlan.QueryTestPoints(string.Format("Select * from TestPoint where TestCaseId = {0} ", testCaseId));
            ITestPoint lastTestPoint = null;
            if (testPoints.Count > 0)
            {
                lastTestPoint = testPoints.Last();
            }
            string mostRecentResult = "Active";
            ITestCaseResult lastTestCaseResult = null;
            if (lastTestPoint != null)
            {
                lastTestCaseResult = lastTestPoint.MostRecentResult;
            }
            if (lastTestCaseResult != null)
            {
                mostRecentResult = lastTestCaseResult.Outcome.ToString();
            }

            return mostRecentResult;
        }

        /// <summary>
        /// Sets the new execution outcome.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        /// <param name="newExecutionOutcome">The new execution outcome.</param>
        public static void SetNewExecutionOutcome(this TestCase currentTestCase, TestCaseExecutionType newExecutionOutcome)
        {
            if (currentTestCase.ITestCase.Owner == null)
            {
                return;
            }
            var testPoints = ExecutionContext.Preferences.TestPlan.QueryTestPoints(string.Format("Select * from TestPoint where TestCaseId = {0} ", currentTestCase.Id));
            var testRun = ExecutionContext.Preferences.TestPlan.CreateTestRun(false);

            testRun.DateStarted = DateTime.Now;
            testRun.AddTestPoint(testPoints.Last(), currentTestCase.ITestCase.Owner);
            testRun.DateCompleted = DateTime.Now;
            testRun.Save();

            var result = testRun.QueryResults()[0];
            result.Owner = currentTestCase.ITestCase.Owner;
            result.RunBy = currentTestCase.ITestCase.Owner;
            result.State = TestResultState.Completed;
            result.DateStarted = DateTime.Now;
            result.Duration = new TimeSpan(0L);
            result.DateCompleted = DateTime.Now.AddMinutes(0.0);
            result.Comment = "Run from Test Case Manager";
            switch (newExecutionOutcome)
            {
                case TestCaseExecutionType.Active:
                    result.Outcome = TestOutcome.NotExecuted;
                    break;
                case TestCaseExecutionType.Passed:
                    result.Outcome = TestOutcome.Passed;
                    break;
                case TestCaseExecutionType.Failed:
                    result.Outcome = TestOutcome.Failed;
                    break;
                case TestCaseExecutionType.Blocked:
                    result.Outcome = TestOutcome.Blocked;
                    break;
            }
            result.Save();
        }

        /// <summary>
        /// Gets the type of the test case execution.
        /// </summary>
        /// <param name="executionTypeStr">The execution type string.</param>
        /// <returns></returns>
        public static TestCaseExecutionType GetTestCaseExecutionType(string executionTypeStr)
        {
            TestCaseExecutionType testCaseExecutionType = (TestCaseExecutionType)Enum.Parse(typeof(TestCaseExecutionType), executionTypeStr);
            switch (testCaseExecutionType)
            {
                case TestCaseExecutionType.All:
                    break;
                case TestCaseExecutionType.Active:
                    break;
                case TestCaseExecutionType.Passed:
                    break;
                case TestCaseExecutionType.Failed:
                    break;
                default:
                    testCaseExecutionType = TestCaseExecutionType.Active;
                    break;
            }
            return testCaseExecutionType;
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
            log.InfoFormat("Load all test cases in the suite with Title= \"{0}\" id = \"{1}\"", currentSuite.Title, currentSuite.Id);
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
                log.InfoFormat("Associate test case with title= {0}, id= {1} to test: {2}, test type= {3}", testCase.Title, testCase.Id, testForAssociation, testType);
                ITmiTestImplementation imp = ExecutionContext.TestManagementTeamProject.CreateTmiTestImplementation(testForAssociation.FullName, testType, testForAssociation.ClassName, testForAssociation.MethodId);
                testCase.Implementation = imp;
            }
            catch (NullReferenceException ex)
            {
                log.Error(ex);
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
                IEnumerable<ITestCase> allTestCases = ExecutionContext.TestManagementTeamProject.TestCases.Query(queryText);
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
        public static TestCase Save(this TestCase testCase, bool createNew, int? suiteId, ICollection<TestStep> testSteps)
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

            if (suiteId != null)
            {
                var newSuite = TestSuiteManager.GetTestSuiteById((int)suiteId);
                testCase.ITestSuiteBase = newSuite;
            }
            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();
            if (suiteId != null)
            {
                SetTestCaseSuite((int)suiteId, currentTestCase);
            }

            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();

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
        /// Finds all reference test cases for specific shared step.
        /// </summary>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        public static List<TestCase> FindAllReferenceTestCasesForShareStep(int sharedStepId)
        {
            List<TestCase> filteredTestCases = new List<TestCase>();
            List<TestCase> allTestCases = GetAllTestCasesInTestPlan(true);
            foreach (var currentTestCase in allTestCases)
            {
                //List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(currentTestCase.ITestCase.Actions);
                foreach (var currentAction in currentTestCase.ITestCase.Actions)
                {
                    if (currentAction is ISharedStepReference)
                    {
                        ISharedStepReference currentSharedStepReference = currentAction as ISharedStepReference;
                        if (currentSharedStepReference.SharedStepId.Equals(sharedStepId))
                        {
                            filteredTestCases.Add(currentTestCase);
                            break;
                        }
                    }
                }
            }

            return filteredTestCases;
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
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="testCase">The test case.</param>
        private static void SetTestCaseSuite(int suiteId, TestCase testCase)
        {
            var newSuite = TestSuiteManager.GetTestSuiteById(suiteId);
            if (newSuite != null)
            {
                newSuite.AddTestCase(testCase.ITestCase);
                testCase.ITestSuiteBase = newSuite;
            }
        }
    }
}
