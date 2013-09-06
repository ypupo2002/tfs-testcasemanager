using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp.ViewModels
{
    public class TestCaseDetailedViewModel
    {
        public TestCaseDetailedViewModel(int testCaseId, int suiteId)
        {            
            ITestCase iTestCase = ExecutionContext.TeamProject.TestCases.Find(testCaseId);
            ITestSuiteBase iTestSuiteBase = ExecutionContext.TeamProject.TestSuites.Find(suiteId);
            TestCase = new TestCase(iTestCase, iTestSuiteBase);
            TestActions = new List<ITestAction>();
            TestCase.ITestCase.Actions.ToList().ForEach(x => TestActions.Add(x));
            ObservableTestSteps = new ObservableCollection<TestStep>();
            TestActions.GetTestSteps().ForEach(x => ObservableTestSteps.Add(x));
            this.AssociatedAutomation = TestCase.ITestCase.GetAssociatedAutomation();
        }        

        public TestCase TestCase { get; set; }
        public List<ITestAction> TestActions { get; set; }
        public ObservableCollection<TestStep> ObservableTestSteps { get; set; }
        public AssociatedAutomation AssociatedAutomation { get; set; }
    }
}
