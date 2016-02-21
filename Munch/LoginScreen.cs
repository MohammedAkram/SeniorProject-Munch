using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Munch
{
    [Activity(Label = "Munch", Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class LoginScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginScreen);

            // Get our button from the layout resource,
            // and attach an event to it
            SetContentView(Resource.Layout.LoginScreen);
            Button login = FindViewById<Button>(Resource.Id.login);

            login.Click += (object sender, EventArgs e) =>
                Android.Widget.Toast.MakeText(this, "Login Button Clicked!", Android.Widget.ToastLength.Short).Show();
    }
    }
}

