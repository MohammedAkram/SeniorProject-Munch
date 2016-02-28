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

        public override void OnBackPressed()
        {

            Android.Widget.Toast.MakeText(this, "You must logout to do that!", Android.Widget.ToastLength.Short).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
<<<<<<< HEAD
            
           


            

=======
         
>>>>>>> refs/remotes/origin/Mir'sCode
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AdminPortal);

            // Get our button from the layout resource,
            // and attach an event to it
            
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
<<<<<<< HEAD
=======
                StartActivity(typeof(APMIActivity));
>>>>>>> refs/remotes/origin/Mir'sCode
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
<<<<<<< HEAD
=======
                StartActivity(typeof(APMAActivity));
            };

            //Log Out Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutAdminPortalButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
>>>>>>> refs/remotes/origin/Mir'sCode
            };
        }
    }
}