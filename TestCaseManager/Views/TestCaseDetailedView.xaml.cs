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
    public partial class TestCaseDetailedView : UserControl, IContent
    {
        private static bool isInitialized;

        public TestCaseDetailedView()
        {
            InitializeComponent();
        }

        public int TestCaseId { get; set; }
        public int TestSuiteId { get; set; }
        public TestCaseDetailedViewModel TestCaseDetailedViewModel { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(isInitialized)
            {
                return;
            }
            ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                TestCaseDetailedViewModel = new TestCaseDetailedViewModel(TestCaseId, TestSuiteId);
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = TestCaseDetailedViewModel;
                HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());   
        }


        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            isInitialized = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            TestCaseId = int.Parse(fm.Fragments["id"]);
            TestSuiteId = int.Parse(fm.Fragments["suiteId"]);
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
        }

        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id, true, true);
        }   
    }
}
