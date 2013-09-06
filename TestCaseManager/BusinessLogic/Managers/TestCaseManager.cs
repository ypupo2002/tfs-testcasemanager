using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.BusinessLogic.Entities;

namespace TestCaseManagerApp
{
    public static class TestCaseManager
    {
        public static List<TestCase> GetSuiteEntries(this ITestSuiteCollection suiteEntries)
        {
            List<TestCase> testCases = new List<TestCase>();
            foreach (ITestSuiteBase suite in suiteEntries)
            {
                if (suite != null)
                {
                    suite.Refresh();
                    foreach (var tc in suite.TestCases)
                    {
                        testCases.Add(new TestCase(tc.TestCase, suite));
                    }
                    if (suite.TestSuiteType == TestSuiteType.StaticTestSuite)
                    {
                        IStaticTestSuite suite1 = suite as IStaticTestSuite;
                        if (suite1 != null && (suite1.SubSuites.Count > 0))
                        {
                            testCases.AddRange(suite1.SubSuites.GetSuiteEntries());
                        }
                    }
                }
            }
            return testCases;
        }

        public static AssociatedAutomation GetAssociatedAutomation(this ITestCase testCase)
        {
            AssociatedAutomation associatedAutomation;
            if (testCase.Implementation == null)
                associatedAutomation = new AssociatedAutomation();
            else
                associatedAutomation = new AssociatedAutomation(testCase.Implementation.ToString());

            return associatedAutomation;
        }

        public static void SetAssociatedAutomation(this ITestCase testCase, Test test, string testType)
        {
            try
            {
                ITmiTestImplementation imp = ExecutionContext.TeamProject.CreateTmiTestImplementation(test.FullName, testType, test.ClassName, test.MethodId);
                testCase.Implementation = imp;
            }
            catch(NullReferenceException)
            {
                //TODO add exception handling
            }
        }

        public static List<TestCase> GetAllTestCases()
        {
            ExecutionContext.Preferences.TestPlan = TestPlanManager.GetAllTestPlans(ExecutionContext.TeamProject).Where(p => p.Name.Equals(ExecutionContext.Preferences.TestPlan.Name)).ToList()[0];
            ExecutionContext.Preferences.TestPlan.Refresh();
            List<TestCase> testCasesList = ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites.GetSuiteEntries();

            return testCasesList;
        }

        public static TestCase SaveTestCase(this TestCase testCase, bool createNew, int priority, string newSuiteTitle, List<TestStep> testSteps)
        {
            TestCase currentTestCase = testCase;
            if (createNew)
            {
                ITestCase iTestCase = ExecutionContext.TeamProject.TestCases.Create();
                currentTestCase = new TestCase(iTestCase, testCase.ITestSuiteBase);
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
                    ISharedStep sharedStep = ExecutionContext.TeamProject.SharedSteps.Find(currentStep.SharedStepId);
                    //currentTestCase.ITestCase.Actions.Add(sharedStep as ITestAction);
                    ISharedStepReference iSharedStepReference = currentTestCase.ITestCase.CreateSharedStepReference();
                    iSharedStepReference.SharedStepId = sharedStep.Id;
                    currentTestCase.ITestCase.Actions.Add(iSharedStepReference);
                    addedSharedStepGuids.Add(currentStep.StepGuid);
                }
                else if (!currentStep.IsShared)
                {
                    ITestStep iTestStep = currentTestCase.ITestCase.CreateTestStep();
                    iTestStep.Title = currentStep.ITestStep.Title;
                    iTestStep.ExpectedResult = currentStep.ITestStep.ExpectedResult;
                    currentTestCase.ITestCase.Actions.Add(iTestStep);
                }
            }
            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();
            TestSuiteManager.RemoveTestCase(currentTestCase.ITestCase);

            var newSuite = TestSuiteManager.GetSuiteByName(newSuiteTitle);
            newSuite.AddTestCase(currentTestCase.ITestCase);

            return currentTestCase;
        }

