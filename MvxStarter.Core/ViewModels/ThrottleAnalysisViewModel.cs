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

        public IMvxCommand ReturnToHistoricalCommand => new MvxCommand(async () => await ReturnToHistorical());
        public async Task ReturnToHistorical()
        {
            await _navigationService.Navigate<HistoricalViewModel>();
        }

        public ThrottleAnalysisViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            LoadGraphDataCommand = new MvxCommand(LoadGraphData);
        }


        public IMvxCommand LoadGraphDataCommand { get; set; }
        public void LoadGraphData()
        {

        }

        public ISeries[] Series { get; set; } =
        {
                new LineSeries<double>
                {
                    Values = new double[] { 5, 0, 5, 0, 5, 0, 0, 5, 0 , 0, 5, 0 , 0, 5, 0  },
                    Fill = null,
                    GeometrySize = 0,
                    // use the line smoothness property to control the curve
                    // it goes from 0 to 1
                    // where 0 is a straight line and 1 the most curved
                    LineSmoothness = 0
                },
                new LineSeries<double>
                {
                    Values = new double[] { 7, 2, 7, 2, 7, 2 },
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 1
                }
        };

    }
}
