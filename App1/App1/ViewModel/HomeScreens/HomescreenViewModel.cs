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

                return "";
            }
        }
        public Double AvgAmtOfPain
        {
            get { 
            
                return 0;
            }
        }

        public Double AvgMood
        {
            get
            {
                return 0;
            }
        }

        public Double AvgFatigue
        {
            get
            {
                return 0;
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
