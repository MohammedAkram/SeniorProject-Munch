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

namespace Munch.Droid
{
    [Activity(Label = "EditMenu")]
    public class EditMenu : Activity
    {
        public class SpeakersFragment : Fragment
        {
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                View view = inflater.Inflate(Resource.Layout.simple_fragment, null);
                view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.speakers_tab_label);
                view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_speakers);
                return view;
            }
        }

        public class SessionsFragment : Fragment
        {
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                View view = inflater.Inflate(Resource.Layout.simple_fragment, null);
                view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.sessions_tab_label);
                view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_sessions);
                return view;
            }
        }

        public class WhatsOnFragment : Fragment
        {
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                View view = inflater.Inflate(Resource.Layout.simple_fragment, null);
                view.FindViewById<TextView>(Resource.Id.textView1).SetText(Resource.String.whatson_tab_label);
                view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageResource(Resource.Drawable.ic_action_whats_on);
                return view;
            }
        }
    }
}