using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManagerApp.BusinessLogic.Entities
{
    /// <summary>
    /// Contains old/new id pair used to change specific test case actions
    /// </summary>
    public class SharedStepIdReplacePair
    {
        /// <summary>
        /// Gets or sets the old shared step unique identifier.
        /// </summary>
        /// <value>
        /// The old shared step unique identifier.
        /// </value>
        public int OldSharedStepId { get; set; }
        /// <summary>
        /// Gets or sets the new shared step unique identifier.
        /// </summary>
        /// <value>
        /// The new shared step unique identifier.
        /// </value>
        public int NewSharedStepId { get; set; }
    }
}
