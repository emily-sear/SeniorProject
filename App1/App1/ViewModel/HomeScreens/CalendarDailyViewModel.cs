using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class CalendarDailyViewModel : ViewModel.BaseViewModel
    {

        private String selectedDate;
        private double painAmount;
        private double moodAmount;
        private double fatigueAmount;
        private Task<String> getResponse;
        public INavigation Navigation { get; }
        public Command SettingsCommand { get; }
        public Command NewLogCommand { get; }
        public Command HomescreenCommand { get; }
        public Command PrintLogsCommand { get; }

        public double PainAmount
        {
            get => this.painAmount;
            set
            {
                this.painAmount = value;
                OnPropertyChanged("PainAmount");
            }
        }

        public double MoodAmount
        {
            get => this.moodAmount;
            set
            {
                this.moodAmount = value;
                OnPropertyChanged("MoodAmount");
            }

        }

        public double FatigueAmount
        {
            get => this.fatigueAmount;
            set
            {
                this.fatigueAmount = value;
                OnPropertyChanged("FatigueAmount");
            }
        }

        public String MaxDate
        {
            get => DateTime.Today.ToString();
        }

        public String MinDate
        {
            get => DateTime.Today.AddYears(-2).ToString();
        }
        public String SelectedDate
        {
            get => this.selectedDate;
            set => this.selectedDate = value;
        }
        public Command DateChosen { get; }
        public DateTime StartDate
        {
            get
            {
                return DateTime.Today;
            }
        }
        public CalendarDailyViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.selectedDate = DateTime.Today.ToString();
            OnPropertyChanged(propertyName: "SelectedDate");
            SettingsCommand = new Command(OnSettings);
            NewLogCommand = new Command(OnNewLogs);
            HomescreenCommand = new Command(OnHome);
            PrintLogsCommand = new Command(OnPrintLogs);
            DateChosen = new Command(OnDateChosen);


        }

        public async void getDailyLog()
        {
            RestService restService = new RestService();
            await restService.GetDailyLogDataAsync("{\"Email\": \"esear@cuw.edu\"}");
            this.FatigueAmount = CalendarObject.fatigueScale;
            this.MoodAmount = CalendarObject.moodScale;
            this.PainAmount = CalendarObject.painScale;

        }
        private void OnDateChosen()
        {
            OnPropertyChanged(propertyName: "SelectedDate");
        }

        private async void OnSettings()
        {
            await Navigation.PushAsync(new SettingsView());
        }

        private async void OnNewLogs()
        {
            await Navigation.PushAsync(new LevelPageView(new DailyLog()));
        }

        private async void OnHome()
        {
            await Navigation.PushAsync(new HomescreenView());
        }

        private async void OnPrintLogs()
        {
            await Navigation.PushAsync(new PrintLogsView());
        }
    }
}
