
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

    class CV_your_Order_ListViewAdapter : BaseAdapter<CustomerOrderItem>
    {
        private List<CustomerOrderItem> mItems;
        private Context mContext;
        

        public CV_your_Order_ListViewAdapter(Context context, List<CustomerOrderItem> items)
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

        public override CustomerOrderItem this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.CV_your_Order_listViewRow, null, false);
            }

           // TextView Dishname = row.FindViewById<TextView>(Resource.Id.Manage_Your_Order_Txt_Name);
           // Dishname.Text = mItems[position].Dish;

            TextView Dishname = row.FindViewById<TextView>(Resource.Id.Manage_Your_Order_Txt_Name);
            Dishname.Text = mItems[position].Dish.iName;

            TextView Quantity = row.FindViewById<TextView>(Resource.Id.Manage_Your_Order_Txt_Quantity);
            Quantity.Text = mItems[position].Quantity;

            TextView Notes = row.FindViewById<TextView>(Resource.Id.Manage_Your_Order_Txt_Note);
            Notes.Text = mItems[position].Notes;
            /*
            TextView txtThresholdc = row.FindViewById<TextView>(Resource.Id.Manage_Your_Order_Txt_Units);
            txtThresholdc.Text = mItems[position].OrderNumber;
            */
            TextView txtThresholdc = row.FindViewById<TextView>(Resource.Id.Manage_Your_Order_Txt_Units);
            txtThresholdc.Text = mItems[position].Dish.iPrice;

            return row;
        }
    }
}