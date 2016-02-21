
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
using MySql.Data.MySqlClient;

namespace Munch
{
    [Activity(Label = "LoginScreen", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/android:Theme.Holo.Light.NoActionBar")]
    public class LoginScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.LoginScreen);
            

            EditText userName = FindViewById<EditText>(Resource.Id.userName);
            EditText passWord = FindViewById<EditText>(Resource.Id.password);

            Button login = FindViewById<Button>(Resource.Id.login);

            login.Click += delegate
            {

                // username check

               


                string contString = "Server=munchsqldb02.c5n9vlpy3ylv.us-west-2.rds.amazonaws.com;Port=3306;Database=Munch;User Id=root;Password=blueblue;charset=utf8";
                MySqlConnection conn = new MySqlConnection(contString);
                conn.Open();
                string queryString = "SELECT IF(COUNT(*) > 0, 'true', 'false') as Status FROM Accounts WHERE idAccounts = '" + userName.Text + "' && Password = '" + passWord.Text + "';";
                MySqlCommand sqlcmd = new MySqlCommand(queryString, conn);
                String userNameResult = sqlcmd.ExecuteScalar().ToString();
                Console.WriteLine(userNameResult);
                Console.ReadLine();

                conn.Close();

                if(userNameResult.Equals("true") )
                {
                    Android.Widget.Toast.MakeText(this, "Login Successful", Android.Widget.ToastLength.Short).Show();
                    StartActivity(typeof(Menu));
                }

                else
                {
                    Android.Widget.Toast.MakeText(this, "Login Failed", Android.Widget.ToastLength.Short).Show();
                }
                
            };



            

            
        }
    }
}