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
    class APMIListViewAdapter : BaseAdapter<APMIInventoryList>
    {
        private List<APMIInventoryList> mItems;
        private Context mContext;

        public APMIListViewAdapter(Context context, List<APMIInventoryList> items)
        {
            mItems = items;
            mContext = context;
        }

        public override int Count
        {
            get{ return mItems.Count;}
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override APMIInventoryList this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if(row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.APMIListViewRow, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.Manage_Inventory_Txt_Name);
            txtName.Text = mItems[position].Ingredients;

            TextView txtQuantity = row.FindViewById<TextView>(Resource.Id.Manage_Inventory_Txt_Quantity);
            txtQuantity.Text = mItems[position].Quantity;

            TextView txtUnits = row.FindViewById<TextView>(Resource.Id.Manage_Inventory_Txt_Units);
            txtUnits.Text = mItems[position].MeasureUnit;
            

            return row;
        }

    }
}