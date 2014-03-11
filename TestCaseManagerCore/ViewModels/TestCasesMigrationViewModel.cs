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
	using System.Windows.Controls;
	using System.Linq;
	using FirstFloor.ModernUI.Windows.Controls;
	using log4net;
	using Microsoft.TeamFoundation.Client;
	using Microsoft.TeamFoundation.TestManagement.Client;
	using TestCaseManagerCore.BusinessLogic.Entities;
	using TestCaseManagerCore.BusinessLogic.Managers;
	using System.Collections.Concurrent;
	using System.Threading.Tasks;
	using System.Threading;
	using System.IO;

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

		/// <summary>
		/// The source full team project name
		/// </summary>
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
		/// The default json folder
		/// </summary>
		private string defaultJsonFolder;

		/// <summary>
		/// The migration shared steps retry json path
		/// </summary>
		private string migrationSharedStepsRetryJsonPath;

		/// <summary>
		/// The migration suites retry json path
		/// </summary>
		private string migrationSuitesRetryJsonPath;

		/// <summary>
		/// The migration test cases retry json path
		/// </summary>
		private string migrationTestCasesRetryJsonPath;

		/// <summary>
		/// The migration add test cases to suites retry json path
		/// </summary>
		private string migrationAddTestCasesToSuitesRetryJsonPath;

		/// <summary>
		/// The is shared steps migration finished
		/// </summary>
		private bool isSharedStepsMigrationFinished;

		/// <summary>
		/// The is suites migration finished
		/// </summary>
		private bool isSuitesMigrationFinished;

		/// <summary>
		/// The is test cases migration finished
		/// </summary>
		private bool isTestCasesMigrationFinished;

		/// <summary>
		/// The shared steps migration log manager
		/// </summary>
		private MigrationLogManager sharedStepsMigrationLogManager;

		/// <summary>
		/// The suites migration log manager
		/// </summary>
		private MigrationLogManager suitesMigrationLogManager;

		/// <summary>
		/// The test cases migration log manager
		/// </summary>
		private MigrationLogManager testCasesMigrationLogManager;

		/// <summary>
		/// The test cases add to suites migration log manager
		/// </summary>
		private MigrationLogManager testCasesAddToSuitesMigrationLogManager;

		/// <summary>
		/// Gets or sets the cancellation token source.
		/// </summary>
		/// <value>
		/// The cancellation token source.
		/// </value>
		private CancellationTokenSource loggingCancellationTokenSource;

		/// <summary>
		/// Gets or sets the cancellation token.
		/// </summary>
		/// <value>
		/// The cancellation token.
		/// </value>
		private CancellationToken loggingCancellationToken;

		/// <summary>
		/// Gets or sets the cancellation token source.
		/// </summary>
		/// <value>
		/// The cancellation token source.
		/// </value>
		private CancellationTokenSource executionCancellationTokenSource;

		/// <summary>
		/// Gets or sets the cancellation token.
		/// </summary>
		/// <value>
		/// The cancellation token.
		/// </value>
		private CancellationToken executionCancellationToken;

		/// <summary>
		/// Gets or sets the default json folder.
		/// </summary>
		/// <value>
		/// The default json folder.
		/// </value>
		public string DefaultJsonFolder
		{
			get
			{
				return this.defaultJsonFolder;
			}

			set
			{
				this.defaultJsonFolder = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the migration shared steps retry json path.
		/// </summary>
		/// <value>
		/// The migration shared steps retry json path.
		/// </value>
		public string MigrationSharedStepsRetryJsonPath
		{
			get
			{
				return this.migrationSharedStepsRetryJsonPath;
			}

			set
			{
				this.migrationSharedStepsRetryJsonPath = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the migration suites retry json path.
		/// </summary>
		/// <value>
		/// The migration suites retry json path.
		/// </value>
		public string MigrationSuitesRetryJsonPath
		{
			get
			{
				return this.migrationSuitesRetryJsonPath;
			}

			set
			{
				this.migrationSuitesRetryJsonPath = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the migration test cases retry json path.
		/// </summary>
		/// <value>
		/// The migration test cases retry json path.
		/// </value>
		public string MigrationTestCasesRetryJsonPath
		{
			get
			{
				return this.migrationTestCasesRetryJsonPath;
			}

			set
			{
				this.migrationTestCasesRetryJsonPath = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the migration add test cases to suites retry json path.
		/// </summary>
		/// <value>
		/// The migration add test cases to suites retry json path.
		/// </value>
		public string MigrationAddTestCasesToSuitesRetryJsonPath
		{
			get
			{
				return this.migrationAddTestCasesToSuitesRetryJsonPath;
			}

			set
			{
				this.migrationAddTestCasesToSuitesRetryJsonPath = value;
				this.NotifyPropertyChanged();
			}
		}

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
		/// Gets or sets the is shared steps migration finished.
		/// </summary>
		/// <value>
		/// The is shared steps migration finished.
		/// </value>
		public bool IsSharedStepsMigrationFinished
		{
			get
			{
				return this.isSharedStepsMigrationFinished;
			}

			set
			{
				this.isSharedStepsMigrationFinished = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the is suites migration finished.
		/// </summary>
		/// <value>
		/// The is suites migration finished.
		/// </value>
		public bool IsSuitesMigrationFinished
		{
			get
			{
				return this.isSuitesMigrationFinished;
			}

			set
			{
				this.isSuitesMigrationFinished = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the is test cases migration finished.
		/// </summary>
		/// <value>
		/// The is test cases migration finished.
		/// </value>
		public bool IsTestCasesMigrationFinished
		{
			get
			{
				return this.isTestCasesMigrationFinished;
			}

			set
			{
				this.isTestCasesMigrationFinished = value;
				this.NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the progress queue.
		/// </summary>
		/// <value>
		/// The progress queue.
		/// </value>
		public ConcurrentQueue<string> ProgressConcurrentQueue { get; set; }

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
			this.testCasesMapping = new Dictionary<int, int>();
			this.StatusLogQueue = new ConcurrentQueue<string>();
			this.ProgressConcurrentQueue = new ConcurrentQueue<string>();
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
		public void StartSharedStepsFromSourceToDestinationMigration()
		{
			this.executionCancellationTokenSource= new CancellationTokenSource();
			this.executionCancellationToken = this.executionCancellationTokenSource.Token;

			Task t = Task.Factory.StartNew(() =>
			{
				this.MigrateSharedStepsFromSourceToDestinationInternal();
			}, this.executionCancellationToken);
			t.ContinueWith(antecedent =>
			{
				this.StopUiProgressLogging();
			});
		}

		/// <summary>
		/// Stops the shared steps from source to destination migration.
		/// </summary>
		public void StopSharedStepsFromSourceToDestinationMigration()
		{
			log.Info("Stop Shared Steps Migration!");
			if (this.executionCancellationTokenSource != null)
			{
				this.executionCancellationTokenSource.Cancel();
				log.Info("Shared Steps Migration STOPPED!");
			}
		}

		/// <summary>
		/// Migrates the shared steps from source to destination.
		/// </summary>
		private void MigrateSharedStepsFromSourceToDestinationInternal()
		{
			if (!string.IsNullOrEmpty(this.MigrationSharedStepsRetryJsonPath) && File.Exists(this.MigrationSharedStepsRetryJsonPath))
			{
				this.sharedStepsMigrationLogManager = new MigrationLogManager(this.MigrationSharedStepsRetryJsonPath);
				this.sharedStepsMigrationLogManager.LoadCollectionFromExistingFile();
			}
			else
			{
				this.sharedStepsMigrationLogManager = new MigrationLogManager("sharedSteps", this.DefaultJsonFolder);
			}

			List<SharedStep> sourceSharedSteps = SharedStepManager.GetAllSharedStepsInTestPlan(this.sourceTeamProject);
			foreach (SharedStep currentSourceSharedStep in sourceSharedSteps)
			{
				if (this.executionCancellationToken.IsCancellationRequested)
				{
					break;
				}
				string infoMessage = String.Empty;
				try
				{
					infoMessage = String.Format("Start Migrating Shared Step with Source Id= {0}", currentSourceSharedStep.Id);
					log.Info(infoMessage);
					this.ProgressConcurrentQueue.Enqueue(infoMessage);

					List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(currentSourceSharedStep.ISharedStep.Actions);
					SharedStep newSharedStep = currentSourceSharedStep.Save(this.destinationTeamProject, true, testSteps, false);
					newSharedStep.ISharedStep.Refresh();
					this.sharedStepsMapping.Add(currentSourceSharedStep.ISharedStep.Id, newSharedStep.ISharedStep.Id);

					this.sharedStepsMigrationLogManager.Log(currentSourceSharedStep.Id, newSharedStep.Id, true);
					infoMessage = String.Format("Shared Step Migrated SUCCESSFULLY: Source Id= {0}, Destination Id= {1}", currentSourceSharedStep.Id, newSharedStep.Id);
					log.Info(infoMessage);
					this.ProgressConcurrentQueue.Enqueue(infoMessage);
				}
				catch (Exception ex)
				{
					this.sharedStepsMigrationLogManager.Log(currentSourceSharedStep.Id, -1, false, ex.Message);
					log.Error(ex);
					this.ProgressConcurrentQueue.Enqueue(ex.Message);
				}
				finally
				{
					this.sharedStepsMigrationLogManager.Save();
					this.MigrationSharedStepsRetryJsonPath = this.sharedStepsMigrationLogManager.FullResultFilePath;
				}
			}
			this.isSharedStepsMigrationFinished = true;
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

		/// <summary>
		/// Starts the UI progress logging.
		/// </summary>
		/// <param name="progressLabel">The progress label.</param>
		public void StartUiProgressLogging(Label progressLabel)
		{
			log.Info("Start UI Progress logging!");
			progressLabel.IsEnabled = true;
			this.loggingCancellationTokenSource = new CancellationTokenSource();
			this.loggingCancellationToken = this.loggingCancellationTokenSource.Token;
			this.LogProgressInternal(this.ProgressConcurrentQueue, progressLabel);
		}


		/// <summary>
		/// Stops the UI progress logging.
		/// </summary>
		public void StopUiProgressLogging()
		{
			log.Info("Stop UI Progress logging!");		
			if (this.loggingCancellationTokenSource != null)
			{
				this.loggingCancellationTokenSource.Cancel();
				log.Info("UI Progress logging STOPPED!");
			}
		}

		/// <summary>
		/// Determines whether this instance [can start migration].
		/// </summary>
		/// <returns>
		///   <c>true</c> if this instance [can start migration]; otherwise, <c>false</c>.
		/// </returns>
		public bool CanStartMigration()
		{
			bool canStartMigration = true;

			if (this.destinationFullTeamProjectName == null || this.sourceFullTeamProjectName == null )
			{
				// TODO: Add Moder Dialog Message Box Validations
				canStartMigration = false;
			}
			if (string.IsNullOrEmpty(this.SelectedSourceTestPlan) || string.IsNullOrEmpty(this.SelectedDestinationTestPlan))
			{
				canStartMigration = false;
			}
			if (string.IsNullOrEmpty(this.DefaultJsonFolder))
			{
				canStartMigration = false;
			}

			return canStartMigration;
		}

		/// <summary>
		/// Logs the execution.
		/// </summary>
		/// <param name="queue">The queue.</param>
		/// <param name="progressLabel">The progress label.</param>
		private void LogProgressInternal(ConcurrentQueue<string> queue, Label progressLabel)
		{
			Task loggingTask = Task.Factory.StartNew((a) =>
			{
				do
				{
					if (this.loggingCancellationToken.IsCancellationRequested)
					{
						break;
					}
					string currentMessage = String.Empty;
					bool isLoggingMessageDequeued = queue.TryDequeue(out currentMessage);

					if (isLoggingMessageDequeued)
					{
						progressLabel.Dispatcher.InvokeAsync((Action)(() =>
						{
							try
							{
								progressLabel.Content = String.Format("\n{0}", currentMessage);
							}
							catch
							{
							}
						}), System.Windows.Threading.DispatcherPriority.Loaded);
					}
				}
				while (true);
			}, this.loggingCancellationToken);
		}
    }
}
