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
    public class TestStep : BaseNotifyPropertyChanged, ICloneable, IEquatable<TestStep>
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
        /// The action title
        /// </summary>
        private string actionTitle;

        /// <summary>
        /// The action expected result
        /// </summary>
        private string actionExpectedResult;

        /// <summary>
        /// The test step unique identifier
        /// </summary>
        private Guid testStepGuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep" /> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="title">The title.</param>
        /// <param name="testStepGuid">The test step unique identifier.</param>
        public TestStep(bool isShared, string title, Guid testStepGuid)
        {
            this.IsShared = isShared;
            this.Title = title;
            this.TestStepGuid = testStepGuid;
            this.IsPasteEnabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep" /> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="title">The title.</param>
        /// <param name="testStepGuid">The test step unique identifier.</param>
        /// <param name="testStepId">The test step unique identifier.</param>
        /// <param name="actionTitle">The action title.</param>
        /// <param name="actionExpectedResult">The action expected result.</param>
        public TestStep(bool isShared, string title, Guid testStepGuid, int testStepId, string actionTitle, string actionExpectedResult)
            : this(isShared, title, testStepGuid)
        {
            this.TestStepId = testStepId;
            this.ActionTitle = actionTitle;
            this.ActionExpectedResult = actionExpectedResult;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep" /> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="title">The title.</param>
        /// <param name="testStepGuid">The test step unique identifier.</param>
        /// <param name="testStepCore">The test step core.</param>
        public TestStep(bool isShared, string title, Guid testStepGuid, ITestStep testStepCore)
            : this(isShared, title, testStepGuid)
        {
            this.TestStepId = testStepCore.Id;
            this.ActionTitle = testStepCore.Title.ToPlainText();
            this.ActionExpectedResult = testStepCore.ExpectedResult.ToPlainText();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep" /> class.
        /// </summary>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="title">The title.</param>
        /// <param name="testStepGuid">The test step unique identifier.</param>
        /// <param name="testStepCore">The test step core.</param>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        public TestStep(bool isShared, string title, Guid testStepGuid, ITestStep testStepCore, int sharedStepId)
            : this(isShared, title, testStepGuid, testStepCore)
        {
            this.SharedStepId = sharedStepId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep"/> class.
        /// </summary>
        /// <param name="otherTestStep">The other test step.</param>
        public TestStep(TestStep otherTestStep)
            : this(otherTestStep.IsShared, otherTestStep.Title, otherTestStep.TestStepGuid, otherTestStep.TestStepId,
                   otherTestStep.ActionTitle, otherTestStep.ActionExpectedResult)
        {
            this.SharedStepId = otherTestStep.SharedStepId;
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
        public Guid TestStepGuid 
        {
            get
            {
                return this.testStepGuid;
            }

            set
            {
                if (value == default(Guid))
                {
                    this.testStepGuid = Guid.NewGuid();
                }
                else
                {
                    this.testStepGuid = value;
                }
            }
        }

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

        /// <summary>
        /// Gets or sets the action title.
        /// </summary>
        /// <value>
        /// The action title.
        /// </value>
        public string ActionTitle
        {
            get
            {
                return this.actionTitle;
            }

            set
            {
                this.actionTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the action expected result.
        /// </summary>
        /// <value>
        /// The action expected result.
        /// </value>
        public string ActionExpectedResult
        {
            get
            {
                return this.actionExpectedResult;
            }

            set
            {
                this.actionExpectedResult = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            TestStep clonedTestStep = new TestStep(this);
            clonedTestStep.TestStepGuid = Guid.NewGuid();

            return clonedTestStep;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TestStep other)
        {
            bool result = this.TestStepGuid.Equals(other.TestStepGuid) && 
                this.ActionTitle.Equals(other.ActionTitle) &&
                this.ActionExpectedResult.Equals(other.ActionExpectedResult) &&
                this.IsShared.Equals(other.IsShared) &&
                this.SharedStepId.Equals(other.SharedStepId);

            return result;
        }
    }
}
