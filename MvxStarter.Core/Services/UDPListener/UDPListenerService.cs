using ApexSpeedApp.MVVM.Model;
using Codemasters.F1_2021;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MvxStarter.Core.ViewModels;
using MvxStarter.Core.Models;
using ApexSpeed.Core.Services.JSONWriter;

namespace ApexSpeed.Core.Services.UDPListener
{
    public class UDPListenerService : IUDPListenerService
    {

        private readonly int _port;
        private readonly UdpClient _udpClient;
        private IJSONWriter _jsonWriterService;

        //JSON Helper Variables
        List<LapSaveDataModel> LapList = new List<LapSaveDataModel>();
        private byte _previousLapNumber = 0;
        private string _folderTrack;
        private float _sessionTime;
        private bool _validLap = false;
        private string _folderDT;

        public TelemetryModel telemetryModel = new TelemetryModel();

        public UDPListenerService(int port)
        {
            _port = port;
            _udpClient = new UdpClient(_port);
            _jsonWriterService = new JSONWriter.JSONWriter();

        }

        // General Telemetry
        public async Task<TelemetryModel> ReceiveTelemetryAsync(CancellationToken cancellationToken, string folderDT)
        {

            _folderDT = folderDT;
            cancellationToken.ThrowIfCancellationRequested();

            while (true)
            {               
                UdpReceiveResult result = await _udpClient.ReceiveAsync();
                byte[] receiveBytes = result.Buffer;

                // process data
                PacketType pt = CodemastersToolkit.GetPacketType(receiveBytes);

                // IF Car Telemetry Packet
                if (pt == PacketType.CarTelemetry)
                {
                    TelemetryPacket telPack = new TelemetryPacket();
                    telPack.LoadBytes(receiveBytes);

                    // General Telemetry
                    telemetryModel.Throttle = (float)Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Throttle, 2);
                    telemetryModel.RPM = telPack.FieldTelemetryData[telPack.PlayerCarIndex].EngineRpm;
                    telemetryModel.Brake = (float)Math.Round(telPack.FieldTelemetryData[telPack.PlayerCarIndex].Brake, 2);
                    telemetryModel.Speed = telPack.FieldTelemetryData[telPack.PlayerCarIndex].SpeedMph;
                    telemetryModel.Gear = telPack.FieldTelemetryData[telPack.PlayerCarIndex].Gear;
                    telemetryModel.DRS = telPack.FieldTelemetryData[telPack.PlayerCarIndex].DrsActive;

                    // Tyre Temps
                    telemetryModel.TyreTempFL = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.FrontLeft;
                    telemetryModel.TyreTempFR = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.FrontRight;
                    telemetryModel.TyreTempRL = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.RearLeft;
                    telemetryModel.TyreTempRR = telPack.FieldTelemetryData[telPack.PlayerCarIndex].TyreSurfaceTemperature.RearRight;

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

                // IF Car CarStatus Packet
                if (pt == PacketType.CarStatus)
                {
                    //Create new status packet and load in the data
                    CarStatusPacket statusPack = new CarStatusPacket();
                    statusPack.LoadBytes(receiveBytes);

                    telemetryModel.FuelLevel = (float)Math.Round(statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].FuelLevel, 2);
                    telemetryModel.FuelLevelRemainingLaps = statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].FuelRemainingLaps;

                    // Determine ERS Deploy mode
                    // This value will be used in the "Styles" in ApexSpeed.Wpf to change the colour or the ERS Label
                    if (statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].SelectedErsDeployMode.ToString() == "Medium" || statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].SelectedErsDeployMode.ToString() == "None")
                    {
                        telemetryModel.ERSDeploy = false;
                    }
                    else
                    {
                        telemetryModel.ERSDeploy = true;
                    }

                    //Determine Tyre Compound and set image source
                    switch (statusPack.FieldCarStatusData[statusPack.PlayerCarIndex].EquippedVisualTyreCompoundId)
                    {
                        case 18:
                            // Hard
                            telemetryModel.TyreCompound = "/Images/Hard Tyre.png";
                            break;
                        case 16:
                            // Soft
                            telemetryModel.TyreCompound = "/Images/Soft Tyre.png";
                            break;
                        case 17:
                            // Medium
                            telemetryModel.TyreCompound = "/Images/Medium Tyre.png";
                            break;
                        case 7:
                            // Intermediate
                            telemetryModel.TyreCompound = "/Images/Intermediate Tyre.png";
                            break;
                        case 8:
                            // Wet
                            telemetryModel.TyreCompound = "/Images/Wet Tyre.png";
                            break;
                    }
                }

                // If CarDamage Packet
                if (pt == PacketType.CarDamage)
                {
                    //Create new Damage packet and load in the data
                    CarDamagePacket damagePack = new CarDamagePacket();
                    damagePack.LoadBytes(receiveBytes);

                    //Tyre Wear
                    telemetryModel.TyreWearFL = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.FrontLeft, 0);
                    telemetryModel.TyreWearFR = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.FrontRight, 0);
                    telemetryModel.TyreWearRL = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.RearLeft, 0);
                    telemetryModel.TyreWearRR = (float)Math.Round(damagePack.FieldCarDamageData[damagePack.PlayerCarIndex].TyreWear.RearRight, 0);

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

                    telemetryModel.CurrentLapNumber = lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapNumber;
                    telemetryModel.CurrentLapTime = TimeSpan.FromMinutes(lapPack.FieldLapData[lapPack.PlayerCarIndex].CurrentLapTimeMilliseconds / 1000);
                    telemetryModel.PreviousLapTime = TimeSpan.FromMinutes(lapPack.FieldLapData[lapPack.PlayerCarIndex].LastLapTimeMilliseconds / 1000);
                    telemetryModel.LapDistance = lapPack.FieldLapData[lapPack.PlayerCarIndex].LapDistance;

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

                    //Writing to JSON
                    // If starting a new lap
                    if (_validLap == true)
                    {
                        // Add telemetry to list
                        LapList.Add(new LapSaveDataModel(telemetryModel.Throttle, telemetryModel.Brake, telemetryModel.Gear, telemetryModel.Speed, telemetryModel.LapDistance, telemetryModel.CurrentLapTime.TotalMinutes));
                    }

                    // Determine whether a new lap has been started
                    if (telemetryModel.CurrentLapNumber > _previousLapNumber)
                    {
                        // If the previous lap is 0
                        if ((telemetryModel.CurrentLapNumber - 1) == 0)
                        {
                            // Do nothing
                        }
                        else
                        {
                            // Construct fileName string using variables assigned earlier
                            string fileName = @"..\..\..\..\Lap Files\" + _folderTrack + " " + _folderDT + "/Lap " + (telemetryModel.CurrentLapNumber - 1) + " (" + telemetryModel.PreviousLapTime.TotalMinutes + " Seconds)" + ".json";

                            await _jsonWriterService.WriteLapData(LapList, fileName);
                            LapList.Clear();
                            _previousLapNumber = telemetryModel.CurrentLapNumber;
                        }

                    }

                    return telemetryModel;
                }


            }
        }

        public void ListenerDispose()
        {
            if (_udpClient !=null)
            {
                try 
                {
                    _udpClient.Dispose();
                }
                catch
                {

                }
            }
        }



    }


}
