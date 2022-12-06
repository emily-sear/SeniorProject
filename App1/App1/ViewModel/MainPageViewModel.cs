using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public String Email { get; set; }
        public INavigation Navigation { get; }
        public Command CreateNewAccountCommand { get; }
        public Command SubmitCommand { get; }
        public MainPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            SubmitCommand = new Command(LoginButtonClicked);
            CreateNewAccountCommand = new Command(CreateNewAccountButtonClicked);
        }
        private async void LoginButtonClicked()
        {
            RestService restService = new RestService();
            Console.WriteLine(this.Email);
            if(!String.IsNullOrEmpty(this.Email))
            {
                bool userFound = await restService.SignUserIn(Email);
                Console.WriteLine(userFound);
                if(userFound == true)
                {
                    await this.Navigation.PushAsync(new HomescreenView());
                }
            }
            //TODO: make a subscription to say that sign in was unsuccessful
        }

        private async void CreateNewAccountButtonClicked()
        {
            await this.Navigation.PushAsync(new SignUpPersonalDetailsPage());
        }
    }
}
