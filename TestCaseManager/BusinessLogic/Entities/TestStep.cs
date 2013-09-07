using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    /// <summary>
    /// Contains Test Step object information properties
    /// </summary>
    public class TestStep
    {
        public TestStep(bool isShared, ITestStep iTestStep, string title, string guid)
        {
            IsShared = isShared;
            ITestStep = iTestStep;
            Title = title;
            StepGuid = guid;
        }
        
        public TestStep(bool isShared, ITestStep iTestStep, string title, int sharedStepId, string guid)
            : this(isShared, iTestStep, title, guid)
        {
            SharedStepId = sharedStepId;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is shared].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is shared]; otherwise, <c>false</c>.
        /// </value>
        public bool IsShared { get; set; }
        /// <summary>
        /// Gets or sets the core test step object.
        /// </summary>
        /// <value>
        /// The core test step object.
        /// </value>
        public ITestStep ITestStep { get; set; }
        /// <summary>
        /// Gets or sets the test step title.
        /// </summary>
        /// <value>
        /// The test step title.
        /// </value>
        public string Title { get; set; }
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
    }
}
