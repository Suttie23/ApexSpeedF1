using Codemasters.F1_2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ApexSpeedApp.MVVM;
using ApexSpeedApp.MVVM.ViewModel;

namespace ApexSpeedApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for LiveAnalysisView.xaml
    /// </summary>
    /// 
    public partial class LiveAnalysisView : UserControl
    {

        private bool? isStreaming = false;

        public LiveAnalysisView()
        {
            InitializeComponent();
        }

        private void UDPListenerButton_Click(object sender, RoutedEventArgs e)
        {

            UdpClient receivingUdpClient = new(20777);

            // UDP Listener
            try
            {

                // Begin asynchronous listening
                receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);
                ListeningLabel.Content = "Listening for UDP Data...";

            }
            catch (Exception ex)
            {
                // Display error
                //DebugBox.Text += ex.Message.ToString();
                MessageBox.Show("Error Message: " + ex.Message);

            }

            void TelemetryReceiver(IAsyncResult res)
            {
                // Remote host IP
                IPEndPoint RemoteIpEndPoint = new(IPAddress.Any, 0);

                    // Return UDP datagram
                    byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    try
                    {

                        PacketType pt = CodemastersToolkit.GetPacketType(receiveBytes);

                        Packet pa = new Packet();
                        pa.LoadBytes(receiveBytes);

                        // Ensure the game is running the 2021 UDP format
                        if (pa.PacketFormat == 2021)
                        {

                            Dispatcher.BeginInvoke(new Action(delegate
                            {

                                ListenerTestLabel.Content = "UDP Format: " + pa.PacketFormat.ToString() + " Detected";

                            }));
                        }

                        // IF Car Telemetry Packet
                        if (pt == PacketType.CarTelemetry)
                        {
                            TelemetryPacket telPack = new TelemetryPacket();
                            telPack.LoadBytes(receiveBytes);

                       
                        }

                        // IF Car CarStatus Packet
                        if (pt == PacketType.CarStatus)
                        {
                            CarStatusPacket statusPack = new CarStatusPacket();
                            statusPack.LoadBytes(receiveBytes);

                        }

                    }
                    catch (Exception e)
                    {

                        MessageBox.Show("Error Message: " + e.Message + "\n\nIt is likely that your  UDP Format is not set to to 2021, change this by going to (Settings > Telemetry Settings > UDP Format > 2021) in your F1 game!", "UDP Format Error!");

                        // Exit the application if there is a version mismatch!
                        Dispatcher.BeginInvoke(new Action(delegate
                        {

                            System.Windows.Application.Current.Shutdown();

                        }));
                    }

                    // Begin Call Async Method
                    receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);
                
            }
        }

        private void UDPStopListenerButton_Click(object sender, RoutedEventArgs e)
        {
            //receivingUdpClient.Close();
            ListeningLabel.Content = "No Longer Listening...";
        }

    }
}
