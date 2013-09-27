// <copyright file="ProjectSelectionViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Forms;
    using FirstFloor.ModernUI.Windows.Controls;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Provides methods and properties related to the Project Selection View
    /// </summary>
    public class ProjectSelectionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSelectionViewModel"/> class.
        /// </summary>
        public ProjectSelectionViewModel()
        {
            this.ObservableTestPlans = new ObservableCollection<string>();
        }

        /// <summary>
        /// Gets or sets the test service.
        /// </summary>
        /// <value>
        /// The test service.
        /// </value>
        public ITestManagementService TestService { get; set; }

        /// <summary>
        /// Gets or sets the full name of the team project.
        /// </summary>
        /// <value>
        /// The full name of the team project.
        /// </value>
        public string FullTeamProjectName { get; set; }

        /// <summary>
        /// Gets or sets the observable test plans.
        /// </summary>
        /// <value>
        /// The observable test plans.
        /// </value>
        public ObservableCollection<string> ObservableTestPlans { get; set; }

        /// <summary>
        /// Load project settings from TFS team project picker.
        /// </summary>
        /// <param name="projectPicker">The project picker.</param>
        public void LoadProjectSettingsFromUserDecision(TeamProjectPicker projectPicker)
        {
            ExecutionContext.Preferences.TfsUri = null;
            ExecutionContext.Preferences.TestProjectName = null;

            try
            {
                using (projectPicker)
                {
                    var userSelected = projectPicker.ShowDialog();

                    if (userSelected == DialogResult.Cancel)
                    {
                        return;
                    }

                    if (projectPicker.SelectedTeamProjectCollection != null)
                    {
                        ExecutionContext.Preferences.TfsUri = projectPicker.SelectedTeamProjectCollection.Uri;
                        ExecutionContext.Preferences.TestProjectName = projectPicker.SelectedProjects[0].Name;
                        ExecutionContext.TfsTeamProjectCollection = projectPicker.SelectedTeamProjectCollection;
                        this.TestService = (ITestManagementService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ITestManagementService));
                        this.InitializeTestProjectByName(this.TestService, ExecutionContext.Preferences.TestProjectName);

                        // InitializeTestPlans(ExecutionContext.TeamProject);
                    }
                    this.FullTeamProjectName = this.GenerateFullTeamProjectName();
                   
                    RegistryManager.WriteCurrentTeamProjectName(ExecutionContext.Preferences.TestProjectName);
                    RegistryManager.WriteCurrentTeamProjectUri(ExecutionContext.Preferences.TfsUri.ToString());
                }
            }
            catch (Exception)
            {
                ModernDialog.ShowMessage("Error selecting team project.", "Warning", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Loads project settings from registry.
        /// </summary>
        public void LoadProjectSettingsFromRegistry()
        {
            string teamProjectUri = RegistryManager.GetTeamProjectUri();
            string teamProjectName = RegistryManager.GetTeamProjectName();
            string projectDllPath = RegistryManager.GetProjectDllPath();
            if (!string.IsNullOrEmpty(teamProjectUri) && !string.IsNullOrEmpty(teamProjectName))
            {
                ExecutionContext.Preferences.TfsUri = new Uri(teamProjectUri);
                ExecutionContext.Preferences.TestProjectName = teamProjectName;
                ExecutionContext.TfsTeamProjectCollection = new TfsTeamProjectCollection(ExecutionContext.Preferences.TfsUri);
                this.TestService = (ITestManagementService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ITestManagementService));
                this.InitializeTestProjectByName(this.TestService, ExecutionContext.Preferences.TestProjectName);
                try
                {
                    this.FullTeamProjectName = this.GenerateFullTeamProjectName();                   
                }
                catch (SocketException)
                {
                    // TODO: Add exception logging
                    return;
                }
                catch (WebException)
                {
                    // TODO: Add exception logging
                    return;
                }
            }
        }

        /// <summary>
        /// Initializes the test plans.
        /// </summary>
        /// <param name="testManagementTeamProject">The _testproject.</param>
        public void InitializeTestPlans(ITestManagementTeamProject testManagementTeamProject)
        {
            this.ObservableTestPlans.Clear();
            ITestPlanCollection testPlans = TestPlanManager.GetAllTestPlans(testManagementTeamProject);
            foreach (ITestPlan tp in testPlans)
            {
                this.ObservableTestPlans.Add(tp.Name);
            }
        }       

        /// <summary>
        /// Generates the full name of the team project.
        /// </summary>
        /// <returns>The full name of the team project</returns>
        public string GenerateFullTeamProjectName()
        {
            string fullTeamProjectName = string.Concat(ExecutionContext.Preferences.TfsUri, "/", ExecutionContext.Preferences.TestProjectName);
            return fullTeamProjectName;
        }

        /// <summary>
        /// Initializes the test project by name.
        /// </summary>
        /// <param name="testManagementService">The test management service.</param>
        /// <param name="testProjectName">Name of the test project.</param>
        public void InitializeTestProjectByName(ITestManagementService testManagementService, string testProjectName)
        {
            ExecutionContext.TestManagementTeamProject = testManagementService.GetTeamProject(testProjectName);
        }
    }
}
