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
using com.refractored.fab;

namespace Munch
{
    [Activity(Label = "APMAActivity",
        Theme = "@android:style/Theme.Holo.Light.NoActionBar",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]

    public class APMAActivity : Activity
    {
        //List
        private List<APAMAccountList> mItems;
        public ListView mListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.APAccountManagement);

            //Log Out Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutAccntMgmtButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Load Up List
            mListView = FindViewById<ListView>(Resource.Id.accntMgmtListView);
            mItems = new List<APAMAccountList>();
            mItems.Add(new APAMAccountList() { Name = "Name", Username = "Username", Password = "Password"});

            APAMListViewAdapter adapter = new APAMListViewAdapter(this, mItems);
            mListView.Adapter = adapter;

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.APAMfab);
            fab.AttachToListView(mListView);
            fab.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_APAccountManagement manageAccount = new dialog_APAccountManagement();
                manageAccount.Show(transaction, "dialog fragment");

                Android.Widget.Toast.MakeText(this, "Dialog Opened", Android.Widget.ToastLength.Short).Show();
            };

        }
    }
}