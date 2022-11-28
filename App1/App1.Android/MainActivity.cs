using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android;
using System;
using Android.Content.PM;
using Android.Views;
using Android.Support.V4.App;
using Google.Android.Material.Snackbar;
using PdfSharp.Xamarin.Forms;
using Android.Runtime;
using static Android.Resource;

namespace App1.Droid
{
    [Activity(Label = "App1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity MainActivityInstance { get; private set; }
        readonly string[] PermissionsWrite =
        {
           Manifest.Permission.WriteExternalStorage
        };

        const int RequestWriteId = 0;
        View layout;
        TextView textWrite;
        Button buttonGetWrite;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            PdfSharp.Xamarin.Forms.Droid.Platform.Init();
            XamForms.Controls.Droid.Calendar.Init();
            MainActivityInstance = this;
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {

            Console.WriteLine("made it");
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}