        public static void DuplicateTestCase(this TestCase testCase, List<TextReplacePair> textReplacePairs, List<SharedStepIdReplacePair> sharedStepIdReplacePair, string newSuiteTitle, bool replaceInTitles, bool replaceSharedSteps, bool replaceInSteps)
        {
            List<TestStep> testSteps = testCase.ITestCase.Actions.ToList().GetTestSteps();
            ITestCase iTestCase = ExecutionContext.TeamProject.TestCases.Create();
            TestCase currentTestCase = new TestCase(iTestCase, testCase.ITestSuiteBase);

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
            ReplaceStepsInTestCase(testCase, textReplacePairs, sharedStepIdReplacePair, testSteps, replaceSharedSteps, replaceInSteps);
            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();

            var newSuite = TestSuiteManager.GetSuiteByName(newSuiteTitle);
            newSuite.AddTestCase(currentTestCase.ITestCase);
        }

        public static void FindAndReplaceInTestCase(this TestCase testCase, List<TextReplacePair> textReplacePairs, List<SharedStepIdReplacePair> sharedStepIdReplacePair, bool replaceInTitles, bool replaceSharedSteps, bool replaceInSteps)
        {
            TestCase currentTestCase = testCase;
            currentTestCase.ITestCase = ExecutionContext.TeamProject.TestCases.Find(testCase.ITestCase.Id);
            List<TestStep> testSteps = currentTestCase.ITestCase.Actions.ToList().GetTestSteps();
            if (replaceInTitles)
            {
                string newTitle = currentTestCase.ITestCase.Title.ReplaceAll(textReplacePairs);
                currentTestCase.ITestCase.Title = newTitle;
            }
            ReplaceStepsInTestCase(currentTestCase, textReplacePairs, sharedStepIdReplacePair, testSteps, replaceSharedSteps, replaceInSteps);

            currentTestCase.ITestCase.Flush();
            currentTestCase.ITestCase.Save();
        }

        private static void ReplaceStepsInTestCase(TestCase testCase, List<TextReplacePair> textReplacePairs, List<SharedStepIdReplacePair> sharedStepIdReplacePair, List<TestStep> testSteps, bool replaceSharedSteps, bool replaceInSteps)
        {
            if (replaceSharedSteps || replaceInSteps)
            {
                testCase.ITestCase.Actions.Clear();
                List<string> addedSharedStepGuids = new List<string>();

                foreach (TestStep currentStep in testSteps)
                {
                    if (currentStep.IsShared && !addedSharedStepGuids.Contains(currentStep.StepGuid) && replaceSharedSteps)
                    {
                        int newSharedStepId = GetNewSharedStepId(currentStep.SharedStepId, sharedStepIdReplacePair);
                        if (!replaceSharedSteps)
                        {
                            newSharedStepId = currentStep.SharedStepId;
                        }
                        ISharedStep sharedStep = ExecutionContext.TeamProject.SharedSteps.Find(newSharedStepId);
                        ISharedStepReference iSharedStepReference = testCase.ITestCase.CreateSharedStepReference();
                        iSharedStepReference.SharedStepId = sharedStep.Id;
                        testCase.ITestCase.Actions.Add(iSharedStepReference);
                        addedSharedStepGuids.Add(currentStep.StepGuid);
                    }
                    else if (!currentStep.IsShared)
                    {
                        ITestStep iTestStep = testCase.ITestCase.CreateTestStep();
                        if (replaceInSteps)
                        {
                            iTestStep.Title = currentStep.ITestStep.Title.ToString().ReplaceAll(textReplacePairs);
                            iTestStep.ExpectedResult = currentStep.ITestStep.ExpectedResult.ToString().ReplaceAll(textReplacePairs);
                        }
                        else
                        {
                            iTestStep.Title = currentStep.ITestStep.Title;
                            iTestStep.ExpectedResult = currentStep.ITestStep.ExpectedResult;
                        }
                        testCase.ITestCase.Actions.Add(iTestStep);
                    }
                }
            }
        }

        private static int GetNewSharedStepId(int currentSharedStepId, List<SharedStepIdReplacePair> sharedStepIdReplacePair)
        {
            int newSharedStepId = currentSharedStepId;
            foreach (SharedStepIdReplacePair currentPair in sharedStepIdReplacePair)
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
