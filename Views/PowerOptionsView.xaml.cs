// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.IO;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Windows_Cleanup_WinUI_3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PowerOptionsView : Page
    {
        public PowerOptionsView()
        {
            this.InitializeComponent();
        }

        private void cmdExec(string? path)
        {
            try
            {
                // Get the installed location of the application package
                string appFolderPath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;

                // Specify the relative path to your .bat file inside the AppX folder
                string relativeBatPath = path;

                // Combine the app folder path and the relative bat path
                string batFilePath = Path.Combine(appFolderPath, relativeBatPath);

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = batFilePath,
                    WorkingDirectory = appFolderPath,
                    UseShellExecute = true
                };

                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during execution
            }
        }



        private void btnShutdown_Click(object sender, RoutedEventArgs e)
        {
            cmdExec(@"Scripts\\PowerOptions\\cmd_shutdown.bat");
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            cmdExec(@"Scripts\\PowerOptions\\cmd_restart.bat");
        }

        private void btnAbortSR_Click(object sender, RoutedEventArgs e)
        {
            cmdExec(@"Scripts\\PowerOptions\\cmd_abortpowertask.bat");
        }

        private async void btnLogOff_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Quick Restart",
                Content = "The quick restart button will logoff your computer, and it can't be canceled. Do you want to continue?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };
            dialog.XamlRoot = btnLogOff.XamlRoot;
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                cmdExec(@"Scripts\\PowerOptions\\cmd_logoff.bat");
            }
            else
            {
                return;
            }

        }
    }
}
