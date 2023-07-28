// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Reflection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Windows_Cleanup_WinUI_3.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Windows_Cleanup_WinUI_3;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    private NavigationViewItem _lastItem;

    public MainWindow()
    {
        //var exists = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
        //if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1) System.Diagnostics.Process.GetCurrentProcess().Kill();
        InitializeComponent();
        Activated += MainWindow_Activated;
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
    }

    private void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
    {
        AppTitleBar.Margin = new Thickness()
        {
            Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2 : 1),
            Top = AppTitleBar.Margin.Top,
            Right = AppTitleBar.Margin.Right,
            Bottom = AppTitleBar.Margin.Bottom
        };
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
