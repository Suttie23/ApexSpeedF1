using System;
using System.Drawing.Text;
using System.Globalization;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;

namespace F1_Telemetry_App
{
    public partial class mainWindow : Form
    {

        UdpClient receivingUdpClient = new(20777);

        public mainWindow()
        {
            InitializeComponent();

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

            // Invoke a delegate to ensure there is no cross-threading!
            this.Invoke(new MethodInvoker(delegate
            {
                // If correct version
                if (packetHeader.packetFormat == 2021)
                {
                    VerLabel.Text = "Running Supported Version: " + packetHeader.packetFormat;
                    SessionTime.Text = packetHeader.sessionTime.ToString();
                }
                // If unsupported version
                else
                {
                    MessageBox.Show("UDP Format " + packetHeader.packetFormat.ToString() + " is unsupported" + "\n\nPlease change UDP Format to 2021 (Settings > Telemetry Settings > UDP Format" ,"UDP Format Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    VerLabel.Text = "Running Unsupported Version: " + packetHeader.packetFormat;

                    // Exit the application if there is a version mismatch!
                    this.Close();
                    Environment.Exit(0);

                }

            }));

            // Begin Call Async Method
            receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);

        }

    }

}