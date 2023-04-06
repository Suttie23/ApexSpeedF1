using ApexSpeedApp.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ApexSpeed.Core.Services.JSONWriter
{
    public class JSONWriter : IJSONWriter
    {

        public string _folderTrack;
        public string _folderDT;
        public byte _currentLapNumber;
        public TimeSpan _previousLapTime;
        List<LapSaveDataModel> _lapList = new List<LapSaveDataModel>();

        public async Task WriteLapData(string folderTrack, string folderDT, byte currentLapNumber, TimeSpan previousLapTime, List<LapSaveDataModel> LapList)
        {

            _folderTrack = folderTrack;
            _folderDT = folderDT;
            _currentLapNumber = currentLapNumber;
            _previousLapTime = previousLapTime;
            _lapList = LapList;

            // Construct fileName string using variables assigned earlier
            string fileName = @"..\..\..\..\Lap Files\" + _folderTrack + " " + _folderDT + "/Lap " + (_currentLapNumber - 1) + " (" + _previousLapTime.TotalMinutes + " Seconds)" + ".json";

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
        }

    }
}
