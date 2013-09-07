﻿using System;
using System.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace TestCaseManagerApp
{
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
            DefaultLinkNavigator _navigator = new DefaultLinkNavigator();
            _navigator.Navigate(new Uri(url, UriKind.Relative), source, null);
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
        /// Navigates to test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="testCaseId">The test case unique identifier.</param>
        /// <param name="suiteId">The suite unique identifier.</param>
        public static void NavigateToTestCasesEditView(this FrameworkElement source, int testCaseId, int suiteId)
        {
            string url = String.Format("/Views/TestCaseEditView.xaml#id={0}&suiteId={1}", testCaseId, suiteId);
     
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
            string url = String.Format("/Views/TestCaseDetailedView.xaml#id={0}&suiteId={1}", testCaseId, suiteId);

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
            string url = String.Format("/Views/TestCaseEditView.xaml#id={0}&suiteId={1}&createNew={2}&duplicate={3}", testCaseId, suiteId, createNew, duplicate);

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
            string url = String.Format("/Views/AssociateTestView.xaml#id={0}&suiteId={1}&createNew={2}&duplicate={3}", testCaseId, suiteId, createNew, duplicate);

            source.Navigate(url);
        }

        /// <summary>
        /// Navigates to test cases edit view.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        public static void NavigateToTestCasesEditView(this FrameworkElement source, bool createNew, bool duplicate)
        {
            string url = String.Format("/Views/TestCaseEditView.xaml#createNew={0}&duplicate={1}", createNew, duplicate);

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
    }
}
