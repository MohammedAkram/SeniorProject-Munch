
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Json;
using System.IO;

namespace Munch
{
    [Activity(Label = "Munch", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/android:Theme.Holo.Light.NoActionBar")]
    public class LoginScreen : Activity
    {
        /*
        Method LoginAuth():
        This method will connect to the MySQL database and check the accounts table for any matches on the entered user name and password.
        It returns an Int that will match a case in the screenChange() method. 
        Security features have been added to prevent unauthorized SQL Injections to the database. 
        */

        private bool userNameCheck(String userName, String password)
        {
            
            string pattern = @"^\w+$";
            Regex regex = new Regex(pattern);
            Boolean IdSymbolCheck = regex.IsMatch(userName);
            Boolean PassSymbolCheck = regex.IsMatch(password);

            if (IdSymbolCheck && PassSymbolCheck == true)
            {
                return true;
            }
            else return false;
        }


        private async Task<JsonValue> FetchLoginAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            
            

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    Console.Out.WriteLine("||||||||||||||||||||||||||||||||||||" +jsonDoc.ToString());
                    return jsonDoc.ToString();
                }
            }
        }

        private void ParseAndDisplay(String json)
        {

            List<String> products = JsonConvert.DeserializeObject<List<String>>(json);
            Console.Out.WriteLine(products[0]);
        }



        /*
            if (status.Equals("true"))
                {
                    if (level.Equals("0"))
                    {
                        return 0;
                    }

                    else {
                        return 1;
                    }

                }

                else return 2;
            }
          */

        //To Prevent SQLINJECT
        //string pattern = @"^\w+$";
        //prevents special characters being used
        //Example SELECT IF(COUNT(*) > 0, 'true', 'false') as Status FROM Accounts WHERE idAccounts =  ''or 1 =1; drop table security;--';--'&& Password = 'somepassword';
        //This will drop the table security





        /*
        Method screenChange():
        This method will change the view and launch neccessary activities related to each possible case that is determined
        by the LogInAuth() method.  
        */

        public void screenChange(int result)
        {

            EditText user = FindViewById<EditText>(Resource.Id.userName);
            EditText pass = FindViewById<EditText>(Resource.Id.password);
            //if the result is 0, launch the admin portal and reset the text views for username and password to ""
            if (result == 0)
            {
                Android.Widget.Toast.MakeText(this, "Login Successful", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(AdminPortal));
                user.Text = "";
                pass.Text = "";
            }

            //if the result is 1, launch the menu and reset the text views for username and password to ""
            else if (result == 1)
            {
                Android.Widget.Toast.MakeText(this, "Login Successful", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(Menu));
                user.Text = "";
                pass.Text = "";
            }

            //if the result is 2, the user input a combination of username and password that did not match any in the database
            else if (result == 2)
            {
                Android.Widget.Toast.MakeText(this, "Login Failed", Android.Widget.ToastLength.Short).Show();
            }

            //if the result is 3, there is an error establishing a connection to the server
            else if (result == 3)
            {
                Android.Widget.Toast.MakeText(this, "Cannot connect to server", Android.Widget.ToastLength.Short).Show();
            }

            //if the result is 4, the user is attempting to use illegal characters
            else if (result == 4)
            {
                Android.Widget.Toast.MakeText(this, "Cannot use special characters", Android.Widget.ToastLength.Short).Show();
            }

        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.LoginScreen);

            Button login = FindViewById<Button>(Resource.Id.login);
            EditText user = FindViewById<EditText>(Resource.Id.userName);
            EditText pass = FindViewById<EditText>(Resource.Id.password);
            String loginQueryURL = "http://54.191.139.104/login.php?id=Admin&&password=admin";
            int result;

            login.Click += async (sender, e) => {

                JsonValue json = await FetchLoginAsync(loginQueryURL);
                ParseAndDisplay(json);
                //screenChange(result);
                
            };
        }
    }
}



