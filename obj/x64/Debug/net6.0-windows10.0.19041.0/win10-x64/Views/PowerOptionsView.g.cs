﻿#pragma checksum "E:\Users\sahil\source\repos\Windows_Cleanup_WinUI_3\Views\PowerOptionsView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8446EC4491A739112A921C1221919C7A1EFB7CCA04E82EDC8CCE86001F496CD5"
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
    partial class PowerOptionsView : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2306")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\PowerOptionsView.xaml line 23
                {
                    this.ExpanderStack = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 3: // Views\PowerOptionsView.xaml line 45
                {
                    this.ExpanderStack1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 4: // Views\PowerOptionsView.xaml line 67
                {
                    this.ExpanderStack2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 5: // Views\PowerOptionsView.xaml line 91
                {
                    this.ExpanderStack3 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 6: // Views\PowerOptionsView.xaml line 102
                {
                    this.btnLogOff = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnLogOff).Click += this.btnLogOff_Click;
                }
                break;
            case 7: // Views\PowerOptionsView.xaml line 78
                {
                    this.btnAbortSR = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnAbortSR).Click += this.btnAbortSR_Click;
                }
                break;
            case 8: // Views\PowerOptionsView.xaml line 56
                {
                    this.btnRestart = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnRestart).Click += this.btnRestart_Click;
                }
                break;
            case 9: // Views\PowerOptionsView.xaml line 34
                {
                    this.btnShutdown = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnShutdown).Click += this.btnShutdown_Click;
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2306")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

