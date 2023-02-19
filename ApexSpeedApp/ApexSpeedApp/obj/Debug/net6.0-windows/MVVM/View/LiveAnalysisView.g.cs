﻿#pragma checksum "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4565CC1D64025280E4C1ECB7F1E2169B76B42359"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ApexSpeedApp.MVVM.View;
using ApexSpeedApp.MVVM.ViewModel;
using LiveChartsCore.SkiaSharpView.WPF;
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


namespace ApexSpeedApp.MVVM.View {
    
    
    /// <summary>
    /// LiveAnalysisView
    /// </summary>
    public partial class LiveAnalysisView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UDPListenerButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ListenerTestLabel;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ListeningLabel;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ThrottleLabel;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UDPStopListenerButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ApexSpeedApp;component/mvvm/view/liveanalysisview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UDPListenerButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml"
            this.UDPListenerButton.Click += new System.Windows.RoutedEventHandler(this.UDPListenerButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ListenerTestLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.ListeningLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.ThrottleLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.UDPStopListenerButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\..\MVVM\View\LiveAnalysisView.xaml"
            this.UDPStopListenerButton.Click += new System.Windows.RoutedEventHandler(this.UDPStopListenerButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

