﻿// <copyright file="AssociatedAutomation.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains Associated Automation information properties
    /// </summary>
    public class AssociatedAutomation
    {
        /// <summary>
        /// The none text constant
        /// </summary>
        public const string NONE = "None";

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociatedAutomation"/> class.
        /// </summary>
        public AssociatedAutomation()
        {
            this.TestName = NONE;
            this.Assembly = NONE;
            this.Type = NONE;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociatedAutomation"/> class.
        /// </summary>
        /// <param name="testInfo">The test information.</param>
        public AssociatedAutomation(string testInfo)
        {
            string[] infos = testInfo.Split(',');
            this.TestName = infos[1];
            this.Assembly = infos[2];
            this.Type = infos[3];
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
