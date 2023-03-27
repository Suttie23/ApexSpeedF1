using System;
using System.Collections.Generic;
using System.Text;

namespace ApexSpeed.Core.Models.JSONWrappers
{
    public class DataPointModel
    {

        public double LapDistance { get; set; }

        public double Brake { get; set; }

        public double Gear { get; set; }

        public double SpeedMph { get; set; }

        public double Throttle { get; set; }

        public double CurrentLapTime { get; set; }
    }
}
