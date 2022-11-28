using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Internal;
using PdfSharp.SharpZipLib;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;

[assembly: ExportFont("verdana.tff", Alias="Verdana")]

namespace App1.ViewModel
{
    class PrintLogsViewModel : ViewModel.BaseViewModel
    {
        private DateTime startDate;
        private DateTime endDate;
        public Command HomeCommand { get; }
        public Command NewLogCommand { get; }
        public Command DailyLogsCommand { get; }
        public Command SettingsCommand { get; }
        public Command PrintButtonCommand { get; }
        public INavigation Navigation { get; }

        public Boolean OverviewCheckChanged { get; set; }
        public Boolean DailyOverviewCheckChanged { get; set; }
        public Boolean DailyIndepthCheckChanged { get; set; }
        public DateTime StartDate
        { 
            get => startDate; 
            set
            {
                startDate = value;
                OnPropertyChanged(propertyName: "MinEndDate");
            }
        }
        public DateTime EndDate
        { 
            get => endDate; 
            set
            {
                endDate = value;
                OnPropertyChanged(propertyName: "MaxStartDate");
            }
        }
        
        public DateTime MaxStartDate
        {
            get 
            {
                if(DateTime.Compare(this.endDate, DateTime.Today) < 0)
                {
                    return this.endDate;
                } else
                {
                    return DateTime.Today;
                }
            }
        }

        public DateTime MaxEndDate
        {
            get
            {
                return DateTime.Today;
            }
        }

        public DateTime MinStartDate
        {
            get
            {
                return DateTime.Today.AddYears(-2);
            }
        }

        public DateTime MinEndDate
        {
            get
            {
                if(DateTime.Compare(this.startDate, DateTime.Today.AddYears(-2)) > 0 && DateTime.Compare(startDate, DateTime.Today) != 0)
                {
                    return this.startDate;
                } 
                else
                {
                    return DateTime.Today.AddYears(-2);
                }
            }
        }
        public PrintLogsViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            SettingsCommand = new Command(OnSettings);
            HomeCommand = new Command(OnHome);
            DailyLogsCommand = new Command(OnDailyLogs);
            NewLogCommand = new Command(OnNewLogs);
            PrintButtonCommand = new Command(OnPrint);
            this.endDate = DateTime.Today;
            this.startDate = DateTime.Today;
        }

        private async void OnPrint()
        {
            Console.WriteLine(DateTime.Today);
            Console.WriteLine(this.endDate);
            Console.WriteLine(this.startDate);
            /*            RestService restService = new RestService();
                        await restService.GetDailyLogDataAsync("{\"Datetime\": { \"$date\": {\"$gt\":\"" + makePrettyDate(startDate, -1) + "\", \"$lt\": \"" + makePrettyDate(this.endDate, 1) + "\"}}}");*/
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if(status != PermissionStatus.Granted)
            {
                var statusRequest = await Permissions.RequestAsync<Permissions.StorageWrite>();
                
             
            }
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if(status != PermissionStatus.Granted)
            {
               var statusRequestRead = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            PdfDocument MyPDF = new PdfDocument();
            PdfPage page = MyPDF.Pages.Add();
            XGraphics Mygraphics = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 100);
            Mygraphics.DrawString("Hello, World!", font, XBrushes.Black,
                new XRect(0, 0, page.Width, 0));
            IFileService service = DependencyService.Get<IFileService>();
            service.Save(MyPDF, "HelloWorld.pdf");

        }
        private async void OnDailyLogs()
        {
            await Navigation.PushAsync(new CalendarDailyView());
        }
        private async void OnHome()
        {
            await Navigation.PushAsync(new HomescreenView());
        }

        private async void OnNewLogs()
        {
            await Navigation.PushAsync(new LevelPageView());
        }
        private async void OnSettings()
        { 
            await Navigation.PushAsync(new SettingsView());
        }
       
        private String makePrettyDate(DateTime oldDate, double addSubtract)
        {
            DateTime date = oldDate.AddDays(addSubtract);
            String dateString = date.Year + "-";
            if(date.Month < 10)
            {
                dateString += "0" + date.Month + "-";
            } else
            {
                dateString += date.Month + "-";
            }

            if(date.Day < 10)
            {
                dateString += "0" + date.Day;
            }
            else
            {
                dateString += date.Day;
            }
            return dateString;
        }
    }
}
