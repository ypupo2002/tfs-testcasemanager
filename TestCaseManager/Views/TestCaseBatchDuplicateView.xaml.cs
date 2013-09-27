// <copyright file="TestCaseBatchDuplicateView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using TestCaseManagerApp.BusinessLogic.Entities;
using TestCaseManagerApp.Helpers;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the batch duplicate, find replace page
    /// </summary>
    public partial class TestCaseBatchDuplicateView : UserControl, IContent
    {
        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseBatchDuplicateView"/> class.
        /// </summary>
        public TestCaseBatchDuplicateView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test cases batch duplicate view model.
        /// </summary>
        /// <value>
        /// The test cases batch duplicate view model.
        /// </value>
        public TestCasesBatchDuplicateViewModel TestCasesBatchDuplicateViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when a this instance becomes the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            isInitialized = false;
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbSuite, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
        }

        /// <summary>
        /// Called just before this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        /// <remarks>
        /// The method is also invoked when parent frames are about to navigate.
        /// </remarks>
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
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
            Task t = Task.Factory.StartNew(() =>
            {
                if (this.TestCasesBatchDuplicateViewModel != null)
                {
                    this.TestCasesBatchDuplicateViewModel = new ViewModels.TestCasesBatchDuplicateViewModel(this.TestCasesBatchDuplicateViewModel);
                    this.TestCasesBatchDuplicateViewModel.FilterTestCases();
                }
                else
                {
                    this.TestCasesBatchDuplicateViewModel = new ViewModels.TestCasesBatchDuplicateViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = this.TestCasesBatchDuplicateViewModel;
                this.HideProgressBar();
                this.tbTitleFilter.Focus();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        private void HideProgressBar()
        {
            this.progressBar.Visibility = System.Windows.Visibility.Hidden;
            this.mainGrid.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Shows the progress bar.
        /// </summary>
        private void ShowProgressBar()
        {
            this.progressBar.Visibility = System.Windows.Visibility.Visible;
            this.mainGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.ClearDefaultContent(ref TestCasesBatchDuplicateViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTextSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTextSuiteFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.ClearDefaultContent(ref TestCasesBatchDuplicateViewModel.InitialViewFilters.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.RestoreDefaultText("Title", ref TestCasesBatchDuplicateViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.RestoreDefaultText("Suite", ref TestCasesBatchDuplicateViewModel.InitialViewFilters.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_KeyUp(object sender, KeyEventArgs e)
        {
            TestCasesBatchDuplicateViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_KeyUp(object sender, KeyEventArgs e)
        {
            TestCasesBatchDuplicateViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_KeyUp(object sender, KeyEventArgs e)
        {
            TestCasesBatchDuplicateViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the MouseEnter event of the cbSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void cbSuite_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            {
                cbSuite.IsDropDownOpen = true;
                cbSuite.Focus();
            }          
        }

        /// <summary>
        /// Handles the MouseMove event of the cbSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void cbSuite_MouseMove(object sender, MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnBatchDuplicate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnBatchDuplicate_Click(object sender, RoutedEventArgs e)
        {
            this.InitializeCurrentSelectedTestCases();
            string newSuiteTitle = cbSuite.Text;
            List<TextReplacePair> textReplacePairsList = this.TestCasesBatchDuplicateViewModel.ObservableTextReplacePairs.ToList();
            List<SharedStepIdReplacePair> sharedStepIdReplacePairList = this.TestCasesBatchDuplicateViewModel.ObservableSharedStepIdReplacePairs.ToList();
            int duplicatedCount = 0;
            ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                foreach (TestCase currentSelectedTestCase in TestCasesBatchDuplicateViewModel.SelectedTestCases)
                {
                    currentSelectedTestCase.DuplicateTestCase(textReplacePairsList, sharedStepIdReplacePairList, newSuiteTitle, this.TestCasesBatchDuplicateViewModel.ReplaceInTitles, this.TestCasesBatchDuplicateViewModel.ReplaceSharedSteps, this.TestCasesBatchDuplicateViewModel.ReplaceInTestSteps);
                    duplicatedCount++;
                }
                this.TestCasesBatchDuplicateViewModel.FilterTestCases();
            });
            t.ContinueWith(antecedent =>
            {
                TestCasesBatchDuplicateViewModel.InitializeTestCases();
                HideProgressBar();
                ModernDialog.ShowMessage(string.Format("{0} test cases duplicated.", duplicatedCount), "Success!", MessageBoxButton.OK);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Handles the Click event of the btnFindAndReplace control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnFindAndReplace_Click(object sender, RoutedEventArgs e)
        {
            this.InitializeCurrentSelectedTestCases();
            List<TextReplacePair> textReplacePairsList = TestCasesBatchDuplicateViewModel.ObservableTextReplacePairs.ToList();
            List<SharedStepIdReplacePair> sharedStepIdReplacePairList = TestCasesBatchDuplicateViewModel.ObservableSharedStepIdReplacePairs.ToList();
            int replacedCount = 0;
            ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < TestCasesBatchDuplicateViewModel.SelectedTestCases.Count; i++)
                {
                    TestCasesBatchDuplicateViewModel.SelectedTestCases[i].FindAndReplaceInTestCase(textReplacePairsList, sharedStepIdReplacePairList, TestCasesBatchDuplicateViewModel.ReplaceInTitles, TestCasesBatchDuplicateViewModel.ReplaceSharedSteps, TestCasesBatchDuplicateViewModel.ReplaceInTestSteps);
                    replacedCount++;
                }               
            });
            t.ContinueWith(antecedent =>
            {
                TestCasesBatchDuplicateViewModel.InitializeTestCases();
                TestCasesBatchDuplicateViewModel.FilterTestCases();
                HideProgressBar();
                ModernDialog.ShowMessage(string.Format("{0} test cases replaced.", replacedCount), "Success!", MessageBoxButton.OK);
            }, TaskScheduler.FromCurrentSynchronizationContext());           
        }

        /// <summary>
        /// Initializes the current selected test cases.
        /// </summary>
        private void InitializeCurrentSelectedTestCases()
        {
            TestCasesBatchDuplicateViewModel.SelectedTestCases.Clear();
            foreach (TestCase currentSelectedItem in dgTestCases.SelectedItems)
            {
                TestCasesBatchDuplicateViewModel.SelectedTestCases.Add(currentSelectedItem);
            }
        }
    }
}