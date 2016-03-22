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