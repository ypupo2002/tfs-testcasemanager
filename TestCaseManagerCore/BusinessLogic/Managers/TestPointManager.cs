// <copyright file="TestPointManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Contains helper methods for working with ITestPoint objects
    /// </summary>
    public static class TestPointManager
    {

        /// <summary>
        /// Gets the test points by test case unique identifier.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <returns></returns>
        public static ITestPointCollection GetTestPointsByTestCaseId(int testCaseId)
        {
            return  ExecutionContext.Preferences.TestPlan.QueryTestPoints(string.Format("Select * from TestPoint where TestCaseId = {0} ", testCaseId));
        }
    }
}
