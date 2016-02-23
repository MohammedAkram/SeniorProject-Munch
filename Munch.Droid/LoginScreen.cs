
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



        public void LoginAuth()
        {
            EditText user = FindViewById<EditText>(Resource.Id.userName);
            EditText pass = FindViewById<EditText>(Resource.Id.password);
            
            //To Prevent SQLINJECT
            //string pattern = @"^\w+$";
            //prevents special characters being used
            //Example SELECT IF(COUNT(*) > 0, 'true', 'false') as Status FROM Accounts WHERE idAccounts =  ''or 1 =1; drop table security;--';--'&& Password = 'somepassword';
            //This will drop the table security
            string pattern = @"^\w+$";
            Regex regex = new Regex(pattern);
            Boolean IdSymbolCheck = regex.IsMatch(user.Text);
            Boolean PassSymbolCheck = regex.IsMatch(pass.Text);
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
                    string queryString = "SELECT IF(COUNT(*) > 0, 'true', 'false') as Status FROM Accounts WHERE idAccounts = '" + user.Text + "' && Password = '" + pass.Text + "';";
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
                       
                        Android.Widget.Toast.MakeText(this, "Login Successful", Android.Widget.ToastLength.Short).Show();
                        if (level == 0)
                        {
                            StartActivity(typeof(AdminPortal));
                            user.Text = "";
                            pass.Text = "";
                        }
                        else
                        {
                            StartActivity(typeof(Menu));
                        }

                    }


                    else
                    {
                        conn.Close();
                        Android.Widget.Toast.MakeText(this, "Login Failed", Android.Widget.ToastLength.Short).Show();
                    }
                }
                else
                {
                    Android.Widget.Toast.MakeText(this, "Cannot connect to server", Android.Widget.ToastLength.Short).Show();

                }

            }
            else
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


            ProgressDialog progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Contacting server. Please wait...");
            progress.SetCancelable(true);
            progress.OnStart();

            login.Click += delegate
            {
                
              
                    ProgressDialog progressDialog = ProgressDialog.Show(this, "", "Logging In");
                    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                    new Thread(new ThreadStart(delegate
                    {
                        //LOAD METHOD TO GET ACCOUNT INFO
                        
                        RunOnUiThread(() =>
                                      LoginAuth()
                                      );
                        //HIDE PROGRESS DIALOG
                        RunOnUiThread(() => progressDialog.Dismiss()); //progressBar.Visibility = ViewStates.Gone);
                    })).Start();

                

                
                

            };

        }
    }

}



