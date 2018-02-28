using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Windows.Forms;
using System.IO;

using GongSolutions.Wpf;
using GongSolutions.Wpf.DragDrop;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using Microsoft.VisualBasic;

using Valve.VR;
using OxyPlot;
using OxyPlot.Series;



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool initialized = false;
        private bool mouseMoving = false, mouseLeftButtonDown = false;
        private bool isDragging = false;
        private System.Windows.Point mouseStartPoint;
        private List<DragObject> dragList;
        private DragObject draggingTarget;
        private System.Windows.Point originalTargetPos;
        private string path = System.IO.Path.GetFullPath(".");

        //For drag & drop in grid
        private System.Windows.Point anchorPoint;
        private System.Windows.Point currentPoint;
        private bool isInDrag;

        private bool canSetImage = false;
        private Double temperature;
        private string applicationFilePath = "";

        private string[] batteryImages = new string[5];

        public string chargingStatus;

        //Viewport
        public ObservableCollection<Viewport> viewports { get; protected set; }

        public Canvas canvas;

        public MainWindow()
        {
           
            InitializeComponent();
            AddEventListener();
            InitializeDragList();
            InitializeViewport();
            InitializeBatteryImagePath();

            ComponentDispatcher.ThreadIdle += new EventHandler(SetImage);
            ComponentDispatcher.ThreadIdle += new EventHandler(SetBattery);
            ComponentDispatcher.ThreadIdle += new EventHandler(GetRAMUsage);
            ComponentDispatcher.ThreadIdle += new EventHandler(SetPCPerformance);

            timer.Enabled = true;
            timer.Start();
            timer.Elapsed += GetCPUUsage;
            timer.Elapsed += GetCPUUsageWMI;
            timer.AutoReset = true;
            
            //Initialize OpenVR & SteamVR and launch SteamVR application
            EVRInitError initError = new EVRInitError();
            OpenVR.Init(ref initError, EVRApplicationType.VRApplication_VRMonitor);

            GetViveControllerBattery();

            //Set DataContext of [plot1] to find its resources from another class (i.e. MainViewModel) other than [MainWindow] class
            MainViewModel m = new MainViewModel();
            plot1.DataContext = m;
        }

        private void InitializeViewport()
        {
            viewports = new ObservableCollection<Viewport>();

            for (int i = 1; i < 6; i++)
            {
                string imagePath = "pack://application:,,,/Resources/avatarIcon.png";
                Viewport vp = new Viewport("Viewport " + i, imagePath, "Viewport " + i);
                viewports.Add(vp);
                RaisePropertyChanged("viewports");
                this.DataContext = this;
            }
        }

        private void InitializeDragList()
        {
            dragList = new List<DragObject>();
            dragList.Add(new DragObject().Initialize(dragBtn1).SetDraggingMode(DraggingMode.Vertical)
                .SetRearrangeFlag(true));
            dragList.Add(new DragObject().Initialize(dragBtn2).SetDraggingMode(DraggingMode.Horizontal)
                .SetRearrangeFlag(true));
            dragList.Add(new DragObject().Initialize(dragBtn3).SetDraggingMode(DraggingMode.Both)
                 .SetRearrangeFlag(true));
            dragList.Add(new DragObject().Initialize(freeDragBtn).SetDraggingMode(DraggingMode.Both)
                .SetRearrangeFlag(false));

            //foreach (FrameworkElement element in scrollViewerWrapPanel.Children)
            //{
            //    dragList.Add(new DragObject().Initialize(element).SetDraggingMode(DraggingMode.Both).SetRearrangeFlag(true));
            //}
        }

        private void InitializeBatteryImagePath()
        {
            string folderPath = "pack://application:,,,/Resources/";

            //batteryImages[0] = folderPath + "low.png";
            //batteryImages[1] = folderPath + "medium.png";
            //batteryImages[2] = folderPath + "high.png";
            //batteryImages[3] = folderPath + "full.png";

            batteryImages[0] = folderPath + "off.png";
            batteryImages[1] = folderPath + "low.png";
            batteryImages[2] = folderPath + "medium.png";
            batteryImages[3] = folderPath + "high.png";
            batteryImages[4] = folderPath + "full.png";
        }

        private void ChangeColorBtn_Click(object sender, RoutedEventArgs e)
        {
            TextBlock txt = new TextBlock();
            txt.Text = "CLICKED!!";
            txt.Foreground = System.Windows.Media.Brushes.Blue;
            changeColorBtn.Content = txt;
        }

        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            if (dlg.ShowDialog() == true)
            {
                Console.WriteLine(dlg.FileName);
                BackgroundImage.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void AddEventListener()
        {
            changeColorBtn.Click += ChangeColorBtn_Click;
            browseBtn.Click += BrowseBtn_Click;
            colorPicker.SelectedColorChanged += OnColorPickerValueChanged;
            colorSpectrumSlider.ValueChanged += OnColorSliderValueChanged;

            dragBtn1.PreviewMouseLeftButtonUp += dragObject_PreviewMouseLeftButtonUp;
            dragBtn1.PreviewMouseLeftButtonDown += dragObject_PreviewMouseLeftButtonDown;
            dragBtn1.PreviewMouseMove += vertical_PreviewMouseMove;
            dragBtn2.PreviewMouseLeftButtonUp += dragObject_PreviewMouseLeftButtonUp;
            dragBtn2.PreviewMouseLeftButtonDown += dragObject_PreviewMouseLeftButtonDown;
            dragBtn2.PreviewMouseMove += horizontal_PreviewMouseMove;
            dragBtn3.PreviewMouseLeftButtonUp += dragObject_PreviewMouseLeftButtonUp;
            dragBtn3.PreviewMouseLeftButtonDown += dragObject_PreviewMouseLeftButtonDown;
            dragBtn3.PreviewMouseMove += free_PreviewMouseMove;
            BackgroundImage.MouseMove += BackgroundImage_MouseMove;

            freeDragBtn.PreviewMouseLeftButtonDown += dragObject_PreviewMouseLeftButtonDown;
            freeDragBtn.PreviewMouseMove += free_PreviewMouseMove;

            fbIcon.PreviewMouseDoubleClick += dragObject_PreviewMouseDoubleClick;
            avatarIcon.PreviewMouseDoubleClick += dragObject_PreviewMouseDoubleClick;
            pictureBox1.PreviewMouseDoubleClick += dragObject_PreviewMouseDoubleClick;
            screenImageBox.PreviewMouseDoubleClick += dragObject_PreviewMouseDoubleClick;

            mainWindow.SizeChanged += MainWindow_SizeChanged;

            //foreach (FrameworkElement element in scrollViewerWrapPanel.Children)
            //{
            //    element.PreviewMouseDown += grid_MouseLeftButtonDown;
            //    element.PreviewMouseUp += grid_MouseLeftButtonUp;
            //    element.PreviewMouseMove += grid_MouseMove;
            //}
            fbIcon.PreviewMouseMove += grid_MouseMove;
            avatarIcon.PreviewMouseMove += grid_MouseMove;


            Console.WriteLine("All listeners are added.");
        }

        //Maximize / Minimize event
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }


        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            scrollViewer.Width = mainWindow.ActualWidth - 15;
            // scrollViewer.Height = Double.NaN;
            scrollViewerGrid.Width = mainWindow.ActualWidth;
            //scrollViewerGrid.Height = scrollViewerWrapPanel.Height + 15;
            // scrollViewerGrid.Height = 

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(viewportControl); i++)
            {
                var child = VisualTreeHelper.GetChild(viewportControl, i) as FrameworkElement;

                if (child != null && child is WrapPanel)
                {
                    WrapPanel wp = child as WrapPanel;
                    scrollViewerGrid.Height = wp.Height + 15;
                    Console.WriteLine("OK");
                }
            }
        }

        private void dragObject_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Control cont = sender as System.Windows.Controls.Control;

            if (!Selector.GetIsSelected(cont))
            {
                foreach (System.Windows.Controls.Control c in drapDropCanvas.Children)
                {
                    Selector.SetIsSelected(c, false);
                    Canvas.SetZIndex(c, Canvas.GetZIndex(cont) - 1);
                }
                Selector.SetIsSelected(cont, true);
                Canvas.SetZIndex(cont, 100);
            }
            else
            {
                Selector.SetIsSelected(cont, false);
            }
        }

        private void BackgroundImage_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void SetImage(object sender, EventArgs e)
        {
            if (canSetImage)
            {
                screenImage.Source = CopyScreen();
                Canvas.SetZIndex(screenImage, 100);
            }

        }

        private void OnColorPickerValueChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            colorRect.Fill = new SolidColorBrush(e.NewValue.Value);
            colorName.Text = colorPicker.SelectedColorText;
        }

        private void OnColorSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized)
            {
                colorRect.Fill = new SolidColorBrush(colorSpectrumSlider.SelectedColor);
            }
            initialized = true;
            colorName.Text = System.Drawing.ColorTranslator.FromHtml(colorSpectrumSlider.SelectedColor.ToString()).ToString();
        }

        private BitmapSource CopyScreen()
        {
            using (var screenBmp = new Bitmap(
             (int)SystemParameters.PrimaryScreenWidth,
             (int)SystemParameters.PrimaryScreenHeight,
             System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = Graphics.FromImage(screenBmp))
                {
                    bmpGraphics.CopyFromScreen(0, 0, 0, 0, screenBmp.Size);
                    return Imaging.CreateBitmapSourceFromHBitmap(
                        screenBmp.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }

        private void horizontal_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mouseLeftButtonDown)
            {
                Drag(sender, DraggingMode.Horizontal, e);
            }
        }

        private void vertical_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mouseLeftButtonDown)
            {
                Drag(sender, DraggingMode.Vertical, e);
            }
        }

        private void free_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mouseLeftButtonDown)
            {
                Drag(sender, DraggingMode.Both, e);
            }
        }

        private void dragObject_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseLeftButtonDown = true;
            mouseStartPoint = e.GetPosition(null);
            System.Windows.Controls.Control control = sender as System.Windows.Controls.Control;
            originalTargetPos = GetPoint(control);
            control.CaptureMouse();

            foreach (DragObject c in dragList)
            {
                System.Windows.Controls.Control con = sender as System.Windows.Controls.Control;
                if (c.target == con)
                {
                    draggingTarget = c;
                    Canvas.SetZIndex(draggingTarget.target, 2);
                    Canvas.SetZIndex(dragCanvas, 2);
                }
            }
        }

        //Rearrangement
        private void dragObject_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mouseLeftButtonDown = false;
            bool arranged = false;

            if (isDragging)
            {
                isDragging = false;

                foreach (DragObject c in dragList)
                {
                    System.Windows.Controls.Control con = sender as System.Windows.Controls.Control;
                    con.ReleaseMouseCapture();
                    if (draggingTarget.rearrange)
                    {
                        if (c.rearrange)
                        {
                            if (draggingTarget.draggingMode == DraggingMode.Horizontal)
                            {
                                //if (draggingTarget.target.Margin.Left > c.target.Margin.Left &&
                                //    draggingTarget.target.Margin.Left < c.target.Margin.Left + c.target.Width &&
                                //    draggingTarget.target.Margin.Top == c.target.Margin.Top)
                                if (Canvas.GetLeft(draggingTarget.target) > Canvas.GetLeft(c.target) &&
                                   Canvas.GetLeft(draggingTarget.target) < Canvas.GetLeft(c.target) + c.target.Width &&
                                   Canvas.GetTop(draggingTarget.target) == Canvas.GetTop(c.target))
                                {
                                    //draggingTarget.target.Margin = c.target.Margin;
                                    //c.target.Margin = originalTargetPos;
                                    SetPoint(draggingTarget.target, GetPoint(c.target));
                                    SetPoint(c.target, originalTargetPos);
                                    arranged = true;
                                    Console.WriteLine("Rearrange");
                                }
                            }
                            else if (draggingTarget.draggingMode == DraggingMode.Vertical)
                            {
                                if (Canvas.GetTop(draggingTarget.target) > Canvas.GetTop(c.target) &&
                                    Canvas.GetTop(draggingTarget.target) < Canvas.GetTop(c.target) + c.target.Height &&
                                    Canvas.GetLeft(draggingTarget.target) == Canvas.GetLeft(c.target))
                                {
                                    SetPoint(draggingTarget.target, GetPoint(c.target));
                                    SetPoint(c.target, originalTargetPos);
                                    arranged = true;
                                    Console.WriteLine("Rearrange");
                                }
                            }
                            else if (draggingTarget.draggingMode == DraggingMode.Both)
                            {
                                if (Canvas.GetLeft(draggingTarget.target) > Canvas.GetLeft(c.target) &&
                                    Canvas.GetLeft(draggingTarget.target) < Canvas.GetLeft(c.target) + c.target.Width &&
                                    Canvas.GetTop(draggingTarget.target) > Canvas.GetTop(c.target) &&
                                    Canvas.GetTop(draggingTarget.target) < Canvas.GetTop(c.target) + c.target.Height)
                                {
                                    SetPoint(draggingTarget.target, GetPoint(c.target));
                                    SetPoint(c.target, originalTargetPos);
                                    arranged = true;
                                    Console.WriteLine("Rearrange");
                                }
                            }
                        }
                    }
                }

                if (!arranged)
                {
                    SetPoint(draggingTarget.target, originalTargetPos);
                    Console.WriteLine("No Intersection");
                }
                Canvas.SetZIndex(draggingTarget.target, 0);
                Canvas.SetZIndex(dragCanvas, 0);
            }
        }

        private void Drag(object target, DraggingMode draggingMode, System.Windows.Input.MouseEventArgs e)
        {
            mouseMoving = true;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                System.Windows.Controls.Control targetControl = target as System.Windows.Controls.Control;
                System.Windows.Point position = e.GetPosition(dragCanvas);

                if (Math.Abs(position.X - mouseStartPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - mouseStartPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    isDragging = true;

                    double x = position.X - targetControl.Width / 2;
                    double y = position.Y - targetControl.Height / 2;
                    //Console.WriteLine(position.X + ":" + position.Y + " ~ " + targetControl.Margin.Left + ":" + targetControl.Margin.Top);
                    switch (draggingMode)
                    {
                        case DraggingMode.Horizontal:
                            Canvas.SetLeft(targetControl, x);
                            break;
                        case DraggingMode.Vertical:
                            Canvas.SetTop(targetControl, y);
                            break;
                        case DraggingMode.Both:
                            //targetControl.Margin = new Thickness(x, y, 0, 0);
                            Canvas.SetLeft(targetControl, x);
                            Canvas.SetTop(targetControl, y);
                            break;
                    }
                }
            }
        }

        private void SetPoint(System.Windows.UIElement element, double x, double y)
        {
            Canvas.SetLeft(element, x);
            Canvas.SetTop(element, y);
        }

        private void SetPoint(System.Windows.UIElement element, System.Windows.Point point)
        {
            Canvas.SetLeft(element, point.X);
            Canvas.SetTop(element, point.Y);
        }

        private System.Windows.Point GetPoint(System.Windows.UIElement element)
        {
            double x = Canvas.GetLeft(element);
            double y = Canvas.GetTop(element);

            return new System.Windows.Point(x, y);
        }

        private FrameworkElement draggingViewport = new FrameworkElement();
        //For drag & drop in grid
        private void grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            anchorPoint = e.GetPosition(null);
            if (element != null) element.CaptureMouse();
            e.Handled = true;
            originalTargetPos = GetPoint(element);
            draggingViewport = element;
        }

        private object overlappingElement = new object();
        private TranslateTransform transform = new TranslateTransform();

        private void grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                isInDrag = true;
            }
            else
            {
                isInDrag = false;
            }
            if (!isInDrag) return;
            currentPoint = e.GetPosition(null);
            var element = sender as FrameworkElement;
            isDragging = true;

            transform.X += currentPoint.X - anchorPoint.X;
            transform.Y += currentPoint.Y - anchorPoint.Y;

            if (element == draggingViewport)
            {
                element.RenderTransform = transform;
                element.RenderTransformOrigin = new System.Windows.Point(transform.X, transform.Y);
            }
            anchorPoint = currentPoint;
        }

        private void grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isInDrag) return;
            var element = sender as System.Windows.UIElement;
            mouseLeftButtonDown = false;
            bool arranged = false;

            if (isDragging)
            {

                //Reset transform back to original position
                //if (!arranged)
                //{
                //    if (element != null) element.ReleaseMouseCapture();
                //    element.RenderTransform = new TranslateTransform();
                //    transform = new TranslateTransform();
                //    Grid.SetZIndex(element, 1);
                //    Console.WriteLine("No Intersection");
                //}

                try
                {
                    Grid.SetZIndex(draggingViewport, 1);
                    Grid.SetZIndex(dragCanvas, 1);
                }
                catch
                {

                }
                transform = new TranslateTransform();

                element.ReleaseMouseCapture();
                draggingViewport = null;
                isDragging = false;
                isInDrag = false;
                e.Handled = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            DependencyObject parent = btn.Parent;
            Console.WriteLine("Delete button clicked");
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var result = child is System.Windows.Controls.Label ? child : null;

                if (result != null)
                {
                    Console.WriteLine("Child Found");
                    System.Windows.Controls.Label l = (System.Windows.Controls.Label)result;
                    foreach (Viewport v in viewports)
                    {
                        if (l.Content.ToString() == v.labelText)
                        {
                            Grid parentGrid = new Grid();
                            parentGrid = (Grid)viewportControl.ItemContainerGenerator.ContainerFromItem((Grid)parent);
                            viewportControl.Items.Remove(parentGrid);
                        }
                    }
                }
            }
        }

        //Shut Down
        private void shutdownButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown", "/s /t 0");
        }

        //Restart
        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("shutdown", "/r /t 0");
        }

        //Sleep
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        private void sleepButton_Click(object sender, RoutedEventArgs e)
        {
            SetSuspendState(false, true, true);
        }
       
        //Log off
        [DllImport("user32")]
        public static extern void LockWorkStation();
        private void logoffButton_Click(object sender, RoutedEventArgs e)
        {
            LockWorkStation();
        }

        //Get temperature
        public Double GetTemperature()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
            foreach (ManagementObject obj in searcher.Get())
            {
                Double temp = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                temperature = (temp - 2732) / 10.0;
            }
            return temperature;
        }

        private void browseApplication_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "EXE files | *.exe";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    if (dlg.FileName != null)
                    {
                        applicationFilePath = dlg.FileName;

                        //Getting only file name from full file path
                        applicationPathTextBox.Text = System.IO.Path.GetFileName(dlg.FileName);
                        
                        ////Getting full file path
                        //applicationPathTextBox.Text = dlg.FileName;
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error " + ex.Message);
                }
            }
        }

        private void screenViewToggle_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            
            if(toggleButton.IsChecked == true)
            {
                canSetImage = true;
                screenImage.Visibility = Visibility.Visible;
            }
            else
            {
                canSetImage = false;
                screenImage.Visibility = Visibility.Hidden;
            }
        }

        private void launchApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            if(applicationFilePath != "")
            {
                Process.Start(applicationFilePath);
            }
        }

        public string GetBatteryStatus()
        {
            chargingStatus = SystemInformation.PowerStatus.PowerLineStatus.ToString();
            if (chargingStatus == "Online")
            {
                chargingStatus = "Plugged";
            }
            else if (chargingStatus == "Offline")
            {
                chargingStatus = "Unplugged";
            }
            return (SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString();
        }

        private void SetBattery(object sender, EventArgs e)
        {
            batteryLabel.Content = GetBatteryStatus() + " % " + chargingStatus;
        }

        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        string cpuUsage = "";
        string ramUsage = "";
        string controllerBattery = "";
        private void SetPCPerformance(object sender, EventArgs e)
        {
            GetViveControllerBattery();
            
            UpdateGraph();
            pcPerformanceLabel.Content = "CPU : " + cpuUsage + " WMI : " + cpuUsageWMI + "\n" + ramUsage + "\n" + controllerBattery;
        }

        //Copy & Past openvr_api.dll [32bit version - Can be found in Unity folder] into **FilePath/bin/Debug [together with the build *.exe]
        private void GetViveControllerBattery()
        {
            CVRSystem hmd = OpenVR.System;
            ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
            float leftBat = hmd.GetFloatTrackedDeviceProperty(hmd.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.LeftHand), ETrackedDeviceProperty.Prop_DeviceBatteryPercentage_Float, ref error);
            float rightBat = hmd.GetFloatTrackedDeviceProperty(hmd.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.RightHand), ETrackedDeviceProperty.Prop_DeviceBatteryPercentage_Float, ref error);
            leftBat = leftBat * 100f;
            rightBat = rightBat * 100f;
            int leftBatInt = (int)leftBat;
            int rightBatInt = (int)rightBat;
            string leftBatString = ((int)leftBat).ToString() + " %";
            string rightBatString = ((int)rightBat).ToString() + " %";

            if(leftBatInt == 0)
            {
                leftBatString = "Off";
            }
            if(rightBatInt == 0)
            {
                rightBatString = "Off";
            }
            controllerBattery = "Controller Battery : Left - " + leftBatString + " / Right - " + rightBatString;

            ChangeBatteryImage(leftBatInt, rightBatInt);
        }

        private void ChangeBatteryImage(int left, int right)
        {
            int leftIndex = 5, rightIndex = 5;
            if(left > 0)
            {
                leftIndex = 1;
                if (left > 25)
                {
                    leftIndex = 2;
                    if (left > 50)
                    {
                        leftIndex = 3;
                        if (left > 75)
                        {
                            leftIndex = 4;
                        }
                    }
                }
            }
            else
            {
                leftIndex = 0;
            }
            leftBatteryIndicator.Source = new BitmapImage(new Uri(batteryImages[leftIndex]));
            leftBatteryPercentage.Content = left.ToString();
            if(left <= 0)
            {
                leftBatteryPercentage.Content = "OFF";
            }

            if (right > 0)
            {
                rightIndex = 1;
                if (right > 25)
                {
                    rightIndex = 2;
                    if (right > 50)
                    {
                        rightIndex = 3;
                        if (right > 75)
                        {
                            rightIndex = 4;
                        }
                    }
                }
            }
            else
            {
                rightIndex = 0;
            }
            rightBatteryIndicator.Source = new BitmapImage(new Uri(batteryImages[rightIndex]));
            rightBatteryPercentage.Content = right.ToString();
            if (right <= 0)
            {
                rightBatteryPercentage.Content = "OFF";
            }
        }

        Double cpuUsageDouble = 0f;
        private async void GetCPUUsage(object sender, EventArgs e)
        {
            //https://social.technet.microsoft.com/wiki/contents/articles/12984.understanding-processor-processor-time-and-process-processor-time.aspx
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            //CPU
            //cpuUsage = Math.Round((cpuCounter.NextValue() / Environment.ProcessorCount), 2) + " %";
            cpuCounter.NextValue();
            await Task.Delay(1000);
            cpuUsageDouble = Math.Round(cpuCounter.NextValue(), 2);
            cpuUsage = cpuUsageDouble + " %";
        }

        double cpuUsageWMIDouble = 0f;
        string cpuUsageWMI;
        private void GetCPUUsageWMI(object sender, EventArgs e)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
            var cpuTimes = searcher.Get().Cast<ManagementObject>().Select(mo => new { Name = mo["Name"], Usage = mo["PercentProcessorTime"] }).ToList();

            int sum = 0;
            int average = 0;

            cpuUsageWMIDouble = Convert.ToDouble(cpuTimes[0].Usage);
            cpuUsageWMI = cpuTimes[0].Usage.ToString() + " %";
        }

        private void GetRAMUsage(object sender, EventArgs e)
        {
            //RAM
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            string ramUsageData = ramCounter.NextValue() / 1000f + " GB";
            Microsoft.VisualBasic.Devices.ComputerInfo computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            ulong totalMemory = computerInfo.TotalPhysicalMemory;
            double ramUsageInDouble = Math.Round(((((totalMemory / 1000000000f) - (ramCounter.NextValue() / 1000f)) / (totalMemory / 1000000000f)) * 100), 2);
            double currentRamUsage = Math.Round((totalMemory / 1000000000f) - (ramCounter.NextValue() / 1000f), 2);
            double totalRam = Math.Round(totalMemory / 1000000000f, 2);
            ramUsageData = ramUsageInDouble.ToString() + " %";
            ramUsage = "RAM: " + currentRamUsage + " / " + totalRam + " (" + ramUsageData + ")";
        }

        private double timerX = 0;

        public void UpdateGraph()
        {
            MainViewModel.AddPoint(new DataPoint(timerX, cpuUsageWMIDouble));
            timerX += 0.01;
            
            plot1.InvalidatePlot(true);
        }
    }


    //A class dedicated to place all resources for controls binding / to find out of the [MainWindow] class
    public class MainViewModel
    {
        public string Title { get; set; }

        //Is static so that no need instantiate MainViewModel to use this list;
        //Oxyplot use DataPoint as datatype for graph data (allows floating point numbers) whereas *Point only allows Integer)
        public static IList<DataPoint> Points { get; set; }

        //Automatically called when controls tries to find resources from this class
        public MainViewModel()
        {
            this.Title = "Example 2";
            MainViewModel.Points = new List<DataPoint>();
        }

        //Is static so that no need instantiate MainViewModel to call this method
        public static void AddPoint(DataPoint point)
        {
            Points.Add(point);

            if (Points.Count > 1000)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    DataPoint newPoint = new DataPoint();
                    newPoint = new DataPoint(Points[i].X - 0.01, Points[i].Y);
                    Points[i] = newPoint;
                }

                Points.RemoveAt(0);
            }
        }

    }

    public enum DraggingMode
    {
        Horizontal,
        Vertical,
        Both
    }

    public class DragObject
    {
        public System.Drawing.Rectangle bound;
        public System.Windows.Shapes.Rectangle outline;
        public System.Windows.FrameworkElement target;
        public DraggingMode draggingMode = DraggingMode.Both;
        public bool rearrange = false;

        public DragObject Initialize(FrameworkElement target)
        {
            this.target = target;
            InitializeBound();

            return this;
        }

        private void InitializeBound()
        {
            bound = new System.Drawing.Rectangle((int)target.Margin.Left, (int)target.Margin.Top, (int)target.Width, (int)target.Height);
            bound.Location = new System.Drawing.Point((int)target.Margin.Left, (int)target.Margin.Top);
            bound.Width = (int)target.Width;
            bound.Height = (int)target.Height;

            int x = (int)(target.Margin.Left + target.Width / 2);
            int y = (int)(target.Margin.Top + target.Height / 2);
            outline = new System.Windows.Shapes.Rectangle()
            {
                Width = (int)target.Width,
                Height = (int)target.Height,
                Stroke = System.Windows.Media.Brushes.Red,
                StrokeThickness = 3,
                Visibility = Visibility.Visible,

                Margin = new Thickness(bound.Location.X, bound.Location.Y, target.Width, target.Height),
            };
        }

        public DragObject SetDraggingMode(DraggingMode mode)
        {
            draggingMode = mode;

            return this;
        }

        public DragObject SetRearrangeFlag(bool rearrangeOrNot)
        {
            rearrange = rearrangeOrNot;

            return this;
        }

        public DragObject DrawOutline(Grid grid)
        {
            grid.Children.Add(outline);

            return this;
        }

        public System.Collections.ObjectModel.ObservableCollection<System.Windows.Shapes.Rectangle> items;
        private void Add()
        {
            var rect = new System.Windows.Shapes.Rectangle
            {
                Stroke = System.Windows.Media.Brushes.Red,
                StrokeThickness = 1,
                Height = 10,
                Width = 100
            };
            items.Add(rect);
        }
    }

    public class Viewport : INotifyPropertyChanged
    {
        public BitmapImage viewportImage { get; set; }
        public string imagePath;
        public string labelText { get; set; }
        public string name { get; set; }
        public System.Windows.Controls.Button editButton, deleteButton;

        public Viewport(string name, string imagePath, string label)
        {
            this.name = name;
            this.viewportImage = new BitmapImage(new Uri(imagePath));
            this.imagePath = imagePath;
            this.labelText = label;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
    
}

