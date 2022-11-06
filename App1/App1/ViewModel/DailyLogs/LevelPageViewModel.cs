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
        private double avgHeartRate;
        private Boolean onPeriod;
        private LevelPageView levelPageView;
        public INavigation Navigation { get; }
        public Command NextCommand { get; }
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

        public LevelPageViewModel(INavigation navigation, LevelPageView levelPageView)
        {
            this.levelPageView = levelPageView;
            if (DailyLog.ValuesSet)
            {
                PainScale = DailyLog.PainScale;
                MoodScale = DailyLog.MoodScale;
                FatigueScale = DailyLog.FatigueScale;
                AvgHeartRate = DailyLog.AvgHeartRate;
                OnPeriod = DailyLog.OnPeriod;
            } 
            this.Navigation = navigation;
            NextCommand = new Command(OnNext);
            YesCommand = new Command(OnYes);
            NoCommand = new Command(OnNo);
        }

        private async void OnNext()
        {
            DailyLog.PainScale = this.painScale;
            DailyLog.MoodScale = this.moodScale;
            DailyLog.FatigueScale = this.fatigueScale;
            DailyLog.AvgHeartRate = this.avgHeartRate;
            DailyLog.OnPeriod = this.onPeriod;
            DailyLog.ValuesSet = true;
            await Navigation.PushAsync(new SymptomsPageView());
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
