using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class SignUpMedicationsViewModel : ViewModel.BaseViewModel
    {
        private string medicationName;
        private string medicationDosage;
        private DateTime medicationStartDate;
        private string medicationOrderingDoctor;

        private List<Medication> medications;

        public Command NextCommand { get; }
        public Command BackCommand { get; }
        public Command AddAnotherMedicationCommand { get; }
        public INavigation Navigation { get; set; }
        public string MedicationName 
        { 
            get => medicationName;
            set => medicationName = value;
        }
        public string MedicationDosage
        {
            get => medicationDosage;
            set => this.medicationDosage = value;
        }

        public DateTime MedicationStartDate
        {
            get => this.medicationStartDate;
            set => this.medicationStartDate = value;
        }

        public string MedicationOrderingDoctor
        {
            get => this.medicationOrderingDoctor;
            set => this.medicationOrderingDoctor = value;
        }

        public string MedicationsInList
        {
            get
            {
                string medicationsInList = "";
                for(int i = 0; i < this.medications.Count; i++)
                {
                    if(i == (this.medications.Count - 1))
                    {
                        medicationsInList += medications[i].Name;
                    }
                    else
                    {
                        medicationsInList += medications[i].Name + ", ";
                    }
                }
                return medicationsInList;
            }
        }

        public DateTime MinDate
        {
            get
            {
                return DateTime.Today.AddYears(-100);
            }
        }

        public DateTime MaxDate
        {
            get
            {
                return DateTime.Today;
            }
        }
        public SignUpMedicationsViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            NextCommand = new Command(OnNext);
            BackCommand = new Command(OnBack);
            AddAnotherMedicationCommand = new Command(AddAnotherMedication);
            this.medicationStartDate = DateTime.Today;
            if(MedicalDetails.medications != null)
            {
                this.medications = MedicalDetails.medications;
                OnPropertyChanged(propertyName: "MedicationsInList");
            } 
            else
            {
                this.medications = new List<Medication>();
            }
            
        }

        private void AddAnotherMedication()
        {
            Medication med = new Medication(medicationName, medicationDosage, medicationStartDate, medicationOrderingDoctor);
            this.medications.Add(med);

            this.medicationName = "";
            OnPropertyChanged(propertyName: "MedicationName");
            this.medicationDosage = "";
            OnPropertyChanged(propertyName: "MedicationDosage");
            this.medicationStartDate = DateTime.Today;
            OnPropertyChanged(propertyName: "MedicationStartDate");
            this.medicationOrderingDoctor = "";
            OnPropertyChanged(propertyName: "MedicationOrderingDoctor");

            OnPropertyChanged(propertyName: "MedicationsInList");
        }
        private async void OnNext()
        {
            
            if(!String.IsNullOrEmpty(this.medicationName))
            {
                Medication med = new Medication(medicationName, medicationDosage, medicationStartDate, medicationOrderingDoctor);
                this.medications.Add(med);
            }

            MedicalDetails.medications = this.medications;
            Console.WriteLine("made it to here");

            await Navigation.PushAsync(new SignUpReviewPage());

        }
        
        private async void OnBack()
        {
            await Navigation.PushAsync(new SignUpMedicalDetailsPage());
        }
    }
}
