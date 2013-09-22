// <copyright file="LightTestCase.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System;

    /// <summary>
    /// Contains Test Case object information properties. Light Version used for copy commands.
    /// </summary>
    [Serializable]
    public class LightTestCase : IEquatable<LightTestCase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightTestCase"/> class.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="parentSuiteId">The parent suite unique identifier.</param>
        public LightTestCase(int testCaseId, int parentSuiteId)
        {
            this.TestCaseId = testCaseId;
            this.ParentSuiteId = parentSuiteId;
        }

        /// <summary>
        /// Gets or sets the test case unique identifier.
        /// </summary>
        /// <value>
        /// The test case unique identifier.
        /// </value>
        public int TestCaseId { get; set; }

        /// <summary>
        /// Gets or sets the parent suite unique identifier.
        /// </summary>
        /// <value>
        /// The parent suite unique identifier.
        /// </value>
        public int ParentSuiteId { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(LightTestCase other)
        {
            return this.TestCaseId.Equals(other.TestCaseId);
        }
    }
}
