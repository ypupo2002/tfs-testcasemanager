// <copyright file="TestCaseEditView.xaml.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.Helpers;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case edit(edit mode) page
    /// </summary>
    public partial class TestCaseEditView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// The save command
        /// </summary>
        public static RoutedCommand SaveCommand = new RoutedCommand();

        /// <summary>
        /// The save and close command
        /// </summary>
        public static RoutedCommand SaveAndCloseCommand = new RoutedCommand();

        /// <summary>
        /// The share step command
        /// </summary>
        public static RoutedCommand ShareStepCommand = new RoutedCommand();

        /// <summary>
        /// The change step command
        /// </summary>
        public static RoutedCommand ChangeStepCommand = new RoutedCommand();

        /// <summary>
        /// The insert step command
        /// </summary>
        public static RoutedCommand InsertStepCommand = new RoutedCommand();

        /// <summary>
        /// The edit step command
        /// </summary>
        public static RoutedCommand EditStepCommand = new RoutedCommand();

        /// <summary>
        /// The associate test command
        /// </summary>
        public static RoutedCommand AssociateTestCommand = new RoutedCommand();

        /// <summary>
        /// The add shared step command
        /// </summary>
        public static RoutedCommand AddSharedStepCommand = new RoutedCommand();

        /// <summary>
        /// The delete test step command
        /// </summary>
        public static RoutedCommand DeleteTestStepCommand = new RoutedCommand();

        /// <summary>
        /// The move up test step command
        /// </summary>
        public static RoutedCommand MoveUpTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The move down test step command
        /// </summary>
        public static RoutedCommand MoveDownTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEditView"/> class.
        /// </summary>
        public TestCaseEditView()
        {
            this.InitializeComponent();
            this.InitializeFastKeys();
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
        /// Gets or sets a value indicating whether [create new].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [create new]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateNew { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [duplicate].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [duplicate]; otherwise, <c>false</c>.
        /// </value>
        public bool Duplicate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [comes from associated automation].
        /// </summary>
        /// <value>
        /// <c>true</c> if [comes from associated automation]; otherwise, <c>false</c>.
        /// </value>
        public bool ComesFromAssociatedAutomation { get; set; }

        /// <summary>
        /// Gets or sets the current edited step unique identifier.
        /// </summary>
        /// <value>
        /// The current edited step unique identifier.
        /// </value>
        public string CurrentEditedStepGuid { get; set; }

        /// <summary>
        /// Gets or sets the test case edit view model.
        /// </summary>
        /// <value>
        /// The test case edit view model.
        /// </value>
        public TestCaseEditViewModel TestCaseEditViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            this.InitializeUrlParameters(e);
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
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbArea, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbPriority, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
            //ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbSuite, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
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
            Task t = Task.Factory.StartNew(() =>
            {
                TestCaseEditViewModel = new TestCaseEditViewModel(TestCaseId, TestSuiteId, CreateNew, Duplicate);
            });
            t.ContinueWith(antecedent =>
            {
                this.InitializeUiRelatedViewSettings();
                InitializePageTitle();
                this.HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Initializes the page title.
        /// </summary>
        private void InitializePageTitle()
        {
            if (CreateNew && !Duplicate)
            {
                tbPageTitle.Text = "Create New";
            }
            else if (CreateNew && Duplicate)
            {
                tbPageTitle.Text = "Duplicate";
            }
            else
            {
                tbPageTitle.Text = "Edit";
            }
        }

        /// <summary>
        /// Initializes the UI related view settings.
        /// </summary>
        private void InitializeUiRelatedViewSettings()
        {
            this.DataContext = this.TestCaseEditViewModel;

            rtbAction.SetText(TestCaseEditViewModel.ActionDefaultText);
            rtbExpectedResult.SetText(TestCaseEditViewModel.ExpectedResultDefaultText);
            tbSharedStepFilter.Text = TestCaseEditViewModel.SharedStepSearchDefaultText;

            if (this.Duplicate || !this.CreateNew)
            {
                this.SetTestCasePropertiesFromDuplicateTestCase();
            }
            else if (!this.Duplicate && this.CreateNew)
            {
                this.SetTestCasePropertiesToDefault();
                btnDuplicate.IsEnabled = false;
            }
            else
            {
                btnDuplicate.IsEnabled = false;
            }
        }

        /// <summary>
        /// Sets the test case properties from the test case from which we duplicate.
        /// </summary>
        private void SetTestCasePropertiesFromDuplicateTestCase()
        {
            cbArea.SelectedIndex = this.TestCaseEditViewModel.Areas.FindIndex(0, x => x.Equals(this.TestCaseEditViewModel.TestCase.ITestCase.Area));
            cbPriority.SelectedIndex = this.TestCaseEditViewModel.Priorities.FindIndex(0, x => x.Equals(this.TestCaseEditViewModel.TestCase.ITestCase.Priority));
            //cbSuite.SelectedIndex = this.TestCaseEditViewModel.TestSuiteList.FindIndex(0, x => x.Title.Equals(this.TestCaseEditViewModel.TestCase.ITestSuiteBase.Title));
        }

        /// <summary>
        /// Sets the test case properties to default values.
        /// </summary>
        private void SetTestCasePropertiesToDefault()
        {
            cbArea.SelectedIndex = 0;
            cbPriority.SelectedIndex = 0;
            //cbSuite.SelectedIndex = 0;
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
        /// Initializes the URL parameters.
        /// </summary>
        /// <param name="e">The <see cref="FragmentNavigationEventArgs"/> instance containing the event data.</param>
        private void InitializeUrlParameters(FragmentNavigationEventArgs e)
        {
            this.CreateNew = false;
            this.Duplicate = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            string testCaseId = fm.Get("id");
            if (!string.IsNullOrEmpty(testCaseId))
            {
                this.TestCaseId = int.Parse(testCaseId);
            }
            string suiteId = fm.Get("suiteId");
            if (!string.IsNullOrEmpty(suiteId))
            {
                this.TestSuiteId = int.Parse(suiteId);
            }
            string createNew = fm.Get("createNew");
            if (!string.IsNullOrEmpty(createNew))
            {
                this.CreateNew = bool.Parse(createNew);
            }
            string duplicate = fm.Get("duplicate");
            if (!string.IsNullOrEmpty(duplicate))
            {
                this.Duplicate = bool.Parse(duplicate);
            }
            string comesFromAssociatedAutomation = fm.Get("comesFromAssociatedAutomation");
            if (!string.IsNullOrEmpty(comesFromAssociatedAutomation))
            {
                this.ComesFromAssociatedAutomation = bool.Parse(comesFromAssociatedAutomation);
            }
        }

        /// <summary>
        /// Initializes the fast keys.
        /// </summary>
        private void InitializeFastKeys()
        {
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            SaveAndCloseCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Shift | ModifierKeys.Control));
            AssociateTestCommand.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            DeleteTestStepCommand.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.Alt));
            MoveUpTestStepsCommand.InputGestures.Add(new KeyGesture(Key.Up, ModifierKeys.Alt));
            MoveDownTestStepsCommand.InputGestures.Add(new KeyGesture(Key.Down, ModifierKeys.Alt));
            AddSharedStepCommand.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
            ShareStepCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            EditStepCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            ChangeStepCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            InsertStepCommand.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));
        }

        /// <summary>
        /// Handles the Click event of the btnInsertStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInsertStep_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dgTestSteps.SelectedIndex;
            string stepTitle = TestCaseEditViewModel.GetStepTitle(rtbAction.GetText());
            string expectedResult = TestCaseEditViewModel.GetExpectedResult(rtbExpectedResult.GetText());
            TestStep testStepToInsert = TestStepManager.CreateNewTestStep(TestCaseEditViewModel.TestCase, stepTitle, expectedResult);
            TestCaseEditViewModel.InsertTestStepInTestCase(testStepToInsert, selectedIndex);
            dgTestSteps.SelectedIndex = dgTestSteps.SelectedIndex + 1;
            dgTestSteps.Focus();
        }

        /// <summary>
        /// Handles the Click event of the btnShare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PrompDialogWindow();
            dialog.ShowDialog();

            Task t = Task.Factory.StartNew(() => 
            {
                while (string.IsNullOrEmpty(ExecutionContext.SharedStepTitle) && !ExecutionContext.SharedStepTitleDialogCancelled) 
                { 
                } 
            });
            t.Wait();

            if (!ExecutionContext.SharedStepTitleDialogCancelled)
            {
                List<TestStep> selectedTestSteps = new List<TestStep>();
                this.GetTestStepsFromGrid(selectedTestSteps);
                bool isThereSharedStepSelected = TestStepManager.IsThereSharedStepSelected(selectedTestSteps);
                if (isThereSharedStepSelected)
                {
                    ModernDialog.ShowMessage("Shared steps cannon be shared again!", "Warning", MessageBoxButton.OK);
                    return;
                }

                ISharedStep sharedStepCore = this.TestCaseEditViewModel.TestCase.CreateNewSharedStep(ExecutionContext.SharedStepTitle, selectedTestSteps);
                sharedStepCore.Refresh();
                this.TestCaseEditViewModel.ObservableSharedSteps.Add(new SharedStep(sharedStepCore));
            }

            ExecutionContext.SharedStepTitleDialogCancelled = false;
        }

        /// <summary>
        /// Gets the test steps from grid.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        private void GetTestStepsFromGrid(List<TestStep> selectedTestSteps)
        {
            foreach (var item in dgTestSteps.SelectedItems)
            {
                selectedTestSteps.Add(item as TestStep);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDeleteStep_Click(object sender, RoutedEventArgs e)
        {
            this.DeleteStepInternal();   
        }

        /// <summary>
        /// Handles the Command event of the deleteStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void deleteStep_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.DeleteStepInternal();
            dgTestSteps.Focus();
        }

        /// <summary>
        /// Deletes the test steps internal.
        /// </summary>
        private void DeleteStepInternal()
        {
            if (dgTestSteps.SelectedItems != null)
            {
                List<TestStep> testStepsToBeRemoved = TestCaseEditViewModel.MarkInitialStepsToBeRemoved(dgTestSteps.SelectedItems.Cast<TestStep>().ToList());
                this.TestCaseEditViewModel.RemoveTestSteps(testStepsToBeRemoved);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteSharedStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDeleteSharedStep_Click(object sender, RoutedEventArgs e)
        {
            if (dgTestSteps.SelectedItems != null)
            {
                List<TestStep> testStepsToBeRemoved = this.TestCaseEditViewModel.MarkStepsToBeRemoved(dgTestSteps.SelectedItems.Cast<TestStep>().ToList());
                this.TestCaseEditViewModel.DeleteAllMarkedStepsForRemoval(testStepsToBeRemoved);              
            }
        }

        /// <summary>
        /// Handles the Click event of the btnMoveUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            this.MoveUpInternal();            
        }

        /// <summary>
        /// Handles the Command event of the moveUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void moveUp_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.MoveUpInternal();
            dgTestSteps.Focus();
        }

        /// <summary>
        /// Handles the Command event of the moveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void moveDown_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.MoveDownInternal();
            dgTestSteps.Focus();
        }

        /// <summary>
        /// Moves up test steps internal.
        /// </summary>
        private void MoveUpInternal()
        {
            // validate the move if it's out of the boudaries return
            if (dgTestSteps.SelectedItems.Count == 0)
            {
                return;
            }
            int startIndex = TestCaseEditViewModel.ObservableTestSteps.IndexOf(dgTestSteps.SelectedItems[0] as TestStep);
            if (startIndex == 0)
            {
                return;
            }
            int count = dgTestSteps.SelectedItems.Count;
            TestCaseEditViewModel.CreateNewTestStepCollectionAfterMoveUp(startIndex, count);

            this.SelectNextItemsAfterMoveUp(startIndex, count);
            if (dgTestSteps.SelectedItems.Count == 0)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnMoveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            this.MoveDownInternal();
        }

        /// <summary>
        /// Moves down test steps internal.
        /// </summary>
        private void MoveDownInternal()
        {
            // validate the move if it's out of the boudaries return
            if (dgTestSteps.SelectedItems.Count == 0)
            {
                return;
            }
            int startIndex = TestCaseEditViewModel.ObservableTestSteps.IndexOf(dgTestSteps.SelectedItems[0] as TestStep);
            int count = dgTestSteps.SelectedItems.Count;
            if (startIndex == TestCaseEditViewModel.ObservableTestSteps.Count - 1)
            {
                return;
            }
            if ((startIndex + count) >= TestCaseEditViewModel.ObservableTestSteps.Count)
            {
                return;
            }

            TestCaseEditViewModel.CreateNewTestStepCollectionAfterMoveDown(startIndex, count);

            this.SelectNextItemsAfterMoveDown(startIndex, count);
        }

        /// <summary>
        /// Selects the next test steps after move up.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedTestStepsCount">The selected test steps count.</param>
        private void SelectNextItemsAfterMoveUp(int startIndex, int selectedTestStepsCount)
        {
            dgTestSteps.SelectedItems.Clear();
            for (int i = startIndex - 1; i < startIndex + selectedTestStepsCount - 1; i++)
            {
                dgTestSteps.SelectedItems.Add(dgTestSteps.Items[i]);
            }
        }

        /// <summary>
        /// Selects the next test steps after move down.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedStepsCount">The selected test steps count.</param>
        private void SelectNextItemsAfterMoveDown(int startIndex, int selectedStepsCount)
        {
            dgTestSteps.SelectedItems.Clear();
            for (int i = startIndex + 1; i < startIndex + selectedStepsCount + 1; i++)
            {
                dgTestSteps.SelectedItems.Add(dgTestSteps.Items[i]);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            if (currentSharedStep == null)
            {
                ModernDialog.ShowMessage("Please select a shared step to add first!", "Warning", MessageBoxButton.OK);
                return;
            }
            int currentSelectedIndex = dgTestSteps.SelectedIndex;
            this.TestCaseEditViewModel.InsertSharedStep(currentSharedStep, currentSelectedIndex + 1);
            dgTestSteps.SelectedIndex = dgTestSteps.SelectedIndex + currentSharedStep.ISharedStep.Actions.Count;
            dgTestSteps.Focus();
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the dgTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
            {
                this.EditCurrentTestStepInternal();
            }
        }

        /// <summary>
        /// Edits the current test step internal.
        /// </summary>
        private void EditCurrentTestStepInternal()
        {
            this.EnableSaveStepButton();
            rtbAction.ClearDefaultContent(ref this.TestCaseEditViewModel.IsActionTextSet);
            rtbExpectedResult.ClearDefaultContent(ref this.TestCaseEditViewModel.IsExpectedResultTextSet);
            TestStep currentTestStep = this.GetSelectedTestStep();
            this.CurrentEditedStepGuid = currentTestStep.StepGuid;
            rtbAction.SetText(currentTestStep.ITestStep.Title);
            rtbExpectedResult.SetText(currentTestStep.ITestStep.ExpectedResult);
        }

        /// <summary>
        /// Gets the selected test step.
        /// </summary>
        /// <returns>the selected test step</returns>
        private TestStep GetSelectedTestStep()
        {
            TestStep currentTestStep = dgTestSteps.SelectedItem as TestStep;
            return currentTestStep;
        }

        /// <summary>
        /// Enables the save step button.
        /// </summary>
        private void EnableSaveStepButton()
        {
            btnSaveTestStep.IsEnabled = true;
            btnCancelEdit.IsEnabled = true;
            btnEdit.IsEnabled = false;
            btnInsertStep.IsEnabled = false;
            btnShare.IsEnabled = false;
            btnDeleteStep.IsEnabled = false;
            btnCancelEdit.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Handles the Click event of the btnSaveTestStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSaveTestStep_Click(object sender, RoutedEventArgs e)
        {
            this.DisableSaveButton();
            TestStep currentTestStep = this.TestCaseEditViewModel.ObservableTestSteps.Where(x => x.StepGuid.Equals(this.CurrentEditedStepGuid)).FirstOrDefault();
            string stepTitle = this.TestCaseEditViewModel.GetStepTitle(rtbAction.GetText());
            string expectedResult = this.TestCaseEditViewModel.GetExpectedResult(rtbExpectedResult.GetText());
            currentTestStep.ITestStep.Title = stepTitle;
            currentTestStep.ITestStep.ExpectedResult = expectedResult;
            this.CurrentEditedStepGuid = string.Empty;
        }

        /// <summary>
        /// Handles the Click event of the btnCancelEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            this.DisableSaveButton();
            this.TestCaseEditViewModel.IsActionTextSet = false;
            this.TestCaseEditViewModel.IsExpectedResultTextSet = false;
            rtbAction.ClearDefaultContent(ref this.TestCaseEditViewModel.IsActionTextSet);
            rtbExpectedResult.ClearDefaultContent(ref this.TestCaseEditViewModel.IsExpectedResultTextSet);
            this.CurrentEditedStepGuid = string.Empty;
        }

        /// <summary>
        /// Disables the save button.
        /// </summary>
        private void DisableSaveButton()
        {
            btnSaveTestStep.IsEnabled = false;
            btnCancelEdit.IsEnabled = false;
            btnEdit.IsEnabled = true;
            btnInsertStep.IsEnabled = true;
            btnShare.IsEnabled = true;
            btnDeleteStep.IsEnabled = true;            
        }

        /// <summary>
        /// Handles the Click event of the btnDuplicate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDuplicate_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(this.TestCaseEditViewModel.TestCase.ITestCase.Id, this.TestCaseEditViewModel.TestCase.ITestSuiteBase.Id, true, true);
        }

        /// <summary>
        /// Handles the Click event of the btnSaveAndCloseTestCase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSaveAndCloseTestCase_Click(object sender, RoutedEventArgs e)
        {
            this.SaveTestCaseInternal();
            this.NavigateToTestCasesInitialView();
        }

        /// <summary>
        /// Handles the Click event of the btnSaveTestCase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSaveTestCase_Click(object sender, RoutedEventArgs e)
        {
            this.SaveTestCaseInternal();
        }

        /// <summary>
        /// Saves the test case internal.
        /// </summary>
        /// <returns>the saved test case</returns>
        private TestCase SaveTestCaseInternal()
        {
            int priority = int.Parse(cbPriority.Text);
            string suiteTitle = tbSuite.Text;
            TestCase savedTestCase;
            if ((this.CreateNew || this.Duplicate) && !this.TestCaseEditViewModel.IsAlreadyCreated)
            {
                savedTestCase = this.TestCaseEditViewModel.TestCase.Save(true, priority, suiteTitle, this.TestCaseEditViewModel.ObservableTestSteps.ToList());
                this.TestCaseEditViewModel.TestCase = savedTestCase;
                this.TestCaseEditViewModel.IsAlreadyCreated = true;
                this.CreateNew = false;
                this.Duplicate = false;
                btnDuplicate.IsEnabled = true;
                this.TestCaseEditViewModel.CreateNew = false;
                this.TestCaseEditViewModel.Duplicate = false;
            }
            else
            {
                savedTestCase = this.TestCaseEditViewModel.TestCase.Save(false, priority, suiteTitle, this.TestCaseEditViewModel.ObservableTestSteps.ToList());
            }
            this.TestCaseId = savedTestCase.ITestCase.Id;

            return savedTestCase;
        }

        /// <summary>
        /// Handles the Click event of the btnUndo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnRedo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesInitialView();
        }

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.EditCurrentTestStepInternal();
        }

        /// <summary>
        /// Handles the Click event of the btnChange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            this.DisableSaveButton();
            rtbAction.ClearDefaultContent(ref this.TestCaseEditViewModel.IsActionTextSet);
            rtbExpectedResult.ClearDefaultContent(ref this.TestCaseEditViewModel.IsExpectedResultTextSet);
            TestStep currentTestStep = this.GetSelectedTestStep();
            rtbAction.SetText(currentTestStep.ITestStep.Title);
            rtbExpectedResult.SetText(currentTestStep.ITestStep.ExpectedResult);
        }   

        /// <summary>
        /// Handles the MouseEnter event of the cbSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbSuite_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if (ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            //{
            //    cbSuite.IsDropDownOpen = true;
            //    cbSuite.Focus();
            //}         
        }

        /// <summary>
        /// Handles the MouseEnter event of the cbArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbArea_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            {
                cbArea.IsDropDownOpen = true;
                cbArea.Focus();
            }      
        }

        /// <summary>
        /// Handles the MouseEnter event of the cbPriority control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbPriority_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            {
                cbPriority.IsDropDownOpen = true;
                cbPriority.Focus();
            }           
        }

        /// <summary>
        /// Handles the MouseMove event of the cbSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbSuite_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the MouseMove event of the cbArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbArea_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the MouseMove event of the cbPriority control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbPriority_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbSharedStepFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSharedStepFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSharedStepFilter.ClearDefaultContent(ref TestCaseEditViewModel.IsSharedStepSearchTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbSharedStepFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSharedStepFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSharedStepFilter.RestoreDefaultText(TestCaseEditViewModel.SharedStepSearchDefaultText, ref TestCaseEditViewModel.IsSharedStepSearchTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbSharedStepFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbSharedStepFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TestCaseEditViewModel.ReinitializeSharedStepCollection();
            string sharedStepTitleFilter = tbSharedStepFilter.Text;
            var filteredList = TestCaseEditViewModel.ObservableSharedSteps
               .Where(t => (!string.IsNullOrEmpty(sharedStepTitleFilter) ? t.ISharedStep.Title.ToLower().Contains(sharedStepTitleFilter.ToLower()) : true)).ToList();
            TestCaseEditViewModel.ObservableSharedSteps.Clear();
            filteredList.ForEach(x => TestCaseEditViewModel.ObservableSharedSteps.Add(x));
        }

        /// <summary>
        /// Handles the GotFocus event of the rtbStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void rtbStep_GotFocus(object sender, RoutedEventArgs e)
        {
            rtbAction.ClearDefaultContent(ref TestCaseEditViewModel.IsActionTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the rtbStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void rtbStep_LostFocus(object sender, RoutedEventArgs e)
        {
            rtbAction.RestoreDefaultText(TestCaseEditViewModel.ActionDefaultText, ref TestCaseEditViewModel.IsActionTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the rtbExpectedResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void rtbExpectedResult_GotFocus(object sender, RoutedEventArgs e)
        {
            rtbExpectedResult.ClearDefaultContent(ref this.TestCaseEditViewModel.IsExpectedResultTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the rtbExpectedResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void rtbExpectedResult_LostFocus(object sender, RoutedEventArgs e)
        {
            rtbExpectedResult.RestoreDefaultText(TestCaseEditViewModel.ExpectedResultDefaultText, ref this.TestCaseEditViewModel.IsExpectedResultTextSet); 
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgSharedSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            int currentSelectedIndex = dgTestSteps.SelectedIndex;
            this.TestCaseEditViewModel.InsertSharedStep(currentSharedStep, currentSelectedIndex + 1);            
        }

        /// <summary>
        /// Handles the Click event of the btnAssociateToAutomation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAssociateToAutomation_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(ExecutionContext.ProjectDllPath))
            {
                ModernDialog.ShowMessage("Provide Existing Project Path Dll.", "Warning", MessageBoxButton.OK);
                this.NavigateToAppearanceSettingsView();
            }
            else
            {
                TestCase currentTestCase = this.SaveTestCaseInternal();
                this.NavigateToAssociateAutomationView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, this.CreateNew, this.Duplicate);
            }
        }         
    }
}