// <copyright file="AssociateTestViewModel.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.ViewModels
{
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

    /// <summary>
    /// Contains properties and logic related to the association of tests to test cases
    /// </summary>
    public class AssociateTestViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssociateTestViewModel"/> class.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        public AssociateTestViewModel(int testCaseId)
        {            
            ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Find(testCaseId);
            this.TestCase = new TestCase(testCaseCore, null);
            this.TestCaseId = testCaseId;
            List<Test> testsList = ProjectManager.GetTests(ExecutionContext.ProjectDllPath);
            this.ObservableTests = new ObservableCollection<Test>();
            this.AssociateTestViewFilters = new AssociateTestViewFilters();
            testsList.ForEach(t => this.ObservableTests.Add(t));
            this.InitializeInitialTestsCollection();
            this.TestTypes = new List<string>()
            {
                "Small Integration Test", "Unit Test", "Large Integration Test"
            };
        }

        /// <summary>
        /// Gets or sets the test case unique identifier.
        /// </summary>
        /// <value>
        /// The test case unique identifier.
        /// </value>
        public int TestCaseId { get; set; }

        /// <summary>
        /// Gets or sets the test suite unique identifier.
        /// </summary>
        /// <value>
        /// The test suite unique identifier.
        /// </value>
        public int TestSuiteId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [create new].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [create new]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateNew { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [duplicate].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [duplicate]; otherwise, <c>false</c>.
        /// </value>
        public bool Duplicate { get; set; }

        /// <summary>
        /// Gets or sets the observable tests. This collection is bind to the UI Grids.
        /// </summary>
        /// <value>
        /// The observable tests.
        /// </value>
        public ObservableCollection<Test> ObservableTests { get; set; }

        /// <summary>
        /// Gets or sets the initial tests collection. This collection is used to restore the default search collection after deleting search criterias.
        /// </summary>
        /// <value>
        /// The initial tests collection.
        /// </value>
        public ObservableCollection<Test> InitialTestsCollection { get; set; }

        /// <summary>
        /// Gets or sets the test types. Unit Test, Integration Test in the large/small.
        /// </summary>
        /// <value>
        /// The test types.
        /// </value>
        public List<string> TestTypes { get; set; }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        /// <value>
        /// The test case.
        /// </value>
        public TestCase TestCase { get; set; }

        /// <summary>
        /// Gets or sets the associated automation.
        /// </summary>
        /// <value>
        /// The associated automation.
        /// </value>
        public AssociatedAutomation AssociatedAutomation { get; set; }

        /// <summary>
        /// Gets or sets the associate test view filters.
        /// </summary>
        /// <value>
        /// The associate test view filters.
        /// </value>
        public AssociateTestViewFilters AssociateTestViewFilters { get; set; }

        /// <summary>
        /// Associates the test case automatic test.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <param name="testType">Type of the test.</param>
        public void AssociateTestCaseToTest(Test test, string testType)
        {
            this.TestCase.ITestCase.SetAssociatedAutomation(test, testType);
            this.TestCase.ITestCase.Save();
        }

        /// <summary>
        /// Initializes the initial tests collection.
        /// </summary>
        private void InitializeInitialTestsCollection()
        {
            this.InitialTestsCollection = new ObservableCollection<Test>();
            foreach (var currentTest in this.ObservableTests)
            {
                this.InitialTestsCollection.Add(currentTest);
            }
        }       
    }
}