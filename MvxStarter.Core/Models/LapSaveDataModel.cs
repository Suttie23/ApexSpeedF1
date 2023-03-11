﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApexSpeedApp.MVVM.Model
{
    public class LapSaveDataModel
    {

        // Telemetry Values
        public float Throttle { get; set; }
        public float Brake { get; set; }
        public sbyte Gear { get; set; }
        public ushort SpeedMph { get; set; }

        // Lap Values
        public uint LastLapTimeMilliseconds { get; set; }
        public float LapDistance { get; set; }

        public LapSaveDataModel(float Throttle, float Brake, sbyte Gear, ushort SpeedMph, float LapDistance)
        {
            this.Throttle = Throttle;
            this.Brake = Brake;
            this.Gear = Gear;
            this.SpeedMph = SpeedMph;
            this.LapDistance = LapDistance;

        }
    }

}