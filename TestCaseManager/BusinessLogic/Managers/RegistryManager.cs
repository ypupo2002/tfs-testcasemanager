using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TestCaseManagerApp
{
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
            catch { }
            return teamProjectUri;
        }

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
            catch { }
            return teamProjectName;
        }

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
            catch { }
            return projectDllPath;
        }

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
            catch { }
            return testPlan;
        }

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
                    colorsStr = colors.Split('&');
            }
            catch { }
            return colorsStr;
        }

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
            catch { }
            return theme;
        }
    }
}
