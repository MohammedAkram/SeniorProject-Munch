using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Munch.Droid
{
    [Activity(Label = "DBconnect")]
    public class DBconnect : Activity
    {
        string connectionString = "Network Address = munchsqldb02.c5n9vlpy3ylv.us-west-2.rds.amazonaws.com;" + "Database = Munch;" + "User ID = root;" + "Password = blueblue;";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var
                builder = new AlertDialog.Builder(this);
                builder.SetTitle("Sucess!");
                builder.SetMessage("Connected to Munch!");
                builder.SetCancelable(false);
                builder.SetPositiveButton("Hurray!", delegate { Finish(); });
                builder.Show();
            }
        }
    }
}