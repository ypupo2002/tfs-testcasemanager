﻿#pragma checksum "..\..\..\Views\TestCaseEditView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5FE04F70497B5A68A9816F994C9AAA1E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TestCaseManagerApp.BusinessLogic.Converters;
using TestCaseManagerApp.Helpers;
using TestCaseManagerApp.Views;


namespace TestCaseManagerApp.Views {
    
    
    /// <summary>
    /// TestCaseEditView
    /// </summary>
    public partial class TestCaseEditView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressBar;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbPageTitle;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUndo;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRedo;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSuite;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbArea;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPriority;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition Column3;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox rtbAction;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox rtbExpectedResult;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAssociateToAutomation;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInsertStep;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveTestStep;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelEdit;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTestSteps;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMoveUp;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMoveDown;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnShare;
        
        #line default
        #line hidden
        
        
        #line 188 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteStep;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnChange;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSharedStepFilter;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSharedSteps;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveAndCloseTestCase;
        
        #line default
        #line hidden
        
        
        #line 216 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveTestCase;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 224 "..\..\..\Views\TestCaseEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDuplicate;
        
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
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 10 "..\..\..\Views\TestCaseEditView.xaml"
            ((TestCaseManagerApp.Views.TestCaseEditView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 16 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnSaveTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 17 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnSaveAndCloseTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 18 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 19 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnChange_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 20 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnInsertStep_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 21 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnAssociateToAutomation_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 22 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.deleteStep_Command);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 23 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.moveUp_Command);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 24 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.moveDown_Command);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 25 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 26 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.btnShare_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 28 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.copyTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 29 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.cutTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 30 "..\..\..\Views\TestCaseEditView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.pasteTestSteps_Command);
            
            #line default
            #line hidden
            return;
            case 16:
            this.progressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 17:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 18:
            this.tbPageTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 19:
            this.btnUndo = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnUndo.Click += new System.Windows.RoutedEventHandler(this.btnUndo_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            this.btnRedo = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnRedo.Click += new System.Windows.RoutedEventHandler(this.btnRedo_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            this.tbSuite = ((System.Windows.Controls.TextBox)(target));
            return;
            case 22:
            this.cbArea = ((System.Windows.Controls.ComboBox)(target));
            
            #line 77 "..\..\..\Views\TestCaseEditView.xaml"
            this.cbArea.MouseEnter += new System.Windows.Input.MouseEventHandler(this.cbArea_MouseEnter);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\Views\TestCaseEditView.xaml"
            this.cbArea.MouseMove += new System.Windows.Input.MouseEventHandler(this.cbArea_MouseMove);
            
            #line default
            #line hidden
            return;
            case 23:
            this.cbPriority = ((System.Windows.Controls.ComboBox)(target));
            
            #line 79 "..\..\..\Views\TestCaseEditView.xaml"
            this.cbPriority.MouseEnter += new System.Windows.Input.MouseEventHandler(this.cbPriority_MouseEnter);
            
            #line default
            #line hidden
            
            #line 79 "..\..\..\Views\TestCaseEditView.xaml"
            this.cbPriority.MouseMove += new System.Windows.Input.MouseEventHandler(this.cbPriority_MouseMove);
            
            #line default
            #line hidden
            return;
            case 24:
            this.Column3 = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 25:
            this.rtbAction = ((System.Windows.Controls.RichTextBox)(target));
            
            #line 99 "..\..\..\Views\TestCaseEditView.xaml"
            this.rtbAction.GotFocus += new System.Windows.RoutedEventHandler(this.rtbStep_GotFocus);
            
            #line default
            #line hidden
            
            #line 99 "..\..\..\Views\TestCaseEditView.xaml"
            this.rtbAction.LostFocus += new System.Windows.RoutedEventHandler(this.rtbStep_LostFocus);
            
            #line default
            #line hidden
            return;
            case 26:
            this.rtbExpectedResult = ((System.Windows.Controls.RichTextBox)(target));
            
            #line 100 "..\..\..\Views\TestCaseEditView.xaml"
            this.rtbExpectedResult.GotFocus += new System.Windows.RoutedEventHandler(this.rtbExpectedResult_GotFocus);
            
            #line default
            #line hidden
            
            #line 100 "..\..\..\Views\TestCaseEditView.xaml"
            this.rtbExpectedResult.LostFocus += new System.Windows.RoutedEventHandler(this.rtbExpectedResult_LostFocus);
            
            #line default
            #line hidden
            return;
            case 27:
            this.btnAssociateToAutomation = ((System.Windows.Controls.Button)(target));
            
            #line 103 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnAssociateToAutomation.Click += new System.Windows.RoutedEventHandler(this.btnAssociateToAutomation_Click);
            
            #line default
            #line hidden
            return;
            case 28:
            this.btnInsertStep = ((System.Windows.Controls.Button)(target));
            
            #line 119 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnInsertStep.Click += new System.Windows.RoutedEventHandler(this.btnInsertStep_Click);
            
            #line default
            #line hidden
            return;
            case 29:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 120 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnEdit.Click += new System.Windows.RoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 30:
            this.btnSaveTestStep = ((System.Windows.Controls.Button)(target));
            
            #line 121 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnSaveTestStep.Click += new System.Windows.RoutedEventHandler(this.btnSaveTestStep_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            this.btnCancelEdit = ((System.Windows.Controls.Button)(target));
            
            #line 122 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnCancelEdit.Click += new System.Windows.RoutedEventHandler(this.btnCancelEdit_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            this.dgTestSteps = ((System.Windows.Controls.DataGrid)(target));
            
            #line 132 "..\..\..\Views\TestCaseEditView.xaml"
            this.dgTestSteps.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.dgTestSteps_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 132 "..\..\..\Views\TestCaseEditView.xaml"
            this.dgTestSteps.PreviewMouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.dgTestSteps_PreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            case 33:
            this.btnMoveUp = ((System.Windows.Controls.Button)(target));
            
            #line 184 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnMoveUp.Click += new System.Windows.RoutedEventHandler(this.btnMoveUp_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            this.btnMoveDown = ((System.Windows.Controls.Button)(target));
            
            #line 185 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnMoveDown.Click += new System.Windows.RoutedEventHandler(this.btnMoveDown_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 186 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            this.btnShare = ((System.Windows.Controls.Button)(target));
            
            #line 187 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnShare.Click += new System.Windows.RoutedEventHandler(this.btnShare_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            this.btnDeleteStep = ((System.Windows.Controls.Button)(target));
            
            #line 188 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnDeleteStep.Click += new System.Windows.RoutedEventHandler(this.btnDeleteStep_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            this.btnChange = ((System.Windows.Controls.Button)(target));
            
            #line 189 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnChange.Click += new System.Windows.RoutedEventHandler(this.btnChange_Click);
            
            #line default
            #line hidden
            return;
            case 39:
            this.tbSharedStepFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 194 "..\..\..\Views\TestCaseEditView.xaml"
            this.tbSharedStepFilter.GotFocus += new System.Windows.RoutedEventHandler(this.tbSharedStepFilter_GotFocus);
            
            #line default
            #line hidden
            
            #line 194 "..\..\..\Views\TestCaseEditView.xaml"
            this.tbSharedStepFilter.LostFocus += new System.Windows.RoutedEventHandler(this.tbSharedStepFilter_LostFocus);
            
            #line default
            #line hidden
            
            #line 194 "..\..\..\Views\TestCaseEditView.xaml"
            this.tbSharedStepFilter.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbSharedStepFilter_KeyUp);
            
            #line default
            #line hidden
            return;
            case 40:
            this.dgSharedSteps = ((System.Windows.Controls.DataGrid)(target));
            
            #line 196 "..\..\..\Views\TestCaseEditView.xaml"
            this.dgSharedSteps.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dgSharedSteps_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 41:
            this.btnSaveAndCloseTestCase = ((System.Windows.Controls.Button)(target));
            
            #line 215 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnSaveAndCloseTestCase.Click += new System.Windows.RoutedEventHandler(this.btnSaveAndCloseTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 42:
            this.btnSaveTestCase = ((System.Windows.Controls.Button)(target));
            
            #line 216 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnSaveTestCase.Click += new System.Windows.RoutedEventHandler(this.btnSaveTestCase_Click);
            
            #line default
            #line hidden
            return;
            case 43:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 223 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 44:
            this.btnDuplicate = ((System.Windows.Controls.Button)(target));
            
            #line 224 "..\..\..\Views\TestCaseEditView.xaml"
            this.btnDuplicate.Click += new System.Windows.RoutedEventHandler(this.btnDuplicate_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

