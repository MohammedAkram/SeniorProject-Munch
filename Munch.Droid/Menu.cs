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
    [Activity(Label = "Menu")]
    public class Menu : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ActionBar.SetDisplayShowTitleEnabled(false);
            ActionBar.SetDisplayShowHomeEnabled(false);
            ActionBar.SetCustomView(Resource.Layout.Menu_ActionBar);
            ActionBar.SetDisplayShowCustomEnabled(true);
            SetContentView(Resource.Layout.MenuTab1);

            LinearLayout Tab1 = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
         

            Tab1.Click += delegate
            {
                SetContentView(Resource.Layout.MenuTab1);
                StartActivity(typeof(MenuTabActivity1));
            };

            LinearLayout Tab2 = FindViewById<LinearLayout>(Resource.Id.linearLayout2);

            Tab2.Click += delegate
            {
                SetContentView(Resource.Layout.MenuTab2);
            };

            LinearLayout Tab3 = FindViewById<LinearLayout>(Resource.Id.linearLayout3);

            Tab3.Click += delegate
            {
                SetContentView(Resource.Layout.MenuTab3);
            };

            LinearLayout Tab4 = FindViewById<LinearLayout>(Resource.Id.linearLayout4);

            Tab4.Click += delegate
            {
                SetContentView(Resource.Layout.MenuTab4);
            };



        }

        
    }
    }

