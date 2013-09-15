using System;
using System.Windows;
using System.Windows.Controls;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            this.DataContext = ExecutionContext.SettingsViewModel;
            tbAssociatedProjectDllPath.Text = ExecutionContext.ProjectDllPath;
        }

        /// <summary>
        /// Handles the Click event of the btnBrowse control. Opens file dialog with dll filter.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".dll";
            dlg.Filter = "Assembly Files (*.dll)|*.dll";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                tbAssociatedProjectDllPath.Text = filename;
                RegistryManager.WriteCurrentProjectDllPath(filename);
                ExecutionContext.ProjectDllPath = filename;
            }
        }
    }
}
