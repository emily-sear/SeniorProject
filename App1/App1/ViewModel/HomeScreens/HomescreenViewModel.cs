using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace App1.ViewModel
{
    class HomescreenViewModel
    {
        public INavigation Navigation { get; }
        public Command SettingsCommand { get; }
        public Command NewLogCommand { get; }
        public Command DailyLogsCommand { get; }
        public Command PrintLogsCommand { get; }

        public String MostCommonSymptom
        {
            get
            {
                return "Migraines";
            }
        }
        public int AvgAmtOfPain
        {
            get
            {
                // TODO: get the average amount of pain
                return 5;
            }
        }

        public int AvgMood
        {
            get
            {
                // TODO: get the average mood from this week
                return 5; 
            }
        }

        public int AvgFatigue
        {
            get
            {
                // TODO: get the average fatigue from this week
                return 5;
            }
        }

        public HomescreenViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            SettingsCommand = new Command(OnSettings);
            PrintLogsCommand = new Command(OnPrintLogs);
            DailyLogsCommand = new Command(OnDailyLogs);
            NewLogCommand = new Command(OnNewLogs);
        }
        private async void OnSettings()
        {
            await Navigation.PushAsync(new SettingsView());
        }

        private async void OnNewLogs()
        {
            await Navigation.PushAsync(new LevelPageView());
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
