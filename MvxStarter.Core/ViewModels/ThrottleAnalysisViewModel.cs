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
using Newtonsoft.Json;
using System.IO;
using ApexSpeed.Core.Models;
using System.Linq;
using System.Collections.ObjectModel;
using Codemasters.F1_2021;

namespace ApexSpeed.Core.ViewModels
{
    public class ThrottleAnalysisViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;

        private readonly ObservableCollection<ObservablePoint> _obeservablePoints;

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
                    LineSmoothness = 0
                }
            };
        }

        public ObservableCollection<ISeries> Series { get; set; }


        public IMvxCommand LoadGraphDataCommand { get; set; }
        public void LoadGraphData()
        {
            _obeservablePoints.Add(new ObservablePoint(0, 4));
        }

    }
}
