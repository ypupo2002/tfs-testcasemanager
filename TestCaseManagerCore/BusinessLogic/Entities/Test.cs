﻿// <copyright file="Test.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains Test object information properties
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodId">The method unique identifier.</param>
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