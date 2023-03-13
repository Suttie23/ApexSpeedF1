using ApexSpeed.Core.ViewModels;
using ApexSpeedApp.MVVM.Model;
using Codemasters.F1_2021;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvxStarter.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MvxStarter.Core.ViewModels
{
    public class TelemetryViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;
        UdpClient receivingUdpClient = new UdpClient(20777);


        // Telemetry variables
        private float _throttle;
        private float _rpm;
        private float _brake;
        private ushort _speed;
        private sbyte _gear;
        private bool _drsDeploy;

        // Version
        private string _version;

        //Tyre temp variables
        private float _tyreTempFL;
        private float _tyreTempFR;
        private float _tyreTempRL;
        private float _tyreTempRR;

        //Tyre wear variables
        private float _tyreWearFL;
        private float _tyreWearFR;
        private float _tyreWearRL;
        private float _tyreWearRR;

        //Status variables
        private float _fuelLevel;
        private float _fuelRemainingLaps;
        private string _tyreCompound;
        private bool _ersDeploy;

        //Lap Variables
        private byte _currentLapNumber;
        private byte _previousLapNumber = 0;
        private TimeSpan _currentLapTime;
        private TimeSpan _previousLapTime;
        private float _lapDistance;

        //JSON helpers
        List<LapSaveDataModel> LapList = new List<LapSaveDataModel>();
        private string _folderTrack;
        private float _sessionTime;
        private string _folderDT;
        private bool _validLap = false;

        // Navigation Locking variables
        private bool _lockNav = true;
        private bool _stopListeningActive = false;

        // Navigation Locking Properties
        public bool LockNavigation
        {
            get { return _lockNav; }
            set
            {
                _lockNav = value;
                RaisePropertyChanged(() => LockNavigation);
            }
        }

        public bool StopListeningActive
        {
            get { return _stopListeningActive; }
            set
            {
                _stopListeningActive = value;
                RaisePropertyChanged(() => StopListeningActive);
            }
        }

        // Telemtry Properties

        public float Throttle
        {
            get { return _throttle; }
            set
            {
                // ref = reference to first name, this essentially checks whether the reference of _firstName is the same as the value. If it has changed, trigger InotifyPropertyChanged
                SetProperty(ref _throttle, value);
            }
        }

        public float RPM
        {
            get { return _rpm; }
            set
            {
                SetProperty(ref _rpm, value);
            }
        }

        public float Brake
        {
            get { return _brake; }
            set
            {

                SetProperty(ref _brake, value);
            }
        }

        public ushort Speed
        {
            get { return _speed; }
            set
            {

                SetProperty(ref _speed, value);
            }
        }

        public sbyte Gear
        {
            get { return _gear; }
            set
            {

                SetProperty(ref _gear, value);

            }
        }

        public bool DRSDeploy
        {
            get { return _drsDeploy; }
            set
            {
                SetProperty(ref _drsDeploy, value);
            }
        }

        //Version Property
        public string Version
        {
            get { return _version; }
            set
            {
                SetProperty(ref _version, value);                
            }
        }

        //Tyre Temp Properties
        public float TyreTempFL
        {
            get { return _tyreTempFL; }
            set
            {
                SetProperty(ref _tyreTempFL, value);
            }
        }

        public float TyreTempFR
        {
            get { return _tyreTempFR; }
            set
            {
                SetProperty(ref _tyreTempFR, value);
            }
        }

        public float TyreTempRL
        {
            get { return _tyreTempRL; }
            set
            {
                SetProperty(ref _tyreTempRL, value);
            }
        }

        public float TyreTempRR
        {
            get { return _tyreTempRR; }
            set
            {
                SetProperty(ref _tyreTempRR, value);
            }
        }

        //Tyre Wear Properties
        public float TyreWearFL
        {
            get { return _tyreWearFL; }
            set
            {
                SetProperty(ref _tyreWearFL, value);
            }
        }

        public float TyreWearFR
        {
            get { return _tyreWearFR; }
            set
            {
                SetProperty(ref _tyreWearFR, value);
            }
        }

        public float TyreWearRL
        {
            get { return _tyreWearRL; }
            set
            {
                SetProperty(ref _tyreWearRL, value);
            }
        }

        public float TyreWearRR
        {
            get { return _tyreWearRR; }
            set
            {
                SetProperty(ref _tyreWearRR, value);
            }
        }

        //Status Properties
        public float FuelLevel
        {
            get { return _fuelLevel; }
            set
            {
                SetProperty(ref _fuelLevel, value);
            }
        }

        public float FuelLevelRemainingLaps
        {
            get { return _fuelRemainingLaps; }
            set
            {
                SetProperty(ref _fuelRemainingLaps, value);
            }
        }

        public string TyreCompound
        {
            get { return _tyreCompound; }
            set
            {
                SetProperty(ref _tyreCompound, value);
            }
        }

        public bool ERSDeploy
        {
            get { return _ersDeploy; }
            set
            {
                SetProperty(ref _ersDeploy, value);
            }
        }

        //Lap Properties
        public byte CurrentLapNumber
        {
            get { return _currentLapNumber; }
            set
            {
                SetProperty(ref _currentLapNumber, value);
            }
        }

        public TimeSpan CurrentLapTime
        {
            get { return _currentLapTime; }
            set
            {
                SetProperty(ref _currentLapTime, value);
            }
        }

        public TimeSpan PreviousLapTime
        {
            get { return _previousLapTime; }
            set
            {
                SetProperty(ref _previousLapTime, value);
            }
        }

        public float LapDistance
        {
            get { return _lapDistance; }
            set
            {
                SetProperty(ref _lapDistance, value);
            }
        }

        //Ctor
        public TelemetryViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            GetTelemetryCommand = new MvxCommand(GetTelemetry);
            StopListeningCommand = new MvxCommand(StopListening);

        }

        // Home Navigation Command
        public IMvxCommand NavToHomeCommand => new MvxCommand(async () => await NavToHome());
        public async Task NavToHome()
        {
            receivingUdpClient.Client.Shutdown(SocketShutdown.Receive);
            receivingUdpClient.Close();
            await _navigationService.Navigate<HomeViewModel>();
        }

        // Historical Navigation Command
        public IMvxCommand NavToHistoricalCommand => new MvxCommand(async () => await NavToHistorical());
        public async Task NavToHistorical()
        {
            receivingUdpClient.Client.Shutdown(SocketShutdown.Receive);
            receivingUdpClient.Close();
            await _navigationService.Navigate<HistoricalViewModel>();
        }

        // Stop Listening Command
        public IMvxCommand StopListeningCommand { get; set; }
        public void StopListening()
        {
            receivingUdpClient.Client.Shutdown(SocketShutdown.Receive);
            receivingUdpClient.Close();
            LockNavigation = true;
        }

        // Get Telemetry Command
        public IMvxCommand GetTelemetryCommand { get; set; }

        // EVERYTHING BELOW SHOULD IDEALLY BE MADE INTO A SERVICE
        public void GetTelemetry()
        {
            //Navigation Locking
            LockNavigation = false;
            StopListeningActive = true;

            // For Lap Data Folder Creation
            var formatInfo = new CultureInfo("en-US").DateTimeFormat;
            formatInfo.DateSeparator = "-";
            formatInfo.TimeSeparator = ".";
            _folderDT = DateTime.Now.ToString("g", formatInfo);

            try
            {
                // Begin asynchronous listening
                receivingUdpClient.BeginReceive(TelemetryReceiver, null);
                Debug.WriteLine("Listening");

            } catch ( Exception e)
            {
                Debug.WriteLine(e);
            }
            
        }

        // Listener
        void TelemetryReceiver(IAsyncResult res)
        {
            // Remote host IP
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // Return UDP datagram
            byte[] receiveBytes = receivingUdpClient.EndReceive(res, ref RemoteIpEndPoint);

            try
            {

                PacketType pt = CodemastersToolkit.GetPacketType(receiveBytes);

                Packet pa = new Packet();
                pa.LoadBytes(receiveBytes);

                // Ensure the game is running the 2021 UDP format
                if (pa.PacketFormat == 2021)
                {
                    Version = pa.PacketFormat.ToString();
                }

                // IF Car Telemetry Packet
                if (pt == PacketType.CarTelemetry)
                {
                    TelemetryPacket telPack = new TelemetryPacket();
                    telPack.LoadBytes(receiveBytes);

                    //General Telemetry
                    Throttle = (float)Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Throttle, 2);
                    RPM = telPack.FieldTelemetryData[telPack.PlayerCarIndex].EngineRpm;
                    Brake = (float)Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Brake, 2);
                    Speed = telPack.FieldTelemetryData[telPack.PlayerCarIndex].SpeedMph;
                    Gear = telPack.FieldTelemetryData[telPack.PlayerCarIndex].Gear;
                    DRSDeploy = telPack.FieldTelemetryData[telPack.PlayerCarIndex].DrsActive;

                    //Tyre Temps
                    TyreTempFL = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.FrontLeft;
                    TyreTempFR = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.FrontRight;
                    TyreTempRL = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.RearLeft;
                    TyreTempRR = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.RearRight;

                    // Determine DRS Deploy mode
                    // This value will be used in the "Styles" in ApexSpeed.Wpf to change the colour or the DRS Label
                    if (telPack.FieldTelemetryData[telPack.PlayerCarIndex].DrsActive == true)
                    {
                        DRSDeploy = true;
                    } else
                    {
                        DRSDeploy = false;
                    }
                }

                // IF Car CarStatus Packet
                if (pt == PacketType.CarStatus)
                {
                    //Create new status packet and load in the data
                    CarStatusPacket statusPack = new CarStatusPacket();
                    statusPack.LoadBytes(receiveBytes);

                    FuelLevel = (float)Math.Round(statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].FuelLevel, 2);
                    FuelLevelRemainingLaps = statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].FuelRemainingLaps;

                    // Determine ERS Deploy mode
                    // This value will be used in the "Styles" in ApexSpeed.Wpf to change the colour or the ERS Label
                    if(statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].SelectedErsDeployMode.ToString() == "Medium" || statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].SelectedErsDeployMode.ToString() == "None")
                    {
                        ERSDeploy = false;
                    } else
                    {
                        ERSDeploy = true;
                    }

                    //Determine Tyre Compound and set image source
                    switch (statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].EquippedVisualTyreCompoundId)
                    {
                        case 18:
                            // Hard
                            TyreCompound = "/Images/Hard Tyre.png";
                            break;
                        case 16:
                            // Soft
                            TyreCompound = "/Images/Soft Tyre.png";
                            break;
                        case 17:
                            // Medium
                            TyreCompound = "/Images/Medium Tyre.png";
                            break;
                        case 7:
                            // Intermediate
                            TyreCompound = "/Images/Intermediate Tyre.png";
                            break;
                        case 8:
                            // Wet
                            TyreCompound = "/Images/Wet Tyre.png";
                            break;
                    }
                }

                if (pt == PacketType.CarDamage)
                {
                    //Create new Damage packet and load in the data
                    CarDamagePacket damagePack = new CarDamagePacket();
                    damagePack.LoadBytes(receiveBytes);

                    //Tyre Wear
                    TyreWearFL = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.FrontLeft, 0);
                    TyreWearFR = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.FrontRight, 0);
                    TyreWearRL = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.RearLeft, 0);
                    TyreWearRR = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.RearRight, 0);

                }

                // If Session Packet
                if (pt == PacketType.Session)
                {
                    SessionPacket lobPack = new SessionPacket();
                    lobPack.LoadBytes(receiveBytes);

                    // Setting _folderTrack variable for writing to JSON
                    _folderTrack = lobPack.SessionTrack.ToString();
                    _sessionTime = lobPack.SessionTime;

                }

                // IF Lap Packet
                if (pt == PacketType.Lap)
                {

                    //Create new Lap packet and load in the data
                    LapPacket lapPack = new LapPacket();
                    lapPack.LoadBytes(receiveBytes);

                    CurrentLapNumber = lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapNumber;
                    CurrentLapTime = TimeSpan.FromMinutes(lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapTimeMilliseconds / 1000);
                    PreviousLapTime = TimeSpan.FromMinutes(lapPack.FieldLapData[lapPack.PlayerCarIndex].LastLapTimeMilliseconds / 1000);
                    LapDistance = lapPack.FieldLapData[lapPack.PlayerCarIndex].LapDistance;

                    // Determine whether the car is on an out lap or not 
                    if (lapPack.FieldLapData[lapPack.PlayerCarIndex].LapDistance > 0 && _sessionTime > 3)
                    {
                        // If the value is positive, the car is not on an outlap
                        _validLap = true;
                    }
                    else
                    {
                        // If the value is negative, the car is on an outlap
                        _validLap = false;
                    }

                }

                //Writing to JSON
                // If starting a new lap
                if (_validLap == true)
                {
                    // Add telemetry to list
                    LapList.Add(new LapSaveDataModel(Throttle, Brake, Gear, Speed, LapDistance));
                }

                    // Determine whether a new lap has been started
                    if (CurrentLapNumber > _previousLapNumber)
                    {
                        // If the previous lap is 0
                        if ((CurrentLapNumber - 1) == 0)
                        {
                            // Do nothing
                        } 
                        else
                        {
                            // Write LapList to JSON

                            // Construct fileName string using variables assigned earlier
                            string fileName = @"..\..\..\..\Lap Files\" + _folderTrack + " " + _folderDT + "/Lap " + (CurrentLapNumber - 1) + ".json";

                            // Create directory
                            FileInfo fi = new FileInfo(fileName);
                            if (!fi.Directory.Exists)
                            {
                                System.IO.Directory.CreateDirectory(fi.DirectoryName);
                            }

                            LapList.RemoveAt(LapList.Count - 1);

                            // JSON Serialise
                            string json = JsonConvert.SerializeObject(LapList, Newtonsoft.Json.Formatting.Indented);
                            StreamWriter sw = new StreamWriter(fileName);
                            sw.WriteLine(json);
                            sw.Close();

                            // Clear LapList for next lap
                            LapList.Clear();

                            _previousLapNumber = CurrentLapNumber;
                        }

                    }
                
            }
            // Exception Handling
            catch (Exception e)
            {
                if (e is ObjectDisposedException || e is SocketException)
                {
                    return;
                }
            }

            // Begin Call Async Method
            receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);

        }

    }

}



