// <copyright file="TestCaseEditView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerApp.BusinessLogic.Entities;
using TestCaseManagerApp.BusinessLogic.Enums;
using TestCaseManagerApp.Helpers;
using TestCaseManagerApp.ViewModels;
using UndoMethods;

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
        /// The copy test steps command
        /// </summary>
        public static RoutedCommand CopyTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The cut test steps command
        /// </summary>
        public static RoutedCommand CutTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The paste test steps command
        /// </summary>
        public static RoutedCommand PasteTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The undo command
        /// </summary>
        public static RoutedCommand UndoCommand = new RoutedCommand();

        /// <summary>
        /// The redo command
        /// </summary>
        public static RoutedCommand RedoCommand = new RoutedCommand();

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The test case unique identifier
        /// </summary>
        private int testCaseId;

        /// <summary>
        /// The test suite unique identifier
        /// </summary>
        private int testSuiteId;

        /// <summary>
        /// The create new test case
        /// </summary>
        private bool createNew;

        /// <summary>
        /// The duplicate the test case
        /// </summary>
        private bool duplicate;

        /// <summary>
        /// The is already saved
        /// </summary>
        private bool isAlreadyCreated;

        /// <summary>
        /// The is fake item inserted
        /// </summary>
        private bool isFakeItemInserted;

        /// <summary>
        /// The current edited step unique identifier
        /// </summary>
        private Guid currentEditedStepGuid;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEditView"/> class.
        /// </summary>
        public TestCaseEditView()
        {
            this.InitializeComponent();
            this.InitializeFastKeys();
        }  

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
                TestCaseEditViewModel = new TestCaseEditViewModel(this.testCaseId, this.testSuiteId, this.createNew, this.duplicate);
            });
            t.ContinueWith(antecedent =>
            {
                this.InitializeUiRelatedViewSettings();
                this.InitializePageTitle();
                UndoRedoManager.Instance().RedoStackStatusChanged += new UndoRedoManager.OnStackStatusChanged(RedoStackStatusChanged);
                UndoRedoManager.Instance().UndoStackStatusChanged += new UndoRedoManager.OnStackStatusChanged(UndoStackStatusChanged);
                this.HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());         
        }

        /// <summary>
        /// Undoes the stack status changed.
        /// </summary>
        /// <param name="hasItems">if set to <c>true</c> [has items].</param>
        private void UndoStackStatusChanged(bool hasItems)
        {
            btnUndo.IsEnabled = hasItems;
        }

        /// <summary>
        /// Redoes the stack status changed.
        /// </summary>
        /// <param name="hasItems">if set to <c>true</c> [has items].</param>
        private void RedoStackStatusChanged(bool hasItems)
        {
            btnRedo.IsEnabled = hasItems;
        }

        /// <summary>
        /// Initializes the page title.
        /// </summary>
        private void InitializePageTitle()
        {
            if (this.createNew && !this.duplicate)
            {
                tbPageTitle.Text = "Create New";
            }
            else if (this.createNew && this.duplicate)
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

            //if (this.duplicate || !this.createNew)
            //{
            //    this.SetTestCasePropertiesFromDuplicateTestCase();
            //}
            //else if (!this.duplicate && this.createNew)
            if (!this.duplicate && this.createNew)
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
        /// Sets the test case properties to default values.
        /// </summary>
        private void SetTestCasePropertiesToDefault()
        {
            cbArea.SelectedIndex = 0;
            cbPriority.SelectedIndex = 0;
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
            this.createNew = false;
            this.duplicate = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            string testCaseId = fm.Get("id");
            if (!string.IsNullOrEmpty(testCaseId))
            {
                this.testCaseId = int.Parse(testCaseId);
            }
            string suiteId = fm.Get("suiteId");
            if (!string.IsNullOrEmpty(suiteId))
            {
                this.testSuiteId = int.Parse(suiteId);
            }
            string createNew = fm.Get("createNew");
            if (!string.IsNullOrEmpty(createNew))
            {
                this.createNew = bool.Parse(createNew);
            }
            string duplicate = fm.Get("duplicate");
            if (!string.IsNullOrEmpty(duplicate))
            {
                this.duplicate = bool.Parse(duplicate);
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
            UndoCommand.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));
            RedoCommand.InputGestures.Add(new KeyGesture(Key.Y, ModifierKeys.Control));
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
            TestStep testStepToInsert = TestStepManager.CreateNewTestStep(TestCaseEditViewModel.TestCase, stepTitle, expectedResult, default(Guid));
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
                List<TestStep> selectedTestSteps = new List<TestStep>();
                this.GetTestStepsFromGrid(selectedTestSteps);
                bool isThereSharedStepSelected = TestStepManager.IsThereSharedStepSelected(selectedTestSteps);
                if (isThereSharedStepSelected)
                {
                    ModernDialog.ShowMessage("Shared steps cannon be shared again!", "Warning", MessageBoxButton.OK);
                    return;
                }

                ISharedStep sharedStepCore = this.TestCaseEditViewModel.TestCase.CreateNewSharedStep(newTitle, selectedTestSteps);
                sharedStepCore.Refresh();
                this.TestCaseEditViewModel.ObservableSharedSteps.Add(new SharedStep(sharedStepCore));
            }
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
                List<TestStepFull> testStepsToBeRemoved = TestCaseEditViewModel.MarkInitialStepsToBeRemoved(dgTestSteps.SelectedItems.Cast<TestStep>().ToList());
                this.TestCaseEditViewModel.RemoveTestSteps(testStepsToBeRemoved);
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
            if (dgTestSteps.SelectedItems.Count == 0)
            {
                return;
            }
            using (new UndoTransaction("Move up selected steps", true))
            {
                TestCaseEditViewModel.CreateNewTestStepCollectionAfterMoveUp(startIndex, count);
                this.SelectNextItemsAfterMoveUp(startIndex, count);
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
            using (new UndoTransaction("Move down selected steps", true))
            {
                TestCaseEditViewModel.CreateNewTestStepCollectionAfterMoveDown(startIndex, count);
                this.SelectNextItemsAfterMoveDown(startIndex, count);
            }       
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
            UndoRedoManager.Instance().Push((si, c) => this.SelectNextItemsAfterMoveDown(si, c), startIndex - 1, selectedTestStepsCount, "Select next items after move up");
        }

        /// <summary>
        /// Selects the next test steps after move down.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedTestStepsCount">The selected test steps count.</param>
        private void SelectNextItemsAfterMoveDown(int startIndex, int selectedTestStepsCount)
        {
            dgTestSteps.SelectedItems.Clear();
            for (int i = startIndex + 1; i < startIndex + selectedTestStepsCount + 1; i++)
            {
                dgTestSteps.SelectedItems.Add(dgTestSteps.Items[i]);
            }
            UndoRedoManager.Instance().Push((si, c) => this.SelectNextItemsAfterMoveUp(si, c), startIndex + 1, selectedTestStepsCount, "Select next items after move up");
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.InsertSharedStepInternal();
        }

        /// <summary>
        /// Inserts the shared step internal.
        /// </summary>
        private void InsertSharedStepInternal()
        {
            SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            if (currentSharedStep == null)
            {
                ModernDialog.ShowMessage("Please select a shared step to add first!", "Warning", MessageBoxButton.OK);
                return;
            }
            using (new UndoTransaction("Insert Shared step's inner test steps to the test case test steps Observable collection"))
            {
                int currentSelectedIndex = dgTestSteps.SelectedIndex;
                this.TestCaseEditViewModel.InsertSharedStep(currentSharedStep, currentSelectedIndex);
                int index = dgTestSteps.SelectedIndex + currentSharedStep.ISharedStep.Actions.Count;
                UndoRedoManager.Instance().Push((i) => this.ChangeSelectedIndexTestStepsDataGrid(i), dgTestSteps.SelectedIndex);
                this.ChangeSelectedIndexTestStepsDataGrid(index);
            }
        }

        /// <summary>
        /// Changes the selected index test steps data grid.
        /// </summary>
        /// <param name="newIndex">The new index.</param>
        private void ChangeSelectedIndexTestStepsDataGrid(int newIndex)
        {
            UndoRedoManager.Instance().Push((i) => this.ChangeSelectedIndexTestStepsDataGrid(i), dgTestSteps.SelectedIndex);
            dgTestSteps.SelectedIndex = newIndex;
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
            List<TestStep> selectedTestSteps = this.AddMissedSelectedSharedSteps();
            this.UpdateSelectedTestSteps(selectedTestSteps);
        }

        /// <summary>
        /// Updates the selected test steps. Add updated selected test steps.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        private void UpdateSelectedTestSteps(List<TestStep> selectedTestSteps)
        {
            dgTestSteps.SelectedItems.Clear();
            foreach (TestStep currentTestStep in selectedTestSteps)
            {
                dgTestSteps.SelectedItems.Add(currentTestStep);
            }
        }

        /// <summary>
        /// Adds the missed selected shared steps. Select non-selected shared steps because part of the shared step was already selected.
        /// </summary>
        /// <returns>updated selected test steps list</returns>
        private List<TestStep> AddMissedSelectedSharedSteps()
        {
            List<TestStep> selectedTestSteps = this.GetAllSelectedTestSteps();
            foreach (TestStep currentSelectedTestStep in this.TestCaseEditViewModel.ObservableTestSteps)
            {
                for (int i = 0; i < selectedTestSteps.Count; i++)
                {
                    if (currentSelectedTestStep.TestStepGuid.Equals(selectedTestSteps[i].TestStepGuid) &&
                        !currentSelectedTestStep.TestStepId.Equals(selectedTestSteps[i].TestStepId))
                    {
                        selectedTestSteps.Add(currentSelectedTestStep);
                        break;
                    }
                }
            }
            return selectedTestSteps;
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
            this.currentEditedStepGuid = currentTestStep.TestStepGuid;
            rtbAction.SetText(currentTestStep.ActionTitle);
            rtbExpectedResult.SetText(currentTestStep.ActionExpectedResult);
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
            TestStep currentTestStep = this.TestCaseEditViewModel.ObservableTestSteps.Where(x => x.TestStepGuid.Equals(this.currentEditedStepGuid)).FirstOrDefault();
            string stepTitle = this.TestCaseEditViewModel.GetStepTitle(rtbAction.GetText());
            string expectedResult = this.TestCaseEditViewModel.GetExpectedResult(rtbExpectedResult.GetText());
            currentTestStep.ActionTitle = stepTitle;
            currentTestStep.ActionExpectedResult = expectedResult;
            this.currentEditedStepGuid = default(Guid);
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
            this.currentEditedStepGuid = default(Guid);
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
            this.InitializePageTitle();
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
            if ((this.createNew || this.duplicate) && !this.isAlreadyCreated)
            {
                savedTestCase = this.TestCaseEditViewModel.TestCase.Save(true, suiteTitle, this.TestCaseEditViewModel.ObservableTestSteps.ToList());
                this.TestCaseEditViewModel.TestCase = savedTestCase;
                this.isAlreadyCreated = true;
                this.createNew = false;
                this.duplicate = false;
                btnDuplicate.IsEnabled = true;
            }
            else
            {
                savedTestCase = this.TestCaseEditViewModel.TestCase.Save(false, suiteTitle, this.TestCaseEditViewModel.ObservableTestSteps.ToList());
            }
            this.testCaseId = savedTestCase.ITestCase.Id;
            this.TestCaseEditViewModel.TestCaseIdLabel = savedTestCase.ITestCase.Id.ToString();

            return savedTestCase;
        }

        /// <summary>
        /// Handles the Click event of the btnUndo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (UndoRedoManager.Instance().HasUndoOperations)
            {
                UndoRedoManager.Instance().Undo();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRedo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            if (UndoRedoManager.Instance().HasRedoOperations)
            {
                UndoRedoManager.Instance().Redo();
            }
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
        /// Handles the Command event of the copyTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void copyTestSteps_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestStep> selectedTestSteps = this.GetAllSelectedTestSteps();
            TestStepManager.CopyToClipboardTestSteps(true, selectedTestSteps);
        }

        /// <summary>
        /// Gets all selected test steps.
        /// </summary>
        /// <returns></returns>
        private List<TestStep> GetAllSelectedTestSteps()
        {
            List<TestStep> selectedTestStepsSorted = new List<TestStep>();
            List<TestStep> selectedTestSteps = dgTestSteps.SelectedItems.Cast<TestStep>().ToList();

            foreach (TestStep currentTestStep in this.TestCaseEditViewModel.ObservableTestSteps)
            {
                for (int i = 0; i < selectedTestSteps.Count; i++)
                {
                    if (currentTestStep.Equals(selectedTestSteps[i]))
                    {
                        selectedTestStepsSorted.Add(selectedTestSteps[i]);
                        selectedTestSteps.RemoveAt(i);
                        break;
                    } 
                }
            }

            return selectedTestStepsSorted;
        }

        /// <summary>
        /// Handles the Command event of the cutTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void cutTestSteps_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestStep> selectedTestSteps = this.GetAllSelectedTestSteps();
            TestStepManager.CopyToClipboardTestSteps(false, selectedTestSteps);
            ClipBoardTestStep clipBoardTestStep = TestStepManager.GetFromClipboardTestSteps();
            if (clipBoardTestStep != null && clipBoardTestStep.TestSteps != null)
            {
                using (new UndoTransaction("Delete cut steps"))
                {
                    this.TestCaseEditViewModel.DeleteCutTestSteps(clipBoardTestStep.TestSteps);
                }
            }          
        }

        /// <summary>
        /// Handles the Command event of the pasteTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void pasteTestSteps_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            ClipBoardTestStep clipBoardTestStep = TestStepManager.GetFromClipboardTestSteps();
            int selectedIndex = dgTestSteps.SelectedIndex;
            Guid previousOldGuid = default(Guid);
            Guid previousNewGuid = default(Guid);
            using (new UndoTransaction("Copies previously selected test steps"))
            {
                foreach (TestStep copiedTestStep in clipBoardTestStep.TestSteps)
                {
                    TestStep testStepToBeInserted = (TestStep)copiedTestStep.Clone();
                    if (copiedTestStep.TestStepGuid.Equals(previousOldGuid))
                    {
                        testStepToBeInserted.TestStepGuid = previousNewGuid;
                    }
                    TestCaseEditViewModel.InsertTestStepInTestCase(testStepToBeInserted, selectedIndex++);
                    previousNewGuid = testStepToBeInserted.TestStepGuid;
                    previousOldGuid = copiedTestStep.TestStepGuid;
                }
            }
            if (clipBoardTestStep.ClipBoardCommand == ClipBoardCommand.Cut)
            {
                //this.TestCaseEditViewModel.DeleteCutTestSteps(clipBoardTestStep.TestSteps);
                System.Windows.Forms.Clipboard.Clear();
            }
          
            dgTestSteps.Focus();

            // If fake item was inserted in order the paste to be enabled, we delete it from the test steps and select the next item in the grid
            if (isFakeItemInserted)
            {
                this.TestCaseEditViewModel.ObservableTestSteps.RemoveAt(0);
                isFakeItemInserted = false;
            }            
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
            rtbAction.SetText(currentTestStep.ActionTitle);
            rtbExpectedResult.SetText(currentTestStep.ActionExpectedResult);
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
            this.InsertSharedStepInternal();
        }

        /// <summary>
        /// Handles the Click event of the btnAssociateToAutomation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAssociateToAutomation_Click(object sender, RoutedEventArgs e)
        {
            string projectDllPath = RegistryManager.GetProjectDllPath();
            if (!File.Exists(projectDllPath))
            {
                ModernDialog.ShowMessage("Provide Existing Project Path Dll.", "Warning", MessageBoxButton.OK);
                this.NavigateToAppearanceSettingsView();
            }
            else
            {
                TestCase currentTestCase = this.SaveTestCaseInternal();
                this.NavigateToAssociateAutomationView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, this.createNew, this.duplicate);
            }
        }

        /// <summary>
        /// Handles the PreviewMouseRightButtonDown event of the dgTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClipBoardTestStep clipBoardItem = TestStepManager.GetFromClipboardTestSteps();
            bool isPasteEnabled = clipBoardItem == null ? false : true;
            dgTestStepsPasteMenuItem.IsEnabled = isPasteEnabled;

            // If there isnt't items in the grid, we add a fake one in order the paste operation to be enabled
            this.AddFakeInitialTestStepForPaste(isPasteEnabled);
            this.dgTestSteps.Focus();
        }

        /// <summary>
        /// Adds the fake initial test step for paste.
        /// </summary>
        /// <param name="isPasteEnabled">if set to <c>true</c> [is paste enabled].</param>
        private void AddFakeInitialTestStepForPaste(bool isPasteEnabled)
        {
            // If there isnt't items in the grid, we add a fake one in order the paste operation to be enabled

            if (this.TestCaseEditViewModel.ObservableTestSteps.Count == 0 && isPasteEnabled)
            {
                this.TestCaseEditViewModel.ObservableTestSteps.Add(new TestStep(false, String.Empty, default(Guid)));
                this.dgTestSteps.SelectedIndex = 0;
                this.isFakeItemInserted = true;
            }
        }

        /// <summary>
        /// Handles the event of the dgTestSteps_SelectedCellsChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            this.UpdateButtonMenuAvailabilityInternal();        
        }

        /// <summary>
        /// Updates the button menu availability internal.
        /// </summary>
        private void UpdateButtonMenuAvailabilityInternal()
        {
            btnEdit.IsEnabled = true;
            btnMoveUp.IsEnabled = true;
            btnMoveDown.IsEnabled = true;
            btnDeleteStep.IsEnabled = true;
            btnChange.IsEnabled = true;
            btnShare.IsEnabled = true;

            dgTestStepsCopyMenuItem.IsEnabled = true;
            dgTestStepsCutMenuItem.IsEnabled = true;
            dgTestStepsShareMenuItem.IsEnabled = true;
            dgTestStepsDeleteMenuItem.IsEnabled = true;

            if (dgTestSteps.SelectedItems.Count == 0)
            {
                btnEdit.IsEnabled = false;
                btnMoveUp.IsEnabled = false;
                btnMoveDown.IsEnabled = false;
                btnDeleteStep.IsEnabled = false;
                btnChange.IsEnabled = false;
                btnShare.IsEnabled = false;

                dgTestStepsCopyMenuItem.IsEnabled = false;
                dgTestStepsCutMenuItem.IsEnabled = false;
                dgTestStepsShareMenuItem.IsEnabled = false;
                dgTestStepsDeleteMenuItem.IsEnabled = false;
            }

            btnAdd.IsEnabled = true;
            if (dgSharedSteps.SelectedItems.Count == 0)
            {
                btnAdd.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handles the SelectedCellsChanged event of the dgSharedSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            btnAdd.IsEnabled = true;
            if (dgSharedSteps.SelectedItems.Count == 0)
            {
                btnAdd.IsEnabled = false;
            }
        }     
    }
}