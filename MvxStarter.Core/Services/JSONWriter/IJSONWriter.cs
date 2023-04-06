using ApexSpeedApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApexSpeed.Core.Services.JSONWriter
{
    public interface IJSONWriter
    {
        Task WriteLapData(List<LapSaveDataModel> LapList, string filename);
    }
}
