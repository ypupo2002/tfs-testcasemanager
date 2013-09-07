using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManagerApp.BusinessLogic.Entities
{
    /// <summary>
    /// Contains Test object information properties
    /// </summary>
    public class Test
    {
        public Test(string fullName, string className, Guid methodId)
        {
            this.FullName = fullName;
            this.ClassName = className;
            this.MethodId = methodId;
        }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }
        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName { get; set; }
        /// <summary>
        /// Gets or sets the method unique identifier.
        /// </summary>
        /// <value>
        /// The method unique identifier.
        /// </value>
        public Guid MethodId { get; set; }
    }
}
