using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace Munch
{
    public class EMItemList
    {
        // Public var for all Card Menu items
        public string ItemName;
        public string ItemDescription;
        public string ItemIngredients;
        public string ItemCalorie;
        public string ItemCost;
        public string ItemPrice;

        public string iName
        {
            get { return ItemName; }
        }

        public string iDescription
        {
            get { return ItemDescription; }
        }

        public string iIngredients
        {
            get { return ItemIngredients; }
        }

        public string iCalorie
        {
            get { return ItemCalorie; }
        }

        public string iCost
        {
            get { return ItemCost; }
        }

        public string iPrice
        {
            get { return ItemPrice; }
        }
    }

    public class AP_EM_ItemList
    {
        //Edit Menu Cards
        public static EMItemList[] mBuiltInCards = {
            new EMItemList { ItemName = "McChicken Sandwich",
                         ItemDescription = "Delectable cheap sandwiches with fake chicken.",
                         ItemIngredients = "Chicken, Lettuce, Bread, Mayo",
                         ItemCalorie = "420 blazin",
                         ItemCost = "$2.40" ,
                         ItemPrice = "$3.00"   },
            new EMItemList { ItemName = "Chipotle Burger",
                         ItemDescription = "E.Coli Sensitive.",
                         ItemIngredients = "Steak, Lettuce, Cheese, Avocado, Cream Cheese, Mayo",
                         ItemCalorie = "420 blaze it",
                         ItemCost = "$4.50" ,
                         ItemPrice = "$9.60"   },
            new EMItemList { ItemName = "Alan's Sandwiches",
                         ItemDescription = "The bane of NYIT Students.",
                         ItemIngredients = "Grilled Chicken, Lettuce, Cheese, Avocado, Cream Cheese, Mayo, Stale Bread",
                         ItemCalorie = "420 blaze it",
                         ItemCost = "$3.00" ,
                         ItemPrice = "$8.50"   },
            new EMItemList { ItemName = "Alan's Sandwiches",
                         ItemDescription = "The bane of NYIT Students.",
                         ItemIngredients = "Grilled Chicken, Lettuce, Cheese, Avocado, Cream Cheese, Mayo, Stale Bread",
                         ItemCalorie = "420 blaze it",
                         ItemCost = "$3.00" ,
                         ItemPrice = "$8.50"   },
        };

        // Array of Items
        private EMItemList[] mItems;

        // Load into array
        public AP_EM_ItemList ()
        {
            mItems = mBuiltInCards;
        } 

        // Number of items
        public int numItems
        {
            get { return mItems.Length; }
        }

        // Accessing a card
        public EMItemList this[int i]
        {
            get { return mItems[i]; }
        }
    }
}