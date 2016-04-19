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
    class AP_VR_IngredientListViewAdapter : BaseAdapter<AP_VR_IngredientList>
    {
        private List<AP_VR_IngredientList> mItems;
        private Context mContext;

        public AP_VR_IngredientListViewAdapter(Context context, List<AP_VR_IngredientList> items)
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

        public override AP_VR_IngredientList this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.APVRIngreditentsListViewRow, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.AP_VR_Ingredient_Ingredients);
            txtName.Text = mItems[position].Ingredients;

            TextView txtQuantity = row.FindViewById<TextView>(Resource.Id.AP_VR_Ingredient_amtMeasure);
            txtQuantity.Text = mItems[position].MeasureUnit;

            TextView txtUsed = row.FindViewById<TextView>(Resource.Id.AP_VR_Ingredient_amtUsed);
            txtQuantity.Text = mItems[position].used;

            TextView txtThreshold = row.FindViewById<TextView>(Resource.Id.AP_VR_Ingredient_Date);
            txtThreshold.Text = mItems[position].DATE;

            return row;
        }

    }
}