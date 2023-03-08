using ApexSpeedApp.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSpeedApp.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand LiveAnalysisViewCommand { get; set; }
        public RelayCommand HistoricalAnalysisViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public LiveAnalysisViewModel LiveAnalysisVM { get; set; }
        public HistoricalAnalysisViewModel HistoricalAnalysisVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }

            set 
            { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            LiveAnalysisVM = new LiveAnalysisViewModel();
            HistoricalAnalysisVM = new HistoricalAnalysisViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            LiveAnalysisViewCommand = new RelayCommand(o =>
            {
                CurrentView = LiveAnalysisVM;
            });

            HistoricalAnalysisViewCommand = new RelayCommand(o =>
            {
                CurrentView = HistoricalAnalysisVM;
            });


        }


    }
}
