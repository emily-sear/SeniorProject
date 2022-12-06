using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace App1.ViewModel
{
    class HomescreenViewModel : BaseViewModel
    {
        private List<DailyLog> dailyLogs;
        public INavigation Navigation { get; }
        public Command SettingsCommand { get; }
        public Command NewLogCommand { get; }
        public Command DailyLogsCommand { get; }
        public Command PrintLogsCommand { get; }

        public Double AvgOverall
        {
            get; set;
        }
        public Double AvgAmtOfPain
        {
            get; set;
        }

        public Double AvgMood
        {
            get; set;
        }

        public Double AvgFatigue
        {
            get; set;
        }

        public HomescreenViewModel(INavigation navigation)
        {

            this.Navigation = navigation;
            SettingsCommand = new Command(OnSettings);
            PrintLogsCommand = new Command(OnPrintLogs);
            DailyLogsCommand = new Command(OnDailyLogs);
            NewLogCommand = new Command(OnNewLogs);
            this.FillValues();
        }
        private async void FillValues()
        {
            RestService restService = new RestService();
            this.dailyLogs = await restService.GetDailyLogDataAsync("{\"$and\":[{\"Datetime\": { \"$date\": {\"$gt\":\"" + makeRestDate(DateTime.Today, -8) + "\", \"$lt\": \"" + makeRestDate(DateTime.Today, 1) + "\"}}},{\"Email\":\"" + User.email + "\"}]}");

            double overall = 0;
            double pain = 0;
            double mood = 0;
            double fatigue = 0;
            int count = 0;
            foreach(DailyLog dailyLog in this.dailyLogs)
            {
                overall += dailyLog.OverallScale;
                pain += dailyLog.PainScale;
                mood += dailyLog.MoodScale;
                fatigue += dailyLog.FatigueScale;
                count++;
            }

            this.AvgOverall = overall / count;
            this.AvgAmtOfPain = pain / count;
            this.AvgMood = mood / count;
            this.AvgFatigue = fatigue / count;

            OnPropertyChanged(propertyName: "AvgOverall");
            OnPropertyChanged(propertyName: "AvgAmtOfPain");
            OnPropertyChanged(propertyName: "AvgMood");
            OnPropertyChanged(propertyName: "AvgFatigue");
        }
        private String makeRestDate(DateTime oldDate, double addSubtract)
        {
            DateTime date = oldDate.AddDays(addSubtract);
            String dateString = date.Year + "-";
            if (date.Month < 10)
            {
                dateString += "0" + date.Month + "-";
            }
            else
            {
                dateString += date.Month + "-";
            }

            if (date.Day < 10)
            {
                dateString += "0" + date.Day;
            }
            else
            {
                dateString += date.Day;
            }
            return dateString;
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
