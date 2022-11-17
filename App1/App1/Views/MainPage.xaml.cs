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

        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            RestService restService = new RestService();
            await Navigation.PushAsync(new HomescreenView());
        }

        private async void CreateNewAccountButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPersonalDetailsPage());
        }
    }
}
