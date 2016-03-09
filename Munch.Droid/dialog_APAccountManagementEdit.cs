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
    public class OnSignEventArgs_AccountManagementEdit : EventArgs
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

        public OnSignEventArgs_AccountManagementEdit(string level, string username, string password) : base()
        {
            Level = level;
            Username = username;
            Password = password;
        }

    }

    class dialog_APAccountManagementEdit : DialogFragment
    {
        private EditText level;
        private EditText Username;
        private EditText Password;
        private Button dEditaccount;
        private Button dDeleteaccount;

        public event EventHandler<OnSignEventArgs_AccountManagement> editItemComplete;
        public event EventHandler<OnSignEventArgs_AccountManagement> deleteItemComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Load Up values from Dialog Box
            var view = inflater.Inflate(Resource.Layout.dialog_APMAEdit, container, false);
            level = view.FindViewById<EditText>(Resource.Id.Add_Account_Level);
            Username = view.FindViewById<EditText>(Resource.Id.Add_Account_Username);
            Password = view.FindViewById<EditText>(Resource.Id.Add_Account_Password);
            dEditaccount = view.FindViewById<Button>(Resource.Id.btn_Edit_Account);
            dDeleteaccount = view.FindViewById<Button>(Resource.Id.btn_Delete_Account);
            //Click Event for Edit Account
            dEditaccount.Click += dEditaccount_Click;
            //Click Event for Delete Account
            dDeleteaccount.Click += dDeleteaccount_Click;
            return view;
        }
        //Edit Account Action
        private void dEditaccount_Click(object sender, EventArgs e)
        {
            editItemComplete.Invoke(this, new OnSignEventArgs_AccountManagement(level.Text, Username.Text, Password.Text));
            var webClient = new WebClient();
            webClient.DownloadString("http://54.191.98.63/editaccount.php?id=" + Username.Text + " &&  = " + " && password=" + Password.Text + "&&level=" + level.Text + "");
            this.Dismiss();
        }
        //Delete Account Action
        private void dDeleteaccount_Click(object sender, EventArgs e)
        {
            deleteItemComplete.Invoke(this, new OnSignEventArgs_AccountManagement(level.Text, Username.Text, Password.Text));
            var webClient = new WebClient();
            webClient.DownloadString("http://54.191.98.63/register.php?id=" + Username.Text + "&&password=" + Password.Text + "&&level=" + level.Text + "");
            this.Dismiss();
        }

    }
}