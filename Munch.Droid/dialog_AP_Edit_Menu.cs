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
    public class OnSignEventArgs_ManageMenuadd : EventArgs
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
        public OnSignEventArgs_ManageMenuadd(string dishname, string description, string ingredients, string quant, string calories, string cost, string price) : base()
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
    class dialog_AP_Edit_Menu : DialogFragment
    {
        private EditText DishName;
        private EditText Description;
        private EditText Ingredients;
        private EditText Quantity;
        private EditText calories;
        private EditText cost;
        private EditText price;
        private Button dAddDish;
      

        public event EventHandler<OnSignEventArgs_ManageMenuadd> addItemComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Load Up values from Dialog Box
            var view = inflater.Inflate(Resource.Layout.dialog_APEMAdd, container, false);
            DishName = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_Dishname);
            Description = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_description);
            Ingredients = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_ingredients);
            Quantity = view.FindViewById<EditText>(Resource.Id.EMQuantity);
            calories = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_calories);
            cost = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_cost);
            price = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_price);
            
            //Spinner Pull
            String[] spinnerFeed = AP_EM_Activity.ingredientsTransferList.ToArray();
            var ingSpinner = view.FindViewById<Spinner>(Resource.Id.spnr_EMIngredients);
            var ingAdapter = new ArrayAdapter<String>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, spinnerFeed);
            ingSpinner.Adapter = ingAdapter;

            dAddDish = view.FindViewById<Button>(Resource.Id.btn_Add_dish);
            dAddDish.Click += dAddDish_Click;


     
            return view;
        }

        private void dAddDish_Click(object sender, EventArgs e)
        {
            addItemComplete.Invoke(this, new OnSignEventArgs_ManageMenuadd(DishName.Text, Description.Text, Ingredients.Text, Quantity.Text, calories.Text, cost.Text, price.Text));
            var webClient = new WebClient();
            /*
            webClient.DownloadString("http://54.191.98.63/editaccount.php?id=" + select + "&&newid=" + Username.Text + "&&level=" + level.Text + "&&delete=0&&password=" + Password.Text + "");
            */
            this.Dismiss();
        }

        private void dDelete_Click(object sender, EventArgs e)
        {

        }
    }
}