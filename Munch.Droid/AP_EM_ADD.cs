using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using com.refractored.fab;
using System.Threading;
using System.Net;
using System.Threading.Tasks;
using System.Json;
using System.IO;
using Newtonsoft.Json;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;


namespace Munch
{
    [Activity(Label = "AP_EM_Add", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class AP_EM_Add : Activity
    {
        private async Task<JsonValue> FetchInventoryAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON String:
                    return jsonDoc.ToString();
                }
            }
        }
        private List<AP_MI_InventoryList> ParseAndDisplay(String json)
        {
            List<AP_MI_InventoryList> dataTableList = JsonConvert.DeserializeObject<List<AP_MI_InventoryList>>(json);
            Console.Out.WriteLine(dataTableList[0].Ingredients);
            Console.Out.WriteLine(dataTableList[0].Quantity);
            Console.Out.WriteLine(dataTableList[0].MeasureUnit);
            Console.Out.WriteLine(dataTableList.Count());
            return dataTableList;
        }

        //Ingredients Spinner
        public static List<String> ingredientsTransferList = new List<String>();
        //Selected Ingredient String
        private String selectedIngredient;

        //Values
        private EditText DishName;
        private EditText Description;
        private EditText Ingredients;
        private EditText Quantity;
        private EditText calories;
        private EditText cost;
        private EditText price;
        private Button dAddDish;
        private Button dAddIngredient;
        //List of Ingredients
        public List<String> dIngredientsList = new List<string> { };
        public Spinner ingspin;


        protected override async void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.ContentTransitions);
            Window.EnterTransition = new Explode();
            Window.ExitTransition = new Explode();
            Window.AllowReturnTransitionOverlap = true;
            Window.AllowEnterTransitionOverlap = true;

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.APEMAdd);
            //LogOut Button
            Button logout = FindViewById<Button>(Resource.Id.AddLogOut_Edit_Menu_Button);
            logout.Click += delegate
            {
                Android.Widget.Toast.MakeText(this, "Logged Out Successfully", Android.Widget.ToastLength.Short).Show();
                StartActivity(typeof(LoginScreen));
            };

            //Pull resources
            DishName = FindViewById<EditText>(Resource.Id.Add_EditMenu_Dishname);
            Description = FindViewById<EditText>(Resource.Id.Add_EditMenu_description);
            Ingredients = FindViewById<EditText>(Resource.Id.Add_EditMenu_ingredients);
            Quantity = FindViewById<EditText>(Resource.Id.EMQuantity);
            calories = FindViewById<EditText>(Resource.Id.Add_EditMenu_calories);
            cost = FindViewById<EditText>(Resource.Id.Add_EditMenu_cost);
            price = FindViewById<EditText>(Resource.Id.Add_EditMenu_price);

            //Spinner Loadup
            //Push
            String inventoryURL = "http://54.191.98.63/inventory.php";
            JsonValue json = await FetchInventoryAsync(inventoryURL);
            List<AP_MI_InventoryList> parsedData = ParseAndDisplay(json);
            if (ingredientsTransferList.Count() == 0)
            {
                for (int i = 0; i < parsedData.Count(); i++)
                {
                    ingredientsTransferList.Add(parsedData[i].Ingredients.ToString());
                }

            }
            //Pull
            
            String[] spinnerFeed = ingredientsTransferList.ToArray();
            var ingSpinner = FindViewById<Spinner>(Resource.Id.spnr_EMIngredientsAdd);
            var ingAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, spinnerFeed);
            ingSpinner.Adapter = ingAdapter;
            ingspin = ingSpinner;


            //Click Events
            dAddDish = FindViewById<Button>(Resource.Id.btn_Add_dish);
            dAddDish.Click += dAddDish_Click;
            dAddIngredient = FindViewById<Button>(Resource.Id.btn_Addaddingredient);
            dAddIngredient.Click += dAddIngredient_Click;
        }

        private void dAddDish_Click(object sender, EventArgs e)
        {

            Ingredients.Text = "";
            string strToDB = "";
            for (int i = 0; i < dIngredientsList.Count(); i++)
            {
                if (i == (dIngredientsList.Count() - 1))
                {
                    strToDB = strToDB + dIngredientsList[i];
                }
                else {
                    strToDB = strToDB + dIngredientsList[i] + ", ";
                }
            }
            var webClient = new WebClient();

            webClient.DownloadString("http://54.191.98.63/managemenu.php?dish=" + DishName.Text + "&&desc=" + Description.Text + "&&Ingredients=" + strToDB + "&&calories=" + calories.Text + " &&cost=" + cost.Text + "&&price=" + price.Text + "&&delete=0");
            this.Finish();
        }
        private void dAddIngredient_Click(object sender, EventArgs e)
        {
            selectedIngredient = ingspin.SelectedItem.ToString();
            dIngredientsList.Add((Quantity.Text.ToString() + " " + selectedIngredient.ToString()));
            Console.WriteLine(Quantity.Text.ToString() + " " + selectedIngredient.ToString());
        }
    }
}