#nullable enable
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvxStarter.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;
using System.IO;
using ApexSpeed.Core.Models;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace ApexSpeed.Core.ViewModels
{
    public class ThrottleAnalysisViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;

        ObservableCollection<ObservablePoint> _obeservablePointsA = new();
        ObservableCollection<ObservablePoint> _obeservablePointsB = new();

        public IMvxCommand ReturnToHistoricalCommand => new MvxCommand(async () => await ReturnToHistorical());
        public async Task ReturnToHistorical()
        {
            await _navigationService.Navigate<HistoricalViewModel>();
        }

        public ThrottleAnalysisViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            LoadGraphDataACommand = new MvxCommand(LoadGraphDataA);
            LoadGraphDataBCommand = new MvxCommand(LoadGraphDataB);
            RemoveGraphDataACommand = new MvxCommand(RemoveGraphDataA);
            RemoveGraphDataBCommand = new MvxCommand(RemoveGraphDataB);

            _obeservablePointsA = new ObservableCollection<ObservablePoint>
            {

            };
            _obeservablePointsB = new ObservableCollection<ObservablePoint>
            {

            };

            Series = new ObservableCollection<ISeries>
            {
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsA,
                    Fill = null,
                    LineSmoothness = 0,
                    GeometrySize = 0,                                     
                },
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePointsB,
                    Fill = null,
                    LineSmoothness = 0,
                    GeometrySize = 0
                }
            };
        }

        public ObservableCollection<ISeries> Series { get; set; }


        public IMvxCommand LoadGraphDataACommand { get; set; }
        public async void LoadGraphDataA()
        {
            string jsonPath = @"C:\Users\Suttie\Desktop\F1_2021_Telemetry\Lap Files\Catalunya 3-13-2023 12.01 PM\Lap 1.json";
            using FileStream stream = File.OpenRead(jsonPath);
            ObservableCollection<ThrottleDataPointModel>? wrappers =
                await JsonSerializer.DeserializeAsync<ObservableCollection<ThrottleDataPointModel>>(stream);

            if (wrappers is null)
            {
                // Deserialization failed
            }

            foreach (ThrottleDataPointModel wrapper in wrappers)
            {
                _obeservablePointsA.Add(new(wrapper.LapDistance, wrapper.Throttle));
            }

        }

        public IMvxCommand LoadGraphDataBCommand { get; set; }
        public async void LoadGraphDataB()
        {
            string jsonPath = @"C:\Users\Suttie\Desktop\F1_2021_Telemetry\Lap Files\Catalunya 3-13-2023 12.01 PM\Lap 2.json";
            using FileStream stream = File.OpenRead(jsonPath);
            ObservableCollection<ThrottleDataPointModel>? wrappers =
                await JsonSerializer.DeserializeAsync<ObservableCollection<ThrottleDataPointModel>>(stream);

            if (wrappers is null)
            {
                // Deserialization failed
            }

            foreach (ThrottleDataPointModel wrapper in wrappers)
            {
                _obeservablePointsB.Add(new(wrapper.LapDistance, wrapper.Throttle));
            }

        }

        public IMvxCommand RemoveGraphDataACommand { get; set; }
        public void RemoveGraphDataA()
        {

            _obeservablePointsA.Clear();

        }

        public IMvxCommand RemoveGraphDataBCommand { get; set; }
        public void RemoveGraphDataB()
        {

            _obeservablePointsB.Clear();

        }

    }
}
