﻿#pragma checksum "C:\Users\cdand\source\repos\UBB-SE-2025-DreamTeamt\UBB-SE-2025-Marketplace-CustomerSupport-main\View\ReturnItemPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D34EC9864BB16F987453D3FC20CBAE7746AF8C231C2ECEF98320593C9976982A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Marketplace_SE
{
    partial class ReturnItemPage : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2503")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // View\ReturnItemPage.xaml line 13
                {
                    this.Back_Button = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.Back_Button).Click += this.Click_Back;
                }
                break;
            case 3: // View\ReturnItemPage.xaml line 15
                {
                    this.Describe_TextBlock = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 4: // View\ReturnItemPage.xaml line 16
                {
                    this.Description_TextBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                }
                break;
            case 5: // View\ReturnItemPage.xaml line 17
                {
                    this.Moneyback_CheckBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.Moneyback_CheckBox).Click += this.Click_MoneyCheckBox;
                }
                break;
            case 6: // View\ReturnItemPage.xaml line 18
                {
                    this.Anotherproduct_CheckBox = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.Anotherproduct_CheckBox).Click += this.Click_ProductCheckBox;
                }
                break;
            case 7: // View\ReturnItemPage.xaml line 19
                {
                    this.Button_Return_Item = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.Button_Return_Item).Click += this.Click_Return_Item;
                }
                break;
            case 8: // View\ReturnItemPage.xaml line 20
                {
                    this.Display_TextBlock = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2503")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

