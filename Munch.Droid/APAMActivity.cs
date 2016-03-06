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


namespace Munch
{
    [Activity(Label = "APMAActivity",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]

    public class APMAActivity : Activity
    {

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

            mListView = FindViewById<ListView>(Resource.Id.accntMgmtListView);
            parsedData.Insert(0, (new APAMAccountList() { idAccounts = "Account Type", Level = "Username"}));

            APAMListViewAdapter adapter = new APAMListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.APAMfab);
            fab.AttachToListView(mListView);
            fab.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_APAccountManagement manageAccount = new dialog_APAccountManagement();
                manageAccount.Show(transaction, "dialog fragment");

                manageAccount.addItemComplete += manageAccountDialog_addItemComplete; 
            };

        }
        void manageAccountDialog_addItemComplete(object sender, OnSignEventArgs_AccountManagement e)
        {
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
        }
        private void ActLikeRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Account Added", Android.Widget.ToastLength.Short).Show());
            StartActivity(typeof(APMAActivity));
        }
    }
}