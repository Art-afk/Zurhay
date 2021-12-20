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
    [Activity(Label = "Disclamer", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DisclamerActivity : Activity
    {
        Typeface btfonts = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Luminari.ttf");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FAQ);
            // Create your application here
            var centralname = FindViewById<TextView>(Resource.Id.textView_CentralDataFAQ);
            centralname.Text = "О приложение";
            centralname.Typeface = btfonts;

            Button bt_exit_from_layout_FAQ = FindViewById<Button>(Resource.Id.bt_exit_from_layout_FAQ);
            bt_exit_from_layout_FAQ.Typeface = btfonts;
            bt_exit_from_layout_FAQ.Click += delegate
            {
                Finish();
            };

            WebView FAQWebView = FindViewById<WebView>(Resource.Id.webview_FAQ_central);
            FAQWebView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            FAQWebView.LoadUrl("file:///android_asset/www/Disclamer/index.html");
            // Create your application here
        }
    }
}