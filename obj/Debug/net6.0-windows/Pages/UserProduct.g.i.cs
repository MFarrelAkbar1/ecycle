﻿#pragma checksum "..\..\..\..\Pages\UserProduct.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67BA70600D7D76580022DC9661106D4D49F8DAE9"
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


namespace Ecycle.Pages {
    
    
    /// <summary>
    /// UserProduct
    /// </summary>
    public partial class UserProduct : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 45 "..\..\..\..\Pages\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ProductList;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Pages\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ProductForm;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\Pages\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FormTitle;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\Pages\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProductName;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\Pages\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtProductNamePlaceholder;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Pages\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProductDescription;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\Pages\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtProductDescriptionPlaceholder;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\Pages\UserProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Ecycle;component/pages/userproduct.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\UserProduct.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 35 "..\..\..\..\Pages\UserProduct.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BackButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 41 "..\..\..\..\Pages\UserProduct.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddProduct_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ProductList = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            
            #line 62 "..\..\..\..\Pages\UserProduct.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditProduct_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 66 "..\..\..\..\Pages\UserProduct.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteProduct_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ProductForm = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.FormTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.txtProductName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtProductNamePlaceholder = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.txtProductDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.txtProductDescriptionPlaceholder = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\..\..\Pages\UserProduct.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.SaveProduct_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

