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

        private void BarcelonaButton_Click(object sender, RoutedEventArgs e)
        {
            BarcelonaImg.Visibility = Visibility.Visible;
            AustriaImg.Visibility = Visibility.Hidden;
        }

        private void AustriaButton_Click(object sender, RoutedEventArgs e)
        {
            BarcelonaImg.Visibility = Visibility.Hidden;
            AustriaImg.Visibility = Visibility.Visible;
        }

        private void ThrottleTipButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("1) When analysing your throttle, keep an eye on how aggressive you are being. You may be losing time due to loss of traction on corner exit!\n\n" +
                "2) When cornering, try to keep your throttle pressure consistant or change it gradually. Sudden changes will upset the balance of the car and could lead to a spin!\n\n" +
                "3) Keep an eye on where you are applying the throttle on corner exit, you may be pushing too hard too early or pushing too late!"        
                , "Throttle Tips"
            );
        }

        private void BrakeTipButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("1) Do your braking on approach to the corner and try to be finished before you start turning. Turning and braking will cause you to lock up and not only lose time in the corner, but will damage your tyres!\n\n" +
                "2) Give 'Trail Braking' a try if you are succeeding with mastering the above tip. Trail braking involves gently braking through the corner to keep the weight and load of the car on the front wheels, thus providing better rotation which will in turn allow you to corner more effectively!\n\n" +
                "3) It may sound obvious, but compare your braking with throttle input - you might be braking and accelerating at the same time!"
                , "Brake Tips"
            );
        }

        private void GearTipButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("1) Losing grip or spinning out on a specific corner? You might be changing gear mid turn. This will upset the balance of the car and cause the car to break traction with the rear tyres. Only change gear mid corner if you are confident that the turn isn't sharp!\n\n" +
                "2) If you are having trouble with accelerating too quickly out of a corner and spinning on corner exit, try selecting a gear (or two) higher than you think you need. This will stop you from putting the power down as quickly!"
                , "Brake Tips"
            );
        }

        private void SpeedTipButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("1) Be sure to analyse your speed with the other graphs in mind - you'll likely find a relationship between braking, throttle and gear trends on your speed on track!\n\n"
                , "Speed Tips"
            );
        }

        private void TimeTipButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("1) Be sure to analyse your time with the other graphs in mind - you'll discover a relationship between braking, throttle, gear and speed trends on your overall time!\n\n"
                , "Speed Tips"
            );
        }
    }
}
