using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace App1.ViewModel
{
    class SettingsViewModel : ViewModel.BaseViewModel
    {
        public INavigation Navigation { get; }
        public Command HomeCommand { get; }
        public Command NewLogCommand { get; }
        public Command DailyLogsCommand { get; }
        public Command PrintLogsCommand { get; }
        public SettingsViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            PrintLogsCommand = new Command(OnPrintLogs);
            HomeCommand = new Command(OnHome);
            NewLogCommand = new Command(OnNewLogs);
            DailyLogsCommand = new Command(OnDailyLogs);

            
        }

        private async void OnHome()
        {
            await Navigation.PushAsync(new HomescreenView());
        }

        private async void OnNewLogs()
        {
            await Navigation.PushAsync(new LevelPageView(new DailyLog()));
        }

        private async void OnDailyLogs()
        {
            await Navigation.PushAsync(new CalendarDailyView());
        }

        private async void OnPrintLogs()
        {
            await Navigation.PushAsync(new PrintLogsView());
        }

    }
}
