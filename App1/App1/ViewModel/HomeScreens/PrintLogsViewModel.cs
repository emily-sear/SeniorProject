using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
using PdfSharpCore.Drawing.Layout;

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
            RestService restService = new RestService();
            await restService.GetDailyLogDataAsync("{\"Datetime\": { \"$date\": {\"$gt\":\"" + makeRestDate(startDate, -1) + "\", \"$lt\": \"" + makeRestDate(this.endDate, 1) + "\"}}}");

            
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if(status != PermissionStatus.Granted)
            {
                var statusRequest = await Permissions.RequestAsync<Permissions.StorageWrite>();
                status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            }

            if(status == PermissionStatus.Granted)
            {
                PdfDocument pdf = new PdfDocument();
                PdfPage page = pdf.Pages.Add();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                this.DrawPDFTitleAndDate(gfx, page);
                IFileService service = DependencyService.Get<IFileService>();
                string pdfName = "";
                if(!startDate.Equals(endDate))
                {
                   pdfName = "DailyLogs" + this.makePrettyDate(startDate) + "-" + this.makePrettyDate(endDate) + ".pdf";
                }
                else
                {
                    pdfName = "DailyLogs" + this.makePrettyDate(startDate) + ".pdf";
                }
                service.Save(pdf, pdfName);
            }
            else
            {
                MessagingCenter.Send<PrintLogsViewModel>(this, "PermissionsDenied");
            }


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
            await Navigation.PushAsync(new LevelPageView(new DailyLog()));
        }
        private async void OnSettings()
        { 
            await Navigation.PushAsync(new SettingsView());
        }

        private void DrawPDFTitleAndDate(XGraphics gfx, PdfPage page)
        {
            XFont font = new XFont("Verdana", 40);
            XStringFormat format = new XStringFormat();
            XRect rect = new XRect(0, 0, page.Width, 0);
            format.Alignment = XStringAlignment.Center;
            gfx.DrawString("Chronically Tracking", font, XBrushes.Black, rect, format);
            rect.Y += 50;
            font = new XFont("Verdana", 20);
            if(!startDate.Equals(endDate))
            {
                gfx.DrawString(this.makePrettyDate(startDate) + " - " + this.makePrettyDate(endDate), font, XBrushes.Black, rect, format);
            }
            else
            {
                gfx.DrawString(this.makePrettyDate(startDate), font, XBrushes.Black, rect, format);
            }

            rect.Y += 30;
            XPen pen = new XPen(XColors.Black, 3);
            gfx.DrawLine(pen, 0, rect.Y, page.Width, rect.Y);

        }
       
        private String makeRestDate(DateTime oldDate, double addSubtract)
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

        private String makePrettyDate(DateTime date)
        {
            String dateString = "";
            if (date.Month < 10)
            {
                dateString += "0" + date.Month + "-";
            }
            else
            {
                dateString += date.Month + "-";
            }

            if (date.Day < 10)
            {
                dateString += "0" + date.Day + "-";
            }
            else
            {
                dateString += date.Day + "-";
            }
            dateString += date.Year;

            return dateString;
        }
    }
}
