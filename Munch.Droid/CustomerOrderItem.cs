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

    public class CustomerOrderItem
    {
        public EMItemList Dish { get; set; }
       // public string Dish { get; set; }
        public string Quantity { get; set; }
        public string OrderNumber { get; set; }
        public string Notes { get; set; }       

    }

}