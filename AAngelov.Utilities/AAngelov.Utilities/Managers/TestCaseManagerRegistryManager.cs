using System;

namespace AAngelov.Utilities.Managers
{
    /// <summary>
    /// Contains Test Case Manager APP Registry related methods
    /// </summary>
    public class TestCaseManagerRegistryManager : BaseRegistryManager
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static TestCaseManagerRegistryManager instance;

        /// <summary>
        /// The should show comment window registry sub key name
        /// </summary>
        private readonly string shouldShowCommentWindowRegistrySubKeyName = "shouldShowCommentWindow";

        /// <summary>
        /// The suite filter registry sub key name
        /// </summary>
        private readonly string suiteFilterRegistrySubKeyName = "suiteFilter";

        /// <summary>
        /// The suite filter registry sub key name
        /// </summary>
        private readonly string selectedSuiteIdFilterRegistrySubKeyName = "selectedSuiteId";

        /// <summary>
        /// The show subsuite test cases registry sub key name
        /// </summary>
        private readonly string showSubsuiteTestCasesRegistrySubKeyName = "showSubsuiteTestCases";

        /// <summary>
        /// The project DLL path registry sub key name
        /// </summary>
        private readonly string projectDllPathRegistrySubKeyName = "projectPathDll";

        /// <summary>
        /// The team project URI registry sub key name
        /// </summary>
        private readonly string teamProjectUriRegistrySubKeyName = "teamProjectUri";

        /// <summary>
        /// The team project name registry sub key name
        /// </summary>
        private readonly string teamProjectNameRegistrySubKeyName = "teamProjectName";

        /// <summary>
        /// The test plan registry sub key name
        /// </summary>
        private readonly string testPlanRegistrySubKeyName = "testPlan";

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseManagerRegistryManager"/> class.
        /// </summary>
        public TestCaseManagerRegistryManager()
        {
            this.MainRegistrySubKey = "TestCaseManager/settings";
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static TestCaseManagerRegistryManager Instance 
        {
            get
            {
                if(instance == null)
                {
                    instance = new TestCaseManagerRegistryManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Writes the show subsuite test cases.
        /// </summary>
        /// <param name="showSubsuiteTestCases">if set to <c>true</c> [show subsuite test cases].</param>
        public void WriteShowSubsuiteTestCases(bool showSubsuiteTestCases)
        {
            Write(this.GenerateMergedKey(showSubsuiteTestCasesRegistrySubKeyName), showSubsuiteTestCases); 
        }

        /// <summary>
        /// Writes the should comment window show.
        /// </summary>
        /// <param name="shouldCommentWindowShow">if set to <c>true</c> [should comment window show].</param>
        public void WriteShouldCommentWindowShow(bool shouldCommentWindowShow)
        {
            Write(this.GenerateMergedKey(shouldShowCommentWindowRegistrySubKeyName), shouldCommentWindowShow);
        }        

        /// <summary>
        /// Writes the suite filter.
        /// </summary>
        /// <param name="suiteFilter">The suite filter.</param>
        public void WriteSuiteFilter(string suiteFilter)
        {
            Write(this.GenerateMergedKey(suiteFilterRegistrySubKeyName), suiteFilter);
        }

        /// <summary>
        /// Writes the selected suite unique identifier.
        /// </summary>
        /// <param name="suiteId">The suite unique identifier.</param>
        public void WriteSelectedSuiteId(int suiteId)
        {
            Write(this.GenerateMergedKey(selectedSuiteIdFilterRegistrySubKeyName), suiteId);
        }

        /// <summary>
        /// Writes the current team project URI to registry.
        /// </summary>
        /// <param name="teamProjectUri">The team project URI.</param>
        public void WriteCurrentTeamProjectUri(string teamProjectUri)
        {
            Write(this.GenerateMergedKey(teamProjectUriRegistrySubKeyName), teamProjectUri);
        }

        /// <summary>
        /// Writes the name of the current team project to registry.
        /// </summary>
        /// <param name="teamProjectName">Name of the team project.</param>
        public void WriteCurrentTeamProjectName(string teamProjectName)
        {
            Write(this.GenerateMergedKey(teamProjectNameRegistrySubKeyName), teamProjectName);
        }

        /// <summary>
        /// Writes the current test plan to registry.
        /// </summary>
        /// <param name="testPlan">The test plan.</param>
        public void WriteCurrentTestPlan(string testPlan)
        {
            Write(this.GenerateMergedKey(testPlanRegistrySubKeyName), testPlan);
        }

        /// <summary>
        /// Writes the current project DLL path to registry.
        /// </summary>
        /// <param name="projectDllPath">The project DLL path.</param>
        public void WriteCurrentProjectDllPath(string projectDllPath)
        {
            Write(this.GenerateMergedKey(projectDllPathRegistrySubKeyName), projectDllPath);
        }

        /// <summary>
        /// Reads the show subsuite test cases.
        /// </summary>
        /// <returns>should show subsuite test cases</returns>
        public bool ReadShowSubsuiteTestCases()
        {
            return ReadBool(GenerateMergedKey(showSubsuiteTestCasesRegistrySubKeyName));
        }

        /// <summary>
        /// Reads the should comment window show.
        /// </summary>
        /// <returns>should Comment Window Show</returns>
        public bool ReadShouldCommentWindowShow()
        {
            return ReadBool(GenerateMergedKey(shouldShowCommentWindowRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the team project URI from registry.
        /// </summary>
        /// <returns>team project URI</returns>
        public string GetTeamProjectUri()
        {
            return ReadStr(GenerateMergedKey(teamProjectUriRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the name of the team project from registry.
        /// </summary>
        /// <returns>name of the team project</returns>
        public string GetTeamProjectName()
        {
            return ReadStr(GenerateMergedKey(teamProjectNameRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the project DLL path from registry.
        /// </summary>
        /// <returns>the project DLL path</returns>
        public string GetProjectDllPath()
        {
            return ReadStr(GenerateMergedKey(projectDllPathRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the test plan from registry.
        /// </summary>
        /// <returns>the test plan</returns>
        public string GetTestPlan()
        {
            return ReadStr(GenerateMergedKey(testPlanRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the suite filter.
        /// </summary>
        /// <returns>the suite filter</returns>
        public string GetSuiteFilter()
        {
            return ReadStr(GenerateMergedKey(suiteFilterRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the selected suite unique identifier from registry;
        /// </summary>
        /// <returns>selected suite id</returns>
        public int GetSelectedSuiteId()
        {
            int result = -1;
            try
            {
                result = ReadInt(GenerateMergedKey(selectedSuiteIdFilterRegistrySubKeyName));
            }
            catch (NullReferenceException)
            {
                result = -1;
            }

            return result;
        }
    }
}
