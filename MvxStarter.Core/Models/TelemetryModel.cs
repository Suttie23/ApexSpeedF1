using System;
using System.Collections.Generic;
using System.Text;

namespace MvxStarter.Core.Models
{
    internal class TelemetryModel
    {
        //Telemetry
        public int Throttle { get; set; }
        public int RPM { get; set; }
        public int Brake { get; set; }
        public ushort Speed { get; set; }
        public sbyte Gear { get; set; }
        public byte DRS { get; set; }

        //Version
        public string Version { get; set; }

        //Tyre Temp
        public int TyreTempFL { get; set; }
        public int TyreTempFR { get; set; }
        public int TyreTempRL { get; set; }
        public int TyreTempRR { get; set; }

        //Tyre Wear
        public int TyreWearFL { get; set; }
        public int TyreWearFR { get; set; }
        public int TyreWearRL { get; set; }
        public int TyreWearRR { get; set; }

        //Status
        public float FuelLevel { get; set; }
        public float FuelRemainingLaps { get; set; }
        public byte TyreCompound { get; set; }
        public string ERSDeploy { get; set; }

    }
}
