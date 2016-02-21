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
    [Activity(Label = "AdminPortal", MainLauncher = true, Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class AdminPortal : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AdminPortal);

            // Get our button from the layout resource,
            // and attach an event to it
            SetContentView(Resource.Layout.AdminPortal);
            //edit menu button
            Button edit_menu = FindViewById<Button>(Resource.Id.fuckingpat_1);

            edit_menu.Click += delegate
            {
                Android.Widget.Toast.MakeText(this, "Go somewhere 1", Android.Widget.ToastLength.Short).Show();
            };

            //manage inventory button
            Button manage_inventory = FindViewById<Button>(Resource.Id.fuckingpat_2);

            manage_inventory.Click += delegate
            {
                Android.Widget.Toast.MakeText(this, "Go somewhere 2", Android.Widget.ToastLength.Short).Show();
            };

            //view reports button
            Button view_reports = FindViewById<Button>(Resource.Id.fuckingpat_3);

            view_reports.Click += delegate
            {
                Android.Widget.Toast.MakeText(this, "Go somewhere 3", Android.Widget.ToastLength.Short).Show();
            };

            //account management button
            Button account_management = FindViewById<Button>(Resource.Id.fuckingpat_4);

            account_management.Click += delegate
            {
                Android.Widget.Toast.MakeText(this, "Go somewhere 4", Android.Widget.ToastLength.Short).Show();
            };
        }
    }
}