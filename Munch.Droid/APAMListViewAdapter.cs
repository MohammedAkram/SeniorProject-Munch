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

namespace Munch
{
    class APAMListViewAdapter : BaseAdapter<APAMAccountList>
    {
        private List<APAMAccountList> mItems;
        private Context mContext;

        public APAMListViewAdapter(Context context, List<APAMAccountList> items)
        {
            mItems = items;
            mContext = context;
        }

        public override int Count
        {
            get { return mItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override APAMAccountList this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.APAMListViewRow, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = mItems[position].Name;

            TextView txtUsername = row.FindViewById<TextView>(Resource.Id.txtUsername);
            txtUsername.Text = mItems[position].Username;

            TextView txtPassword = row.FindViewById<TextView>(Resource.Id.txtPassword);
            txtPassword.Text = mItems[position].Password;

            return row;
        }

    }
}