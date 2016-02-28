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
    [Activity(Label = "APMIActivity", 
        Theme = "@android:style/Theme.Holo.Light.NoActionBar", 
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]

    public class APMIActivity : Activity
    {
        //List
        private List<APMIInventoryList> mItems;
        public ListView mListView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.APManageInventory);

            //Log Out Button
            Button logout = FindViewById<Button>(Resource.Id.LogOutMngInvtryButton);
            logout.Click += delegate
            {
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Load Up List
            mListView = FindViewById<ListView>(Resource.Id.mngInventoryListView);
            mItems = new List<APMIInventoryList>();
            mItems.Add(new APMIInventoryList() { Name = "Name", Description = "Description", Quantity = "Quantity", Price = "Price" });
            mItems.Add(new APMIInventoryList() { Name = "fuck", Description = "what the fuck", Quantity = "2", Price = "43.23" });
            mItems.Add(new APMIInventoryList() { Name = "FUCKING", Description = "FACK", Quantity = "0", Price = "14.23" });
            mItems.Add(new APMIInventoryList() { Name = "FACK", Description = "FUCKING", Quantity = "2", Price = "43.23" });

            APMIListViewAdapter adapter = new APMIListViewAdapter(this, mItems);
            mListView.Adapter = adapter;

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.APMIfab);
            fab.AttachToListView(mListView);
            fab.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_APManageInventory manageinventoryDialog = new dialog_APManageInventory();
                manageinventoryDialog.Show(transaction, "dialog fragment");

                Android.Widget.Toast.MakeText(this, "Dialog Opened", Android.Widget.ToastLength.Short).Show();
            };

        }
    }
}