using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    public partial class TestCasesInitialView : System.Windows.Controls.UserControl, IContent
    {
        private static bool isInitialized;

        public TestCasesInitialView()
        {
            InitializeComponent();
        }

        public TestCasesInitialViewModel TestCasesInitialViewModel { get; set; }
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DuplicateCommand = new RoutedCommand();
        public static RoutedCommand PreviewCommand = new RoutedCommand();
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand RefreshCommand = new RoutedCommand();

        private void TestCaseInitialView_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (isInitialized)
            {
                return;
            }
            ShowProgressBar();
            InitializeFastKeys();
            Task t = Task.Factory.StartNew(() =>
            {
                if (TestCasesInitialViewModel != null)
                {
                    TestCasesInitialViewModel = new ViewModels.TestCasesInitialViewModel(TestCasesInitialViewModel);
                    TestCasesInitialViewModel.FilterTestCases();
                }
                else
                {
                    TestCasesInitialViewModel = new ViewModels.TestCasesInitialViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = TestCasesInitialViewModel;
                HideProgressBar();
                this.tbTitleFilter.Focus();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
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

        private static void InitializeFastKeys()
        {
            EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            DuplicateCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            PreviewCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
            NewCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            RefreshCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
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

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            PreviewSelectedTestCase();
        }

        private void PreviewSelectedTestCase()
        {           
            ValidateTestCaseSelection(() => {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                this.NavigateToTestCasesDetailedView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
            } );     
        }

        private void ValidateTestCaseSelection(Action action)
        {
            if (dgTestCases.SelectedIndex != -1)
            {
                action.Invoke();               
            }
            else
            {
                DisplayNonSelectionWarning();
            }
        }

        private static void DisplayNonSelectionWarning()
        {
            ModernDialog.ShowMessage("No selected test case.", "Warning", MessageBoxButton.OK);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateTestCaseSelection(() =>
            {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
            });           
        }

        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateTestCaseSelection(() =>
            {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, true, true);
            });           
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(true, false);
        }     

        private void tbIdFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.ClearDefaultContent(ref TestCasesInitialViewModel.isIdFilterSet);
        }

        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.ClearDefaultContent(ref TestCasesInitialViewModel.isTitleFilterSet);
        }

        private void tbTextSuiteFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.ClearDefaultContent(ref TestCasesInitialViewModel.isSuiteFilterSet);
        }

        private void tbIdFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.RestoreDefaultText("ID", ref TestCasesInitialViewModel.isIdFilterSet);
        }

        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.RestoreDefaultText("Title", ref TestCasesInitialViewModel.isTitleFilterSet);
        }

        private void tbSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.RestoreDefaultText("Suite", ref TestCasesInitialViewModel.isSuiteFilterSet);
        }

        private void tbIdFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TestCasesInitialViewModel.FilterTestCases();     
        }  

        private void tbTitleFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TestCasesInitialViewModel.FilterTestCases();
        }

        private void tbSuiteFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            TestCasesInitialViewModel.FilterTestCases();
        }

        private void dgTestCases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreviewSelectedTestCase();
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
            {
                TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
                this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
            }
        }

        private void cbHideAutomated_Unchecked(object sender, RoutedEventArgs e)
        {
            TestCasesInitialViewModel.FilterTestCases();
        }

        private void cbHideAutomated_Checked(object sender, RoutedEventArgs e)
        {
            TestCasesInitialViewModel.FilterTestCases();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            TestCasesInitialViewModel.FilterTestCases();
        }
    }
}