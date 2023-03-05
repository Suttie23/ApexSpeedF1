using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSpeedApp.MVVM.Model
{
    public class LapSaveData
    {

        // Telemetry Values
        public float Throttle { get; set; }
        public float Brake { get; set; }
        public sbyte Gear { get; set; }
        public ushort SpeedMph { get; set; }

        // Lap Values
        public uint LastLapTimeMilliseconds { get; set; }
        public float LapDistance { get; set; }
        public byte CurrentLapNumber { get; set; }

        public LapSaveData()
        {
            Throttle = 100; 
        }

        public void LapToJSON()
        {

            string fileName = @"..\..\..\Lap Files\TEST.json";
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using StreamWriter sw = new StreamWriter(fileName);
            sw.WriteLine(json);
            sw.Close();
        }

    }

}
