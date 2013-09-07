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

namespace TestCaseManagerApp.ViewModels
{
    public class ProjectSelectionViewModel
    {
        public void LoadProjectSelectionFromUser(TeamProjectPicker projectPicker)
        {
            ExecutionContext.Preferences.TfsUri = null;
            ExecutionContext.Preferences.TestProjectName = null;

            try
            {
                using (projectPicker)
                {
                    var userSelected = projectPicker.ShowDialog();

                    if (userSelected == DialogResult.Cancel)
                        return;

                    if (projectPicker.SelectedTeamProjectCollection != null)
                    {
                        ExecutionContext.Preferences.TfsUri = projectPicker.SelectedTeamProjectCollection.Uri;
                        ExecutionContext.Preferences.TestProjectName = projectPicker.SelectedProjects[0].Name;
                        ExecutionContext.TfsTeamProjectCollection = projectPicker.SelectedTeamProjectCollection;
                        TestService = (ITestManagementService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ITestManagementService));
                        InitializeTestProjectByName(TestService, ExecutionContext.Preferences.TestProjectName);
                        //InitializeTestPlans(ExecutionContext.TeamProject);
                    }
                    FullTeamProjectName = GenerateFullTeamProjectName();
                   
                    RegistryManager.WriteCurrentTeamProjectName(ExecutionContext.Preferences.TestProjectName);
                    RegistryManager.WriteCurrentTeamProjectUri(ExecutionContext.Preferences.TfsUri.ToString());
                }
            }
            catch (Exception)
            {
                ModernDialog.ShowMessage("Error selecting team project.", "Warning", MessageBoxButton.OK);
            }
        }

        public ITestManagementService TestService { get; set; }
        public string FullTeamProjectName { get; set; }

        public void InitializeFromRegistry()
        {
            string teamProjectUri = RegistryManager.GetTeamProjectUri();
            string teamProjectName = RegistryManager.GetTeamProjectName();
            string projectDllPath = RegistryManager.GetProjectDllPath();
            if (!String.IsNullOrEmpty(teamProjectUri) && !String.IsNullOrEmpty(teamProjectName))
            {
                ExecutionContext.Preferences.TfsUri = new Uri(teamProjectUri);
                ExecutionContext.Preferences.TestProjectName = teamProjectName;
                ExecutionContext.TfsTeamProjectCollection = new TfsTeamProjectCollection(ExecutionContext.Preferences.TfsUri);
                ExecutionContext.ProjectDllPath = projectDllPath;
                TestService = (ITestManagementService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ITestManagementService));
                InitializeTestProjectByName(TestService, ExecutionContext.Preferences.TestProjectName);
                try
                {
                    FullTeamProjectName = GenerateFullTeamProjectName();                   
                }
                catch (SocketException)
                {
                    return;
                }
                catch (WebException)
                {
                    return;
                }
            }
        }

        public string GenerateFullTeamProjectName()
        {
            string fullTeamProjectName = String.Concat(ExecutionContext.Preferences.TfsUri, "/", ExecutionContext.Preferences.TestProjectName);
            return fullTeamProjectName;
        }

        public void InitializeTestProjectByName(ITestManagementService test_service, string testProjectName)
        {
            ExecutionContext.TestManagementTeamProject = test_service.GetTeamProject(testProjectName);
        }
    }
}
