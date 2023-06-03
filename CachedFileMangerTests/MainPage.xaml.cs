using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace CachedFileMangerTests
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Test1_Click(object sender, RoutedEventArgs e)
        {
            TB.Text = "";
            EXTB.Text = "";
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation =PickerLocationId.DocumentsLibrary;
          
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
           
            savePicker.SuggestedFileName = "New Document";
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                try
                {
                    CachedFileManager.DeferUpdates(file);
                }
                catch (Exception ex)
                {
                    EXTB.Text = "Exception: " + ex.Message+ "\n";
                    EXTB.Text += ex.InnerException+ "\n";
                }

                await FileIO.WriteTextAsync(file, file.Name);
               
                FileUpdateStatus status =
                    await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == FileUpdateStatus.Complete)
                {
                    TB.Text = "File: " + file.Name + " was saved.";
                }
                else
                {
                    TB.Text = "File: " + file.Name + " couldn't be saved.";
                }
            }
            else
            {
                TB.Text = "Operation cancelled.";
            }
        }

        private async void Test2_Click(object sender, RoutedEventArgs e)
        {
            TB.Text = "";
            EXTB.Text = "";
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
           
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
           
            savePicker.SuggestedFileName = "New Document";
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                await FileIO.WriteTextAsync(file, file.Name);
                FileUpdateStatus status =
                    await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == FileUpdateStatus.Complete)
                {
                    TB.Text = "File: " + file.Name + " was saved.";
                }
                else
                {
                    TB.Text = "File: " + file.Name + " couldn't be saved.";
                }
            }
            else
            {
                TB.Text = "Operation cancelled.";
            }
        }
    }
}
