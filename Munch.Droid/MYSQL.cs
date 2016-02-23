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
using MySql.Data;

namespace Munch
{
    class MYSQL
    {
        public bool check_connection(string conn)

        {

            bool result = false;

            MySqlConnection connection = new MySqlConnection(conn);

            try

            {

                connection.Open();

                result = true;

                connection.Close();

            }

            catch

            {

                result = false;

            }

            return result;

        }



    }
}