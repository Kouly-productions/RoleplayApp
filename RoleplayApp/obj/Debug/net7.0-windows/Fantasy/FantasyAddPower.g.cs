﻿#pragma checksum "..\..\..\..\Fantasy\FantasyAddPower.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3418947AE5D247E78CBDEDA27F7A9EBD15D2B3B7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RoleplayApp.Fantasy;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace RoleplayApp.Fantasy {
    
    
    /// <summary>
    /// FantasyAddPower
    /// </summary>
    public partial class FantasyAddPower : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WriteName;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WriteAbilityLevelRequirement;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ChooseType;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox IsAOEBox;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox WriteDescription;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Done;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RoleplayApp;component/fantasy/fantasyaddpower.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.WriteName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.WriteAbilityLevelRequirement = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            
            #line 65 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddImage_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ChooseType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.IsAOEBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.WriteDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.Done = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\..\..\Fantasy\FantasyAddPower.xaml"
            this.Done.Click += new System.Windows.RoutedEventHandler(this.Done_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

