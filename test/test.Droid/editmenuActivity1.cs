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

namespace test.Droid
{
    [Activity(Label = "editmenuActivity1")]
    public class editmenuActivity1 : Activity
    {
     
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.Editmenulayout1);
           
            ActionBar.Tab tab = ActionBar.NewTab();
            tab.SetText(Resources.GetString(Resource.String.tab1_text));
            tab.SetIcon(Resource.Drawable.Icon);
            tab.TabSelected += (sender, args) =>
            {
                //add code to make tab do something when clicked
                SetContentView(Resource.Layout.Editmenulayout1);

            };

            ActionBar.AddTab(tab);
            tab = ActionBar.NewTab();
            tab.SetText(Resources.GetString(Resource.String.tab2_text));
            tab.SetIcon(Resource.Drawable.Icon);
            tab.TabSelected += (sender, args) =>
            {
                SetContentView(Resource.Layout.Editmenulayout2);//add code to make tab do something when clicked
            };

            ActionBar.AddTab(tab);



            // Create your application here
        }
    }
}