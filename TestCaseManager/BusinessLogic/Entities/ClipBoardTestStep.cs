// <copyright file="ClipBoardTestStep.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using TestCaseManagerApp.BusinessLogic.Enums;

    /// <summary>
    /// Helper class which contains test steps which will be moved to clipboard + clipboard mode: copy or cut
    /// </summary>
    [Serializable]
    public class ClipBoardTestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClipBoardTestStep"/> class.
        /// </summary>
        /// <param name="testCases">The test steps.</param>
        /// <param name="clipBoardCommand">The clip board command.</param>
        public ClipBoardTestStep(List<TestStep> testSteps, ClipBoardCommand clipBoardCommand)
        {
            this.TestSteps = testSteps;
            this.ClipBoardCommand = clipBoardCommand;
        }

        /// <summary>
        /// Gets or sets the test cases.
        /// </summary>
        /// <value>
        /// The test cases.
        /// </value>
        public List<TestStep> TestSteps { get; set; }

        /// <summary>
        /// Gets or sets the clip board command.
        /// </summary>
        /// <value>
        /// The clip board command.
        /// </value>
        public ClipBoardCommand ClipBoardCommand { get; set; }
    }
}
