using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Newtonsoft.Json;
using Android.Graphics.Drawables;
using PubNubMessaging.Core;

namespace Munch
{

    [Activity(Label = "SplitPayment", Theme = "@style/MyTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorPortrait)]

   

    public class SplitPayment : ActionBarActivity
    {

        class Customer
        {
            public int custNum { get; set; }
            public List<EMItemList> order { get; set; }
            public double subtotal { get; set; }

        }


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


        //drawer stuff
        private SupportToolbar mToolbar;
        private MyActionBarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private ArrayAdapter mLeftAdapter;
        private List<String> mLeftDataSet;
        private Button anchorButton;


        // current customer that is dragging orders
        int currentCustomer = 1;
        //list of lists<String> contains every customers order
        List<List<String>> splits = new List<List<string>>();

        // center text view that holds what customer is currently active
        TextView result;

        Dictionary<string, EMItemList> itemsOnMenu = new Dictionary<string, EMItemList>();
        Dictionary<int, Android.Graphics.Color> colorDictionary = new Dictionary<int, Android.Graphics.Color>();
        List<Customer> customerList = new List<Customer>();


        //holds all of the ordered dishes
        List<CustomerOrderItem> orderList = CustomerPortal.CustomerOrderList;

        //holds all of the generated buttons 
        List<Button> btnList = new List<Button>();

        //holds the IDs of each button that was generated
        List<int> idList = new List<int>();


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SplitPaymentScreen);

            //drawer stuff 
            mToolbar = FindViewById<SupportToolbar>(Resource.Id.DToolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
            mLeftDrawer.Tag = 0;
            SetSupportActionBar(mToolbar);


           




            mDrawerToggle = new MyActionBarDrawerToggle(
                this,                           //Host Activity
                mDrawerLayout,                  //DrawerLayout
                Resource.String.openDrawer,     //Opened Message
                Resource.String.closeDrawer     //Closed Message
            );

            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
            mDrawerToggle.SyncState();

            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.openDrawer);
                }

                else
                {
                    SupportActionBar.SetTitle(Resource.String.closeDrawer);
                }
            }

            else
            {

                SupportActionBar.SetTitle(Resource.String.closeDrawer);
            }


            var dropZone = FindViewById<FrameLayout>(Resource.Id.dropzone);
            
            Random randomGen = new Random();
            Android.Graphics.Color color = Android.Graphics.Color.Argb(255, randomGen.Next(256), randomGen.Next(256), randomGen.Next(256));
            dropZone.SetBackgroundColor(color);
            colorDictionary.Add(currentCustomer, color);

            //adds an empty list so the next indexes correspond with the customer number
            splits.Add(new List<String>());
            splits.Add(new List<String>());

            // first button on the list
            anchorButton = FindViewById<Button>(Resource.Id.anchorButton);

            //give it a unique ID
            anchorButton.Id = 123455;

