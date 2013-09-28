// <copyright file="TestCase.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Entities;
    using TestCaseManagerApp.BusinessLogic.Enums;
    using UndoMethods;

    /// <summary>
    /// Contains Test Case object information properties
    /// </summary>
    [Serializable]
    public class TestCase : BaseNotifyPropertyChanged, IEquatable<TestCase>
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
        /// The owner
        /// </summary>
        [NonSerialized]
        private TeamFoundationIdentityName teamFoundationIdentityName;

        /// <summary>
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// The area
        /// </summary>
        private string area;        

        /// <summary>
        /// The priority
        /// </summary>
        private Priority priority;

        /// <summary>
        /// The is initialized
        /// </summary>
        private bool isInitialized;

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
            this.isInitialized = true;    
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
        /// Gets the display name of the owner.
        /// </summary>
        /// <value>
        /// The display name of the owner.
        /// </value>
        public string OwnerDisplayName { get; set; }

        /// <summary>
        /// Gets the team foundation unique identifier.
        /// </summary>
        /// <value>
        /// The team foundation unique identifier.
        /// </value>
        public Guid TeamFoundationId { get; set; }

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
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(t => this.Title = t, this.title, "Change the test case title");
                }                
                this.title = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        public string Area
        {
            get
            {
                return this.area;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(a => this.Area = a, this.area, "Change the test case area");
                }               
                this.area = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public TeamFoundationIdentityName TeamFoundationIdentityName
        {
            get
            {
                return this.teamFoundationIdentityName;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(t => this.TeamFoundationIdentityName = t, this.teamFoundationIdentityName, "Change the test case owner");
                }                
                this.teamFoundationIdentityName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public Priority Priority
        {
            get
            {
                return this.priority;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(p => this.Priority = p, this.priority, "Change the test case priority");
                }                
                this.priority = value;
                this.NotifyPropertyChanged();
            }
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
