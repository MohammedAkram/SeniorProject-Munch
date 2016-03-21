using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace Munch
{
    [Activity(Label = "CustomerPortal",  Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class CustomerPortal : FragmentActivity
    {
        //For the Sliding Tabs
        private ViewPager mViewPager;
        private CV_SlidingTabScrollView mScrollView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerPortal);
            
            //For Sliding Tabs
            mScrollView = FindViewById<CV_SlidingTabScrollView>(Resource.Id.CV_sliding_tab);
            mViewPager = FindViewById<ViewPager>(Resource.Id.CV_viewPager);
            mViewPager.Adapter = new MenuPagerAdapter(SupportFragmentManager);
            mScrollView.ViewPager = mViewPager;
        }
    }

    //Populates the ScrollView with tabs and references for activities per tab
    public class MenuPagerAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> mFragmentHolder;

        public MenuPagerAdapter(Android.Support.V4.App.FragmentManager fragManager) : base(fragManager)
        {
            mFragmentHolder = new List<Android.Support.V4.App.Fragment>();
            mFragmentHolder.Add(new Welcome());
            mFragmentHolder.Add(new Breakfast());
            mFragmentHolder.Add(new Lunch());
            mFragmentHolder.Add(new Dinner());
            mFragmentHolder.Add(new Your_Order());
        }

        public override int Count
        {
            get { return mFragmentHolder.Count; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return mFragmentHolder[position];
        }
    }

    //Welcome Splash Screen
    public class Welcome : Android.Support.V4.App.Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Welcome, container, false);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Welcome";
        }
    }

    //Breakfast Menu
    public class Breakfast : Android.Support.V4.App.Fragment
    {
        //For the cards
        public RecyclerView mRecyclerView;
        public RecyclerView.LayoutManager LayoutManager;
        public Munch.CVBFItemListAdapter mAdapter;
        public AP_EM_ItemList mItemList;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Breakfast, container, false);

            mItemList = new AP_EM_ItemList();
            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.cv_breakfastrecyclerView);
            LayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(LayoutManager);
            mAdapter = new Munch.CVBFItemListAdapter(mItemList);
            mRecyclerView.SetAdapter(mAdapter);

            return view;
        }

        //Create Cards
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

        //Item Container
        public class CVBFItemListHolder : RecyclerView.ViewHolder
        {
            public TextView Name { get; private set; }
            public TextView Description { get; private set; }
            public TextView Ingredients { get; private set; }
            public TextView ItemCalorie { get; private set; }
            public TextView ItemCost { get; private set; }
            public TextView ItemPrice { get; private set; }

            public CVBFItemListHolder(View itemView, Action<int> listener) : base(itemView)
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

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Breakfast";
        }
    }

    //Lunch Menu
    public class Lunch : Android.Support.V4.App.Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Lunch, container, false);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Lunch";
        }
    }

    //Dinner Menu
    public class Dinner : Android.Support.V4.App.Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Dinner, container, false);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Dinner";
        }
    }

    //Your Order
    public class Your_Order : Android.Support.V4.App.Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Your_Order, container, false);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Your Order";
        }
    }
}