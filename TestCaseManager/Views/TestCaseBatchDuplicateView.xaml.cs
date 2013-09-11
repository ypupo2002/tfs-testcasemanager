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
    public partial class TestCaseBatchDuplicateView : UserControl, IContent
    {
        public TestCasesBatchDuplicateViewModel TestCasesBatchDuplicateViewModel { get; set; }

        public TestCaseBatchDuplicateView()
        {
            InitializeComponent();
            InitializeSearchBoxes();  
        }

        private void InitializeSearchBoxes()
        {
            tbTitleFilter.Text = "Title";
            tbSuiteFilter.Text = "Suite";
        }

        private void UserControl_Initialized_1(object sender, EventArgs e)
        {
            if (TestCasesBatchDuplicateViewModel == null)
            {
                TestCasesBatchDuplicateViewModel = new ViewModels.TestCasesBatchDuplicateViewModel();
                this.DataContext = TestCasesBatchDuplicateViewModel;
            }
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                if (TestCasesBatchDuplicateViewModel != null)
                {
                    TestCasesBatchDuplicateViewModel = new ViewModels.TestCasesBatchDuplicateViewModel(TestCasesBatchDuplicateViewModel);
                    TestCasesBatchDuplicateViewModel.FilterTestCases();
                }
                else
                {
                    //TestCasesBatchDuplicateViewModel = new ViewModels.TestCasesBatchDuplicateViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                //this.DataContext = TestCasesBatchDuplicateViewModel;
                HideProgressBar();
                //cbSuite.SelectedIndex = 0;
                this.tbTitleFilter.Focus();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
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

        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.ClearDefaultContent(ref TestCasesBatchDuplicateViewModel.TitleFlag);
        }

        private void tbTextSuiteFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.ClearDefaultContent(ref TestCasesBatchDuplicateViewModel.SuiteFlag);
        }

        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.RestoreDefaultText("Title", ref TestCasesBatchDuplicateViewModel.TitleFlag);
        }

        private void tbSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSuiteFilter.RestoreDefaultText("Suite", ref TestCasesBatchDuplicateViewModel.SuiteFlag);
        }

        private void tbIdFilter_KeyUp(object sender, KeyEventArgs e)
        {
            TestCasesBatchDuplicateViewModel.FilterTestCases();
        }

        private void tbTitleFilter_KeyUp(object sender, KeyEventArgs e)
        {
            TestCasesBatchDuplicateViewModel.FilterTestCases();
        }

        private void tbSuiteFilter_KeyUp(object sender, KeyEventArgs e)
        {
            TestCasesBatchDuplicateViewModel.FilterTestCases();
        }       

        private void cbSuite_MouseEnter(object sender, MouseEventArgs e)
        {
            cbSuite.IsDropDownOpen = true;
            cbSuite.Focus();
        }

        private void cbSuite_MouseMove(object sender, MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        private void btnBatchDuplicate_Click(object sender, RoutedEventArgs e)
        {
            InitializeCurrentSelectedTestCases();
            string newSuiteTitle = cbSuite.Text;
            List<TextReplacePair> textReplacePairsList = TestCasesBatchDuplicateViewModel.ObservableTextReplacePairs.ToList();
            List<SharedStepIdReplacePair> sharedStepIdReplacePairList = TestCasesBatchDuplicateViewModel.ObservableSharedStepIdReplacePairs.ToList();
            int duplicatedCount = 0;

            progressBar.Visibility = System.Windows.Visibility.Visible;
            mainGrid.Visibility = System.Windows.Visibility.Hidden;

            Task t = Task.Factory.StartNew( () =>
            {
                foreach (TestCase currentSelectedTestCase in TestCasesBatchDuplicateViewModel.SelectedTestCases)
                {
                    currentSelectedTestCase.DuplicateTestCase(textReplacePairsList, sharedStepIdReplacePairList, newSuiteTitle,
                        TestCasesBatchDuplicateViewModel.ReplaceInTitles, TestCasesBatchDuplicateViewModel.ReplaceSharedSteps, TestCasesBatchDuplicateViewModel.ReplaceInSteps);
                    duplicatedCount++;
                }
                TestCasesBatchDuplicateViewModel.ReinitializeTestCases();
                TestCasesBatchDuplicateViewModel.FilterTestCases();
            });
            t.ContinueWith(antecedent =>
            {
                progressBar.Visibility = System.Windows.Visibility.Hidden;
                mainGrid.Visibility = System.Windows.Visibility.Visible;
                ModernDialog.ShowMessage(String.Format("{0} test cases duplicated.", duplicatedCount), "Success!", MessageBoxButton.OK);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void InitializeCurrentSelectedTestCases()
        {
            TestCasesBatchDuplicateViewModel.SelectedTestCases.Clear();
            foreach (TestCase currentSelectedItem in dgTestCases.SelectedItems)
            {
                TestCasesBatchDuplicateViewModel.SelectedTestCases.Add(currentSelectedItem);
            }
        }

        private void btnFindAndReplace_Click(object sender, RoutedEventArgs e)
        {
            InitializeCurrentSelectedTestCases();
            List<TextReplacePair> textReplacePairsList = TestCasesBatchDuplicateViewModel.ObservableTextReplacePairs.ToList();
            List<SharedStepIdReplacePair> sharedStepIdReplacePairList = TestCasesBatchDuplicateViewModel.ObservableSharedStepIdReplacePairs.ToList();
            int replacedCount = 0;

            progressBar.Visibility = System.Windows.Visibility.Visible;
            mainGrid.Visibility = System.Windows.Visibility.Hidden;

            Task t = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < TestCasesBatchDuplicateViewModel.SelectedTestCases.Count; i++)
                {
                    TestCasesBatchDuplicateViewModel.SelectedTestCases[i].FindAndReplaceInTestCase(textReplacePairsList, sharedStepIdReplacePairList,
                       TestCasesBatchDuplicateViewModel.ReplaceInTitles, TestCasesBatchDuplicateViewModel.ReplaceSharedSteps, TestCasesBatchDuplicateViewModel.ReplaceInSteps);
                    replacedCount++;
                }               
            });
            t.ContinueWith(antecedent =>
            {
                TestCasesBatchDuplicateViewModel.ReinitializeTestCases();
                TestCasesBatchDuplicateViewModel.FilterTestCases();
                progressBar.Visibility = System.Windows.Visibility.Hidden;
                mainGrid.Visibility = System.Windows.Visibility.Visible;
                ModernDialog.ShowMessage(String.Format("{0} test cases replaced.", replacedCount), "Success!", MessageBoxButton.OK);
            }, TaskScheduler.FromCurrentSynchronizationContext());           
        }     
    }
}