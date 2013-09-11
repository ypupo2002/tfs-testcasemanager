// <copyright file="RegistryManager.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp
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
        /// The TFS settings registry sub key name- main sub key for team project URI and team project name
        /// </summary>
        private static string tfsSettingsRegistrySubKeyName = "TfsSettings";

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
    }
}
