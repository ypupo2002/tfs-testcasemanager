// <copyright file="TestCasesInitialView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.BusinessLogic.Enums;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.ViewModels;
using TestCaseManagerCore;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case initial(search mode) page
    /// </summary>
    public partial class TestCasesInitialView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        public static RoutedCommand RemoveCommand = new RoutedCommand();

        /// <summary>
        /// The remove test case from suite command
        /// </summary>
        public static RoutedCommand RemoveTestCaseFromSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The rename suite command
        /// </summary>
        public static RoutedCommand RenameSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The add suite command
        /// </summary>
        public static RoutedCommand AddSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The remove suite command
        /// </summary>
        public static RoutedCommand RemoveSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The copy suite command
        /// </summary>
        public static RoutedCommand CopySuiteCommand = new RoutedCommand();

        /// <summary>
        /// The cut suite command
        /// </summary>
        public static RoutedCommand CutSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The paste suite command
        /// </summary>
        public static RoutedCommand PasteSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The copy test cases command
        /// </summary>
        public static RoutedCommand CopyTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The cut test cases command
        /// </summary>
        public static RoutedCommand CutTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The paste test cases command
        /// </summary>
        public static RoutedCommand PasteTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The export test cases command
        /// </summary>
        public static RoutedCommand ExportTestCasesCommand = new RoutedCommand();

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
                    this.TestCasesInitialViewModel = new TestCaseManagerCore.ViewModels.TestCasesInitialViewModel(this.TestCasesInitialViewModel);
                }
                else
                {
                    TestCasesInitialViewModel = new TestCaseManagerCore.ViewModels.TestCasesInitialViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = TestCasesInitialViewModel;
                this.UpdateButtonsStatus();
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
            EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            DuplicateCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));
            PreviewCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            NewCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Alt));
            RemoveCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
            ExportTestCasesCommand.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Alt));
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
                if (currentTestCase.ITestSuiteBase != null)
                {
                    log.InfoFormat("Preview test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                    this.NavigateToTestCasesDetailedView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                }
                else
                {
                    log.InfoFormat("Preview test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, -1);
                    this.NavigateToTestCasesDetailedView(currentTestCase.ITestCase.Id, -1);
                }
               
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
                if (currentTestCase.ITestSuiteBase != null)
                {
                    log.InfoFormat("Preview test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                    this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                }
                else
                {
                    log.InfoFormat("Preview test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, -1);
                    this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, -1);
                }
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
                if (currentTestCase.ITestSuiteBase != null)
                {
                    log.InfoFormat("Preview test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                    this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, true, true);
                }
                else
                {
                    log.InfoFormat("Preview test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, -1);
                    this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, -1, true, true);
                }                
            });
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            log.InfoFormat("Navigate to Create New Test Case, Suite Id: {0}", selectedSuiteId);
            this.NavigateToTestCasesEditView(selectedSuiteId, true, false);
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
            tbIdFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultId, ref this.TestCasesInitialViewModel.InitialViewFilters.IsIdTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultTitle, ref this.TestCasesInitialViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultSuite, ref this.TestCasesInitialViewModel.InitialViewFilters.IsSuiteTextSet);
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
        /// Handles the GotFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbAssignedToFilter.ClearDefaultContent(ref this.TestCasesInitialViewModel.InitialViewFilters.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbAssignedToFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultAssignedTo, ref this.TestCasesInitialViewModel.InitialViewFilters.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPriorityFilter.ClearDefaultContent(ref this.TestCasesInitialViewModel.InitialViewFilters.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbPriorityFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultPriority, ref this.TestCasesInitialViewModel.InitialViewFilters.IsPriorityTextSet);

        }

        /// <summary>
        /// Handles the KeyUp event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
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
            if (dgTestCases.SelectedItem == null)
            {
                return;
            }
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
            {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                if (currentTestCase.ITestSuiteBase != null)
                {
                    log.InfoFormat("Edit test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                    this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                }
                else
                {
                    log.InfoFormat("Edit test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, -1);
                    this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, -1);
                }                
            }
            else
            {
                this.PreviewSelectedTestCase();
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
            int selectedSuiteId = (int)tvSuites.SelectedValue;
                
            if (selectedSuiteId.Equals(-1) && this.TestCasesInitialViewModel.IsThereSubnodeSelected(this.TestCasesInitialViewModel.Suites))
            {
                return;
            }

            // Remove the initial view filters because we are currently filtering by suite and the old filters are not valid any more
            this.TestCasesInitialViewModel.ResetInitialFilters();
            RegistryManager.WriteSelectedSuiteId(selectedSuiteId);
            this.TestCasesInitialViewModel.TestCasesCount = "...";
            this.ShowTestCasesProgressbar();
            List<TestCase> suiteTestCaseCollection = new List<TestCase>();
            Task t = Task.Factory.StartNew(() =>
            {
                if (selectedSuiteId != -1)
                {
                    suiteTestCaseCollection = TestCaseManager.GetAllTestCaseFromSuite(selectedSuiteId);
                  
                }
                else if (isInitialized)
                {
                    suiteTestCaseCollection = TestCaseManager.GetAllTestCasesInTestPlan(true);
                    log.InfoFormat("Load all test cases in the test plan.");
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.TestCasesInitialViewModel.InitializeInitialTestCaseCollection(suiteTestCaseCollection);
                this.TestCasesInitialViewModel.FilterTestCases();
                this.HideTestCasesProgressbar();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Handles the Command event of the removeTestCaseFromSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void removeTestCaseFromSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.RemoveTestCaseFromSuiteInternal();
        }

        /// <summary>
        /// Handles the Command event of the renameSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void renameSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.RenameSuiteInternal();
            e.Handled = true;
        }

        /// <summary>
        /// Handles the Command event of the copySuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void copySuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            Suite suite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
            suite.CopyToClipboard(true);
        }

        /// <summary>
        /// Handles the Command event of the copyTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void copyTestCases_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestCase> testCases = this.GetSelectedTestCasesInternal();
            //List<LightTestCase> lightTetCases = TestCaseManager.GetLightTestCases(testCases);
            TestCaseManager.CopyToClipboardTestCases(true, testCases);
        }

        /// <summary>
        /// Handles the Command event of the cutTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void cutTestCases_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestCase> testCases = this.GetSelectedTestCasesInternal();
            //List<LightTestCase> lightTetCases = TestCaseManager.GetLightTestCases(testCases);
            TestCaseManager.CopyToClipboardTestCases(false, testCases);
        }

        /// <summary>
        /// Handles the Command event of the pasteTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void pasteTestCases_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            Suite parentSuite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
            ClipBoardTestCase clipBoardTestCase = TestCaseManager.GetFromClipboardTestCases();
            if (clipBoardTestCase != null)
            {
                this.PasteTestCasesToSuiteInternal(parentSuite, clipBoardTestCase);
            }
        }

        /// <summary>
        /// Gets the selected test cases internal.
        /// </summary>
        /// <returns>selected test cases list collection</returns>
        private List<TestCase> GetSelectedTestCasesInternal()
        {
            List<TestCase> testCases = new List<TestCase>();
            foreach (TestCase currentTestCase in dgTestCases.SelectedItems)
            {
                testCases.Add(currentTestCase);
            }
            return testCases;
        }

        /// <summary>
        /// Handles the Command event of the cutSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void cutSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            Suite suite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
            suite.CopyToClipboard(false);
        }

        /// <summary>
        /// Handles the Command event of the pasteSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void pasteSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            Suite suiteToPasteIn = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
            Suite clipboardSuite = Suite.GetFromClipboard();
            ClipBoardTestCase clipBoardTestCase = TestCaseManager.GetFromClipboardTestCases();

            if (clipboardSuite != null && suiteToPasteIn.Id.Equals(clipboardSuite.Id))
            {
                ModernDialog.ShowMessage("Cannot paste suite under itself!", "Warrning!", MessageBoxButton.OK);
                return;
            }

            if (clipboardSuite != null)
            {
                this.PasteSuiteToSelectedSuiteInternal(suiteToPasteIn, clipboardSuite);
            }
            else if (clipBoardTestCase != null)
            {
                this.PasteTestCasesToSuiteInternal(suiteToPasteIn, clipBoardTestCase);
            }
        }

        /// <summary>
        /// Pastes the suite automatic selected suite internal.
        /// </summary>
        /// <param name="suiteToPasteIn">The suite to paste in.</param>
        /// <param name="clipboardSuite">The clipboard suite.</param>
        private void PasteSuiteToSelectedSuiteInternal(Suite suiteToPasteIn, Suite clipboardSuite)
        {
            if (clipboardSuite.ClipBoardCommand.Equals(ClipBoardCommand.Copy))
            {
                this.TestCasesInitialViewModel.CopyPasteSuiteToParentSuite(suiteToPasteIn, clipboardSuite);
            }
            else
            {
                this.TestCasesInitialViewModel.CutPasteSuiteToParentSuite(suiteToPasteIn, clipboardSuite);
            }
        }

        /// <summary>
        /// Pastes the test cases automatic suite internal.
        /// </summary>
        /// <param name="suiteToPasteIn">The suite to paste in.</param>
        /// <param name="clipBoardTestCase">The clip board test case.</param>
        private void PasteTestCasesToSuiteInternal(Suite suiteToPasteIn, ClipBoardTestCase clipBoardTestCase)
        {
            this.ShowTestCasesProgressbar();
            Task t = Task.Factory.StartNew(() =>
            {
                if (clipBoardTestCase.ClipBoardCommand.Equals(ClipBoardCommand.Copy))
                {
                    TestSuiteManager.PasteTestCasesToSuite(suiteToPasteIn.Id, clipBoardTestCase);
                }
                else
                {
                    TestSuiteManager.PasteTestCasesToSuite(suiteToPasteIn.Id, clipBoardTestCase);
                }
            });
            t.ContinueWith(antecedent =>
            {
                if (clipBoardTestCase.ClipBoardCommand.Equals(ClipBoardCommand.Cut))
                {
                    System.Windows.Forms.Clipboard.Clear();
                }
                this.TestCasesInitialViewModel.AddTestCasesToObservableCollection(suiteToPasteIn, clipBoardTestCase.TestCases[0].TestSuiteId);
                this.HideTestCasesProgressbar();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Shows the test cases progressbar.
        /// </summary>
        private void ShowTestCasesProgressbar()
        {
            progressBarTestCases.Visibility = System.Windows.Visibility.Visible;
            dgTestCases.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Hides the test cases progressbar.
        /// </summary>
        private void HideTestCasesProgressbar()
        {
            progressBarTestCases.Visibility = System.Windows.Visibility.Hidden;
            dgTestCases.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Handles the Command event of the removeSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void removeSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.RemoveSuiteInternal();
            e.Handled = true;
        }

        /// <summary>
        /// Removes the suite internal.
        /// </summary>
        private void RemoveSuiteInternal()
        {
            int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            try
            {
                if (ModernDialog.ShowMessage("If you delete this test suite, you will also delete all test suites that are children of this test suite!", "Delete this test suite?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    TestSuiteManager.DeleteSuite(selectedSuiteId);
                    this.TestCasesInitialViewModel.DeleteSuiteObservableCollection(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
                }
            }
            catch (ArgumentException)
            {
                ModernDialog.ShowMessage("The root suite cannot be deleted!", "Warrning!", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Handles the Command event of the addSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void addSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.AddSuiteInternal();
            e.Handled = true;
        }

        /// <summary>
        /// Renames the suite internal.
        /// </summary>
        private void RenameSuiteInternal()
        {
            Suite selectedSuite = tvSuites.SelectedItem as Suite;
            RegistryManager.WriteTitleTitlePromtDialog(selectedSuite.Title);

            var dialog = new PrompDialogWindow();
            dialog.ShowDialog();
            bool isCanceled;
            string newTitle;
            Task t = Task.Factory.StartNew(() =>
            {
                isCanceled = RegistryManager.GetIsCanceledTitlePromtDialog();
                newTitle = RegistryManager.GetTitleTitlePromtDialog();
                while (string.IsNullOrEmpty(newTitle) && !isCanceled)
                {
                }
            });
            t.Wait();
            isCanceled = RegistryManager.GetIsCanceledTitlePromtDialog();
            newTitle = RegistryManager.GetTitleTitlePromtDialog();
            if (!isCanceled)
            {
                int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
                if (selectedSuiteId == -1)
                {
                    ModernDialog.ShowMessage("Cannot rename root suite!", "Warrning!", MessageBoxButton.OK);
                    return;
                }
                TestSuiteManager.RenameSuite(selectedSuiteId, newTitle);
                this.TestCasesInitialViewModel.RenameSuiteInObservableCollection(this.TestCasesInitialViewModel.Suites, selectedSuiteId, newTitle);
            }
        }

        /// <summary>
        /// Adds the suite internal.
        /// </summary>
        private void AddSuiteInternal()
        {
            RegistryManager.WriteTitleTitlePromtDialog(string.Empty);

            var dialog = new PrompDialogWindow();
            dialog.ShowDialog();
            bool isCanceled;
            string newTitle;
            Task t = Task.Factory.StartNew(() =>
            {
                isCanceled = RegistryManager.GetIsCanceledTitlePromtDialog();
                newTitle = RegistryManager.GetTitleTitlePromtDialog();
                while (string.IsNullOrEmpty(newTitle) && !isCanceled)
                {
                }
            });
            t.Wait();
            isCanceled = RegistryManager.GetIsCanceledTitlePromtDialog();
            newTitle = RegistryManager.GetTitleTitlePromtDialog();
            if (!isCanceled)
            {
                int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
                bool canBeAddedNewSuite = false;
                int? newSuiteId = TestSuiteManager.AddChildSuite(selectedSuiteId, newTitle, out canBeAddedNewSuite);
                if (canBeAddedNewSuite)
                {
                    this.TestCasesInitialViewModel.AddChildSuiteObservableCollection(this.TestCasesInitialViewModel.Suites, selectedSuiteId, (int)newSuiteId);
                }
                else
                {
                    ModernDialog.ShowMessage("Cannot add new suite to Requirments Suite!", "Warrning!", MessageBoxButton.OK);
                }
            }
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

            this.UpdateSuiteContextMenuItemsStatus();
        }

        /// <summary>
        /// Updates the suite context menu items status.
        /// </summary>
        private void UpdateSuiteContextMenuItemsStatus()
        {
            int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            Suite suite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);

            // Quit if its requirment based suite because no child suites are allowed
            if (suite.IsPasteAllowed)
            {
                suite.IsPasteEnabled = true;
                return;
            }

            Suite clipBoardItem = Suite.GetFromClipboard();
            if (clipBoardItem == null)
            {
                suite.IsPasteEnabled = false;
            }
        }

        /// <summary>
        /// Handles the PreviewMouseRightButtonDown event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.UpdateTestCasesContextMenuItemsStatus();
        }

        /// <summary>
        /// Updates the test cases context menu items status.
        /// </summary>
        private void UpdateTestCasesContextMenuItemsStatus()
        {
            int selectedSuiteId = RegistryManager.GetSelectedSuiteId();
            Suite suite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);

            // If the selected suite is requirement based the paste command is not enabled or if the clipboard object is not list of testcases
            dgTestCaseContextItemPaste.IsEnabled = true;
            ClipBoardTestCase clipBoardTestCase = TestCaseManager.GetFromClipboardTestCases();
            if (clipBoardTestCase == null || !suite.IsPasteAllowed)
            {
                dgTestCaseContextItemPaste.IsEnabled = false;
                suite.IsPasteEnabled = false;
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

        /// <summary>
        /// Handles the Click event of the btnRemoveTestCase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRemoveTestCase_Click(object sender, RoutedEventArgs e)
        {           
            this.RemoveTestCaseFromSuiteInternal();
        }

        /// <summary>
        /// Removes the test case from suite internal.
        /// </summary>
        private void RemoveTestCaseFromSuiteInternal()
        {
            if (dgTestCases.SelectedItems.Count == 0)
            {
                this.DisplayNonSelectionWarning();
                return;
            }
            int selectedIndex = dgTestCases.SelectedIndex;
            do
            {
                TestCase currentTestCase = dgTestCases.SelectedItems[0] as TestCase;
                this.TestCasesInitialViewModel.RemoveTestCaseFromSuite(currentTestCase);
            }
            while (dgTestCases.SelectedItems.Count != 0);
            if (selectedIndex == dgTestCases.Items.Count)
            {
                dgTestCases.SelectedIndex = selectedIndex - 1;
            }
            else
            {
                dgTestCases.SelectedIndex = selectedIndex;
            }
            dgTestCases.Focus();
        }

        /// <summary>
        /// Handles the SelectedCellsChanged event of the dgTestCases control. Disable Preview and Duplicate buttons if more than one row is selected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            this.UpdateButtonsStatus();
            this.TestCasesInitialViewModel.SelectedTestCasesCount = this.dgTestCases.SelectedItems.Count.ToString();
        }

        /// <summary>
        /// Updates the buttons status.
        /// </summary>
        private void UpdateButtonsStatus()
        {
            btnPreview.IsEnabled = true;
            btnDuplicate.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnPreview1.IsEnabled = true;
            btnDuplicate1.IsEnabled = true;
            btnEdit1.IsEnabled = true;
            btnExport.IsEnabled = true;
            btnExport1.IsEnabled = true;
            btnRemoveTestCase.IsEnabled = true;
            btnRemoveTestCase1.IsEnabled = true;
            dgTestCaseContextItemEdit.IsEnabled = true;
            dgTestCaseContextItemPreview.IsEnabled = true;
            dgTestCaseContextItemDuplicate.IsEnabled = true;

            dgTestCaseContextItemCopy.IsEnabled = true;
            dgTestCaseContextItemCut.IsEnabled = true;
            dgTestCaseContextItemRemove.IsEnabled = true;
            if (dgTestCases.SelectedItems.Count < 1)
            {
                btnPreview.IsEnabled = false;
                btnDuplicate.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnPreview1.IsEnabled = false;
                btnDuplicate1.IsEnabled = false;
                btnEdit1.IsEnabled = false;
                btnRemoveTestCase.IsEnabled = false;
                btnRemoveTestCase1.IsEnabled = false;             
                dgTestCaseContextItemEdit.IsEnabled = false;
                dgTestCaseContextItemPreview.IsEnabled = false;
                dgTestCaseContextItemDuplicate.IsEnabled = false;

                dgTestCaseContextItemCopy.IsEnabled = false;
                dgTestCaseContextItemCut.IsEnabled = false;
                dgTestCaseContextItemRemove.IsEnabled = false;
            }

            if (this.TestCasesInitialViewModel.ObservableTestCases.Count == 0)
            {
                btnExport.IsEnabled = false;
                btnExport1.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".html";
            dlg.Filter = "Html Files (*.html)|*.html";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.ShowTestCasesProgressbar();
                List<TestCase> suiteTestCaseCollection = new List<TestCase>();
                Task t = Task.Factory.StartNew(() =>
                {
                    this.TestCasesInitialViewModel.ExportTestCases(filename);
                });
                t.ContinueWith(antecedent =>
                {
                    this.HideTestCasesProgressbar();
                    ModernDialog.ShowMessage("Test Cases Exported!", "Success!", MessageBoxButton.OK);
                    System.Diagnostics.Process.Start(filename);
                }, TaskScheduler.FromCurrentSynchronizationContext());                
            }            
        }
    }
}