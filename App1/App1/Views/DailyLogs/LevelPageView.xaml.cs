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
    public partial class LevelPageView : ContentPage
    {
        public LevelPageView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            BindingContext = new ViewModel.LevelPageViewModel(Navigation, this);
        }

        public void yesButton_Clicked(object sender, EventArgs e)
        {
            yesButton.BorderColor = Color.Black;
            noButton.BorderColor = Color.Transparent;
        }

        public void noButton_Clicked(object sender, EventArgs e)
        {
            noButton.BorderColor = Color.Black;
            yesButton.BorderColor = Color.Transparent;
        }

    }
}