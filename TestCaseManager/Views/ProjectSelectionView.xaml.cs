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
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    public partial class ProjectSelectionView : System.Windows.Controls.UserControl
    {
        public ProjectSelectionViewModel ProjectSelectionViewModel { get; set; }

        public ProjectSelectionView()
        {
            this.ProjectSelectionViewModel = new ProjectSelectionViewModel();
            InitializeComponent();
            ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                this.ProjectSelectionViewModel.InitializeFromRegistry();
                InitializeTestPlans(ExecutionContext.TestManagementTeamProject);
                tbTfsProject.Text = this.ProjectSelectionViewModel.FullTeamProjectName;
            });
            t.ContinueWith(antecedent =>
            {              
                HideProgressBar();
            }, TaskScheduler.FromCurrentSynchronizationContext());            
        }       

        private void HideProgressBar()
        {
            progressBar.Visibility = System.Windows.Visibility.Hidden;
            mainGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void ShowProgressBar()
        {
            progressBar.Visibility = System.Windows.Visibility.Visible;
            mainGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
            this.ProjectSelectionViewModel.LoadProjectSelectionFromUser(projectPicker);
            InitializeTestPlans(ExecutionContext.TestManagementTeamProject);
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
            if (ExecutionContext.TestManagementTeamProject == null)
            {
                ModernDialog.ShowMessage("No test project selected.", "Warning", MessageBoxButton.OK);
                return;
            }
            ExecutionContext.Preferences.TestPlan = TestPlanManager.GetTestPlanByName(ExecutionContext.TestManagementTeamProject, selectedTestPlan);
            AddNewLinksToWindow();
        }

        private void AddNewLinksToWindow()
        {
            ModernWindow mw = Window.GetWindow(this) as ModernWindow;
            mw.MenuLinkGroups.Clear();
            LinkGroup lg = new LinkGroup();
            Link l1 = new Link();
            l1.DisplayName = "All Test Cases";
            Uri u1 = new Uri("/Views/TestCasesInitialView.xaml", UriKind.Relative);
            l1.Source = u1;
            mw.ContentSource = u1;
            lg.Links.Add(l1);
            Uri u2 = new Uri("/Views/TestCaseBatchDuplicateView.xaml", UriKind.Relative);
            Link l2 = new Link();
            l2.DisplayName = "Find/Replace/Duplicate";
            l2.Source = u2;
            lg.Links.Add(l2);
            mw.MenuLinkGroups.Add(lg);
        }

        private void cbTestPlans_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cbo_MouseMove(sender, e);
        }
    }
}
