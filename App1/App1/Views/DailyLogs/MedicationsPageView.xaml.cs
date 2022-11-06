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
    public partial class MedicationsPageView : ContentPage
    {
        public MedicationsPageView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            BindingContext = new ViewModel.MedicationsPageViewModel(Navigation, this);
        }
        private void yesButton_Clicked(object sender, EventArgs e)
        {
            yesButton.BorderColor = Color.Black;
            noButton.BorderColor = Color.Transparent;
        }

        private void noButton_Clicked(object sender, EventArgs e)
        {
            noButton.BorderColor = Color.Black;
            yesButton.BorderColor = Color.Transparent;
        }

        public void resetButton()
        {
            noButton.BorderColor = Color.Transparent;
            yesButton.BorderColor = Color.Transparent;
        }
    }
}