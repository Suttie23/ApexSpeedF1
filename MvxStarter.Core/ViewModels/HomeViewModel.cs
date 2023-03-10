using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MvxStarter.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;


        public HomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IMvxCommand NavToLiveTelemetryCommand => new MvxCommand(async () => await NavToLiveTelemetry());

        public async Task NavToLiveTelemetry()
        {
            await _navigationService.Navigate<TelemetryViewModel>();
        }
    }
}
