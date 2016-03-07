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
    public class OnSignEventArgs_InventoryManagementEdit : EventArgs
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

    class dialog__AP_Manage_InventoryEdit : DialogFragment
    {
        private EditText ingredients;
        private EditText quantity;
        private EditText measureUnit;
        private Button dEditInventory;
        private Button dDeleteInventory;

        public event EventHandler<OnSignEventArgs_InventoryManagement> editItemComplete;
        public event EventHandler<OnSignEventArgs_InventoryManagement> deleteItemComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Load Up values from Dialog Box
            var view = inflater.Inflate(Resource.Layout.dialog_APMIEdit_, container, false);
            ingredients = view.FindViewById<EditText>(Resource.Id.txt_Edit_Ingredient);
            quantity = view.FindViewById<EditText>(Resource.Id.txt_Edit_Quantity1);
            measureUnit = view.FindViewById<EditText>(Resource.Id.txt_Edit_Unit1);
            dEditInventory = view.FindViewById<Button>(Resource.Id.btn_Edit_Inventory);
            dDeleteInventory = view.FindViewById<Button>(Resource.Id.btn_Delete_Inventory);
            //Click Event for Edit Account
            dEditInventory.Click += dEditInventory_Click;
            //Click Event for Delete Account
            dDeleteInventory.Click += dDeleteInventory_Click;
            return view;
        }
        //Edit Account Action
        private void dEditInventory_Click(object sender, EventArgs e)
        {
            editItemComplete.Invoke(this, new OnSignEventArgs_InventoryManagement(ingredients.Text, quantity.Text, measureUnit.Text));
            var webClient = new WebClient();
            webClient.DownloadString("http://54.191.98.63/register.php?id=" + quantity.Text + "&&password=" + measureUnit.Text + "&&level=" + ingredients.Text + "");
            this.Dismiss();
        }
        //Delete Account Action
        private void dDeleteInventory_Click(object sender, EventArgs e)
        {
            deleteItemComplete.Invoke(this, new OnSignEventArgs_InventoryManagement(ingredients.Text, quantity.Text, measureUnit.Text));
            var webClient = new WebClient();
            webClient.DownloadString("http://54.191.98.63/register.php?id=" + quantity.Text + "&&password=" + measureUnit.Text + "&&level=" + ingredients.Text + "");
            this.Dismiss();
        }

    }
}