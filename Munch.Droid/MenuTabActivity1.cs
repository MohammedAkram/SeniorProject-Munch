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
    [Activity(Label = "MenuTabActivity1")]
    public class MenuTabActivity1 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here

            string contString = "Server=munchsqldb02.c5n9vlpy3ylv.us-west-2.rds.amazonaws.com;Port=3306;Database=Munch;User Id=root;Password=blueblue;charset=utf8";
            MySqlConnection conn = new MySqlConnection(contString);
            conn.Open();



            string queryString = "SELECT Breakfast FROM Menu";
            MySqlCommand sqlcmd = new MySqlCommand(queryString, conn);
            TextView tabtext1 = FindViewById<TextView>(Resource.Id.textView1);
            String result = sqlcmd.ExecuteScalar().ToString();
            Console.WriteLine(result);
            Console.ReadLine();
            //tabtext1.Text = (result);
            conn.Close();


        }
    }
}
