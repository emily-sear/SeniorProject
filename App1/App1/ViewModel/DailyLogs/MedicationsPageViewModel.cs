using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class MedicationsPageViewModel : BaseViewModel
    {
        private List<MedicationDailyLog> medications;
        private string medicationName;
        private string timeTaken;
        private string dosage;
        private Boolean dailyMed;
        private string notes;
        private string medicationsLabel;
        private MedicationsPageView medicationsPageView;

        public Command NextCommand { get; }
        public Command BackCommand { get; }
        public Command YesCommand { get; }
        public Command NoCommand { get; }
        public Command AddAnotherCommand { get; }
        public INavigation Navigation { get; }
        public string MedicationName 
        { 
            get => medicationName; 
            set
            {
                medicationName = value;
                OnPropertyChanged(propertyName: "MedicationName");
            }
        }
        public string TimeTaken 
        {
            get => timeTaken; 
            set
            {
                this.timeTaken = value;
                OnPropertyChanged(propertyName: "TimeTaken");
            }
        }
        public string Dosage
        { 
            get => dosage; 
            set
            {
                this.dosage = value;
                OnPropertyChanged(propertyName: "Dosage");
            }
        }
        public bool DailyMed 
        {
            get => dailyMed;
            set
            {
                this.dailyMed = value;
                OnPropertyChanged(propertyName: "DailyMed");
            }
        }
        public string Notes 
        { 
            get => notes;
            set
            {
                this.notes = value;
                OnPropertyChanged(propertyName: "Notes");
            }
        }
        public string MedicationsLabel 
        { 
            get => medicationsLabel; 
            set
            {
                if(String.IsNullOrEmpty(this.medicationsLabel)) 
                {
                    medicationsLabel = value;
                }
                else
                {
                    medicationsLabel += ", " + value;
                }
                OnPropertyChanged(propertyName: "MedicationsLabel");
            }
        }
        private DailyLog dailyLog;

        public MedicationsPageViewModel(INavigation navigation, DailyLog dailyLog, MedicationsPageView medicationsPageView)
        {
            this.dailyLog = dailyLog;
            if(dailyLog.MedicationsSet)
            {
                foreach (MedicationDailyLog med in dailyLog.Medications)
                {
                    this.MedicationsLabel = med.MedicationName;
                }
            }
            Navigation = navigation;
            this.medicationsPageView = medicationsPageView;
            this.medications = new List<MedicationDailyLog>();
            NextCommand = new Command(OnNext);
            BackCommand = new Command(OnBack);
            YesCommand = new Command(OnYes);
            NoCommand = new Command(OnNo);
            AddAnotherCommand = new Command(OnAddAnother);
        }

        private async void OnNext()
        {
            if(!String.IsNullOrEmpty(this.medicationName))
            {
                MedicationDailyLog med = new MedicationDailyLog(this.medicationName, this.timeTaken, this.dosage, this.dailyMed, this.notes);
                this.medications.Add(med);
            }
            dailyLog.MedicationsSet = true;
            dailyLog.Medications = this.medications;
            await Navigation.PushAsync(new ReviewPageView(dailyLog));
        }

        private async void OnBack()
        {
            await Navigation.PushAsync(new SymptomsPageView(this.dailyLog));
        }

        private void OnYes()
        {
            this.dailyMed = true;
        }
        private void OnNo()
        {
            this.dailyMed = false;
        }

        private void OnAddAnother()
        {
            MedicationDailyLog med = new MedicationDailyLog(this.medicationName, this.timeTaken, this.dosage, this.dailyMed, this.notes);
            this.medications.Add(med);

            this.MedicationsLabel = this.MedicationName;
            this.MedicationName = "";
            this.TimeTaken = "";
            this.Dosage = "";
            this.DailyMed = true;
            this.Notes = "";

            Console.WriteLine(medicationsLabel);
            this.medicationsPageView.resetButton();
        }
    }
}
