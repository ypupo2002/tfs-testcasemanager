using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp
{
    /// <summary>
    /// Contains App Execution Context Properties
    /// </summary>
    public class ExecutionContext
    {
        /// <summary>
        /// Gets or sets the preferences.
        /// </summary>
        /// <value>
        /// The preferences.
        /// </value>
        public static Preferences Preferences
        {
            get
            {
                return preferences;
            }
            set
            {
                preferences = value;
            }
        }

        /// <summary>
        /// Gets or sets the TFS team project collection.
        /// </summary>
        /// <value>
        /// The TFS team project collection.
        /// </value>
        public static TfsTeamProjectCollection TfsTeamProjectCollection
        {
            get
            {
                return tfsTeamProjectCollection;
            }
            set
            {
                tfsTeamProjectCollection = value; 
            }
        }

        /// <summary>
        /// Gets or sets the test management team project.
        /// </summary>
        /// <value>
        /// The test management team project.
        /// </value>
        public static ITestManagementTeamProject TestManagementTeamProject
        {
            get
            {
                return teamProject;
            }
            set
            {
                teamProject = value;
            }
        }

        /// <summary>
        /// Gets or sets the project DLL path.
        /// </summary>
        /// <value>
        /// The project DLL path.
        /// </value>
        public static string ProjectDllPath { get; set; }

        /// <summary>
        /// Gets or sets the shared step title.
        /// </summary>
        /// <value>
        /// The shared step title.
        /// </value>
        public static string SharedStepTitle { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [shared step title dialog cancelled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [shared step title dialog cancelled]; otherwise, <c>false</c>.
        /// </value>
        public static bool SharedStepTitleDialogCancelled { get; set; }
        /// <summary>
        /// Gets or sets the settings view model.
        /// </summary>
        /// <value>
        /// The settings view model.
        /// </value>
        public static SettingsViewModel SettingsViewModel { get; set; }

        private static Preferences preferences;
        private static TfsTeamProjectCollection tfsTeamProjectCollection;
        private static ITestManagementTeamProject teamProject; 
    }
}
