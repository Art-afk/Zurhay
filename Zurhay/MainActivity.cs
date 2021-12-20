using System.Text;
using System;
using System.Collections.Generic;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Content;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Content.Res;
using Android.Graphics;


namespace Zurhay
{
    [Activity(Label = "Доступный Буддизм", MainLauncher = true, Icon = "@drawable/ic_zur", Theme ="@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
   // [Activity(Label = "Доступный Буддизм", MainLauncher = true, Icon = "@drawable/ZurhayIco", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class MainActivity : ActionBarActivity
    {
        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
    //    private RelativeLayout mRightDrawer;
        private ArrayAdapter mLeftAdapter;
//private ArrayAdapter mRightAdapter;
        private List<string> mLeftDataSet;
       // private List<string> mRightDataSet;
        private Button btContactUS;
        private Button Btzurhaitolay;
        private Button btAvalibleBuddhism;
        private Button btsangha_Russia_to_Lay;
        private Button btdatsans;
        private Button btmantras;
        private Button btzakazmolebna;
       //определим дату рождения
        public TextView DatePikerText;
        const int DATE_DIALOG_ID = 0;
        public DateTime thisDay;
        public DateTime BirdthDay;
        public TextView TextBirdyhday;



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main); 
            
            // Создание и работа с тулбаром
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
       //     mRightDrawer = FindViewById<RelativeLayout>(Resource.Id.right_drawer_lay);
            
            SetSupportActionBar(mToolbar);
            //задаем шрифт для кнопок
            Typeface btfonts = Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Luminari.ttf");

            mLeftDrawer.Tag = 0;
         //   mRightDrawer.Tag = 1;

            
            mLeftDataSet = new List<string>();
            mLeftDataSet.Add ("Зурхай");
            mLeftDataSet.Add("Доступный буддизм");
            mLeftDataSet.Add("Сангха России");
            mLeftDataSet.Add("Дацаны");
            mLeftDataSet.Add("Мантры");
           // mLeftDataSet.Add("Заказ молебна");
            //mLeftDataSet.Add("Контакты");
            mLeftAdapter = new ArrayAdapter  <string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick; //нажатие на один из mLeftDataSet

            // mLeftDrawer.Adapter = Android.Resource.Layout.SimpleListItem1
            //right layout
           // mRightDrawer.Adapter 


            mDrawerToggle = new MyActionBarDrawerToggle(
                this,     //Host Activity
                mDrawerLayout,      //DrawerLayout
                Resource.String.openDrawer,      //Opened  Message
                Resource.String.closeDrawer     //close Message
                );
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true); //показывать называние окна
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mDrawerToggle.SyncState();
            
            if(bundle != null)
            {
                if (bundle.GetString("DrawerState")== "Opened")
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
             // это при открытие первый раз
                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }
            //открытие окна с контактами
            /* var showContacts = FindViewById<Button>(Resource.Id.btContacts);
              showContacts.Click += (sender, e) =>
              {
                  var contact = new Intent(this, typeof(ContactActivity));
                  //contact.PutExtra("FirstData", "Data from FirstActivity");
                StartActivity(contact);
               }
                */
                

            
            Btzurhaitolay = FindViewById<Button>(Resource.Id.Btzurhaitolay); //кнопка Зурхай
            Btzurhaitolay.SetTypeface(btfonts, TypefaceStyle.Bold);
            Btzurhaitolay.Click += Btzurhaitolay_Click;

            btAvalibleBuddhism = FindViewById<Button>(Resource.Id.btAvalibleBuddhism); //Кнопка Доступный Буддизм
            btAvalibleBuddhism.SetTypeface(btfonts, TypefaceStyle.Bold);
            btAvalibleBuddhism.Click += BtAvalibleBuddhism_Click;

            btsangha_Russia_to_Lay = FindViewById<Button>(Resource.Id.btsangha_Russia_to_Lay); // Кнопка Сангха России
            btsangha_Russia_to_Lay.SetTypeface(btfonts, TypefaceStyle.Bold);
            btsangha_Russia_to_Lay.Click += Btsangha_Russia_to_Lay_Click;

            btdatsans = FindViewById<Button>(Resource.Id.btdatsans); //Кнопка Датсаны
            btdatsans.SetTypeface(btfonts, TypefaceStyle.Bold);
            btdatsans.Click += Btdatsans_Click;

            btmantras = FindViewById< Button > (Resource.Id.btmantras); //Кнопка Мантры
            btmantras.SetTypeface(btfonts, TypefaceStyle.Bold);
            btmantras.Click += Btmantras_Click;

        /*    btzakazmolebna = FindViewById<Button>(Resource.Id.btzakazmolebna); //Заказ Молебна
            btzakazmolebna.SetTypeface(btfonts, TypefaceStyle.Bold);
            btzakazmolebna.Click += Btzakazmolebna_Click;
        
            btContactUS = FindViewById<Button>(Resource.Id.btContactUS); //Кнока Контакты
            btContactUS.SetTypeface(btfonts, TypefaceStyle.Bold);
            btContactUS.Click += (object sender, EventArgs args) => // Вызов всплывающего меню
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                ContactActivity splashDialog = new ContactActivity();
                splashDialog.Show(transaction, "dialog fragment");
            };
            */


            //final EditText secondEditText = (TextView)findViewById(R.id.editText2); //находим поле в котором хотим поменять шрифт
            //secondEditText.setTypeface(Typeface.createFromAsset(getAssets(), "fonts/Catwalk.ttf")); //задаем шрифт Catwakl.ttf из assets
            //Nullable BirdthDay;

            //BirdthDay = null;


            BirdthDay = new DateTime(2000, 01, 01); // ЗАМЕНИТЬ НА ИЗ СОХРАНЕННОГО!!!
            DateTime TempNullDate = new DateTime(2000, 01, 01);
            int compareDate = TempNullDate.CompareTo(BirdthDay);

            if (compareDate == 0)
            {
                BirdthDay = new DateTime(2000, 01, 01);
                DataBirdthdayUpdate(BirdthDay);

                //если переменная день рождение пустая - делаем то -то:



            }


        }// -----------------------------------------------------------------Закончился OnCreate

