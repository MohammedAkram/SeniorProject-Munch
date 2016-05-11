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
    [Activity(Label = "WaiterPortal", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class WaiterPortal : Activity
    {
        public ListView mListView;
        public static string IDACCOUNT;
        public static List<WaiterPortal_List>  Selecttable = new List<WaiterPortal_List>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WaiterPortal);

            mListView = FindViewById<ListView>(Resource.Id.Waiter_portal_ListView);
            WaiterPortal_ListViewAdapter adapter = new WaiterPortal_ListViewAdapter(this, Selecttable);
            mListView.Adapter = adapter;
            mListView.ItemClick += mListView_itemClick;
        }

        private void mListView_itemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var v = mListView.Adapter.GetView(e.Position, null, null);
            var idaccount = (TextView)v.FindViewById(Resource.Id.Manage_Selecteedtable_Name);
            IDACCOUNT = idaccount.Text;
            StartActivity(typeof(WP_CustomerOrder));
        }
    }
}