using ApexSpeed.Core.Services;
using ApexSpeed.Core.ViewModels;
using ApexSpeedApp.MVVM.Model;
using Codemasters.F1_2021;
using MvvmCross.Commands;
using MvvmCross.Logging.LogProviders;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvxStarter.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvxStarter.Core.ViewModels
{
    public class TelemetryViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;
        private readonly IUDPListenerService _udpListenerService;
        private CancellationTokenSource cts;

        public bool listenLoop = true;

        // Navigation Locking Prop
        private bool _lockNav = true;
        public bool LockNavigation
        {
            get { return _lockNav; }
            set
            {
                _lockNav = value;
                RaisePropertyChanged(() => LockNavigation);
            }
        }

        // Stiop Listening Button Prop
        private bool _stopListeningActive = false;
        public bool StopListeningActive
        {
            get { return _stopListeningActive; }
            set
            {
                _stopListeningActive = value;
                RaisePropertyChanged(() => StopListeningActive);
            }
        }

        // Telemtry Properties
        private float _throttle;
        public float Throttle
        {
            get { return _throttle; }
            set
            {
                // ref = reference to first name, this essentially checks whether the reference of _throttle is the same as the value. If it has changed, trigger InotifyPropertyChanged
                SetProperty(ref _throttle, value);
            }
        }

        private float _rpm;
        public float RPM
        {
            get { return _rpm; }
            set
            {
                SetProperty(ref _rpm, value);
            }
        }

        private float _brake;
        public float Brake
        {
            get { return _brake; }
            set
            {
                SetProperty(ref _brake, value);
            }
        }

        private ushort _speed;
        public ushort Speed
        {
            get { return _speed; }
            set
            {

                SetProperty(ref _speed, value);
            }
        }

        private sbyte _gear;
        public sbyte Gear
        {
            get { return _gear; }
            set
            {

                SetProperty(ref _gear, value);

            }
        }

        private bool _drsDeploy;
        public bool DRSDeploy
        {
            get { return _drsDeploy; }
            set
            {
                SetProperty(ref _drsDeploy, value);
            }
        }

        //Tyre temp variables
        private float _tyreTempFL;
        public float TyreTempFL
        {
            get { return _tyreTempFL; }
            set
            {
                SetProperty(ref _tyreTempFL, value);
            }
        }

        private float _tyreTempFR;
        public float TyreTempFR
        {
            get { return _tyreTempFR; }
            set
            {
                SetProperty(ref _tyreTempFR, value);
            }
        }

        private float _tyreTempRL;
        public float TyreTempRL
        {
            get { return _tyreTempRL; }
            set
            {
                SetProperty(ref _tyreTempRL, value);
            }
        }

        private float _tyreTempRR;
        public float TyreTempRR
        {
            get { return _tyreTempRR; }
            set
            {
                SetProperty(ref _tyreTempRR, value);
            }
        }

        //Tyre wear variables
        private float _tyreWearFL;
        public float TyreWearFL
        {
            get { return _tyreWearFL; }
            set
            {
                SetProperty(ref _tyreWearFL, value);
            }
        }

        private float _tyreWearFR;
        public float TyreWearFR
        {
            get { return _tyreWearFR; }
            set
            {
                SetProperty(ref _tyreWearFR, value);
            }
        }

        private float _tyreWearRL;
        public float TyreWearRL
        {
            get { return _tyreWearRL; }
            set
            {
                SetProperty(ref _tyreWearRL, value);
            }
        }

        private float _tyreWearRR;
        public float TyreWearRR
        {
            get { return _tyreWearRR; }
            set
            {
                SetProperty(ref _tyreWearRR, value);
            }
        }

        //Status variables
        private float _fuelLevel;
        public float FuelLevel
        {
            get { return _fuelLevel; }
            set
            {
                SetProperty(ref _fuelLevel, value);
            }
        }

        private float _fuelRemainingLaps;
        public float FuelLevelRemainingLaps
        {
            get { return _fuelRemainingLaps; }
            set
            {
                SetProperty(ref _fuelRemainingLaps, value);
            }
        }

        private string _tyreCompound;
        public string TyreCompound
        {
            get { return _tyreCompound; }
            set
            {
                SetProperty(ref _tyreCompound, value);
            }
        }


        private bool _ersDeploy;
        public bool ERSDeploy
        {
            get { return _ersDeploy; }
            set
            {
                SetProperty(ref _ersDeploy, value);
            }
        }


        //Lap Variables
        private byte _currentLapNumber;
        public byte CurrentLapNumber
        {
            get { return _currentLapNumber; }
            set
            {
                SetProperty(ref _currentLapNumber, value);
            }
        }

        private TimeSpan _currentLapTime;
        public TimeSpan CurrentLapTime
        {
            get { return _currentLapTime; }
            set
            {
                SetProperty(ref _currentLapTime, value);
            }
        }

        private TimeSpan _previousLapTime;
        public TimeSpan PreviousLapTime
        {
            get { return _previousLapTime; }
            set
            {
                SetProperty(ref _previousLapTime, value);
            }
        }

        private float _lapDistance;
        public float LapDistance
        {
            get { return _lapDistance; }
            set
            {
                SetProperty(ref _lapDistance, value);
            }
        }

        private TelemetryModel _telemetry;
        public TelemetryModel Telemetry
        {
            get { return _telemetry; }
            set
            {
                // ref = reference to first name, this essentially checks whether the reference of _throttle is the same as the value. If it has changed, trigger InotifyPropertyChanged
                SetProperty(ref _telemetry, value);
            }
        }

        //Ctor
        public TelemetryViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            _udpListenerService = new UDPListenerService(20777);

            GetTelemetryCommand = new MvxCommand(GetTelemetry);
            StopListeningCommand = new MvxCommand(StopListening);

        }

        // Home Navigation Command
        public IMvxCommand NavToHomeCommand => new MvxCommand(async () => await NavToHome());
        public async Task NavToHome()
        {
            _udpListenerService.ListenerDispose();
            await _navigationService.Navigate<HomeViewModel>();
        }

        // Historical Navigation Command
        public IMvxCommand NavToHistoricalCommand => new MvxCommand(async () => await NavToHistorical());
        public async Task NavToHistorical()
        {
            _udpListenerService.ListenerDispose();
            await _navigationService.Navigate<HistoricalViewModel>();
        }

        // Get Telemetry Command
        public IMvxCommand GetTelemetryCommand { get; set; }
        public async void GetTelemetry()
        {

            //Navigation Locking
            LockNavigation = false;
            StopListeningActive = true;
            cts = new CancellationTokenSource();

            listenLoop = true;

            while (listenLoop == true)
            {
                try
                {
                    // Telemetry Values
                    Telemetry = await _udpListenerService.ReceiveTelemetryAsync(cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // Handle the cancellation
                    Debug.WriteLine("Task was cancelled.");
                    _udpListenerService.ListenerDispose();
                }


                // General Telemetry Values
                Throttle = Telemetry.Throttle;
                RPM = Telemetry.RPM;
                Brake = Telemetry.Brake;
                Speed = Telemetry.Speed;
                Gear = Telemetry.Gear;
                DRSDeploy = Telemetry.DRS;

                // Tyre Temps
                TyreTempFL = Telemetry.TyreTempFL;
                TyreTempFR = Telemetry.TyreTempFR;
                TyreTempRL = Telemetry.TyreTempRL;
                TyreTempRR = Telemetry.TyreTempRR;

                // Tyre Wear
                TyreWearFL = Telemetry.TyreWearFL;
                TyreWearFR = Telemetry.TyreWearFR;
                TyreWearRL = Telemetry.TyreWearRL;
                TyreWearRR = Telemetry.TyreWearRR;

                // Car Status
                FuelLevel = Telemetry.FuelLevel;
                FuelLevelRemainingLaps = Telemetry.FuelLevelRemainingLaps;
                ERSDeploy = Telemetry.ERSDeploy;
                TyreCompound = Telemetry.TyreCompound;

                // Lap Data
                CurrentLapNumber = Telemetry.CurrentLapNumber;
                CurrentLapTime = Telemetry.CurrentLapTime;
                PreviousLapTime = Telemetry.PreviousLapTime;
                LapDistance = Telemetry.LapDistance;
            }
        }

        // Stop Listening Command
        public IMvxCommand StopListeningCommand { get; set; }
        public void StopListening()
        {
            cts?.Cancel();
            listenLoop = false;
            LockNavigation = true;
            StopListeningActive = false;
        }
    }
}