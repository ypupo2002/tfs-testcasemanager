// <copyright file="TestSuiteManager.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Contains helper methods for working with TestSuite entities
    /// </summary>
    public static class TestSuiteManager
    {   
        /// <summary>
        /// Gets all test suites from the current test plan.
        /// </summary>
        /// <returns>list of all test suites</returns>
        public static List<ITestSuiteBase> GetAllTestSuitesInTestPlan()
        {
            List<ITestSuiteBase> testSuites = GetAllTestSuites(ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites);
            return testSuites;
        }

        /// <summary>
        /// Gets the test suite core object by name.
        /// </summary>
        /// <param name="suiteName">The suite name.</param>
        /// <returns>test suite core object</returns>
        public static ITestSuiteBase GetTestSuiteByName(string suiteName)
        {
            var firstMatchingSuite = ExecutionContext.TestManagementTeamProject.TestSuites.Query(string.Concat("SELECT * FROM TestSuite where Title = '", suiteName, "'")).First();

            return firstMatchingSuite;
        }

        /// <summary>
        /// Gets the test suite core object by id.
        /// </summary>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <returns>test suite core object</returns>
        public static ITestSuiteBase GetTestSuiteById(int suiteId)
        {
            ITestSuiteBase testSuiteBase = null;
            if (suiteId != 0)
            {
                testSuiteBase = ExecutionContext.TestManagementTeamProject.TestSuites.Find(suiteId);
            }
            else
            {
                testSuiteBase = TestSuiteManager.GetAllTestSuitesInTestPlan().FirstOrDefault();
            }

            return testSuiteBase;
        }

        /// <summary>
        /// Adds a test case to the suite.
        /// </summary>
        /// <param name="currentSuite">The current suite.</param>
        /// <param name="testCaseToAdd">The test case to be added.</param>
        public static void AddTestCase(this ITestSuiteBase currentSuite, ITestCase testCaseToAdd)
        {
            if (currentSuite is IRequirementTestSuite)
            {
                IRequirementTestSuite suite = currentSuite as IRequirementTestSuite;
                if (suite.TestCases.Where(x => x.Id.Equals(testCaseToAdd.Id)).ToList().Count == 0)
                {
                    suite.TestCases.AddCases(new List<ITestCase>() { testCaseToAdd });
                }
            }
            else if (currentSuite is IStaticTestSuite)
            {
                IStaticTestSuite suite = currentSuite as IStaticTestSuite;
                if (suite.Entries.Where(x => x.Id.Equals(testCaseToAdd.Id)).ToList().Count == 0)
                {
                    suite.Entries.Add(testCaseToAdd);
                }
            }  
        }

        /// <summary>
        /// Removes the specified test case from the test suite.
        /// </summary>
        /// <param name="testCaseToRemove">The test case to be removed.</param>
        public static void RemoveTestCase(ITestCase testCaseToRemove)
        {
            RemoveTestCaseInternal(testCaseToRemove, ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites);
        }

        /// <summary>
        /// Removes the test case internal.
        /// </summary>
        /// <param name="testCaseToRemove">The test case to be removed.</param>
        /// <param name="suitesToSearch">The suites which will be searched.</param>
        private static void RemoveTestCaseInternal(ITestCase testCaseToRemove, ITestSuiteCollection suitesToSearch)
        {
            foreach (ITestSuiteBase currentSuite in suitesToSearch)
            {
                if (currentSuite != null)
                {
                    foreach (var currentTestCase in currentSuite.TestCases)
                    {
                        if (currentTestCase.Id.Equals(testCaseToRemove.Id))
                        {
                            ((IStaticTestSuite)currentSuite).Entries.Remove(testCaseToRemove);
                        }
                    }

                    if (currentSuite.TestSuiteType == TestSuiteType.StaticTestSuite)
                    {
                        IStaticTestSuite suite1 = currentSuite as IStaticTestSuite;
                        if (suite1 != null && (suite1.SubSuites.Count > 0))
                        {
                            RemoveTestCaseInternal(testCaseToRemove, suite1.SubSuites);
                        }
                    }
                }
            }         
        }

        /// <summary>
        /// Returns all suites in current suite collection
        /// </summary>
        /// <param name="suites">The suites.</param>
        /// <returns>list of all suites</returns>
        private static List<ITestSuiteBase> GetAllTestSuites(ITestSuiteCollection suites)
        {
            List<ITestSuiteBase> testSuites = new List<ITestSuiteBase>();
            foreach (ITestSuiteBase currentSuite in suites)
            {
                if (currentSuite != null)
                {
                    currentSuite.Refresh();
                    if (!testSuites.Contains(currentSuite))
                    {
                        testSuites.Add(currentSuite);
                    }

                    if (currentSuite is IStaticTestSuite)
                    {
                        IStaticTestSuite suite1 = currentSuite as IStaticTestSuite;
                        if (suite1 != null && (suite1.SubSuites.Count > 0))
                        {
                            testSuites.AddRange(GetAllTestSuites(suite1.SubSuites));
                        }
                    }
                }
            }

            return testSuites;
        }
    }
}
