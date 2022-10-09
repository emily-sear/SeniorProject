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
    public partial class SignUpMedicationsPage : ContentPage
    {
        public SignUpMedicationsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new ViewModel.SignUpMedicationsViewModel(Navigation);
            InitializeComponent();
        }
    }
}