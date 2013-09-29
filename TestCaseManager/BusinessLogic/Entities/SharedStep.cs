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
    using TestCaseManagerApp.BusinessLogic.Enums;

    /// <summary>
    /// Contains information about Shared Step object
    /// </summary>
    public class SharedStep : TestBase
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

            this.Title = sharedStepCore.Title;
            this.Area = sharedStepCore.Area;
            this.Priority = (Priority)sharedStepCore.Priority;
            this.TeamFoundationIdentityName = new TeamFoundationIdentityName(sharedStepCore.OwnerTeamFoundationId, sharedStepCore.OwnerName);
            this.OwnerDisplayName = sharedStepCore.OwnerName;
            this.TeamFoundationId = sharedStepCore.OwnerTeamFoundationId;
            base.isInitialized = true;
            this.Id = sharedStepCore.Id;
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
