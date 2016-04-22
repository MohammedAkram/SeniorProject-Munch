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
    [Activity(Label = "APMAActivity",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]

    public class AP_MA_Activity : Activity
    {
        public override void OnBackPressed()
        {
            StartActivity(typeof(AdminPortal));
    }

        //List
        private List<AP_AM_AccountList> mItems;
        public ListView mListView;

        //Swipe to Refresh
        public SwipeRefreshLayout refresher;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.APAccountManagement);

            //Log Out Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutAccntMgmtButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Load Up List
            String accountsURL = "http://54.191.98.63/accounts.php";
            JsonValue json = await JsonParsing< Task < JsonValue >>.FetchDataAsync(accountsURL);
            List<AP_AM_AccountList> parsedData = JsonParsing<AP_AM_AccountList>.ParseAndDisplay(json);
            mItems = parsedData;

            mListView = FindViewById<ListView>(Resource.Id.accntMgmtListView);
            parsedData.Insert(0, (new AP_AM_AccountList() { idAccounts = "Username", Level = "Account Type"}));

            AP_AM_ListViewAdapter adapter = new AP_AM_ListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;
            //Long click item click
            mListView.ItemLongClick += mListView_ItemLongClick;

            //Swipe to refresh
            var refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.swipecontainerAPAM);
            refresher.SetColorScheme(Resource.Color.primary, Resource.Color.accent_pressed, Resource.Color.ripple, Resource.Color.primary_pressed);
            refresher.Refresh += HandleRefresh;

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.APAMfab);
            fab.AttachToListView(mListView);
            fab.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_APAccountManagement manageAccount = new dialog_APAccountManagement();
                manageAccount.Show(transaction, "dialog fragment");
                //Add button starts action
                manageAccount.addItemComplete += manageAccountDialog_addItemComplete; 
            };

        }

        //Swipe to Refresh Activity
        void HandleRefresh (object sender, EventArgs e)
        {
            StartActivity(typeof(AP_MA_Activity));
        }
        public static String xxc;
        //Long Click item in ListView
        void mListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            xxc = mItems[e.Position].idAccounts;
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_APAccountManagementEdit manageAccount = new dialog_APAccountManagementEdit();
            manageAccount.Show(transaction, "dialog fragment");
            //Edit button starts action
            manageAccount.editItemComplete += manageAccountDialog_editItemComplete;
            //Delete button starts action
            manageAccount.deleteItemComplete += manageAccountDialog_deleteItemComplete;
        }

        //Add Button
        //Action to run for add item button in dialog
        void manageAccountDialog_addItemComplete(object sender, OnSignEventArgs_AccountManagement e)
        {
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
        }
        //Thread to run for add item
        private void ActLikeRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Account Added", Android.Widget.ToastLength.Short).Show());
            StartActivity(typeof(AP_MA_Activity));
        }
        
        //Edit Button
        //Action to run for edit item button in dialog
        void manageAccountDialog_editItemComplete(object send, OnSignEventArgs_AccountManagement e)
        {
            Thread thread = new Thread(EditRequest);
            thread.Start();
        }
        //Thread to run for edit item
        private void EditRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Acccount Edited", Android.Widget.ToastLength.Short));
            StartActivity(typeof(AP_MA_Activity));
        }

        
        //Action to run for edit item button in dialog
        void manageAccountDialog_deleteItemComplete(object send, OnSignEventArgs_AccountManagement e)
        {
            Thread thread = new Thread(deleteRequest);
            thread.Start();
        }
        //Thread to run for edit item
        private void deleteRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Acccount Deleted", Android.Widget.ToastLength.Long));
            StartActivity(typeof(AP_MA_Activity));
        }
    }
}