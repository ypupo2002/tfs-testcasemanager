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
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    public partial class TestCasesInitialView : System.Windows.Controls.UserControl, IContent
    {
        public TestCasesInitialViewModel TestCasesInitialViewModel { get; set; }
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DuplicateCommand = new RoutedCommand();
        public static RoutedCommand PreviewCommand = new RoutedCommand();
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand RefreshCommand = new RoutedCommand();

        public TestCasesInitialView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            DuplicateCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
            PreviewCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
            NewCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            RefreshCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));

            progressBar.Visibility = System.Windows.Visibility.Visible;
            mainGrid.Visibility = System.Windows.Visibility.Hidden;

            Task t = Task.Factory.StartNew(() =>
            {
                MaximizeWindow();
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());     
          
            Task t2 = t.ContinueWith(antecedent =>
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
            t2.ContinueWith(antecedent =>
            {                
                this.DataContext = TestCasesInitialViewModel;
                progressBar.Visibility = System.Windows.Visibility.Hidden;
                mainGrid.Visibility = System.Windows.Visibility.Visible;
                this.tbTitleFilter.Focus();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void MaximizeWindow()
        {
            try
            {
                Window w = Window.GetWindow(this);
                w.WindowState = WindowState.Maximized;
            }
            catch { }
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

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }

        private void tbIdFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.ClearDefaultContent(ref TestCasesInitialViewModel.IdFlag);
        }

        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.ClearDefaultContent(ref TestCasesInitialViewModel.TitleFlag);
        }

        private void tbTextSuiteFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.ClearDefaultContent(ref TestCasesInitialViewModel.SuiteFlag);
        }

        private void tbIdFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.RestoreDefaultText("ID", ref TestCasesInitialViewModel.IdFlag);
        }

        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.RestoreDefaultText("Title", ref TestCasesInitialViewModel.TitleFlag);
        }

        private void tbSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.RestoreDefaultText("Suite", ref TestCasesInitialViewModel.SuiteFlag);
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