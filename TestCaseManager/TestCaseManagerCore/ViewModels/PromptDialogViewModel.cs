// <copyright file="PromptDialogViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using FirstFloor.ModernUI.Presentation;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;
    
    /// <summary>
    /// Holds PromptDialogView Properties
    /// </summary>
    public class PromptDialogViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The content
        /// </summary>
        private string content;

        /// <summary>
        /// The is canceled
        /// </summary>
        private bool isCanceled;

        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        /// <value>
        /// The Content.
        /// </value>
        public string Content
        {
            get
            {
                if (this.content == null)
                {
                    this.content = RegistryManager.GetContentPromtDialog();
                }

                return this.content;
            }

            set
            {
               
                this.content = value;
                RegistryManager.WriteTitleTitlePromtDialog(this.content);
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is canceled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is canceled]; otherwise, <c>false</c>.
        /// </value>
        public bool IsCanceled
        {
            get
            {
                return this.isCanceled;
            }

            set
            {
                this.isCanceled = value;
                RegistryManager.WriteIsCanceledTitlePromtDialog(this.isCanceled);
                this.NotifyPropertyChanged();
            }
        }
    }
}