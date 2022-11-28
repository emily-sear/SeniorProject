using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PdfSharpCore.Pdf;
using Android;

[assembly: Xamarin.Forms.Dependency(typeof(App1.Droid.FileService))]
namespace App1.Droid
{
    class FileService : IFileService
    {
        public void Save(PdfDocument pdf, string name, string location = "temp")
        {
            string root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            string filePath = "";
            if(!Directory.Exists(root + "/DailyLogs"))
            {
                Java.IO.File dailyLogsDir = new Java.IO.File(root + "/DailyLogs");
                dailyLogsDir.Mkdir();
                filePath = Path.Combine(dailyLogsDir.Path, name);
            }
            else
            {
                string temp = root + "/DailyLogs";
                filePath = Path.Combine(temp, name);
            }


             
            pdf.Save(filePath);
            
        }
    }
}