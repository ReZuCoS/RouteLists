﻿#pragma checksum "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C51EF7E0EA07B9F20DF064C29FA484D5E478156CCA2169E33BE7F22436D7E66E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RouteLists.View.Pages.ListPages;
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


namespace RouteLists.View.Pages.ListPages {
    
    
    /// <summary>
    /// PageDrivers
    /// </summary>
    public partial class PageDrivers : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 21 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewMain;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxSearh;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;
        
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
            System.Uri resourceLocater = new System.Uri("/RouteLists;component/view/pages/listpages/pagedrivers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
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
            
            #line 10 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
            ((RouteLists.View.Pages.ListPages.PageDrivers)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ClearFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.listViewMain = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.textBoxSearh = ((System.Windows.Controls.TextBox)(target));
            
            #line 219 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
            this.textBoxSearh.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ListUpdateOnSearh);
            
            #line default
            #line hidden
            return;
            case 7:
            this.addButton = ((System.Windows.Controls.Button)(target));
            
            #line 228 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
            this.addButton.Click += new System.Windows.RoutedEventHandler(this.AddDriver);
            
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
            case 3:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 35 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.EditSelectedDriver);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            case 4:
            
            #line 95 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowDriverReport);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 174 "..\..\..\..\..\View\Pages\ListPages\PageDrivers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CopyCarNumber);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
