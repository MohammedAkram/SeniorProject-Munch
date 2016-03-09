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
    [Activity(Label = "AP_Edit_Menu_Activity",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]


    public class AP_Edit_Menu_Activity : Activity
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



        private List<AP_EditMenu_DishList> ParseAndDisplay(String json)
        {

            List<AP_EditMenu_DishList> dataTableList = JsonConvert.DeserializeObject<List<AP_EditMenu_DishList>>(json);
            Console.Out.WriteLine(dataTableList[0].Tab);
            Console.Out.WriteLine(dataTableList[0].Dish);
            Console.Out.WriteLine(dataTableList[0].Ingredient);
            Console.Out.WriteLine(dataTableList[0].CostPrice);
            Console.Out.WriteLine(dataTableList[0].SellPrice);
            Console.Out.WriteLine(dataTableList.Count());
            return dataTableList;
        }

        //List
        public ListView mListView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.APEditMenu);
            EditText Tab = FindViewById<EditText>(Resource.Id.txtTab);
            EditText Dish = FindViewById<EditText>(Resource.Id.txtDish);
            EditText Ingredient = FindViewById<EditText>(Resource.Id.txtIngredient);
            EditText CostPrice = FindViewById<EditText>(Resource.Id.txtCostPrice);
            EditText SellPrice = FindViewById<EditText>(Resource.Id.txtSellPrice);

            String inventoryURL = "http://54.191.98.63/inventory.php";

            //Log Out Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutEditMenuButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Load Up List
            //pull the data from the DB and parse it into APMIInventoryList objects 
            JsonValue json = await FetchInventoryAsync(inventoryURL);
            List<AP_EditMenu_DishList> parsedData = ParseAndDisplay(json);
            mListView = FindViewById<ListView>(Resource.Id.EditMenuListView);
            parsedData.Insert(0, (new AP_EditMenu_DishList() { Tab = "Tab", Dish = "Dish", Ingredient = "Ingredient", CostPrice = "Cost Price", SellPrice = "Sell Price", }));

            AP_EditMenuListViewAdapter adapter = new AP_EditMenuListViewAdapter(this, parsedData);
            mListView.Adapter = adapter;

            mListView.ItemLongClick += mListView_ItemLongClick;

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.APMIfab);
            fab.AttachToListView(mListView);
            fab.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_APEditMenu EditMenuDialog = new dialog_EditMenu();
                manageinventoryDialog.Show(transaction, "dialog fragment");
                manageinventoryDialog.addItemComplete += manageinventoryDialog_addItemComplete;

            };
        }
        //Swipe to Refresh Activity
        void HandleRefresh(object sender, EventArgs e)
        {
            StartActivity(typeof(AP_Edit_Menu_Activity));
        }

        /* void mListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog__AP_Manage_InventoryEdit manageInventory = new dialog__AP_Manage_InventoryEdit();
            manageInventory.Show(transaction, "dialog fragment");
            //Edit button starts action
            manageInventory.editItemComplete += manageinventoryDialog_addItemComplete;
            //Delete button starts action
            manageInventory.deleteItemComplete += manageinventoryDialog_deleteItemComplete;
        }
        */


        void EditMenuDialog_addItemComplete(object sender, OnSignEventArgs_InventoryManagement e)
        {
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
        }

        void EditMenuDialog_deleteItemComplete(object sender, OnSignEventArgs_InventoryManagement e)
        {
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();
        }

        private void ActLikeRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Dialog Opened", Android.Widget.ToastLength.Short).Show());
            //refersh the activity so that the last added item appears
            // this needs to be fixed so the screen doesnt flash 
            StartActivity(typeof(AP_Edit_Menu_Activity));
        }



        //Edit Button
        //Action to run for edit item button in dialog
        void EditMenuDialog_editItemComplete(object send, OnSignEventArgs_InventoryManagementEdit e)
        {
            Thread thread = new Thread(EditRequest);
            thread.Start();
        }
        //Thread to run for edit item

        private void EditRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Acccount Edited", Android.Widget.ToastLength.Short));
            StartActivity(typeof(AP_Edit_Menu_Activity));
        }


        //Action to run for edit item button in dialog
        void EditMenuDialog_deleteItemComplete(object send, dialog_EditMenuEdit e)
        {
            Thread thread = new Thread(deleteRequest);
            thread.Start();
        }
        //Thread to run for edit item
        private void deleteRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Acccount Deleted", Android.Widget.ToastLength.Long));
            StartActivity(typeof(AP_Edit_Menu_Activity));
        }
    }
}
