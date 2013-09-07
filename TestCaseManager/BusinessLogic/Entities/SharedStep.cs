using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    /// <summary>
    /// Contains information about Shared Step object
    /// </summary>
    public class SharedStep
    {     
        public SharedStep(ISharedStep iSharedStep)
        {
            ISharedStep = iSharedStep;
            List<TestStep> allTestSteps = TestStepManager.GetAllTestStepsInSharedStep(iSharedStep);
            StepsToolTip = TestStepManager.GenerateTestStepsText(allTestSteps);
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
