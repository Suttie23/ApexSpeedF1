using Codemasters.F1_2021;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ApexSpeedApp.MVVM.Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.Pkcs;

namespace ApexSpeedApp.MVVM.View
{
    /// <summary>
    /// Interaction logic for LiveAnalysisView.xaml
    /// </summary>
    /// 
    public partial class LiveAnalysisView : UserControl
    {


        //JSON TESTING
        List<LapSaveData> LapList = new List<LapSaveData>();

        //Locals
        public float _throttle;
        public float _brake;
        public float _gear;
        public float _speed;
        public float _lapDistance;
        public bool _newLap = false;
        public int _lapNo;

        public LiveAnalysisView()
        {
            InitializeComponent();
            
        }

        // UDP Listener button
        private void UDPListenerButton_Click(object sender, RoutedEventArgs e)
        {

            // Port 20777 (Default for F1 2021)
            UdpClient receivingUdpClient = new(20777);

            // UDP Listener
            try
            {

                // Begin asynchronous listening
                receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);
                ListeningLabel.Content = "Listening...";

            }
            catch (Exception ex)
            {
                // Display error
                //DebugBox.Text += ex.Message.ToString();
                MessageBox.Show("Error Message: " + ex.Message);

            }

            // Async Result for listener
            void TelemetryReceiver(IAsyncResult res)
            {
                // Remote host IP
                IPEndPoint RemoteIpEndPoint = new(IPAddress.Any, 0);

                    // Return UDP datagram
                    byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    try
                    {
                    
                        // Get UDP Packet Type
                        PacketType pt = CodemastersToolkit.GetPacketType(receiveBytes);

                        // Create new packet and load in the data
                        Packet pa = new Packet();
                        pa.LoadBytes(receiveBytes);

                        // Ensure the game is running the 2021 UDP format
                        if (pa.PacketFormat == 2021)
                        {
                            // Delegate to avoid cross threading
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                // Update UI to show UDP format
                                ListenerTestLabel.Content = "UDP Format: " + pa.PacketFormat.ToString() + " Detected";

                            }));
                        }

