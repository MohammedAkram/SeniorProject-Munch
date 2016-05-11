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
    [Activity(Label = "APMIActivity",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]


    public class AP_MI_Activity : Activity
    {

        public override void OnBackPressed()
        {
            StartActivity(typeof(AdminPortal));
        }

        // create the dataTable objects to store the json table data.

        private List<AP_MI_InventoryList> mItems;
        //List
        public ListView mListView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.APManageInventory);
            EditText name = FindViewById<EditText>(Resource.Id.txtName1);
            EditText unit = FindViewById<EditText>(Resource.Id.txtUnit1);
            EditText quant = FindViewById<EditText>(Resource.Id.txtQuantity1);
            EditText thres = FindViewById<EditText>(Resource.Id.txtMinThreshold);


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
            JsonValue json = await JsonParsing<Task<JsonValue>>.FetchDataAsync(inventoryURL);
            List<AP_MI_InventoryList> parsedData = JsonParsing<AP_MI_InventoryList>.ParseAndDisplay(json);
            mItems = parsedData;
            mListView = FindViewById<ListView>(Resource.Id.mngInventoryListView);
            parsedData.Insert(0, (new AP_MI_InventoryList() { Ingredients = "Name", Quantity = "Quantity", MeasureUnit = "Units", Threshold = "Minimum Quantity" }));

            AP_MI_ListViewAdapter adapter = new AP_MI_ListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;
            mListView.ItemLongClick += mListView_ItemLongClick;

            //Swipe to refresh
            var refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.swipecontainerAPMI);
            refresher.SetColorScheme(Resource.Color.primary, Resource.Color.accent_pressed, Resource.Color.ripple, Resource.Color.primary_pressed);
            refresher.Refresh += HandleRefresh;

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
        //Swipe to Refresh Activity
        void HandleRefresh(object sender, EventArgs e)
        {
            StartActivity(typeof(AP_MI_Activity));
        }

        public static String xcc;
        void mListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            xcc = mItems[e.Position].Ingredients;
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_AP_Manage_InventoryEdit manageInventory = new dialog_AP_Manage_InventoryEdit();
            manageInventory.Show(transaction, "dialog fragment");
            //Edit button starts action
            manageInventory.editItemComplete += manageinventoryDialog_addItemComplete;
            //Delete button starts action
            manageInventory.deleteItemComplete += manageinventoryDialog_deleteItemComplete;
        }

        

        void manageinventoryDialog_addItemComplete(object sender, OnSignEventArgs_InventoryManagement e)
        {
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
        }

        void manageinventoryDialog_deleteItemComplete(object sender, OnSignEventArgs_InventoryManagement e)
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



        //Edit Button
        //Action to run for edit item button in dialog
        void manageAccountDialog_editItemComplete(object send, OnSignEventArgs_InventoryManagementEdit e)
        {
            Thread thread = new Thread(EditRequest);
            thread.Start();
        }
        //Thread to run for edit item

        private void EditRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Item Edited", Android.Widget.ToastLength.Short));
            StartActivity(typeof(AP_MI_Activity));
        }


        //Action to run for edit item button in dialog
        void manageAccountDialog_deleteItemComplete(object send, dialog_AP_Manage_InventoryEdit e)
        {
            Thread thread = new Thread(deleteRequest);
            thread.Start();
        }
        //Thread to run for edit item
        private void deleteRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Item Deleted", Android.Widget.ToastLength.Long));
            StartActivity(typeof(AP_MI_Activity));
        }
    }
}
