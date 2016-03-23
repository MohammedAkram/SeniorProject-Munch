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
using Android.Support.V4.Widget;

namespace Munch
{
    [Activity(Label = "Waiter_Table_Selection_Activity", 
        Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]

    public class Waiter_Table_Selection_Activity : Activity
    {


        private List<Waiter_Table_Selection_List> mItems;

        private async Task<JsonValue> FetchTablesAsync(string url)
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

        public static string TableURL = "http://54.191.98.63/managetables.php?waiter=" + LoginScreen.loginUsername + "&&tables=";
        public ListView mListView;
        private List<Waiter_Table_Selection_List> ParseAndDisplay(String json)
        {

            List<Waiter_Table_Selection_List> dataTableList = JsonConvert.DeserializeObject<List<Waiter_Table_Selection_List>>(json);
            Console.Out.WriteLine(dataTableList[0].idAccounts);
            Console.Out.WriteLine(dataTableList[0].isTaken);
            Console.Out.WriteLine(dataTableList.Count());
            return dataTableList;
        }

       

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Waiter_Table_Selection);
            string tableURL = "http://54.191.98.63/tables.php";
            JsonValue json = await FetchTablesAsync(tableURL);
            List<Waiter_Table_Selection_List> parsedData = ParseAndDisplay(json);
            mItems = parsedData;
            mListView = FindViewById<ListView>(Resource.Id.Waiter_Table_Selection_ListView);
            

            Waiter_Table_Selection_ListViewAdapter adapter = new Waiter_Table_Selection_ListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;

            
            var fab = FindViewById<FloatingActionButton>(Resource.Id.Waiter_Table_Selection_fab);
            fab.AttachToListView(mListView);
            fab.Click += (object sender, EventArgs args) =>
            
            {
                string strToDB = "";
                for (int i = 0; i < mListView.Count; i++) {
                    Console.WriteLine("list count = " + mListView.Count);
                    View rowView = mListView.GetChildAt(i);
                    if (rowView != null) {
                        TextView txtTable = rowView.FindViewById<TextView>(Resource.Id.Waiter_Table_Selection_Txt_Table);
                        CheckBox checkTable = rowView.FindViewById<CheckBox>(Resource.Id.Waiter_Table_Selection_checkBox);
                        Console.WriteLine(checkTable.Checked);
                        if (checkTable.Checked)
                        {
                            strToDB = strToDB + txtTable.Text.ToString() + ",";
                        }
                    }
                }
                if(strToDB.Length != 0)
                {     
                strToDB = strToDB.Remove(strToDB.Length - 1);
                }
                
                Console.WriteLine("~~~~~~~~~~~~ " + strToDB);

                var webClient = new WebClient();
                Console.WriteLine("~~~~~~~~~~~~ "  + strToDB);
                TableURL = TableURL + strToDB;
                webClient.DownloadString(TableURL);
                this.Finish();
                StartActivity(typeof(AdminPortal));
            };

        }
        
    }
}