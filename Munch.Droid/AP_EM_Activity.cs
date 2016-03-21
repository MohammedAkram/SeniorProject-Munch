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
using Android.Support.V7.Widget;

namespace Munch
{
    [Activity(Label = "AP_EM_Activity", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class AP_EM_Activity : Activity
    {

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
        
        //Ingredients Spinner
        public static List<String> ingredientsTransferList = new List<String>();
        //Quantity Spinner
        public static List<String> quantityTransferList = new List<String>();
        // Loads the Cards
        RecyclerView mRecyclerView;
        // Layout manager that shows the cards in RecyclerView
        RecyclerView.LayoutManager mLayoutManager;
        // Adapter for access to data
        CVBFItemListAdapter mAdapter;
        // array list managed by adapter
        AP_EM_ItemList mItemList;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Create Menu List
            mItemList = new AP_EM_ItemList();

            //Set View
            SetContentView(Resource.Layout.APEditMenu);
            Button logout = FindViewById<Button>(Resource.Id.LogOut_Edit_Menu_Button);
            logout.Click += delegate
            {
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Set up layout manager to view all cards on recycler view
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            //Menu List Adapter
            mAdapter = new CVBFItemListAdapter(mItemList);
            //Item Long Click
            mAdapter.ItemClick += OnItemClick;
            //Put adapter into RecyclerView
            mRecyclerView.SetAdapter(mAdapter);

            //Push data to ingredients spinner
            String inventoryURL = "http://54.191.98.63/inventory.php";
            JsonValue json = await FetchInventoryAsync(inventoryURL);
            List<AP_MI_InventoryList> parsedData = ParseAndDisplay(json);

            for (int i = 0; i < parsedData.Count(); i++)
            {
                ingredientsTransferList.Add(parsedData[i].Ingredients.ToString());
            }

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.APEMfab);
            fab.AttachToRecyclerView(mRecyclerView);
            fab.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_AP_Edit_Menu manageMenu = new dialog_AP_Edit_Menu();
                manageMenu = new dialog_AP_Edit_Menu();
                manageMenu.Show(transaction, "dialog fragment");
            };
        }

        void OnItemClick(object sender, int position)
        {
            Button editItem = FindViewById<Button>(Resource.Id.btn_cardModify);

            editItem.Click += delegate
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_AP_EM_Modify manageMenu = new dialog_AP_EM_Modify();
                manageMenu = new dialog_AP_EM_Modify();
                manageMenu.Show(transaction, "dialog fragment");

                manageMenu.editItemComplete += manageMenu_dialog_EM;
            };

            Android.Widget.Toast.MakeText(this, "THE FUCK YOU PRESSING??", Android.Widget.ToastLength.Short).Show();
        }


        void manageMenu_dialog_EM(object sender, OnSignEventArgs_ManageMenu e)
        {
            Thread thread = new Thread(editRequest);
            thread.Start();
        }
        private void editRequest()
        {
            RunOnUiThread(() => Android.Widget.Toast.MakeText(this, "Item Modified", Android.Widget.ToastLength.Short).Show());
            StartActivity(typeof(AP_EM_Activity));
        }

        public override void OnBackPressed()
        {
            StartActivity(typeof(AdminPortal));
        }
    }

    public class ItemListHolder : RecyclerView.ViewHolder
    {
        public TextView Name { get; private set; }
        public TextView Description { get; private set; }
        public TextView Ingredients { get; private set; }
        public TextView ItemCalorie { get; private set; }
        public TextView ItemCost { get; private set; }
        public TextView ItemPrice { get; private set; }

        public ItemListHolder(View itemView, Action<int> listener) : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Title);
            Description = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Description);
            Ingredients = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Ingredients);
            ItemCalorie = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Calorie);
            ItemCost = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Cost);
            ItemPrice = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Price);

            itemView.Click += (sender, e) => listener(Position);

        }
    }

    public class CVBFItemListAdapter : RecyclerView.Adapter
    {
        //Event handler for item clicks
        public event EventHandler<int> ItemClick;
        //Data Set
        public AP_EM_ItemList mItemList;
        //start method to load adapter
        public CVBFItemListAdapter(AP_EM_ItemList itemitem)
        {
            mItemList = itemitem;
        }

        //Create the Card
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Inflate all items for the card
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.APEMCardView, parent, false);
            // Viewholder that holds references to the card
            ItemListHolder ih = new ItemListHolder(itemView, OnClick);
            return ih;
        }

        // Fill in the card with whatever
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ItemListHolder ih = holder as ItemListHolder;
            //Set values
            ih.Name.Text = mItemList[position].iName;
            ih.Description.Text = mItemList[position].iDescription;
            ih.Ingredients.Text = mItemList[position].iIngredients;
            ih.ItemCalorie.Text = mItemList[position].iCalorie;
            ih.ItemCost.Text = mItemList[position].iCost;
            ih.ItemPrice.Text = mItemList[position].iPrice;
        }

        //Get number of items left in the array list
        public override int ItemCount
        {
            get { return mItemList.numItems; }
        }

        // If item is clicked
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}
 