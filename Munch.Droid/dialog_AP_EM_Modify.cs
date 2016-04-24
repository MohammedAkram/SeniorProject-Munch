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
    public class OnSignEventArgs_ManageMenu : EventArgs
    {
        private string mdishname;
        private string mDescription;
        private string mIngredients;
        private string mQuantity;
        private string mcalories;
        private string mcost;
        private string mprice;

        public string DishName
        {
            get { return mdishname; }
            set { mdishname = value; }
        }
        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
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
        public string Calories
        {
            get { return mcalories; }
            set { mcalories = value; }
        }
        public string Cost
        {
            get { return mcost; }
            set { mcost = value; }
        }
        public string Price
        {
            get { return mprice; }
            set { mprice = value; }
        }
        public OnSignEventArgs_ManageMenu(string dishname, string description, string ingredients, string quant, string calories, string cost, string price) : base()
        {
            DishName = dishname;
            Description = description;
            Ingredients = ingredients;
            Quantity = quant;
            Calories = calories;
            Cost = cost;
            Price = price;
        }
    }
    class dialog_AP_EM_Modify : DialogFragment
    {
        private EditText DishName;
        private EditText Description;
        private EditText Ingredients;
        private EditText Quantity;
        private EditText calories;
        private EditText cost;
        private EditText price;
        private String selectedIngredient;
        private Button dAddIngredient;
        private Button dEditDish;
        private Button dDeleteDish;
        public Spinner ingspin;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Load Up values from Dialog Box
            var view = inflater.Inflate(Resource.Layout.dialog_APEMEdit, container, false);

            //Edit and Delete Buttons
            dEditDish = view.FindViewById<Button>(Resource.Id.btn_Edit_dish);
            dEditDish.Click += dEditDish_Click;
            dDeleteDish = view.FindViewById<Button>(Resource.Id.btn_Delete_dish);
            dDeleteDish.Click += dDelete_Click;
            return view;
          }

        private void dEditDish_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        private void dDelete_Click(object sender, EventArgs e)
        {
            var webClient = new WebClient();
            webClient.DownloadString("http://54.191.98.63/deletemenu.php?DishName=" + AP_EM_Activity.dishName_to_order.iName);
            this.Dismiss();
            Android.Widget.Toast.MakeText(this.Context, "Logged Out Successfully", Android.Widget.ToastLength.Long).Show();
        }
    }
}