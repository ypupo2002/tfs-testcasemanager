using System;
using System.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace TestCaseManagerApp
{
    public static class Navigator
    {
        public static void Navigate(this FrameworkElement source, string url)
        {
            DefaultLinkNavigator _navigator = new DefaultLinkNavigator();
            _navigator.Navigate(new Uri(url, UriKind.Relative), source, null);
        }

        public static void NavigateToTestCasesInitialView(this FrameworkElement source)
        {
            string url = "/Views/TestCasesInitialView.xaml";
            source.Navigate(url);
        }

        public static void NavigateToTestCasesEditView(this FrameworkElement source, int testCaseId, int suiteId)
        {
            string url = String.Format("/Views/TestCaseEditView.xaml#id={0}&suiteId={1}", testCaseId, suiteId);
     
            source.Navigate(url);
        }

        public static void NavigateToTestCasesDetailedView(this FrameworkElement source, int testCaseId, int suiteId)
        {
            string url = String.Format("/Views/TestCaseDetailedView.xaml#id={0}&suiteId={1}", testCaseId, suiteId);

            source.Navigate(url);
        }

        public static void NavigateToAppearanceSettingsView(this FrameworkElement source)
        {
            source.Navigate("/Views/SettingsView.xaml");
        }

        public static void NavigateToTestCasesEditView(this FrameworkElement source, int testCaseId, int suiteId, bool createNew, bool duplicate)
        {
            string url = String.Format("/Views/TestCaseEditView.xaml#id={0}&suiteId={1}&createNew={2}&duplicate={3}", testCaseId, suiteId, createNew, duplicate);

            source.Navigate(url);
        }

        public static void NavigateToAssociateAutomationView(this FrameworkElement source, int testCaseId, int suiteId, bool createNew, bool duplicate)
        {
            string url = String.Format("/Views/AssociateTestView.xaml#id={0}&suiteId={1}&createNew={2}&duplicate={3}", testCaseId, suiteId, createNew, duplicate);

            source.Navigate(url);
        }

        public static void NavigateToTestCasesEditView(this FrameworkElement source, bool createNew, bool duplicate)
        {
            string url = String.Format("/Views/TestCaseEditView.xaml#createNew={0}&duplicate={1}", createNew, duplicate);

            source.Navigate(url);
        }

        public static void NavigateToTestCasesEditViewFromAssociatedAutomation(this FrameworkElement source)
        {
            string url = "/Views/TestCaseEditView.xaml#comesFromAssociatedAutomation=true";

            source.Navigate(url);
        }
    }
}
