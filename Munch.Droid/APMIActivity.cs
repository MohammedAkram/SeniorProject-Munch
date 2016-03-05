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

namespace Munch
{
    [Activity(Label = "APMIActivity",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]

    public class APMIActivity : Activity
    {

        //List
        private List<APMIInventoryList> mItems;
        public ListView mListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.APManageInventory);
            EditText name = FindViewById<EditText>(Resource.Id.txtName1);
            EditText unit = FindViewById<EditText>(Resource.Id.txtUnit1);
            EditText quant = FindViewById<EditText>(Resource.Id.txtQuantity1);

            //Log Out Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutMngInvtryButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Load Up List
            mListView = FindViewById<ListView>(Resource.Id.mngInventoryListView);
            mItems = new List<APMIInventoryList>();
            mItems.Add(new APMIInventoryList() { Name = "Name", Description = "Description", Quantity = "Quantity", Price = "Price" });
            mItems.Add(new APMIInventoryList() { Name = "fuck", Description = "what the fuck", Quantity = "2", Price = "43.23" });
            mItems.Add(new APMIInventoryList() { Name = "FUCKING", Description = "FACK", Quantity = "0", Price = "14.23" });
            mItems.Add(new APMIInventoryList() { Name = "FACK", Description = "FUCKING", Quantity = "2", Price = "43.23" });
            
            APMIListViewAdapter adapter = new APMIListViewAdapter(this, mItems);
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

            Thread.Sleep(200);
           // String url = "http://54.191.139.104/addinventory.php?name=beef&&unit=lb&&quantity=10";
            //var webClient = new WebClient();
            //webClient.DownloadString("http://54.191.139.104/addinventory.php?name=beef&&unit=lb&&quantity=10");
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            /* string URI = "http://54.191.139.104/addinventory.php?";
             string myParameters = "name=beef&&unit=lb&&quantity=10";
             using (WebClient wc = new WebClient())
             {
                 wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                 string HtmlResult = wc.UploadString(URI, myParameters);
             }*/
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Dialog Opened", Android.Widget.ToastLength.Short).Show());
            }

        }
    }
