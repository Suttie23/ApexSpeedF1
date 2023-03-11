using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvxStarter.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ApexSpeed.Core.ViewModels
{
    public class HistoricalViewModel : MvxViewModel
    {
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

        public IMvxCommand NavToThrottleCommand => new MvxCommand(async () => await NavToThrottle());
        public async Task NavToThrottle()
        {
            await _navigationService.Navigate<ThrottleAnalysisViewModel>();
        }

        public HistoricalViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
