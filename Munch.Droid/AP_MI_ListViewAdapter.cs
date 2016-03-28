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
    class AP_MI_ListViewAdapter : BaseAdapter<AP_MI_InventoryList>
    {
        private List<AP_MI_InventoryList> mItems;
        private Context mContext;

        public AP_MI_ListViewAdapter(Context context, List<AP_MI_InventoryList> items)
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

        public override AP_MI_InventoryList this[int position]
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

            TextView txtThreshold = row.FindViewById<TextView>(Resource.Id.Manage_Inventory_Txt_Threshold);
            txtThreshold.Text = mItems[position].Threshold;

            TextView txtUnits = row.FindViewById<TextView>(Resource.Id.Manage_Inventory_Txt_Units);
            txtUnits.Text = mItems[position].MeasureUnit;
            

            return row;
        }

    }
}