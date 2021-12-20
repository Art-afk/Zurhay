using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Text.Style;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Graphics;


namespace Zurhay
{
    [Activity(Label = "MantrasActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MantrasActivity : Activity
    {
        // WebView web_view; 
        Typeface btfonts = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Luminari.ttf");

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //    var webView = FindViewById<WebView>(Resource.Id.webView);

            SetContentView(Resource.Layout.Mantras);
            // Create your application here

            var centralname = FindViewById<TextView>(Resource.Id.textView_CentralDataMantras);
            centralname.Typeface = btfonts;
            var webView = FindViewById<WebView>(Resource.Id.textView_DateNameMantras);

           Button bt_exit_from_layout_Mantras = FindViewById<Button>(Resource.Id.bt_exit_from_layout_Mantras);
            bt_exit_from_layout_Mantras.Typeface = btfonts;
            bt_exit_from_layout_Mantras.Click += delegate
            {
                Finish();
            };

            webView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            //webView.LoadDataWithBaseURL("file:///android_asset/", mantramsg, "text/html", "UTF-8", null);
            webView.LoadUrl("file:///android_asset/www/Mantras/mantras.html");
         //   Console.WriteLine(Convert.ToChar("7"));
                //Encoding.ASCII.GetString();

        
        }

     
    }
}