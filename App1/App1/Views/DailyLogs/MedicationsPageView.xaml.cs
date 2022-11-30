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
        public MedicationsPageView(DailyLog dailyLog)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            if(dailyLog == null)
            {
                dailyLog = new DailyLog();
            }
            BindingContext = new ViewModel.MedicationsPageViewModel(Navigation, dailyLog, this);
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