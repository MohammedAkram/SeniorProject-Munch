using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PubNubMessaging.Core;
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
    [Activity(Label = "CV2_Menu", Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorPortrait)]
    public class CV2_Menu : Activity
    {
        //For the cards
        public RecyclerView mRecyclerView;
        public RecyclerView.LayoutManager mLayoutManager;
        public CVMItemListAdapter mAdapter;
        public AP_EM_ItemList mItemList;
        public string menuURL = "http://54.191.98.63/customermenu.php";

        //Animation Views
        //Set up splash screen as a view
        View mView;
        //Set up fab to disappear
        View fView;
        View headerView;
        View frameView;

        #region "pubnub"
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

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CV_Menu);

            #region "logout"
            //logout button
            Button logout = FindViewById<Button>(Resource.Id.menuLogOut);
            logout.Click += (sender, e) => {

                EditText input = new EditText(this);
                RunOnUiThread(() =>
                {
                    AlertDialog.Builder builder;
                    builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Logout?");
                    builder.SetMessage("Enter the password associated with " + LoginScreen.loginUsername + " to logout");
                    builder.SetCancelable(false);
                    builder.SetView(input);
                    builder.SetPositiveButton("OK", delegate {

                        if (input.Text == ("lmo"))
                        {
                            var webClient = new WebClient();
                            Console.WriteLine();
                            webClient.DownloadString("http://54.191.98.63/logout.php?id=" + LoginScreen.loginUsername);
                            this.Finish();
                            SetContentView(Resource.Layout.LoginScreen);
                            Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                            StartActivity(typeof(LoginScreen));
                        }
                        else
                        {
                            Android.Widget.Toast.MakeText(this, "Incorrect Password", Android.Widget.ToastLength.Short).Show();
                        }


                    });
                    builder.Show();
                }
                );




                /*
                var webClient = new WebClient();
                Console.WriteLine();
                webClient.DownloadString("http://54.191.98.63/logout.php?id=" + LoginScreen.loginUsername);
                this.Finish();
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
                */
            };
            #endregion

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

            mAdapter.NotifyDataSetChanged();

            Button orderbtn = FindViewById<Button>(Resource.Id.menuorderBtn);
            Button callbtn = FindViewById<Button>(Resource.Id.menuCallWaiter);
            Button pay = FindViewById<Button>(Resource.Id.menupayButton);
            pay.Click += (s, o) =>
            {
                RunOnUiThread(() =>
                {
                    AlertDialog.Builder builder;
                    builder = new AlertDialog.Builder(this);
                    builder.SetTitle("How would you like to pay?");
                    builder.SetMessage("Choose from the 3 options below:\n• Single Check: One check with everything ordered.\n• Split Evenly: Splits the check evenly.\n• Split by Dish: Pick who pays for what dish.");
                    builder.SetCancelable(true);
                    
                    builder.SetPositiveButton("Split by Dish", delegate {
                        StartActivity(typeof(SplitPayment));
                    });
                    builder.SetNeutralButton("Split Evenly", delegate
                    {
                        
                        StartActivity(typeof(EvenPayment));

                    });
                    builder.SetNegativeButton("Single Check", delegate
                    {
                        double subtotal = 0;

                        //Getting subtotal of items ordered
                        foreach (CustomerOrderItem x in CustomerPortal.CustomerOrderList)
                        {
                            int numQty = Int32.Parse(x.Quantity);
                            while (numQty > 0)
                            {
                                double price = Convert.ToDouble(x.Dish.iPrice);
                                subtotal += price;
                                numQty--;
                            }
                        }
                        //Adding tax to subtotal
                        Double TotalCost = Math.Round(((subtotal * .08875) + subtotal), 2);
                        String TotalCostString = Convert.ToString(TotalCost);

                        pubnub.Publish<string>(LoginScreen.loginUsername, "Here's their single check total: $" + TotalCostString, DisplayReturnMessage, DisplayErrorMessage);

                    });
                    builder.Show();
                }
               );






                
                //Android.Widget.Toast.MakeText(this, "Your waiter has been notified that you are ready to pay!", Android.Widget.ToastLength.Long).Show();
                //pubnub.Publish<string>(LoginScreen.loginUsername, LoginScreen.loginUsername + ": Requires Assistance, " + LoginScreen.loginUsername + " Ready To Pay", DisplayReturnMessage, DisplayErrorMessage);
            };
            orderbtn.Click += (s, o) => StartActivity(typeof(CV2_YourOrder));
            callbtn.Click += (s, o) => {
                //Sends a message to the table's channel with the help message. 
                Android.Widget.Toast.MakeText(this, "Your waiter has been notified that you need help!", Android.Widget.ToastLength.Long).Show();
                pubnub.Publish<string>(LoginScreen.loginUsername, LoginScreen.loginUsername + ": Requires Assistance, " + LoginScreen.loginUsername + " requires your assistance", DisplayReturnMessage, DisplayErrorMessage);
            };


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

        public override void OnBackPressed()
        {

        }

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