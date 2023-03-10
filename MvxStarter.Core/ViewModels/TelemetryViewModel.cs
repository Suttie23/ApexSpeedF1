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
        private byte _tyreCompound;
        private bool _ersDeploy;

        //Lap Variables
        private byte _currentLapNumber;
        private TimeSpan _currentLapTime;
        private TimeSpan _previousLapTime;
        private float _lapDistance;

        //JSON helpers
        List<LapSaveDataModel> LapList = new List<LapSaveDataModel>();
        private string _folderTrack;
        private string _folderDT;
        private bool _validLap = false;

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

        public byte TyreCompound
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


        public TelemetryViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            GetTelemetryCommand = new MvxCommand(GetTelemetry);
        }

        public IMvxCommand NavToHomeCommand => new MvxCommand(async () => await NavToHome());

        public async Task NavToHome()
        {
            await _navigationService.Navigate<HomeViewModel>();
        }

        public IMvxCommand GetTelemetryCommand { get; set; }

        UdpClient receivingUdpClient = new UdpClient(20777);

        // EVERYTHING BELOW SHOULD BE MADE INTO A SERVICE
        public void GetTelemetry()
        {

            // For Lap Data Folder Creation
            var formatInfo = new CultureInfo("en-US").DateTimeFormat;
            formatInfo.DateSeparator = "-";
            formatInfo.TimeSeparator = ".";
            _folderDT = DateTime.Now.ToString("g", formatInfo);

            try
            {
                // Begin asynchronous listening
                receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);
                Debug.WriteLine("Listening");

            } catch ( Exception e)
            {
                Debug.WriteLine(e);
            }
            
        }

        void TelemetryReceiver(IAsyncResult res)
        {
            // Remote host IP
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

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
                    this.Version = pa.PacketFormat.ToString();
                    Debug.WriteLine("Running version " +pa.PacketFormat);
                }

                // IF Car Telemetry Packet
                if (pt == PacketType.CarTelemetry)
                {
                    TelemetryPacket telPack = new TelemetryPacket();
                    telPack.LoadBytes(receiveBytes);

                    //General Telemetry
                    this.Throttle = (float)Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Throttle, 2);
                    this.RPM = telPack.FieldTelemetryData[telPack.PlayerCarIndex].EngineRpm;
                    this.Brake = (float)Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Brake, 2);
                    this.Speed = telPack.FieldTelemetryData[telPack.PlayerCarIndex].SpeedMph;
                    this.Gear = telPack.FieldTelemetryData[telPack.PlayerCarIndex].Gear;
                    this.DRSDeploy = telPack.FieldTelemetryData[telPack.PlayerCarIndex].DrsActive;

                    //Tyre Temps
                    this.TyreTempFL = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.FrontLeft;
                    this.TyreTempFR = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.FrontRight;
                    this.TyreTempRL = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.RearLeft;
                    this.TyreTempRR = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.RearRight;

                    if (telPack.FieldTelemetryData[telPack.PlayerCarIndex].DrsActive == true)
                    {
                        this.DRSDeploy = true;
                    } else
                    {
                        this.DRSDeploy = false;
                    }
                }

                // IF Car CarStatus Packet
                if (pt == PacketType.CarStatus)
                {
                    //Create new status packet and load in the data
                    CarStatusPacket statusPack = new CarStatusPacket();
                    statusPack.LoadBytes(receiveBytes);

                    this.FuelLevel = (float)Math.Round(statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].FuelLevel, 2);
                    this.FuelLevelRemainingLaps = statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].FuelRemainingLaps;

                    if(statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].SelectedErsDeployMode.ToString() == "Medium" || statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].SelectedErsDeployMode.ToString() == "None")
                    {
                        this.ERSDeploy = false;
                    } else
                    {
                        this.ERSDeploy = true;
                    }

                    //ADD TYRE COMPOUND
                }

                if (pt == PacketType.CarDamage)
                {
                    //Create new Damage packet and load in the data
                    CarDamagePacket damagePack = new CarDamagePacket();
                    damagePack.LoadBytes(receiveBytes);

                    //Tyre Wear
                    this.TyreWearFL = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.FrontLeft, 0);
                    this.TyreWearFR = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.FrontRight, 0);
                    this.TyreWearRL = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.RearLeft, 0);
                    this.TyreWearRR = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.RearRight, 0);

                }

                // IF Lap Packet
                if (pt == PacketType.Lap)
                {
                    //Create new Lap packet and load in the data
                    LapPacket lapPack = new LapPacket();
                    lapPack.LoadBytes(receiveBytes);

                    if (lapPack.FieldLapData[lapPack.PlayerCarIndex].LapDistance > 0)
                    {
                        _validLap = true;
                    }
                    else
                    {
                        _validLap = false;
                    }

                    this.CurrentLapNumber = lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapNumber;
                    this.CurrentLapTime = TimeSpan.FromMinutes(lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapTimeMilliseconds / 1000);
                    this.PreviousLapTime = TimeSpan.FromMinutes(lapPack.FieldLapData[lapPack.PlayerCarIndex].LastLapTimeMilliseconds / 1000);
                    this.LapDistance = lapPack.FieldLapData[lapPack.PlayerCarIndex].LapDistance;

                }

                // If Session Packet
                if (pt == PacketType.Session)
                {
                    SessionPacket lobPack = new SessionPacket();
                    lobPack.LoadBytes(receiveBytes);

                    _folderTrack = lobPack.SessionTrack.ToString();


                }

                //Writing to JSON
                // If starting a new lap
                if (_validLap == true)
                {
                    // Add telemetry to list
                    LapList.Add(new LapSaveDataModel(this.Throttle, this.Brake, this.Gear, this.Speed, this.LapDistance));


                    string fileName = @"..\..\..\..\Lap Files\" + _folderTrack + " " + _folderDT + "/Lap " + this.CurrentLapNumber + ".json";

                    // Create directory
                    FileInfo fi = new FileInfo(fileName);
                    if (!fi.Directory.Exists)
                    {
                        System.IO.Directory.CreateDirectory(fi.DirectoryName);
                    }

                    // Write LapList to JSON
                    string json = JsonConvert.SerializeObject(LapList, Newtonsoft.Json.Formatting.Indented);
                    StreamWriter sw = new StreamWriter(fileName);
                    sw.WriteLine(json);
                    sw.Close();

                    if (_lapDistance >= 0 && _lapDistance <= 3)
                    {

                        LapList.Clear();
                    }

                }

            }
            catch (Exception e)
            {

            }

            // Begin Call Async Method
            receivingUdpClient.BeginReceive(new AsyncCallback(TelemetryReceiver), null);

        }

    }

}



