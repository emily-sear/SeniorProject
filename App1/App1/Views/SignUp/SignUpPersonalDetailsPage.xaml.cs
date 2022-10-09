using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using App1.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPersonalDetailsPage : ContentPage
    {
        public SignUpPersonalDetailsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            BindingContext = new SignUpPersonalDetailsModel(Navigation);

        }

        private async void Next_Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(SignUpMedicalDetailsPage)}");
        }
    }
}