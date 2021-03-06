﻿#pragma checksum "..\..\..\Views\TestCaseEditView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F0D73BD7E864188C3CC91A7F2DFF3E96"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18210
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Converters;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TestCaseManagerApp.Views;
using TestCaseManagerCore.BusinessLogic.Converters;
using TestCaseManagerCore.BusinessLogic.Enums;
using TestCaseManagerCore.Helpers;


namespace TestCaseManagerApp.Views {
    
    
    /// <summary>
    /// TestCaseEditView
    /// </summary>
    public partial class TestCaseEditView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 39 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressBar;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUndo;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRedo;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTitle;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSuite;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbArea;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPriority;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition Column3;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox rtbAction;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox rtbExpectedResult;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAssociateToAutomation;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInsertStep;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveTestStep;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelEdit;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTestSteps;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem dgTestStepsCutMenuItem;
        
        #line default
        #line hidden
        
        
        #line 177 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem dgTestStepsCopyMenuItem;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem dgTestStepsPasteMenuItem;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem dgTestStepsShareMenuItem;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem dgTestStepsEditMenuItem;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem dgTestStepsDeleteMenuItem;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMoveUp;
        
        #line default
        #line hidden
        
        
        #line 230 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMoveDown;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 232 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnShare;
        
        #line default
        #line hidden
        
        
        #line 233 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteStep;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnChange;
        
        #line default
        #line hidden
        
        
        #line 246 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSharedStepFilter;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSharedSteps;
        
        #line default
        #line hidden
        
        
        #line 285 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveAndCloseTestCase;
        
        #line default
        #line hidden
        
        
        #line 286 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveTestCase;
        
        #line default
        #line hidden
        
        
        #line 287 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 288 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDuplicate;
        
        #line default
        #line hidden
        
        
        #line 290 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPreview;
        
