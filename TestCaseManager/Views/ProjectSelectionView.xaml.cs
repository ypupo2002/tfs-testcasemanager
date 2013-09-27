﻿// <copyright file="ProjectSelectionView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
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
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.Helpers;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the project selection page
    /// </summary>
    public partial class ProjectSelectionView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSelectionView"/> class.
        /// </summary>
        public ProjectSelectionView()
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
        public ProjectSelectionViewModel ProjectSelectionViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
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
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbTestPlans, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
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
            this.ProjectSelectionViewModel = new ProjectSelectionViewModel();
            Task t = Task.Factory.StartNew(() =>
            {
                this.ProjectSelectionViewModel.LoadProjectSettingsFromRegistry();
                ProjectSelectionViewModel.InitializeTestPlans(ExecutionContext.TestManagementTeamProject);
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = this.ProjectSelectionViewModel;
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
        /// Handles the Click event of the BrowseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
            this.ProjectSelectionViewModel.LoadProjectSettingsFromUserDecision(projectPicker);
            ProjectSelectionViewModel.InitializeTestPlans(ExecutionContext.TestManagementTeamProject);
        }

        /// <summary>
        /// Handles the Click event of the DisplayButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedTestPlan = cbTestPlans.Text;
            if (string.IsNullOrEmpty(selectedTestPlan))
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
            this.AddNewLinksToWindow();
        }

        /// <summary>
        /// Adds the new links to the window.
        /// </summary>
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

        /// <summary>
        /// Handles the MouseMove event of the cbTestPlans control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbTestPlans_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
             ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }
    }
}
