// <copyright file="PrompCheckboxListDialogControl.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using AAngelov.Utilities.UI.Managers;
using TestCaseManagerCore.ViewModels;
using AAngelov.Utilities.UI.Managers;
using TestCaseManagerCore.BusinessLogic.Managers;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the shared step name prompt dialog
    /// </summary>
    public partial class PrompCheckboxListDialogControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrompCheckboxListDialogControl"/> class.
        /// </summary>
        public PrompCheckboxListDialogControl()
        {
            this.InitializeComponent();            
        }

        /// <summary>
        /// Gets or sets the prompt dialog view model.
        /// </summary>
        /// <value>
        /// The prompt dialog view model.
        /// </value>
        public PrompCheckboxListDialogViewModel PrompCheckboxListDialogViewModel { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(true);
            UIRegistryManager.Instance.WriteIsCheckboxDialogSubmitted(false);
            PrompCheckboxListDialogViewModel = new PrompCheckboxListDialogViewModel();
            this.DataContext = this.PrompCheckboxListDialogViewModel;         
        }

        /// <summary>
        /// Handles the Click event of the ButtonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            PrompCheckboxListDialogViewModel.IsCanceled = false;
            List<String> checkedProperties = PrompCheckboxListDialogViewModel.PropertiesToBeExported.ToList();
            string checkedPropertiesStr = string.Empty;
            if (checkedProperties.Count != 0)
            {
                checkedPropertiesStr = checkedProperties.Aggregate((i, j) => i + " " + j);
            }
            UIRegistryManager.Instance.WriteCheckedPropertiesToBeExported(checkedPropertiesStr);
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(false);
            UIRegistryManager.Instance.WriteIsCheckboxDialogSubmitted(true);
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
            PrompCheckboxListDialogViewModel.IsCanceled = true;
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(false);
            Window window = Window.GetWindow(this);
            window.Close();
        }  
    }
}
