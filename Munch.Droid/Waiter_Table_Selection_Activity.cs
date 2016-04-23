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
using PubNubMessaging.Core;

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



        //pubnub
        /*
*************************************************************************
*************************************************************************
********************************PUBNUB***********************************
*************************************************************************
*************************************************************************
*/
        Pubnub pubnub = new Pubnub("pub-c-ddf91c9e-baf7-47af-8ca8-276337355d46", "sub-c-d70d769c-ebda-11e5-8112-02ee2ddab7fe");
        void DisplaySubscribeReturnMessage(string result)
        {
            Console.WriteLine("SUBSCRIBE REGULAR CALLBACK: ");
            Console.WriteLine(result);

            if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(result.Trim()))
            {
                List<object> deserializedMessage = pubnub.JsonPluggableLibrary.DeserializeToListOfObject(result);
                if (deserializedMessage != null && deserializedMessage.Count > 0)
                {
                    object subscribedObject = (object)deserializedMessage[0];
                    if (subscribedObject != null)
                    {
                        //IF CUSTOM OBJECT IS EXCEPTED, YOU CAN CAST THIS OBJECT TO YOUR CUSTOM CLASS TYPE
                        string resultActualMessage = pubnub.JsonPluggableLibrary.SerializeToJsonString(subscribedObject);
                        string re = resultActualMessage.Replace('"', ' ');
                        string s1 = re.Substring(1, re.IndexOf(',') - 1);
                        string s2 = re.Substring((re.IndexOf(',') + 1));



                        Notification.Builder builder = new Notification.Builder(this)
                        .SetContentTitle(s1)
                        .SetContentText(s2)
                        .SetPriority(2)
                        .SetColor(2)
                        .SetVibrate(new long[1])
                        .SetSmallIcon(Resource.Drawable.Icon);

                        // Build the notification:
                        Notification notification = builder.Build();

                        // Get the notification manager:
                        NotificationManager notificationManager =
                            GetSystemService(Context.NotificationService) as NotificationManager;

                        // Publish the notification:
                        const int notificationId = 0;
                        notificationManager.Notify(notificationId, notification);


                    }
                }
            }
        }
        void DisplaySubscribeConnectStatusMessage(string result)
        {
            Console.WriteLine("SUBSCRIBE CONNECT CALLBACK");
        }
        void DisplayErrorMessage(PubnubClientError pubnubError)
        {
            Console.WriteLine(pubnubError.StatusCode);
        }
        void DisplayReturnMessage(string result)
        {
            Console.WriteLine("PUBLISH STATUS CALLBACK");
            Console.WriteLine(result);
        }



        /*
        *************************************************************************
        *************************************************************************
        ****************************END OF PUBNUB********************************
        *************************************************************************
        *************************************************************************
        */


        protected override async void OnCreate(Bundle bundle)
        {

            

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Waiter_Table_Selection);
            string tableURL = "http://54.191.98.63/tables.php";
            JsonValue json = await JsonParsing<Task<JsonValue>>.FetchDataAsync(tableURL);
            
            List<Waiter_Table_Selection_List> parsedData = JsonParsing<Waiter_Table_Selection_List>.ParseAndDisplay(json);

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
                            WaiterPortal.Selecttable.Add(new WaiterPortal_List() { selectedtable = txtTable.Text });

                            pubnub.Subscribe<string>(
                        txtTable.Text,
                        DisplaySubscribeReturnMessage,
                        DisplaySubscribeConnectStatusMessage,
                        DisplayErrorMessage
                        );
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
                StartActivity(typeof(WaiterPortal));
            };

            this.Finish();
        }
        
    }
}