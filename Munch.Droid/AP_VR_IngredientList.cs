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
    class AP_VR_IngredientList
    {
        public string ingredientName { get; set; }
        public string unitMeasure { get; set; }
        public string amtUsed { get; set; }
        public string date { get; set; }
    }
}