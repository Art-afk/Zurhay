using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



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
    [Activity(Label = "DatsansActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DatsansActivity : Activity
    {
        private Button bt_exit_from_layout_Datsans;
        WebView DatsansView;
        Typeface btfonts = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Luminari.ttf");

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Datsans);
            // Create your application here
            //load html datsans
            var centralname = FindViewById<TextView>(Resource.Id.textView_CentralDataDatsans);
            centralname.Typeface = btfonts;
            bt_exit_from_layout_Datsans = FindViewById<Button>(Resource.Id.bt_exit_from_layout_Datsans);
            bt_exit_from_layout_Datsans.Typeface = btfonts;
            bt_exit_from_layout_Datsans.Click += delegate
            {
                Finish();
            };

            DatsansView = FindViewById<WebView>(Resource.Id.webview_DateNameDatsans);
            DatsansView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            DatsansView.LoadUrl("file:///android_asset/www/Datsans/datsans.html");

            //string emoji = "ཨོཾ་མུ་ནི་མུ་ནི་མ་ཧཱ་མུ་ནི་སྭཱ་ཧཱ༎";

            

           
        }

      
    }
}