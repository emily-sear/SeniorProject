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
    public partial class ReviewPageView : ContentPage
    {
        public ReviewPageView(DailyLog dailyLog)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            if(dailyLog == null)
            {
                dailyLog = new DailyLog();
            }
            BindingContext = new ViewModel.ReviewPageViewModel(Navigation, dailyLog);
        }
    }
}