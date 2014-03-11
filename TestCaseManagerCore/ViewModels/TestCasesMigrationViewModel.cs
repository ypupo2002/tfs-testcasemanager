// <copyright file="TestCasesMigrationViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TestCaseManagerCore.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Net;
	using System.Net.Sockets;
	using System.Windows;
	using System.Windows.Forms;
	using System.Linq;
	using FirstFloor.ModernUI.Windows.Controls;
	using log4net;
	using Microsoft.TeamFoundation.Client;
	using Microsoft.TeamFoundation.TestManagement.Client;
	using TestCaseManagerCore.BusinessLogic.Entities;
	using TestCaseManagerCore.BusinessLogic.Managers;
using System.Collections.Concurrent;

    /// <summary>
    /// Provides methods and properties related to the Migration View
    /// </summary>
	public class TestCasesMigrationViewModel : BaseProjectSelectionViewModel
    {
        /// <summary>
        /// The log
        /// </summary>
       private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// The source preferences
		/// </summary>
		private Preferences sourcePreferences;

		/// <summary>
		/// The source TFS team project collection
		/// </summary>
		private TfsTeamProjectCollection sourceTfsTeamProjectCollection;

		/// <summary>
		/// The source team project
		/// </summary>
		private ITestManagementTeamProject sourceTeamProject;

		/// <summary>
		/// The destination preferences
		/// </summary>
		private Preferences destinationPreferences;

		/// <summary>
		/// The destination TFS team project collection
		/// </summary>
		private TfsTeamProjectCollection destinationTfsTeamProjectCollection;

		/// <summary>
		/// The destination team project
		/// </summary>
		private ITestManagementTeamProject destinationTeamProject;

		private string sourceFullTeamProjectName;

		/// <summary>
		/// The destination full team project name
		/// </summary>
		private string destinationFullTeamProjectName;

		/// <summary>
		/// The suites mapping
		/// </summary>
		private Dictionary<int, int> suitesMapping;

		/// <summary>
		/// The shared steps mapping
		/// </summary>
		private Dictionary<int, int> sharedStepsMapping;

		/// <summary>
		/// The test cases mapping
		/// </summary>
		private Dictionary<int, int> testCasesMapping;

		/// <summary>
		/// Gets or sets the name of the source full team project.
		/// </summary>
		/// <value>
		/// The name of the source full team project.
		/// </value>
		public string SourceFullTeamProjectName
		{
			get
			{
				return this.sourceFullTeamProjectName;
			}

			set
			{
				this.sourceFullTeamProjectName = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the name of the destination full team project.
		/// </summary>
		/// <value>
		/// The name of the destination full team project.
		/// </value>
		public string DestinationFullTeamProjectName
		{
			get
			{
				return this.destinationFullTeamProjectName;
			}

			set
			{

				this.destinationFullTeamProjectName = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TestCasesMigrationViewModel"/> class.
		/// </summary>
		public TestCasesMigrationViewModel()
        {
            this.ObservableSourceTestPlans = new ObservableCollection<string>();
			this.ObservableDestinationTestPlans = new ObservableCollection<string>();
			this.ObservableSuitesToBeSkipped = new ObservableCollection<TextReplacePair>();
			this.ObservableSuitesToBeSkipped.Add(new TextReplacePair());
			this.suitesMapping = new Dictionary<int, int>();
			this.sharedStepsMapping = new Dictionary<int, int>();
			this.StatusLogQueue = new ConcurrentQueue<string>();
        }

        /// <summary>
        /// Gets or sets the test service.
        /// </summary>
        /// <value>
        /// The test service.
        /// </value>
        public ITestManagementService SourceTestService { get; set; }

		/// <summary>
		/// Gets or sets the destination test service.
		/// </summary>
		/// <value>
		/// The destination test service.
		/// </value>
		public ITestManagementService DestinationTestService { get; set; }

		/// <summary>
		/// Gets or sets the observable source test plans.
		/// </summary>
		/// <value>
		/// The observable source test plans.
		/// </value>
        public ObservableCollection<string> ObservableSourceTestPlans { get; set; }

		/// <summary>
		/// Gets or sets the observable destination test plans.
		/// </summary>
		/// <value>
		/// The observable destination test plans.
		/// </value>
		public ObservableCollection<string> ObservableDestinationTestPlans { get; set; }

		/// <summary>
		/// Gets or sets the observable suites to be skipped.
		/// </summary>
		/// <value>
		/// The observable suites to be skipped.
		/// </value>
		public ObservableCollection<TextReplacePair> ObservableSuitesToBeSkipped { get; set; }

		/// <summary>
		/// Gets or sets the selected source test plan.
		/// </summary>
		/// <value>
		/// The selected source test plan.
		/// </value>
        public string SelectedSourceTestPlan { get; set; }

		/// <summary>
		/// Gets or sets the selected destination test plan.
		/// </summary>
		/// <value>
		/// The selected destination test plan.
		/// </value>
		public string SelectedDestinationTestPlan { get; set; }

		/// <summary>
		/// Gets or sets the status log queue.
		/// </summary>
		/// <value>
		/// The status log queue.
		/// </value>
		public ConcurrentQueue<string> StatusLogQueue { get; set; }

		/// <summary>
		/// Loads the project settings from user decision source.
		/// </summary>
		/// <param name="projectPicker">The project picker.</param>
		public void LoadProjectSettingsFromUserDecisionSource(TeamProjectPicker projectPicker)
		{
			base.LoadProjectSettingsFromUserDecision(projectPicker, ref this.sourceTfsTeamProjectCollection, ref this.sourceTeamProject, ref this.sourcePreferences, this.SourceTestService, this.SelectedSourceTestPlan, false);
			this.SourceFullTeamProjectName = base.GenerateFullTeamProjectName(this.sourcePreferences.TfsUri.ToString(), this.sourcePreferences.TestProjectName);
			this.sourcePreferences.TestPlan = TestPlanManager.GetTestPlanByName(this.sourceTeamProject, this.SelectedSourceTestPlan);
			base.InitializeTestPlans(this.sourceTeamProject, this.ObservableSourceTestPlans);
		}

		/// <summary>
		/// Loads the project settings from user decision destination.
		/// </summary>
		/// <param name="projectPicker">The project picker.</param>
		public void LoadProjectSettingsFromUserDecisionDestination(TeamProjectPicker projectPicker)
		{
			base.LoadProjectSettingsFromUserDecision(projectPicker, ref this.destinationTfsTeamProjectCollection, ref this.destinationTeamProject, ref this.destinationPreferences, this.DestinationTestService, this.SelectedDestinationTestPlan, false);
			this.DestinationFullTeamProjectName = base.GenerateFullTeamProjectName(this.destinationPreferences.TfsUri.ToString(), this.destinationPreferences.TestProjectName);
			this.destinationPreferences.TestPlan = TestPlanManager.GetTestPlanByName(this.destinationTeamProject, this.SelectedDestinationTestPlan);
			base.InitializeTestPlans(this.destinationTeamProject, this.ObservableDestinationTestPlans);
		}

		/// <summary>
		/// Migrates the shared steps from source to destination.
		/// </summary>
		public void MigrateSharedStepsFromSourceToDestination()
		{
			List<SharedStep> sourceSharedSteps = SharedStepManager.GetAllSharedStepsInTestPlan(this.sourceTeamProject);
			foreach (SharedStep currentSourceSharedStep in sourceSharedSteps)
			{
				 List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(currentSourceSharedStep.ISharedStep.Actions);
				 SharedStep newSharedStep = currentSourceSharedStep.Save(this.destinationTeamProject, true, testSteps);
				 newSharedStep.ISharedStep.Refresh();
				 suitesMapping.Add(currentSourceSharedStep.ISharedStep.Id, newSharedStep.ISharedStep.Id);
			}
		}

		/// <summary>
		/// Migrates the suites from source to destination.
		/// </summary>
		public void MigrateSuitesFromSourceToDestination(ITestSuiteCollection subSuitesCore, int parentId)
		{
			//List<Suite> sourceSuites = TestSuiteManager.GetAllSuites(ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites).ToList();			
		
			if (subSuitesCore == null || subSuitesCore.Count == 0)
			{
				return;
			}
			foreach (ITestSuiteBase currentSuite in subSuitesCore)
			{
				if (currentSuite != null)
				{
					currentSuite.Refresh();

					bool canBeAddedNewSuite;
					int newSuiteId = TestSuiteManager.AddChildSuite(this.destinationTeamProject, this.destinationPreferences.TestPlan, parentId, currentSuite.Title, out canBeAddedNewSuite);
					if (newSuiteId != 0)
					{
						suitesMapping.Add(currentSuite.Id, newSuiteId);
					}

					if (!(currentSuite is IRequirementTestSuite))
					{
                        IStaticTestSuite suite = currentSuite as IStaticTestSuite;
						MigrateSuitesFromSourceToDestination(suite.SubSuites, newSuiteId);
					}
				}
			}
		}

		/// <summary>
		/// Migrates the test cases from source to destination.
		/// </summary>
		public void MigrateTestCasesFromSourceToDestination()
		{
			ITestPlan sourceTestPlan = TestPlanManager.GetTestPlanByName(this.sourceTeamProject, this.SelectedSourceTestPlan);
			List<TestCase> sourceTestCases = TestCaseManager.GetAllTestCasesInTestPlan(this.sourceTeamProject, sourceTestPlan, false);
			foreach (TestCase currentSourceTestCase in sourceTestCases)
			{
				//Don't migrate the test case if its suite is in the exclusion list
				if (currentSourceTestCase.ITestSuiteBase != null && this.ObservableSuitesToBeSkipped.Count(t => t.NewText.Equals(currentSourceTestCase.ITestSuiteBase.Title)) > 0)
				{
					continue;
				}
				List<TestStep> currentSourceTestCaseTestSteps = TestStepManager.GetTestStepsFromTestActions(currentSourceTestCase.ITestCase.Actions);
				bool shouldCreateTestCase = true;
				foreach (TestStep currentTestStep in currentSourceTestCaseTestSteps)
				{
					if (currentTestStep.IsShared)
					{
						//If the test step is shared we change the current shared step id with the newly created shared step in the destination team project
						if (sharedStepsMapping.ContainsKey(currentTestStep.SharedStepId))
						{
							currentTestStep.SharedStepId = sharedStepsMapping[currentTestStep.SharedStepId];
						}
						else
						{
							// Don't save if the required shared steps are missing
							shouldCreateTestCase = false;
						}
					}
				}
				if (shouldCreateTestCase)
				{
					TestCase newTestCase = currentSourceTestCase.Save(this.destinationTeamProject, true, null, currentSourceTestCaseTestSteps);
					testCasesMapping.Add(currentSourceTestCase.Id, newTestCase.Id);
				}
			}
		}

		/// <summary>
		/// Adds the new test cases to new suites destination.
		/// </summary>
		public void AddNewTestCasesToNewSuitesDestination()
		{
			ITestPlan destinationTestPlan = TestPlanManager.GetTestPlanByName(this.destinationTeamProject, this.SelectedDestinationTestPlan);
			List<TestCase> destinationTestCases = TestCaseManager.GetAllTestCasesInTestPlan(this.destinationTeamProject, destinationTestPlan, false);
			ITestPlan sourceTestPlan = TestPlanManager.GetTestPlanByName(this.sourceTeamProject, this.SelectedSourceTestPlan);
			List<TestCase> sourceTestCases = TestCaseManager.GetAllTestCasesInTestPlan(this.sourceTeamProject, sourceTestPlan, false);

			foreach (TestCase currentSourceTestCase in sourceTestCases)
			{
				if (currentSourceTestCase.ITestSuiteBase == null)
				{
					continue;
				}
				else
				{
					int sourceParentSuiteId = currentSourceTestCase.ITestSuiteBase.Id;
					ITestSuiteBase destinationSuite = this.destinationTeamProject.TestSuites.Find(sourceParentSuiteId);
					if (testCasesMapping.ContainsKey(currentSourceTestCase.Id))
					{
						TestCase currentDestinationTestCase = destinationTestCases.FirstOrDefault(t => t.Id.Equals(testCasesMapping[currentSourceTestCase.Id]));
						destinationSuite.AddTestCase(currentDestinationTestCase.ITestCase);
					}				
				}
			}
		}
    }
}
