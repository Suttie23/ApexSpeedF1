using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Drawing.Text;
using System.Globalization;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
using F1_Telemetry_App;
using LiveChartsCore.Measure;
using System.Security.Permissions;

namespace F1_Telemetry_App
{


    public partial class mainWindow : Form
    {

        UdpClient receivingUdpClient = new(20777);

        public mainWindow()
        {
            InitializeComponent();

            pieChart1.EasingFunction = null;

            pieChart1.Total = 4000000;

            pieChart1.Series = new GaugeBuilder()
                .WithLabelsSize(15)
                .WithInnerRadius(100)
                .WithBackgroundInnerRadius(70)
                .AddValue(4000000, "ERS")
                .BuildSeries();

        }

        private void ListenerButton_Click(object sender, EventArgs e)
        {
            // UDP Listener
            try
            {
                // Begin asynchronous listening
                receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);

            }
            catch (Exception ex)
            {
                // Display error
                StatusBox.Text += ex.Message.ToString();

            }

        }

        void TelemetryReceiver(IAsyncResult res)
        {
            // Remote host IP
            IPEndPoint RemoteIpEndPoint = new(IPAddress.Any, 0);

            // Return UDP datagram
            byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

            // Header UDP packet conversion
            var packetHeader = PacketHeader.FromByteArray(receiveBytes);

            // If unsupported version
            if (packetHeader.packetFormat != 2020)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    MessageBox.Show("UDP Format " + packetHeader.packetFormat.ToString() + " is unsupported" + "\n\nPlease change UDP Format to 2021 (Settings > Telemetry Settings > UDP Format", "UDP Format Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    VerLabel.Text = "Running Unsupported Version: " + packetHeader.packetFormat;

                    // Exit the application if there is a version mismatch!
                    this.Close();
                    Environment.Exit(0);
                }));

            }
            // If supported version
            else
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    VerLabel.Text = "Running Supported Version: " + packetHeader.packetFormat;
                    SessionTimeLabel.Text = packetHeader.sessionTime.ToString();

                }));
            }

            if (packetHeader.packetId == 2) // Lap Data
            {

            }
            else if (packetHeader.packetId == 6) // Car Telemetry
            {

            }
            else if (packetHeader.packetId == 7) //Car status
            {

                var packet = PacketCarStatusData.FromByteArray(receiveBytes);

                this.Invoke(new MethodInvoker(delegate
                {
                    TyreCompundLabel.Text = "Tyre Compound: " + packet.carStatusData[packetHeader.playerCarIndex].actualTyreCompound;
                    ERSModeLabel.Text = "ERS Deployment Mode: " + packet.carStatusData[packetHeader.playerCarIndex].ersDeployMode;
                    ERSStorageLabel.Text = "ERS Stored: " + packet.carStatusData[packetHeader.playerCarIndex].ersStoreEnergy + " Joules";

                    pieChart1.Series = new GaugeBuilder()
                        .WithLabelsSize(15)
                        .WithInnerRadius(50)
                        .WithOffsetRadius(10)
                        .WithBackgroundInnerRadius(50)
                        .AddValue(packet.carStatusData[packetHeader.playerCarIndex].ersStoreEnergy)
                        .BuildSeries();

                }));

            }
           
            // Begin Call Async Method
            receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);

        }

    }

}