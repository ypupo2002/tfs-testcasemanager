using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.Helpers;

namespace TestCaseManagerApp.Views
{
    public partial class ProjectSelectionView : System.Windows.Controls.UserControl
    {
        public ITestManagementService TestService { get; set; }

        public ProjectSelectionView()
        {
            InitializeComponent();
            
            string teamProjectUri = RegistryManager.GetTeamProjectUri();
            string teamProjectName = RegistryManager.GetTeamProjectName();
            string projectDllPath = RegistryManager.GetProjectDllPath();
            InitializeFromRegistry(teamProjectUri, teamProjectName, projectDllPath);

        }

        private void InitializeFromRegistry(string teamProjectUri, string teamProjectName, string projectPathDll)
        {
            if (!String.IsNullOrEmpty(teamProjectUri) && !String.IsNullOrEmpty(teamProjectName))
            {
                ExecutionContext.Preferences.TfsUri = new Uri(teamProjectUri);
                ExecutionContext.Preferences.TestProjectName = teamProjectName;
                ExecutionContext.Tfs = new TfsTeamProjectCollection(ExecutionContext.Preferences.TfsUri);
                ExecutionContext.ProjectDllPath = projectPathDll;
                TestService = (ITestManagementService)ExecutionContext.Tfs.GetService(typeof(ITestManagementService));
                InitializeTestProjectByName(TestService, ExecutionContext.Preferences.TestProjectName);
                try
                {
                    InitializeTestPlans(ExecutionContext.TeamProject);
                    string fullTeamProjectName = GenerateFullTeamProjectName();
                    tbTfsProject.Text = fullTeamProjectName;                    
                }
                catch (SocketException)
                {
                    return;
                }
                catch(WebException)
                {
                    return;
                }
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            LoadProjectSelectionFromUser();
        }

        private void LoadProjectSelectionFromUser()
        {
            ExecutionContext.Preferences.TfsUri = null;
            ExecutionContext.Preferences.TestProjectName = null;

            try
            {
                using (var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false))
                {
                    var userSelected = projectPicker.ShowDialog();

                    if (userSelected == DialogResult.Cancel)
                        return;

                    if (projectPicker.SelectedTeamProjectCollection != null)
                    {
                        ExecutionContext.Preferences.TfsUri = projectPicker.SelectedTeamProjectCollection.Uri;
                        ExecutionContext.Preferences.TestProjectName = projectPicker.SelectedProjects[0].Name;
                        ExecutionContext.Tfs = projectPicker.SelectedTeamProjectCollection;
                        TestService = (ITestManagementService)ExecutionContext.Tfs.GetService(typeof(ITestManagementService));
                        InitializeTestProjectByName(TestService, ExecutionContext.Preferences.TestProjectName);
                        Task.Factory.StartNew(() =>
                        {
                            InitializeTestPlans(ExecutionContext.TeamProject);
                        }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                    string fullTeamProjectName = GenerateFullTeamProjectName();
                    tbTfsProject.Text = fullTeamProjectName;
                    RegistryManager.WriteCurrentTeamProjectName(ExecutionContext.Preferences.TestProjectName);
                    RegistryManager.WriteCurrentTeamProjectUri(ExecutionContext.Preferences.TfsUri.ToString());
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error selecting team project: " + ex.Message, "Error");
            }

            //_preferenceCollection.SaveToRegistry();
        }

        private static string GenerateFullTeamProjectName()
        {
            string fullTeamProjectName = String.Concat(ExecutionContext.Preferences.TfsUri, "/", ExecutionContext.Preferences.TestProjectName);
            return fullTeamProjectName;
        }

        private static void InitializeTestProjectByName(ITestManagementService test_service, string testProjectName)
        {
            ExecutionContext.TeamProject = test_service.GetTeamProject(testProjectName);
        }

        private void InitializeTestPlans(ITestManagementTeamProject _testproject)
        {
            cbTestPlans.Items.Clear();
            ITestPlanCollection testPlans = TestPlanManager.GetAllTestPlans(_testproject);
            List<string> testPlanNames = new List<string>();
            foreach (ITestPlan tp in testPlans)
            {
                testPlanNames.Add(tp.Name);
            }
            cbTestPlans.ItemsSource = testPlanNames;
            cbTestPlans.SelectedIndex = 0;
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedTestPlan = cbTestPlans.Text;
            if (String.IsNullOrEmpty(selectedTestPlan))
            {
                ModernDialog.ShowMessage("No test plan selected.", "Warning", MessageBoxButton.OK);
                return;
            }
            if (ExecutionContext.TeamProject == null)
            {
                ModernDialog.ShowMessage("No test project selected.", "Warning", MessageBoxButton.OK);
                return;
            }
            ExecutionContext.Preferences.TestPlan = TestPlanManager.GetAllTestPlans(ExecutionContext.TeamProject).Where(p => p.Name.Equals(selectedTestPlan)).FirstOrDefault();
            AddNewLinksToWindow();
        }

        private void AddNewLinksToWindow()
        {
            ModernWindow mw = Window.GetWindow(this) as ModernWindow;
            mw.MenuLinkGroups.Clear();
            LinkGroup lg = new LinkGroup();
            Link l1 = new Link();
            l1.DisplayName = "Initial View";
            Uri u1 = new Uri("/Views/TestCasesInitialView.xaml", UriKind.Relative);
            l1.Source = u1;
            mw.ContentSource = u1;
            lg.Links.Add(l1);
            Uri u2 = new Uri("/Views/TestCaseBatchDuplicateView.xaml", UriKind.Relative);
            Link l2 = new Link();
            l2.DisplayName = "Batch Replace/Duplicate";
            l2.Source = u2;
            lg.Links.Add(l2);
            mw.MenuLinkGroups.Add(lg);
        }

        private void cbTestPlans_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBox_DropdownBehavior.cbo_MouseMove(sender, e);
        }
    }
}
