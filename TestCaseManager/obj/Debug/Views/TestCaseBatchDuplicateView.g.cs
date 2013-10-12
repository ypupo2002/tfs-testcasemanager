﻿#pragma checksum "..\..\..\Views\TestCaseBatchDuplicateView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D6256786F6D9B35D5B49429DC83E5C27"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18331
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
using TestCaseManagerCore.BusinessLogic.Enums;
using TestCaseManagerCore.Helpers;


namespace TestCaseManagerApp.Views {
    
    
    /// <summary>
    /// TestCaseBatchDuplicateView
    /// </summary>
    public partial class TestCaseBatchDuplicateView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressBar;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSuite;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPriority;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbTeamFoundationIdentityNames;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgSharedStepIds;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTextsToBeReplaced;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTitleFilter;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSuiteFilter;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbAssignedToFilter;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPriorityFilter;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTestCases;
        
        #line default
        #line hidden
        
        
        #line 198 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBatchDuplicate;
        
        #line default
        #line hidden
        
        
        #line 199 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFindAndReplace;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSelectedTestCaseCount;
        
        #line default
        #line hidden
        
        
        #line 206 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbTestCaseCount;
        
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
            System.Uri resourceLocater = new System.Uri("/TestCaseManager;component/views/testcasebatchduplicateview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
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
            
            #line 10 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            ((TestCaseManagerApp.Views.TestCaseBatchDuplicateView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.progressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 3:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.cbSuite = ((System.Windows.Controls.ComboBox)(target));
            
            #line 52 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.cbSuite.MouseEnter += new System.Windows.Input.MouseEventHandler(this.cbSuite_MouseEnter);
            
            #line default
            #line hidden
            
            #line 53 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.cbSuite.MouseMove += new System.Windows.Input.MouseEventHandler(this.cbSuite_MouseMove);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cbPriority = ((System.Windows.Controls.ComboBox)(target));
            
            #line 56 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.cbPriority.MouseEnter += new System.Windows.Input.MouseEventHandler(this.cbPriority_MouseEnter);
            
            #line default
            #line hidden
            
            #line 57 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.cbPriority.MouseMove += new System.Windows.Input.MouseEventHandler(this.cbPriority_MouseMove);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cbTeamFoundationIdentityNames = ((System.Windows.Controls.ComboBox)(target));
            
            #line 61 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.cbTeamFoundationIdentityNames.MouseEnter += new System.Windows.Input.MouseEventHandler(this.cbTeamFoundationIdentityNames_MouseEnter);
            
            #line default
            #line hidden
            
            #line 62 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.cbTeamFoundationIdentityNames.MouseMove += new System.Windows.Input.MouseEventHandler(this.cbTeamFoundationIdentityNames_MouseMove);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dgSharedStepIds = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            this.dgTextsToBeReplaced = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 9:
            this.tbTitleFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 154 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbTitleFilter.GotFocus += new System.Windows.RoutedEventHandler(this.tbTitleFilter_GotFocus);
            
            #line default
            #line hidden
            
            #line 154 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbTitleFilter.LostFocus += new System.Windows.RoutedEventHandler(this.tbTitleFilter_LostFocus);
            
            #line default
            #line hidden
            
            #line 154 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbTitleFilter.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbTitleFilter_KeyUp);
            
            #line default
            #line hidden
            return;
            case 10:
            this.tbSuiteFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 156 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbSuiteFilter.GotFocus += new System.Windows.RoutedEventHandler(this.tbTextSuiteFilter_GotFocus);
            
            #line default
            #line hidden
            
            #line 156 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbSuiteFilter.LostFocus += new System.Windows.RoutedEventHandler(this.tbSuiteFilter_LostFocus);
            
            #line default
            #line hidden
            
            #line 156 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbSuiteFilter.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbSuiteFilter_KeyUp);
            
            #line default
            #line hidden
            return;
            case 11:
            this.tbAssignedToFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 158 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbAssignedToFilter.GotFocus += new System.Windows.RoutedEventHandler(this.tbAssignedToFilter_GotFocus);
            
            #line default
            #line hidden
            
            #line 158 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbAssignedToFilter.LostFocus += new System.Windows.RoutedEventHandler(this.tbAssignedToFilter_LostFocus);
            
            #line default
            #line hidden
            
            #line 158 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbAssignedToFilter.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbAssignedToFilter_KeyUp);
            
            #line default
            #line hidden
            return;
            case 12:
            this.tbPriorityFilter = ((System.Windows.Controls.TextBox)(target));
            
            #line 160 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbPriorityFilter.GotFocus += new System.Windows.RoutedEventHandler(this.tbPriorityFilter_GotFocus);
            
            #line default
            #line hidden
            
            #line 160 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbPriorityFilter.LostFocus += new System.Windows.RoutedEventHandler(this.tbPriorityFilter_LostFocus);
            
            #line default
            #line hidden
            
            #line 160 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.tbPriorityFilter.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbPriorityFilter_KeyUp);
            
            #line default
            #line hidden
            return;
            case 13:
            this.dgTestCases = ((System.Windows.Controls.DataGrid)(target));
            
            #line 164 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.dgTestCases.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.dgTestCases_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.btnBatchDuplicate = ((System.Windows.Controls.Button)(target));
            
            #line 198 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.btnBatchDuplicate.Click += new System.Windows.RoutedEventHandler(this.btnBatchDuplicate_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.btnFindAndReplace = ((System.Windows.Controls.Button)(target));
            
            #line 199 "..\..\..\Views\TestCaseBatchDuplicateView.xaml"
            this.btnFindAndReplace.Click += new System.Windows.RoutedEventHandler(this.btnFindAndReplace_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.tbSelectedTestCaseCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 17:
            this.tbTestCaseCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
