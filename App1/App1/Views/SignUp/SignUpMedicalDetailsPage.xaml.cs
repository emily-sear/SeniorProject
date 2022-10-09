using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpMedicalDetailsPage : ContentPage
    {
 
        public SignUpMedicalDetailsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            BindingContext = new ViewModel.SignUpMedicalDetailsViewModel(Navigation);
            
        }

        private void RelationPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (RelationPicker.SelectedItem == "Other")
            {
                otherInput.IsVisible = true;
            }
        }

        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPersonalDetailsPage());
        }

        private async void NextButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpMedicationsPage());
        }
    }
}