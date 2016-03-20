using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace Munch
{
    [Activity(Label = "CustomerPortal",  Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class CustomerPortal : FragmentActivity
    {
        private ViewPager mViewPager;
        private CV_SlidingTabScrollView mScrollView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerPortal);
            mScrollView = FindViewById<CV_SlidingTabScrollView>(Resource.Id.CV_sliding_tab);
            mViewPager = FindViewById<ViewPager>(Resource.Id.CV_viewPager);

            mViewPager.Adapter = new MenuPagerAdapter(SupportFragmentManager);
            mScrollView.ViewPager = mViewPager;
        }
    }

    //Populates the ScrollView with tabs and references for activities per tab
    public class MenuPagerAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> mFragmentHolder;

        public MenuPagerAdapter (Android.Support.V4.App.FragmentManager fragManager) : base(fragManager)
        {
            mFragmentHolder = new List<Android.Support.V4.App.Fragment>();
            mFragmentHolder.Add(new Breakfast());
            mFragmentHolder.Add(new Lunch());
            mFragmentHolder.Add(new Dinner());
            mFragmentHolder.Add(new Your_Order());
        }

        public override int Count
        {
            get { return mFragmentHolder.Count;}
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return mFragmentHolder[position];
        }
    }

    //Breakfast Menu
    public class Breakfast : Android.Support.V4.App.Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Breakfast, container, false);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Breakfast";
        }
    }

    //Lunch Menu
    public class Lunch : Android.Support.V4.App.Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Lunch, container, false);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Lunch";
        }
    }

    //Dinner Menu
    public class Dinner : Android.Support.V4.App.Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Dinner, container, false);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Dinner";
        }
    }

    //Your Order
    public class Your_Order : Android.Support.V4.App.Fragment
    {
        private TextView mTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CV_Your_Order, container, false);
            return view;
        }

        public override string ToString() //Called on line 156 in SlidingTabScrollView
        {
            return "Your Order";
        }
    }
}