                        // IF Car Telemetry Packet
                        if (pt == PacketType.CarTelemetry)
                        {
                            //Create new telemetry packet and load in the data
                            TelemetryPacket telPack = new TelemetryPacket();
                            telPack.LoadBytes(receiveBytes);

                        _throttle = (float)Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Throttle, 2);
                        _brake = (float)Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Brake, 2);
                        _gear = telPack.FieldTelemetryData[telPack.PlayerCarIndex].Gear; ;
                        _speed = telPack.FieldTelemetryData[telPack.PlayerCarIndex].SpeedMph;

                        // Delegate to avoid cross threading
                        Dispatcher.BeginInvoke(new Action(delegate
                            {
                                // Update UI elements
                                SpeedDisplay.Content = "MPH: " + telPack.FieldTelemetryData[telPack.PlayerCarIndex].SpeedMph;
                                ThrottleDisplay.Content = Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Throttle, 2); 
                                BrakeDisplay.Content = Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Brake, 2);
                                GearDisplay.Content = telPack.FieldTelemetryData[telPack.PlayerCarIndex].Gear;
                                RPMDisplay.Content = telPack.FieldTelemetryData[telPack.PlayerCarIndex].EngineRpm;

                                // Gauge Databinding
                                ThrotBindBox.Text = telPack.FieldTelemetryData[telPack.PlayerCarIndex].Throttle.ToString();
                                BrakeBindBox.Text = telPack.FieldTelemetryData[telPack.PlayerCarIndex].Brake.ToString();
                                RPMBindBox.Text = telPack.FieldTelemetryData[telPack.PlayerCarIndex].EngineRpm.ToString();

                                // Tyre Temperatures
                                TyreTempFrontLeftDisplay.Content = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.FrontLeft;
                                TyreTempFrontRightDisplay.Content = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.FrontRight;
                                TyreTempRearLeftDisplay.Content = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.RearLeft;
                                TyreTempRearRightDisplay.Content = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.RearRight;

                            }));

                        // IF DRS is active
                        if (telPack.FieldTelemetryData[telPack.PlayerCarIndex].DrsActive == true)
                        {
                            // Delegate to avoid cross threading
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                // Change UI accordingly
                                DRSToggleDisplay.Foreground = System.Windows.Media.Brushes.YellowGreen;
                            }));
               
                        }
                        else
                        {
                            // Delegate to avoid cross threading
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                // Change UI accordingly
                                DRSToggleDisplay.Foreground = System.Windows.Media.Brushes.White;
                            }));
                            
                        }

                    }   
                   
                    // IF Car CarStatus Packet
                    if (pt == PacketType.CarStatus)
                    {
                        //Create new status packet and load in the data
                        CarStatusPacket statusPack = new CarStatusPacket();
                        statusPack.LoadBytes(receiveBytes);

                        // Delegate to avoid cross threading
                        Dispatcher.BeginInvoke(new Action(delegate
                        {
                            // Update UI
                            FuelDisplay.Content = "FUEL: " + Math.Round(statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].FuelLevel, 2);
                            FuelDLapsRemainingDisplay.Content = "Laps Of Fuel: " + statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].FuelRemainingLaps;                            

                        }));

                        //Determine Tyre Compound and update UI
                        switch (statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].EquippedVisualTyreCompoundId)
                            {
                                case 18:
                                    // Hard
                                    Dispatcher.BeginInvoke(new Action(delegate
                                    {

                                        TyreCompoundImage.Source = new BitmapImage(new Uri("/Images/Hard Tyre.png", UriKind.Relative));

                                    }));
                                    break;
                                case 16:
                                    // Soft
                                    Dispatcher.BeginInvoke(new Action(delegate
                                    {

                                        TyreCompoundImage.Source = new BitmapImage(new Uri("/Images/Soft Tyre.png", UriKind.Relative));

                                    }));
                                    break;
                                case 17:
                                    // Medium
                                    Dispatcher.BeginInvoke(new Action(delegate
                                    {

                                        TyreCompoundImage.Source = new BitmapImage(new Uri("/Images/Medium Tyre.png", UriKind.Relative));

                                    }));
                                    break;
                                case 7:
                                    // Intermediate
                                    Dispatcher.BeginInvoke(new Action(delegate
                                    {

                                        TyreCompoundImage.Source = new BitmapImage(new Uri("/Images/Intermediate Tyre.png", UriKind.Relative));

                                    }));
                                    break;
                                case 8:
                                    // Wet
                                    Dispatcher.BeginInvoke(new Action(delegate
                                    {

                                        TyreCompoundImage.Source = new BitmapImage(new Uri("/Images/Wet Tyre.png", UriKind.Relative));

                                    }));
                                    break;
                            }

                        // Determine ERS Status
                        string Overtake = statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].SelectedErsDeployMode.ToString();
                        if (Overtake == "Medium" || Overtake == "None")
                        {
                            // Delegate to avoid cross threading
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                // Update UI Accordingly
                                ERSToggleDisplay.Foreground = System.Windows.Media.Brushes.White;


                            }));
                        }else
                        {
                            // Delegate to avoid cross threading
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                // Update UI Accordingly
                                ERSToggleDisplay.Foreground = System.Windows.Media.Brushes.YellowGreen;
                            }));
                        }
                        


                    }

                        // IF Car CarDamage Packet
                        if (pt == PacketType.CarDamage)
                        {
                            //Create new damage packet and load in the data
                            CarDamagePacket damPack = new CarDamagePacket();
                            damPack.LoadBytes(receiveBytes);

                            // Delegate to avoid cross threading
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                // Update UI

                                // Tyre Wear
                                TyreWearFrontLeftDisplay.Content = Math.Round(damPack.FieldCarDamageData[damPack.PlayerCarIndex].TyreWear.FrontLeft, 0);
                                TyreWearFrontRightDisplay.Content = Math.Round(damPack.FieldCarDamageData[damPack.PlayerCarIndex].TyreWear.FrontRight, 0);
                                TyreWearRearLeftDisplay.Content = Math.Round(damPack.FieldCarDamageData[damPack.PlayerCarIndex].TyreWear.RearLeft, 0);
                                TyreWearRearRightDisplay.Content = Math.Round(damPack.FieldCarDamageData[damPack.PlayerCarIndex].TyreWear.RearRight, 0);
                            }));

                        }

                    // IF Lap Packet
                    if (pt == PacketType.Lap)
                    {
                        //Create new damage packet and load in the data
                        LapPacket lapPack = new LapPacket();
                        lapPack.LoadBytes(receiveBytes);

                        if (lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapTimeMilliseconds >= 0.1)
                        {
                            _newLap = true;
                        }

                        _lapDistance = lapPack.FieldLapData[lapPack.PlayerCarIndex].LapDistance;
                        _lapNo = lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapNumber;

                        // Delegate to avoid cross threading
                        Dispatcher.BeginInvoke(new Action(delegate
                        {
                            CurrentLapDisplay.Content = TimeSpan.FromMinutes(lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapTimeMilliseconds / 1000);
                            PreviousLapDisplay.Content = TimeSpan.FromMinutes(lapPack.FieldLapData[lapPack.PlayerCarIndex].LastLapTimeMilliseconds / 1000);
                            Sector1Display.Content = "Sector 1: " + lapPack.FieldLapData[lapPack.PlayerCarIndex].Sector1TimeMilliseconds / 1000;
                            Sector2Display.Content = "Sector 2: " + lapPack.FieldLapData[lapPack.PlayerCarIndex].Sector2TimeMilliseconds / 1000;
                            LapNoDisplay.Content = "Lap: " + lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapNumber;
                        }));

                        

                    }

                }
                    // Catch error if UDP cannot be read
                    catch (Exception e)
                    {
                        // Display error message
                        MessageBox.Show("Error Message: " + e.Message + "\n\nIt is likely that your  UDP Format is not set to to 2021, change this by going to (Settings > Telemetry Settings > UDP Format > 2021) in your F1 game!", "UDP Format Error!");

                        // Exit the application if there is a version mismatch!
                        Dispatcher.BeginInvoke(new Action(delegate
                        {

                            System.Windows.Application.Current.Shutdown();

                        }));
                    }

                if(_newLap == true)
                {

                    LapList.Add(new LapSaveData(_throttle, _brake, (sbyte)_gear, (ushort)_speed, _lapDistance));

                    string fileName = @"..\..\..\Lap Files\Lap " +_lapNo+ ".json";
                    string json = JsonConvert.SerializeObject(LapList, Newtonsoft.Json.Formatting.Indented);
                    using StreamWriter sw = new StreamWriter(fileName);
                    sw.WriteLine(json);
                    sw.Close();
                    
                }

                _newLap = false;

                // Begin Call Async Method
                receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);
                
            }
            
        }

        // Stop listening for UDP Button
        private void UDPStopListenerButton_Click(object sender, RoutedEventArgs e)
        {
            //LapSave.LapToJSON();
        }

    }
}
