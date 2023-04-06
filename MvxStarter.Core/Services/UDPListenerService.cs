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

namespace ApexSpeed.Core.Services
{
    public class UDPListenerService : IUDPListenerService
    {

        private readonly int _port;
        private readonly UdpClient _udpClient;

        public float _throttle;

        public TelemetryModel telemetryModel = new TelemetryModel();

        public UDPListenerService(int port)
        {
            _port = port;
            _udpClient = new UdpClient(_port);

        }

        // General Telemetry
        public async Task<TelemetryModel> ReceiveTelemetryAsync()
        {

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

                }

                return telemetryModel;

            }


        }


    }


}
