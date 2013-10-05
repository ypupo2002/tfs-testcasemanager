// <copyright file="TestCaseDetailedView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case detailed(read mode) page
    /// </summary>
    public partial class TestCaseDetailedView : UserControl, IContent
    {
        /// <summary>
        /// The edit command
        /// </summary>
        public static RoutedCommand EditCommand = new RoutedCommand();

        /// <summary>
        /// The duplicate command
        /// </summary>
        public static RoutedCommand DuplicateCommand = new RoutedCommand();

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseDetailedView"/> class.
        /// </summary>
        public TestCaseDetailedView()
        {
            this.InitializeComponent();
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
        /// Gets or sets the test case detailed view model.
        /// </summary>
        /// <value>
        /// The test case detailed view model.
        /// </value>
        public TestCaseDetailedViewModel TestCaseDetailedViewModel { get; set; }

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
                TestCaseDetailedViewModel = new TestCaseDetailedViewModel(this.TestCaseId, this.TestSuiteId);
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = TestCaseDetailedViewModel;
                EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
                DuplicateCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));
                this.HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());   
        }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            isInitialized = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            this.TestCaseId = int.Parse(fm.Fragments["id"]);
            this.TestSuiteId = int.Parse(fm.Fragments["suiteId"]);
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
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestCaseDetailedViewModel.TestCase.ITestSuiteBase != null)
            {
                this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
            }
            else
            {
                this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, -1);
            }            
        }

        /// <summary>
        /// Handles the Click event of the DuplicateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestCaseDetailedViewModel.TestCase.ITestSuiteBase != null)
            {
                this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id, true, true);
            }
            else
            {
                this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, -1, true, true);
            }            
        }

        /// <summary>
        /// Handles the LoadingRow event of the dgTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridRowEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Adding 1 to make the row count start at 1 instead of 0
            // as pointed out by daub815
            e.Row.Header = (e.Row.GetIndex() + 1).ToString(); 
        }   
    }
}
