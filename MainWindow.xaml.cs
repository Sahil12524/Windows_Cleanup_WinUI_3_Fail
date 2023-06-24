// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices; // For DllImport
using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Windows_Cleanup_WinUI_3.Views;
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Windows_Cleanup_WinUI_3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        WindowsSystemDispatcherQueueHelper m_wsdqHelper; // See below for implementation.
        MicaController m_backdropController;
        SystemBackdropConfiguration m_configurationSource;

        private NavigationViewItem _lastItem;

        private AppWindow m_AppWindow;

        public MainWindow()
        {
            this.InitializeComponent();
            m_AppWindow = GetAppWindowForCurrentWindow();
            m_AppWindow.Changed += AppWindow_Changed;
            bool isTallTitleBar = true;
            // A taller title bar is only supported when drawing a fully custom title bar
            if (AppWindowTitleBar.IsCustomizationSupported() && m_AppWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                if (isTallTitleBar)
                {
                    // Choose a tall title bar to provide more room for interactive elements 
                    // like search box or person picture controls.
                    m_AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
                }
                else
                {
                    //_mainAppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Standard;
                }
                // Recalculate the drag region for the custom title bar 
                // if you explicitly defined new draggable areas.
                SetDragRegionForCustomTitleBar(m_AppWindow);
            }
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                var titleBar = m_AppWindow.TitleBar;
                titleBar.ExtendsContentIntoTitleBar = true;
                AppTitleBar.Loaded += AppTitleBar_Loaded;
                AppTitleBar.SizeChanged += AppTitleBar_SizeChanged;
                titleBar.IconShowOptions = IconShowOptions.ShowIconAndSystemMenu;
                m_AppWindow.SetIcon("Assets/cleaning broom.ico");
                SetTitleBarColors();
            }
            else
            {
                // In the case that title bar customization is not supported, hide the custom title bar
                // element.
                AppTitleBar.Visibility = Visibility.Collapsed;

                // Show alternative UI for any functionality in
                // the title bar, such as search.
            }
            var exists = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1) System.Diagnostics.Process.GetCurrentProcess().Kill();
            //ExtendsContentIntoTitleBar = true;
            //SetTitleBar(AppTitleBar);
            Activated += MainWindow_Activated;
            TrySetSystemBackdrop();
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }

        [DllImport("Shcore.dll", SetLastError = true)]
        internal static extern int GetDpiForMonitor(IntPtr hmonitor, Monitor_DPI_Type dpiType, out uint dpiX, out uint dpiY);

        internal enum Monitor_DPI_Type : int
        {
            MDT_Effective_DPI = 0,
            MDT_Angular_DPI = 1,
            MDT_Raw_DPI = 2,
            MDT_Default = MDT_Effective_DPI
        }

        private double GetScaleAdjustment()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
            IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

            // Get DPI.
            var result = GetDpiForMonitor(hMonitor, Monitor_DPI_Type.MDT_Default, out var dpiX, out var _);
            if (result != 0)
            {
                throw new Exception("Could not get DPI for monitor.");
            }

            var scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
            return scaleFactorPercent / 100.0;
        }

        private void SetDragRegionForCustomTitleBar(AppWindow appWindow)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported()
                && appWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                var scaleAdjustment = GetScaleAdjustment();

                RightPaddingColumn.Width = new GridLength(appWindow.TitleBar.RightInset / scaleAdjustment);
                LeftPaddingColumn.Width = new GridLength(appWindow.TitleBar.LeftInset / scaleAdjustment);

                List<Windows.Graphics.RectInt32> dragRectsList = new();

                Windows.Graphics.RectInt32 dragRectL;
                dragRectL.X = (int)((LeftPaddingColumn.ActualWidth) * scaleAdjustment);
                dragRectL.Y = 0;
                dragRectL.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
                dragRectL.Width = (int)((IconColumn.ActualWidth
                                        + TitleColumn.ActualWidth
                                        + LeftDragColumn.ActualWidth) * scaleAdjustment);
                dragRectsList.Add(dragRectL);

                Windows.Graphics.RectInt32 dragRectR;
                dragRectR.X = (int)((LeftPaddingColumn.ActualWidth
                                    + IconColumn.ActualWidth
                                    + AppTitleTextBlock.ActualWidth
                                    + LeftDragColumn.ActualWidth) * scaleAdjustment);
                dragRectR.Y = 0;
                dragRectR.Height = (int)(AppTitleBar.ActualHeight * scaleAdjustment);
                dragRectR.Width = (int)(RightDragColumn.ActualWidth * scaleAdjustment);
                dragRectsList.Add(dragRectR);

                Windows.Graphics.RectInt32[] dragRects = dragRectsList.ToArray();

                appWindow.TitleBar.SetDragRectangles(dragRects);
            }
        }

        private void AppWindow_Changed(AppWindow sender, AppWindowChangedEventArgs args)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (args.DidPresenterChange
                && AppWindowTitleBar.IsCustomizationSupported())
            {
                switch (sender.Presenter.Kind)
                {
                    case AppWindowPresenterKind.CompactOverlay:
                        // Compact overlay - hide custom title bar
                        // and use the default system title bar instead.
                        AppTitleBar.Visibility = Visibility.Collapsed;
                        sender.TitleBar.ResetToDefault();
                        break;

                    case AppWindowPresenterKind.FullScreen:
                        // Full screen - hide the custom title bar
                        // and the default system title bar.
                        AppTitleBar.Visibility = Visibility.Collapsed;
                        sender.TitleBar.ExtendsContentIntoTitleBar = true;
                        break;

                    case AppWindowPresenterKind.Overlapped:
                        // Normal - hide the system title bar
                        // and use the custom title bar instead.
                        AppTitleBar.Visibility = Visibility.Visible;
                        sender.TitleBar.ExtendsContentIntoTitleBar = true;
                        SetDragRegionForCustomTitleBar(sender);
                        break;

                    default:
                        // Use the default system title bar.
                        sender.TitleBar.ResetToDefault();
                        break;
                }
            }
        }

        private void AppTitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                SetDragRegionForCustomTitleBar(m_AppWindow);
            }
        }

        private void AppTitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported()
                && m_AppWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                // Update drag region if the size of the title bar changes.
                SetDragRegionForCustomTitleBar(m_AppWindow);
            }
        }

        private bool SetTitleBarColors()
        {
            // Check to see if customization is supported.
            // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
            // Windows App SDK on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                if (m_AppWindow is null)
                {
                    m_AppWindow = GetAppWindowForCurrentWindow();
                }
                var titleBar = m_AppWindow.TitleBar;

                // Set active window colors
                // Note: No effect when app is running on Windows 10 since color customization is not
                // supported.

                // Set inactive window colors
                // Note: No effect when app is running on Windows 10 since color customization is not
                // supported.
                titleBar.ButtonBackgroundColor = ((SolidColorBrush)App.Current.Resources["WindowCaptionButtonBackground"]).Color;
                titleBar.ButtonInactiveBackgroundColor = ((SolidColorBrush)App.Current.Resources["WindowCaptionButtonBackground"]).Color;
                titleBar.ButtonHoverBackgroundColor = ((SolidColorBrush)App.Current.Resources["WindowCaptionButtonBackgroundPointerOver"]).Color;
                titleBar.ButtonPressedBackgroundColor = ((SolidColorBrush)App.Current.Resources["WindowCaptionButtonBackgroundPressed"]).Color;
                titleBar.ButtonForegroundColor = ((SolidColorBrush)App.Current.Resources["WindowCaptionButtonStroke"]).Color;
                titleBar.ButtonInactiveForegroundColor = ((SolidColorBrush)App.Current.Resources["WindowCaptionButtonStroke"]).Color;
                titleBar.ButtonHoverForegroundColor = ((SolidColorBrush)App.Current.Resources["WindowCaptionButtonStrokePointerOver"]).Color;
                titleBar.ButtonPressedForegroundColor = ((SolidColorBrush)App.Current.Resources["WindowCaptionButtonStrokePressed"]).Color;
                return true;
            }
            return false;
        }

        class WindowsSystemDispatcherQueueHelper
        {
            [StructLayout(LayoutKind.Sequential)]
            struct DispatcherQueueOptions
            {
                internal int dwSize;
                internal int threadType;
                internal int apartmentType;
            }

            [DllImport("CoreMessaging.dll")]
            private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

            object m_dispatcherQueueController = null;
            public void EnsureWindowsSystemDispatcherQueueController()
            {
                if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
                {
                    // one already exists, so we'll just use it.
                    return;
                }

                if (m_dispatcherQueueController == null)
                {
                    DispatcherQueueOptions options;
                    options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
                    options.threadType = 2;    // DQTYPE_THREAD_CURRENT
                    options.apartmentType = 2; // DQTAT_COM_STA
                    CreateDispatcherQueueController(options, ref m_dispatcherQueueController);
                }
            }
        }

        public bool TrySetSystemBackdrop()
        {
            if (MicaController.IsSupported())
            {
                m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
                m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

                // Create the policy object.
                m_configurationSource = new SystemBackdropConfiguration();
                this.Activated += Window_Activated;
                this.Closed += Window_Closed;
                ((FrameworkElement)this.Content).ActualThemeChanged += Window_ThemeChanged;

                // Initial configuration state.
                m_configurationSource.IsInputActive = true;
                SetConfigurationSourceTheme();

                m_backdropController = new MicaController();

                // Enable the system backdrop.
                // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_backdropController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
                m_backdropController.SetSystemBackdropConfiguration(m_configurationSource);
                return true; // succeeded
            }

            return false; // Mica is not supported on this system
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            // Make sure any Mica/Acrylic controller is disposed
            // so it doesn't try to use this closed window.
            if (m_backdropController != null)
            {
                m_backdropController.Dispose();
                m_backdropController = null;
            }
            this.Activated -= Window_Activated;
            m_configurationSource = null;
        }

        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (m_configurationSource != null)
            {
                SetConfigurationSourceTheme();
            }
        }

        private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)this.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = SystemBackdropTheme.Light; break;
                case ElementTheme.Default: m_configurationSource.Theme = SystemBackdropTheme.Default; break;
            }
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (args.WindowActivationState == WindowActivationState.Deactivated)
            {
                AppTitleTextBlock.Foreground =
                    (SolidColorBrush)App.Current.Resources["WindowCaptionForegroundDisabled"];
            }
            else
            {
                AppTitleTextBlock.Foreground =
                    (SolidColorBrush)App.Current.Resources["WindowCaptionForeground"];
            }
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null || item == _lastItem)
                return;
            var ClickedView = item.Tag?.ToString();
            //NavView.Header = $"{ClickedView}";
            if (!NavigateToView(ClickedView)) return;
            _lastItem = item;
        }

        private bool NavigateToView(string clickedView)
        {
            var view = Assembly.GetExecutingAssembly().GetType($"Windows_Cleanup_WinUI_3.Views.{clickedView}");
            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
                return false;
            ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "HomeView")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }
            ContentFrame.Navigate(typeof(HomeView));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsView));
                NavView.Header = "Settings";
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag)
                {
                    case "HomeView":
                        ContentFrame.Navigate(typeof(HomeView));
                        NavView.Header = "Home";
                        break;
                    case "BasicToolsView":
                        ContentFrame.Navigate(typeof(BasicToolsView));
                        NavView.Header = "Basic Tools";
                        break;
                    case "NetToolsView":
                        ContentFrame.Navigate(typeof(NetToolsView));
                        NavView.Header = "Net Tools";
                        break;
                    case "PowerOptionsView":
                        ContentFrame.Navigate(typeof(PowerOptionsView));
                        NavView.Header = "Power Options";
                        break;
                    case "AdvancedToolsView":
                        ContentFrame.Navigate(typeof(AdvancedToolsView));
                        NavView.Header = "Advanced Tools";
                        break;

                    default:
                        break;
                }
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            GC.Collect();
        }
    }
}
