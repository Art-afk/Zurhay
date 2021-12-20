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
   

    [Activity(Label = "DivinityInBuddizmActivity", Theme ="@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DivinityInBuddizmActivity : Activity
    {
        //List<string> _mDivinity = new List<string>();
           private List<string> _mDivinity;
           private ArrayAdapter ArrayDivinity;
        public WebView FAQWebView;
        public AlertDialog dlgAlert;
        Typeface btfonts = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Luminari.ttf");

        protected override void OnCreate(Bundle bundle)

        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DivinityInBuddizm);

            var centralname = FindViewById<TextView>(Resource.Id.textView_CentralDataDivinityInBuddizm);
            centralname.Typeface = btfonts;

            // Create your application here

            FAQWebView = FindViewById<WebView>(Resource.Id.webview_DivinityInBuddizm_central);
            FAQWebView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            FAQWebView.SetVerticalScrollbarOverlay(true);

            FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/bozhestva-buddizma.html");

            Button bt_choiceDivinity = FindViewById<Button>(Resource.Id.bt_choceDivinity__DivinityInBuddizm);
            bt_choiceDivinity.Click += ShowAlterMenu;

            Button bt_exit_from_layout_Divinity = FindViewById<Button>(Resource.Id.bt_exit_from_layout_Divinity);
            bt_exit_from_layout_Divinity.Typeface = btfonts;
            bt_exit_from_layout_Divinity.Click += delegate
            {
                Finish();
            };


        }

        public void ShowAlterMenu(object sender, EventArgs e)
        {
           
            _mDivinity = new List<string>();
            _mDivinity.Add("��������"); //1
            _mDivinity.Add("��������������");//2
            _mDivinity.Add("�������");//3
            _mDivinity.Add("�������"); //4
            _mDivinity.Add("�����������"); //5
            _mDivinity.Add("���������");
            _mDivinity.Add("�����������");
            _mDivinity.Add("�������������");
            _mDivinity.Add("��������������");
            _mDivinity.Add("����������");
            _mDivinity.Add("�����������");
            _mDivinity.Add("������������");
            _mDivinity.Add("����������");
            _mDivinity.Add("������������");
            _mDivinity.Add("���������");
            _mDivinity.Add("������");
            _mDivinity.Add("�������");
            _mDivinity.Add("������������");
            _mDivinity.Add("��������");
            _mDivinity.Add("�����������");
            _mDivinity.Add("���������");
            _mDivinity.Add("�������");
            _mDivinity.Add("���������");
            _mDivinity.Add("�����");
            _mDivinity.Add("��������");
            _mDivinity.Add("��������");
            _mDivinity.Add("�������������");
            _mDivinity.Add("�����������");
            _mDivinity.Add("�����");
            _mDivinity.Add("�������������");
            _mDivinity.Add("�������������������");
            _mDivinity.Add("������������");
            _mDivinity.Add("�������");
            _mDivinity.Add("��������");
            _mDivinity.Add("��������");
            //�����
            ArrayDivinity = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _mDivinity);

             dlgAlert = (new AlertDialog.Builder(this)).Create();
            dlgAlert.SetTitle("�������� ��������");
            var listview = new ListView(this);
            listview.Adapter = new AlertListViewAdapter(this, _mDivinity);
            listview.ItemClick += Listview_ItemClick;
            dlgAlert.SetView(listview);
            dlgAlert.SetButton("OK", handllerNotingButton);
            dlgAlert.Show();



                    }

        private void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
          //����� ������ �������� �������� ����� ��� � ����� �� �����
            Toast.MakeText(this, "�� ������� " + _mDivinity[e.Position], ToastLength.Short).Show();
          

            string click = _mDivinity[e.Position];
          //  int click2 = _mDivinity[e.Id];
            switch(click)
                {//switch start
                case "��������":
                    {

                        //Button btnClicked = objAlertDialog.GetButton(e.w);
                        FAQWebView.ClearHistory();
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Amitabha.html");
                        dlgAlert.Dismiss();

                        //FAQWebView.Reload();


                        break;

                    }
                case "��������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/AVALOKITEShVARA.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Akshobya.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Amitayus.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�����������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Amogasiddhi.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "���������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/BELAYa_TARA.html");
                        dlgAlert.Dismiss();
                        break;
                        
                    }
                case "�����������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/BelyyStarec.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/BuddaMediciny.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "��������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/BuddaShakyamuni.html");
                        dlgAlert.Dismiss();
                        break;
                     }
                case "����������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Bhavachakra.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�����������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Vadjradhara.html");
                        dlgAlert.Dismiss();
                        break;
                    } 
                        case "������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Vadjrayogini.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "����������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Vadjrapani.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Vadjrasattva.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                    
                case "���������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Vayrochana.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Garuda.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Gubilha.html");
                        dlgAlert.Dismiss();
                        break;
                    }

                case "������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Guhyasamadja.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                    
                        case "��������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Dzambala.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�����������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/ZelenayaTara.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                
                case "���������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Kalachakra.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Maytreya.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "���������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Mandjushri.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�����":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Marpa.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "��������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Mahakala.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "��������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Milarepa.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Padmasambhava.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�����������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/PaldenLhamo.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�����":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Pehar.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Samantabhadra.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/SmeyuschiysyaBuddaHotey.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "������������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Ushnishavidjaya.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "�������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Conkapa.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "��������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Chitipati.html");
                        dlgAlert.Dismiss();
                        break;
                    }
                case "��������":
                    {
                        FAQWebView.LoadUrl("file:///android_asset/www/DivinityBuddizm/Yamantaka.html");
                        dlgAlert.Dismiss();
                        break;
                    }

                default:
                    break;

            }//end switch


        }
        void handllerNotingButton(object sender, DialogClickEventArgs e)
        {
            AlertDialog objAlertDialog = sender as AlertDialog;
            Button btnClicked = objAlertDialog.GetButton(e.Which);
            Toast.MakeText(this, "�� ������� " + btnClicked.Text, ToastLength.Long).Show();
        }

        private void textSmaller()
        {

            // WebSettings settings = mWebView.getSettings();
            //FAQWebView. setTextZoom(settings.getTextZoom() - 10);
        }

        private void textBigger()
        {

         //   WebSettings settings = FAQWebView.getSettings();
          //  settings.setTextZoom(settings.getTextZoom() + 10);
        }
    }
}