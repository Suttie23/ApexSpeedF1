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
using Codemasters.F1_2021;
using Microsoft.VisualStudio.PlatformUI;

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

            try
            {

                PacketType pt = CodemastersToolkit.GetPacketType(receiveBytes);

                Packet pa = new Packet();
                pa.LoadBytes(receiveBytes);

                // Ensure the game is running the 2021 UDP format
                if (pa.PacketFormat != 2021)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show("UDP Format is unsupported" + "\n\nPlease change UDP Format to 2021 (Settings > Telemetry Settings > UDP Format", "UDP Format Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Exit the application if there is a version mismatch!
                        this.Close();
                        Environment.Exit(0);
                    }));
                }
                else
                {

                    this.Invoke(new MethodInvoker(delegate
                    {
                        VerLabel.Text = "Running Supported Version: " + pa.PacketFormat.ToString();
                        SessionTimeLabel.Text = pa.SessionTime.ToString();

                    }));
                }

                // IF Car Telemetry Packet
                if (pt == PacketType.CarTelemetry)
                {
                    TelemetryPacket telPack = new TelemetryPacket();
                    telPack.LoadBytes(receiveBytes);

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
                            .AddValue(Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Throttle, 2))
                            .BuildSeries();

                        SpeedGauge.EasingFunction = null;
                        SpeedGauge.Total = 1;
                        SpeedGauge.InitialRotation = -225;
                        SpeedGauge.MaxAngle = 270;
                        SpeedGauge.Series = new GaugeBuilder()
                            .WithLabelsSize(25)
                            .WithInnerRadius(50)
                            .WithBackgroundInnerRadius(50)
                            .AddValue(telPack.FieldTelemetryData[telPack.PlayerCarIndex].SpeedMph)
                            .BuildSeries();

                        BrakeGauge.EasingFunction = null;
                        BrakeGauge.Total = 1;
                        BrakeGauge.InitialRotation = -225;
                        BrakeGauge.MaxAngle = 270;
                        BrakeGauge.Series = new GaugeBuilder()
                            .WithLabelsSize(25)
                            .WithInnerRadius(50)
                            .WithBackgroundInnerRadius(50)
                            .AddValue(Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Brake, 2))
                            .BuildSeries();

                    }));

                }

                // IF Car CarStatus Packet
                if (pt == PacketType.CarStatus)
                {
                    CarStatusPacket statusPack = new CarStatusPacket();
                    statusPack.LoadBytes(receiveBytes);

                    this.Invoke(new MethodInvoker(delegate
                    {

                        ERSGauge.EasingFunction = null;
                        ERSGauge.Total = 1;
                        ERSGauge.InitialRotation = -225;
                        ERSGauge.MaxAngle = 270;
                        ERSGauge.Series = new GaugeBuilder()
                            .WithLabelsSize(25)
                            .WithInnerRadius(50)
                            .WithBackgroundInnerRadius(50)
                            .AddValue(statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].ErsStoredEnergyJoules)
                            .BuildSeries();

                        TyreCompundLabel.Text = statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].EquippedTyreCompound.ToString();
                        ERSModeLabel.Text = statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].SelectedErsDeployMode.ToString();

                    }));

                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                this.Invoke(new MethodInvoker(delegate
                {
                    MessageBox.Show("UDP Format is unsupported" + "\n\nPlease change UDP Format to 2021 (Settings > Telemetry Settings > UDP Format", "UDP Format Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Exit the application if there is a version mismatch!
                    this.Close();
                    Environment.Exit(0);
                }));

            }

            // Begin Call Async Method
            receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);

        }

    }

}