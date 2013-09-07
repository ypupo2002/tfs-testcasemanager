using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    /// <summary>
    /// Contains helper methods for working with ITestPlan objects
    /// </summary>
    public static class TestPlanManager
    {
        /// <summary>
        /// Gets TestPlan by name.
        /// </summary>
        /// <param name="_testproject">TFS team project</param>
        /// <param name="testPlanName">Name of the test plan.</param>
        /// <returns>the found test plan</returns>
        public static ITestPlan GetTestPlanByName(ITestManagementTeamProject _testproject, string testPlanName)
        {
            ITestPlanCollection testPlans =  GetAllTestPlans(ExecutionContext.TestManagementTeamProject);
            ITestPlan testPlan = default(ITestPlan);
            foreach (ITestPlan currentTestPlan in testPlans)
            {
                if (currentTestPlan.Name.Equals(testPlanName))
                {
                    testPlan = currentTestPlan;
                    break;
                }
            }

            return testPlan;
        }

        /// <summary>
        /// Gets all test plans in specified TFS team project.
        /// </summary>
        /// <param name="_testproject">The _testproject.</param>
        /// <returns>collection of all test plans</returns>
        public static ITestPlanCollection GetAllTestPlans(ITestManagementTeamProject _testproject)
        {
            return _testproject.TestPlans.Query("SELECT * FROM TestPlan");
        }
    }
}
