// <copyright file="TestPlanManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
    using System.Threading;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Contains helper methods for working with ITestPlan objects
    /// </summary>
    public static class TestPlanManager
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets TestPlan by name.
        /// </summary>
        /// <param name="testManagementTeamProject">TFS team project</param>
        /// <param name="testPlanName">Name of the test plan.</param>
        /// <returns>the found test plan</returns>
        public static ITestPlan GetTestPlanByName(ITestManagementTeamProject testManagementTeamProject, string testPlanName)
        {
            ITestPlanCollection testPlans = GetAllTestPlans(TestCaseManagerCore.ExecutionContext.TestManagementTeamProject);
            ITestPlan testPlan = null;
            if (testPlans != null)
            {
                foreach (ITestPlan currentTestPlan in testPlans)
                {
                    if (currentTestPlan.Name.Equals(testPlanName))
                    {
                        testPlan = currentTestPlan;
                        break;
                    }
                }
            }

            return testPlan;
        }

        /// <summary>
        /// Gets all test plans in specified TFS team project.
        /// </summary>
        /// <param name="testManagementTeamProject">The _testproject.</param>
        /// <returns>collection of all test plans</returns>
        public static ITestPlanCollection GetAllTestPlans(ITestManagementTeamProject testManagementTeamProject)
        {
            ITestPlanCollection testPlanCollection = null;
            int retryCount = 0;
            try
            {
                testPlanCollection = testManagementTeamProject.TestPlans.Query("SELECT * FROM TestPlan");
            }
            catch (Exception ex)
            {
                log.Error("Getting all plans error.", ex);
                retryCount++;
                throw ex;
            }    

            return testPlanCollection;
        }
    }
}
