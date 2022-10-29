using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private  void LoginButtonClicked(object sender, EventArgs e)
        {
            RestService restService = new RestService();
/*            restService.PostDataAsync();*/
            Console.WriteLine("Rest Service Completed");
        }

        private async void CreateNewAccountButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPersonalDetailsPage());
        }
    }
}
