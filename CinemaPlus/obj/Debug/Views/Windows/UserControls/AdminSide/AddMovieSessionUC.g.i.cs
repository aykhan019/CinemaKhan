﻿#pragma checksum "..\..\..\..\..\..\Views\Windows\UserControls\AdminSide\AddMovieSessionUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C12A167F60ED6BB0CAE46B4DA8BB00155061EEB977E2F43378EAADF267496F0B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CinemaPlus.Views.UserControls.AdminSide;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Expression.Interactivity.Input;
using Microsoft.Expression.Interactivity.Layout;
using Microsoft.Expression.Interactivity.Media;
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
using System.Windows.Interactivity;
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


namespace CinemaPlus.Views.UserControls.AdminSide {
    
    
    /// <summary>
    /// AddMovieSessionUC
    /// </summary>
    public partial class AddMovieSessionUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 51 "..\..\..\..\..\..\Views\Windows\UserControls\AdminSide\AddMovieSessionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CinemasCB;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\..\Views\Windows\UserControls\AdminSide\AddMovieSessionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox HallsCB;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\..\..\Views\Windows\UserControls\AdminSide\AddMovieSessionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DatesCB;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\..\..\Views\Windows\UserControls\AdminSide\AddMovieSessionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TimesCB;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\..\..\..\Views\Windows\UserControls\AdminSide\AddMovieSessionUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CinemaPlus.Views.UserControls.AdminSide.MovieSessionUC SessionUC;
        
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
            System.Uri resourceLocater = new System.Uri("/CinemaPlus;component/views/windows/usercontrols/adminside/addmoviesessionuc.xaml" +
                    "", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Views\Windows\UserControls\AdminSide\AddMovieSessionUC.xaml"
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
            this.CinemasCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.HallsCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.DatesCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.TimesCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.SessionUC = ((CinemaPlus.Views.UserControls.AdminSide.MovieSessionUC)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

