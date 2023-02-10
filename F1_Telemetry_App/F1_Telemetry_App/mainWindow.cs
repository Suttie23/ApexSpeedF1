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
using LiveCharts.Defaults;

namespace F1_Telemetry_App
{

    public partial class mainWindow : Form
    {

        UdpClient receivingUdpClient = new(20777);

        public mainWindow()
        {
            InitializeComponent();

            ThrottleGauge.EasingFunction = null;
            ThrottleGauge.Total = 1;
            ThrottleGauge.InitialRotation = -225;
            ThrottleGauge.MaxAngle = 270;
            ThrottleGauge.Series = new GaugeBuilder()
                .WithLabelsSize(25)
                .WithInnerRadius(50)
                .WithBackgroundInnerRadius(50)
                .AddValue(0, "Throttle")
                .BuildSeries();

            SpeedGauge.EasingFunction = null;
            SpeedGauge.Total = 210;
            SpeedGauge.InitialRotation = -225;
            SpeedGauge.MaxAngle = 270;
            SpeedGauge.Series = new GaugeBuilder()
                .WithLabelsSize(25)
                .WithInnerRadius(50)
                .WithBackgroundInnerRadius(50)
                .AddValue(0, "Speed")
                .BuildSeries();

            BrakeGauge.EasingFunction = null;
            BrakeGauge.Total = 1;
            BrakeGauge.InitialRotation = -225;
            BrakeGauge.MaxAngle = 270;
            BrakeGauge.Series = new GaugeBuilder()
                .WithLabelsSize(25)
                .WithInnerRadius(50)
                .WithBackgroundInnerRadius(50)
                .AddValue(0, "Brake")
                .BuildSeries();


            ERSGauge.EasingFunction = null;
            ERSGauge.Total = 4000000;
            ERSGauge.Series = new GaugeBuilder()
                .WithLabelsSize(25)
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
                DebugBox.Text = "Listening for UDP Data...";

            }
            catch (Exception ex)
            {
                // Display error
                DebugBox.Text += ex.Message.ToString();

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

                var packet = PacketCarTelemetryData.FromByteArray(receiveBytes);

                this.Invoke(new MethodInvoker(delegate
                {

                    ThrottleGauge.EasingFunction = null;
                    ThrottleGauge.Total = 1;
                    ThrottleGauge.InitialRotation = -225;
                    ThrottleGauge.MaxAngle = 270;
                    ThrottleGauge.Series = new GaugeBuilder()
                        .WithLabelsSize(25)
                        .WithInnerRadius(50)
                        .WithBackgroundInnerRadius(50)
                        .AddValue(Math.Round(packet.carTelemetryData[packetHeader.playerCarIndex].throttle, 2))
                        .BuildSeries();

                    SpeedGauge.EasingFunction = null;
                    SpeedGauge.Series = new GaugeBuilder()
                        .WithLabelsSize(25)
                        .WithInnerRadius(50)
                        .WithOffsetRadius(10)
                        .WithBackgroundInnerRadius(50)
                        .AddValue(Math.Round(packet.carTelemetryData[packetHeader.playerCarIndex].speed * 0.621371192))
                        .BuildSeries();

                    BrakeGauge.EasingFunction = null;
                    BrakeGauge.Total = 1;
                    BrakeGauge.InitialRotation = -225;
                    BrakeGauge.MaxAngle = 270;
                    BrakeGauge.Series = new GaugeBuilder()
                        .WithLabelsSize(25)
                        .WithInnerRadius(50)
                        .WithBackgroundInnerRadius(50)
                        .AddValue(Math.Round(packet.carTelemetryData[packetHeader.playerCarIndex].brake, 2))
                        .BuildSeries();

                }));

            }
            else if (packetHeader.packetId == 7) //Car status
            {

                var packet = PacketCarStatusData.FromByteArray(receiveBytes);

                this.Invoke(new MethodInvoker(delegate
                {
                    TyreCompundLabel.Text = "Tyre Compound: " + packet.carStatusData[packetHeader.playerCarIndex].actualTyreCompound;
                    ERSModeLabel.Text = "ERS Deployment Mode: " + packet.carStatusData[packetHeader.playerCarIndex].ersDeployMode;
                    ERSStorageLabel.Text = "ERS Stored: " + packet.carStatusData[packetHeader.playerCarIndex].ersStoreEnergy + " Joules";

                    ERSGauge.EasingFunction = null;
                    ERSGauge.Series = new GaugeBuilder()
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