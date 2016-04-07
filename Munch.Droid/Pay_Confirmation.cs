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
    [Activity(Label = "Pay_Confirmation")]
    public class Pay_Confirmation : Activity
    {
        public override void OnBackPressed()
        {
           
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Pay_Confirmation);
           
           
        }
    }
}