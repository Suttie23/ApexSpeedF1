using ApexSpeedApp.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ObservableObject = CommunityToolkit.Mvvm.ComponentModel.ObservableObject;

namespace ApexSpeedApp.MVVM.Model
{
    public class GaugeModels : ObservableObject
    {

        private float _Throttlevalue;
        private float _Brakevalue;
        private float _RPMvalue;

        // For changing property values and updating gauge UI elements (Throttle)
        public float ThrottleValue
        {
            get { return _Throttlevalue; }
            set
            {
                _Throttlevalue = value;
                OnPropertyChanged();
            }
        }

        // For changing property values and updating gauge UI elements (Brake)
        public float BrakeValue
        {
            get { return _Brakevalue; }
            set
            {
                _Brakevalue = value;
               OnPropertyChanged();
            }
        }

        // For changing property values and updating gauge UI elements (RPM)
        public float RPMValue
        {
            get { return _RPMvalue; }
            set
            {
                _RPMvalue = value;
               OnPropertyChanged();
            }
        }


    }
}
