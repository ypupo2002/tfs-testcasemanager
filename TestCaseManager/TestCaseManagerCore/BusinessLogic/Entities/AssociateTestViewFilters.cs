﻿// <copyright file="AssociateTestViewFilters.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

using AAngelov.Utilities.UI.Core;

namespace TestCaseManagerCore.BusinessLogic.Entities
{
    /// <summary>
    /// Contains search filters for the associate automation view
    /// </summary>
    public class AssociateTestViewFilters : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The full name default text
        /// </summary>
        public const string FullNameDefaultText = "Full Name";

        /// <summary>
        /// The class name default text
        /// </summary>
        public const string ClassNameDefaultText = "Class Name";

        /// <summary>
        /// The is full name filter set
        /// </summary>
        public bool IsFullNameFilterSet;

        /// <summary>
        /// The is class name filter
        /// </summary>
        public bool IsClassNameFilterSet;

        /// <summary>
        /// The full name filter
        /// </summary>
        private string fullNameFilter;

        /// <summary>
        /// The class name filter
        /// </summary>
        private string classNameFilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociateTestViewFilters"/> class.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="className">Name of the class.</param>
        public AssociateTestViewFilters(string fullName, string className)
        {
            this.FullNameFilter = fullName;
            this.ClassNameFilter = className;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociateTestViewFilters"/> class.
        /// </summary>
        public AssociateTestViewFilters()
        {
            this.FullNameFilter = FullNameDefaultText;
            this.ClassNameFilter = ClassNameDefaultText; 
        }

        /// <summary>
        /// Gets or sets the full name filter.
        /// </summary>
        /// <value>
        /// The full name filter.
        /// </value>
        public string FullNameFilter
        {
            get
            {
                return this.fullNameFilter;
            }

            set
            {
                this.fullNameFilter = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the class name filter.
        /// </summary>
        /// <value>
        /// The class name filter.
        /// </value>
        public string ClassNameFilter
        {
            get
            {
                return this.classNameFilter;
            }

            set
            {
                this.classNameFilter = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}