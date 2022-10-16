﻿using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace App1.ViewModel
{
    class PrintLogsViewModel : ViewModel.BaseViewModel
    {
        private DateTime startDate;
        private DateTime endDate;
        public Command HomeCommand { get; }
        public Command NewLogCommand { get; }
        public Command DailyLogsCommand { get; }
        public Command SettingsCommand { get; }
        public Command PrintButtonCommand { get; }
        public INavigation Navigation { get; }

        public Boolean OverviewCheckChanged { get; set; }
        public Boolean DailyOverviewCheckChanged { get; set; }
        public Boolean DailyIndepthCheckChanged { get; set; }
        public DateTime StartDate
        { 
            get => startDate; 
            set
            {
                startDate = value;
                OnPropertyChanged(propertyName: "MinEndDate");
            }
        }
        public DateTime EndDate
        { 
            get => endDate; 
            set
            {
                endDate = value;
                OnPropertyChanged(propertyName: "MaxStartDate");
            }
        }
        
        public DateTime MaxStartDate
        {
            get 
            {
                if(DateTime.Compare(this.endDate, DateTime.Today) < 0)
                {
                    return this.endDate;
                } else
                {
                    return DateTime.Today;
                }
            }
        }

        public DateTime MaxEndDate
        {
            get
            {
                return DateTime.Today;
            }
        }

        public DateTime MinStartDate
        {
            get
            {
                return DateTime.Today.AddYears(-2);
            }
        }

        public DateTime MinEndDate
        {
            get
            {
                if(DateTime.Compare(this.startDate, DateTime.Today.AddYears(-2)) > 0 && DateTime.Compare(startDate, DateTime.Today) != 0)
                {
                    return this.startDate;
                } 
                else
                {
                    return DateTime.Today.AddYears(-2);
                }
            }
        }
        public PrintLogsViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            SettingsCommand = new Command(OnSettings);
            HomeCommand = new Command(OnHome);
            DailyLogsCommand = new Command(OnDailyLogs);
            this.endDate = DateTime.Today;
            this.startDate = DateTime.Today;
        }

        private async void OnDailyLogs()
        {
            await Navigation.PushAsync(new CalendarDailyView());
        }
        private async void OnHome()
        {
            await Navigation.PushAsync(new HomescreenView());
        }
        private async void OnSettings()
        {
            Console.WriteLine("made it");
            await Navigation.PushAsync(new SettingsView());
        }
    }
}