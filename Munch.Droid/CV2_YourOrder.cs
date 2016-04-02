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

            //CustomerPortal.CustomerOrderList.Insert(0, (new CustomerOrderItem() { Dish = "dishname", Quantity = "test quant", Notes = "test note", OrderNumber = "" }));
            //  CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = Menu.dishName_to_order, Quantity = "testq", Notes = "testn", OrderNumber = "" });
            // CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = "huhui", Quantity = "testq", Notes = "testn", OrderNumber = "" });
            // CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = "food", Quantity = "testq", Notes = "testn", OrderNumber = "" });

            CV_your_Order_ListViewAdapter adapter = new CV_your_Order_ListViewAdapter(this, (CustomerPortal.CustomerOrderList));
            Console.Out.WriteLine(CustomerPortal.CustomerOrderList + "******************************");
            mListView.Adapter = adapter;

        }
    }
}