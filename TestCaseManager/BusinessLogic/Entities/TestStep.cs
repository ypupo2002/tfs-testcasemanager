// <copyright file="TestStep.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Entities;

    /// <summary>
    /// Contains Test Step object information properties
    /// </summary>
    [Serializable]
    public class TestStep : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// The is paste enabled
        /// </summary>
        private bool isPasteEnabled;

        /// <summary>
        /// The test step core
        /// </summary>
        [NonSerialized]
        private ITestStep testStepCore;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep" /> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="testStepCore">The attribute test step.</param>
        /// <param name="title">The title.</param>
        /// <param name="guid">The unique identifier.</param>
        public TestStep(bool isShared, ITestStep testStepCore, string title, string guid)
        {
            this.IsShared = isShared;
            this.ITestStep = testStepCore;
            this.Title = title;
            this.StepGuid = guid;
            this.TestStepId = testStepCore.Id;
            this.IsPasteEnabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep"/> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="testStepCore">The test step core.</param>
        /// <param name="title">The title.</param>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        /// <param name="guid">The unique identifier.</param>
        public TestStep(bool isShared, ITestStep testStepCore, string title, int sharedStepId, string guid)
            : this(isShared, testStepCore, title, guid)
        {
            this.SharedStepId = sharedStepId;
        }

        /// <summary>
        /// Gets or sets the test step unique identifier.
        /// </summary>
        /// <value>
        /// The test step unique identifier.
        /// </value>
        public int TestStepId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is shared].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is shared]; otherwise, <c>false</c>.
        /// </value>
        public bool IsShared { get; set; }

        /// <summary>
        /// Gets or sets the shared step unique identifier.
        /// </summary>
        /// <value>
        /// The shared step unique identifier.
        /// </value>
        public int SharedStepId { get; set; }

        /// <summary>
        /// Gets or sets the step unique identifier.
        /// </summary>
        /// <value>
        /// The step unique identifier.
        /// </value>
        public string StepGuid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is pate enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is pate enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsPasteEnabled
        {
            get
            {
                return this.isPasteEnabled;
            }

            set
            {
                this.isPasteEnabled = value;
                this.NotifyPropertyChanged();
            }
        }


        /// <summary>
        /// Gets or sets the core test step object.
        /// </summary>
        /// <value>
        /// The core test step object.
        /// </value>
        public ITestStep ITestStep
        {
            get
            {
                return this.testStepCore;
            }

            set
            {
                this.testStepCore = value;
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
                this.title = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
