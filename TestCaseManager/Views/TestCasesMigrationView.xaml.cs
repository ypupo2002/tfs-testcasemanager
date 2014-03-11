// <copyright file="TestCasesMigrationView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.Helpers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the project selection page
    /// </summary>
    public partial class TestCasesMigrationView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;	

		/// <summary>
		/// The team project
		/// </summary>
		private ITestManagementTeamProject initialTeamProject; 

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSelectionView"/> class.
        /// </summary>
		public TestCasesMigrationView()
        {
            this.InitializeComponent();
            isInitialized = false;
        }

        /// <summary>
        /// Gets or sets the project selection view model.
        /// </summary>
        /// <value>
        /// The project selection view model.
        /// </value>
		public TestCasesMigrationViewModel TestCasesMigrationViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            FragmentManager fm = new FragmentManager(e.Fragment);
        }

        /// <summary>
        /// Called when this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when a this instance becomes the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            isInitialized = false;
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbTestPlansDestination, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
			ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbTestPlansSource, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
        }

        /// <summary>
        /// Called just before this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        /// <remarks>
        /// The method is also invoked when parent frames are about to navigate.
        /// </remarks>
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized)
            {
                return;
            }
            this.ShowProgressBar();
			this.TestCasesMigrationViewModel = new TestCasesMigrationViewModel();
            bool showTfsServerUnavailableException = false;
            Task t = Task.Factory.StartNew(() =>
            {
				//this.ProjectSelectionViewModel.LoadProjectSettingsFromRegistry();
				//try
				//{
				//	ProjectSelectionViewModel.InitializeTestPlans(ExecutionContext.TestManagementTeamProject);
				//}
				//catch(Exception)
				//{
				//	showTfsServerUnavailableException = true;
				//}
            });
            t.ContinueWith(antecedent =>
            {
				this.DataContext = this.TestCasesMigrationViewModel;
                isInitialized = true;
				HideProgressBar();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        private void HideProgressBar()
        {
            progressBar.Visibility = System.Windows.Visibility.Hidden;
            mainGrid.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Shows the progress bar.
        /// </summary>
        private void ShowProgressBar()
        {
            progressBar.Visibility = System.Windows.Visibility.Visible;
            mainGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Handles the Click event of the DisplayButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MigrateButton_Click(object sender, RoutedEventArgs e)
        {
			//this.ProjectSelectionViewModel.SelectedTestPlan = cbTestPlans.Text;
			//if (string.IsNullOrEmpty(this.ProjectSelectionViewModel.SelectedTestPlan))
			//{
			//	ModernDialog.ShowMessage("No test plan selected.", "Warning", MessageBoxButton.OK);
			//	return;
			//}
			//if (ExecutionContext.TestManagementTeamProject == null)
			//{
			//	ModernDialog.ShowMessage("No test project selected.", "Warning", MessageBoxButton.OK);
			//	return;
			//}
			//RegistryManager.WriteCurrentTestPlan(this.ProjectSelectionViewModel.SelectedTestPlan);
			//try
			//{
			//	ExecutionContext.Preferences.TestPlan = TestPlanManager.GetTestPlanByName(ExecutionContext.TestManagementTeamProject, this.ProjectSelectionViewModel.SelectedTestPlan);
			//}
			//catch (Exception)
			//{
			//	ModernDialog.ShowMessage("Team Foundation services are unavailable and no test plans can be populated. Please try again after few seconds.", "Warning", MessageBoxButton.OK);
			//}
        }

		/// <summary>
		/// Handles the MouseMove event of the cbTestPlansDestination control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
		private void cbTestPlansDestination_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ComboBoxDropdownExtensions.cboMouseMove(sender, e);
		}

		/// <summary>
		/// Handles the MouseMove event of the cbTestPlansSource control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
		private void cbTestPlansSource_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ComboBoxDropdownExtensions.cboMouseMove(sender, e);
		}

		/// <summary>
		/// Handles the Click event of the btnMigrate control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnMigrate_Click(object sender, RoutedEventArgs e)
		{

		}

		/// <summary>
		/// Handles the Click event of the brnSourceBrowser control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void brnSourceBrowser_Click(object sender, RoutedEventArgs e)
		{
			var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
			this.TestCasesMigrationViewModel.LoadProjectSettingsFromUserDecisionSource(projectPicker);
		}

		/// <summary>
		/// Handles the Click event of the brnDestinationBrowser control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void brnDestinationBrowser_Click(object sender, RoutedEventArgs e)
		{
			var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
			this.TestCasesMigrationViewModel.LoadProjectSettingsFromUserDecisionDestination(projectPicker);
		}
    }
}
