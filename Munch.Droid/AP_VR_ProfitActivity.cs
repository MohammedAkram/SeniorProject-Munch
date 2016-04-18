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
using System.Threading.Tasks;
using System.Json;
using System.Threading;

namespace Munch
{
    [Activity(Label = "AP_VR_Activity", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class AP_VR_ProfitActivity : Activity
    {
        private List<AP_VR_ProfitList> mItems;
        public ListView mListView;
        public Button[] btns;
        public String profitURL = "http://54.191.98.63/report.php";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.APViewReportsProfit);

            //Logout Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutViewRprtProfitButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Load up List
            JsonValue json = await JsonParsing<Task<JsonValue>>.FetchDataAsync(profitURL);
            List<AP_VR_ProfitList> parsedData = JsonParsing<AP_VR_ProfitList>.ParseAndDisplay(json);
            mItems = parsedData;
            mListView = FindViewById<ListView>(Resource.Id.ProfitReportListView);
            parsedData.Insert(0, (new AP_VR_ProfitList() { dishName = "Name", profit = "Earned", date= "Time"}));

            AP_VR_ProfitListViewAdapter adapter = new AP_VR_ProfitListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;


            //Tabbed Buttons
            this.btns = new int[]
            {
                Resource.Id.APVRProfitDaily,
                Resource.Id.APVRProfitWeekly,
                Resource.Id.APVRProfitMonthly,
                Resource.Id.APVRProfitYearly
            }.Select(FindViewById<Button>).ToArray();

            //Daily Report
            btns[0].Click += async (sender, e) =>
            {
                JsonValue jsondaily = await JsonParsing<Task<JsonValue>>.FetchDataAsync(profitURL);
                List<AP_VR_ProfitList> dailyparsedData = JsonParsing<AP_VR_ProfitList>.ParseAndDisplay(jsondaily);
                mItems = dailyparsedData;
                mListView = FindViewById<ListView>(Resource.Id.ProfitReportListView);
                dailyparsedData.Insert(0, (new AP_VR_ProfitList() { dishName = "Name", profit = "Earned", date = "Time" }));

                AP_VR_ProfitListViewAdapter dailyadapter = new AP_VR_ProfitListViewAdapter(this, dailyparsedData);
                mListView.Adapter = dailyadapter;
            };

            //Weekly Report
            btns[1].Click += async (sender, e) =>
            {
                JsonValue jsonweekly = await JsonParsing<Task<JsonValue>>.FetchDataAsync(profitURL);
                List<AP_VR_ProfitList> weeklyparsedData = JsonParsing<AP_VR_ProfitList>.ParseAndDisplay(jsonweekly);
                mItems = weeklyparsedData;
                mListView = FindViewById<ListView>(Resource.Id.ProfitReportListView);
                weeklyparsedData.Insert(0, (new AP_VR_ProfitList() { dishName = "Name", profit = "Earned", date = "Time" }));

                AP_VR_ProfitListViewAdapter weeklyadapter = new AP_VR_ProfitListViewAdapter(this, weeklyparsedData);
                mListView.Adapter = weeklyadapter;
            };

            //Monthly Report
            btns[2].Click += async (sender, e) =>
            {
                JsonValue jsonmonthly = await JsonParsing<Task<JsonValue>>.FetchDataAsync(profitURL);
                List<AP_VR_ProfitList> monthlyparsedData = JsonParsing<AP_VR_ProfitList>.ParseAndDisplay(jsonmonthly);
                mItems = monthlyparsedData;
                mListView = FindViewById<ListView>(Resource.Id.ProfitReportListView);
                monthlyparsedData.Insert(0, (new AP_VR_ProfitList() { dishName = "Name", profit = "Earned", date = "Time" }));

                AP_VR_ProfitListViewAdapter monthlyadapter = new AP_VR_ProfitListViewAdapter(this, monthlyparsedData);
                mListView.Adapter = monthlyadapter;
            };

            //Yearly Report
            btns[3].Click += async (sender, e) =>
            {
                JsonValue jsonyearly = await JsonParsing<Task<JsonValue>>.FetchDataAsync(profitURL);
                List<AP_VR_ProfitList> yearlyparsedData = JsonParsing<AP_VR_ProfitList>.ParseAndDisplay(jsonyearly);
                mItems = yearlyparsedData;
                mListView = FindViewById<ListView>(Resource.Id.ProfitReportListView);
                yearlyparsedData.Insert(0, (new AP_VR_ProfitList() { dishName = "Name", profit = "Earned", date = "Time" }));

                AP_VR_ProfitListViewAdapter yearlyadapter = new AP_VR_ProfitListViewAdapter(this, yearlyparsedData);
                mListView.Adapter = yearlyadapter;
            };
        }
    }
}