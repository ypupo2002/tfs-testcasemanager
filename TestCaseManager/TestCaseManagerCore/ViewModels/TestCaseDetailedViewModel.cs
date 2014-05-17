﻿// <copyright file="TestCaseDetailedViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains methods and properties related to the TestCaseDetailed View
    /// </summary>
    public class TestCaseDetailedViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseDetailedViewModel"/> class.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        public TestCaseDetailedViewModel(int testCaseId, int suiteId)
        { 
            ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Find(testCaseId);
            ITestSuiteBase testSuiteBaseCore = null;
            if (suiteId != -1)
            {
                testSuiteBaseCore = ExecutionContext.TestManagementTeamProject.TestSuites.Find(suiteId);
            }

            this.TestCase = new TestCase(testCaseCore, testSuiteBaseCore, ExecutionContext.Preferences.TestPlan);
            this.TestActions = new List<ITestAction>();
            this.TestCase.ITestCase.Actions.ToList().ForEach(x => this.TestActions.Add(x));
            this.ObservableTestSteps = new ObservableCollection<TestStep>();
            List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(ExecutionContext.TestManagementTeamProject, this.TestActions);
            this.AssociatedAutomation = this.TestCase.ITestCase.GetAssociatedAutomation();
            TestStepManager.UpdateGenericSharedSteps(testSteps);
            this.InitializeTestSteps(testSteps);
        }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        /// <value>
        /// The test case.
        /// </value>
        public TestCase TestCase { get; set; }

        /// <summary>
        /// Gets or sets the test actions.
        /// </summary>
        /// <value>
        /// The test actions.
        /// </value>
        public List<ITestAction> TestActions { get; set; }

        /// <summary>
        /// Gets or sets the observable test steps.
        /// </summary>
        /// <value>
        /// The observable test steps.
        /// </value>
        public ObservableCollection<TestStep> ObservableTestSteps { get; set; }

        /// <summary>
        /// Gets or sets the associated automation.
        /// </summary>
        /// <value>
        /// The associated automation.
        /// </value>
        public AssociatedAutomation AssociatedAutomation { get; set; }

        /// <summary>
        /// Filters the initialization test steps.
        /// </summary>
        private void InitializeTestSteps(List<TestStep> testSteps)
        {
            foreach (var curretTestStep in testSteps)
            {
                if (!TestStepManager.IsInitializationTestStep(curretTestStep))
                {
                    this.ObservableTestSteps.Add(curretTestStep);
                }
            }
        }
    }
}