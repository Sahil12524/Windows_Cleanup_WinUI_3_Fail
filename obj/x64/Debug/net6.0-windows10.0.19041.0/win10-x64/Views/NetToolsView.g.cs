﻿#pragma checksum "E:\Users\sahil\source\repos\Windows_Cleanup_WinUI_3\Views\NetToolsView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3EF209199521B119979E04F09C6FEFD766E8B9CBE15A5C12A4DA246AF3866353"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Windows_Cleanup_WinUI_3.Views
{
    partial class NetToolsView : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\NetToolsView.xaml line 24
                {
                    this.ExpanderStack = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 3: // Views\NetToolsView.xaml line 48
                {
                    this.ExpanderStack1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 4: // Views\NetToolsView.xaml line 72
                {
                    this.ExpanderStack2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 5: // Views\NetToolsView.xaml line 96
                {
                    this.ExpanderStack3 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 6: // Views\NetToolsView.xaml line 120
                {
                    this.ExpanderStack4 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 7: // Views\NetToolsView.xaml line 131
                {
                    this.btnPacketLossTest = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnPacketLossTest).Click += this.btnPacketLossTest_Click;
                }
                break;
            case 8: // Views\NetToolsView.xaml line 107
                {
                    this.btnEnableNet = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnEnableNet).Click += this.btnEnableNet_Click;
                }
                break;
            case 9: // Views\NetToolsView.xaml line 83
                {
                    this.btnResetIP = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnResetIP).Click += this.btnResetIP_Click;
                }
                break;
            case 10: // Views\NetToolsView.xaml line 59
                {
                    this.btnFlushDNS = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnFlushDNS).Click += this.btnFlushDNS_Click;
                }
                break;
            case 11: // Views\NetToolsView.xaml line 35
                {
                    this.btnDisableNet = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnDisableNet).Click += this.btnDisableNet_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
