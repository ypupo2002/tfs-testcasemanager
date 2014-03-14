﻿#pragma checksum "..\..\..\Views\AssociateTestView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "70F19123554D0E08C7BDFDEEB752442D"
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
using TestCaseManagerCore.Helpers;


namespace TestCaseManagerApp.Views {
    
    
    /// <summary>
    /// AssociateTestView
    /// </summary>
    public partial class AssociateTestView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 10 "..\..\..\Views\AssociateTestView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\AssociateTestView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbFullName;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\AssociateTestView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbClassName;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\AssociateTestView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbTestType;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Views\AssociateTestView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgTests;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Views\AssociateTestView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAssociate;
        
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
            System.Uri resourceLocater = new System.Uri("/TestCaseManager;component/views/associatetestview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AssociateTestView.xaml"
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
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.tbFullName = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\..\Views\AssociateTestView.xaml"
            this.tbFullName.GotFocus += new System.Windows.RoutedEventHandler(this.tbFullName_GotFocus);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\Views\AssociateTestView.xaml"
            this.tbFullName.LostFocus += new System.Windows.RoutedEventHandler(this.tbFullName_LostFocus);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\Views\AssociateTestView.xaml"
            this.tbFullName.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbFullName_KeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbClassName = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\Views\AssociateTestView.xaml"
            this.tbClassName.GotFocus += new System.Windows.RoutedEventHandler(this.tbClassName_GotFocus);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\Views\AssociateTestView.xaml"
            this.tbClassName.LostFocus += new System.Windows.RoutedEventHandler(this.tbClassName_LostFocus);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\Views\AssociateTestView.xaml"
            this.tbClassName.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbClassName_KeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbTestType = ((System.Windows.Controls.ComboBox)(target));
            
            #line 29 "..\..\..\Views\AssociateTestView.xaml"
            this.cbTestType.MouseEnter += new System.Windows.Input.MouseEventHandler(this.cbTestType_MouseEnter);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\Views\AssociateTestView.xaml"
            this.cbTestType.MouseMove += new System.Windows.Input.MouseEventHandler(this.cbTestType_MouseMove);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dgTests = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.btnAssociate = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\Views\AssociateTestView.xaml"
            this.btnAssociate.Click += new System.Windows.RoutedEventHandler(this.btnAssociate_Click);
            
            #line default
            #line hidden
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
            case 6:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 35 "..\..\..\Views\AssociateTestView.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.dgTests_MouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

