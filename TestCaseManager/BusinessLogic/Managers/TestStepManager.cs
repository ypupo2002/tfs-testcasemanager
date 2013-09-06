using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.BusinessLogic.Entities;

namespace TestCaseManagerApp
{
    public static class TestStepManager
    {
        public static List<TestStep> GetTestSteps(this List<ITestAction> testActions, Dictionary<int, string> alreadyAddedSharedSteps = null, List<SharedStep> sharedSteps = null)
        {
            if (alreadyAddedSharedSteps == null)
                alreadyAddedSharedSteps = new Dictionary<int, string>();
            List<TestStep> testSteps = new List<TestStep>();           
            foreach (var currentAction in testActions)
            {
                if (currentAction is ITestStep)
                {
                    string stepGuid = Guid.NewGuid().ToString();
                    testSteps.Add(new TestStep(false, (currentAction as ITestStep), String.Empty, stepGuid));
                }
                else if (currentAction is ISharedStepReference)
                {
                    ISharedStepReference currentSharedStepReference = currentAction as ISharedStepReference;
                    ISharedStep currentSharedStep = ExecutionContext.TeamProject.SharedSteps.Find(currentSharedStepReference.SharedStepId);
                    string sharedStepGuid = GetSharedStepGuid(alreadyAddedSharedSteps, currentSharedStep);
                 
                    if (sharedSteps != null)                    
                        sharedSteps.Add(new SharedStep(currentSharedStep));
                    testSteps.AddRange(currentSharedStep.GetInnerTestSteps(sharedStepGuid));
                }
            }
            return testSteps;
        }

        public static string GetSharedStepGuid(Dictionary<int, string> alreadyAddedSharedSteps, ISharedStep currentSharedStep)
        {
            string sharedStepUniqueGuid = Guid.NewGuid().ToString();
            //if (!alreadyAddedSharedSteps.ContainsKey(currentSharedStep.Id))
            //    alreadyAddedSharedSteps.Add(currentSharedStep.Id, sharedStepUniqueGuid);
            //else
            //    sharedStepUniqueGuid = alreadyAddedSharedSteps[currentSharedStep.Id];
            return sharedStepUniqueGuid;
        }

        public static bool IsThereSharedStepSelected(this List<TestStep> selectedTestSteps)
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

        public static List<TestStep> GetInnerTestSteps(this ISharedStep currentSharedStep, string sharedStepUniqueGuid = "")
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

        public static string GenerateTestStepsText(this List<TestStep> testSteps)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TestStep currentTestStep in testSteps)
            {
                sb.AppendLine(String.Format("{0}   {1}", currentTestStep.ITestStep.Title, currentTestStep.ITestStep.ExpectedResult));
                sb.AppendLine(new String('*', 20));
            }

            string result = sb.ToString();
            return result;
        }

        public static TestStep CreateNewTestStep(this TestCase testCase, string step, string expectedResult, string guid = "")
        {
            ITestStep iTestStep = testCase.ITestCase.CreateTestStep();
            iTestStep.ExpectedResult = expectedResult;
            iTestStep.Title = step;
            if (String.IsNullOrEmpty(guid))
                guid = Guid.NewGuid().ToString();
            TestStep testStepToInsert = new TestStep(false, iTestStep, String.Empty, guid);

            return testStepToInsert;
        }

        public static ISharedStep CreateNewSharedTestStep(this TestCase testCase, string sharedStepTitle, string stepTitle, string expectedResult)
        {
            ISharedStepReference iSharedStepReference = testCase.ITestCase.CreateSharedStepReference();
            ISharedStep iSharedStep = ExecutionContext.TeamProject.SharedSteps.Create();
            iSharedStepReference.SharedStepId = iSharedStep.Id;
            iSharedStep.Title = sharedStepTitle;
            ITestStep iTestStep = iSharedStep.CreateTestStep();
            iTestStep.ExpectedResult = expectedResult;
            iTestStep.Title = stepTitle;
            iSharedStep.Actions.Add(iTestStep);
            //TestStep testStepToInsert = new TestStep(true, iTestStep, iSharedStep.Title, Guid.NewGuid().ToString());

            return iSharedStep;
        }

        public static ISharedStep CreateNewSharedTestStep(this TestCase testCase, string sharedStepTitle, List<TestStep> selectedTestSteps)
        {
            //ISharedStepReference iSharedStepReference = testCase.ITestCase.CreateSharedStepReference();
            ISharedStep iSharedStep = ExecutionContext.TeamProject.SharedSteps.Create();
            //iSharedStepReference.SharedStepId = iSharedStep.Id;
            iSharedStep.Title = sharedStepTitle;
            string sharedStepGuid = Guid.NewGuid().ToString();
            iSharedStep.Save();
            CreateSharedStepStepsFromSelected(selectedTestSteps, iSharedStep, sharedStepGuid, sharedStepTitle);
            iSharedStep.Save();

            return iSharedStep;
        }

        private static void CreateSharedStepStepsFromSelected(List<TestStep> selectedTestSteps, ISharedStep iSharedStep, string sharedStepGuid, string sharedStepTitle)
        {
            foreach (TestStep currentTestStep in selectedTestSteps)
            {
                ITestStep iTestStep = iSharedStep.CreateTestStep();
                iTestStep.ExpectedResult = currentTestStep.ITestStep.ExpectedResult;
                iTestStep.Title = currentTestStep.ITestStep.Title;
                iSharedStep.Actions.Add(iTestStep);
                currentTestStep.StepGuid = sharedStepGuid;
                currentTestStep.Title = sharedStepTitle;
                currentTestStep.IsShared = true;
                currentTestStep.SharedStepId = iSharedStep.Id;
            }
        }      

        public static List<ISharedStep> GetSharedTestSteps()
        {
            return ExecutionContext.TeamProject.SharedSteps.Query("select * from WorkItems where [System.TeamProject] = @project and [System.WorkItemType] = 'Shared Steps'").ToList();
        }
    }
}