        private void DataBirdthdayUpdate(DateTime DataChoise)
        {
         //   TextBirdyhday = FindViewById<TextView>(Resource.Id.Main_right_drawer_textView_Birdthday);
         //   TextBirdyhday.Text = BirdthDay.ToString("d");
        }

     /*   private void Btzakazmolebna_Click(object sender, EventArgs e)
        {
            var btzakazmolebna = new Intent(this, typeof(ZakazMolebnaActivity));
            StartActivity(btzakazmolebna);
        }
        */
        private void Btmantras_Click(object sender, EventArgs e) //открываем раздел мантры
        {
            var btmantras = new Intent(this, typeof(MantrasActivity));
            StartActivity(btmantras);
        }

        private void Btdatsans_Click(object sender, EventArgs e) //открываем раздел датцаны
        {
            var btdatsans = new Intent(this, typeof(DatsansActivity));
            StartActivity(btdatsans);
        }

        private void Btsangha_Russia_to_Lay_Click(object sender, EventArgs e) //открываем раздел Сангха России
        {
            var btsangha_Russia_to_Lay = new Intent(this, typeof(SanghaRussiaActivity));
            StartActivity(btsangha_Russia_to_Lay);
        }

        private void BtAvalibleBuddhism_Click(object sender, EventArgs e) //открываем раздел Доступный буддизм 
        {
            var avalibBuddizm = new Intent(this, typeof(AvailiableBuddizmActivity));
            StartActivity(avalibBuddizm);
    }

