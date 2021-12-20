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
    [Activity(Label = "ZurhayYears", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ZurhayYearsActivity : Activity
    {
        WebView ZurhayYearsWebView;
        public DateTime thisDay;
        public DateTime RealTimeToDay;
        public TextView bt_ChoiseDate;
        const int DATE_DIALOG_ID = 0;
   
         Typeface btfonts = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Luminari.ttf");
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ZurhayYears);

            var centralname = FindViewById<TextView>(Resource.Id.textView_CentralDataZurhayYears);
            centralname.Typeface = btfonts;

            Button bt_exit_from_layout_ZurhayYears = FindViewById<Button>(Resource.Id.bt_exit_from_layout_ZurhayYears);
            bt_exit_from_layout_ZurhayYears.Typeface = btfonts;
            bt_exit_from_layout_ZurhayYears.Click += delegate
            {
                Finish();
            };

            thisDay = DateTime.Now; //получаем текущую дату
            bt_ChoiseDate = FindViewById<Button>(Resource.Id.textView2ZurhayYears);
            bt_ChoiseDate.Click += delegate
            {
                ShowDialog(DATE_DIALOG_ID);

            };
            ZurhayYearsWebView = FindViewById<WebView>(Resource.Id.webView_central_DateNameZurhayYears);
            ZurhayYearsWebView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/index.html");
            retrieveset(); //прогружаем сохраненные данные раньше

            if (bt_ChoiseDate.Text == "01 января 1900")
            {
                //saveset();
              


                var alertmessage = new AlertDialog.Builder(this);
                alertmessage.SetMessage("Для правильного отображения гороскопа выберите дату своего рождения!");
                alertmessage.SetPositiveButton("OK", delegate {
                    ShowDialog(DATE_DIALOG_ID);

                   // Finish(); 
                });       
                alertmessage.Create().Show();//создаём и показываем.

            }
            
            // Create your application here
        }
        protected void saveset()
        {//сохраним наши переменные для будущего открытия
            var pref = Application.Context.GetSharedPreferences("Zur_Pref", FileCreationMode.Private);
            var prefEditor = pref.Edit();
            prefEditor.PutString("DateBirdth", bt_ChoiseDate.Text);
            prefEditor.PutInt("TimeYear", thisDay.Year);
            prefEditor.PutInt("TimeMonth", thisDay.Month);
            prefEditor.PutInt("TimeDate", thisDay.Day);
            prefEditor.Commit();
        }
        protected void retrieveset()
        {//загружаем переменные при загрузки страницы
            var pref = Application.Context.GetSharedPreferences("Zur_Pref", FileCreationMode.Private);
            if (pref.Contains("DateBirdth"))
                {
            var tpref = pref.GetString("DateBirdth", null);
            var tYear = pref.GetInt("TimeYear", 0);
            var tMonth = pref.GetInt("TimeMonth", 0);
            var tDay = pref.GetInt("TimeDate", 0);

                thisDay = new DateTime(tYear, tMonth, tDay);
            bt_ChoiseDate.Text = tpref;
            RunOnUiThread(() => Toast.MakeText(this, "Ваш день рождения: " + tpref, ToastLength.Long).Show());
                UpdateZurhayInfo();
           
                }

    

        }
        private void UpdateDisplayDate()
        {//обновляем дату 
            bt_ChoiseDate.Text = thisDay.Day + " " + thisDay.ToString("MMMM")+ " " + thisDay.Year;
            saveset();
        }
        protected override Dialog OnCreateDialog(int id)
        { //создаем диалог для получения даты из календаря
            switch (id)
            {
                case DATE_DIALOG_ID:
                    return new DatePickerDialog(this, OnDateSet, thisDay.Year, thisDay.Month - 1, thisDay.Day);
            }
            return null;
        }
        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        { //получаем дату, и обновляем все переменные
            this.thisDay = e.Date;
            UpdateDisplayDate();
            UpdateZurhayInfo();

        }
        private void UpdateZurhayInfo()
        {
            Console.WriteLine("смотрим дату перед рисованием: " + thisDay.Date);
            int tempYear = thisDay.Year;
            int prevYear = tempYear -1;
            switch(tempYear)
            { 
                case 1932:
                {
                        if (thisDay.Month < 2)
                        {//не знаю пока куда
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/stolko.html");
                        }
                        else
                        {
                           
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/1932.html");
                            if(thisDay.Month < 2 & thisDay.Day <6)
                                ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/stolko.html");
                        }
                            break;
                           
                        
                }
                case 1933:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if(thisDay.Day < 25)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                        
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");

                        }
                        else
                        {
                          
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");

                        }
                        break;
                    }
                case 1934:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 14)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1935:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 4)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1936:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 24)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1937:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 11)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1938:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 1)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1939:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 19)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1940:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 8)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1941:
                    {
                        if (thisDay.Month == 1)
                        {
                            if (thisDay.Day < 27)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1942:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 15)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1943:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 5)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1944:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 25)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1945:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 13)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1946:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 3)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1947:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 21)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1948:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 10)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1949:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 29)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1950:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 17)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1951:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 6)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1952:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 27)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1953:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 15)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1954:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 4)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1955:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 23)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1956:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 12)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1957:
                    {
                        if (thisDay.Month == 1)
                        {
                            if (thisDay.Day < 31)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1958:
                    {
                        if (thisDay.Month <= 19)
                        {
                            if (thisDay.Day < 19)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1959:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 8)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1960:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 27)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1961:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 16)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1962:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 5)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1963:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 25)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1964:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 14)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1965:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 2)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1966:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 21)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1967:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 10)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1968:
                    {
                        if (thisDay.Month == 1)
                        {
                            if (thisDay.Day < 30)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1969:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 17)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1970:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 7)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1971:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 26)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1972:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 15)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1973:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 4)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1974:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 23) 
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1975:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 12)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1976:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 1)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1977:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 19)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1978:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 8)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1979:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 27)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1980:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 17)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1981:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 5)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1982:
                    {
                       
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 24)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1983:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 13)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1984:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 2)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1985:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 20)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1986:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 9)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1987:
                    {
                        if (thisDay.Month == 1)
                        {
                            if (thisDay.Day < 30)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1988:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 18)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1989:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 18)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1990:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 26)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1991:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 15)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1992:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 4)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1993:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 22)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1994:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 11)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1995:
                    {
                        if (thisDay.Month ==1 )
                        {
                            if (thisDay.Day < 31)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1996:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 19)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1997:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 8)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1998:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 27)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 1999:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 17)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
              
                case 2000:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 6)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2001:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 24)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2002:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 13)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2003:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 2)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2004:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 21)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2005:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 9)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2006:
                    {
                        if (thisDay.Month == 1)
                        {
                            if (thisDay.Day < 30)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2007:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 18)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2008:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 8)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2009:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 25)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2010:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 14)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2011:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 3)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2012:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 22)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2013:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 11)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2014:
                    {
                        if (thisDay.Month ==1)
                        {
                            if (thisDay.Day < 31)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2015:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 20)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                case 2016:
                    {
                        if (thisDay.Month <= 2)
                        {
                            if (thisDay.Day < 9)
                            { ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + prevYear + ".html"); break; }
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        else
                        {
                            ZurhayYearsWebView.LoadUrl("file:///android_asset/www/ZurhayYears/" + tempYear + ".html");
                        }
                        break;
                    }
                default: break;
            }
        }
    }
}