using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace test.Droid
{
	[Activity (Label = "test.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button ResturantLayout = FindViewById<Button> (Resource.Id.button1);
            Button EditMenu = FindViewById<Button>(Resource.Id.button2);

            EditMenu.Click += delegate
            {
                StartActivity(typeof(editmenuActivity1));
            };
            ResturantLayout.Click += delegate {
                StartActivity(typeof(restaurantlayoutActivity1));
            
            
			};
		}
	}
}


