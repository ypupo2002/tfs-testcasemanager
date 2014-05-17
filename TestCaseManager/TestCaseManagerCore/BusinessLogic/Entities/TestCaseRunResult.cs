// <copyright file="TestCaseRunResult.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    /// <summary>
    /// Contains Test Case Run Result Times
    /// </summary>
    public class TestCaseRunResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseRunResult" /> class.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="runBy">The run by.</param>
        public TestCaseRunResult(DateTime startDate, DateTime endDate, TimeSpan duration, string runBy)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Duration = duration;
            this.RunBy = runBy;
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the run by.
        /// </summary>
        /// <value>
        /// The run by.
        /// </value>
        public string RunBy { get; set; }   
    }
}
