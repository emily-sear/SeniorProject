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
    public partial class PrintLogsView : ContentPage
    {
        public PrintLogsView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            BindingContext = new ViewModel.PrintLogsViewModel(Navigation);
            MessagingCenter.Subscribe<ViewModel.PrintLogsViewModel>(this, "PermissionsDenied", (sender) =>
            {
                DisplayAlert("Error", "In order to print daily logs to a PDF, you must accept all permissions requested. Please visit your settings and accept these permissions and try again.", "Ok");
            });

            MessagingCenter.Subscribe<ViewModel.PrintLogsViewModel>(this, "LogPrinted", (sender) =>
            {
                DisplayAlert("Success", "Your pdf of logs was successfully printed, please visit the files application of your device to view your logs.", "Ok");
            });

            MessagingCenter.Subscribe<ViewModel.PrintLogsViewModel>(this, "NoLogs", (sender) =>
            {
                DisplayAlert("No Logs Found", "No logs were found with the dates selected. Please change the dates selected.", "Ok");
            });
        }
    }
}