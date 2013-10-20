// <copyright file="Navigator.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore
{
    using System;
    using System.Windows;
    using FirstFloor.ModernUI.Windows.Navigation;

    /// <summary>
    /// Contains methods which navigate to different views with option to set different parameters
    /// </summary>
    public static class Navigator
    {
        /// <summary>
        /// Navigates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="url">The URL.</param>
        public static void Navigate(this FrameworkElement source, string url)
        {
            DefaultLinkNavigator navigator = new DefaultLinkNavigator();
            navigator.Navigate(new Uri(url, UriKind.Relative), source, null);
        }

        /// <summary>
        /// Navigates to test cases initial view.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void NavigateToTestCasesInitialView(this FrameworkElement source)
        {
            string url = "/Views/TestCasesInitialView.xaml";
            source.Navigate(url);
        }

        /// <summary>
        /// Navigates the back.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void NavigateBack(this FrameworkElement source)
        {
            string url = "cmd://browseback";
            //source.Navigate(url);
            DefaultLinkNavigator navigator = new DefaultLinkNavigator();
            navigator.Navigate(new Uri(url, UriKind.Absolute), source, "_self");
        }

        /// <summary>
        /// Navigates the automatic shared steps initial view.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void NavigateToSharedStepsInitialView(this FrameworkElement source)
        {
            string url = "/Views/SharedStepsInitialView.xaml";
            source.Navigate(url);
        }

        /// <summary>
        /// Navigates to test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        public static void NavigateToTestCasesEditView(this FrameworkElement source, int testCaseId, int suiteId)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#id={0}&suiteId={1}", testCaseId, suiteId);
     
            source.Navigate(url);
        }

        /// <summary>
        /// Navigates to test cases detailed view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        public static void NavigateToTestCasesDetailedView(this FrameworkElement source, int testCaseId, int suiteId)
        {
            string url = string.Format("/Views/TestCaseDetailedView.xaml#id={0}&suiteId={1}", testCaseId, suiteId);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates to appearance settings view.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void NavigateToAppearanceSettingsView(this FrameworkElement source)
        {
            source.Navigate("/Views/SettingsView.xaml");
        }

        /// <summary>
        /// Navigates to test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public static void NavigateToTestCasesEditView(this FrameworkElement source, int testCaseId, int suiteId, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#id={0}&suiteId={1}&createNew={2}&duplicate={3}", testCaseId, suiteId, createNew, duplicate);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates the automatic test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="isSharedStep">if set to <c>true</c> [is shared step].</param>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public static void NavigateToTestCasesEditView(this FrameworkElement source, bool isSharedStep, int sharedStepId, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#isSharedStep={0}&sharedStepId={1}&createNew={2}&duplicate={3}", isSharedStep, sharedStepId, createNew, duplicate);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates the automatic test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="isSharedStep">if set to <c>true</c> [is shared step].</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public static void NavigateToTestCasesEditView(this FrameworkElement source, bool isSharedStep, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#isSharedStep={0}&createNew={1}&duplicate={2}", isSharedStep, createNew, duplicate);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates the automatic test cases edit view. Shared step edit.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="isShared">if set to <c>true</c> [is shared].</param>
        /// <param name="sharedStepId">The test step unique identifier.</param>
        public static void NavigateToTestCasesEditView(this FrameworkElement source, bool isShared, int sharedStepId)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#isSharedStep={0}&sharedStepId={1}", isShared, sharedStepId);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates to associate automation view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public static void NavigateToAssociateAutomationView(this FrameworkElement source, int testCaseId, int suiteId, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/AssociateTestView.xaml#id={0}&suiteId={1}&createNew={2}&duplicate={3}", testCaseId, suiteId, createNew, duplicate);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates to test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public static void NavigateToTestCasesEditView(this FrameworkElement source, int suiteId, bool createNew, bool duplicate)
        {
            string url = string.Format("/Views/TestCaseEditView.xaml#suiteId={0}&createNew={1}&duplicate={2}", suiteId, createNew, duplicate);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates the automatic test case batch duplicate view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="loadTestCases">if set to <c>true</c> [load test cases].</param>
        /// <param name="testCaseIds">The test case ids.</param>
        public static void NavigateToTestCaseBatchDuplicateView(this FrameworkElement source, bool loadTestCases, bool loadSpecificTestCases)
        {
            string url = string.Format("/Views/TestCaseBatchDuplicateView.xaml#loadTestCases={0}&loadSpecificTestCases={1}", loadTestCases, loadSpecificTestCases);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates to test cases edit view from associated automation.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void NavigateToTestCasesEditViewFromAssociatedAutomation(this FrameworkElement source)
        {
            string url = "/Views/TestCaseEditView.xaml#comesFromAssociatedAutomation=true";

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates the automatic test cases execution arrangement.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        public static void NavigateToTestCasesExecutionArrangement(this FrameworkElement source, int suiteId)
        {
            string url = string.Format("/Views/TestCaseExecutionArrangmentView.xaml#suiteId={0}", suiteId);
            source.Navigate(url);
        }
    }
}
