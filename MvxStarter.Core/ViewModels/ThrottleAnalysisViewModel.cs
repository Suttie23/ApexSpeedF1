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
using Codemasters.F1_2021;
using System.Text.Json;

namespace ApexSpeed.Core.ViewModels
{
    public class ThrottleAnalysisViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;

        ObservableCollection<ObservablePoint> _obeservablePoints = new();

        public IMvxCommand ReturnToHistoricalCommand => new MvxCommand(async () => await ReturnToHistorical());
        public async Task ReturnToHistorical()
        {
            await _navigationService.Navigate<HistoricalViewModel>();
        }

        public ThrottleAnalysisViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            LoadGraphDataCommand = new MvxCommand(LoadGraphData);

            _obeservablePoints = new ObservableCollection<ObservablePoint>
            {

            };

            Series = new ObservableCollection<ISeries>
            {
            new LineSeries<ObservablePoint>
                {
                    Values = _obeservablePoints,
                    Fill = null,
                    LineSmoothness = 0,
                    
                }
            };
        }

        public ObservableCollection<ISeries> Series { get; set; }


        public IMvxCommand LoadGraphDataCommand { get; set; }
        public async void LoadGraphData()
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
                _obeservablePoints.Add(new(wrapper.LapDistance, wrapper.Throttle));
            }

        }

    }
}
