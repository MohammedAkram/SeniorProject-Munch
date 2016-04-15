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
using System.Net;

namespace Munch
{
    [Activity(Label = "CV2_YourOrder", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class CV2_YourOrder : Activity
    {
        public ListView mListView;
        private List<CustomerOrderItem> mItems;

        public static int count = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //set view
            SetContentView(Resource.Layout.CV_Your_Order);

            mListView = FindViewById<ListView>(Resource.Id.mngYour_Order_ListView);
            Button orderUP = FindViewById<Button>(Resource.Id.ORderUP);

            CV_your_Order_ListViewAdapter adapter = new CV_your_Order_ListViewAdapter(this, (CustomerPortal.CustomerOrderList));
            mListView.Adapter = adapter;
            mListView.ItemClick += MListView_ItemClick;
            //Orderbutton click

            orderUP.Click += OrderUP_Click;

        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            
          //  mListView.RemoveViewAt(e.Position);
          //  mListView.DeferNotifyDataSetChanged();
        }
        



        //order button 
        private void OrderUP_Click(object sender, EventArgs e)
        {

            //loop into listview
            for (int i = 0; i < mListView.Count; i++)
            {
                //gets each row view and stores it into V 
                var v = mListView.Adapter.GetView(i, null, null);
                //get the textview of the dish name
                var dishname = (TextView)v.FindViewById(Resource.Id.Manage_Your_Order_Txt_Name);
                var quantity = (TextView)v.FindViewById(Resource.Id.Manage_Your_Order_Txt_Quantity);
                var notes = (TextView)v.FindViewById(Resource.Id.Manage_Your_Order_Txt_Note);
                var price = (TextView)v.FindViewById(Resource.Id.Manage_Your_Order_Txt_Units);

                var loginname = LoginScreen.loginUsername;


                //NEED URL FOR ADDING TO MYSQL
                var webClient = new WebClient();
                webClient.DownloadString("http://54.191.98.63/orders.php?idAccounts=" + loginname + "&&name=" + dishname.Text + "&&Quantity=" + quantity.Text + "&&Note=" + notes.Text + "&&count=" + count + "&&price=" + price.Text);
            }
            
            Android.Widget.Toast.MakeText(this, "ORDER UP, FOOD WILL BE READY IN 10 MIN", Android.Widget.ToastLength.Short).Show();
        }
    }
}