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
        private List<String> dIngredientsList;
        private Button dAddIngredient;
        private Button dEditDish;
        private Button dDeleteDish;

        public event EventHandler<OnSignEventArgs_ManageMenu> editItemComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Load Up values from Dialog Box
            var view = inflater.Inflate(Resource.Layout.dialog_APEMEdit, container, false);
            DishName = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_Dishname);
            Description = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_description);
            Ingredients = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_ingredients);
            dAddIngredient = view.FindViewById<Button>(Resource.Id.btn_addingredient_Edit);
            Quantity = view.FindViewById<EditText>(Resource.Id.EMQuantity);
            calories = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_calories);
            cost = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_cost);
            price = view.FindViewById<EditText>(Resource.Id.Add_EditMenu_price);

            //Spinner Pull
            String[] spinnerFeed = AP_EM_Activity.ingredientsTransferList.ToArray();
            var ingSpinner = view.FindViewById<Spinner>(Resource.Id.spnr_EMIngredientsEdit);
            var ingAdapter = new ArrayAdapter<String>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, spinnerFeed);
            ingSpinner.Adapter = ingAdapter;
            selectedIngredient = ingSpinner.SelectedItem.ToString();

            //Edit and Delete Buttons
            dEditDish = view.FindViewById<Button>(Resource.Id.btn_Edit_dish);
            dEditDish.Click += dEditDish_Click;
            dDeleteDish = view.FindViewById<Button>(Resource.Id.btn_Delete_dish);
            dDeleteDish.Click += dDelete_Click;
            return view;
          }

        private void dAddIngredient_Click(object sender, EventArgs e)
        {
            dIngredientsList.Add((Quantity.Text.ToString() + " " + selectedIngredient.ToString()));
            Console.WriteLine("u dun fucked up......" + Quantity.Text.ToString() + " " + selectedIngredient.ToString());
        }

        private void dEditDish_Click(object sender, EventArgs e)
        {
            editItemComplete.Invoke(this, new OnSignEventArgs_ManageMenu(DishName.Text, Description.Text, Ingredients.Text, Quantity.Text, calories.Text, cost.Text, price.Text));
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