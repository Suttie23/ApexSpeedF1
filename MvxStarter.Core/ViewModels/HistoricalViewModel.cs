using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvxStarter.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ApexSpeed.Core.Models.JSONWrappers;
using System.IO;
using System.Text.Json;

namespace ApexSpeed.Core.ViewModels
{
    public class HistoricalViewModel : MvxViewModel
    {

        private string _selectedFileA;
        public string SelectedFileA
        {
            get { return _selectedFileA; }
            set
            {
                SetProperty(ref _selectedFileA, value);
            }
        }

        private string _selectedFileB;
        public string SelectedFileB
        {
            get { return _selectedFileB; }
            set
            {
                SetProperty(ref _selectedFileB, value);
            }
        }

        private readonly IMvxNavigationService _navigationService;

        public IMvxCommand NavToHomeCommand => new MvxCommand(async () => await NavToHome());
        public async Task NavToHome()
        {
            await _navigationService.Navigate<HomeViewModel>();
        }


        public IMvxCommand NavToLiveTelemetryCommand => new MvxCommand(async () => await NavToLiveTelemetry());
        public async Task NavToLiveTelemetry()
        {
            await _navigationService.Navigate<TelemetryViewModel>();
        }

        //Throttle
        ObservableCollection<ObservablePoint> _obeservablePointsThrottleA = new();
        ObservableCollection<ObservablePoint> _obeservablePointsThrottleB = new();

        //Brake
        ObservableCollection<ObservablePoint> _obeservablePointsBrakeA = new();
        ObservableCollection<ObservablePoint> _obeservablePointsBrakeB = new();

        //Gear
        ObservableCollection<ObservablePoint> _obeservablePointsGearA = new();
        ObservableCollection<ObservablePoint> _obeservablePointsGearB = new();

        //Speed
        ObservableCollection<ObservablePoint> _obeservablePointsSpeedA = new();
        ObservableCollection<ObservablePoint> _obeservablePointsSpeedB = new();

        //Time
        ObservableCollection<ObservablePoint> _obeservablePointsTimeA = new();
        ObservableCollection<ObservablePoint> _obeservablePointsTimeB = new();

        public HistoricalViewModel(IMvxNavigationService navigationService)
        {
            LoadGraphDataACommand = new MvxCommand(LoadGraphDataA);
            LoadGraphDataBCommand = new MvxCommand(LoadGraphDataB);
            _navigationService = navigationService;

            // Throttle Series
            ThrottleSeries = new ObservableCollection<ISeries>
            {
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsThrottleA,
                    Fill = null,
                    Name = "Lap A:",
                    LineSmoothness = 0,
                    GeometrySize = 0,
                },
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsThrottleB,
                    Fill = null,
                    Name = "Lap B:",
                    LineSmoothness = 0,
                    GeometrySize = 0
                }
            };

            // Brake Series
            BrakeSeries = new ObservableCollection<ISeries>
            {
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsBrakeA,
                    Fill = null,
                    Name = "Lap A:",
                    LineSmoothness = 0,
                    GeometrySize = 0,
                },
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsBrakeB,
                    Fill = null,
                    Name = "Lap B:",
                    LineSmoothness = 0,
                    GeometrySize = 0
                }
            };

            // Gear Series
            GearSeries = new ObservableCollection<ISeries>
            {
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsGearA,
                    Fill = null,
                    Name = "Lap A:",
                    LineSmoothness = 0,
                    GeometrySize = 0,
                },
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsGearB,
                    Fill = null,
                    Name = "Lap B:",
                    LineSmoothness = 0,
                    GeometrySize = 0
                }
            };

            // Speed Series
            SpeedSeries = new ObservableCollection<ISeries>
            {
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsSpeedA,
                    Fill = null,
                    Name = "Lap A:",
                    LineSmoothness = 0,
                    GeometrySize = 0,
                },
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsSpeedB,
                    Fill = null,
                    Name = "Lap B:",
                    LineSmoothness = 0,
                    GeometrySize = 0
                }
            };

            // Time Series
            TimeSeries = new ObservableCollection<ISeries>
            {
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsTimeA,
                    Fill = null,
                    Name = "Lap A:",
                    LineSmoothness = 0,
                    GeometrySize = 0,
                },
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsTimeB,
                    Fill = null,
                    Name = "Lap B:",
                    LineSmoothness = 0,
                    GeometrySize = 0
                }
            };
        }
        public ObservableCollection<ISeries> ThrottleSeries { get; set; }

        public ObservableCollection<ISeries> BrakeSeries { get; set; }

        public ObservableCollection<ISeries> GearSeries { get; set; }

        public ObservableCollection<ISeries> SpeedSeries { get; set; }

        public ObservableCollection<ISeries> TimeSeries { get; set; }

        public IMvxCommand LoadGraphDataACommand { get; set; }
        public async void LoadGraphDataA()
        {
            string jsonPath = SelectedFileA;
            try
            {
                using FileStream stream = File.OpenRead(jsonPath);
                ObservableCollection<DataPointModel>? wrappers =
                await JsonSerializer.DeserializeAsync<ObservableCollection<DataPointModel>>(stream);

                if (wrappers is null)
                {
                    // Deserialization failed
                }

                foreach (DataPointModel wrapper in wrappers)
                {
                    _obeservablePointsThrottleA.Add(new(wrapper.LapDistance, wrapper.Throttle));
                    _obeservablePointsBrakeA.Add(new(wrapper.LapDistance, wrapper.Brake));
                    _obeservablePointsGearA.Add(new(wrapper.LapDistance, wrapper.Gear));
                    _obeservablePointsSpeedA.Add(new(wrapper.LapDistance, wrapper.SpeedMph));
                    _obeservablePointsTimeA.Add(new(wrapper.LapDistance, wrapper.CurrentLapTime));
                }
            }
            catch
            {

            }


        }

        public IMvxCommand LoadGraphDataBCommand { get; set; }
        public async void LoadGraphDataB()
        {
            string jsonPath = SelectedFileB;
            try
            {
                using FileStream stream = File.OpenRead(jsonPath);
                ObservableCollection<DataPointModel>? wrappers =
                    await JsonSerializer.DeserializeAsync<ObservableCollection<DataPointModel>>(stream);

                if (wrappers is null)
                {
                    // Deserialization failed
                }

                foreach (DataPointModel wrapper in wrappers)
                {
                    _obeservablePointsThrottleB.Add(new(wrapper.LapDistance, wrapper.Throttle));
                    _obeservablePointsBrakeB.Add(new(wrapper.LapDistance, wrapper.Brake));
                    _obeservablePointsGearB.Add(new(wrapper.LapDistance, wrapper.Gear));
                    _obeservablePointsSpeedB.Add(new(wrapper.LapDistance, wrapper.SpeedMph));
                    _obeservablePointsTimeB.Add(new(wrapper.LapDistance, wrapper.CurrentLapTime)); ;
                }
            }
            catch
            {
                SelectedFileB = "";
            }

        }

    }
}
