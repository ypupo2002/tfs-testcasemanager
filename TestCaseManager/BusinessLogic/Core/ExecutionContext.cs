using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp
{
    public class ExecutionContext
    {
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

        public static TfsTeamProjectCollection Tfs
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

        public static ITestManagementTeamProject TeamProject
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

        public static string ProjectDllPath { get; set; }

        public static string SharedStepTitle { get; set; }
        public static bool SharedStepTitleDialogCancelled { get; set; }
        public static SettingsViewModel SettingsViewModel { get; set; }

        private static Preferences preferences;
        private static TfsTeamProjectCollection tfsTeamProjectCollection;
        private static ITestManagementTeamProject teamProject; 
    }
}
