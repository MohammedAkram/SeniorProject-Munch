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

namespace Munch
{
    [Activity(Label = "EvenPayment", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class EvenPayment : Activity
    {

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



        double subtotal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EvenPayment);

            TextView SplitAmount = FindViewById<TextView>(Resource.Id.splitamount);
            TextView SplitAmountOwe = FindViewById<TextView>(Resource.Id.EvenSplitid);
            TextView Total = FindViewById<TextView>(Resource.Id.EvenSplitTotal);
            Button Decrease = FindViewById<Button>(Resource.Id.decrease);
            Button Increase = FindViewById<Button>(Resource.Id.increase);

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
            Double TotalCost = Math.Round(((subtotal * .08875) + subtotal),2);
            String TotalCostString = Convert.ToString(TotalCost);

            Total.Text ="Total Amount: " + TotalCostString;
            
            //Starting with 1 customer
            int intSplitAmount = Int32.Parse(SplitAmount.Text);
            double SplitTotal = Math.Round((TotalCost / intSplitAmount), 2);
            SplitAmountOwe.Text = "$" + Convert.ToString(TotalCost / intSplitAmount);

            //Decreasing number of customer
            Decrease.Click += delegate
            {
                if (intSplitAmount<=1)
                {
                    Android.Widget.Toast.MakeText(this, "Cannot go below 1!", Android.Widget.ToastLength.Short).Show();
                }
                else
                {
                    intSplitAmount--;
                    SplitAmount.Text = "$" + Convert.ToString(intSplitAmount);
                    SplitTotal = Math.Round((TotalCost / intSplitAmount), 2);
                    SplitAmountOwe.Text = "$" + Convert.ToString(SplitTotal);
                }               
            };
            //Increasing number of customer
            Increase.Click += delegate
            {
                intSplitAmount++;
                SplitAmount.Text = "$" + Convert.ToString(intSplitAmount);
                SplitTotal = Math.Round((TotalCost / intSplitAmount), 2);
                SplitAmountOwe.Text = "$"+Convert.ToString(SplitTotal);
            };
            Button pay = FindViewById<Button>(Resource.Id.EvenSplitButton);

            pay.Click += delegate
            {
                pubnub.Publish<string>(LoginScreen.loginUsername, "Here's how they split: $" + TotalCostString + " x"+ intSplitAmount, DisplayReturnMessage, DisplayErrorMessage);
            };

        }
    }
}