using System;
using System.Globalization;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace F1_21_Telemetry
{
    class Program
    {
        static void Main(string[] args)
        {

            // Ensuring floats use dots as decimal separator
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            // Setting up UDP Listener
            UdpClient receivingUdpClient = new(20777);
            IPEndPoint RemoteIpEndPoint = new(IPAddress.Any, 0);
            Console.WriteLine("Listening to F1 Session...\n");


            while (true)
            {
                // If Receiving
                if (receivingUdpClient.Available > 0)
                {
                    byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    // Receive UDP bytes from the PacketHeader
                    var packetHeader = PacketHeader.FromByteArray(receiveBytes);

                    // Determine game version and exit with error if not 2021
                    if (packetHeader.packetFormat != 2021)
                    {
                        ExitWithError($"UDP format {packetHeader.packetFormat} not supported. Current version only supports 2021!");
                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            }

        }

        // Exit with error message
        static void ExitWithError(string error)
        {
            Console.WriteLine(error);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}