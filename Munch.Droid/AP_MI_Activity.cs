using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.Apache.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using System.Net;
using System.Threading.Tasks;
using System.Json;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Munch
{
    [Activity(Label = "APMIActivity",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]


    public class AP_MI_Activity : Activity
    {

        public override void OnBackPressed()
        {
            StartActivity(typeof(AdminPortal));
        }

        // create the dataTable objects to store the json table data.


        private async Task<JsonValue> FetchInventoryAsync(string url)
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



        private List<AP_MI_InventoryList> ParseAndDisplay(String json)
        {

            List<AP_MI_InventoryList> dataTableList = JsonConvert.DeserializeObject<List<AP_MI_InventoryList>>(json);
            Console.Out.WriteLine(dataTableList[0].Ingredients);
            Console.Out.WriteLine(dataTableList[0].Quantity);
            Console.Out.WriteLine(dataTableList[0].MeasureUnit);
            Console.Out.WriteLine(dataTableList.Count());
            return dataTableList;
        }

        //List
        public ListView mListView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.APManageInventory);
            EditText name = FindViewById<EditText>(Resource.Id.txtName1);
            EditText unit = FindViewById<EditText>(Resource.Id.txtUnit1);
            EditText quant = FindViewById<EditText>(Resource.Id.txtQuantity1);

            String inventoryURL = "http://54.191.98.63/inventory.php";

            //Log Out Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutMngInvtryButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Load Up List
            //pull the data from the DB and parse it into APMIInventoryList objects 
            JsonValue json = await FetchInventoryAsync(inventoryURL);
            List<AP_MI_InventoryList> parsedData = ParseAndDisplay(json);
            mListView = FindViewById<ListView>(Resource.Id.mngInventoryListView);
            parsedData.Insert(0, (new AP_MI_InventoryList() { Ingredients = "Name", Quantity = "Quantity", MeasureUnit = "Units",}));    

            AP_MI_ListViewAdapter adapter = new AP_MI_ListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.APMIfab);
            fab.AttachToListView(mListView);
            fab.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_APManageInventory manageinventoryDialog = new dialog_APManageInventory();
                manageinventoryDialog.Show(transaction, "dialog fragment");

                manageinventoryDialog.addItemComplete += manageinventoryDialog_addItemComplete;
                
            };
        }

        void manageinventoryDialog_addItemComplete(object sender, OnSignEventArgs e)
        {
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
        }



        private void ActLikeRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Dialog Opened", Android.Widget.ToastLength.Short).Show());
            //refersh the activity so that the last added item appears
            // this needs to be fixed so the screen doesnt flash 
            StartActivity(typeof(AP_MI_Activity));
        }

        }
    }
