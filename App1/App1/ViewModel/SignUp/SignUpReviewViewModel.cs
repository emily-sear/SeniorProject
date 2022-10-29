using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class SignUpReviewViewModel : ViewModel.BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public string FullName
        {
            get
            {
                return User.firstName + " " + User.lastName;
            }
        }

        public string Email
        {
            get
            {
                return User.email;
            }
        }

        public Boolean IsDiganosesNullOrEmpty
        {
            get
            {
                if(MedicalDetails.diganoses != null || MedicalDetails.diganoses.Count != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public Boolean IsMedicationsNullOrEmpty
        {
            get
            {
                if(MedicalDetails.medications != null || MedicalDetails.medications.Count != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string Diganoses
        {
            get
            {
                string names = "";
               
                for(int i = 0; i < MedicalDetails.diganoses.Count; i++)
                {
                    if(i == (MedicalDetails.diganoses.Count - 1))
                    {
                        names += MedicalDetails.diganoses[i];
                    }
                    else
                    {
                        names += MedicalDetails.diganoses[i] + ", ";
                    }
                }
                return names;
            }
        }

        public string Medications
        {
            get
            {
                string medicationsInList = "";
                for (int i = 0; i < MedicalDetails.medications.Count; i++)
                {
                    if (i == (MedicalDetails.medications.Count - 1))
                    {
                        medicationsInList += MedicalDetails.medications[i].Name;
                    }
                    else
                    {
                        medicationsInList += MedicalDetails.medications[i].Name + ", ";
                    }
                }
                return medicationsInList;
            }
        }

        public Command BackCommand { get;  }
        public Command SubmitCommand { get; }

        public SignUpReviewViewModel(INavigation navaigation)
        {
            Navigation = navaigation;
            OnPropertyChanged(propertyName: "IsMedicationsNullOrEmpty");
            OnPropertyChanged(propertyName: "IsDiganosesNullOrEmpty");
            BackCommand = new Command(OnBack);
            SubmitCommand = new Command(OnSubmit);
        }

        private async void OnBack()
        {
            await Navigation.PushAsync(new SignUpMedicationsPage());
        }

        private async void OnSubmit()
        {
            RestService restService = new RestService();
            await restService.PostAccountCreationDataAsync();

            if (User.inDatabase == true)
            {
                await Navigation.PushAsync(new HomescreenView());
            }
        }
    }
}
