// <copyright file="TestCase.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Enums;

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
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="testCaseCore">The test case core object.</param>
        /// <param name="testSuiteBaseCore">The test suite base core object.</param>
        public TestCase(ITestCase testCaseCore, ITestSuiteBase testSuiteBaseCore)
        {
            this.ITestCase = testCaseCore;
            this.ITestSuiteBase = testSuiteBaseCore;
            this.TestCaseId = testCaseCore.Id;
            this.Title = testCaseCore.Title;
            this.Area = testCaseCore.Area;
            this.Priority = (Priority)testCaseCore.Priority;
            this.TeamFoundationIdentityName = new TeamFoundationIdentityName(testCaseCore.OwnerTeamFoundationId, testCaseCore.OwnerName);
            this.OwnerDisplayName = testCaseCore.OwnerName;
            this.TeamFoundationId = testCaseCore.OwnerTeamFoundationId;
            this.TestSuiteId = (testSuiteBaseCore == null) ? null : (int?)testSuiteBaseCore.Id;
            base.isInitialized = true;
            this.Id = testCaseCore.Id;
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
        public int? TestSuiteId { get; set; }
       

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
            this.Priority = (Priority)testBase.Priority;
            this.ITestCase.Title = testBase.Title;
            this.ITestCase.Area = testBase.Area;
            this.ITestCase.Priority = (int)testBase.Priority;
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
