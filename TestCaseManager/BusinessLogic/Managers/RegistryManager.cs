using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TestCaseManagerApp
{
    /// <summary>
    /// Contains helper methods for saving and reading specific app related information from Windows Registry
    /// </summary>
    public class RegistryManager
    {
        public static string MainRegistrySubKeyName = "TestCaseManager";
        public static string DataRegistrySubKeyName = "data";
        public static string ThemeRegistrySubKeyName = "theme";
        public static string AppereanceRegistrySubKeyName = "Appereance";
        public static string TfsSettingsRegistrySubKeyName = "TfsSettings";
        public static string AutomationAssociationRegistrySubKeyName = "AutomationAssociation";
        public static string ProjectDllPathRegistrySubKeyName = "ProjectPathDll";
        public static string TeamProjectUriRegistrySubKeyName = "teamProjectUri";
        public static string TeamProjectNameRegistrySubKeyName = "teamProjectName";
        public static string TestPlanRegistrySubKeyName = "testPlan";
        public static string ColorRegistrySubKeyName = "color";

        /// <summary>
        /// Writes the current theme to registry.
        /// </summary>
        /// <param name="theme">The theme name.</param>
        public static void WriteCurrentTheme(string theme)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(MainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(DataRegistrySubKeyName);
            RegistryKey appereanceR = dataR.CreateSubKey(AppereanceRegistrySubKeyName);
            appereanceR.SetValue(ThemeRegistrySubKeyName, theme);
            appereanceR.Close();
            dataR.Close();
            ata.Close();
        }

        /// <summary>
        /// Writes the current team project URI to registry.
        /// </summary>
        /// <param name="teamProjectUri">The team project URI.</param>
        public static void WriteCurrentTeamProjectUri(string teamProjectUri)
        {
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(MainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(DataRegistrySubKeyName);
            RegistryKey tfsSettingsR = dataR.CreateSubKey(TfsSettingsRegistrySubKeyName);
            tfsSettingsR.SetValue(TeamProjectUriRegistrySubKeyName, teamProjectUri);
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
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(MainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(DataRegistrySubKeyName);
            RegistryKey tfsSettingsR = dataR.CreateSubKey(TfsSettingsRegistrySubKeyName);
            tfsSettingsR.SetValue(TeamProjectNameRegistrySubKeyName, teamProjectName);
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
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(MainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(DataRegistrySubKeyName);
            RegistryKey tfsSettingsR = dataR.CreateSubKey(TfsSettingsRegistrySubKeyName);
            tfsSettingsR.SetValue(TestPlanRegistrySubKeyName, testPlan);
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
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(MainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(DataRegistrySubKeyName);
            RegistryKey associatedAutomation = dataR.CreateSubKey(AutomationAssociationRegistrySubKeyName);
            associatedAutomation.SetValue(ProjectDllPathRegistrySubKeyName, projectDllPath);
            associatedAutomation.Close();
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
            RegistryKey ata = Registry.CurrentUser.CreateSubKey(MainRegistrySubKeyName);
            RegistryKey dataR = ata.CreateSubKey(DataRegistrySubKeyName);
            RegistryKey appereanceR = dataR.CreateSubKey(AppereanceRegistrySubKeyName);
            appereanceR.SetValue(ColorRegistrySubKeyName, String.Format("{0}&{1}&{2}", red, green, blue));
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
            string teamProjectUri = String.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(MainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(DataRegistrySubKeyName);
                RegistryKey tfsSettings = dataR.OpenSubKey(TfsSettingsRegistrySubKeyName);

                if (tfsSettings != null && dataR != null && ata != null)
                {
                    teamProjectUri = (string)tfsSettings.GetValue(TeamProjectUriRegistrySubKeyName);
                    tfsSettings.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            { 
                //TODO: Add Exception Logging
            }
            return teamProjectUri;
        }

        /// <summary>
        /// Gets the name of the team project from registry.
        /// </summary>
        /// <returns>name of the team project</returns>
        public static string GetTeamProjectName()
        {
            string teamProjectName = String.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(MainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(DataRegistrySubKeyName);
                RegistryKey tfsSettings = dataR.OpenSubKey(TfsSettingsRegistrySubKeyName);

                if (tfsSettings != null && dataR != null && ata != null)
                {
                    teamProjectName = (string)tfsSettings.GetValue(TeamProjectNameRegistrySubKeyName);
                    tfsSettings.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            { 
                //TODO: Add Exception Logging
            }

            return teamProjectName;
        }

        /// <summary>
        /// Gets the project DLL path from registry.
        /// </summary>
        /// <returns>the project DLL path</returns>
        public static string GetProjectDllPath()
        {
            string projectDllPath = String.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(MainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(DataRegistrySubKeyName);
                RegistryKey associatedAutomation = dataR.OpenSubKey(AutomationAssociationRegistrySubKeyName);

                if (associatedAutomation != null && dataR != null && ata != null)
                {
                    projectDllPath = (string)associatedAutomation.GetValue(ProjectDllPathRegistrySubKeyName);
                    associatedAutomation.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                //TODO: Add Exception Logging
            }

            return projectDllPath;
        }

        /// <summary>
        /// Gets the test plan from registry.
        /// </summary>
        /// <returns>the test plan</returns>
        public static string GetTestPlan()
        {
            string testPlan = String.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(MainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(DataRegistrySubKeyName);
                RegistryKey tfsSettings = dataR.OpenSubKey(TfsSettingsRegistrySubKeyName);

                if (tfsSettings != null && dataR != null && ata != null)
                {
                    testPlan = (string)tfsSettings.GetValue(TestPlanRegistrySubKeyName);
                    tfsSettings.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                //TODO: Add Exception Logging
            }

            return testPlan;
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
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(MainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(DataRegistrySubKeyName);
                RegistryKey appereanceR = dataR.OpenSubKey(AppereanceRegistrySubKeyName);
                string colors = String.Empty;

                if (appereanceR != null && dataR != null && ata != null)
                {
                    colors = (string)appereanceR.GetValue(ColorRegistrySubKeyName);
                    appereanceR.Close();
                    dataR.Close();
                    ata.Close();
                }
                if (!String.IsNullOrEmpty(colors))
                {
                    colorsStr = colors.Split('&');
                }
            }
            catch
            {
                //TODO: Add Exception Logging
            }

            return colorsStr;
        }

        /// <summary>
        /// Gets the theme from registry.
        /// </summary>
        /// <returns>the theme</returns>
        public static string GetTheme()
        {
            string theme = String.Empty;
            try
            {
                RegistryKey ata = Registry.CurrentUser.OpenSubKey(MainRegistrySubKeyName);
                RegistryKey dataR = ata.OpenSubKey(DataRegistrySubKeyName);
                RegistryKey appereanceR = dataR.OpenSubKey(AppereanceRegistrySubKeyName);

                if (appereanceR != null && dataR != null && ata != null)
                {
                    theme = (string)appereanceR.GetValue(ThemeRegistrySubKeyName);
                    appereanceR.Close();
                    dataR.Close();
                    ata.Close();
                }
            }
            catch
            {
                //TODO: Add Exception Logging
            }

            return theme;
        }
    }
}