        private void Btzurhaitolay_Click(object sender, EventArgs e) //открываем раздел Зурхай
        {
            var zurhaylay = new Intent(this, typeof(ZurhayActivity));
            StartActivity(zurhaylay);

        }

        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e) 
        {
            // throw new NotImplementedException();
            //Console.WriteLine(mLeftDataSet[e.Position]); //проверяем что выводи e.position 
            string click = mLeftDataSet[e.Position];
            switch (click)
            {
                /*case "Контакты":
                    {
                               var contact = new Intent(this, typeof(ContactActivity));
                               //contact.PutExtra("FirstData", "Data from FirstActivity");
                               StartActivity(contact);
                         

                        btContactUS = FindViewById<Button>(Resource.Id.btContactUS);
                            //Pull up dialog
                            FragmentTransaction transaction = FragmentManager.BeginTransaction();
                            ContactActivity splashDialog = new ContactActivity();
                            splashDialog.Show(transaction, "dialog fragment");
                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                        
                            break;
        }
               */
                case "Зурхай":
                    {
                        Btzurhaitolay_Click(sender, e);
                        break;
                    }
                case "Доступный буддизм":
                    {
                        BtAvalibleBuddhism_Click(sender, e);
                        break;
                    }
                case "Сангха России":
                    {
                        Btsangha_Russia_to_Lay_Click(sender, e);
                        break;
                            }
                case "Дацаны":
                    {
                        Btdatsans_Click(sender, e);
                        break;

                    }
                case "Мантры":
                    {
                        Btmantras_Click(sender, e);
                        break;
                    }
             /*   case "Заказ молебна":
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
                    // иконка нажата, нужно убедится что слой правый закрылся чтоб не пересекались
               //     mDrawerLayout.CloseDrawer(mRightDrawer);
                    mDrawerToggle.OnOptionsItemSelected(item);

                    return true;

                //case Resource.Id.action_refresh:
                    // refrash
                //    return true;

                case Resource.Id.action_help:
                    {

                        TextView showPopupMenu = FindViewById<TextView>(Resource.Id.action_help);

                        PopupMenu menu = new PopupMenu(this, showPopupMenu);

                        // with Android 3 need to use MenuInfater to inflate the menu
                        //menu.MenuInflater.Inflate (Resource.Menu.popup_menu, menu.Menu);

                        // with Android 4 Inflate can be called directly on the menu
                        menu.Inflate(Resource.Menu.popup_menu);

                        menu.MenuItemClick += (s1, arg1) => {
                            Console.WriteLine("{0} selected", arg1.Item.TitleFormatted);
                            string Tmenunema = Convert.ToString(arg1.Item.TitleFormatted);
                            switch(Tmenunema)
                            { 
                            case "хз":
                                {
                                    break;
                                }
                                case "Контакты":
                                    {
                                        FragmentTransaction transaction = FragmentManager.BeginTransaction();
                                        ContactActivity splashDialog = new ContactActivity();
                                        splashDialog.Show(transaction, "dialog fragment");
                                        mDrawerLayout.CloseDrawer(mLeftDrawer);
                                        break; }
                                case "О Приложение":
                                    {

                                        var Disclamer = new Intent(this, typeof(DisclamerActivity));
                                        StartActivity(Disclamer);

                                        break; }
                            }
                        };

                        // Android 4 now has the DismissEvent
                        menu.DismissEvent += (s2, arg2) => {
                            Console.WriteLine("menu dismissed");
                        };

                        menu.Show();
                        /*       if (mDrawerLayout.IsDrawerOpen(mRightDrawer))
                               {
                                   // правывый слой уже открыт , закрывем.
                                   mDrawerLayout.CloseDrawer(mRightDrawer);
                               }
                               else
                               {
                                   //правый слой закрыт, открываем, и закрываем левый.
                                   mDrawerLayout.OpenDrawer(mRightDrawer);
                                   mDrawerLayout.CloseDrawer(mLeftDrawer);
                               }
                               */
                        mDrawerLayout.CloseDrawer(mLeftDrawer); // если нет правого, то просто закрываем левый
                        return true;
                    }
                default:
                    return base.OnOptionsItemSelected(item);
            }

        }     

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        //сохраним позиции меню и название при повороте устройства
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