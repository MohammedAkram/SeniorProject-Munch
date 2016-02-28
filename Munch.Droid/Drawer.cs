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
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;

namespace Munch
{
	[Activity(Label = "Drawer",  MainLauncher = true,  Icon = "@drawerable/icon", Theme= "@style/DrawerStyle")]
    public class Activity1 : ActionBarActivity
    {
		private SupportToolbar mToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			SetContentView (Resource.Layout.Main);

			mToolbar = FindViewById<SupportToolbar> (Resource.Id.toolbar);
			SetSupportActionBar (mToolbar);

        }
    }
}