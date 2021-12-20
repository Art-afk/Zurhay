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
    class ContactActivity : DialogFragment
    {
        public Button btExitToMainMenu;



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
         

             base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Contacts, container, false);

            btExitToMainMenu = view.FindViewById<Button>(Resource.Id.btExitToMainMenuFromContacts);
            btExitToMainMenu.Click += BtExitToMainMenu_Click;

            return view;
            
            

        }

        private void BtExitToMainMenu_Click(object sender, EventArgs e)
        {
            //  throw new NotImplementedException();
            Dismiss();
            Toast.MakeText(Activity, "Спасибо за использование нашего продукта!", ToastLength.Short).Show();

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //заголовог невидимый
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; //установка анимации
            


        }

        
       

    }
}