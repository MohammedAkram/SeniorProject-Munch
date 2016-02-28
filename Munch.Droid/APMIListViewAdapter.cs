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

            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = mItems[position].Name;

            TextView txtDescription = row.FindViewById<TextView>(Resource.Id.txtDescription);
            txtDescription.Text = mItems[position].Description;

            TextView txtQuantity = row.FindViewById<TextView>(Resource.Id.txtQuantity);
            txtQuantity.Text = mItems[position].Quantity;

            TextView txtPrice = row.FindViewById<TextView>(Resource.Id.txtPrice);
            txtPrice.Text = mItems[position].Price;

            return row;
        }

    }
}