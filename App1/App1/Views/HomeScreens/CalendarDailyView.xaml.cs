using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarDailyView : ContentPage
    {
        public CalendarDailyView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            try
            {
                ViewModel.CalendarDailyViewModel calendarViewModel = new ViewModel.CalendarDailyViewModel(Navigation);
                calendarViewModel.getDailyLog();
                BindingContext = calendarViewModel;
                
            } catch(Exception e)
            {
                Console.WriteLine("we fucked up");
            }
            
        }
    }
}