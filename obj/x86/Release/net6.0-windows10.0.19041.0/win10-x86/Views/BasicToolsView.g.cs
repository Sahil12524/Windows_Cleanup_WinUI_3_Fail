﻿#pragma checksum "E:\Users\sahil\source\repos\Windows_Cleanup_WinUI_3\Views\BasicToolsView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "21C4EB851ECA8EA10AF879CAAD08F894BF095560D25CF066B27F8E61A61917FD"
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
    partial class BasicToolsView : 
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
            case 2: // Views\BasicToolsView.xaml line 28
                {
                    this.ExpanderStack = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 3: // Views\BasicToolsView.xaml line 52
                {
                    this.ExpanderStack1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 4: // Views\BasicToolsView.xaml line 76
                {
                    this.ExpanderStack2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 5: // Views\BasicToolsView.xaml line 100
                {
                    this.ExpanderStack3 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 6: // Views\BasicToolsView.xaml line 111
                {
                    this.btnSSDTrim = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnSSDTrim).Click += this.btnSSDTrim_Click;
                }
                break;
            case 7: // Views\BasicToolsView.xaml line 87
                {
                    this.btnCHKDSK = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnCHKDSK).Click += this.btnCHKDSK_Click;
                }
                break;
            case 8: // Views\BasicToolsView.xaml line 63
                {
                    this.btnWindowsUpdateCacheClean = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnWindowsUpdateCacheClean).Click += this.btnWindowsUpdateCacheClean_Click;
                }
                break;
            case 9: // Views\BasicToolsView.xaml line 39
                {
                    this.btnTempFilesRemove = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnTempFilesRemove).Click += this.btnTempFilesRemove_Click;
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

