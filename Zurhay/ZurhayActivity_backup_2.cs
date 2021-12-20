using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Threading.Tasks;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Zurhay
{
    [Activity(Label = "ZurhayActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ZurhayActivity : Activity
    {
        private TextView bt_ExittoMainFromZurhay;
        private TextView bt_Zurhayrightdate;
        private TextView bt_Zurhayleftdate;
        private TextView CentralDataText;
        private Button bt_exit_from_layout_Zurhay;
        public DateTime thisDay;
        public DateTime RealTimeToDay;

        //    public int thisMonth = 0; 
        public int TempMouths = 0;
        public string XmlMouths = string.Empty;
        //  public string MonthName = string.Empty;
        public int DaysInMonth = 0;
        //    public int thisDayDate = 0;
        public int TempDayDate = 0;
        //  public int thisYear =0;
        public int TempYear = 0;
        public int WhatDateToday = 0;
        public string DateTodayWhatDayToday = string.Empty;
        public string DateTodayGoodDay = string.Empty;
        public string DateTodayBadDoDay = string.Empty;
        public string DateTodayWhatNeedDo = string.Empty;
        public TextView DatePikerText;
        public DateTime BirdthDay;
        const int DATE_DIALOG_ID = 0;
        public string documentsPathtoDownload = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public string localDownloadFilename = "ZurhayCALDAYNEW.xml";
        int needchangexml = 0;

        Typeface btfonts = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Luminari.ttf");

        ProgressBar _progressBar;



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Zurhay);

            _progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            _progressBar.Visibility = ViewStates.Gone;

            TextView DownloadZurhayNewXML = FindViewById<TextView>(Resource.Id.ZurhaytextView11);
            //  DownloadZurhayNewXML.Click += DownloadZurhayNewXML_Click;            



            // Create your application here
            // bt_ExittoMainFromZurhay = FindViewById<Button>(Resource.Id.bt_ExittoMainFromZurhay);
            bt_ExittoMainFromZurhay = FindViewById<TextView>(Resource.Id.bt_ExittoZurhayYarsfromZurhay);
            bt_ExittoMainFromZurhay.SetTypeface(btfonts, TypefaceStyle.Bold);
            bt_ExittoMainFromZurhay.Click += bt_ExittoZurhayYarsfromZurhay_Click;
            bt_Zurhayrightdate = FindViewById<TextView>(Resource.Id.bt_Zurhayrightdate); //тыкаем вправа на 1 день
            bt_Zurhayrightdate.Click += Bt_Zurhayrightdate_Click;
            bt_Zurhayleftdate = FindViewById<TextView>(Resource.Id.bt_Zurhayleftdate);
            bt_Zurhayleftdate.Click += Bt_Zurhayleftdate_Click;

            bt_exit_from_layout_Zurhay = FindViewById<Button>(Resource.Id.bt_exit_from_layout_Zurhay);
            bt_exit_from_layout_Zurhay.Typeface = btfonts;
            bt_exit_from_layout_Zurhay.Click += delegate
            {
                Finish();
            };



            thisDay = DateTime.Now; //получаем текущую дату
            DateTime RealTimeToDay = DateTime.Today; //перменная с сегодняшним числом
                                                     //System.DateTime thismounth = thismounth;
                                                     //thisDay.Month
                                                     //---------Data определяем дату и всякую хрень-------------------------------------------------------------
                                                     // thisMonth = Int32.Parse(thisDay.ToString("MM")); //месяц в цифрах
                                                     //  MonthName = thisDay.ToString("MMMM"); // название месяца
                                                     // thisDayDate = Int32.Parse(thisDay.ToString("dd"));//день
                                                     // thisYear = Int32.Parse(thisDay.ToString("yyyy"));//год
                                                     //  WhatDateToday = thisDayDate;
                                                     //DaysInMonth = DateTime.DaysInMonth(thisYear, thisMonth);

            //Int32.Parse(thisDayDate); //превращаем дату в число
            //  Int32.Parse(thisYear); //конвертим год в число
            // Display the date in the default (general) format.
            //   Console.WriteLine(thisDay.ToString());  //    5/3/2012 12:00:00 AM
            // Console.WriteLine();
            // Display the date in a variety of formats.
            //  Console.WriteLine(thisDay.ToString("d")); //    5/3/2012
            //  Console.WriteLine(thisDay.ToString("D")); //    Thursday, May 03, 2012
            //  Console.WriteLine(thisDay.ToString("g")); //    5/3/2012 12:00 AM

            //объявлем кнопку для вызова фрагмента диалога календаря
            DatePikerText = FindViewById<TextView>(Resource.Id.textViewZurhay_CentralData);
            DatePikerText.Typeface = btfonts;
            DatePikerText.Click += delegate
            {
                ShowDialog(DATE_DIALOG_ID);
            };

            //если обновления не качались ни когда(первый запуск - качаем)
            if (File.Exists(System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename)))
            {
            }
            else
            {
                needchangexml = 1;

                DownloadZurhayNewXML_Click(null, null);

            }
            UpdateDisplayDate();
            DoParsingMyXmlFile();

            //End onCreate-------------------------------------------------------------------------------------------------
        }//onCreate

        private void DownloadZurhayNewXML_Click(object sender, EventArgs e)
        {
            //var url = new Uri("http://mipomnim.pp.ua/uploads/download.php?XMLName=BJUrzgMtsI.xml"); // download file http://zurhay.ru/editor/uploads/download.php?XMLName=give&userid=3
            var url = new Uri("http://zurhay.ru/editor/uploads/download.php?XMLName=give&userid=3"); // download file 
            DonwloadZurhayXML(url);
        }

        async void StartDownloadHandler(object sender, System.EventArgs e)
        {
            //_progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            _progressBar.Visibility = ViewStates.Visible;
            _progressBar.Progress = 0;
            Progress<DownloadBytesProgress> progressReporter = new Progress<DownloadBytesProgress>();
            progressReporter.ProgressChanged += (s, args) => _progressBar.Progress = (int)(100 * args.PercentComplete);

            Task<int> downloadTask = DownloadHelper.CreateDownloadTask(DownloadHelper.ToDownload, progressReporter);



            int bytesDownloaded = await downloadTask;
            System.Diagnostics.Debug.WriteLine("Downloaded {0} bytes.", bytesDownloaded);
            _progressBar.Visibility = ViewStates.Gone;

        }


        //   public static DateTime GetCreationTime(String path)
        //  {
        //  }
        private void CheckZurhayNewXML()
        {
            //проверям устарели ли версия xml

        }
        public void DonwloadZurhayXML(Uri url)
        {
            //Скачиваем новую xml
            //var url = new Uri("http://сайт.ру/файл"); // download file
            var alertmessage = new AlertDialog.Builder(this);
            alertmessage.SetMessage("Прошло более двух недель, хотите скачать обнавленный календарь?(~400Кб)");
            alertmessage.SetPositiveButton("OK", delegate {
                try
                {
                    //качаем календарь
                    WebClient webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    webClient.DownloadStringAsync(url);
                    webClient.DownloadStringCompleted += (s, e) => {
                        var text = e.Result; // get the downloaded text
                        string localPathToDownload = System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename);
                        File.WriteAllText(localPathToDownload, text); // writes to local storage
                        Console.WriteLine(File.Exists(System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename)) ? "File exists." : "File does not exist.");
                        if (File.Exists(System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename)))
                        {
                            Toast.MakeText(this, "Календарь скачался", ToastLength.Short).Show();
                            UpdateDisplayDate();
                            DoParsingMyXmlFile();
                        }
                        else Toast.MakeText(this, "Сервер недоступен, попробуйте позже", ToastLength.Short).Show();

                    };
                    // Finish(); //пока оставлю финишь. надо сделать
                }
                catch (WebException webEx)
                {
                    Console.WriteLine(webEx.ToString());
                    if (webEx.Status == WebExceptionStatus.ConnectFailure)
                    {
                        Console.WriteLine("Are you behind a firewall?  If so, go through the proxy server.");
                    }
                    Toast.MakeText(this, "Сервер недоступен, попробуйте позже", ToastLength.Short).Show();
                }
            });


            alertmessage.SetNegativeButton("Нет", delegate // вместо  (s, e) => попробовать написать delegate типо:("OK", delegate { Finish(); });
            {
                needchangexml = 1;
                //тут надо что-то сделать на кнопку NO
                // Finish(); //нахерен всё. грохаем лэйр
            });
            alertmessage.Create().Show();//создаём и показываем.



        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        { //получаем дату, и обновляем все переменные
            this.thisDay = e.Date;
            UpdateDisplayDate();
            DoParsingMyXmlFile();

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
        private void UpdateDisplayDate()
        {//обновляем дату 
            CentralDataText = FindViewById<TextView>(Resource.Id.textViewZurhay_CentralData);
            CentralDataText.Text = thisDay.Day + " " + thisDay.ToString("MMMM");
        }
        private void Bt_Zurhayleftdate_Click(object sender, EventArgs e)
        {//на один день влево
            thisDay = thisDay.AddDays(-1);
            UpdateDisplayDate();
            DoParsingMyXmlFile();
        }

        private void Bt_Zurhayrightdate_Click(object sender, EventArgs e)
        {//на один день вправо
            //  DateTime thisDay = DateTime.Today; //получаем текущую дату
            thisDay = thisDay.AddDays(1);
            UpdateDisplayDate();
            DoParsingMyXmlFile();

        }

        public void DoParsingMyXmlFile()
        {
            //парсинг xml файла
            //----XML работаем с нашей  xml-------------------------------------------------------------------------------------------------- 
            //XmlDocument newXmlDocument = new XmlDocument();
            //XmlTextReader xmlread = new XmlTextReader(Assets.Open("xml/ZurhayDataText.xml"));
            //string xmlread = Assets.Open("xml/ZurhayDataText.xml");
            //проверяем подгружен ли новый файл календаря
            //  File.ReadLines(System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename));
            // File.Delete(System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename));
            XDocument doc = XDocument.Load(Assets.Open("xml/ZurhayDataText.xml"));
            // string docpatch = Assets.Open("xml/ZurhayDataText.xml")
            // DateTime FileLastDateModifyInsideFile = File.GetLastWriteTime(doc);
            // Assets.
            DateTime FileLastDateModifyDownloadFile = File.GetLastWriteTime(System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename));
            if (needchangexml == 0)
            {
                //if (FileLastDateModifyDownloadFile.Day == thisDay.Day && (FileLastDateModifyDownloadFile - thisDay).TotalDays == 0)
                if ((thisDay - FileLastDateModifyDownloadFile).TotalDays >= 14)
                {
                    DownloadZurhayNewXML_Click(null, null);
                }
                //int FileLastDateModifINT = FileLastDateModif.Ticks{ GetDatabasePath;};
                //    - thisDay;
                if (File.Exists(System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename)))
                {

                    XDocument doc1 = XDocument.Load(System.IO.Path.Combine(documentsPathtoDownload, localDownloadFilename));
                    doc = doc1;
                }
                else
                {
                    if (DateTime.Now < thisDay.AddDays(-30))
                    {
                        DownloadZurhayNewXML_Click(null, null);
                    }
                    XDocument doc2 = XDocument.Load(Assets.Open("xml/ZurhayDataText.xml"));
                    doc = doc2;
                }
            }

            //---------------------пробуем открыть наш xml--------------------------
            try
            {
                //XDocument x1 = new XDocument();
                //x1 = XDocument.Load(doc);
                //XDocument.Load(doc);
            }
            catch (XmlException ex) // в случае ошибки вываливаем окно с предложением что делать
            {
                var alertmessage = new AlertDialog.Builder(this);
                alertmessage.SetMessage("@string/xml_error_load");
                alertmessage.SetPositiveButton("OK", delegate {
                    //тут надо что то сделать на кнопку ОК
                    //!!!!доделать функцию
                    Finish(); //пока оставлю финишь. надо сделать
                });
                alertmessage.SetNegativeButton("No", delegate // вместо  (s, e) => попробовать написать delegate типо:("OK", delegate { Finish(); });
                {
                    //тут надо что-то сделать на кнопку NO
                    Finish(); //нахерен всё. грохаем лэйр
                });
                alertmessage.Create().Show();//создаём и показываем.
            }
            //----парсим XML-------------------------------------------------------------------------------------
            foreach (XElement el in doc.Root.Elements())
            {


                //   Console.WriteLine("год сейчас!: {0} {1} {2} {3}", el.Name, el.Attribute("year").Value, el.Attribute("Month").Value, el.Attribute("day").Value); //проверяем параметры при отладке
                //    Console.WriteLine("а нужен!: {0}", WhatDateToday);

                if (el.Name != null)//проверяем чтоб параметры были не пустые
                {
                    //                    Console.WriteLine("год сейчас!!!!!!!!!!:" + xmlread.Value + ":между этим");
                    if (el.Name == "datetoday") //проверяем чтоб входящяя переменная была год.
                    {
                        TempYear = Int32.Parse(el.Attribute("year").Value); //конвертим параметр года в цифры
                        if (TempYear == thisDay.Year) //сравниваем текущий год с годом их xml параметра
                        {//если да - проверям месяц
                            TempMouths = Int32.Parse(el.Attribute("Month").Value);
                            if (TempMouths == thisDay.Month)
                            {
                                XmlMouths = el.Attribute("MunthName").Value;
                                TempDayDate = Int32.Parse(el.Attribute("day").Value);
                                if (TempDayDate == thisDay.Day)
                                {
                                    foreach (XElement element in el.Elements())
                                    {
                                        //определяем текствью
                                        TextView textView_DateName = FindViewById<TextView>(Resource.Id.textViewZurhay_DateName);
                                        TextView textView_GoodDoDay = FindViewById<TextView>(Resource.Id.textViewZurhay_GoodDoDay);
                                        TextView textView_BadDoDay = FindViewById<TextView>(Resource.Id.textViewZurhay_BadDoDay);
                                        TextView textView_WhatNeedDo = FindViewById<TextView>(Resource.Id.textViewZurhay_WhatNeedDo);
                                        TextView textView_WhatDatsanDoToday = FindViewById<TextView>(Resource.Id.textViewZurhay_WhatDatsanDoToday);

                                        //  CentralDataText = FindViewById<TextView>(Resource.Id.textViewZurhay_CentralData);
                                        UpdateDisplayDate();

                                        //присваеваем текст
                                        textView_DateName.Text = el.Element("WhatDayToday").Value;
                                        textView_GoodDoDay.Text = el.Element("GoodDay").Value;
                                        //    Console.WriteLine("проверяем поле BadDoDay: {0}", el.Element("BadDoDay").Value);
                                        textView_BadDoDay.Text = el.Element("BadDoDay").Value;
                                        //      Console.WriteLine("проверяем поле WhatNeedDo: {0}", el.Element("WhatNeedDo").Value);
                                        string tempWhatNeedDo = el.Element("WhatNeedDo").Value;
                                        if (tempWhatNeedDo != "")
                                        {
                                            textView_WhatNeedDo.Visibility = ViewStates.Visible;
                                            ImageView ZurhayImageView_WhatNeedDoToday = FindViewById<ImageView>(Resource.Id.ZurhayImageView_WhatNeedDoToday);
                                            ZurhayImageView_WhatNeedDoToday.Visibility = ViewStates.Visible;

                                            textView_WhatNeedDo.Text = el.Element("WhatNeedDo").Value;
                                        }
                                        else
                                        {
                                            textView_WhatNeedDo.Visibility = ViewStates.Gone;
                                            ImageView ZurhayImageView_WhatNeedDoToday = FindViewById<ImageView>(Resource.Id.ZurhayImageView_WhatNeedDoToday);
                                            ZurhayImageView_WhatNeedDoToday.Visibility = ViewStates.Gone;

                                        }
                                        string temphatDatsanDoToday = el.Element("WhatDatsanDoToday").Value;
                                        if (temphatDatsanDoToday != "")
                                        {
                                            textView_WhatDatsanDoToday.Visibility = ViewStates.Visible;
                                            ImageView ZurhayImageView_WhatDatsanDoToday = FindViewById<ImageView>(Resource.Id.ZurhayImageView_WhatDatsanDoToday);
                                            ZurhayImageView_WhatDatsanDoToday.Visibility = ViewStates.Visible;
                                            textView_WhatDatsanDoToday.Text = el.Element("WhatDatsanDoToday").Value;

                                        }
                                        else
                                        {
                                            textView_WhatDatsanDoToday.Visibility = ViewStates.Gone;
                                            ImageView ZurhayImageView_WhatDatsanDoToday = FindViewById<ImageView>(Resource.Id.ZurhayImageView_WhatDatsanDoToday);
                                            ZurhayImageView_WhatDatsanDoToday.Visibility = ViewStates.Gone;
                                        }

                                    }
                                    break;//данные получили, цикл закончили.
                                }
                            }
                            /*   Console.WriteLine("Elements:");
                               foreach(XElement element in el.Elements())
                               {
                               //    Console.WriteLine("Element: {0}: {1}", element.Name, element.Value);
                               }
                               */

                        }
                    }



                }
                else
                {

                    var alertmessage = new AlertDialog.Builder(this);
                    alertmessage.SetMessage("@string/xml_error_load");
                    alertmessage.SetPositiveButton("OK", delegate {
                        //тут надо что то сделать на кнопку ОК
                        //!!!!доделать функцию
                        Finish(); //пока оставлю финишь. надо сделать
                    });
                    alertmessage.SetNegativeButton("No", delegate // вместо  (s, e) => попробовать написать delegate типо:("OK", delegate { Finish(); });
                    {
                        //тут надо что-то сделать на кнопку NO
                        Finish(); //нахерен всё. грохаем лэйр
                    });
                    alertmessage.Create().Show();//создаём и показываем.
                }
                //break;

            }


            //---------------end DoParsingMyXmlFile--------------------------------------------------------------
        }//DoParsingMyXmlFile

        private void bt_ExittoZurhayYarsfromZurhay_Click(object sender, EventArgs e)
        {
            //  Finish();
            var ZurhayYearsActivity = new Intent(this, typeof(ZurhayYearsActivity));
            //  ZurhayYearsActivity.PutExtra("name", CentralDataText.ToString()); //передача переменной в другой activity
            StartActivity(ZurhayYearsActivity);
            //            DoParsingMyXmlFile(sender, e);


        }
    }
}