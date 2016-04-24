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
using Android.Transitions;
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

        private List<AP_EM_ItemList> mItems;

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

        //Set up add item as a view
        View mView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string menuURL = "http://54.191.98.63/menu.php";
            JsonValue json = await JsonParsing<Task<JsonValue>>.FetchDataAsync(menuURL);
            List<EMItemList> parsedData = JsonParsing<EMItemList>.ParseAndDisplay(json);
            AP_EM_ItemList.mBuiltInCards = parsedData.ToArray();

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
            //Put adapter into RecyclerView
            mRecyclerView.SetAdapter(mAdapter);
            //Item Click
            mAdapter.ItemClick += OnItemClick;

            mView = FindViewById(Resource.Id.fullAPEMAdd);

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.APEMfab);
            fab.AttachToRecyclerView(mRecyclerView);
            FindViewById<FloatingActionButton>(Resource.Id.APEMfab).Click += (sender, e) =>
            {
                StartActivity(typeof(AP_EM_Add));
                OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.right_out);
            };
        }

        //Reveal Animation
        private void Reveal(bool bottom = false)
        {
            mView.Visibility = ViewStates.Visible;
            int cx = 0; 
            int cy = 0;
            if (bottom)
            {
                cx = mView.Right;
                cy = (mView.Top + mView.Bottom);
            }
            else
            {
                cx = (mView.Left + mView.Right) / 2;
                cy = (mView.Top + mView.Bottom) / 2;
            }
            int finalRadius = mView.Width;
            var anim = ViewAnimationUtils.CreateCircularReveal(mView, cx, cy, 0, finalRadius);
            anim.Start();
        }
        private void Hide(bool bottom = false)
        {
            int cx = 0; 
            int cy = 0;
            if (bottom)
            {
                cx = mView.Right;
                cy = (mView.Top + mView.Bottom);
            }
            else
            {
                cx = (mView.Left + mView.Right) / 2;
                cy = (mView.Top + mView.Bottom) / 2;
            }
            int initialRadius = mView.Width;
            var anim = ViewAnimationUtils.CreateCircularReveal(mView, cx, cy, initialRadius, 0);
            anim.AnimationEnd += (sender, e) => { mView.Visibility = ViewStates.Invisible; };
            anim.Start();
        }

        public static EMItemList dishName_to_order;

        void OnItemClick(object sender, int position)
        {

            dialog_AP_EM_Modify modifyMenu = new dialog_AP_EM_Modify();
            modifyMenu = new dialog_AP_EM_Modify();
            dishName_to_order = mItemList[position];

            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            modifyMenu.Show(transaction, "something");
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
 