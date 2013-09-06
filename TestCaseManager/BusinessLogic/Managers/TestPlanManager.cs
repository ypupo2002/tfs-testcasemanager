using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    public static class TestPlanManager
    {
        public static ITestPlanCollection GetAllTestPlans(ITestManagementTeamProject _testproject)
        {
            return _testproject.TestPlans.Query("Select * from TestPlan");

        }
    }
}
