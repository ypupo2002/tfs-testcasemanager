// <copyright file="TestSuiteManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Enums;

    /// <summary>
    /// Contains helper methods for working with TestSuite entities
    /// </summary>
    public static class TestSuiteManager
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets all suites.
        /// </summary>
        /// <param name="subSuitesCore">The sub suites core.</param>
        /// <returns>list of all suites</returns>
        public static ObservableCollection<Suite> GetAllSuites(ITestSuiteCollection subSuitesCore)
        {
            ObservableCollection<Suite> subSuites = new ObservableCollection<Suite>();
            foreach (ITestSuiteBase currentSuite in subSuitesCore)
            {
                if (currentSuite != null)
                {
                    currentSuite.Refresh();
                    ObservableCollection<Suite> childred = null;
                    if (currentSuite is IStaticTestSuite)
                    {
                        IStaticTestSuite suite = currentSuite as IStaticTestSuite;
                        if (suite.SubSuites != null && suite.SubSuites.Count > 0)
                        {
                            childred = GetAllSuites(suite.SubSuites);
                        }
                    }                    
                    Suite newSuite = new Suite(currentSuite.Title, currentSuite.Id, childred);

                    // Cannot add suites to requirements based suite
                    if (currentSuite is IRequirementTestSuite)
                    {
                        newSuite.IsPasteEnabled = false;
                        newSuite.IsSuiteAddEnabled = false;
                        newSuite.IsAddSuiteAllowed = false;
                        newSuite.IsPasteAllowed = false;
                    }
                    SetParentToAllChildrenSuites(childred, newSuite);
                 
                    subSuites.Add(newSuite);
                }             
            } 

            return subSuites;
        }    

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
        /// Renames the suite.
        /// </summary>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="newName">The new name.</param>
        public static void RenameSuite(int suiteId, string newName)
        {
            ITestSuiteBase currentSuite = ExecutionContext.TestManagementTeamProject.TestSuites.Find(suiteId);
            log.InfoFormat("Change Suite title from {0} to {1}, Suite Id = {2}", currentSuite.Title, newName, currentSuite.Id);
            currentSuite.Title = newName;
            ExecutionContext.Preferences.TestPlan.Save();          
        }

        /// <summary>
        /// Adds the child suite.
        /// </summary>
        /// <param name="parentSuiteId">The parent suite unique identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="canBeAdded">if set to <c>true</c> [can be added].</param>
        /// <returns>
        /// new suite unique identifier.
        /// </returns>
        public static int AddChildSuite(int parentSuiteId, string title, out bool canBeAdded)
        {
            ITestSuiteBase parentSuite = null;
            if (parentSuiteId != -1)
            {
                parentSuite = ExecutionContext.TestManagementTeamProject.TestSuites.Find(parentSuiteId);
            }

            if (parentSuite is IRequirementTestSuite)
            {
                canBeAdded = false;
                return 0;
            }
            IStaticTestSuite staticSuite = ExecutionContext.TestManagementTeamProject.TestSuites.CreateStatic();
            canBeAdded = true;
            staticSuite.Title = title;

            if (parentSuite != null && parentSuite is IStaticTestSuite && parentSuiteId != -1)
            {
                IStaticTestSuite parentSuiteStatic = parentSuite as IStaticTestSuite;
                parentSuiteStatic.Entries.Add(staticSuite);
                log.InfoFormat("Add child suite to suite with Title= {0}, Id = {1}, child suite title= {2}", parentSuite.Title, parentSuite.Id, title);
            }
            else
            {
                ExecutionContext.Preferences.TestPlan.RootSuite.Entries.Add(staticSuite);
                log.InfoFormat("Add child suite with title= {0} to test plan", title);
            }

            return staticSuite.Id;
        }

        /// <summary>
        /// Pastes the suite to parent.
        /// </summary>
        /// <param name="parentSuiteId">The parent suite unique identifier.</param>
        /// <param name="suiteToAddId">The suite automatic add unique identifier.</param>
        /// <param name="clipBoardCommand">The clip board command.</param>
        /// <exception cref="System.ArgumentException">The requirments based suites cannot have child suites!</exception>
        public static void PasteSuiteToParent(int parentSuiteId, int suiteToAddId, ClipBoardCommand clipBoardCommand)
        {
            ITestSuiteBase parentSuite = null;
            ITestSuiteBase suiteToAdd = null;
            try
            {
                parentSuite = ExecutionContext.TestManagementTeamProject.TestSuites.Find(parentSuiteId);
            }
            catch(TestManagementValidationException ex)
            {
                log.Error(ex);
            }
            try
            {
                suiteToAdd = ExecutionContext.TestManagementTeamProject.TestSuites.Find(suiteToAddId);
            }
            catch (TestManagementValidationException ex)
            {
                log.Error(ex);
            }
          
            IStaticTestSuite oldParent = suiteToAdd.Parent;
            if (parentSuite != null && parentSuite is IRequirementTestSuite)
            {
                throw new ArgumentException("The requirments based suites cannot have child suites!");
            }

            if (parentSuite != null && parentSuite is IStaticTestSuite && parentSuiteId != -1)
            {
                IStaticTestSuite parentSuiteStatic = parentSuite as IStaticTestSuite;
                parentSuiteStatic.Entries.Add(suiteToAdd);
                log.InfoFormat("Add child suite to suite with Title= {0}, Id = {1}, child suite title= {2}, id= {3}", parentSuite.Title, parentSuite.Id, suiteToAdd.Title, suiteToAdd.Id);
            }
            else
            {
                ExecutionContext.Preferences.TestPlan.RootSuite.Entries.Add(suiteToAdd);
            }
            if (clipBoardCommand.Equals(ClipBoardCommand.Cut))
            {
                DeleteSuite(suiteToAddId, oldParent);
            }

            ExecutionContext.Preferences.TestPlan.Save();
        }

        /// <summary>
        /// Pastes the test cases to suite.
        /// </summary>
        /// <param name="suiteToAddInId">The suite automatic add information unique identifier.</param>
        /// <param name="clipBoardCommand">The clip board test case object.</param>
        /// <exception cref="System.ArgumentException">New test cases cannot be added to requirement based suites!</exception>
        public static void PasteTestCasesToSuite(int suiteToAddInId, ClipBoardTestCase clipBoardTestCase)
        {
            ITestSuiteBase suiteToAddIn = ExecutionContext.TestManagementTeamProject.TestSuites.Find(suiteToAddInId);
            if (suiteToAddIn is IRequirementTestSuite)
            {
                throw new ArgumentException("New test cases cannot be added to requirement based suites!");
            }
            IStaticTestSuite suiteToAddInStatic = suiteToAddIn as IStaticTestSuite;
            ITestSuiteBase oldSuite;
            List<TestCase> allTestCasesInPlan = null;
            if (clipBoardTestCase.TestCases[0].TestSuiteId != null)
            {
                oldSuite = ExecutionContext.TestManagementTeamProject.TestSuites.Find((int)clipBoardTestCase.TestCases[0].TestSuiteId);
            }
            else
            {
                oldSuite = null;
            }            

            foreach (TestCase currentTestCase in clipBoardTestCase.TestCases)
            {
                ITestCase testCaseCore = null;
                if (oldSuite is IRequirementTestSuite)
                {
                    IRequirementTestSuite suite = oldSuite as IRequirementTestSuite;
                    testCaseCore = suite.TestCases.Where(x => x.TestCase != null && x.TestCase.Id.Equals(currentTestCase.TestCaseId)).FirstOrDefault().TestCase;
                }
                else if (oldSuite is IStaticTestSuite)
                {
                    IStaticTestSuite suite = oldSuite as IStaticTestSuite;
                    testCaseCore = suite.Entries.Where(x => x.TestCase != null && x.TestCase.Id.Equals(currentTestCase.TestCaseId)).FirstOrDefault().TestCase;
                }
                else if (oldSuite == null)
                {
                    if (allTestCasesInPlan == null)
                    {
                        allTestCasesInPlan = TestCaseManager.GetAllTestCasesInTestPlan();                        
                    }
                    testCaseCore = allTestCasesInPlan.Where(t => t.TestCaseId.Equals(currentTestCase.TestCaseId)).FirstOrDefault().ITestCase;
                }
                if (!suiteToAddInStatic.Entries.Contains(testCaseCore))
                {
                    suiteToAddInStatic.Entries.Add(testCaseCore);
                }
                if (clipBoardTestCase.ClipBoardCommand.Equals(ClipBoardCommand.Cut))
                {
                    if (oldSuite is IStaticTestSuite)
                    {
                        IStaticTestSuite suite = oldSuite as IStaticTestSuite;
                        suite.Entries.Remove(testCaseCore);
                    }                   
                }              
            }           

            ExecutionContext.Preferences.TestPlan.Save();
        }

        /// <summary>
        /// Deletes the suite.
        /// </summary>
        /// <param name="suiteToBeRemovedId">The suite to be removed unique identifier.</param>
        /// <param name="parent">The parent.</param>
        /// <exception cref="System.ArgumentException">The root suite cannot be deleted!</exception>
        public static void DeleteSuite(int suiteToBeRemovedId, IStaticTestSuite parent = null)
        {
            // If it's root suite throw exception
            if (suiteToBeRemovedId == -1)
            {
                throw new ArgumentException("The root suite cannot be deleted!");
            }
            ITestSuiteBase currentSuite = ExecutionContext.TestManagementTeamProject.TestSuites.Find(suiteToBeRemovedId);

            // Don't remove test cases becuase the suite can be child of another active suite
            // Remove all test cases in the suite
            // RemoveAllTestCasesInSuite(currentSuite);

            // Performs recursive delete for each child suite which is static. Requirements suites don't have childs.
            ////if (currentSuite is IStaticTestSuite)
            ////{
            ////    IStaticTestSuite staticSuite = currentSuite as IStaticTestSuite;
            ////    foreach (var currentChild in staticSuite.SubSuites)
            ////    {
            ////        DeleteSuite(currentChild.Id);
            ////    }
            ////}   
       
            // Remove the parent child relation. This is the only way to delete the suite.
            if (parent != null)
            {
                log.InfoFormat("Remove suite Title= \"{0}\", id= \"{1}\" from suite Title= \"{2}\", id= \"{3}\"", currentSuite.Title, currentSuite.Id, parent.Title, parent.Id);
                parent.Entries.Remove(currentSuite);
            }
            else if (currentSuite.Parent != null)
            {
                log.InfoFormat("Remove suite Title= \"{0}\", id= \"{1}\" from suite Title= \"{2}\", id= \"{3}\"", currentSuite.Title, currentSuite.Id, currentSuite.Parent.Title, currentSuite.Parent.Id);
                currentSuite.Parent.Entries.Remove(currentSuite);
            }
            else
            {
                // If it's initial suite, remove it from the test plan.
                ExecutionContext.Preferences.TestPlan.RootSuite.Entries.Remove(currentSuite);
                log.Info("Remove suite Title= \"{0}\", id= \"{1}\" from test plan.");
            }

            // Apply changes to the suites
            ExecutionContext.Preferences.TestPlan.Save();
        }

        /// <summary>
        /// Removes all test cases information suite.
        /// </summary>
        /// <param name="currentSuite">The current suite.</param>
        public static void RemoveAllTestCasesInSuite(ITestSuiteBase currentSuite)
        {
            foreach (ITestCase currentTestCase in currentSuite.TestCases)
            {
                RemoveTestCaseInternal(currentTestCase, currentSuite);
            }
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
            if (suiteId > 0)
            {
                // If it's old test case
                testSuiteBase = ExecutionContext.TestManagementTeamProject.TestSuites.Find(suiteId);
            }
            else
            {
                // If the test case is new it will be added to root suite of the test plan
                testSuiteBase = ExecutionContext.Preferences.TestPlan.RootSuite;
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
        /// Removes the specified test case from the test suite.
        /// </summary>
        /// <param name="testCaseToRemove">The test case to be removed.</param>
        public static void RemoveTestCase(TestCase testCaseToRemove)
        {
            RemoveTestCaseInternal(testCaseToRemove.ITestCase, testCaseToRemove.ITestSuiteBase);
        }

        /// <summary>
        /// Sets the parent to all children suites.
        /// </summary>
        /// <param name="childred">The childred.</param>
        /// <param name="newSuite">The new suite.</param>
        public static void SetParentToAllChildrenSuites(ObservableCollection<Suite> childred, Suite newSuite)
        {
            if (childred != null)
            {
                foreach (Suite currentChild in childred)
                {
                    currentChild.Parent = newSuite;
                }
            }
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
                    if (currentSuite is IRequirementTestSuite)
                    {
                        IRequirementTestSuite suite = currentSuite as IRequirementTestSuite;
                        if (suite.TestCases.Where(x => x.Id.Equals(testCaseToRemove.Id)).ToList().Count == 0)
                        {
                            suite.TestCases.RemoveEntries(new List<ITestSuiteEntry>() { testCaseToRemove.TestSuiteEntry });
                        }
                    }
                    else if (currentSuite is IStaticTestSuite)
                    {
                        foreach (var currentTestCase in currentSuite.TestCases)
                        {
                            if (currentTestCase.Id.Equals(testCaseToRemove.Id))
                            {
                                ((IStaticTestSuite)currentSuite).Entries.Remove(testCaseToRemove);
                            }
                        }
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
        /// Removes the test case internal.
        /// </summary>
        /// <param name="testCaseToRemove">The test case automatic remove.</param>
        /// <param name="currentSuite">The current suite.</param>
        private static void RemoveTestCaseInternal(ITestCase testCaseToRemove, ITestSuiteBase currentSuite)
        {
            if (currentSuite != null)
            {
                log.InfoFormat("Remove Test Case with id= {0}, Title = {1} from Suite with id= {2}, Title= {3}", testCaseToRemove.Id, testCaseToRemove.Title, currentSuite.Id, currentSuite.Title);
                if (currentSuite is IRequirementTestSuite)
                {
                    IRequirementTestSuite suite = currentSuite as IRequirementTestSuite;
                    suite.AllTestCases.Remove(testCaseToRemove);
                    suite.TestCases.RemoveCases(new List<ITestCase>() { testCaseToRemove });
                }
                else if (currentSuite is IStaticTestSuite)
                {
                    IStaticTestSuite suite = currentSuite as IStaticTestSuite;
                    suite.Entries.Remove(testCaseToRemove);
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
