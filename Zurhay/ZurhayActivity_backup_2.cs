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
            bt_Zurhayrightdate = FindViewById<TextView>(Resource.Id.bt_Zurhayrightdate); //������ ������ �� 1 ����
            bt_Zurhayrightdate.Click += Bt_Zurhayrightdate_Click;
            bt_Zurhayleftdate = FindViewById<TextView>(Resource.Id.bt_Zurhayleftdate);
            bt_Zurhayleftdate.Click += Bt_Zurhayleftdate_Click;

            bt_exit_from_layout_Zurhay = FindViewById<Button>(Resource.Id.bt_exit_from_layout_Zurhay);
            bt_exit_from_layout_Zurhay.Typeface = btfonts;
            bt_exit_from_layout_Zurhay.Click += delegate
            {
                Finish();
            };



            thisDay = DateTime.Now; //�������� ������� ����
            DateTime RealTimeToDay = DateTime.Today; //��������� � ����������� ������
                                                     //System.DateTime thismounth = thismounth;
                                                     //thisDay.Month
                                                     //---------Data ���������� ���� � ������ �����-------------------------------------------------------------
                                                     // thisMonth = Int32.Parse(thisDay.ToString("MM")); //����� � ������
                                                     //  MonthName = thisDay.ToString("MMMM"); // �������� ������
                                                     // thisDayDate = Int32.Parse(thisDay.ToString("dd"));//����
                                                     // thisYear = Int32.Parse(thisDay.ToString("yyyy"));//���
                                                     //  WhatDateToday = thisDayDate;
                                                     //DaysInMonth = DateTime.DaysInMonth(thisYear, thisMonth);

            //Int32.Parse(thisDayDate); //���������� ���� � �����
            //  Int32.Parse(thisYear); //��������� ��� � �����
            // Display the date in the default (general) format.
            //   Console.WriteLine(thisDay.ToString());  //    5/3/2012 12:00:00 AM
            // Console.WriteLine();
            // Display the date in a variety of formats.
            //  Console.WriteLine(thisDay.ToString("d")); //    5/3/2012
            //  Console.WriteLine(thisDay.ToString("D")); //    Thursday, May 03, 2012
            //  Console.WriteLine(thisDay.ToString("g")); //    5/3/2012 12:00 AM

            //�������� ������ ��� ������ ��������� ������� ���������
            DatePikerText = FindViewById<TextView>(Resource.Id.textViewZurhay_CentralData);
            DatePikerText.Typeface = btfonts;
            DatePikerText.Click += delegate
            {
                ShowDialog(DATE_DIALOG_ID);
            };

            //���� ���������� �� �������� �� �����(������ ������ - ������)
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
            //�������� �������� �� ������ xml

        }
        public void DonwloadZurhayXML(Uri url)
        {
            //��������� ����� xml
            //var url = new Uri("http://����.��/����"); // download file
            var alertmessage = new AlertDialog.Builder(this);
            alertmessage.SetMessage("������ ����� ���� ������, ������ ������� ����������� ���������?(~400��)");
            alertmessage.SetPositiveButton("OK", delegate {
                try
                {
                    //������ ���������
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
                            Toast.MakeText(this, "��������� ��������", ToastLength.Short).Show();
                            UpdateDisplayDate();
                            DoParsingMyXmlFile();
                        }
                        else Toast.MakeText(this, "������ ����������, ���������� �����", ToastLength.Short).Show();

                    };
                    // Finish(); //���� ������� ������. ���� �������
                }
                catch (WebException webEx)
                {
                    Console.WriteLine(webEx.ToString());
                    if (webEx.Status == WebExceptionStatus.ConnectFailure)
                    {
                        Console.WriteLine("Are you behind a firewall?  If so, go through the proxy server.");
                    }
                    Toast.MakeText(this, "������ ����������, ���������� �����", ToastLength.Short).Show();
                }
            });


            alertmessage.SetNegativeButton("���", delegate // ������  (s, e) => ����������� �������� delegate ����:("OK", delegate { Finish(); });
            {
                needchangexml = 1;
                //��� ���� ���-�� ������� �� ������ NO
                // Finish(); //������� ��. ������� ����
            });
            alertmessage.Create().Show();//������ � ����������.



        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        { //�������� ����, � ��������� ��� ����������
            this.thisDay = e.Date;
            UpdateDisplayDate();
            DoParsingMyXmlFile();

        }

        protected override Dialog OnCreateDialog(int id)
        { //������� ������ ��� ��������� ���� �� ���������
            switch (id)
            {
                case DATE_DIALOG_ID:
                    return new DatePickerDialog(this, OnDateSet, thisDay.Year, thisDay.Month - 1, thisDay.Day);
            }
            return null;
        }
        private void UpdateDisplayDate()
        {//��������� ���� 
            CentralDataText = FindViewById<TextView>(Resource.Id.textViewZurhay_CentralData);
            CentralDataText.Text = thisDay.Day + " " + thisDay.ToString("MMMM");
        }
        private void Bt_Zurhayleftdate_Click(object sender, EventArgs e)
        {//�� ���� ���� �����
            thisDay = thisDay.AddDays(-1);
            UpdateDisplayDate();
            DoParsingMyXmlFile();
        }

        private void Bt_Zurhayrightdate_Click(object sender, EventArgs e)
        {//�� ���� ���� ������
            //  DateTime thisDay = DateTime.Today; //�������� ������� ����
            thisDay = thisDay.AddDays(1);
            UpdateDisplayDate();
            DoParsingMyXmlFile();

        }

        public void DoParsingMyXmlFile()
        {
            //������� xml �����
            //----XML �������� � �����  xml-------------------------------------------------------------------------------------------------- 
            //XmlDocument newXmlDocument = new XmlDocument();
            //XmlTextReader xmlread = new XmlTextReader(Assets.Open("xml/ZurhayDataText.xml"));
            //string xmlread = Assets.Open("xml/ZurhayDataText.xml");
            //��������� ��������� �� ����� ���� ���������
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

            //---------------------������� ������� ��� xml--------------------------
            try
            {
                //XDocument x1 = new XDocument();
                //x1 = XDocument.Load(doc);
                //XDocument.Load(doc);
            }
            catch (XmlException ex) // � ������ ������ ���������� ���� � ������������ ��� ������
            {
                var alertmessage = new AlertDialog.Builder(this);
                alertmessage.SetMessage("@string/xml_error_load");
                alertmessage.SetPositiveButton("OK", delegate {
                    //��� ���� ��� �� ������� �� ������ ��
                    //!!!!�������� �������
                    Finish(); //���� ������� ������. ���� �������
                });
                alertmessage.SetNegativeButton("No", delegate // ������  (s, e) => ����������� �������� delegate ����:("OK", delegate { Finish(); });
                {
                    //��� ���� ���-�� ������� �� ������ NO
                    Finish(); //������� ��. ������� ����
                });
                alertmessage.Create().Show();//������ � ����������.
            }
            //----������ XML-------------------------------------------------------------------------------------
            foreach (XElement el in doc.Root.Elements())
            {


                //   Console.WriteLine("��� ������!: {0} {1} {2} {3}", el.Name, el.Attribute("year").Value, el.Attribute("Month").Value, el.Attribute("day").Value); //��������� ��������� ��� �������
                //    Console.WriteLine("� �����!: {0}", WhatDateToday);

                if (el.Name != null)//��������� ���� ��������� ���� �� ������
                {
                    //                    Console.WriteLine("��� ������!!!!!!!!!!:" + xmlread.Value + ":����� ����");
                    if (el.Name == "datetoday") //��������� ���� �������� ���������� ���� ���.
                    {
                        TempYear = Int32.Parse(el.Attribute("year").Value); //��������� �������� ���� � �����
                        if (TempYear == thisDay.Year) //���������� ������� ��� � ����� �� xml ���������
                        {//���� �� - �������� �����
                            TempMouths = Int32.Parse(el.Attribute("Month").Value);
                            if (TempMouths == thisDay.Month)
                            {
                                XmlMouths = el.Attribute("MunthName").Value;
                                TempDayDate = Int32.Parse(el.Attribute("day").Value);
                                if (TempDayDate == thisDay.Day)
                                {
                                    foreach (XElement element in el.Elements())
                                    {
                                        //���������� ��������
                                        TextView textView_DateName = FindViewById<TextView>(Resource.Id.textViewZurhay_DateName);
                                        TextView textView_GoodDoDay = FindViewById<TextView>(Resource.Id.textViewZurhay_GoodDoDay);
                                        TextView textView_BadDoDay = FindViewById<TextView>(Resource.Id.textViewZurhay_BadDoDay);
                                        TextView textView_WhatNeedDo = FindViewById<TextView>(Resource.Id.textViewZurhay_WhatNeedDo);
                                        TextView textView_WhatDatsanDoToday = FindViewById<TextView>(Resource.Id.textViewZurhay_WhatDatsanDoToday);

                                        //  CentralDataText = FindViewById<TextView>(Resource.Id.textViewZurhay_CentralData);
                                        UpdateDisplayDate();

                                        //����������� �����
                                        textView_DateName.Text = el.Element("WhatDayToday").Value;
                                        textView_GoodDoDay.Text = el.Element("GoodDay").Value;
                                        //    Console.WriteLine("��������� ���� BadDoDay: {0}", el.Element("BadDoDay").Value);
                                        textView_BadDoDay.Text = el.Element("BadDoDay").Value;
                                        //      Console.WriteLine("��������� ���� WhatNeedDo: {0}", el.Element("WhatNeedDo").Value);
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
                                    break;//������ ��������, ���� ���������.
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
                        //��� ���� ��� �� ������� �� ������ ��
                        //!!!!�������� �������
                        Finish(); //���� ������� ������. ���� �������
                    });
                    alertmessage.SetNegativeButton("No", delegate // ������  (s, e) => ����������� �������� delegate ����:("OK", delegate { Finish(); });
                    {
                        //��� ���� ���-�� ������� �� ������ NO
                        Finish(); //������� ��. ������� ����
                    });
                    alertmessage.Create().Show();//������ � ����������.
                }
                //break;

            }


            //---------------end DoParsingMyXmlFile--------------------------------------------------------------
        }//DoParsingMyXmlFile

        private void bt_ExittoZurhayYarsfromZurhay_Click(object sender, EventArgs e)
        {
            //  Finish();
            var ZurhayYearsActivity = new Intent(this, typeof(ZurhayYearsActivity));
            //  ZurhayYearsActivity.PutExtra("name", CentralDataText.ToString()); //�������� ���������� � ������ activity
            StartActivity(ZurhayYearsActivity);
            //            DoParsingMyXmlFile(sender, e);


        }
    }
}