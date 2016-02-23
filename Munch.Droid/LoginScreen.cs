
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
        public int LoginAuth()
        {
            //To Prevent SQLINJECT
            //string pattern = @"^\w+$";
            //prevents special characters being used
            //Example SELECT IF(COUNT(*) > 0, 'true', 'false') as Status FROM Accounts WHERE idAccounts =  ''or 1 =1; drop table security;--';--'&& Password = 'somepassword';
            //This will drop the table security
            EditText user = FindViewById<EditText>(Resource.Id.userName);
            EditText pass = FindViewById<EditText>(Resource.Id.password);
            string pattern = @"^\w+$";
            Regex regex = new Regex(pattern);
            Boolean IdSymbolCheck = regex.IsMatch(user.Text);
            Boolean PassSymbolCheck = regex.IsMatch(pass.Text);

            //server connection info
            string contString = "Server=munchsqldb02.c5n9vlpy3ylv.us-west-2.rds.amazonaws.com;Port=3306;Database=Munch;User Id=root;Password=blueblue;charset=utf8";
            Console.WriteLine("Symbol Check for Id = " + IdSymbolCheck);
            Console.WriteLine("Symbol Check for Password = " + PassSymbolCheck);
            MYSQL mysql = new MYSQL();
            if (IdSymbolCheck && PassSymbolCheck == true)
            {
                Boolean source_result = mysql.check_connection(contString);
                if (source_result == true)
                {
                    MySqlConnection conn = new MySqlConnection(contString);
                    conn.Open();
                    string queryString = "SELECT IF(COUNT(*) > 0, 'true', 'false') as Status FROM Accounts WHERE BINARY idAccounts = '" + user.Text + "' && BINARY Password = '" + pass.Text + "';";
                    MySqlCommand sqlcmd = new MySqlCommand(queryString, conn);
                    String userNameResult = sqlcmd.ExecuteScalar().ToString();
                    Console.WriteLine("Login Sucess = " + userNameResult);

                    if (userNameResult.Equals("true"))
                    {
                        String queryLevel = "SELECT Level FROM Munch.Accounts WHERE idAccounts = '" + user.Text + "'; ";
                        MySqlCommand sqlcmdLevel = new MySqlCommand(queryLevel, conn);
                        String userLevelResult = sqlcmdLevel.ExecuteScalar().ToString();

                        Console.WriteLine("User Level = " + userLevelResult);

                        conn.Close();

                        int level = Convert.ToInt32(userLevelResult);

                        Console.ReadLine();
                       
                        
                        if (level == 0)
                        {
                            return 0;
                            
                        }
                        else
                        {
                            
                            return 1;
                            
                        }

                    }

                    else
                    {
                        conn.Close();
                        return 2;                       
                    }
                }
                else
                {
                    return 3;
                }

            }
            else
{
                return 4;                
            }
        }
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


            int result;

            login.Click += delegate
            {
                ProgressDialog progressDialog = ProgressDialog.Show(this, "", "Logging In");
                    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                    new Thread(new ThreadStart(delegate
                    {
                        //LOAD METHOD TO GET ACCOUNT INFO
                        result = LoginAuth();

                        RunOnUiThread(() => screenChange(result));

                        //HIDE PROGRESS DIALOG
                        RunOnUiThread(() => progressDialog.Dismiss());
                        
                    })).Start();
            };
        }
    }
}



