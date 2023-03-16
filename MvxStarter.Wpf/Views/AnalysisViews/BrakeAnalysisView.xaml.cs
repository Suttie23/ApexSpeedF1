using MvvmCross.Platforms.Wpf.Views;
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

namespace ApexSpeed.Wpf.Views.AnalysisViews
{
    /// <summary>
    /// Interaction logic for BrakeAnalysisView.xaml
    /// </summary>
    public partial class BrakeAnalysisView : MvxWpfView
    {
        string filePathA;
        string filePathB;

        public BrakeAnalysisView()
        {
            InitializeComponent();
        }

        private void UpLapAButton_Click(object sender, RoutedEventArgs e)
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

        private void UpLapBButton_Click(object sender, RoutedEventArgs e)
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
