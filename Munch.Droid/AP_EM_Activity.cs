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
    [Activity(Label = "AP_EM_Activity", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class AP_EM_Activity : Activity
    {

        public override void OnBackPressed()
        {
            StartActivity(typeof(AdminPortal));
        }
        // Loads the Cards
        RecyclerView mRecyclerView;
        // Layout manager that shows the cards in RecyclerView
        RecyclerView.LayoutManager mLayoutManager;
        // Adapter for access to data
        ItemListAdapter mAdapter;
        // array list managed by adapter
        AP_EM_ItemList mItemList;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Create Menu List
            mItemList = new AP_EM_ItemList();
            //Set View
            SetContentView(Resource.Layout.APEditMenu);
            //Set up layout manager to view all cards on recycler view
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);

            //Menu List Adapter
            mAdapter = new ItemListAdapter(mItemList);
            //Item Long Click
            mAdapter.ItemClick += OnItemClick;
            //Put adapter into RecyclerView
            mRecyclerView.SetAdapter(mAdapter);

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
        

        void OnItemClick (object sender, int position)
        {
            //Edit and Delete Dialogs????
            Toast.MakeText(this, "Edit and Delete Dialogs Go Here", ToastLength.Short).Show();
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

        public ItemListHolder (View itemView, Action<int> listener) : base(itemView)
        {
            Name = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Title);
            Description = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Description);
            Ingredients = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Ingredients);
            ItemCalorie = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Calorie);
            ItemCost = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Cost);
            ItemPrice = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Price);

            itemView.Click += (sender, e) => listener(base.Position);
        }
    }

    public class ItemListAdapter : RecyclerView.Adapter
    {
        //Event handler for item clicks
        public event EventHandler<int> ItemClick;
        //Data Set
        public AP_EM_ItemList mItemList;
        //start method to load adapter
        public ItemListAdapter (AP_EM_ItemList itemitem)
        {
            mItemList = itemitem;
        }

        //Create the Card
        public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
        {
            //Inflate all items for the card
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.APEMCardView, parent, false);
            // Viewholder that holds references to the card
            ItemListHolder ih = new ItemListHolder(itemView, OnClick);
            return ih;
        }

        // Fill in the card with whatever
        public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
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
        void OnClick (int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
    }
}