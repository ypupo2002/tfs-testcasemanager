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
    public class TestCase : BaseNotifyPropertyChanged
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
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// The area
        /// </summary>
        private string area;

        /// <summary>
        /// The owner
        /// </summary>
        private string ownerName;

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
            this.OwnerName = testCaseCore.OwnerName;
            this.TestSuiteId = testSuiteBaseCore.Id;
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
        public int TestSuiteId { get; set; }

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
        public string OwnerName
        {
            get
            {
                return this.ownerName;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(o => this.OwnerName = o, this.ownerName, "Change the test case owner");
                }                
                this.ownerName = value;
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
    }
}
