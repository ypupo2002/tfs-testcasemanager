// <copyright file="TestCasesInitialView.xaml.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerApp.BusinessLogic.Entities;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case initial(search mode) page
    /// </summary>
    public partial class TestCasesInitialView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// The edit command
        /// </summary>
        public static RoutedCommand EditCommand = new RoutedCommand();

        /// <summary>
        /// The duplicate command
        /// </summary>
        public static RoutedCommand DuplicateCommand = new RoutedCommand();

        /// <summary>
        /// The preview command
        /// </summary>
        public static RoutedCommand PreviewCommand = new RoutedCommand();

        /// <summary>
        /// The new command
        /// </summary>
        public static RoutedCommand NewCommand = new RoutedCommand();

        /// <summary>
        /// The refresh command
        /// </summary>
        public static RoutedCommand RefreshCommand = new RoutedCommand();

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialView"/> class.
        /// </summary>
        public TestCasesInitialView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test cases initial view model.
        /// </summary>
        /// <value>
        /// The test cases initial view model.
        /// </value>
        public TestCasesInitialViewModel TestCasesInitialViewModel { get; set; }

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
        /// Handles the Loaded event of the TestCaseInitialView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TestCaseInitialView_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized)
            {
                return;
            }
            this.ShowProgressBar();
            this.InitializeFastKeys();
            Task t = Task.Factory.StartNew(() =>
            {
                if (this.TestCasesInitialViewModel != null)
                {
                    this.TestCasesInitialViewModel = new ViewModels.TestCasesInitialViewModel(this.TestCasesInitialViewModel);
                    this.TestCasesInitialViewModel.FilterTestCases();
                }
                else
                {
                    TestCasesInitialViewModel = new ViewModels.TestCasesInitialViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = TestCasesInitialViewModel;
                this.HideProgressBar();
                this.tbTitleFilter.Focus();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Initializes the fast keys.
        /// </summary>
        private void InitializeFastKeys()
        {
            EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            DuplicateCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            PreviewCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
            NewCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            RefreshCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
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
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            this.PreviewSelectedTestCase();
        }

        /// <summary>
        /// Previews the selected test case.
        /// </summary>
        private void PreviewSelectedTestCase()
        {
            this.ValidateTestCaseSelection(() =>
            {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                this.NavigateToTestCasesDetailedView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
            });     
        }

        /// <summary>
        /// Validates the test case selection.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ValidateTestCaseSelection(Action action)
        {
            if (dgTestCases.SelectedIndex != -1)
            {
                action.Invoke();               
            }
            else
            {
                this.DisplayNonSelectionWarning();
            }
        }

        /// <summary>
        /// Displays the non selection warning.
        /// </summary>
        private void DisplayNonSelectionWarning()
        {
            ModernDialog.ShowMessage("No selected test case.", "Warning", MessageBoxButton.OK);
        }

        /// <summary>
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateTestCaseSelection(() =>
            {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
            });           
        }

        /// <summary>
        /// Handles the Click event of the DuplicateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateTestCaseSelection(() =>
            {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, true, true);
            });           
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(true, false);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.ClearDefaultContent(ref TestCasesInitialViewModel.InitialViewFilters.IsIdTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.ClearDefaultContent(ref TestCasesInitialViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTextSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTextSuiteFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.ClearDefaultContent(ref TestCasesInitialViewModel.InitialViewFilters.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.RestoreDefaultText("ID", ref TestCasesInitialViewModel.InitialViewFilters.IsIdTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.RestoreDefaultText("Title", ref TestCasesInitialViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.RestoreDefaultText("Suite", ref TestCasesInitialViewModel.InitialViewFilters.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();     
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.PreviewSelectedTestCase();
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
            {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
            }
        }

        /// <summary>
        /// Handles the Selected event of the treeViewItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void treeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            int suiteId = (int)tvSuites.SelectedValue;
            if (suiteId.Equals(-1) && this.TestCasesInitialViewModel.IsThereSubnodeSelected(this.TestCasesInitialViewModel.Suites))
            {
                return;
            }

            // Remove the initial view filters because we are currently filtering by suite and the old filters are not valid any more
            this.TestCasesInitialViewModel.InitialViewFilters = new InitialViewFilters();
            RegistryManager.WriteSelectedSuiteIdFilter(suiteId);
            progressBarTestCases.Visibility = System.Windows.Visibility.Visible;
            dgTestCases.Visibility = System.Windows.Visibility.Hidden;
            List<TestCase> suiteTestCaseCollection = new List<TestCase>();
            Task t = Task.Factory.StartNew(() =>
            {              
                if (suiteId != -1)
                {
                    suiteTestCaseCollection = TestCaseManager.GetAllTestCaseFromSuite(suiteId);
                }
                else if (isInitialized)
                {
                    suiteTestCaseCollection = TestCaseManager.GetAllTestCasesInTestPlan();
                }           
            });
            t.ContinueWith(antecedent =>
            {
                this.TestCasesInitialViewModel.InitializeInitialTestCaseCollection(suiteTestCaseCollection);
                this.TestCasesInitialViewModel.FilterTestCases();
                progressBarTestCases.Visibility = System.Windows.Visibility.Hidden;
                dgTestCases.Visibility = System.Windows.Visibility.Visible;
            }, TaskScheduler.FromCurrentSynchronizationContext());       
        }

        /// <summary>
        /// Handles the PreviewMouseRightButtonDown event of the TreeViewItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TreeViewItem treeViewItem = this.VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Visuals the upward search.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>parent tree view item</returns>
        private TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            return source as TreeViewItem;
        }

        /// <summary>
        /// Handles the Unchecked event of the cbHideAutomated control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void cbHideAutomated_Unchecked(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the Checked event of the cbHideAutomated control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void cbHideAutomated_Checked(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the KeyDown event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                this.PreviewSelectedTestCase();
            }
        }
    }
}