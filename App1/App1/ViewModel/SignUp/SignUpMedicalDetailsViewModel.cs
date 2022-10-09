using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class SignUpMedicalDetailsViewModel : ViewModel.BaseViewModel
    {
        private string userType;
        private string diganoses;
        private bool userTypeErrorVisible;

        public Command BackCommand { get; }
        public Command NextCommand { get; }
        public INavigation Navigation { get; set; }

        public string Diganoses 
        { 
            get => this.diganoses;
            set
            {
                this.diganoses = value;
            }
        }
        public string UserType
        {   
            get => userType; 
            set 
            {
                this.userType = value;
            }
        }

        public bool UserTypeErrorVisible
        {
            get => userTypeErrorVisible;
        }

        public SignUpMedicalDetailsViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            BackCommand = new Command(OnBack);
            NextCommand = new Command(OnNext);

            if(MedicalDetails.diganoses != null && MedicalDetails.diganoses.Count > 0)
            {
                this.diganoses = String.Join(", ", MedicalDetails.diganoses);
            }

            if (User.userType.ToString() != "UNDEFINED")
            {
                this.userTypeErrorVisible = false;
                OnPropertyChanged(propertyName: "UserTypeErrorVisible");
                string enumType = User.userType.ToString();
                if (enumType.Equals("OTHER"))
                {
                    this.userType = "Other";
                }
                else if (enumType.Equals("CAREGIVER"))
                {
                    this.userType = "Caregiver";
                }
                else if (enumType.Equals("PATIENT"))
                {
                    this.userType = "Person tracking own symptoms";
                }

                OnPropertyChanged(propertyName: "UserType");
            } 

        }

        private async void OnBack()
        {
            if(!String.IsNullOrEmpty(this.diganoses))
            {
                List<string> diganosesList = this.diganoses.Split(',').ToList();

                MedicalDetails.diganoses = diganosesList;
            }
            if(!String.IsNullOrEmpty(this.userType))
            {
                if (this.userType == "Other")
                {
                    User.userType = User.userTypes.OTHER;
                }
                else if (this.userType == "Caregiver")
                {
                    User.userType = User.userTypes.CAREGIVER;
                }
                else if(this.userType == "Person tracking own symptoms")
                {
                    User.userType = User.userTypes.PATIENT;
                }
            }

            
            await Navigation.PushAsync(new SignUpPersonalDetailsPage());
        }

        private async void OnNext()
        {

            if (!String.IsNullOrEmpty(this.diganoses))
            {
                List<string> diganosesList = this.diganoses.Split(',').ToList();

                MedicalDetails.diganoses = diganosesList;
            }
            if (!String.IsNullOrEmpty(this.userType))
            {
                if (this.userType == "Other")
                {
                    User.userType = User.userTypes.OTHER;
                }
                else if (this.userType == "Caregiver")
                {
                    User.userType = User.userTypes.CAREGIVER;
                }
                else if(this.userType == "Person tracking own symptoms")
                {
                    User.userType = User.userTypes.PATIENT;
                }
                await Navigation.PushAsync(new SignUpMedicationsPage());
            }

        }
    }
}
