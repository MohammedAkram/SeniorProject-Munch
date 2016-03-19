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
using PubNubMessaging.Core;

namespace Munch
{
    [Activity(MainLauncher = true, Label = "AdminPortal", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class AdminPortal : Activity
    {
        Pubnub pubnub = new Pubnub("pub-c-ddf91c9e-baf7-47af-8ca8-276337355d46", "sub-c-d70d769c-ebda-11e5-8112-02ee2ddab7fe");

        public override void OnBackPressed()
        {
            
            Android.Widget.Toast.MakeText(this, "You must logout to do that!", Android.Widget.ToastLength.Short).Show();
        }
            void DisplaySubscribeReturnMessage(string result)
        {
            Console.WriteLine("SUBSCRIBE REGULAR CALLBACK:");
            Console.WriteLine(result);
            if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(result.Trim()))
            {
                List<object> deserializedMessage = pubnub.JsonPluggableLibrary.DeserializeToListOfObject(result);
                if (deserializedMessage != null && deserializedMessage.Count > 0)
                {
                    object subscribedObject = (object)deserializedMessage[0];
                    if (subscribedObject != null)
                    {
                        //IF CUSTOM OBJECT IS EXCEPTED, YOU CAN CAST THIS OBJECT TO YOUR CUSTOM CLASS TYPE
                        string resultActualMessage = pubnub.JsonPluggableLibrary.SerializeToJsonString(subscribedObject);
                    }
                }
            }
        }
        void DisplaySubscribeConnectStatusMessage(string result)
        {
            Console.WriteLine("SUBSCRIBE CONNECT CALLBACK");
        }
        void DisplayErrorMessage(PubnubClientError pubnubError)
        {
            Console.WriteLine(pubnubError.StatusCode);
        }

        void DisplayReturnMessage(string result)
        {
            Console.WriteLine("PUBLISH STATUS CALLBACK");
            Console.WriteLine(result);
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {

            pubnub.Subscribe<string>(
            "my_channel",
            DisplaySubscribeReturnMessage,
            DisplaySubscribeConnectStatusMessage,
            DisplayErrorMessage
);

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AdminPortal);

            // Get our button from the layout resource,
            // and attach an event to it
            
            //edit menu button
            Button edit_menu = FindViewById<Button>(Resource.Id.EditMenuButton);

            edit_menu.Click += delegate
            {

                pubnub.Publish<string>("my_channel", "~~~~~~~~~ CAN I SEE THE TEST MESSAGE PLEASE WORK ~~~~~~~~~~~", DisplayReturnMessage, DisplayErrorMessage);
                StartActivity(typeof(AP_EM_Activity));
            };

            //manage inventory button
            Button manage_inventory = FindViewById<Button>(Resource.Id.ManageInventoryButton);

            manage_inventory.Click += delegate
            {
                
                StartActivity(typeof(AP_MI_Activity));

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
                
                StartActivity(typeof(AP_MA_Activity));
            };

            //Log Out Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutAdminPortalButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };
        }
    }
}