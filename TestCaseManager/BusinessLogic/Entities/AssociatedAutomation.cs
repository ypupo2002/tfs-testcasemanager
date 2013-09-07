using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManagerApp
{
    /// <summary>
    /// Contains Associated Automation information properties
    /// </summary>
    public class AssociatedAutomation
    {
        public const string NONE = "None";     

        public AssociatedAutomation()
        {
            TestName = NONE;
            Assembly = NONE;
            Type = NONE;
        }

        public AssociatedAutomation(string testInfo)
        {
            string[] infos = testInfo.Split(',');
            TestName = infos[1];
            Assembly = infos[2];
            Type = infos[3];
        }

        /// <summary>
        /// Gets or sets the type of the test.
        /// </summary>
        /// <value>
        /// The test type.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the assembly of the associated test.
        /// </summary>
        /// <value>
        /// the assembly of the associated test.
        /// </value>
        public string Assembly { get; set; }
        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        /// <value>
        /// The name of the test.
        /// </value>
        public string TestName { get; set; }
    }
}
