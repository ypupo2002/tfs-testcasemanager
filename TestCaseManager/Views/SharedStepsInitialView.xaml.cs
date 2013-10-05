// <copyright file="SharedStepsInitialView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.ViewModels;
using TestCaseManagerCore;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case initial(search mode) page
    /// </summary>
    public partial class SharedStepsInitialView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// The edit command
        /// </summary>
        public static RoutedCommand EditCommand = new RoutedCommand();

        /// <summary>
        /// The duplicate command
        /// </summary>
        public static RoutedCommand DuplicateCommand = new RoutedCommand();

        /// <summary>
        /// The preview command
        /// </summary>
        public static RoutedCommand PreviewCommand = new RoutedCommand();

        /// <summary>
        /// The new command
        /// </summary>
        public static RoutedCommand NewCommand = new RoutedCommand();

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialView"/> class.
        /// </summary>
        public SharedStepsInitialView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the shared steps initial view model.
        /// </summary>
        /// <value>
        /// The shared steps initial view model.
        /// </value>
        public SharedStepsInitialViewModel SharedStepsInitialViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when a this instance becomes the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            isInitialized = false;
        }

        /// <summary>
        /// Called just before this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        /// <remarks>
        /// The method is also invoked when parent frames are about to navigate.
        /// </remarks>
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        /// <summary>
        /// Handles the Loaded event of the TestCaseInitialView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TestCaseInitialView_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized)
            {
                return;
            }
            this.ShowProgressBar();
            this.InitializeFastKeys();
            Task t = Task.Factory.StartNew(() =>
            {
                if (this.SharedStepsInitialViewModel != null)
                {
                    this.SharedStepsInitialViewModel = new TestCaseManagerCore.ViewModels.SharedStepsInitialViewModel(this.SharedStepsInitialViewModel);
                }
                else
                {
                    SharedStepsInitialViewModel = new TestCaseManagerCore.ViewModels.SharedStepsInitialViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = SharedStepsInitialViewModel;
                this.UpdateButtonsStatus();
                this.HideProgressBar();
                this.tbTitleFilter.Focus();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Initializes the fast keys.
        /// </summary>
        private void InitializeFastKeys()
        {
            EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            DuplicateCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));
            PreviewCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            NewCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Alt));
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        private void HideProgressBar()
        {
            progressBar.Visibility = System.Windows.Visibility.Hidden;
            mainGrid.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Shows the progress bar.
        /// </summary>
        private void ShowProgressBar()
        {
            progressBar.Visibility = System.Windows.Visibility.Visible;
            mainGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Validates the shared steps selection.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ValidateSharedStepsSelection(Action action)
        {
            if (dgSharedSteps.SelectedIndex != -1)
            {
                action.Invoke();
            }
            else
            {
                this.DisplayNonSelectionWarning();
            }
        }

        /// <summary>
        /// Displays the non selection warning.
        /// </summary>
        private void DisplayNonSelectionWarning()
        {
            ModernDialog.ShowMessage("No selected test case.", "Warning", MessageBoxButton.OK);
        }

        /// <summary>
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateSharedStepsSelection(() =>
            {
                SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
                this.NavigateToTestCasesEditView(true, currentSharedStep.ISharedStep.Id);
            });
        }

        /// <summary>
        /// Handles the Click event of the DuplicateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDuplicate_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateSharedStepsSelection(() =>
            {
                SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
                this.NavigateToTestCasesEditView(true, currentSharedStep.ISharedStep.Id, true, true);
            });
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            this.NavigateToTestCasesEditView(true, true, false);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.ClearDefaultContent(ref SharedStepsInitialViewModel.InitialViewFilters.IsIdTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.ClearDefaultContent(ref SharedStepsInitialViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFilters.DetaultId, ref this.SharedStepsInitialViewModel.InitialViewFilters.IsIdTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFilters.DetaultTitle, ref this.SharedStepsInitialViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbAssignedToFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFilters.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbAssignedToFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFilters.DetaultAssignedTo, ref this.SharedStepsInitialViewModel.InitialViewFilters.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPriorityFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFilters.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbPriorityFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFilters.DetaultPriority, ref this.SharedStepsInitialViewModel.InitialViewFilters.IsPriorityTextSet);

        }

        /// <summary>
        /// Handles the KeyUp event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }    

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            this.NavigateToTestCasesEditView(true, currentSharedStep.ISharedStep.Id);
        }

        /// <summary>
        /// Handles the KeyDown event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
                this.NavigateToTestCasesEditView(true, currentSharedStep.ISharedStep.Id);
            }
        }       

        /// <summary>
        /// Handles the SelectedCellsChanged event of the dgTestCases control. Disable Preview and Duplicate buttons if more than one row is selected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            this.UpdateButtonsStatus();
        }

        /// <summary>
        /// Updates the buttons status.
        /// </summary>
        private void UpdateButtonsStatus()
        {
            btnDuplicate.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDuplicate1.IsEnabled = true;
            btnEdit1.IsEnabled = true;
            dgSharedStepsContextItemEdit.IsEnabled = true;
            dgSharedStepsContextItemPreview.IsEnabled = true;
            dgSharedStepsContextItemDuplicate.IsEnabled = true;
            if (dgSharedSteps.SelectedItems.Count < 1)
            {
                btnDuplicate.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDuplicate1.IsEnabled = false;
                btnEdit1.IsEnabled = false;
                dgSharedStepsContextItemEdit.IsEnabled = false;
                dgSharedStepsContextItemPreview.IsEnabled = false;
                dgSharedStepsContextItemDuplicate.IsEnabled = false;
            }
        }
    }
}