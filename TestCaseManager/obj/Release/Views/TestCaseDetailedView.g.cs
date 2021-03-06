﻿#pragma checksum "..\..\..\Views\TestCaseDetailedView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3D1567E6E9C9A236E82BEDE0BFEDAB75"
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


namespace TestCaseManagerApp.Views {
    
    
    /// <summary>
    /// TestCaseDetailedView
    /// </summary>
    public partial class TestCaseDetailedView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressBar;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gAssociatedAutomationInfo;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTestSteps;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDuplicate;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPass;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFail;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\Views\TestCaseDetailedView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBlock;
        
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
            System.Uri resourceLocater = new System.Uri("/TestCaseManager;component/views/testcasedetailedview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\TestCaseDetailedView.xaml"
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
            
            #line 9 "..\..\..\Views\TestCaseDetailedView.xaml"
            ((TestCaseManagerApp.Views.TestCaseDetailedView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 15 "..\..\..\Views\TestCaseDetailedView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.EditButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 16 "..\..\..\Views\TestCaseDetailedView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.DuplicateButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.progressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 5:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.gAssociatedAutomationInfo = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.dgTestSteps = ((System.Windows.Controls.DataGrid)(target));
            
            #line 74 "..\..\..\Views\TestCaseDetailedView.xaml"
            this.dgTestSteps.LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.dgTestSteps_LoadingRow);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\Views\TestCaseDetailedView.xaml"
            this.btnEdit.Click += new System.Windows.RoutedEventHandler(this.EditButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnDuplicate = ((System.Windows.Controls.Button)(target));
            
            #line 118 "..\..\..\Views\TestCaseDetailedView.xaml"
            this.btnDuplicate.Click += new System.Windows.RoutedEventHandler(this.DuplicateButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnPass = ((System.Windows.Controls.Button)(target));
            
            #line 121 "..\..\..\Views\TestCaseDetailedView.xaml"
            this.btnPass.Click += new System.Windows.RoutedEventHandler(this.btnPass_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnFail = ((System.Windows.Controls.Button)(target));
            
            #line 122 "..\..\..\Views\TestCaseDetailedView.xaml"
            this.btnFail.Click += new System.Windows.RoutedEventHandler(this.btnFail_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnBlock = ((System.Windows.Controls.Button)(target));
            
            #line 123 "..\..\..\Views\TestCaseDetailedView.xaml"
            this.btnBlock.Click += new System.Windows.RoutedEventHandler(this.btnBlock_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

