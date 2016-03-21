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
using System.Net;

namespace Munch
{
    /*public class OnSignEventArgs_Editmenu : EventArgs
    {
        private string mIngredients;
        private string mQuantity;
        private string mMeasureUnit;
        public string Ingredients
        {
            get { return mIngredients; }
            set { mIngredients = value; }
        }
        public string Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public string MeasureUnit
        {
            get { return mMeasureUnit; }
            set { mMeasureUnit = value; }
        }
        public OnSignEventArgs_InventoryManagementEdit(string ingredients, string quantity, string measureUnit) : base()
        {
            Ingredients = ingredients;
            Quantity = quantity;
            MeasureUnit = measureUnit;
        }
    }
*/
    class dialog_AP_Edit_Menu : DialogFragment
    {
        private EditText DishName;
        private EditText Description;
        private EditText Ingredients;
        private EditText calories;
        private EditText cost;
        private EditText price;
        private Button dAddDish;


        /*
                public event EventHandler<OnSignEventArgs_InventoryManagement> editItemComplete;
                public event EventHandler<OnSignEventArgs_InventoryManagement> deleteItemComplete;
                string select = (AP_MI_Activity.xcc);*/
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Load Up values from Dialog Box
            var view = inflater.Inflate(Resource.Layout.dialog_APEMAdd, container, false);
            DishName = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_Dishname);
            Description = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_description);
            /**
            Ingredients = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_ingredients);
            **/
            calories = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_calories);
            cost = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_cost);
            price = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_price);
            dAddDish = view.FindViewById<Button>(Resource.Id.btn_Add_dish);

            //Spinner Pull
            String[] spinnerFeed = AP_EM_Activity.ingredientsTransferList.ToArray();
            var ingSpinner = view.FindViewById<Spinner>(Resource.Id.spnr_EMIngredients);
            var ingAdapter = new ArrayAdapter<String>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, spinnerFeed);
            ingSpinner.Adapter = ingAdapter;


            /*  dDeleteInventory = view.FindViewById<Button>(Resource.Id.btn_Delete_Inventory);
              ingredients.Text = select;
              //Click Event for Edit Account
              dEditInventory.Click += dEditInventory_Click;
              //Click Event for Delete Account
              dDeleteInventory.Click += dDeleteInventory_Click;
              */
            return view;
          }

        /*
          //Edit Inventory Action
          private void dEditInventory_Click(object sender, EventArgs e)
          {
              editItemComplete.Invoke(this, new OnSignEventArgs_InventoryManagement(ingredients.Text, quantity.Text, measureUnit.Text));
              var webClient = new WebClient();
              webClient.DownloadString("http://54.191.98.63/manageinventory.php?name=" + select + "&&unit=" + measureUnit.Text + "&&quantity=" + quantity.Text + "");
              this.Dismiss();
          }
          //Delete Inventory Action
          private void dDeleteInventory_Click(object sender, EventArgs e)
          {
              deleteItemComplete.Invoke(this, new OnSignEventArgs_InventoryManagement(ingredients.Text, quantity.Text, measureUnit.Text));
              var webClient = new WebClient();
              webClient.DownloadString("http://54.191.98.63/manageinventory.php?name=" + select + "&&delete=1");
              this.Dismiss();
              }
          */
    }
}