// <copyright file="Suite.cs" company="Telerik">
// http://www.telerik.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FirstFloor.ModernUI.Presentation;

    /// <summary>
    /// Represents TreeView Suite Node Object
    /// </summary>
    public class Suite : NotifyPropertyChanged
    {
        /// <summary>
        /// The is node expanded
        /// </summary>
        private bool isNodeExpanded;

        /// <summary>
        /// The is selected
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="Suite"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="id">The unique identifier.</param>
        /// <param name="subSuites">The sub suites.</param>
        /// <param name="parent">The parent.</param>
        public Suite(string title, int id, ObservableCollection<Suite> subSuites, Suite parent = null)
        {
            this.Title = title;
            this.Id = id;
            this.SubSuites = subSuites;
            this.Parent = parent;
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Suite Parent { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the sub suites.
        /// </summary>
        /// <value>
        /// The sub suites.
        /// </value>
        public ObservableCollection<Suite> SubSuites { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is node expanded].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is node expanded]; otherwise, <c>false</c>.
        /// </value>
        public bool IsNodeExpanded
        {
            get
            {
                return this.isNodeExpanded;
            }

            set
            {
                this.isNodeExpanded = value;
                this.OnPropertyChanged("IsNodeExpanded");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is selected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is selected]; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.OnPropertyChanged("IsSelected");
            }
        }
    }
}
