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
using System.Net;


namespace Munch
{
    public class OnSignEventArgs_AccountManagement : EventArgs
    {
        private string mLevel;
        private string mUsername;
        private string mPassword;

        public string Level
        {
            get { return mLevel; }
            set { mLevel = value; }
        }

        public string Username
        {
            get { return mUsername; }
            set { mUsername = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public OnSignEventArgs_AccountManagement(string level, string username, string password) : base()
        {
            Level = level;
            Username = username;
            Password = password;
        }

    }
    class dialog_APAccountManagement : DialogFragment
    {
        private EditText level;
        private EditText Username;
        private EditText Password;
        private Button dAddaccount;

        public event EventHandler<OnSignEventArgs_AccountManagement> addItemComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_APMAAdd, container, false);
            level = view.FindViewById<EditText>(Resource.Id.Add_Account_Level);
            Username = view.FindViewById<EditText>(Resource.Id.Add_Account_Username);
            Password = view.FindViewById<EditText>(Resource.Id.Add_Account_Password);
            dAddaccount = view.FindViewById<Button>(Resource.Id.btn_Add_Account);

            dAddaccount.Click += dAddaccount_Click;

            return view;
        }

        private void dAddaccount_Click(object sender, EventArgs e)
        {
            addItemComplete.Invoke(this, new OnSignEventArgs_AccountManagement(level.Text, Username.Text, Password.Text));
            var webClient = new WebClient();
            webClient.DownloadString("http://54.191.98.63/register.php?id=" + Username.Text + "&&password=" + Password.Text + "&&level=" + level.Text + "");
            this.Dismiss();
        }
    }
}