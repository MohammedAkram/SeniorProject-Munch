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
    class EditMenuListViewAdapter : BaseAdapter<EditMenu_DishList>
    {
        private List<AP_MI_InventoryList> mItems;
        private Context mContext;

        public EditMenuListViewAdapter(Context context, List<EditMenu_DishList> items)
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

        public override EditMenu_DishList this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.EditMenuListViewRow, null, false);
            }

            TextView txtTab = row.FindViewById<TextView>(Resource.Id.Edit_Menu_txt_Tab);
            txtTab.Text = mItems[position].Tab;

            TextView txtDsih = row.FindViewById<TextView>(Resource.Id.Edit_Menu_txt_Dish);
            txtDish.Text = mItems[position].Dish;

            TextView txtIngredient = row.FindViewById<TextView>(Resource.Id.Edit_Menu_txt_Ingredient);
            txtIngredient.Text = mItems[position].Ingredient;

            TextView txtCostPrice = row.FindViewById<TextView>(Resource.Id.Edit_Menu_txt_CostPrice);
            txtCostPrice.Text = mItems[position].CostPrice;

            Tiew txtSellPrice = row.FindViewById<TextView>(Resource.Id.Edit_Menu_txt_SellPrice);
            txtSellPrice.Text = mItems[position].SellPrice;


            return row;
        }

    }
}