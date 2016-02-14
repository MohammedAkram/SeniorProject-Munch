using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Munch.Droid
{
	[Activity (Label = "Munch.Droid", MainLauncher = true, Icon = "@drawable/ic_launcher")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button EditMenu = FindViewById<Button> (Resource.Id.button2);

            EditMenu.Click += delegate {
                StartActivity(typeof(EditMenu));
			};
		}
	}
}


