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
using Android.Support.V7.Widget;


namespace Munch
{
    [Activity(Label = "CV2_Menu", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class CV2_Menu : Activity
    {
        //For the cards
        public RecyclerView mRecyclerView;
        public RecyclerView.LayoutManager mLayoutManager;
        public CVMItemListAdapter mAdapter;
        public AP_EM_ItemList mItemList;
        public string menuURL = "http://54.191.98.63/menu.php";
        
        //Animation Views
        //Set up splash screen as a view
        View mView;
        //Set up fab to disappear
        View fView;
        View headerView;
        View frameView;



        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CV_Menu);
            //Set up Pubnub
            JsonValue json = await JsonParsing<Task<JsonValue>>.FetchDataAsync(menuURL);
            List<EMItemList> parsedData = JsonParsing<EMItemList>.ParseAndDisplay(json);
            AP_EM_ItemList.mBuiltInCards = parsedData.ToArray();

            //Associations
            mView = FindViewById(Resource.Id.customerspalsh);
            fView = FindViewById(Resource.Id.CV2Menufab);
            headerView = FindViewById(Resource.Id.cv_menuhead);
            frameView = FindViewById(Resource.Id.cv_framerv);

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.CV2Menufab);
            FindViewById<FloatingActionButton>(Resource.Id.CV2Menufab).Click += (sender, e) =>
            {
                Hide();
                fab.Visibility = ViewStates.Gone;
            };

            //Create Menu List
            mItemList = new AP_EM_ItemList();
            //Set up layout manager to view all cards on recycler view
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.cv_menurecyclerView);
            mRecyclerView.HasFixedSize = true;
            mLayoutManager = new StaggeredGridLayoutManager(2, 1);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            //Menu List Adapter
            mAdapter = new CVMItemListAdapter(mItemList);
            //Put adapter into RecyclerView
            mRecyclerView.SetAdapter(mAdapter);
            mRecyclerView.SetItemAnimator(new DefaultItemAnimator());
            //Item Click
            mAdapter.ItemClick += OnItemClick;

            Button orderbtn = FindViewById<Button>(Resource.Id.menuorderBtn);
            orderbtn.Click += (s, o) => StartActivity(typeof(CV2_YourOrder));


        }

        //Reveal Animation
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
            anim.AnimationEnd += (sender, e) => { mView.Visibility = ViewStates.Gone; };
            anim.Start();
        }

        public static EMItemList dishName_to_order;
        void OnItemClick(object sender, int position)
        {
            dishName_to_order = mItemList[position];
            dialog_Cusomer_Add_Item_Order manageinventoryDialog = new dialog_Cusomer_Add_Item_Order();
            manageinventoryDialog = new dialog_Cusomer_Add_Item_Order();

            Android.App.FragmentTransaction mtransaction = FragmentManager.BeginTransaction();
            manageinventoryDialog.Show(mtransaction, "diag diag");
        }

        //Item Container
        public class CVMItemListHolder : RecyclerView.ViewHolder
        {
            public TextView Name { get; private set; }
            public TextView Description { get; private set; }
            public TextView ItemCalorie { get; private set; }
            public TextView ItemPrice { get; private set; }

            public CVMItemListHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Name = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Title);
                Description = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Description);
                ItemCalorie = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Calorie);
                ItemPrice = itemView.FindViewById<TextView>(Resource.Id.Menu_Item_Price);

                itemView.Click += (sender, e) => listener(Position);
            }
        }

        //Create Cards
        public class CVMItemListAdapter : RecyclerView.Adapter
        {
            //Event handler for item clicks
            public event EventHandler<int> ItemClick;
            //Data Set
            public AP_EM_ItemList mItemList;
            //start method to load adapter
            public CVMItemListAdapter(AP_EM_ItemList itemitem)
            {
                mItemList = itemitem;
            }

            //Create the Card
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                //Inflate all items for the card
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CVCardView, parent, false);
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
                ih.ItemCalorie.Text = mItemList[position].iCalorie;
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
}