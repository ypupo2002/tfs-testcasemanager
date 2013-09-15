// <copyright file="PrompDialogUserControl.xaml.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows;
using TestCaseManagerApp.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the shared step name prompt dialog
    /// </summary>
    public partial class PrompDialogUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrompDialogUserControl"/> class.
        /// </summary>
        public PrompDialogUserControl()
        {
            this.InitializeComponent();            
        }

        /// <summary>
        /// Handles the Click event of the ButtonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            ExecutionContext.SharedStepTitle = tbSharedStepTitle.Text;
            Window window = Window.GetWindow(this);
            window.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ExecutionContext.SharedStepTitle = string.Empty;
            ExecutionContext.SharedStepTitleDialogCancelled = true;
            Window window = Window.GetWindow(this);
            window.Close();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbSharedStepTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbSharedStepTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSharedStepTitle.Text))
            {
                btnOk.IsEnabled = true;
            }
            else
            {
                btnOk.IsEnabled = false;
            }
        }
    }
}
