using MvvmCross.Platforms.Wpf.Views;
using MvxStarter.Wpf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Codemasters.F1_2021.LapPacket;

namespace ApexSpeed.Wpf.Views
{
    /// <summary>
    /// Interaction logic for HistoricalView.xaml
    /// </summary>
    public partial class HistoricalView : MvxWpfView
    {

        string filePathA;
        string filePathB;

        public HistoricalView()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void UploadLapAButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            // Set the default file extension and filter
            openFileDialog.DefaultExt = "json";
            openFileDialog.Filter = "Json files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                // Get the filepath from the dialog
                filePathA = openFileDialog.FileName;

            }

            LapATB.Text = filePathA;
        }

        private void UploadLapBButton_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            // Set the default file extension and filter
            openFileDialog.DefaultExt = "json";
            openFileDialog.Filter = "Json files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                // Get the filepath from the dialog
                filePathB = openFileDialog.FileName;

            }

            LapBTB.Text = filePathB;

        }
    }
}
