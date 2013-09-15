using System;
using System.Linq;
using System.Windows;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerApp.BusinessLogic.Entities;
using TestCaseManagerApp.Helpers;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    public partial class AssociateTestView : System.Windows.Controls.UserControl, IContent
    {
        
        public AssociateTestViewModel AssociateTestViewModel { get; set; }

        public AssociateTestView()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            FragmentManager fm = new FragmentManager(e.Fragment);
            string testCaseIdStr = fm.Get("id");
            if (!string.IsNullOrEmpty(testCaseIdStr))
            {
                int testCaseId = int.Parse(testCaseIdStr);
                AssociateTestViewModel = new AssociateTestViewModel(testCaseId);
            }
           
            string suiteId = fm.Get("suiteId");
            if (!string.IsNullOrEmpty(suiteId))
            {
                AssociateTestViewModel.TestSuiteId = int.Parse(suiteId);
            }
            string createNew = fm.Get("createNew");
            if (!string.IsNullOrEmpty(createNew))
            {
                AssociateTestViewModel.CreateNew = bool.Parse(createNew);
            }
            string duplicate = fm.Get("duplicate");
            if (!string.IsNullOrEmpty(duplicate))
            {
                AssociateTestViewModel.Duplicate = bool.Parse(duplicate);
            }
           
            this.DataContext = AssociateTestViewModel;
            cbTestType.SelectedIndex = 0;
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

        private void FilterTests()
        {
            if (AssociateTestViewModel == null)
            {
                return;
            }
            ReinitializeTests();
            string fullNameFilter = tbFullName.Text.Equals("Full Name") ? string.Empty : tbFullName.Text;
            string classNameFilter = tbClassName.Text.Equals("Class Name") ? string.Empty : tbClassName.Text;

            var filteredList = AssociateTestViewModel.ObservableTests
                .Where(t => ((AssociateTestViewModel.AssociateTestViewFilters.IsFullNameFilterSet && !string.IsNullOrEmpty(fullNameFilter)) ? t.FullName.ToLower().Contains(fullNameFilter.ToLower()) : true)
                    && ((AssociateTestViewModel.AssociateTestViewFilters.IsClassNameFilterSet && !string.IsNullOrEmpty(classNameFilter)) ? t.ClassName.ToLower().Contains(classNameFilter.ToLower()) : true)).ToList();
            AssociateTestViewModel.ObservableTests.Clear();
            filteredList.ForEach(x => AssociateTestViewModel.ObservableTests.Add(x));
        }

        private void ReinitializeTests()
        {
            AssociateTestViewModel.ObservableTests.Clear();
            foreach (var item in AssociateTestViewModel.InitialTestsCollection)
            {
                AssociateTestViewModel.ObservableTests.Add(item);
            }
        }

        private void tbFullName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbFullName.ClearDefaultContent(ref AssociateTestViewModel.AssociateTestViewFilters.IsFullNameFilterSet);
        }

        private void tbFullName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbFullName.RestoreDefaultText("Full Name", ref AssociateTestViewModel.AssociateTestViewFilters.IsFullNameFilterSet);
        }

        private void tbFullName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            FilterTests();
        }

        private void tbClassName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbClassName.ClearDefaultContent(ref AssociateTestViewModel.AssociateTestViewFilters.IsClassNameFilterSet);
        }

        private void tbClassName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbClassName.RestoreDefaultText("Class Name", ref AssociateTestViewModel.AssociateTestViewFilters.IsClassNameFilterSet);
        }

        private void tbClassName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            FilterTests();
        }

        private void cbTestType_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            cbTestType.IsDropDownOpen = true;
            cbTestType.Focus();
        }

        private void cbTestType_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }        

        private void btnAssociate_Click(object sender, RoutedEventArgs e)
        {
            Test currentSelectedTest = dgTests.SelectedItem as Test;
            string testType = cbTestType.Text;
            AssociateTestViewModel.AssociateTestCaseToTest(currentSelectedTest, testType);
            //ModernDialog.ShowMessage("Test Associated.", "Success", MessageBoxButton.OK);
            this.NavigateToTestCasesEditView(AssociateTestViewModel.TestCaseId, AssociateTestViewModel.TestSuiteId, AssociateTestViewModel.CreateNew, AssociateTestViewModel.Duplicate);
            //this.NavigateToTestCasesEditViewFromAssociatedAutomation();

        }        
    }
}
