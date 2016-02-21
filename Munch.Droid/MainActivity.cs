using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Munch
{
    [Activity(Label = "Munch")]
    public class MainActivity : Activity
    {


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);



            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it


            ActionBar.SetDisplayShowTitleEnabled(false);
            Button ResturantLayout = FindViewById<Button>(Resource.Id.button1);

            ResturantLayout.Click += delegate
            {
                StartActivity(typeof(restaurantlayoutActivity1));
            };
            Button MenuC = FindViewById<Button>(Resource.Id.button2);

            MenuC.Click += delegate
            {
                StartActivity(typeof(Menu));
            };

            
        }
    }
}

