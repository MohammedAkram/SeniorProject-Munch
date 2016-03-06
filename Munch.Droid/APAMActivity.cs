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
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]

    public class APMAActivity : Activity
    {
        public override void OnBackPressed()
        {
            StartActivity(typeof(AdminPortal));
    }

    private async Task<JsonValue> FetchAccountsAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON String:
                    return jsonDoc.ToString();
                }
            }
        }


        private List<APAMAccountList> ParseAndDisplay(String json)
        {

            List<APAMAccountList> dataTableList = JsonConvert.DeserializeObject<List<APAMAccountList>>(json);
            Console.Out.WriteLine(dataTableList[0].idAccounts);
            Console.Out.WriteLine(dataTableList[0].Level);
           
            Console.Out.WriteLine(dataTableList.Count());
            return dataTableList;
        }


        //List
        private List<APAMAccountList> mItems;
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
            JsonValue json = await FetchAccountsAsync(accountsURL);
            List<APAMAccountList> parsedData = ParseAndDisplay(json);
            mItems = parsedData;

            mListView = FindViewById<ListView>(Resource.Id.accntMgmtListView);
            parsedData.Insert(0, (new APAMAccountList() { idAccounts = "Account Type", Level = "Username"}));

            APAMListViewAdapter adapter = new APAMListViewAdapter(this, parsedData);
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
            StartActivity(typeof(APMAActivity));
        }

        //Long Click item in ListView
        void mListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
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
            StartActivity(typeof(APMAActivity));
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
            StartActivity(typeof(APMAActivity));
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
            StartActivity(typeof(APMAActivity));
        }
    }
}