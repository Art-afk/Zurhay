using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Zurhay
{
    [Activity(Label = "AvailiableBuddizmActivity", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class AvailiableBuddizmActivity : ActionBarActivity
    {
        //�������
        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
      //  private RelativeLayout mRightDrawer;
        private ArrayAdapter mLeftAdapter;
        //private ArrayAdapter mRightAdapter;
        private List<string> mLeftDataSet;

        //������
        private Button bt_ExittoMainfromAvailableBuddizm1;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AvailableBuddizm);
            //�������� � �������� ����
            // �������� � ������ � ��������
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar_AvailiableBuddizm);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout_AvailiableBuddizm);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer_AvailiableBuddizm);
         //   mRightDrawer = FindViewById<RelativeLayout>(Resource.Id.right_drawer_lay_AvailiableBuddizm);
            

            SetSupportActionBar(mToolbar);
            //������ ����� ��� ������
            Typeface btfonts = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Luminari.ttf");

            mLeftDrawer.Tag = 0;
          //  mRightDrawer.Tag = 1;


            mLeftDataSet = new List<string>();
            mLeftDataSet.Add("������");
            mLeftDataSet.Add("��������� �������");
            mLeftDataSet.Add("������ ������");
            mLeftDataSet.Add("������");
            mLeftDataSet.Add("������");
            // mLeftDataSet.Add("����� �������");
            mLeftDataSet.Add("��������");
            mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick; //������� �� ���� �� mLeftDataSet

            // mLeftDrawer.Adapter = Android.Resource.Layout.SimpleListItem1
            //right layout
        


            mDrawerToggle = new MyActionBarDrawerToggle(
                this,     //Host Activity
                mDrawerLayout,      //DrawerLayout
                Resource.String.openDrawer,      //Opened  Message
                Resource.String.closeDrawer     //close Message
                );
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true); //���������� ��������� ����
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mDrawerToggle.SyncState();

            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.openDrawer);
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.closeDrawer);
                }

            }
            else
            {
                // ��� ��� �������� ������ ���
                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }
            //�������� ���� � ����������
            /* var showContacts = FindViewById<Button>(Resource.Id.btContacts);
              showContacts.Click += (sender, e) =>
              {
                  var contact = new Intent(this, typeof(ContactActivity));
                  //contact.PutExtra("FirstData", "Data from FirstActivity");
                StartActivity(contact);
               }
                */



            // Create your application here
           


            Button bt_WhatisBuddizm = FindViewById<Button>(Resource.Id.bt_WhatisBuddizm_AvailableBuddizm1); //��� ����� �������
            bt_WhatisBuddizm.SetTypeface(btfonts, TypefaceStyle.Bold);
            bt_WhatisBuddizm.Click += Bt_WhatisBuddizm_Click;

            Button bt_whayiszurhay = FindViewById<Button>(Resource.Id.bt_btWahtIsZurhay); // ��� ����� ������
            bt_whayiszurhay.SetTypeface(btfonts, TypefaceStyle.Bold);
            bt_whayiszurhay.Click += Bt_whayiszurhay_Click;

            Button bt_bojestva = FindViewById<Button>(Resource.Id.btBojestvavBuddizme); // ������ �������� � ��������
            bt_bojestva.SetTypeface(btfonts, TypefaceStyle.Bold);
            bt_bojestva.Click += Bt_bojestva_Click;


            Button bt_toHuraly = FindViewById<Button>(Resource.Id.bt_btHURALY); // ������ ������
            bt_toHuraly.SetTypeface(btfonts, TypefaceStyle.Bold);
            bt_toHuraly.Click += Bt_toHuraly_Click;

            bt_ExittoMainfromAvailableBuddizm1 = FindViewById<Button>(Resource.Id.bt_btBuddizmFAQ); //������ FAQ
            bt_ExittoMainfromAvailableBuddizm1.SetTypeface(btfonts, TypefaceStyle.Bold);
            bt_ExittoMainfromAvailableBuddizm1.Click += Bt_ExittoMainfromAvailableBuddizm1_Click;

            Button bt_toExit = FindViewById<Button>(Resource.Id.bt_btBuddizmExit); // ������ ������
            bt_toExit.SetTypeface(btfonts, TypefaceStyle.Bold);
            bt_toExit.Click += delegate
            {
                Finish();
            }; ;

        }

        private void Bt_WhatisBuddizm_Click(object sender, EventArgs e)
        {
            var WhatisBuddizm = new Intent(this, typeof(WhatisBuddizmActivity));
            StartActivity(WhatisBuddizm);
        }

        private void Bt_whayiszurhay_Click(object sender, EventArgs e)
        {
            var whayiszurhay = new Intent(this, typeof(WhayIsZurhayActivity));
            StartActivity(whayiszurhay);
        }

        private void Bt_bojestva_Click(object sender, EventArgs e)
        {
            var bojestva = new Intent(this, typeof(DivinityInBuddizmActivity));
            StartActivity(bojestva);
        }

        private void Bt_toHuraly_Click(object sender, EventArgs e)
        {
            var bthuryaly = new Intent(this, typeof(HuralyActivity));
            StartActivity(bthuryaly);
        }

        private void Bt_ExittoMainfromAvailableBuddizm1_Click(object sender, EventArgs e)
        {
            var FAQbuddizm = new Intent(this, typeof(FAQActivity));
            StartActivity(FAQbuddizm);
        }

        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // throw new NotImplementedException();
            //Console.WriteLine(mLeftDataSet[e.Position]); //��������� ��� ������ e.position 
            string click = mLeftDataSet[e.Position];
            switch (click)
            {
                case "��������":
                    {
                        /*       var contact = new Intent(this, typeof(ContactActivity));
                               //contact.PutExtra("FirstData", "Data from FirstActivity");
                               StartActivity(contact);
                         */

                        //btContactUS = FindViewById<Button>(Resource.Id.btContactUS);
                        //Pull up dialog
                        FragmentTransaction transaction = FragmentManager.BeginTransaction();
                        ContactActivity splashDialog = new ContactActivity();
                        splashDialog.Show(transaction, "dialog fragment");
                        mDrawerLayout.CloseDrawer(mLeftDrawer);

                        break;
                    }
                case "������":
                    {
                        //  Delegate(StartActivity(typeof(ZurhayActivity)));
                        var zurhaylay = new Intent(this, typeof(ZurhayActivity));
                        StartActivity(zurhaylay);


                        //Btzurhaitolay_Click(sender, e);
                        break;
                    }
                case "��������� �������":
                    {
                        var avalibBuddizm = new Intent(this, typeof(AvailiableBuddizmActivity));
                        StartActivity(avalibBuddizm);
                        // BtAvalibleBuddhism_Click(sender, e);
                        break;
                    }
                case "������ ������":
                    {
                        var btsangha_Russia_to_Lay = new Intent(this, typeof(SanghaRussiaActivity));
                        StartActivity(btsangha_Russia_to_Lay);
                        // Btsangha_Russia_to_Lay_Click(sender, e);
                        break;
                    }
                case "������":
                    {
                        var btdatsans = new Intent(this, typeof(DatsansActivity));
                        StartActivity(btdatsans);
                        //Btdatsans_Click(sender, e);
                        break;

                    }
                case "������":
                    {
                        var btmantras = new Intent(this, typeof(MantrasActivity));
                        StartActivity(btmantras);
                        //Btmantras_Click(sender, e);
                        break;
                    }
                    /*   case "����� �������":
                           {
                               Btzakazmolebna_Click(sender, e);
                               break;
                           }
                           */
            }


        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    // ������ ������, ����� �������� ��� ���� ������ �������� ���� �� ������������
                //    mDrawerLayout.CloseDrawer(mRightDrawer);
                    mDrawerToggle.OnOptionsItemSelected(item);

                    return true;

              //  case Resource.Id.action_refresh:
                    // refrash
              //      return true;


                case Resource.Id.action_help:

               /*     if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                    {
                        // �������� ���� ��� ������ , ��������.
                        mDrawerLayout.CloseDrawer(mRightDrawer);
                    }
                    else
                    {
                        //������ ���� ������, ���������, � ��������� �����.
                        mDrawerLayout.OpenDrawer(mRightDrawer);
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                    }
                    */
                   return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            MenuInflater.Inflate(Resource.Menu.action_menu_AvailiableBuddizm, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        //�������� ������� ���� � �������� ��� �������� ����������
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }
            else
            {
                outState.PutString("DrawerState", "Closed");
            }
            base.OnSaveInstanceState(outState);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {

            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }


    }
}