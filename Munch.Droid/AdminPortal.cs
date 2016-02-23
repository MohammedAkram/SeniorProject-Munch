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
    [Activity(Label = "AdminPortal", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
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
            Button edit_menu = FindViewById<Button>(Resource.Id.EditMenuButton);

            edit_menu.Click += delegate
            {
                SetContentView(Resource.Layout.APEditMenu);
                StartActivity(typeof(Menu));
            };

            //manage inventory button
            Button manage_inventory = FindViewById<Button>(Resource.Id.ManageInventoryButton);

            manage_inventory.Click += delegate
            {
                SetContentView(Resource.Layout.APManageInventory);
            };

            //view reports button
            Button view_reports = FindViewById<Button>(Resource.Id.ViewReportsButton);

            view_reports.Click += delegate
            {
                SetContentView(Resource.Layout.APViewReports);
            };

            //account management button
            Button account_management = FindViewById<Button>(Resource.Id.AccountManagementButton);

            account_management.Click += delegate
            {
                SetContentView(Resource.Layout.APAccountManagement);
            };
        }
    }
}