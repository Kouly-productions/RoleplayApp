﻿#pragma checksum "..\..\..\..\Fantasy\FantasyShowCharacters.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5B5BB89F7067A473C15473A6409F689DD743BD4B"
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
    /// FantasyShowCharacters
    /// </summary>
    public partial class FantasyShowCharacters : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel FantasyCharacterPanel;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NumberOfCharacters;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SortByComboBox;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBox;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox PlayAudio;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GoBack;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateCharacter;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RoleplayApp;component/fantasy/fantasyshowcharacters.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.FantasyCharacterPanel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 2:
            this.NumberOfCharacters = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.SortByComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 60 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
            this.SortByComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SortByComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SearchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 72 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
            this.SearchBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PlayAudio = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.GoBack = ((System.Windows.Controls.Button)(target));
            
            #line 93 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
            this.GoBack.Click += new System.Windows.RoutedEventHandler(this.GoBack_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CreateCharacter = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\..\..\Fantasy\FantasyShowCharacters.xaml"
            this.CreateCharacter.Click += new System.Windows.RoutedEventHandler(this.CreateCharacter_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

