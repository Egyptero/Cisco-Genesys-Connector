﻿#pragma checksum "..\..\..\CiscoAgent\CiscoVoiceView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "284FAA99314C9F034BCDD634D8FA54732BE4A6AE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent {
    
    
    /// <summary>
    /// CiscoVoiceView
    /// </summary>
    public partial class CiscoVoiceView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 4 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent.CiscoVoiceView MySampleViewInteractionWorksheet;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel CallActionWaitingWindow;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel CallButtons;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Transfer;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Hold;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Resume;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Release;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NumberToDial;
        
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
            System.Uri resourceLocater = new System.Uri("/Genesyslab.Desktop.Modules.CiscoVoice;component/ciscoagent/ciscovoiceview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
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
            this.MySampleViewInteractionWorksheet = ((Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent.CiscoVoiceView)(target));
            return;
            case 2:
            this.CallActionWaitingWindow = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 3:
            this.CallButtons = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 4:
            this.Transfer = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
            this.Transfer.Click += new System.Windows.RoutedEventHandler(this.CallButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Hold = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
            this.Hold.Click += new System.Windows.RoutedEventHandler(this.CallButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Resume = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
            this.Resume.Click += new System.Windows.RoutedEventHandler(this.CallButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Release = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\CiscoAgent\CiscoVoiceView.xaml"
            this.Release.Click += new System.Windows.RoutedEventHandler(this.CallButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.NumberToDial = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
