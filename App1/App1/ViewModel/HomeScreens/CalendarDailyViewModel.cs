using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class CalendarDailyViewModel : ViewModel.BaseViewModel
    {

        private String selectedDate;
        private double painAmount;
        private double moodAmount;
        private double fatigueAmount;
        public INavigation Navigation { get; }
        public Command SettingsCommand { get; }
        public Command NewLogCommand { get; }
        public Command HomescreenCommand { get; }
        public Command PrintLogsCommand { get; }

        public double PainAmount
        {
            get
            {
                // TODO: get pain amount 
                return 5;
            }
            set => this.painAmount = value;
        }

        public double MoodAmount
        {
            get
            {
                // TODO: get mood amount 
                return 5;
            }
            set => this.moodAmount = value;

        }

        public double FatigueAmount
        {
            get
            {
                // TODO: fatigue amount 
                return 5;
            }
            set => this.fatigueAmount = value;
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
            Console.WriteLine("made it to the new logs button");
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
