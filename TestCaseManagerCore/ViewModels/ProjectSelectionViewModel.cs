// <copyright file="ProjectSelectionViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows;
    using System.Windows.Forms;
    using FirstFloor.ModernUI.Windows.Controls;
    using log4net;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Provides methods and properties related to the Project Selection View
    /// </summary>
    public class ProjectSelectionViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The log
        /// </summary>
       private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The full team project name
        /// </summary>
        private string fullTeamProjectName;

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
        public string FullTeamProjectName
        {
            get
            {
                return this.fullTeamProjectName;
            }

            set
            {
                this.fullTeamProjectName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the observable test plans.
        /// </summary>
        /// <value>
        /// The observable test plans.
        /// </value>
        public ObservableCollection<string> ObservableTestPlans { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is initialized from registry].
        /// </summary>
        /// <value>
        /// <c>true</c> if [is initialized from registry]; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitializedFromRegistry { get; set; }

        /// <summary>
        /// Gets or sets the selected test plan.
        /// </summary>
        /// <value>
        /// The selected test plan.
        /// </value>
        public string SelectedTestPlan { get; set; }

        /// <summary>
        /// Load project settings from TFS team project picker.
        /// </summary>
        /// <param name="projectPicker">The project picker.</param>
        public void LoadProjectSettingsFromUserDecision(TeamProjectPicker projectPicker)
        {
            ExecutionContext.Preferences.TfsUri = null;
            ExecutionContext.Preferences.TestProjectName = null;
            log.Info("Load project info depending on the user choice from project picker!");
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
                        log.InfoFormat("Picker: TFS URI: {0}", ExecutionContext.Preferences.TfsUri);
                        ExecutionContext.Preferences.TestProjectName = projectPicker.SelectedProjects[0].Name;
                        log.InfoFormat("Picker: Test Project Name: {0}", ExecutionContext.Preferences.TestProjectName);
                        ExecutionContext.TfsTeamProjectCollection = projectPicker.SelectedTeamProjectCollection;
                        log.InfoFormat("Picker: TfsTeamProjectCollection: {0}", ExecutionContext.TfsTeamProjectCollection);
                        this.TestService = (ITestManagementService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ITestManagementService));
                        this.InitializeTestProjectByName(this.TestService, ExecutionContext.Preferences.TestProjectName);
                    }
                    this.FullTeamProjectName = this.GenerateFullTeamProjectName();
                   
                    RegistryManager.WriteCurrentTeamProjectName(ExecutionContext.Preferences.TestProjectName);
                    log.InfoFormat("Test Project Name: {0}", ExecutionContext.Preferences.TestProjectName);
                    RegistryManager.WriteCurrentTeamProjectUri(ExecutionContext.Preferences.TfsUri.ToString());
                    log.InfoFormat("TFS URI: {0}", ExecutionContext.Preferences.TfsUri);
                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage("Error selecting team project.", "Warning", MessageBoxButton.OK);
                log.Error("Project info not selected.", ex);
            }
        }

        /// <summary>
        /// Loads project settings from registry.
        /// </summary>
        public void LoadProjectSettingsFromRegistry()
        {
            log.Info("Load project info loaded from registry!");
            string teamProjectUri = RegistryManager.GetTeamProjectUri();
            string teamProjectName = RegistryManager.GetTeamProjectName();
            string projectDllPath = RegistryManager.GetProjectDllPath();
            if (!string.IsNullOrEmpty(teamProjectUri) && !string.IsNullOrEmpty(teamProjectName))
            {
                ExecutionContext.Preferences.TfsUri = new Uri(teamProjectUri);
                log.InfoFormat("Registry> TFS URI: {0}", ExecutionContext.Preferences.TfsUri);
                ExecutionContext.Preferences.TestProjectName = teamProjectName;
                log.InfoFormat("Registry> Test Project Name: {0}", ExecutionContext.Preferences.TestProjectName);
                ExecutionContext.TfsTeamProjectCollection = new TfsTeamProjectCollection(ExecutionContext.Preferences.TfsUri);
                log.InfoFormat("Registry> TfsTeamProjectCollection: {0}", ExecutionContext.TfsTeamProjectCollection);
                this.TestService = (ITestManagementService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ITestManagementService));
                this.InitializeTestProjectByName(this.TestService, ExecutionContext.Preferences.TestProjectName);
                try
                {
                    this.FullTeamProjectName = this.GenerateFullTeamProjectName();
                    log.InfoFormat("SET FullTeamProjectName to {0}", this.FullTeamProjectName);
                }
                catch (SocketException ex)
                {
                    log.Error("SocketException during connecting to TFS.", ex);
                    return;
                }
                catch (WebException ex)
                {
                    log.Error("WebExceptionduring connecting to TFS.", ex);
                    return;
                }
                this.SelectedTestPlan = RegistryManager.GetTestPlan();
                log.InfoFormat("Registry> SelectedTestPlan: {0}", this.SelectedTestPlan);
                if (!string.IsNullOrEmpty(this.SelectedTestPlan))
                {
                    this.IsInitializedFromRegistry = true;
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
