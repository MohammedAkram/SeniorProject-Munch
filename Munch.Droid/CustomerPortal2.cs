using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using com.refractored.fab;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Munch
{
    [Activity(Label = "CustomerPortal2", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class CustomerPortal2 : Activity
    {
        //Set up splash screen as a view
        View mView;
        //Set up fab to disappear
        View fView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerPortal2);
            //Associations
            mView = FindViewById(Resource.Id.customerspalsh);
            fView = FindViewById(Resource.Id.CV2fab);
            Button menubtn = FindViewById<Button>(Resource.Id.MenuBtn);
            Button orderbtn = FindViewById<Button>(Resource.Id.orderBtn);

            //FAB
            var fab = FindViewById<FloatingActionButton>(Resource.Id.CV2fab);
            FindViewById<FloatingActionButton>(Resource.Id.CV2fab).Click += (sender, e) =>
            {
                Hide();
                fab.Visibility = ViewStates.Gone;
                menubtn.Visibility = ViewStates.Visible;
                orderbtn.Visibility = ViewStates.Visible;
            };

            menubtn.Click += (s, o) => StartActivity(typeof(CV2_Menu));
            orderbtn.Click += (s, o) => StartActivity(typeof(CV2_YourOrder));
        }

        //Reveal Animation
        private void Hide(bool bottom = false)
        {
            int cx = 0;
            int cy = 0;
            if (bottom)
            {
                cx = mView.Right;
                cy = (mView.Top + mView.Bottom);
            }
            else
            {
                cx = (mView.Left + mView.Right) / 2;
                cy = (mView.Top + mView.Bottom) / 2;
            }
            int initialRadius = mView.Width;
            var anim = ViewAnimationUtils.CreateCircularReveal(mView, cx, cy, initialRadius, 0);
            anim.AnimationEnd += (sender, e) => { mView.Visibility = ViewStates.Invisible; };
            anim.Start();
        }
        private void Hide2(bool bottom = false)
        {
            int cx = 0;
            int cy = 0;
            if (bottom)
            {
                cx = fView.Right;
                cy = (fView.Top + fView.Bottom);
            }
            else
            {
                cx = (fView.Left + fView.Right) / 2;
                cy = (fView.Top + fView.Bottom) / 2;
            }
            int initialRadius = fView.Width;
            var anim = ViewAnimationUtils.CreateCircularReveal(fView, cx, cy, initialRadius, 0);
            anim.AnimationEnd += (sender, e) => { fView.Visibility = ViewStates.Invisible; };
            anim.Start();
        }
    }
}