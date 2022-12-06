using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: ExportFont("verdana.tff", Alias = "Verdana")]

namespace App1.ViewModel
{
    class PrintLogsViewModel : ViewModel.BaseViewModel
    {
        private DateTime startDate;
        private DateTime endDate;
        private List<DailyLog> dailyLogs;
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
            RestService restService = new RestService();
            this.dailyLogs = await restService.GetDailyLogDataAsync("{\"$and\":[{\"Datetime\": { \"$date\": {\"$gt\":\"" + makeRestDate(startDate, -1) + "\", \"$lt\": \"" + makeRestDate(this.endDate, 1) + "\"}}},{\"Email\":\"" + User.email + "\"}]}");
           
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if(status != PermissionStatus.Granted)
            {
                var statusRequest = await Permissions.RequestAsync<Permissions.StorageWrite>();
                status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            }

            if(status == PermissionStatus.Granted)
            {
                if(this.dailyLogs.Count > 0)
                {
                    PdfDocument pdf = new PdfDocument();
                    PdfPage page = pdf.Pages.Add();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XRect rect = this.DrawPDFTitleAndDate(gfx, page);
                    this.DrawPDFBody(gfx, page, rect);
                    foreach (DailyLog dailyLog in this.dailyLogs)
                    {
                        PdfPage dailyLogPage = pdf.Pages.Add();
                        XGraphics dailyLogGfx = XGraphics.FromPdfPage(dailyLogPage);
                        this.DrawPDFDailyLogPage(dailyLogGfx, dailyLogPage, dailyLog);
                    }
                    IFileService service = DependencyService.Get<IFileService>();
                    string pdfName = "";
                    if (!startDate.Equals(endDate))
                    {
                        pdfName = "DailyLogs" + this.makePrettyDate(startDate) + "-" + this.makePrettyDate(endDate) + ".pdf";
                    }
                    else
                    {
                        pdfName = "DailyLogs" + this.makePrettyDate(startDate) + ".pdf";
                    }
                    service.Save(pdf, pdfName);
                    MessagingCenter.Send<PrintLogsViewModel>(this, "LogPrinted");
                } 
                else
                {
                    MessagingCenter.Send<PrintLogsViewModel>(this, "NoLogs");
                }

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
        private void DrawPDFDailyLogPage(XGraphics gfx, PdfPage page, DailyLog dailyLog)
        {
           XRect rect = this.DrawPDFTitleAndDate(gfx, page);
           XFont font = new XFont("Verdana", 20);
            XFont fontBold = new XFont("Verdana", 20, XFontStyle.Bold);
            rect.Y += 18;
            gfx.DrawString("Date: " , fontBold, XBrushes.Black, rect);
            rect.X += 60;
            gfx.DrawString(makePrettyDate(dailyLog.CurrentDate), font, XBrushes.Black, rect);
            rect.X -= 60;
            rect.Y += 50;
            gfx.DrawString("Overall Scale: ", fontBold, XBrushes.Black, rect);
            rect.X += 150;
            gfx.DrawString(dailyLog.OverallScale.ToString(), font, XBrushes.Black, rect);
            rect.X -= 150;
            rect.Y += 30;
            gfx.DrawString("Pain Scale: ", fontBold, XBrushes.Black, rect);
            rect.X += 150;
            gfx.DrawString(dailyLog.PainScale.ToString(), font, XBrushes.Black, rect);
            rect.X -= 150;
            rect.Y += 30;
            gfx.DrawString("Mood Scale: ", fontBold, XBrushes.Black, rect);
            rect.X += 150;
            gfx.DrawString(dailyLog.MoodScale.ToString(), font, XBrushes.Black, rect);
            rect.X -= 150;
            rect.Y += 30;
            gfx.DrawString("Fatigue Scale: ", fontBold, XBrushes.Black, rect);
            rect.X += 150;
            gfx.DrawString(dailyLog.FatigueScale.ToString(), font, XBrushes.Black, rect);
            rect.X -= 150;
            rect.Y += 50;

            if(dailyLog.OnPeriod)
            {
                gfx.DrawString("On period", font, XBrushes.Black, rect);
                rect.Y += 50;
            }

            if(dailyLog.Symptoms.Count > 0)
            {
                gfx.DrawString("Symptoms", fontBold, XBrushes.Black, rect);
                rect.Y += 30;
                foreach(SymptomDailyLog sym in dailyLog.Symptoms)
                {
                    if(!String.IsNullOrEmpty(sym.NameOfSymptom) && !String.IsNullOrEmpty(sym.Timeframe))
                    {
                        gfx.DrawString(sym.NameOfSymptom + " (" + sym.Timeframe + ")", font, XBrushes.Black, rect);

                    } else if(!String.IsNullOrEmpty(sym.NameOfSymptom))
                    {
                        gfx.DrawString(sym.NameOfSymptom, font, XBrushes.Black, rect);
                    } else
                    {
                        gfx.DrawString(sym.Timeframe, font, XBrushes.Black, rect);
                    }
                    rect.Y += 30;
                    if (sym.Severeness > 0 && !String.IsNullOrEmpty(sym.AreaOfBody))
                    {
                        gfx.DrawString("Severness: " + sym.Severeness + "- " + sym.AreaOfBody, font, XBrushes.Black, rect);
                        rect.Y += 30;
                    }
                    else if (sym.Severeness > 0)
                    {
                        gfx.DrawString("Severness: " + sym.Severeness, font, XBrushes.Black, rect);
                        rect.Y += 30;
                    }
                    else
                    {
                        gfx.DrawString("Area of Body: " + sym.AreaOfBody, font, XBrushes.Black, rect);
                        rect.Y += 30;
                    }
                    if(!String.IsNullOrEmpty(sym.Notes))
                    {
                        gfx.DrawString(sym.Notes, font, XBrushes.Black, rect);
                        rect.Y += 30;
                    }
                    rect.Y += 20;
                }
            }

            if(dailyLog.Medications.Count > 0)
            {
                
                gfx.DrawString("Medications", fontBold, XBrushes.Black, rect);
                rect.Y += 30;

                foreach(MedicationDailyLog med in dailyLog.Medications)
                {
                    if(!String.IsNullOrEmpty(med.MedicationName) && !String.IsNullOrEmpty(med.TimeTaken))
                    {
                        gfx.DrawString(med.MedicationName + " (" + med.TimeTaken + ")", font, XBrushes.Black, rect);
                        rect.Y += 30;
                    } 
                    else if(!String.IsNullOrEmpty(med.MedicationName))
                    {
                        gfx.DrawString(med.MedicationName, font, XBrushes.Black, rect);
                        rect.Y += 30;
                    }
                    else
                    {
                        gfx.DrawString(med.TimeTaken, font, XBrushes.Black, rect);
                        rect.Y += 30;
                    }

                    if(!String.IsNullOrEmpty(med.Dosage))
                    {
                        gfx.DrawString(med.Dosage, font, XBrushes.Black, rect);
                        rect.Y += 30;
                        if(med.DailyMed)
                        {
                            gfx.DrawString("Daily medication", font, XBrushes.Black, rect);
                            rect.Y += 30;
                        }
                       
                    }

                    if(!String.IsNullOrEmpty(med.Notes))
                    {
                        gfx.DrawString(med.Notes, font, XBrushes.Black, rect);
                        rect.Y += 30;
                    }
                    rect.Y += 20;
                }
            }

        }
        private void DrawPDFBody(XGraphics gfx, PdfPage page, XRect rect)
        {
            XFont font = new XFont("Verdana", 20);
            XStringFormat format = new XStringFormat();

            format.Alignment = XStringAlignment.Center;
            gfx.DrawString("The below scales are graded with ", font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("0 being the best and 10 being the worst.", font, XBrushes.Black, rect, format);
            rect.Y += 50;
            format.Alignment = XStringAlignment.Near;
            Double avgOverall = 0;
            Double highOverall = 0;
            DateTime highOverallDate = DateTime.Today;
            Double lowOverall = 0;
            DateTime lowOverallDate = DateTime.Today;

            Double avgPain = 0;
            Double highPain = 0;
            DateTime highPainDate = DateTime.Today;
            Double lowPain = 0;
            DateTime lowPainDate = DateTime.Today;

            Double avgMood = 0;
            Double highMood = 0;
            DateTime highMoodDate = DateTime.Today;
            Double lowMood = 0;
            DateTime lowMoodDate = DateTime.Today;

            Double avgFatigue = 0;
            Double highFatigue = 0;
            DateTime highFatigueDate = DateTime.Today;
            Double lowFatigue = 0;
            DateTime lowFatigueDate = DateTime.Today;
            int count = 0;

            foreach(DailyLog log in this.dailyLogs)
            {
                avgOverall += log.OverallScale;
                avgPain += log.PainScale;
                avgMood += log.MoodScale;
                avgFatigue += log.FatigueScale;

                if(log.OverallScale < lowOverall || count == 0)
                {
                    lowOverall = log.OverallScale;
                    lowOverallDate = log.CurrentDate;
                }
                if(log.OverallScale > highOverall)
                {
                    highOverall = log.OverallScale;
                    highOverallDate = log.CurrentDate;
                }

                if(log.PainScale < lowPain || count == 0)
                {
                    lowPain = log.PainScale;
                    lowPainDate = log.CurrentDate;
                    
                }
                if(log.PainScale > highPain)
                {
                    highPain = log.PainScale;
                    highPainDate = log.CurrentDate;
                }

                if(log.MoodScale < lowMood || count == 0)
                {
                    lowMood = log.MoodScale;
                    lowMoodDate = log.CurrentDate;
                }
                if(log.MoodScale > highMood)
                {
                    highMood = log.MoodScale;
                    highMoodDate = log.CurrentDate;
                }

                if(log.FatigueScale < lowFatigue || count == 0)
                {
                    lowFatigue = log.FatigueScale;
                    lowFatigueDate = log.CurrentDate;
                }
                if(log.FatigueScale > highFatigue)
                {
                    highFatigue = log.FatigueScale;
                    highFatigueDate = log.CurrentDate;
                }
                count++;
            }
            avgOverall = avgOverall / count;
            avgPain = avgPain / count;
            avgMood = avgMood / count;
            avgFatigue = avgFatigue / count;

            gfx.DrawString("Average Overall Scale: " + avgOverall, font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("Low Overall Scale: " + lowOverall + " (" + makePrettyDate(lowOverallDate) + ") ", font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("High Overall Scale: " + highOverall + " (" + makePrettyDate(highOverallDate) + ") ", font, XBrushes.Black, rect, format);
            rect.Y += 50;

            gfx.DrawString("Average Pain Scale: " + avgPain, font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("Low Pain Scale: " + lowPain + " (" + makePrettyDate(lowPainDate) + ") ", font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("High Pain Scale: " + highPain + " (" + makePrettyDate(highPainDate) + ") ", font, XBrushes.Black, rect, format);
            rect.Y += 50;

            gfx.DrawString("Average Mood Scale: " + avgMood , font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("Low Mood Scale: " + lowMood + " (" + makePrettyDate(lowMoodDate) + ") ", font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("High Mood Scale: " + highMood + " (" + makePrettyDate(highMoodDate) + ") ", font, XBrushes.Black, rect, format);
            rect.Y += 50;

            gfx.DrawString("Average Fatigue Scale: " + avgFatigue, font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("Low Fatigue Scale: " + lowFatigue + " (" + makePrettyDate(lowFatigueDate) + ") ", font, XBrushes.Black, rect, format);
            rect.Y += 30;
            gfx.DrawString("High Fatigue Scale: " + highFatigue + " (" + makePrettyDate(highFatigueDate) + ") ", font, XBrushes.Black, rect, format);
            rect.Y += 50;

        }

        private XRect DrawPDFTitleAndDate(XGraphics gfx, PdfPage page) 
        {
            XFont font = new XFont("Verdana", 40);
            XStringFormat format = new XStringFormat();
            XRect rect = new XRect(5, 0, page.Width - 5, 0);
            format.Alignment = XStringAlignment.Center;
            gfx.DrawString("Chronically Tracking: Daily Logs", font, XBrushes.Black, rect, format);
            rect.Y += 50;
            font = new XFont("Verdana", 20);
            gfx.DrawString(User.firstName + " " + User.lastName, font, XBrushes.Black, rect, format);
            rect.Y += 30;
            if (!startDate.Equals(endDate))
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

            rect.Y += 8;
            return rect;

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
