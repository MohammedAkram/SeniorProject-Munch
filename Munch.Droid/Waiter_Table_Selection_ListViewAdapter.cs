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
    
   class Waiter_Table_Selection_ListViewAdapter : BaseAdapter<Waiter_Table_Selection_List>
    {
       
        private List<Waiter_Table_Selection_List> mItems;
        private Context mContext;

        public Waiter_Table_Selection_ListViewAdapter(Context context, List<Waiter_Table_Selection_List> items)
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

        public override Waiter_Table_Selection_List this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.Waiter_Table_Selection_ListView, null, false);
            }

            TextView txtTable = row.FindViewById<TextView>(Resource.Id.Waiter_Table_Selection_Txt_Table);
            txtTable.Text = mItems[position].idAccounts;

            CheckBox checkTable = row.FindViewById<CheckBox>(Resource.Id.Waiter_Table_Selection_checkBox);
            bool temp = mItems[position].isTaken;

            checkTable.Checked = temp;

            return row;
        }

    }
}