using System;
using System.Collections.Generic;
using System.Text;

namespace MvxStarter.Core.Models
{
    public class TelemetryModel
    {

        //Telemetry
        public float Throttle { get; set; }
        public float RPM { get; set; }
        public float Brake { get; set; }
        public ushort Speed { get; set; }
        public sbyte Gear { get; set; }
        public bool DRS { get; set; }

        //Version
        public string Version { get; set; }

        //Tyre Temp
        public float TyreTempFL { get; set; }
        public float TyreTempFR { get; set; }
        public float TyreTempRL { get; set; }
        public float TyreTempRR { get; set; }

        //Tyre Wear
        public float TyreWearFL { get; set; }
        public float TyreWearFR { get; set; }
        public float TyreWearRL { get; set; }
        public float TyreWearRR { get; set; }

        //Status
        public float FuelLevel { get; set; }
        public float FuelLevelRemainingLaps { get; set; }
        public string TyreCompound { get; set; }
        public bool ERSDeploy { get; set; }

        //Lap Data
        public byte CurrentLapNumber { get; set; }
        public TimeSpan CurrentLapTime { get; set; }
        public TimeSpan PreviousLapTime { get; set; }
        public float LapDistance { get; set; }
    }
}
