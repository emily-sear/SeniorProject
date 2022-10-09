using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.IO;
using System.Text.RegularExpressions;
using Xamarin.Forms;
namespace App1
{
    public class SignUpPersonalDetailsModel : ViewModel.BaseViewModel
    {
        private string firstName;
        private string lastName;
        private string email;
        private string confirmEmail;


        private bool firstNameErrorVisible = false;
        private bool lastNameErrorVisible = false;
        private bool emailErrorVisible = false;


        public SignUpPersonalDetailsModel(INavigation navigation)
        {
            this.Navigation = navigation;
            NextCommand = new Command(OnNext);
            if (!String.IsNullOrEmpty(User.firstName))
            {
                this.firstName = User.firstName;
                OnPropertyChanged(propertyName: "FirstName");
            }
            if(!String.IsNullOrEmpty(User.lastName))
            {
                this.lastName = User.lastName;
                OnPropertyChanged(propertyName: "LastName");
            }
            if (!String.IsNullOrEmpty(User.email))
            {
                this.email = User.email;
                this.confirmEmail = User.email;
                OnPropertyChanged(propertyName: "Email");
                OnPropertyChanged(propertyName: "ConfirmEmail");
            }
        }

        private bool ValidateFields()
        {
            bool validate = false;
            if(!String.IsNullOrWhiteSpace(firstName) && !String.IsNullOrWhiteSpace(lastName) && !String.IsNullOrWhiteSpace(email) && !String.IsNullOrWhiteSpace(confirmEmail))
            {
                if(firstName.Length <= 30 && lastName.Length <= 30 && email.Length <= 50 & confirmEmail.Length <= 50)
                {
                    firstNameErrorVisible = false;
                    validate = true;
                }
            }

            return validate;
            
        }

        private bool validateName(string value)
        {
            return Regex.IsMatch(value, "^[A-Z][a-zA-Z]*$");
            
        }

        private bool validateEmail(string value)
        {
            if(String.IsNullOrEmpty(email))
            {
                return false;
            }
            else
            {
              if(Regex.IsMatch(value, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$") && Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    if(email.Equals(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool FirstNameErrorVisible
        {
            get => firstNameErrorVisible;

        }
        public string FirstName
        {
            get => firstName;
            set {
                if(validateName(value.Trim()))
                {
                    SetProperty(ref firstName, value.Trim());
                    SetProperty(ref firstNameErrorVisible, false);
                    OnPropertyChanged(propertyName: "FirstNameErrorVisible");

                } else
                { 
                    SetProperty(ref firstNameErrorVisible, true);
                    OnPropertyChanged(propertyName:"FirstNameErrorVisible");
                }
                
            }

            }
        public bool LastNameErrorVisible
        {
            get => lastNameErrorVisible;


        }
        public string  LastName
        {
            get => lastName;
            set
            {
                if (validateName(value.Trim()))
                {
                    SetProperty(ref lastName, value.Trim());
                    SetProperty(ref lastNameErrorVisible, false);
                    OnPropertyChanged(propertyName: "LastNameErrorVisible");

                }
                else
                {
                    SetProperty(ref lastNameErrorVisible, true);
                    OnPropertyChanged(propertyName: "LastNameErrorVisible");
                }

            }

        }

        public bool EmailErrorVisible
        {
            get => emailErrorVisible;

        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value.Trim());

        }
        public string ConfirmEmail
        {
            get => confirmEmail;
            set
            {
                if (validateEmail(value.Trim()))
                {
                    SetProperty(ref confirmEmail, value.Trim());
                    SetProperty(ref emailErrorVisible, false);
                    OnPropertyChanged(propertyName: "EmailErrorVisible");

                }
                else
                {
                    SetProperty(ref emailErrorVisible, true);
                    OnPropertyChanged(propertyName: "EmailErrorVisible");

                }

            }

        }
        public Command NextCommand { get;  }
        public INavigation Navigation { get; set; }

        private async void OnNext()
        {
            bool validation = ValidateFields();
            if(validation == true)
            {
                User.firstName = this.firstName;
                User.lastName = this.lastName;
                User.email = this.email;

                await Navigation.PushAsync(new SignUpMedicalDetailsPage());
            } 
            else
            {
                Console.Error.WriteLine("Validation was failed.");
            }
            
        }
    }
}
