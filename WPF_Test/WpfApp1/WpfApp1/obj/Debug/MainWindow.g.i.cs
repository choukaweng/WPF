#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5012549D72A9418803C8E9F36712A02A2B4A61A4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using OxyPlot.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
using WpfApp1;
using WpfApp1.Properties;
using XamlGeneratedNamespace;
using Xceed.Wpf.DataGrid;
using Xceed.Wpf.DataGrid.Automation;
using Xceed.Wpf.DataGrid.Converters;
using Xceed.Wpf.DataGrid.FilterCriteria;
using Xceed.Wpf.DataGrid.Markup;
using Xceed.Wpf.DataGrid.ValidationRules;
using Xceed.Wpf.DataGrid.Views;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace WpfApp1
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 75 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;

#line default
#line hidden


#line 76 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image BackgroundImage;

#line default
#line hidden


#line 78 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid functionGrid;

#line default
#line hidden


#line 86 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton screenViewToggle;

#line default
#line hidden


#line 89 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button browseBtn;

#line default
#line hidden


#line 92 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button changeColorBtn;

#line default
#line hidden


#line 97 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.ColorSpectrumSlider colorSpectrumSlider;

#line default
#line hidden


#line 108 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.ColorPicker colorPicker;

#line default
#line hidden


#line 109 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock colorName;

#line default
#line hidden


#line 110 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas dragCanvas;

#line default
#line hidden


#line 111 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button dragBtn1;

#line default
#line hidden


#line 112 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button dragBtn2;

#line default
#line hidden


#line 113 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button dragBtn3;

#line default
#line hidden


#line 114 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button freeDragBtn;

#line default
#line hidden


#line 116 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle colorRect;

#line default
#line hidden


#line 120 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas drapDropCanvas;

#line default
#line hidden


#line 122 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl fbIcon;

#line default
#line hidden


#line 133 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl avatarIcon;

#line default
#line hidden


#line 144 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl pictureBox1;

#line default
#line hidden


#line 149 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image1;

#line default
#line hidden


#line 154 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl screenImageBox;

#line default
#line hidden


#line 158 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image screenImage;

#line default
#line hidden


#line 164 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollViewer;

#line default
#line hidden


#line 165 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid scrollViewerGrid;

#line default
#line hidden


#line 170 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl viewportControl;

#line default
#line hidden


#line 180 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sleepButton;

#line default
#line hidden


#line 181 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button shutdownButton;

#line default
#line hidden


#line 182 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button restartButton;

#line default
#line hidden


#line 183 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logoffButton;

#line default
#line hidden


#line 184 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label batteryLabel;

#line default
#line hidden


#line 185 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button browseApplication;

#line default
#line hidden


#line 186 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox applicationPathTextBox;

#line default
#line hidden


#line 187 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button launchApplicationButton;

#line default
#line hidden


#line 188 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label pcPerformanceLabel;

#line default
#line hidden


#line 193 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image leftBatteryIndicator;

#line default
#line hidden


#line 194 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image rightBatteryIndicator;

#line default
#line hidden


#line 195 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label leftBatteryPercentage;

#line default
#line hidden


#line 196 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label rightBatteryPercentage;

#line default
#line hidden


#line 199 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal OxyPlot.Wpf.Plot plot1;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/mainwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\MainWindow.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.mainWindow = ((WpfApp1.MainWindow)(target));
                    return;
                case 2:
                    this.mainGrid = ((System.Windows.Controls.Grid)(target));
                    return;
                case 3:
                    this.BackgroundImage = ((System.Windows.Controls.Image)(target));
                    return;
                case 4:
                    this.functionGrid = ((System.Windows.Controls.Grid)(target));
                    return;
                case 5:
                    this.screenViewToggle = ((System.Windows.Controls.Primitives.ToggleButton)(target));

