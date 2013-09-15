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
    public partial class TestCaseEditView : System.Windows.Controls.UserControl, IContent
    {
        private static bool isInitialized;

        public TestCaseEditView()
        {
            InitializeComponent();
            InitializeFastKeys();
        }

        public int TestCaseId { get; set; }
        public int TestSuiteId { get; set; }
        public bool CreateNew { get; set; }
        public bool Duplicate { get; set; }
        public bool ComesFromAssociatedAutomation { get; set; }
        public string CurrentEditedStepGuid { get; set; }
        public TestCaseEditViewModel TestCaseEditViewModel { get; set; }
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand ShareStepCommand = new RoutedCommand();
        public static RoutedCommand AssociateCommand = new RoutedCommand();
        public static RoutedCommand AddSharedStepCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand MoveUpCommand = new RoutedCommand();
        public static RoutedCommand MoveDownCommand = new RoutedCommand();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized)
            {
                return;
            }
         
            ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                TestCaseEditViewModel = new TestCaseEditViewModel(TestCaseId, TestSuiteId, CreateNew, Duplicate);
            });
            t.ContinueWith(antecedent =>
            {
                InitializeUiRelatedViewSettings();
                HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        } 

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            InitializeUrlParameters(e);
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            isInitialized = false;            
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }  

        private void InitializeUiRelatedViewSettings()
        {
            this.DataContext = TestCaseEditViewModel;

            rtbAction.SetText(TestCaseEditViewModel.ActionDefaultText);
            rtbExpectedResult.SetText(TestCaseEditViewModel.ExpectedResultDefaultText);
            tbSharedStepFilter.Text = TestCaseEditViewModel.SharedStepSearchDefaultText;

            if (Duplicate || !CreateNew)
            {
                SetTestCasePropertiesFromPrevious();
            }
            else if (!Duplicate && CreateNew)
            {
                SetTestCasePropertiesToDefault();
                btnDuplicate.IsEnabled = false;
            }
            else
            {
                btnDuplicate.IsEnabled = false;
            }
        }

        private void SetTestCasePropertiesFromPrevious()
        {
            cbArea.SelectedIndex = TestCaseEditViewModel.Areas.FindIndex(0, (x => x.Equals(TestCaseEditViewModel.TestCase.ITestCase.Area)));
            cbPriority.SelectedIndex = TestCaseEditViewModel.Priorities.FindIndex(0, (x => x.Equals(TestCaseEditViewModel.TestCase.ITestCase.Priority)));
            cbSuite.SelectedIndex = TestCaseEditViewModel.TestSuiteList.FindIndex(0, (x => x.Title.Equals(TestCaseEditViewModel.TestCase.ITestSuiteBase.Title)));
        }

        private void SetTestCasePropertiesToDefault()
        {
            cbArea.SelectedIndex = 0;
            cbPriority.SelectedIndex = 0;
            cbSuite.SelectedIndex = 0;
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

        private void InitializeUrlParameters(FragmentNavigationEventArgs e)
        {
            CreateNew = false;
            Duplicate = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            string testCaseId = fm.Get("id");
            if (!string.IsNullOrEmpty(testCaseId))
            {
                TestCaseId = int.Parse(testCaseId);
            }
            string suiteId = fm.Get("suiteId");
            if (!string.IsNullOrEmpty(suiteId))
            {
                TestSuiteId = int.Parse(suiteId);
            }
            string createNew = fm.Get("createNew");
            if (!string.IsNullOrEmpty(createNew))
            {
                CreateNew = bool.Parse(createNew);
            }
            string duplicate = fm.Get("duplicate");
            if (!string.IsNullOrEmpty(duplicate))
            {
                Duplicate = bool.Parse(duplicate);
            }
            string comesFromAssociatedAutomation = fm.Get("comesFromAssociatedAutomation");
            if (!string.IsNullOrEmpty(comesFromAssociatedAutomation))
            {
                ComesFromAssociatedAutomation = bool.Parse(comesFromAssociatedAutomation);
            }
        }

        private static void InitializeFastKeys()
        {
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            AssociateCommand.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            DeleteCommand.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.Alt));
            MoveUpCommand.InputGestures.Add(new KeyGesture(Key.Up, ModifierKeys.Alt));
            MoveDownCommand.InputGestures.Add(new KeyGesture(Key.Down, ModifierKeys.Alt));
            AddSharedStepCommand.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
            ShareStepCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
        }     

        private void btnInsertStep_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dgTestSteps.SelectedIndex;
            string stepTitle = TestCaseEditViewModel.GetStepTitle(rtbAction.GetText());
            string expectedResult = TestCaseEditViewModel.GetExpectedResult(rtbExpectedResult.GetText());
            TestStep testStepToInsert = TestStepManager.CreateNewTestStep(TestCaseEditViewModel.TestCase, stepTitle, expectedResult);
            TestCaseEditViewModel.InsertTestStepInTestCase(testStepToInsert, selectedIndex);
        }

        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PrompDialogWindow();
            dialog.ShowDialog();

            Task t = Task.Factory.StartNew(() => { while (string.IsNullOrEmpty(ExecutionContext.SharedStepTitle) && !ExecutionContext.SharedStepTitleDialogCancelled) { } });
            t.Wait();

            if (!ExecutionContext.SharedStepTitleDialogCancelled)
            {
                List<TestStep> selectedTestSteps = new List<TestStep>();
                GetTestStepsFromGrid(selectedTestSteps);
                bool isThereSharedStepSelected = TestStepManager.IsThereSharedStepSelected(selectedTestSteps);
                if (isThereSharedStepSelected)
                {
                    ModernDialog.ShowMessage("Shared steps cannon be shared again!", "Warning", MessageBoxButton.OK);
                    return;
                }

                ISharedStep iSharedStep = TestCaseEditViewModel.TestCase.CreateNewSharedStep(ExecutionContext.SharedStepTitle, selectedTestSteps);
                TestCaseEditViewModel.ObservableSharedSteps.Add(new SharedStep(iSharedStep));
                TestCaseEditViewModel.UpdateObservableTestSteps(selectedTestSteps);
            }
            //dgTestSteps.DataContext = TestCaseEditViewModel.ObservableTestSteps;
            ExecutionContext.SharedStepTitleDialogCancelled = false;
        }

        private void GetTestStepsFromGrid(List<TestStep> selectedTestSteps)
        {
            foreach (var item in dgTestSteps.SelectedItems)
            {
                selectedTestSteps.Add(item as TestStep);
            }
        }

        private void btnDeleteStep_Click(object sender, RoutedEventArgs e)
        {
            DeleteStepInternal();   
        }

        private void deleteStep_Command(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteStepInternal();
            dgTestSteps.Focus();
        }

        private void DeleteStepInternal()
        {
            if (dgTestSteps.SelectedItems != null)
            {
                List<TestStep> testStepsToBeRemoved = TestCaseEditViewModel.MarkInitialStepsToBeRemoved(dgTestSteps.SelectedItems.Cast<TestStep>().ToList());
                TestCaseEditViewModel.RemoveTestSteps(testStepsToBeRemoved);
            }
        }
      
        private void btnDeleteSharedStep_Click(object sender, RoutedEventArgs e)
        {
            if (dgTestSteps.SelectedItems != null)
            {
                List<TestStep> testStepsToBeRemoved = TestCaseEditViewModel.MarkStepsToBeRemoved(dgTestSteps.SelectedItems.Cast<TestStep>().ToList());
                TestCaseEditViewModel.DeleteAllMarkedStepsForRemoval(testStepsToBeRemoved);              
            }
        }    

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            MoveUpInternal();            
        }

        private void moveUp_Command(object sender, ExecutedRoutedEventArgs e)
        {
            MoveUpInternal();
            dgTestSteps.Focus();
        }

        private void moveDown_Command(object sender, ExecutedRoutedEventArgs e)
        {
            MoveDownInternal();
            dgTestSteps.Focus();
        }

        private void MoveUpInternal()
        {
            // validate the move if it's out of the boudaries return
            if (dgTestSteps.SelectedItems.Count == 0)
                return;
            int startIndex = TestCaseEditViewModel.ObservableTestSteps.IndexOf(dgTestSteps.SelectedItems[0] as TestStep);
            if (startIndex == 0)
                return;

            int count = dgTestSteps.SelectedItems.Count;
            TestCaseEditViewModel.CreateNewTestStepCollectionAfterMoveUp(startIndex, count);

            SelectNextItemsAfterMoveUp(startIndex, count);
            if (dgTestSteps.SelectedItems.Count == 0)
                return;
        }       

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            MoveDownInternal();
        }

        private void MoveDownInternal()
        {
            // validate the move if it's out of the boudaries return
            if (dgTestSteps.SelectedItems.Count == 0)
                return;
            int startIndex = TestCaseEditViewModel.ObservableTestSteps.IndexOf(dgTestSteps.SelectedItems[0] as TestStep);
            int count = dgTestSteps.SelectedItems.Count;
            if (startIndex == TestCaseEditViewModel.ObservableTestSteps.Count - 1)
                return;
            if ((startIndex + count) >= TestCaseEditViewModel.ObservableTestSteps.Count)
                return;

            TestCaseEditViewModel.CreateNewTestStepCollectionAfterMoveDown(startIndex, count);

            SelectNextItemsAfterMoveDown(startIndex, count);
        }      

        private void SelectNextItemsAfterMoveUp(int startIndex, int count)
        {
            dgTestSteps.SelectedItems.Clear();
            for (int i = startIndex - 1; i < startIndex + count - 1; i++)
            {
                dgTestSteps.SelectedItems.Add(dgTestSteps.Items[i]);
            }
        }

        private void SelectNextItemsAfterMoveDown(int startIndex, int count)
        {
            dgTestSteps.SelectedItems.Clear();
            for (int i = startIndex + 1; i < startIndex + count + 1; i++)
            {
                dgTestSteps.SelectedItems.Add(dgTestSteps.Items[i]);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            if(currentSharedStep == null)
            {
                ModernDialog.ShowMessage("Please select a shared step to add first!", "Warning", MessageBoxButton.OK);
                return;
            }
            int currentSelectedIndex = dgTestSteps.SelectedIndex;
            TestCaseEditViewModel.InsertSharedStep(currentSharedStep, currentSelectedIndex + 1);            
        }     

        private void dgTestSteps_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
            {
                EditCurrentStep();
            }
        }

        private void EditCurrentStep()
        {
            EnableSaveStepButton();
            rtbAction.ClearDefaultContent(ref TestCaseEditViewModel.IsActionTextSet);
            rtbExpectedResult.ClearDefaultContent(ref TestCaseEditViewModel.IsExpectedResultTextSet);
            TestStep currentTestStep = GetSelectedTestStep();
            CurrentEditedStepGuid = currentTestStep.StepGuid;
            rtbAction.SetText(currentTestStep.ITestStep.Title);
            rtbExpectedResult.SetText(currentTestStep.ITestStep.ExpectedResult);
        }

        private TestStep GetSelectedTestStep()
        {
            TestStep currentTestStep = dgTestSteps.SelectedItem as TestStep;
            return currentTestStep;
        }

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

        private void btnSaveTestStep_Click(object sender, RoutedEventArgs e)
        {
            DisableSaveButton();
            TestStep currentTestStep = TestCaseEditViewModel.ObservableTestSteps.Where(x => x.StepGuid.Equals(CurrentEditedStepGuid)).FirstOrDefault();
            string stepTitle = TestCaseEditViewModel.GetStepTitle(rtbAction.GetText());
            string expectedResult = TestCaseEditViewModel.GetExpectedResult(rtbExpectedResult.GetText());
            currentTestStep.ITestStep.Title = stepTitle;
            currentTestStep.ITestStep.ExpectedResult = expectedResult;
            CurrentEditedStepGuid = string.Empty;
        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            DisableSaveButton();
            TestCaseEditViewModel.IsActionTextSet = false;
            TestCaseEditViewModel.IsExpectedResultTextSet = false;
            rtbAction.ClearDefaultContent(ref TestCaseEditViewModel.IsActionTextSet);
            rtbExpectedResult.ClearDefaultContent(ref TestCaseEditViewModel.IsExpectedResultTextSet);
            CurrentEditedStepGuid = string.Empty;
        }  

        private void DisableSaveButton()
        {
            btnSaveTestStep.IsEnabled = false;
            btnCancelEdit.IsEnabled = false;
            btnEdit.IsEnabled = true;
            btnInsertStep.IsEnabled = true;
            btnShare.IsEnabled = true;
            btnDeleteStep.IsEnabled = true;            
        }

        private void btnDuplicate_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(TestCaseEditViewModel.TestCase.ITestCase.Id, TestCaseEditViewModel.TestCase.ITestSuiteBase.Id, true, true);
        }

        private void btnSaveAndCloseTestCase_Click(object sender, RoutedEventArgs e)
        {
            SaveTestCaseInternal();
            this.NavigateToTestCasesInitialView();
        }

        private void btnSaveTestCase_Click(object sender, RoutedEventArgs e)
        {
            SaveTestCaseInternal();
        }  

        private TestCase SaveTestCaseInternal()
        {
            int priority = int.Parse(cbPriority.Text);
            string suiteTitle = cbSuite.Text;
            TestCase savedTestCase;
            if ((CreateNew || Duplicate) && !TestCaseEditViewModel.IsAlreadyCreated)
            {
                savedTestCase = TestCaseEditViewModel.TestCase.Save(true, priority, suiteTitle, TestCaseEditViewModel.ObservableTestSteps.ToList());
                TestCaseEditViewModel.TestCase = savedTestCase;
                TestCaseEditViewModel.IsAlreadyCreated = true;
                CreateNew = false;
                Duplicate = false;
                btnDuplicate.IsEnabled = true;
                TestCaseEditViewModel.CreateNew = false;
                TestCaseEditViewModel.Duplicate = false;
            }
            else
            {
                savedTestCase = TestCaseEditViewModel.TestCase.Save(false, priority, suiteTitle, TestCaseEditViewModel.ObservableTestSteps.ToList());
            }
            TestCaseId = savedTestCase.ITestCase.Id;

            return savedTestCase;
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesInitialView();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditCurrentStep();
        }

        private void cbSuite_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cbSuite.IsDropDownOpen = true;
            cbSuite.Focus();
        }

        private void cbArea_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cbArea.IsDropDownOpen = true;
            cbArea.Focus();
        }

        private void cbPriority_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cbPriority.IsDropDownOpen = true;
            cbPriority.Focus();
        }

        private void cbSuite_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        private void cbArea_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        private void cbPriority_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        private void tbSharedStepFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSharedStepFilter.ClearDefaultContent(ref TestCaseEditViewModel.IsSharedStepSearchTextSet);
        }

        private void tbSharedStepFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSharedStepFilter.RestoreDefaultText(TestCaseEditViewModel.SharedStepSearchDefaultText, ref TestCaseEditViewModel.IsSharedStepSearchTextSet);
        }

        private void tbSharedStepFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TestCaseEditViewModel.ReinitializeSharedStepCollection();
            string sharedStepTitleFilter = tbSharedStepFilter.Text;
            var filteredList = TestCaseEditViewModel.ObservableSharedSteps
               .Where(t => (!string.IsNullOrEmpty(sharedStepTitleFilter) ? t.ISharedStep.Title.ToLower().Contains(sharedStepTitleFilter.ToLower()) : true)).ToList();
            TestCaseEditViewModel.ObservableSharedSteps.Clear();
            filteredList.ForEach(x => TestCaseEditViewModel.ObservableSharedSteps.Add(x));
        }

        private void rtbStep_GotFocus(object sender, RoutedEventArgs e)
        {
            rtbAction.ClearDefaultContent(ref TestCaseEditViewModel.IsActionTextSet);
        }

        private void rtbStep_LostFocus(object sender, RoutedEventArgs e)
        {
            rtbAction.RestoreDefaultText(TestCaseEditViewModel.ActionDefaultText, ref TestCaseEditViewModel.IsActionTextSet);
        }

        private void rtbExpectedResult_GotFocus(object sender, RoutedEventArgs e)
        {
            rtbExpectedResult.ClearDefaultContent(ref TestCaseEditViewModel.IsExpectedResultTextSet);
        }

        private void rtbExpectedResult_LostFocus(object sender, RoutedEventArgs e)
        {
            rtbExpectedResult.RestoreDefaultText(TestCaseEditViewModel.ExpectedResultDefaultText, ref TestCaseEditViewModel.IsExpectedResultTextSet); 
        }

        private void dgSharedSteps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
               SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            int currentSelectedIndex = dgTestSteps.SelectedIndex;
            TestCaseEditViewModel.InsertSharedStep(currentSharedStep, currentSelectedIndex + 1);            
        }

        private void btnAssociateToAutomation_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(ExecutionContext.ProjectDllPath))
            {
                ModernDialog.ShowMessage("Provide Existing Project Path Dll.", "Warning", MessageBoxButton.OK);
                this.NavigateToAppearanceSettingsView();
            }
            else
            {
                TestCase currentTestCase = SaveTestCaseInternal();
                this.NavigateToAssociateAutomationView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, CreateNew, Duplicate);
            }
        }     
    }
}