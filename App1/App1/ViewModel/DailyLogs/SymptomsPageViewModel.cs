using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class SymptomsPageViewModel : BaseViewModel
    {
        private List<SymptomDailyLog> symptoms;
        private string symptomName;
        private Double severeness;
        private string timeframe;
        private string placement;
        private Boolean dailyLife;
        private string notes;
        private String symptomsLabel;

        private SymptomsPageView symptomsPageView;
        public Command NextCommand { get; }
        public Command BackCommand { get; }
        public Command YesCommand { get; }
        public Command NoCommand { get; }
        public Command AddAnotherCommand { get; }
        public INavigation Navigation { get; }
        public string SymptomsLabel
        { 
            get => symptomsLabel; 
            set
            {
                if(!String.IsNullOrEmpty(symptomsLabel))
                {
                    symptomsLabel += ", " + value;
                } else
                {
                    symptomsLabel = value;
                }

            } 
        }
        public string SymptomName { get => symptomName; set => symptomName = value; }
        public double Severeness { get => severeness; set => severeness = value; }
        public string Timeframe { get => timeframe; set => timeframe = value; }
        public string Placement { get => placement; set => placement = value; }
        public bool DailyLife { get => dailyLife; set => dailyLife = value; }
        public string Notes { get => notes; set => notes = value; }

        public SymptomsPageViewModel(INavigation navigation, SymptomsPageView symptomsPageView)
        {
            if(DailyLog.SymptomsSet)
            {
                foreach(SymptomDailyLog symptom in DailyLog.Symptoms)
                {
                    this.SymptomsLabel = symptom.NameOfSymptom;
                }
            }
            Navigation = navigation;
            this.symptomsPageView = symptomsPageView;
            this.symptoms = new List<SymptomDailyLog>();
            NextCommand = new Command(OnNext);
            BackCommand = new Command(OnBack);
            YesCommand = new Command(OnYes);
            NoCommand = new Command(OnNo);
            AddAnotherCommand = new Command(OnAddAnother);
        }

        private async void OnNext()
        {
            if(!String.IsNullOrEmpty(SymptomName))
            {
                SymptomDailyLog symptom = new SymptomDailyLog(this.symptomName, this.severeness, this.timeframe, this.dailyLife, this.placement, this.notes);
                this.symptoms.Add(symptom);
            }
            DailyLog.SymptomsSet = true;
            DailyLog.Symptoms = this.symptoms;
            await Navigation.PushAsync(new MedicationsPageView());
        }

        private async void OnBack()
        {
            await Navigation.PushAsync(new LevelPageView());
        }

        private void OnAddAnother()
        {
            SymptomDailyLog symptom = new SymptomDailyLog(this.symptomName, this.severeness, this.timeframe, this.dailyLife, this.placement, this.notes);
            this.symptoms.Add(symptom);
            this.SymptomsLabel = this.SymptomName;
            this.SymptomName = "";
            this.Severeness = 0;
            this.Timeframe = "";
            this.Placement = "";
            this.DailyLife = true;
            this.Notes = "";
            
            this.symptomsPageView.resetBorderColors();
            OnPropertyChanged(propertyName: "SymptomsLabel");
            OnPropertyChanged(propertyName: "SymptomName");
            OnPropertyChanged(propertyName: "Severeness");
            OnPropertyChanged(propertyName: "Timeframe");
            OnPropertyChanged(propertyName: "Placement");
            OnPropertyChanged(propertyName: "Notes");

        }

        private void OnYes()
        {
            this.dailyLife = true;
        }

        private void OnNo()
        {
            this.dailyLife = false;
        }
    }
}
