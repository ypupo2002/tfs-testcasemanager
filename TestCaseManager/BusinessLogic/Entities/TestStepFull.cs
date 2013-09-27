﻿// <copyright file="TestStepFull.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Extends the Test Step object- adds index
    /// </summary>
    public class TestStepFull : TestStep, IEquatable<TestStepFull>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestStepFull"/> class.
        /// </summary>
        /// <param name="testStep">The test step.</param>
        /// <param name="index">The index.</param>
        public TestStepFull(TestStep testStep, int index)
            : base(testStep)
        {
            this.Index = index;
        }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(TestStepFull other)
        {
            bool result = base.Equals(other);

            return result;
        }
    }
}