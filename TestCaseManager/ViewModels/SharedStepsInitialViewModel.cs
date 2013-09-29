// <copyright file="SharedStepsInitialViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Forms;
    using System.Windows.Threading;
    using FirstFloor.ModernUI.Presentation;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerApp.BusinessLogic.Entities;
    using TestCaseManagerApp.BusinessLogic.Enums;

    /// <summary>
    /// Contains methods and properties related to the TestCasesInitial View
    /// </summary>
    public class SharedStepsInitialViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The test cases count after filtering
        /// </summary>
        private string sharedStepsCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialViewModel"/> class.
        /// </summary>
        public SharedStepsInitialViewModel()
        {
            this.InitialViewFilters = new InitialViewFilters();    
            
            this.ObservableSharedSteps = new ObservableCollection<SharedStep>();
            this.InitialTestCaseCollection = new ObservableCollection<SharedStep>();
            this.RefreshSharedSteps();
            this.InitializeInitialTestCaseCollection(this.ObservableSharedSteps);
            this.SharedStepsCount = this.ObservableSharedSteps.Count.ToString();
            this.IsAfterInitialize = true;
        }      

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">The old view model.</param>
        public SharedStepsInitialViewModel(SharedStepsInitialViewModel viewModel)
            : this()
        {
            this.InitialViewFilters = viewModel.InitialViewFilters;         
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is after initialize]. It will be true only right after the contructor is called. Is used to determine if the Initial filter Reset should be called.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is after initialize]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAfterInitialize { get; set; }

        /// <summary>
        /// Gets or sets the observable shared steps.
        /// </summary>
        /// <value>
        /// The observable shared steps.
        /// </value>
        public ObservableCollection<SharedStep> ObservableSharedSteps { get; set; }

        /// <summary>
        /// Gets or sets the initial test case collection.
        /// </summary>
        /// <value>
        /// The initial test case collection.
        /// </value>
        public ObservableCollection<SharedStep> InitialTestCaseCollection { get; set; }

        /// <summary>
        /// Gets or sets the initial view filters.
        /// </summary>
        /// <value>
        /// The initial view filters.
        /// </value>
        public InitialViewFilters InitialViewFilters { get; set; }

        /// <summary>
        /// Gets or sets the shared steps count.
        /// </summary>
        /// <value>
        /// The shared steps count.
        /// </value>
        public string SharedStepsCount
        {
            get
            {
                return this.sharedStepsCount;
            }

            set
            {
                this.sharedStepsCount = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Resets the initial filters. If its comming from another page the filter is saved but on next suite selected it will be reset.
        /// </summary>
        public void ResetInitialFilters()
        {
            if (this.IsAfterInitialize)
            {
                this.IsAfterInitialize = false;
            }
            else
            {
                this.InitialViewFilters.Reset();
            }
        }

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        public void FilterSharedSteps()
        {
            bool shouldSetIdFilter = InitialViewFilters.IsIdTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.IdFilter);
            string idFilter = this.InitialViewFilters.IdFilter;
            bool shouldSetTextFilter = InitialViewFilters.IsTitleTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.TitleFilter);
            string titleFilter = this.InitialViewFilters.TitleFilter.ToLower();
            bool shouldSetPriorityFilter = InitialViewFilters.IsPriorityTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.PriorityFilter);
            string priorityFilter = this.InitialViewFilters.PriorityFilter.ToLower();
            bool shouldSetAssignedToFilter = InitialViewFilters.IsAssignedToTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.AssignedToFilter);
            string assignedToFilter = this.InitialViewFilters.AssignedToFilter.ToLower();

            var filteredList = this.InitialTestCaseCollection.Where(t =>
                (shouldSetIdFilter ? (t.ISharedStep.Id.ToString().Contains(idFilter)) : true) &&
                (shouldSetTextFilter ? (t.Title.ToLower().Contains(titleFilter)) : true) &&
                (shouldSetPriorityFilter ? t.Priority.ToString().ToLower().Contains(priorityFilter) : true) &&
                (shouldSetAssignedToFilter ? t.TeamFoundationIdentityName.DisplayName.ToLower().Contains(assignedToFilter) : true)
                ).ToList();
            this.ObservableSharedSteps.Clear();
            filteredList.ForEach(x => this.ObservableSharedSteps.Add(x));

            this.SharedStepsCount = filteredList.Count.ToString();
        }

        /// <summary>
        /// Refreshes the shared steps
        /// </summary>
        public void RefreshSharedSteps()
        {
            this.ObservableSharedSteps.Clear();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<SharedStep> sharedStepsList = SharedStepManager.GetAllSharedStepsInTestPlan();
            sharedStepsList.ForEach(t => this.ObservableSharedSteps.Add(t));
        }

        /// <summary>
        /// Initializes the initial shared steps collection.
        /// </summary>
        /// <param name="sharedSteps">The shared steps.</param>
        public void InitializeInitialTestCaseCollection(ICollection<SharedStep> sharedSteps)
        {
            this.InitialTestCaseCollection.Clear();
            foreach (var currentTestCase in sharedSteps)
            {
                this.InitialTestCaseCollection.Add(currentTestCase);
            }
        }     
    }
}