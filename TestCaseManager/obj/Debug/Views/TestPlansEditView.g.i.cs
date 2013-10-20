﻿#pragma checksum "..\..\..\Views\TestPlansEditView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "26880A46A43834A2B3A2FF916FD20127"
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
using TestCaseManagerApp.Views;
using TestCaseManagerCore.BusinessLogic.Entities;


namespace TestCaseManagerApp.Views {
    
    
    /// <summary>
    /// TestPlansEditView
    /// </summary>
    public partial class TestPlansEditView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Views\TestPlansEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressBar;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Views\TestPlansEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\TestPlansEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddTestPlan;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\TestPlansEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteTestPlan;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\TestPlansEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTestPlans;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Views\TestPlansEditView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFinish;
        
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
            System.Uri resourceLocater = new System.Uri("/TestCaseManager;component/views/testplanseditview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\TestPlansEditView.xaml"
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
            
            #line 9 "..\..\..\Views\TestPlansEditView.xaml"
            ((TestCaseManagerApp.Views.TestPlansEditView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.TestCaseExecutionArrangmentView_Loaded);
            
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
            this.btnAddTestPlan = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Views\TestPlansEditView.xaml"
            this.btnAddTestPlan.Click += new System.Windows.RoutedEventHandler(this.btnAddTestPlan_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnDeleteTestPlan = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Views\TestPlansEditView.xaml"
            this.btnDeleteTestPlan.Click += new System.Windows.RoutedEventHandler(this.btnDeleteTestPlan_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.dgTestPlans = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.btnFinish = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\Views\TestPlansEditView.xaml"
            this.btnFinish.Click += new System.Windows.RoutedEventHandler(this.btnFinish_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

