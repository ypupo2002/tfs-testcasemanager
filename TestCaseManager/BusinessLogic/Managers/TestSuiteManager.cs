using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace TestCaseManagerApp
{
    public static class TestSuiteManager
    {
        public static List<ITestSuiteBase> GetSuites(this ITestSuiteCollection suites)
        {
            List<ITestSuiteBase> testSuites = new List<ITestSuiteBase>();
            foreach (ITestSuiteBase suite in suites)
            {
                if (suite != null)
                {
                    suite.Refresh();
                    if (!testSuites.Contains(suite))
                        testSuites.Add(suite);
                    if (suite.TestSuiteType == TestSuiteType.StaticTestSuite)
                    {
                        IStaticTestSuite suite1 = suite as IStaticTestSuite;
                        //suite1.Refresh();
                        if (suite1 != null && (suite1.SubSuites.Count > 0))
                            testSuites.AddRange(suite1.SubSuites.GetSuites());
                    }
                }
            }
            return testSuites;
        }

        public static ITestSuiteBase GetSuiteByName(string title)
        {
            var firstMatchingSuite = ExecutionContext.TeamProject.TestSuites.Query("SELECT * FROM TestSuite where Title = '" + title + "'").First();

            return firstMatchingSuite;
        }

        public static void AddTestCase(this ITestSuiteBase currentSuite, ITestCase testCaseToAdd)
        {
            if (((IStaticTestSuite)currentSuite).Entries.Where(x => x.Id.Equals(testCaseToAdd.Id)).ToList().Count == 0)
                ((IStaticTestSuite)currentSuite).Entries.Add(testCaseToAdd);
        }

        public static void RemoveTestCase(ITestCase testCaseToRemove)
        {
            RemoveTestCaseInternal(testCaseToRemove, ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites);
        }

        public static void RemoveTestCaseInternal(ITestCase testCaseToRemove, ITestSuiteCollection suites)
        {
            foreach (ITestSuiteBase currentSuite in suites)
            {
                if (currentSuite != null)
                {
                    foreach (var currentTestCase in currentSuite.TestCases)
                    {
                        if (currentTestCase.Id.Equals(testCaseToRemove.Id))
                            ((IStaticTestSuite)currentSuite).Entries.Remove(testCaseToRemove);
                    }
                    if (currentSuite.TestSuiteType == TestSuiteType.StaticTestSuite)
                    {
                        IStaticTestSuite suite1 = currentSuite as IStaticTestSuite;
                        if (suite1 != null && (suite1.SubSuites.Count > 0))
                            RemoveTestCaseInternal(testCaseToRemove, suite1.SubSuites);
                    }
                }
            }
         
        }
    }
}
