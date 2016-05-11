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

namespace Munch
{
    [Activity(Label = "EvenPayment", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class EvenPayment : Activity
    {
        double subtotal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EvenPayment);

            EditText SplitAmount = FindViewById<EditText>(Resource.Id.splitamount);
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
            SplitAmountOwe.Text = Convert.ToString(TotalCost / intSplitAmount);

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
                    SplitAmount.Text = Convert.ToString(intSplitAmount);
                    SplitTotal = Math.Round((TotalCost / intSplitAmount), 2);
                    SplitAmountOwe.Text = Convert.ToString(SplitTotal);
                }               
            };
            //Increasing number of customer
            Increase.Click += delegate
            {
                intSplitAmount++;
                SplitAmount.Text = Convert.ToString(intSplitAmount);
                SplitTotal = Math.Round((TotalCost / intSplitAmount), 2);
                SplitAmountOwe.Text = Convert.ToString(SplitTotal);
            };


        }
    }
}