        #line default
        #line hidden
        
        
        #line 291 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FirstFloor.ModernUI.Windows.Controls.BBCodeBlock lbSaved;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TestCaseManager;component/views/testcaseeditview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\TestCaseEditView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 12 "..\..\..\Views\TestCaseEditView.xaml"
            ((TestCaseManagerApp.Views.TestCaseEditView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 18 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnSaveTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 19 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnSaveAndCloseTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 20 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 21 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnChange_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 22 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnInsertStep_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 23 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnAssociateToAutomation_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 24 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.deleteStep_Command);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 25 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.moveUp_Command);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 26 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.moveDown_Command);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 27 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 28 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnShare_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 29 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.copyTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 30 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.cutTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 31 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.pasteTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 32 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnUndo_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 33 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnRedo_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 34 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.editSharedStep_Command);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 35 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.duplicateSharedStep_Command);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 36 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 37 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.PreviewButton_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            this.progressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 23:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 24:
            this.btnUndo = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnUndo.Click += new System.Windows.RoutedEventHandler(this.btnUndo_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            this.btnRedo = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnRedo.Click += new System.Windows.RoutedEventHandler(this.btnRedo_Click);
            
            #line default
            #line hidden
            return;
            case 26:
            this.tbTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 27:
            this.tbSuite = ((System.Windows.Controls.TextBox)(target));
            return;
            case 28:
            this.cbArea = ((System.Windows.Controls.ComboBox)(target));
            
            #line 94 "..\..\..\Views\TestCaseEditView.xaml"
            this.cbArea.MouseEnter += new System.Windows.Input.MouseEventHandler(this.cbArea_MouseEnter);
            
            #line default
            #line hidden
            
            #line 94 "..\..\..\Views\TestCaseEditView.xaml"
            this.cbArea.MouseMove += new System.Windows.Input.MouseEventHandler(this.cbArea_MouseMove);
            
            #line default
            #line hidden
            return;
            case 29:
            this.cbPriority = ((System.Windows.Controls.ComboBox)(target));
            
            #line 97 "..\..\..\Views\TestCaseEditView.xaml"
            this.cbPriority.MouseEnter += new System.Windows.Input.MouseEventHandler(this.cbPriority_MouseEnter);
            
            #line default
            #line hidden
            
            #line 97 "..\..\..\Views\TestCaseEditView.xaml"
            this.cbPriority.MouseMove += new System.Windows.Input.MouseEventHandler(this.cbPriority_MouseMove);
            
            #line default
            #line hidden
            return;
            case 30:
            this.Column3 = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 31:
            this.rtbAction = ((System.Windows.Controls.RichTextBox)(target));
            
            #line 126 "..\..\..\Views\TestCaseEditView.xaml"
            this.rtbAction.GotFocus += new System.Windows.RoutedEventHandler(this.rtbStep_GotFocus);
            
            #line default
            #line hidden
            
            #line 126 "..\..\..\Views\TestCaseEditView.xaml"
            this.rtbAction.LostFocus += new System.Windows.RoutedEventHandler(this.rtbStep_LostFocus);
            
            #line default
            #line hidden
            return;
            case 32:
            this.rtbExpectedResult = ((System.Windows.Controls.RichTextBox)(target));
            
            #line 127 "..\..\..\Views\TestCaseEditView.xaml"
            this.rtbExpectedResult.GotFocus += new System.Windows.RoutedEventHandler(this.rtbExpectedResult_GotFocus);
            
            #line default
            #line hidden
            
            #line 127 "..\..\..\Views\TestCaseEditView.xaml"
            this.rtbExpectedResult.LostFocus += new System.Windows.RoutedEventHandler(this.rtbExpectedResult_LostFocus);
            
            #line default
            #line hidden
            return;
            case 33:
            this.btnAssociateToAutomation = ((System.Windows.Controls.Button)(target));
            
            #line 130 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnAssociateToAutomation.Click += new System.Windows.RoutedEventHandler(this.btnAssociateToAutomation_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            this.btnInsertStep = ((System.Windows.Controls.Button)(target));
            
            #line 140 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnInsertStep.Click += new System.Windows.RoutedEventHandler(this.btnInsertStep_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 141 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnEdit.Click += new System.Windows.RoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            this.btnSaveTestStep = ((System.Windows.Controls.Button)(target));
            
            #line 142 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnSaveTestStep.Click += new System.Windows.RoutedEventHandler(this.btnSaveTestStep_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            this.btnCancelEdit = ((System.Windows.Controls.Button)(target));
            
            #line 143 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnCancelEdit.Click += new System.Windows.RoutedEventHandler(this.btnCancelEdit_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            this.dgTestSteps = ((System.Windows.Controls.DataGrid)(target));
            
            #line 156 "..\..\..\Views\TestCaseEditView.xaml"
            this.dgTestSteps.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.dgTestSteps_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 156 "..\..\..\Views\TestCaseEditView.xaml"
            this.dgTestSteps.PreviewMouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.dgTestSteps_PreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 157 "..\..\..\Views\TestCaseEditView.xaml"
            this.dgTestSteps.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.dgTestSteps_SelectedCellsChanged);
            
            #line default
            #line hidden
            
            #line 157 "..\..\..\Views\TestCaseEditView.xaml"
            this.dgTestSteps.LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.dgTestSteps_LoadingRow);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 164 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.copyTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 165 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.pasteTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 166 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.cutTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 43:
            this.dgTestStepsCutMenuItem = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 44:
            this.dgTestStepsCopyMenuItem = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 45:
            this.dgTestStepsPasteMenuItem = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 46:
            this.dgTestStepsShareMenuItem = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 47:
            this.dgTestStepsEditMenuItem = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 48:
            this.dgTestStepsDeleteMenuItem = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 49:
            this.btnMoveUp = ((System.Windows.Controls.Button)(target));
            
            #line 229 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnMoveUp.Click += new System.Windows.RoutedEventHandler(this.btnMoveUp_Click);
            
            #line default
            #line hidden
            return;
            case 50:
            this.btnMoveDown = ((System.Windows.Controls.Button)(target));
            
            #line 230 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnMoveDown.Click += new System.Windows.RoutedEventHandler(this.btnMoveDown_Click);
            
            #line default
            #line hidden
            return;
            case 51:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 231 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 52:
            this.btnShare = ((System.Windows.Controls.Button)(target));
            
            #line 232 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnShare.Click += new System.Windows.RoutedEventHandler(this.btnShare_Click);
            
            #line default
            #line hidden
            return;
            case 53:
            this.btnDeleteStep = ((System.Windows.Controls.Button)(target));
            
            #line 233 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnDeleteStep.Click += new System.Windows.RoutedEventHandler(this.btnDeleteStep_Click);
            
            #line default
            #line hidden
            return;
            case 54:
            this.btnChange = ((System.Windows.Controls.Button)(target));
            
            #line 234 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnChange.Click += new System.Windows.RoutedEventHandler(this.btnChange_Click);
            
            #line default
            #line hidden
            return;
            case 55:
            this.tbSharedStepFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 247 "..\..\..\Views\TestCaseEditView.xaml"
            this.tbSharedStepFilter.GotFocus += new System.Windows.RoutedEventHandler(this.tbSharedStepFilter_GotFocus);
            
            #line default
            #line hidden
            
            #line 247 "..\..\..\Views\TestCaseEditView.xaml"
            this.tbSharedStepFilter.LostFocus += new System.Windows.RoutedEventHandler(this.tbSharedStepFilter_LostFocus);
            
            #line default
            #line hidden
            
            #line 247 "..\..\..\Views\TestCaseEditView.xaml"
            this.tbSharedStepFilter.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbSharedStepFilter_KeyUp);
            
            #line default
            #line hidden
            return;
            case 56:
            this.dgSharedSteps = ((System.Windows.Controls.DataGrid)(target));
            
            #line 251 "..\..\..\Views\TestCaseEditView.xaml"
            this.dgSharedSteps.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.dgSharedSteps_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 58:
            this.btnSaveAndCloseTestCase = ((System.Windows.Controls.Button)(target));
            
            #line 285 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnSaveAndCloseTestCase.Click += new System.Windows.RoutedEventHandler(this.btnSaveAndCloseTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 59:
            this.btnSaveTestCase = ((System.Windows.Controls.Button)(target));
            
            #line 286 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnSaveTestCase.Click += new System.Windows.RoutedEventHandler(this.btnSaveTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 60:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 287 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 61:
            this.btnDuplicate = ((System.Windows.Controls.Button)(target));
            
            #line 289 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnDuplicate.Click += new System.Windows.RoutedEventHandler(this.btnDuplicate_Click);
            
            #line default
            #line hidden
            return;
            case 62:
            this.btnPreview = ((System.Windows.Controls.Button)(target));
            
            #line 290 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnPreview.Click += new System.Windows.RoutedEventHandler(this.PreviewButton_Click);
            
            #line default
            #line hidden
            return;
            case 63:
            this.lbSaved = ((FirstFloor.ModernUI.Windows.Controls.BBCodeBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 42:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 170 "..\..\..\Views\TestCaseEditView.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.dgTestSteps_MouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 57:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 255 "..\..\..\Views\TestCaseEditView.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.dgSharedSteps_MouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

