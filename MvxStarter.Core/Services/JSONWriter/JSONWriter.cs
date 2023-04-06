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

        public string _filename;
        List<LapSaveDataModel> _lapList = new List<LapSaveDataModel>();

        public async Task WriteLapData(List<LapSaveDataModel> LapList, string filename)
        {

            _lapList = LapList;
            _filename = filename;

            // Create directory
            FileInfo fi = new FileInfo(_filename);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }

            //LapList.RemoveAt(LapList.Count - 1);

            using(StreamWriter file = File.CreateText(_filename))
            {
                JsonSerializer serializer = new JsonSerializer
                {
                    Formatting = Formatting.Indented
                };

                await Task.Run(() => serializer.Serialize(file, LapList));
            }

        }

    }
}
