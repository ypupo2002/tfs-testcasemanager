// <copyright file="SharedStep.cs" company="CodePlex">
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

    /// <summary>
    /// Contains information about Shared Step object
    /// </summary>
    public class SharedStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedStep"/> class.
        /// </summary>
        /// <param name="sharedStepCore">The shared step core object.</param>
        public SharedStep(ISharedStep sharedStepCore)
        {
            this.ISharedStep = sharedStepCore;
            List<TestStep> allTestSteps = TestStepManager.GetAllTestStepsInSharedStep(sharedStepCore);
            this.StepsToolTip = TestStepManager.GenerateTestStepsText(allTestSteps);
        }

        /// <summary>
        /// Gets or sets the attribute shared step core object.
        /// </summary>
        /// <value>
        /// The attribute shared step core object.
        /// </value>
        public ISharedStep ISharedStep { get; set; }

        /// <summary>
        /// Gets or sets the shared steps tool tip.
        /// </summary>
        /// <value>
        /// The shared steps tool tip.
        /// </value>
        public string StepsToolTip { get; set; }
    }
}
