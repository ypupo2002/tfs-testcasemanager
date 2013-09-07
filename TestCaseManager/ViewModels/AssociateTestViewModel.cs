using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.BusinessLogic.Entities;
using TestCaseManagerApp.BusinessLogic.Managers;

namespace TestCaseManagerApp.ViewModels
{
    public class AssociateTestViewModel
    {
        public AssociateTestViewModel(int testCaseId)
        {            
            ITestCase iTestCase = ExecutionContext.TestManagementTeamProject.TestCases.Find(testCaseId);
            TestCase = new TestCase(iTestCase, null);
            TestCaseId = testCaseId;
            List<Test> testsList = ProjectManager.GetTests(ExecutionContext.ProjectDllPath);
            ObservableTests = new ObservableCollection<Test>();
            testsList.ForEach(t => ObservableTests.Add(t));
            InitializeInitialTestsCollection();
            TestTypes = new List<string>()
            {
                "Small Integration Test", "Unit Test", "Large Integration Test"
            };
        }

        public int TestCaseId { get; set; }
        public int TestSuiteId { get; set; }
        public bool CreateNew { get; set; }
        public bool Duplicate { get; set; }
        public ObservableCollection<Test> ObservableTests { get; set; }
        public ObservableCollection<Test> InitialTestsCollection { get; set; }
        public List<string> TestTypes { get; set; }
        public TestCase TestCase { get; set; }
        public AssociatedAutomation AssociatedAutomation { get; set; }
        public bool fullNameFlag;
        public bool classNameFlag;

        public void AssociateTestCaseToTest(Test test, string testType)
        {
            TestCase.ITestCase.SetAssociatedAutomation(test, testType);
            TestCase.ITestCase.Save();
        }

        private void InitializeInitialTestsCollection()
        {
            InitialTestsCollection = new ObservableCollection<Test>();
            foreach (var cTest in ObservableTests)
            {
                InitialTestsCollection.Add(cTest);
            }
        }       
    }
}
