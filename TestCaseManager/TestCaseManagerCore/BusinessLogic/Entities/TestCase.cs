// <copyright file="TestCase.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Enums;
    using TestCaseManagerCore.BusinessLogic.Managers;
    using Fidely.Framework;

    /// <summary>
    /// Contains Test Case object information properties
    /// </summary>
    [Serializable]
    public class TestCase : TestBase, IEquatable<TestCase>
    {
        /// <summary>
        /// The test case core object
        /// </summary>
        [NonSerialized]
        private ITestCase testCaseCore;

        /// <summary>
        /// The test suite base core object
        /// </summary>
        [NonSerialized]
        private ITestSuiteBase testSuiteBaseCore;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase" /> class.
        /// </summary>
        /// <param name="testCaseCore">The test case core object.</param>
        /// <param name="testSuiteBaseCore">The test suite base core object.</param>
        /// <param name="initializeStatus">if set to <c>true</c> [initialize status].</param>
        public TestCase(ITestCase testCaseCore, ITestSuiteBase testSuiteBaseCore, ITestPlan testPlan, bool initializeStatus = true)
        {
            this.ITestCase = testCaseCore;
            this.ITestSuiteBase = testSuiteBaseCore;
            this.TestCaseId = testCaseCore.Id;
            this.Title = testCaseCore.Title;
            this.Area = testCaseCore.Area;
            this.Priority = (Priority)testCaseCore.Priority;
            if (testCaseCore.OwnerTeamFoundationId != default(Guid) && !string.IsNullOrEmpty(testCaseCore.OwnerName))
            {
                this.TeamFoundationIdentityName = new TeamFoundationIdentityName(testCaseCore.OwnerTeamFoundationId, testCaseCore.OwnerName);
            }
            this.OwnerDisplayName = testCaseCore.OwnerName;
            this.TeamFoundationId = testCaseCore.OwnerTeamFoundationId;
            this.TestSuiteId = (testSuiteBaseCore == null) ? null : (int?)testSuiteBaseCore.Id;
            base.isInitialized = true;
            this.Id = testCaseCore.Id;
            this.DateCreated = testCaseCore.DateCreated;
            this.DateModified = testCaseCore.DateModified;
            this.CreatedBy = testCaseCore.WorkItem.CreatedBy;
            if (testSuiteBaseCore != null)
            {
                this.TestSuiteTitle = testSuiteBaseCore.Title;
            }
            if (initializeStatus)
            {
                string mostRecentResult = TestCaseManager.GetMostRecentTestCaseResult(testPlan, this.Id);
                this.LastExecutionOutcome = TestCaseManager.GetTestCaseExecutionType(mostRecentResult);
            }
        }

        /// <summary>
        /// Gets or sets the test case unique identifier.
        /// </summary>
        /// <value>
        /// The test case unique identifier.
        /// </value>
        [Alias("id", Description = "Test Case ID (alias of TestCaseId)")]
        public int TestCaseId { get; set; }

        /// <summary>
        /// Gets or sets the test suite unique identifier.
        /// </summary>
        /// <value>
        /// The test suite unique identifier.
        /// </value>
        public int? TestSuiteId { get; set; }

        /// <summary>
        /// Gets or sets the average execution time.
        /// </summary>
        /// <value>
        /// The average execution time.
        /// </value>
        public string AverageExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets the last execution times tool tip.
        /// </summary>
        /// <value>
        /// The last execution times tool tip.
        /// </value>
        public string LastExecutionTimesToolTip { get; set; }

        /// <summary>
        /// Gets or sets the test suite title.
        /// </summary>
        /// <value>
        /// The test suite title.
        /// </value>
        [Alias("suite", Description = "Test Suite Name")]
        public string TestSuiteTitle { get; set; }

        /// <summary>
        /// Gets or sets the last execution outcome.
        /// </summary>
        /// <value>
        /// The last execution outcome.
        /// </value>
        public TestCaseExecutionType LastExecutionOutcome { get; set; }

        /// <summary>
        /// Gets or sets the core test case object.
        /// </summary>
        /// <value>
        /// The core test case object.
        /// </value>
        public ITestCase ITestCase
        {
            get
            {
                return this.testCaseCore;
            }

            set
            {
                this.testCaseCore = value;
            }
        }

        /// <summary>
        /// Gets or sets the core test suite object.
        /// </summary>
        /// <value>
        /// The core test suite object.
        /// </value>
        public ITestSuiteBase ITestSuiteBase
        {
            get
            {
                return this.testSuiteBaseCore;
            }

            set
            {
                this.testSuiteBaseCore = value;
            }
        }

        /// <summary>
        /// Reinitializes from test base.
        /// </summary>
        /// <param name="testBase">The test base.</param>
        public void ReinitializeFromTestBase(TestBase testBase)
        {
            this.Title = testBase.Title;
            this.Area = testBase.Area;
            this.Priority = testBase.Priority;
            this.ITestCase.Title = testBase.Title;
            this.ITestCase.Area = testBase.Area;
            this.ITestCase.Priority = (int)testBase.Priority;
            this.DateCreated = testBase.DateCreated;
            this.DateModified = testBase.DateModified;
            this.CreatedBy = testBase.CreatedBy;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TestCase other)
        {
            return this.TestCaseId.Equals(other.TestCaseId);
        }
    }
}