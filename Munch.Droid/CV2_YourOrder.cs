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

namespace Munch
{
    [Activity(Label = "CV2_YourOrder", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class CV2_YourOrder : Activity
    {
        public ListView mListView;
        private List<CustomerOrderItem> mItems;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CV_Your_Order);
            mListView = FindViewById<ListView>(Resource.Id.mngYour_Order_ListView);
            Button orderUP = FindViewById<Button>(Resource.Id.ORderUP);
            //CustomerPortal.CustomerOrderList.Insert(0, (new CustomerOrderItem() { Dish = "dishname", Quantity = "test quant", Notes = "test note", OrderNumber = "" }));
            //  CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = Menu.dishName_to_order, Quantity = "testq", Notes = "testn", OrderNumber = "" });
            // CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = "huhui", Quantity = "testq", Notes = "testn", OrderNumber = "" });
            // CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = "food", Quantity = "testq", Notes = "testn", OrderNumber = "" });

            CV_your_Order_ListViewAdapter adapter = new CV_your_Order_ListViewAdapter(this, (CustomerPortal.CustomerOrderList));
            Console.Out.WriteLine(CustomerPortal.CustomerOrderList + "******************************");
            mListView.Adapter = adapter;
            //Orderbutton click

            orderUP.Click += OrderUP_Click;

        }
        public static string Namee;
        public static string quantityy;
        public static string notesssssss;
        public static int count = 0;
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
                var notesss = (TextView)v.FindViewById(Resource.Id.Manage_Your_Order_Txt_Note);
                //add the dish name to a static  string called m which can be used to store info in the database
                Namee = (dishname.Text);
                quantityy = (quantity.Text);
                notesssssss = (notesss.Text);
                var loginname = LoginScreen.loginUsername;

                Console.WriteLine(Namee + "################################################3");

                //NEED URL FOR ADDING TO MYSQL
                //var webClient = new WebClient();
                // webClient.DownloadString("HTTP FOR SENDING EACH ORDER IN THE LIST TO SPECIFIC TABLE IN MYSQL");
            }

            Android.Widget.Toast.MakeText(this, "ORDER UP, FOOD WILL BE READY IN 10 MIN", Android.Widget.ToastLength.Short).Show();
        }
    }
}