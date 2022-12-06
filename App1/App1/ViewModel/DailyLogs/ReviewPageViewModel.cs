using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class ReviewPageViewModel
    {
        private Double painLevel;
        private Double moodLevel;
        private Double fatigueLevel;
        private string medications;
        private string symptoms;
        public Command BackCommand { get; }
        public Command SubmitCommand { get; }
        public INavigation Navigation { get; }
        public Double PainLevel { get => painLevel; }
        public Double MoodLevel { get => moodLevel; }
        public Double FatigueLevel { get => fatigueLevel; }
        public String Medications { get => medications; }
        public String Symptoms { get => symptoms; }
        public Boolean IsMedicationsNotNullOrEmpty
        {
            get
            {
                return !String.IsNullOrEmpty(this.medications);
            }
        }
        public Boolean IsSymptomsNotNullOrEmpty
        {
            get
            {
                return !String.IsNullOrEmpty(this.symptoms);
            }
        }
        private DailyLog dailyLog;
        public ReviewPageViewModel(INavigation navigation, DailyLog dailyLog)
        {
            this.dailyLog = dailyLog;
            this.Navigation = navigation;
            this.painLevel = dailyLog.PainScale;
            this.moodLevel = dailyLog.MoodScale;
            this.fatigueLevel = dailyLog.FatigueScale;
            foreach(SymptomDailyLog symptom in dailyLog.Symptoms)
            {
                if (String.IsNullOrEmpty(this.symptoms))
                {
                    this.symptoms = symptom.NameOfSymptom;
                }
                else
                {
                    this.symptoms += ", " + symptom.NameOfSymptom;
                }
            }
            foreach(MedicationDailyLog medication in dailyLog.Medications)
            {
                if(String.IsNullOrEmpty(this.medications))
                {
                    this.medications = medication.MedicationName;
                }
                else
                {
                    this.medications += ", " + medication.MedicationName;
                }
            }
            BackCommand = new Command(OnBack);
            SubmitCommand = new Command(OnSubmit);
        }

        private async void OnBack()
        {

            await Navigation.PushAsync(new MedicationsPageView(this.dailyLog));
        }

        private async void OnSubmit()
        {
            RestService restService = new RestService();
            this.dailyLog.CurrentDate = DateTime.Today;
            await restService.PostDailyLogDataAsync(this.dailyLog);
            await Navigation.PushAsync(new HomescreenView());
        }
    }
}
