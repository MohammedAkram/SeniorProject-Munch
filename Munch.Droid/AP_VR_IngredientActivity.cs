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
using System.Json;
using System.Threading.Tasks;

namespace Munch
{
    [Activity(Label = "AP_VR_IngredientActivity", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class AP_VR_IngredientActivity : Activity
    {
        private List<AP_VR_IngredientList> mItems;
        public ListView mListView;
        public Button[] btns;
        public String IngredientURLD = "http://54.191.98.63/report.php?type=DailyIngredients";//daily
        public String IngredientURLW = "http://54.191.98.63/report.php?type=WeeklyIngredients";//weekly
        public String IngredientURLM = "http://54.191.98.63/report.php?type=MonthlyIngredients";//monthly
        public String IngredientURLY = "http://54.191.98.63/report.php?type=YearlyIngredients";//yearly

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.APViewReportsIngredients);

            //Logout Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutIngredientViewRprtButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Load up List
            JsonValue json = await JsonParsing<Task<JsonValue>>.FetchDataAsync(IngredientURLY);
            List<AP_VR_IngredientList> parsedData = JsonParsing<AP_VR_IngredientList>.ParseAndDisplay(json);
            mItems = parsedData;
            mListView = FindViewById<ListView>(Resource.Id.IngredientReportListView);
            parsedData.Insert(0, (new AP_VR_IngredientList() { Ingredients = "Name", MeasureUnit = "Measurement Unit", used = "Amount Used", DATE = "Time" }));

            AP_VR_IngredientListViewAdapter adapter = new AP_VR_IngredientListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;
            Console.WriteLine(parsedData + "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%5");
            //Tabbed Buttons
            this.btns = new int[]
            {
                Resource.Id.APVRIngredientDaily,
                Resource.Id.APVRIngredientWeekly,
                Resource.Id.APVRIngredientMonthly,
                Resource.Id.APVRIngredientYearly
            }.Select(FindViewById<Button>).ToArray();

            //Daily Report
            btns[0].Click += async (sender, e) =>
            {
                JsonValue jsondaily = await JsonParsing<Task<JsonValue>>.FetchDataAsync(IngredientURLD);
                List<AP_VR_IngredientList> dailyparsedData = JsonParsing<AP_VR_IngredientList>.ParseAndDisplay(jsondaily);
                mItems = dailyparsedData;
                mListView = FindViewById<ListView>(Resource.Id.IngredientReportListView);
                dailyparsedData.Insert(0, (new AP_VR_IngredientList() { Ingredients = "Name", MeasureUnit = "Measurement Unit", used = "Amount Used", DATE = "Time" }));

                AP_VR_IngredientListViewAdapter dailyadapter = new AP_VR_IngredientListViewAdapter(this, dailyparsedData);
                mListView.Adapter = dailyadapter;
            };

            //Weekly Report
            btns[1].Click += async (sender, e) =>
            {
                JsonValue jsonweekly = await JsonParsing<Task<JsonValue>>.FetchDataAsync(IngredientURLW);
                List<AP_VR_IngredientList> weeklyparsedData = JsonParsing<AP_VR_IngredientList>.ParseAndDisplay(jsonweekly);
                mItems = weeklyparsedData;
                mListView = FindViewById<ListView>(Resource.Id.IngredientReportListView);
                weeklyparsedData.Insert(0, (new AP_VR_IngredientList() { Ingredients = "Name", MeasureUnit = "Measurement Unit", used = "Amount Used", DATE = "Time" }));

                AP_VR_IngredientListViewAdapter weeklyadapter = new AP_VR_IngredientListViewAdapter(this, weeklyparsedData);
                mListView.Adapter = weeklyadapter;
            };

            //Monthly Report
            btns[2].Click += async (sender, e) =>
            {
                JsonValue jsonmonthly = await JsonParsing<Task<JsonValue>>.FetchDataAsync(IngredientURLM);
                List<AP_VR_IngredientList> monthlyparsedData = JsonParsing<AP_VR_IngredientList>.ParseAndDisplay(jsonmonthly);
                mItems = monthlyparsedData;
                mListView = FindViewById<ListView>(Resource.Id.IngredientReportListView);
                monthlyparsedData.Insert(0, (new AP_VR_IngredientList() { Ingredients = "Name", MeasureUnit = "Measurement Unit", used = "Amount Used", DATE = "Time" }));

                AP_VR_IngredientListViewAdapter monthlyadapter = new AP_VR_IngredientListViewAdapter(this, monthlyparsedData);
                mListView.Adapter = monthlyadapter;
            };

            //Yearly Report
            btns[3].Click += async (sender, e) =>
            {
                JsonValue jsonyearly = await JsonParsing<Task<JsonValue>>.FetchDataAsync(IngredientURLY);
                List<AP_VR_IngredientList> yearlyparsedData = JsonParsing<AP_VR_IngredientList>.ParseAndDisplay(jsonyearly);
                mItems = yearlyparsedData;
                mListView = FindViewById<ListView>(Resource.Id.IngredientReportListView);
                yearlyparsedData.Insert(0, (new AP_VR_IngredientList() { Ingredients = "Name", MeasureUnit = "Measurement Unit", used = "Amount Used", DATE = "Time" }));

                AP_VR_IngredientListViewAdapter yearlyadapter = new AP_VR_IngredientListViewAdapter(this, yearlyparsedData);
                mListView.Adapter = yearlyadapter;
            };

        }
    }
}