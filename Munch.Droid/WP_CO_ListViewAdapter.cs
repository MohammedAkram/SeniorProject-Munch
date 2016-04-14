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
    class WP_CO_ListViewAdapter : BaseAdapter<WP_CO_OrderList>
    {
        private List<WP_CO_OrderList> mItems;
        private Context mContext;

        public WP_CO_ListViewAdapter(Context context, List<WP_CO_OrderList> items)
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

        public override WP_CO_OrderList this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.WP_COListViewRow, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.WPCODishName);
            txtName.Text = mItems[position].dishName;

            TextView txtQuant = row.FindViewById<TextView>(Resource.Id.WPCOQuantity);
            txtQuant.Text = mItems[position].Quantity;

            TextView txtPrice = row.FindViewById<TextView>(Resource.Id.WPCOPrice);
            txtPrice.Text = mItems[position].Price;

            return row;
        }

    }
}