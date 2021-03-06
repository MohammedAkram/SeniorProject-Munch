using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubNubMessaging.Core;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Threading.Tasks;

namespace Munch
{
    [Activity(Label = "CustomerPortal", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorPortrait)]
    public class CustomerPortal : FragmentActivity
    {

        #region "Pubnub"
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
        #endregion

        public static List<CustomerOrderItem> CustomerOrderList = new List<CustomerOrderItem>();

        public ListView mListView;

        //For the Sliding Tabs
        private ViewPager mViewPager;
        private CV_SlidingTabScrollView mScrollView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerPortal);

            //pubnub shit

            pubnub.Subscribe<string>(
                LoginScreen.loginUsername,
                DisplaySubscribeReturnMessage,
                DisplaySubscribeConnectStatusMessage,
                DisplayErrorMessage
                );


            string menuURL = "http://54.191.98.63/menu.php";
            JsonValue json = await JsonParsing<Task<JsonValue>>.FetchDataAsync(menuURL);
            List<EMItemList> parsedData = JsonParsing<EMItemList>.ParseAndDisplay(json);
            AP_EM_ItemList.mBuiltInCards = parsedData.ToArray();

            //mListView = FindViewById<ListView>(Resource.Id.mngYour_Order_ListView);

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
            mFragmentHolder.Add(new Menu());
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



        /*
       *************************************************************************
       *************************************************************************
       *************************************************************************
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

                        Console.WriteLine("!!~!!~!!~!!~!!~!!~!!" + s2);

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
        *************************************************************************
        *************************************************************************
        *************************************************************************
        */





        public Button helpButton;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {


            var view = inflater.Inflate(Resource.Layout.CV_Welcome, container, false);

            helpButton = view.FindViewById<Button>(Resource.Id.Welcom_Call_Waiter);
            helpButton.Click += (sender, e) =>
            {
                pubnub.Publish<string>(LoginScreen.loginUsername, LoginScreen.loginUsername + ": Requires Assistance, " + LoginScreen.loginUsername + " requires your assistance", DisplayReturnMessage, DisplayErrorMessage);
            };

            return view;

        }



        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Welcome";
        }
    }

    //Menu
    public class Menu : Android.Support.V4.App.Fragment
    {
        //For the cards
        public RecyclerView mRecyclerView;
        public RecyclerView.LayoutManager mLayoutManager;
        public CVMItemListAdapter mAdapter;
        public AP_EM_ItemList mItemList;
        public string menuURL = "http://54.191.98.63/menu.php";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Menu, container, false);
            //Create Menu List
            mItemList = new AP_EM_ItemList();
            //Set up layout manager to view all cards on recycler view
            mRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.cv_menurecyclerView);
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
            return view;
        }


        void OnItemClick(object sender, int position)
        {
            /*
            dishName_to_order = mItemList[position];
            dialog_Cusomer_Add_Item_Order manageinventoryDialog = new dialog_Cusomer_Add_Item_Order();
            manageinventoryDialog = new dialog_Cusomer_Add_Item_Order();
            Android.App.FragmentTransaction ttransaction = FragmentManager.BeginTransaction();
            manageinventoryDialog.Show(ttransaction, "dialog fragment");
            */

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

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Menu";
        }
    }

    //Your Order
    public class Your_Order : Android.Support.V4.App.Fragment
    {
        public ListView mListView;
        private List<CustomerOrderItem> mItems;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Your_Order, container, false);

            mListView = view.FindViewById<ListView>(Resource.Id.mngYour_Order_ListView);

            //CustomerPortal.CustomerOrderList.Insert(0, (new CustomerOrderItem() { Dish = "dishname", Quantity = "test quant", Notes = "test note", OrderNumber = "" }));
            //  CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = Menu.dishName_to_order, Quantity = "testq", Notes = "testn", OrderNumber = "" });
            // CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = "huhui", Quantity = "testq", Notes = "testn", OrderNumber = "" });
            // CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = "food", Quantity = "testq", Notes = "testn", OrderNumber = "" });


            CV_your_Order_ListViewAdapter adapter = new CV_your_Order_ListViewAdapter(this.Activity, (CustomerPortal.CustomerOrderList));
            Console.Out.WriteLine(CustomerPortal.CustomerOrderList + "******************************");
            mListView.Adapter = adapter;
            // adapter.NotifyDataSetChanged();

            return view;


        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Your Order";
        }
    }
}