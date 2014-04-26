// <copyright file="PrompCheckboxListDialogViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.ViewModels
{
    using AAngelov.Utilities.UI.Core;
    using AAngelov.Utilities.UI.Managers;
    using System.Collections.ObjectModel;
    using TestCaseManagerCore.BusinessLogic.Entities;
    
    /// <summary>
    /// Holds PrompCheckboxListDialogViewModel Properties
    /// </summary>
    public class PrompCheckboxListDialogViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The is canceled
        /// </summary>
        private bool isCanceled;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrompCheckboxListDialogViewModel"/> class.
        /// </summary>
        public PrompCheckboxListDialogViewModel()
        {
            this.PropertiesToBeExported = new ObservableCollection<CheckedItem>()
            {
                new CheckedItem("Suite"),
                new CheckedItem("Area"),
                new CheckedItem("Priority"),
                new CheckedItem("Automated"),
                new CheckedItem("AssignedTo"),
                new CheckedItem("Status"),
                new CheckedItem("Comment"),
                new CheckedItem("Steps"),
            };
            this.PreselectItemsFromRegistry();
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
                UIRegistryManager.Instance.WriteIsCanceledTitlePromtDialog(this.isCanceled);
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the properties automatic be exported.
        /// </summary>
        /// <value>
        /// The properties automatic be exported.
        /// </value>
        public ObservableCollection<CheckedItem> PropertiesToBeExported { get; set; }

        /// <summary>
        /// Preselects the items from registry.
        /// </summary>
        private void PreselectItemsFromRegistry()
        {
            string checkedProperties = UIRegistryManager.Instance.ReadCheckedPropertiesToBeExported();
            if (checkedProperties != null)
            {
                foreach (var currentCheckedItem in this.PropertiesToBeExported)
                {
                    if (!checkedProperties.Contains(currentCheckedItem.Description))
                    {
                        currentCheckedItem.Selected = false;
                    }
                    else
                    {
                        currentCheckedItem.Selected = true;
                    }
                }
            }           
        }
    }
}