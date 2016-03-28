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
    [Activity(Label = "MomoTest")]
    public class MomoTest : Activity
    {
        Button[] btns;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CV_Welcome);

            

            this.btns = new int[] {
                Resource.Id.btn_MomoTestButton1,
                Resource.Id.btn_MomoTestButton2,
             
            }.Select(FindViewById<Button>).ToArray();

            btns[0].Click += (sender, e) =>
            {
                Android.Widget.Toast.MakeText(this, "You clicked on MOMO BUTTON 1", Android.Widget.ToastLength.Short).Show();
            };
            btns[1].Click += (sender, e) =>
            {
                Android.Widget.Toast.MakeText(this, "You cliked on MOMO BUTTON 2", Android.Widget.ToastLength.Short).Show();
            };
        }
    }
}