// <copyright file="TestCase.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
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
    /// Contains Test Case object information properties
    /// </summary>
    public class TestCase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="testCaseCore">The test case core object.</param>
        /// <param name="testSuiteBaseCore">The test suite base core object.</param>
        public TestCase(ITestCase testCaseCore, ITestSuiteBase testSuiteBaseCore)
        {
            this.ITestCase = testCaseCore;
            this.ITestSuiteBase = testSuiteBaseCore;
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
