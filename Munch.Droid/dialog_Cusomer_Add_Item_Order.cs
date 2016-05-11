using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Munch
{
    public class dialog_Cusomer_Add_Item_Order : DialogFragment
    {
        private EditText DishName;
        private EditText Notes;
        private EditText Quant;
        private Button addDishToOrder;



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_Add_Dish_to_Order, container, false);
            DishName = view.FindViewById<EditText>(Resource.Id.Add_Dish_to_Order_DishName);
            DishName.Text = CV2_Menu.dishName_to_order.iName;
            Quant = view.FindViewById<EditText>(Resource.Id.Add_Dish_to_Order_Quantity);
            Notes = view.FindViewById<EditText>(Resource.Id.Add_Dish_to_Order_Note);
            addDishToOrder = view.FindViewById<Button>(Resource.Id.btn_Add_Dish_to_Order);
            addDishToOrder.Click += AddtoOrder_Click;

            return view;
        }

        /*
        ****************
        ****************
        ****************
        *DON'T FORGET TO CHANGE ORDERNUMBER*
        ****************
        ****************
        */
    
        private void AddtoOrder_Click(object sender, EventArgs e)
        {
             CustomerPortal.CustomerOrderList.Add(new CustomerOrderItem() { Dish = CV2_Menu.dishName_to_order, Quantity = Quant.Text, Notes = Notes.Text, OrderNumber = CV2_Menu.dishName_to_order.iPrice });
            CV2_YourOrder.mItems.Add(new CustomerOrderItem() { Dish = CV2_Menu.dishName_to_order, Quantity = Quant.Text, Notes = Notes.Text, OrderNumber = CV2_Menu.dishName_to_order.iPrice });
            Console.WriteLine(CustomerPortal.CustomerOrderList.Count());
            this.Dismiss();
            Android.Widget.Toast.MakeText(this.Activity, "Your item has been added to your order. Please check your full order by clicking on the Your Order button.", Android.Widget.ToastLength.Long).Show();

        }
    }
}