using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    /// <summary>
    /// Contains Test Case object information properties
    /// </summary>
    public class TestCase
    {
        public TestCase(ITestCase iTestCase, ITestSuiteBase iTestSuiteBase)
        {
            ITestCase = iTestCase;
            ITestSuiteBase = iTestSuiteBase;
        }

        /// <summary>
        /// Gets or sets the core test case object.
        /// </summary>
        /// <value>
        /// The core test case object.
        /// </value>
        public ITestCase ITestCase { get; set; }
        /// <summary>
        /// Gets or sets the core test suite object.
        /// </summary>
        /// <value>
        /// The core test suite object.
        /// </value>
        public ITestSuiteBase ITestSuiteBase { get; set; }
    }
}
