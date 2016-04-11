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
using com.refractored.fab;
using System.Threading;
using System.Net;
using System.Threading.Tasks;
using System.Json;
using System.IO;
using Newtonsoft.Json;
using Android.Support.V4.Widget;

namespace Munch
{
    [Activity(Label = "WP_CustomerOrder", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class WP_CustomerOrder : Activity
    {
        private List<WP_CO_OrderList> mItems;
        public ListView mListView;
        public SwipeRefreshLayout refresher;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WP_CustomerOrder);

            /**
            //Load Up List
            String accountsURL = "http://54.191.98.63/accounts.php";
            JsonValue json = await JsonParsing<Task<JsonValue>>.FetchDataAsync(accountsURL);
            List<WP_CO_OrderList> parsedData = JsonParsing<WP_CO_OrderList>.ParseAndDisplay(json);
            mItems = parsedData;
            **/
            mListView = FindViewById<ListView>(Resource.Id.WP_customerorderListView);
            /**
            parsedData.Insert(0, (new AP_AM_AccountList() { idAccounts = "Username", Level = "Account Type"}));
            WP_CO_ListViewAdapter adapter = new WP_CO_ListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;
            **/

            //Swipe to Refresh
            var refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.swipecontainerWPCO);
            refresher.SetColorScheme(Resource.Color.primary, Resource.Color.accent_pressed, Resource.Color.accent_material_dark);
            refresher.Refresh += HandleRefresh;
        }

        //Swipe to Refresh Activity
        void HandleRefresh(object sender, EventArgs e)
        {
            StartActivity(typeof(WP_CustomerOrder));
        }
    }
}