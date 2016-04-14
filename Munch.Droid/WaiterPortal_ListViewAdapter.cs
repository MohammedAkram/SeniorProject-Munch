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

    class WaiterPortal_ListViewAdapter : BaseAdapter<WaiterPortal_List>
    {

        private List<WaiterPortal_List> mItems;
        private Context mContext;

        public WaiterPortal_ListViewAdapter(Context context, List<WaiterPortal_List> items)
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

        public override WaiterPortal_List this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.WaiterPortal_ListView, null, false);
            }

             TextView txtTable = row.FindViewById<TextView>(Resource.Id.Manage_Selecteedtable_Name);
             txtTable.Text = mItems[position].selectedtable;





            return row;
        }

    }
}