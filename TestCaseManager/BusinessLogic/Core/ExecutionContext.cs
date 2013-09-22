// <copyright file="ExecutionContext.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
{
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.ViewModels;

    /// <summary>
    /// Contains App Execution Context Properties
    /// </summary>
    public class ExecutionContext
    {
        /// <summary>
        /// The preferences of the current execution
        /// </summary>
        private static Preferences preferences;

        /// <summary>
        /// The TFS team project collection
        /// </summary>
        private static TfsTeamProjectCollection tfsTeamProjectCollection;

        /// <summary>
        /// The team project
        /// </summary>
        private static ITestManagementTeamProject teamProject; 

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
        /// Gets or sets the settings view model.
        /// </summary>
        /// <value>
        /// The settings view model.
        /// </value>
        public static SettingsViewModel SettingsViewModel { get; set; }
    }
}
