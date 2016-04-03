using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Transitions;
using Android.Widget;
using PubNubMessaging.Core;
using Android.Animation;
using Android.Graphics;
using Android.Util;
using Android.Views.Animations;
using System.Net;

namespace Munch
{
    [Activity( Label = "AdminPortal", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class AdminPortal : Activity, ViewTreeObserver.IOnGlobalLayoutListener
    {
        //Pubnub
        Pubnub pubnub = new Pubnub("pub-c-ddf91c9e-baf7-47af-8ca8-276337355d46", "sub-c-d70d769c-ebda-11e5-8112-02ee2ddab7fe");
        void DisplaySubscribeReturnMessage(string result)
        {
            Console.WriteLine("SUBSCRIBE REGULAR CALLBACK: ");
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
                        string re = resultActualMessage.Replace('"', ' ');
                        string s1 = re.Substring(1, re.IndexOf(',')-1);
                        string s2 = re.Substring((re.IndexOf(',') + 1));
                        
                        

                        Notification.Builder builder = new Notification.Builder(this)
                        .SetContentTitle(s1)
                        .SetContentText(s2)
                        .SetPriority(2)
                        .SetColor(2)
                        .SetVibrate(new long[1])
                        .SetSmallIcon(Resource.Drawable.Icon);

                        // Build the notification:
                        Notification notification = builder.Build();

                        // Get the notification manager:
                        NotificationManager notificationManager =
                            GetSystemService(Context.NotificationService) as NotificationManager;

                        // Publish the notification:
                        const int notificationId = 0;
                        notificationManager.Notify(notificationId, notification);


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
        

        Button[] btns;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AdminPortal);

            //pubnub shit
            
            pubnub.Subscribe<string>(
                LoginScreen.loginUsername,
                DisplaySubscribeReturnMessage,
                DisplaySubscribeConnectStatusMessage,
                DisplayErrorMessage
                );
                

            this.btns = new int[]
            {
                Resource.Id.EditMenuButton,
                Resource.Id.ManageInventoryButton,
                Resource.Id.ViewReportsButton,
                Resource.Id.AccountManagementButton,
                Resource.Id.LogOutAdminPortalButton
            }.Select(FindViewById<Button>).ToArray();

            string strToDBAP = Waiter_Table_Selection_Activity.TableURL + "&&unassign=1";


            btns[0].Click += (sender, e) =>
            {
                StartActivity(typeof(AP_EM_Activity));
                OverridePendingTransition(Resource.Animation.right_in, Resource.Animation.right_out);
                
            };
            btns[1].Click += (sender, e) => StartActivity(typeof(AP_MI_Activity));
            btns[2].Click += (sender, e) => SetContentView(Resource.Layout.APViewReports);
            btns[3].Click += (sender, e) => StartActivity(typeof(AP_MA_Activity));
            btns[4].Click += (sender, e) =>
            {
                var webClient = new WebClient();
                Console.WriteLine(strToDBAP);
                webClient.DownloadString(strToDBAP);
                this.Finish();
                SetContentView(Resource.Layout.LoginScreen);
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            ContentView.ViewTreeObserver.AddOnGlobalLayoutListener(this);
        }

        View ContentView
        {
            get
            {
                return FindViewById(Android.Resource.Id.Content);
            }
        }

        public void OnGlobalLayout()
        {
            ContentView.ViewTreeObserver.RemoveGlobalOnLayoutListener(this);
            MakeStartAnimations(btns);
        }

        void MakeStartAnimations(Button[] btns)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                var anim = PrepareAnimation(btns[i], (i % 2) == 0 ? -1 : 1);
                anim.StartDelay = i * 100;
                anim.Start();
            }
        }

        ObjectAnimator PrepareAnimation(View view, int multiplier)
        {
            var path = new Path();

            var metrics = Resources.DisplayMetrics;
            var tx = multiplier * (metrics.WidthPixels - view.PaddingRight);
            var ty = -TypedValue.ApplyDimension(ComplexUnitType.Dip, 64, metrics);

            path.MoveTo(tx, ty);
            path.QuadTo(2 * tx / 3, 0, 0, 0);
            view.TranslationX = tx;
            view.TranslationY = ty;

            var anim = ObjectAnimator.OfFloat(view, "translationX", "translationY", path);
            anim.SetDuration(600);
            anim.SetInterpolator(AnimationUtils.LoadInterpolator(this, Android.Resource.Interpolator.FastOutSlowIn));
            return anim;
        }
    }
}