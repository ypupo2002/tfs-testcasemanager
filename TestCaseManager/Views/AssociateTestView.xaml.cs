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
        public int TestCaseId { get; set; }
        public int TestSuiteId { get; set; }
        public bool CreateNew { get; set; }
        public bool Duplicate { get; set; }
        public AssociateTestViewModel AssociateTestViewModel { get; set; }

        public AssociateTestView()
        {
            InitializeComponent();
            InitializeSearchBoxes();
        }

        private void InitializeSearchBoxes()
        {
            tbFullName.Text = "Full Name";
            tbClassName.Text = "Class Name";
        }

        private void AssociateTestView_Loaded(object sender, RoutedEventArgs e)
        {
            AssociateTestViewModel = new AssociateTestViewModel(TestCaseId);
            this.DataContext = AssociateTestViewModel;
            cbTestType.SelectedIndex = 0;
        }

        private void tbFullName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbFullName.ClearDefaultSearchBoxContent(ref AssociateTestViewModel.fullNameFlag);
        }

        private void tbFullName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbFullName.RestoreDefaultSearchBoxText("Full Name", ref AssociateTestViewModel.fullNameFlag);
        }

        private void tbFullName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            FilterTests();
        }

        private void FilterTests()
        {
            if (AssociateTestViewModel == null)
            {
                return;
            }
            ReinitializeTests();
            string fullNameFilter = tbFullName.Text.Equals("Full Name") ? String.Empty : tbFullName.Text;
            string classNameFilter = tbClassName.Text.Equals("Class Name") ? String.Empty : tbClassName.Text;

            var filteredList = AssociateTestViewModel.ObservableTests
                .Where(t => ((AssociateTestViewModel.fullNameFlag && !String.IsNullOrEmpty(fullNameFilter)) ? t.FullName.ToLower().Contains(fullNameFilter.ToLower()) : true)
                    && ((AssociateTestViewModel.classNameFlag && !String.IsNullOrEmpty(classNameFilter)) ? t.ClassName.ToLower().Contains(classNameFilter.ToLower()) : true)).ToList();
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

        private void tbClassName_GotFocus(object sender, RoutedEventArgs e)
        {
            tbClassName.ClearDefaultSearchBoxContent(ref AssociateTestViewModel.classNameFlag);
        }

        private void tbClassName_LostFocus(object sender, RoutedEventArgs e)
        {
            tbClassName.RestoreDefaultSearchBoxText("Class Name", ref AssociateTestViewModel.classNameFlag);
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
            ComboBox_DropdownBehavior.cbo_MouseMove(sender, e);
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            CreateNew = false;
            Duplicate = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            string testCaseId = fm.Get("id");
            if (!String.IsNullOrEmpty(testCaseId))
                TestCaseId = int.Parse(testCaseId);
            string suiteId = fm.Get("suiteId");
            if (!String.IsNullOrEmpty(suiteId))
                TestSuiteId = int.Parse(suiteId);
            string createNew = fm.Get("createNew");
            if (!String.IsNullOrEmpty(createNew))
                CreateNew = bool.Parse(createNew);
            string duplicate = fm.Get("duplicate");
            if (!String.IsNullOrEmpty(duplicate))
                Duplicate = bool.Parse(duplicate);   
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

        private void btnAssociate_Click(object sender, RoutedEventArgs e)
        {
            Test currentSelectedTest = dgTests.SelectedItem as Test;
            string testType = cbTestType.Text;
            AssociateTestViewModel.AssociateTestCaseToTest(currentSelectedTest, testType);
            //ModernDialog.ShowMessage("Test Associated.", "Success", MessageBoxButton.OK);
            this.NavigateToTestCasesEditView(TestCaseId, TestSuiteId, CreateNew, Duplicate);
            //this.NavigateToTestCasesEditViewFromAssociatedAutomation();

        }        
    }
}
