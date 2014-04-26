// <copyright file="TestBase.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.BusinessLogic.Entities
{
    using System;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Enums;
    using TestCaseManagerCore.BusinessLogic.Managers;
    using AAngelov.Utilities.UI.Core;
    using AAngelov.Utilities.Managers;

    /// <summary>
    /// Contains Base Test Entities properties
    /// </summary>
    [Serializable]
    public class TestBase : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The owner
        /// </summary>
        [NonSerialized]
        private TeamFoundationIdentityName teamFoundationIdentityName;

        /// <summary>
        /// The unique identifier
        /// </summary>
        private int id;

        /// <summary>
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// The area
        /// </summary>
        private string area;        

        /// <summary>
        /// The priority
        /// </summary>
        private Priority priority;

        /// <summary>
        /// The is initialized
        /// </summary>
        protected bool isInitialized;

        /// <summary>
        /// Gets the display name of the owner.
        /// </summary>
        /// <value>
        /// The display name of the owner.
        /// </value>
        public string OwnerDisplayName { get; set; }

        /// <summary>
        /// Gets the team foundation unique identifier.
        /// </summary>
        /// <value>
        /// The team foundation unique identifier.
        /// </value>
        public Guid TeamFoundationId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

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
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(t => this.Title = t, this.title, "Change the test case title");
                }                
                this.title = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        public string Area
        {
            get
            {
                return this.area;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(a => this.Area = a, this.area, "Change the test case area");
                }               
                this.area = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public TeamFoundationIdentityName TeamFoundationIdentityName
        {
            get
            {
                return this.teamFoundationIdentityName;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(t => this.TeamFoundationIdentityName = t, this.teamFoundationIdentityName, "Change the test case owner");
                }                
                this.teamFoundationIdentityName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public Priority Priority
        {
            get
            {
                return this.priority;
            }

            set
            {
                if (this.isInitialized)
                {
                    UndoRedoManager.Instance().Push(p => this.Priority = p, this.priority, "Change the test case priority");
                }                
                this.priority = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
