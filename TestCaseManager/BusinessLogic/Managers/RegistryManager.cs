// <copyright file="RegistryManager.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Win32;

    /// <summary>
    /// Contains helper methods for saving and reading specific app related information from Windows Registry
    /// </summary>
    public class RegistryManager
    {
        /// <summary>
        /// The main registry sub key name
        /// </summary>
        private static string mainRegistrySubKeyName = "TestCaseManager";

        /// <summary>
        /// The data registry sub key name
        /// </summary>
        private static string dataRegistrySubKeyName = "data";

        /// <summary>
        /// The appereance registry sub key name- main theme/color sub key
        /// </summary>
        private static string appereanceRegistrySubKeyName = "Appereance";
        
        /// <summary>
        /// The theme registry sub key name
        /// </summary>
        private static string themeRegistrySubKeyName = "theme";

        /// <summary>
        /// The color registry sub key name
        /// </summary>
        private static string colorRegistrySubKeyName = "color";

        /// <summary>
        /// The shouldOpenDropDownOnHover registry sub key name- shows if the drop downs will be opened on hover
        /// </summary>
        private static string shouldOpenDropDownOnHoverRegistrySubKeyName = "shouldOpenDropDrownOnHover";
      
        /// <summary>
        /// The TFS settings registry sub key name- main sub key for team project URI and team project name
        /// </summary>
        private static string tfsSettingsRegistrySubKeyName = "TfsSettings";

        /// <summary>
        /// The filters registry sub key name
        /// </summary>
        private static string filtersRegistrySubKeyName = "Filters";

        /// <summary>
        /// The initial filters registry sub key name
        /// </summary>
        private static string initialFiltersRegistrySubKeyName = "InitialFilters";

        /// <summary>
        /// The suite filter registry sub key name
        /// </summary>
        private static string suiteFilterRegistrySubKeyName = "suiteFilter";

        /// <summary>
        /// The suite filter registry sub key name
        /// </summary>
        private static string selectedSuiteIdFilterRegistrySubKeyName = "selectedSuiteId";

        /// <summary>
        /// The automation association registry sub key name
        /// </summary>
        private static string automationAssociationRegistrySubKeyName = "AutomationAssociation";

        /// <summary>
        /// The project DLL path registry sub key name
        /// </summary>
        private static string projectDllPathRegistrySubKeyName = "ProjectPathDll";

        /// <summary>
        /// The team project URI registry sub key name
        /// </summary>
        private static string teamProjectUriRegistrySubKeyName = "teamProjectUri";
        
        /// <summary>
        /// The team project name registry sub key name
        /// </summary>
        private static string teamProjectNameRegistrySubKeyName = "teamProjectName";

        /// <summary>
        /// The test plan registry sub key name
        /// </summary>
        private static string testPlanRegistrySubKeyName = "testPlan";

        /// <summary>
        /// The title prompt dialog registry sub key name
        /// </summary>
        private static string titlePromptDialogRegistrySubKeyName = "titlePromptDialog";

        /// <summary>
        /// The title prompt dialog is canceled registry sub key name
        /// </summary>
        private static string isCanceledtitlePromptDialogRegistrySubKeyName = "titlePromptDialogIsCanceled";

        /// <summary>
        /// The title title prompt dialog is canceled registry sub key name
        /// </summary>
        private static string titleTitlePromptDialogIsCanceledRegistrySubKeyName = "titleTitlePromptDialog";

        /// <summary>
        /// Writes the current theme to registry.
        /// </summary>
        /// <param name="theme">The theme name.</param>
        public static void WriteCurrentTheme(string theme)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey appereanceR = dataR.CreateSubKey(appereanceRegistrySubKeyName);
            appereanceR.SetValue(themeRegistrySubKeyName, theme);
            appereanceR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the drop down behavior to registry.
        /// </summary>
        /// <param name="shouldOpenDropDownOnHover">if set to <c>true</c> [should open drop down configuration hover].</param>
        public static void WriteDropDownBehavior(bool shouldOpenDropDownOnHover)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey appereanceR = dataR.CreateSubKey(appereanceRegistrySubKeyName);
            appereanceR.SetValue(shouldOpenDropDownOnHoverRegistrySubKeyName, shouldOpenDropDownOnHover);
            appereanceR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the suite filter.
        /// </summary>
        /// <param name="suiteFilter">The suite filter.</param>
        public static void WriteSuiteFilter(string suiteFilter)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey filtersR = dataR.CreateSubKey(filtersRegistrySubKeyName);
            RegistryKey initialFiltersR = filtersR.CreateSubKey(initialFiltersRegistrySubKeyName);
            initialFiltersR.SetValue(suiteFilterRegistrySubKeyName, suiteFilter);
            initialFiltersR.Close();
            filtersR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the selected suite unique identifier.
        /// </summary>
        /// <param name="suiteId">The suite unique identifier.</param>
        public static void WriteSelectedSuiteId(int suiteId)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey filtersR = dataR.CreateSubKey(filtersRegistrySubKeyName);
            RegistryKey initialFiltersR = filtersR.CreateSubKey(initialFiltersRegistrySubKeyName);
            initialFiltersR.SetValue(selectedSuiteIdFilterRegistrySubKeyName, suiteId.ToString());
            initialFiltersR.Close();
            filtersR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the current team project URI to registry.
        /// </summary>
        /// <param name="teamProjectUri">The team project URI.</param>
        public static void WriteCurrentTeamProjectUri(string teamProjectUri)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey tfsSettingsR = dataR.CreateSubKey(tfsSettingsRegistrySubKeyName);
            tfsSettingsR.SetValue(teamProjectUriRegistrySubKeyName, teamProjectUri);
            tfsSettingsR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the name of the current team project to registry.
        /// </summary>
        /// <param name="teamProjectName">Name of the team project.</param>
        public static void WriteCurrentTeamProjectName(string teamProjectName)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey tfsSettingsR = dataR.CreateSubKey(tfsSettingsRegistrySubKeyName);
            tfsSettingsR.SetValue(teamProjectNameRegistrySubKeyName, teamProjectName);
            tfsSettingsR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the current test plan to registry.
        /// </summary>
        /// <param name="testPlan">The test plan.</param>
        public static void WriteCurrentTestPlan(string testPlan)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey tfsSettingsR = dataR.CreateSubKey(tfsSettingsRegistrySubKeyName);
            tfsSettingsR.SetValue(testPlanRegistrySubKeyName, testPlan);
            tfsSettingsR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the current project DLL path to registry.
        /// </summary>
        /// <param name="projectDllPath">The project DLL path.</param>
        public static void WriteCurrentProjectDllPath(string projectDllPath)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey associatedAutomation = dataR.CreateSubKey(automationAssociationRegistrySubKeyName);
            associatedAutomation.SetValue(projectDllPathRegistrySubKeyName, projectDllPath);
            associatedAutomation.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the if the title promt dialog is canceled .
        /// </summary>
        /// <param name="isCanceled">if set to <c>true</c> [is canceled].</param>
        public static void WriteIsCanceledTitlePromtDialog(bool isCanceled)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey titlePromtDialog = dataR.CreateSubKey(titlePromptDialogRegistrySubKeyName);
            titlePromtDialog.SetValue(isCanceledtitlePromptDialogRegistrySubKeyName, isCanceled.ToString());
            titlePromtDialog.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the title in title promt dialog.
        /// </summary>
        /// <param name="title">The title.</param>
        public static void WriteTitleTitlePromtDialog(string title)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey titlePromtDialog = dataR.CreateSubKey(titlePromptDialogRegistrySubKeyName);
            titlePromtDialog.SetValue(titleTitlePromptDialogIsCanceledRegistrySubKeyName, title);
            titlePromtDialog.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the current colors to registry.
        /// </summary>
        /// <param name="red">The red part.</param>
        /// <param name="green">The green part.</param>
        /// <param name="blue">The blue part.</param>
        public static void WriteCurrentColors(byte red, byte green, byte blue)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(mainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(dataRegistrySubKeyName);
            RegistryKey appereanceR = dataR.CreateSubKey(appereanceRegistrySubKeyName);
            appereanceR.SetValue(colorRegistrySubKeyName, string.Format("{0}&{1}&{2}", red, green, blue));
            appereanceR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Gets the team project URI from registry.
        /// </summary>
        /// <returns>team project URI</returns>
        public static string GetTeamProjectUri()
        {
            string teamProjectUri = string.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey tfsSettings = dataR.OpenSubKey(tfsSettingsRegistrySubKeyName);

                if (tfsSettings != null && dataR != null && ata != null)
                {
                    teamProjectUri = (string)tfsSettings.GetValue(teamProjectUriRegistrySubKeyName);
                    tfsSettings.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            { 
                // TODO: Add Exception Logging
            }
            return teamProjectUri;
        }

        /// <summary>
        /// Gets the name of the team project from registry.
        /// </summary>
        /// <returns>name of the team project</returns>
        public static string GetTeamProjectName()
        {
            string teamProjectName = string.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey tfsSettings = dataR.OpenSubKey(tfsSettingsRegistrySubKeyName);

                if (tfsSettings != null && dataR != null && ata != null)
                {
                    teamProjectName = (string)tfsSettings.GetValue(teamProjectNameRegistrySubKeyName);
                    tfsSettings.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            { 
                // TODO: Add Exception Logging
            }

            return teamProjectName;
        }

        /// <summary>
        /// Gets the project DLL path from registry.
        /// </summary>
        /// <returns>the project DLL path</returns>
        public static string GetProjectDllPath()
        {
            string projectDllPath = string.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey associatedAutomation = dataR.OpenSubKey(automationAssociationRegistrySubKeyName);

                if (associatedAutomation != null && dataR != null && ata != null)
                {
                    projectDllPath = (string)associatedAutomation.GetValue(projectDllPathRegistrySubKeyName);
                    associatedAutomation.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                // TODO: Add Exception Logging
            }

            return projectDllPath;
        }

        /// <summary>
        /// Gets the test plan from registry.
        /// </summary>
        /// <returns>the test plan</returns>
        public static string GetTestPlan()
        {
            string testPlan = string.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey tfsSettings = dataR.OpenSubKey(tfsSettingsRegistrySubKeyName);

                if (tfsSettings != null && dataR != null && ata != null)
                {
                    testPlan = (string)tfsSettings.GetValue(testPlanRegistrySubKeyName);
                    tfsSettings.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                // TODO: Add Exception Logging
            }

            return testPlan;
        }

        /// <summary>
        /// Gets the title in title promt dialog.
        /// </summary>
        /// <returns>the title</returns>
        public static string GetTitleTitlePromtDialog()
        {
            string title = string.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey titlePromtDialog = dataR.OpenSubKey(titlePromptDialogRegistrySubKeyName);

                if (titlePromtDialog != null && dataR != null && ata != null)
                {
                    title = (string)titlePromtDialog.GetValue(titleTitlePromptDialogIsCanceledRegistrySubKeyName);
                    titlePromtDialog.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                // TODO: Add Exception Logging
            }

            return title;
        }

        /// <summary>
        /// Gets if the title promt dialog is canceled.
        /// </summary>
        /// <returns> if the title promt dialog was canceled</returns>
        public static bool GetIsCanceledTitlePromtDialog()
        {
            bool isCanceled = true;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey titlePromtDialog = dataR.OpenSubKey(titlePromptDialogRegistrySubKeyName);

                if (titlePromtDialog != null && dataR != null && ata != null)
                {
                    string isCanceledStr = (string)titlePromtDialog.GetValue(isCanceledtitlePromptDialogRegistrySubKeyName);
                    isCanceled = bool.Parse(isCanceledStr);
                    titlePromtDialog.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                // TODO: Add Exception Logging
            }

            return isCanceled;
        }

        /// <summary>
        /// Gets the colors from registry.
        /// </summary>
        /// <returns>the colors</returns>
        public static string[] GetColors()
        {
            string[] colorsStr = null;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey appereanceR = dataR.OpenSubKey(appereanceRegistrySubKeyName);
                string colors = string.Empty;

                if (appereanceR != null && dataR != null && ata != null)
                {
                    colors = (string)appereanceR.GetValue(colorRegistrySubKeyName);
                    appereanceR.Close();
                    dataR.Close();
                    ata.Close();
                }
                if (!string.IsNullOrEmpty(colors))
                {
                    colorsStr = colors.Split('&');
                }
            }
            catch
            {
                // TODO: Add Exception Logging
            }

            return colorsStr;
        }

        /// <summary>
        /// Gets the theme from registry.
        /// </summary>
        /// <returns>the theme</returns>
        public static string GetTheme()
        {
            string theme = string.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey appereanceR = dataR.OpenSubKey(appereanceRegistrySubKeyName);

                if (appereanceR != null && dataR != null && ata != null)
                {
                    theme = (string)appereanceR.GetValue(themeRegistrySubKeyName);
                    appereanceR.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                // TODO: Add Exception Logging
            }

            return theme;
        }

        /// <summary>
        /// Gets the drop down behavior from registry.
        /// </summary>
        /// <returns>drop down behavior</returns>
        public static bool GetDropDownBehavior()
        {
            bool shouldOpenDropDownOnHover = false;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey appereanceR = dataR.OpenSubKey(appereanceRegistrySubKeyName);

                if (appereanceR != null && dataR != null && ata != null)
                {
                    shouldOpenDropDownOnHover = bool.Parse((string)appereanceR.GetValue(shouldOpenDropDownOnHoverRegistrySubKeyName));
                    appereanceR.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                // TODO: Add Exception Logging
            }

            return shouldOpenDropDownOnHover;
        }

        /// <summary>
        /// Gets the suite filter.
        /// </summary>
        /// <returns>the suite filter</returns>
        public static string GetSuiteFilter()
        {
            string suiteFilter = string.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey filtersR = dataR.OpenSubKey(filtersRegistrySubKeyName);
                RegistryKey initialFiltersR = filtersR.OpenSubKey(initialFiltersRegistrySubKeyName);

                if (filtersR != null && dataR != null && ata != null && initialFiltersR != null)
                {
                    suiteFilter = (string)initialFiltersR.GetValue(suiteFilterRegistrySubKeyName);
                    filtersR.Close();
                    initialFiltersR.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                // TODO: Add Exception Logging
            }

            return suiteFilter;
        }

        /// <summary>
        /// Gets the selected suite unique identifier from registry;
        /// </summary>
        /// <returns>selected suite id</returns>
        public static int GetSelectedSuiteId()
        {
            int selectedSuiteId = -1;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(mainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(dataRegistrySubKeyName);
                RegistryKey filtersR = dataR.OpenSubKey(filtersRegistrySubKeyName);
                RegistryKey initialFiltersR = filtersR.OpenSubKey(initialFiltersRegistrySubKeyName);

                if (filtersR != null && dataR != null && ata != null && initialFiltersR != null)
                {
                    string selectedSuiteIdStr = (string)initialFiltersR.GetValue(selectedSuiteIdFilterRegistrySubKeyName);
                    selectedSuiteId = int.Parse(selectedSuiteIdStr);
                    filtersR.Close();
                    initialFiltersR.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch (Exception ex)
            {
                // TODO: Add Exception Logging
            }

            return selectedSuiteId;
        }
    }
}
