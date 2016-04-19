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
    class AP_VR_ProfitListViewAdapter : BaseAdapter<AP_VR_ProfitList>
    {
        private List<AP_VR_ProfitList> mItems;
        private Context mContext;

        public AP_VR_ProfitListViewAdapter(Context context, List<AP_VR_ProfitList> items)
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

        public override AP_VR_ProfitList this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.APVRProfitListViewRow, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.View_Report_Profit_Name);
            txtName.Text = mItems[position].ItemName;

            TextView txtQuantity = row.FindViewById<TextView>(Resource.Id.View_Report_Profit_Profit);
            txtQuantity.Text = mItems[position].Profit;

            TextView txtThreshold = row.FindViewById<TextView>(Resource.Id.View_Report_Profit_Date);
            txtThreshold.Text = mItems[position].Date;

            return row;
        }

    }
}