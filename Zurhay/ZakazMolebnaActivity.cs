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

namespace Zurhay
{
    [Activity(Label = "ZakazMolebnaActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ZakazMolebnaActivity : Activity
    {
        private Button bt_ExittoMainfromZakazMolebna;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ZakazMolebna);
            // Create your application here

            bt_ExittoMainfromZakazMolebna = FindViewById<Button>(Resource.Id.bt_ExittoMainfromZakazMolebna);
            bt_ExittoMainfromZakazMolebna.Click += bt_ExittoMainfromZakazMolebna_Click;
        }

        private void bt_ExittoMainfromZakazMolebna_Click(object sender, EventArgs e)
        {
            Finish();


        }
    }
}