#line 86 "..\..\MainWindow.xaml"
                    this.screenViewToggle.Click += new System.Windows.RoutedEventHandler(this.screenViewToggle_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.browseBtn = ((System.Windows.Controls.Button)(target));
                    return;
                case 7:
                    this.changeColorBtn = ((System.Windows.Controls.Button)(target));
                    return;
                case 8:
                    this.colorSpectrumSlider = ((Xceed.Wpf.Toolkit.ColorSpectrumSlider)(target));
                    return;
                case 9:
                    this.colorPicker = ((Xceed.Wpf.Toolkit.ColorPicker)(target));
                    return;
                case 10:
                    this.colorName = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 11:
                    this.dragCanvas = ((System.Windows.Controls.Canvas)(target));
                    return;
                case 12:
                    this.dragBtn1 = ((System.Windows.Controls.Button)(target));
                    return;
                case 13:
                    this.dragBtn2 = ((System.Windows.Controls.Button)(target));
                    return;
                case 14:
                    this.dragBtn3 = ((System.Windows.Controls.Button)(target));
                    return;
                case 15:
                    this.freeDragBtn = ((System.Windows.Controls.Button)(target));
                    return;
                case 16:
                    this.colorRect = ((System.Windows.Shapes.Rectangle)(target));
                    return;
                case 17:
                    this.drapDropCanvas = ((System.Windows.Controls.Canvas)(target));
                    return;
                case 18:
                    this.fbIcon = ((System.Windows.Controls.ContentControl)(target));
                    return;
                case 19:
                    this.avatarIcon = ((System.Windows.Controls.ContentControl)(target));
                    return;
                case 20:
                    this.pictureBox1 = ((System.Windows.Controls.ContentControl)(target));
                    return;
                case 21:
                    this.image1 = ((System.Windows.Controls.Image)(target));
                    return;
                case 22:
                    this.screenImageBox = ((System.Windows.Controls.ContentControl)(target));
                    return;
                case 23:
                    this.screenImage = ((System.Windows.Controls.Image)(target));
                    return;
                case 24:
                    this.scrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
                    return;
                case 25:
                    this.scrollViewerGrid = ((System.Windows.Controls.Grid)(target));
                    return;
                case 26:
                    this.viewportControl = ((System.Windows.Controls.ItemsControl)(target));
                    return;
                case 27:
                    this.sleepButton = ((System.Windows.Controls.Button)(target));

#line 180 "..\..\MainWindow.xaml"
                    this.sleepButton.Click += new System.Windows.RoutedEventHandler(this.sleepButton_Click);

#line default
#line hidden
                    return;
                case 28:
                    this.shutdownButton = ((System.Windows.Controls.Button)(target));

#line 181 "..\..\MainWindow.xaml"
                    this.shutdownButton.Click += new System.Windows.RoutedEventHandler(this.shutdownButton_Click);

#line default
#line hidden
                    return;
                case 29:
                    this.restartButton = ((System.Windows.Controls.Button)(target));

#line 182 "..\..\MainWindow.xaml"
                    this.restartButton.Click += new System.Windows.RoutedEventHandler(this.restartButton_Click);

#line default
#line hidden
                    return;
                case 30:
                    this.logoffButton = ((System.Windows.Controls.Button)(target));

#line 183 "..\..\MainWindow.xaml"
                    this.logoffButton.Click += new System.Windows.RoutedEventHandler(this.logoffButton_Click);

#line default
#line hidden
                    return;
                case 31:
                    this.batteryLabel = ((System.Windows.Controls.Label)(target));
                    return;
                case 32:
                    this.browseApplication = ((System.Windows.Controls.Button)(target));

#line 185 "..\..\MainWindow.xaml"
                    this.browseApplication.Click += new System.Windows.RoutedEventHandler(this.browseApplication_Click);

#line default
#line hidden
                    return;
                case 33:
                    this.applicationPathTextBox = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 34:
                    this.launchApplicationButton = ((System.Windows.Controls.Button)(target));

#line 187 "..\..\MainWindow.xaml"
                    this.launchApplicationButton.Click += new System.Windows.RoutedEventHandler(this.launchApplicationButton_Click);

#line default
#line hidden
                    return;
                case 35:
                    this.pcPerformanceLabel = ((System.Windows.Controls.Label)(target));
                    return;
                case 36:
                    this.leftBatteryIndicator = ((System.Windows.Controls.Image)(target));
                    return;
                case 37:
                    this.rightBatteryIndicator = ((System.Windows.Controls.Image)(target));
                    return;
                case 38:
                    this.leftBatteryPercentage = ((System.Windows.Controls.Label)(target));
                    return;
                case 39:
                    this.rightBatteryPercentage = ((System.Windows.Controls.Label)(target));
                    return;
                case 40:
                    this.plot1 = ((OxyPlot.Wpf.Plot)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window mainWindow;
    }
}

