// <copyright file="TestCaseDetailedView.xaml.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case detailed(read mode) page
    /// </summary>
    public partial class TestCaseDetailedView : UserControl, IContent
    {
        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseDetailedView"/> class.
        /// </summary>
        public TestCaseDetailedView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test case unique identifier.
        /// </summary>
        /// <value>
        /// The test case unique identifier.
        /// </value>
        public int TestCaseId { get; set; }

        /// <summary>
        /// Gets or sets the test suite unique identifier.
        /// </summary>
        /// <value>
        /// The test suite unique identifier.
        /// </value>
        public int TestSuiteId { get; set; }

        /// <summary>
        /// Gets or sets the test case detailed view model.
        /// </summary>
        /// <value>
        /// The test case detailed view model.
        /// </value>
        public TestCaseDetailedViewModel TestCaseDetailedViewModel { get; set; }

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
            Task t = Task.Factory.StartNew(() =>
            {
                TestCaseDetailedViewModel = new TestCaseDetailedViewModel(this.TestCaseId, this.TestSuiteId);
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = TestCaseDetailedViewModel;
                this.HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());   
        }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            isInitialized = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            this.TestCaseId = int.Parse(fm.Fragments["id"]);
            this.TestSuiteId = int.Parse(fm.Fragments["suiteId"]);
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
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
        }

        /// <summary>
        /// Handles the Click event of the DuplicateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id, true, true);
        }   
    }
}
