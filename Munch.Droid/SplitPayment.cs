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
    [Activity(Label = "SplitPayment", Theme = "@android:style/Theme.Holo.Light.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.SensorLandscape)]
    public class SplitPayment : Activity
    {

        // current customer that is dragging orders
        int currentCustomer = 1;
        //list of lists<String> contains every customers order
        List<List<String>> splits = new List<List<string>>();

        
        TextView result;
        Button button2;
        protected override void OnCreate(Bundle bundle)
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SplitPaymentScreen);

            splits.Add(new List<String>());


            var button1 = FindViewById<Button>(Resource.Id.button1);
            
            RelativeLayout rl = FindViewById<RelativeLayout>(Resource.Id.buttonholder);


            List<CustomerOrderItem> orderList = CustomerPortal.CustomerOrderList;
            List<Button> btnList = new List<Button>();
            List<int> idList = new List<int>();
            idList.Add(button1.Id);

            RelativeLayout.LayoutParams lp = new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.WrapContent, RelativeLayout.LayoutParams.WrapContent);
            lp.AddRule(LayoutRules.CenterHorizontal);

            //add all of the items ordered to the first index of splits
            foreach (CustomerOrderItem x in CustomerPortal.CustomerOrderList)
            {
                int numQty = Int32.Parse(x.Quantity);
                Console.WriteLine(numQty + "YOLRWTERTREGDFSGSDGDSFGSDGDSFGSDG");
                while (numQty > 0)
                {
                    splits[0].Add(x.Dish.ToString());
                    btnList.Add(new Button(this));

                    Button lastButton = btnList.Last();
                    lastButton.Tag = x.Dish.iName + numQty;
                    lp.RemoveRule(LayoutRules.Below);


                    lp.AddRule(LayoutRules.Below, idList.Last());
                    lastButton.LayoutParameters = lp;
                    lastButton.Text = x.Dish.iName;
                    lastButton.LongClick += Button_LongClickEvent;
                    idList.Add(lastButton.Id);
                    numQty--;

                }
                
            }


            Console.WriteLine(btnList.Count() +  " GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
            //add all the buttons to the relativelayout
            foreach (Button btn in btnList)
            {
                rl.AddView(btn);
            }


            /*
            var button1 = FindViewById<Button>(Resource.Id.button1);
            
            Button button3 = new Button(this);
            
            
            
            button3.LayoutParameters = lp;
            
            rl.AddView(button3);
            button3.Text = "woo";
            // Get UI elements out of the layout
            
            
            button1.LongClick += Button_LongClickEvent;
            button2 = FindViewById<Button>(Resource.Id.button2);
            button2.LongClick += Button_LongClickEvent;
            button3.LongClick += Button_LongClickEvent;

            */
            var dropZone = FindViewById<FrameLayout>(Resource.Id.dropzone);
            result = FindViewById<TextView>(Resource.Id.result);

            // Attach event to drop zone
            dropZone.Drag += DropZone_Drag;
            
            base.OnCreate(bundle);
        }

        void Button_LongClickEvent(object sender, View.LongClickEventArgs e)
        {
            Button b = (Button) sender;
            var data = ClipData.NewPlainText("name", b.Text.ToString());
            ((sender) as Button).StartDrag(data, new View.DragShadowBuilder(((sender) as Button)), null, 0);
        }
 
        void DropZone_Drag(object sender, View.DragEventArgs e)
        {
            // React on different dragging events
            var evt = e.Event;
            switch (evt.Action)
            {
                case DragAction.Ended:
                case DragAction.Started:
                    e.Handled = true;
                    break;
                // Dragged element enters the drop zone
                case DragAction.Entered:
                    result.Text = "Drop it like it's hot!";
                    break;
                // Dragged element exits the drop zone
                case DragAction.Exited:
                    result.Text = "Customer " + currentCustomer;
                    break;
                // Dragged element has been dropped at the drop zone
                case DragAction.Drop:
                    // You can check if element may be dropped here
                    // If not do not set e.Handled to true
                    e.Handled = true;

                    // Try to get clip data
                    var data = e.Event.ClipData;
                    if (data != null)
                        result.Text = data.GetItemAt(0).Text + " has been dropped.";
                    splits[currentCustomer - 1].Add(data.GetItemAt(0).Text);
                    int num = splits[currentCustomer - 1].Count();
                    Console.WriteLine(num);
                    break;
            }

    
        }

               
    }
}