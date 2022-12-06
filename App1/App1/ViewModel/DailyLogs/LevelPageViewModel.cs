using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class LevelPageViewModel : BaseViewModel
    {
        private double painScale;
        private double moodScale;
        private double fatigueScale;
        private double overallScale;
        private double avgHeartRate;
        private Boolean onPeriod;
        private DailyLog dailyLog;

        private LevelPageView levelPageView;
        public INavigation Navigation { get; }
        public Command NextCommand { get; }
        public Command BackCommand { get; }
        public Command YesCommand { get; }
        public Command NoCommand { get; }
        public double PainScale
        {
            get => painScale; 
            set
            {
                this.painScale = value;
                OnPropertyChanged(propertyName: "PainScale");
            } 

        }
        public double MoodScale
        {
            get => moodScale;
            set
            {
                this.moodScale = value;
                OnPropertyChanged(propertyName: "MoodScale");
            }
        }
        public double FatigueScale
        {
            get => fatigueScale; 
            set
            {
                fatigueScale = value;
                OnPropertyChanged(propertyName: "FatigueValue"); 
            }
        }
        public double OverallScale
        {
            get => overallScale;
            set
            {
                overallScale = value;
                OnPropertyChanged(propertyName: "OverallScale");
            }
        }

        public double AvgHeartRate
        {
            get => avgHeartRate;
            set
            {
                try
                {
                    avgHeartRate = (double) value;
                    OnPropertyChanged(propertyName: "AvgHeartRate");
                } catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }

            }
        }

        public Boolean OnPeriod
        {
            get => onPeriod; 
            set
            {
                this.onPeriod = value;
                if(this.onPeriod == true)
                {
                   this.levelPageView.yesButton_Clicked(null, null);
                }
                else
                {
                    this.levelPageView.noButton_Clicked(null, null);
                }

                OnPropertyChanged(propertyName: "OnPeriod");
            }
        }

        public LevelPageViewModel(INavigation navigation, DailyLog dailyLog, LevelPageView levelPageView)
        {
            this.levelPageView = levelPageView;
            this.dailyLog = dailyLog;
            if (dailyLog.ValuesSet)
            {
                PainScale = dailyLog.PainScale;
                MoodScale = dailyLog.MoodScale;
                FatigueScale = dailyLog.FatigueScale;
                OverallScale = dailyLog.OverallScale;
                if(dailyLog.AvgHeartRate != 0)
                {
                    AvgHeartRate = dailyLog.AvgHeartRate;
                }
                OnPeriod = dailyLog.OnPeriod;
            } 
            this.Navigation = navigation;
            NextCommand = new Command(OnNext);
            YesCommand = new Command(OnYes);
            NoCommand = new Command(OnNo);
            BackCommand = new Command(OnBack);
        }
        private async void OnBack()
        {
            await Navigation.PushAsync(new HomescreenView());
        }
        private async void OnNext()
        {
            this.dailyLog.PainScale = this.painScale;
            this.dailyLog.MoodScale = this.moodScale;
            this.dailyLog.FatigueScale = this.fatigueScale;
            this.dailyLog.OverallScale = this.overallScale;
            this.dailyLog.AvgHeartRate = this.avgHeartRate;
            this.dailyLog.OnPeriod = this.onPeriod;
            this.dailyLog.ValuesSet = true;
            await Navigation.PushAsync(new SymptomsPageView(this.dailyLog));
        }

        private void OnYes()
        {
            this.onPeriod = true;
        }

        private void OnNo()
        {
            this.onPeriod = false;
        }
    }
}
