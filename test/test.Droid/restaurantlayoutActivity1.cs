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
    [Activity(Label = "restaurantlayoutActivity1")]
    public class restaurantlayoutActivity1 : Activity, View.IOnTouchListener
    {
        ImageView imageButton1;
        override
        

        protected  void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Restauantlayout1);
            // Create your application here
            imageButton1 = (ImageView)FindViewById(Resource.Id.imageButton1);
            imageButton1.SetOnTouchListener(this);
         //   Button Savefloorlayout = FindViewById<Button>(Resource.Id.Savefloorlayout);
         //   Savefloorlayout.Click+= delegate
         //   {
         //       Savefloorlayout.sa
         //   }

        }



        float x, y = 0f;
        Boolean moving=false;
        public bool OnTouch(View v, MotionEvent e)
        {
           
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    moving = true;
                    break;
                case MotionEventActions.Move:
                    if (moving)
                    {
                        x = e.RawX - imageButton1.Width / 2;
                        y = e.RawY - imageButton1.Width ;
                        imageButton1.SetX(x);
                        imageButton1.SetY(y);
                    }
                    break;
                case MotionEventActions.Up:
                    moving = false;
                    break;
            }
            return true;
        }
    }
}