            //the layout that holds all the buttons
            RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.buttonholder);

            

            //adds the ID of the first button
            idList.Add(anchorButton.Id);

            //creates a new layout parameter for the buttons
            List<RelativeLayout.LayoutParams> layoutHolder = new List<RelativeLayout.LayoutParams>();
            RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            lp.AddRule(LayoutRules.CenterHorizontal);

            //used to create a unique ID for each genereated button
            int idGen = 123456;

            /* now this is some cray cray I made that surprisingly works
             * have fun trying to figure it out 
             * just pray that it keeps working  
             */
            foreach (CustomerOrderItem x in CustomerPortal.CustomerOrderList)
            {

                int numQty = Int32.Parse(x.Quantity);
                while (numQty > 0)
                {
                    if (!itemsOnMenu.ContainsKey(x.Dish.iName))
                    {
                        itemsOnMenu.Add(x.Dish.iName, x.Dish);
                    }
                    
                    splits[0].Add(x.Dish.ToString());
                    btnList.Add(new Button(this));
                    layoutHolder.Add(new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent));
                    RelativeLayout.LayoutParams lastLayout = layoutHolder.Last();
                    lastLayout.AddRule(LayoutRules.CenterHorizontal);
                    Button lastButton = btnList.Last();
                    lastButton.Tag =  + numQty;
                    lastLayout.AddRule(LayoutRules.Below, idList.Last());
                    lastButton.LayoutParameters = lastLayout;
                    lastButton.Text = x.Dish.iName;
                    lastButton.LongClick += Button_LongClickEvent;
                    lastButton.Id = idGen;
                    idList.Add(lastButton.Id);
                    numQty--;
                    idGen++;
                }

            }


            /*
             * 
             * Stuff for the previous and next buttons 
             * 
             */

            customerList.Insert(0, (new Customer() { custNum = currentCustomer, order = new List<EMItemList>(), subtotal = 0.0 }));
            customerList.Insert(1, (new Customer() { custNum = currentCustomer, order = new List<EMItemList>(), subtotal = 0.0 }));
            Android.Graphics.Color value;
            Button prev = FindViewById<Button>(Resource.Id.prevButton);
            Button next = FindViewById<Button>(Resource.Id.nextButton);
            int topCustomer = currentCustomer;
            prev.Click += (s, o) => {
                if(currentCustomer > 1)
                {
                    currentCustomer--;
                    colorDictionary.TryGetValue(currentCustomer, out value);
                    dropZone.SetBackgroundColor(value);
                    result.Text = "Customer " + currentCustomer;
                }
            };
            
            next.Click += (s, o) => {
                
                if (currentCustomer == topCustomer)
                {
                    color = Android.Graphics.Color.Argb(255, randomGen.Next(256), randomGen.Next(256), randomGen.Next(256));
                    
                    splits.Add(new List<string>());
                    currentCustomer++;
                    topCustomer = currentCustomer;
                    customerList.Insert(currentCustomer, (new Customer() { custNum = currentCustomer, order = new List<EMItemList>(), subtotal = 0.0 }));
                    colorDictionary.Add(currentCustomer, color);
                    result.Text = "Customer " + currentCustomer;
                    dropZone.SetBackgroundColor(color);
                }
                else
                {
                    currentCustomer++;
                    colorDictionary.TryGetValue(currentCustomer, out value);
                    dropZone.SetBackgroundColor(value);
                    result.Text = "Customer " + currentCustomer;
                }
                
            };

       
            //add all the buttons to the relativelayout
            foreach (Button btn in btnList)
            {
                rl.AddView(btn);
            }

            
            result = FindViewById<TextView>(Resource.Id.result);

            // Attach event to drop zone
            dropZone.Drag += DropZone_Drag;



            mLeftDataSet = new List<String>();
            mLeftAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
            mLeftDrawer.Adapter = mLeftAdapter;


            





        }//end of onCreate

        public void checkIfEvrythingIsAssigned()
        {
            int itemCount = splits[0].Count();
            int assignedCount = 0;

            foreach (Button btn in btnList)
            {
                var buttonBackground = btn.Background;
                if (buttonBackground is ColorDrawable)
                {
                    assignedCount++;
                }
            }

            if(assignedCount != itemCount)
            {
                RunOnUiThread(() =>
                {
                    Android.Support.V7.App.AlertDialog.Builder builder;
                    builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                    builder.SetTitle("Assign all the food!");
                    builder.SetMessage("Sorry, you must assign all the dishes to a customer before you can proceed!");
                    builder.SetCancelable(true);                    
                    builder.SetPositiveButton("OK", delegate {
                    });
                    builder.Show();
                }
               );
            }

            else
            {
              
                //confirmation screen that shows totals for each check.                
                String confirmationDialogString = "These are the totals per customer! Please make sure that this  is correct. \n";

                
                for (int i = 1; i < customerList.Count(); i++)
                {
                    Customer cus = customerList[i];
                    if (cus.order.Count() != 0)
                    {
                        confirmationDialogString = confirmationDialogString + "Customer " + cus.custNum + ": $" + (Math.Round(((cus.subtotal * .08875) + cus.subtotal), 2)) + "\n";
                    }
                }

                double totalCost = 0;
                foreach (Customer cust in customerList)
                {
                    totalCost += cust.subtotal;
                }
                confirmationDialogString = confirmationDialogString + "Combined Total: $" + (Math.Round(((totalCost * .08875) + totalCost), 2));

                RunOnUiThread(() =>
                {
                    Android.Support.V7.App.AlertDialog.Builder builder;
                    builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                    builder.SetTitle("Confirm split!");
                    builder.SetMessage(confirmationDialogString);
                    builder.SetCancelable(true);
                    builder.SetNegativeButton("We Messed Up", delegate {
                    });
                    builder.SetPositiveButton("All Good!", delegate {
                        string json = JsonConvert.SerializeObject(customerList);
                        Console.WriteLine("WEEEEEWOOOOOOOOOOOOOOOOOOWWWWWWEEEEEEEEEEEEEWWWWWWWWWWOOOOOOOOOOOOOOOOOO");
                        Console.WriteLine(json);


                        SetContentView(Resource.Layout.afterPaymentScreen);
                    });
                    builder.Show();
                });

            }
            
        }


        public void refreshNav()
        {
            mLeftAdapter.Clear();

            for (int i = 1; i < customerList.Count(); i++)
            {
                Customer cus = customerList[i];
                if (cus.order.Count() != 0)
                {

                    mLeftAdapter.Add("Customer " + cus.custNum);
                    foreach (EMItemList em in cus.order)
                    {
                        mLeftAdapter.Add(" • " + em.iName.ToString() + ": $" + em.ItemPrice);
                    }
                    mLeftAdapter.Add("Subtotal: $" + cus.subtotal);
                    mLeftAdapter.Add("Total: $" + (Math.Round(((cus.subtotal * .08875) + cus.subtotal), 2)));
                }
            }

                double totalCost = 0;
                foreach (Customer cust in customerList)
                {
                    totalCost += cust.subtotal;
                }
                mLeftAdapter.Add("Combined Total: $" + (Math.Round(((totalCost * .08875) + totalCost), 2)));
            

        }
        void Button_LongClickEvent(object sender, View.LongClickEventArgs e)
        {
            Button b = (Button) sender;
            var data = ClipData.NewPlainText("name", b.Text.ToString());
            ((sender) as Button).StartDrag(data, new View.DragShadowBuilder(((sender) as Button)), b, 0);
            ((sender) as Button).StartDrag(data, new View.DragShadowBuilder(((sender) as Button)), b, 0);
        }
 
        void DropZone_Drag(object sender, View.DragEventArgs e)
        {
            // React on different dragging events
            var evt = e.Event;
            
            switch (evt.Action)
            {
                case DragAction.Ended:
                case DragAction.Started:
                    e.Handled = true;
                    break;
                // Dragged element enters the drop zone
                case DragAction.Entered:
                    result.Text = "Drop it like it's hot!";
                    break;
                // Dragged element exits the drop zone
                case DragAction.Exited:
                    result.Text = "Customer " + currentCustomer;
                    break;
                // Dragged element has been dropped at the drop zone
                case DragAction.Drop:
                    // You can check if element may be dropped here
                    // If not do not set e.Handled to true
                    e.Handled = true;

                    // Try to get clip data
                    var data = e.Event.ClipData;
                    if (data != null)
                        result.Text = data.GetItemAt(0).Text + " has been dropped.";
                    
                    Button btn = (Button)e.Event.LocalState;
                    EMItemList item;
                    string dishName = data.GetItemAt(0).Text;
                    var buttonBackground = btn.Background;
                    if (buttonBackground is ColorDrawable)
                    {
                        
                        var backgroundColor = (buttonBackground as ColorDrawable).Color;
                        var myValue = colorDictionary.FirstOrDefault(x => x.Value == backgroundColor).Key;
                        Console.WriteLine("PRE-REMOVE: " + customerList[myValue].order.Count());
                        customerList[myValue].order.RemoveAt(customerList[myValue].order.IndexOf(customerList[myValue].order.Where(x => x.iName == dishName).FirstOrDefault()));
                        itemsOnMenu.TryGetValue(dishName, out item);
                        customerList[myValue].subtotal = customerList[myValue].subtotal - Convert.ToDouble(item.iPrice);
                        Console.WriteLine("REMOVED: " + dishName + " FROM CUSTOMER " + myValue);
                        Console.WriteLine("POST-REMOVE: " + customerList[myValue].order.Count());
                        refreshNav();
                    }
                   
                    Android.Graphics.Color value;
                   
                    itemsOnMenu.TryGetValue(dishName, out item);
                    customerList[currentCustomer].order.Add(item);
                    double price = Convert.ToDouble(item.iPrice);
                    customerList[currentCustomer].subtotal += price;
                    colorDictionary.TryGetValue(currentCustomer, out value);
                    btn.SetBackgroundColor(value);
                    splits[currentCustomer].Add(dishName);
                    int num = splits[currentCustomer].Count();
                    Console.WriteLine(num);
                    refreshNav();
                    break;
            }

    
        }

        /*public override bool OnOptionsItemSelected(IMenuItem item)
        {
           mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
        */
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Android.Resource.Id.Home:
                    //The hamburger icon was clicked which means the drawer toggle will handle the event
                    //all we need to do is ensure the right drawer is closed so the don't overlap
                    
                    mDrawerToggle.OnOptionsItemSelected(item);
                    return true;

                case Resource.Id.action_pay:
                    //Refresh
                    //This is where you need to start the next activity
                    checkIfEvrythingIsAssigned();



                    return true;

                

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }




    }
}