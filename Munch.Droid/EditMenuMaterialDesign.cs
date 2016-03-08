using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Views.Animations;
using Android.Animation;

<<<<<<< HEAD
namespace Munch
{
    public class MainEditMenuMaterialDesign : Activity
=======
namespace RecyclerViewTutorial
{
    [Activity(Label = "Munch", Icon = "@drawable/icon")]
    public class MainActivity : Activity
>>>>>>> origin/Anthony'sCode
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
<<<<<<< HEAD
        private List<EditMenu> mMenu;
=======
        private List<Menu> mMenu;
>>>>>>> origin/Anthony'sCode


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
<<<<<<< HEAD
            SetContentView(Resource.Layout.EditMenuMaterialDesign);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mMenu = new List<EditMenu>();
            mMenu.Add(new EditMenu() { Name = "Pizza", Categlory = "Lunch", Price = "$8" });
=======
            SetContentView(Resource.Layout.Main);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mMenu = new List<Menu>();
            mMenu.Add(new Menu() { Name = "Pizza", Categlory = "Lunch", Price = "$8" });
>>>>>>> origin/Anthony'sCode

            //Create our layout manager
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerAdapter(mMenu, mRecyclerView, this);
            mRecyclerView.SetAdapter(mAdapter);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
<<<<<<< HEAD
            MenuInflater.Inflate(Resource.values.actionbar, menu);
=======
            MenuInflater.Inflate(Resource.Menu.actionbar, menu);
>>>>>>> origin/Anthony'sCode
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.add:
                    //Add button clicked
<<<<<<< HEAD
                    mMenu.Add(new EditMenu() { Name = "New Name", Categlory = "New Categlory", Price = "New Price" });
=======
                    mMenu.Add(new Menu() { Name = "New Name", Categlory = "New Categlory", Price = "New Price" });
>>>>>>> origin/Anthony'sCode
                    mAdapter.NotifyItemInserted(mMenu.Count - 1);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }

    public class RecyclerAdapter : RecyclerView.Adapter
    {
<<<<<<< HEAD
        private List<EditMenu> mMenu;
=======
        private List<Menu> mMenu;
>>>>>>> origin/Anthony'sCode
        private RecyclerView mRecyclerView;
        private Context mContext;
        private int mCurrentPosition = -1;

<<<<<<< HEAD
        public RecyclerAdapter(List<EditMenu> menu, RecyclerView recyclerView, Context context)
=======
        public RecyclerAdapter(List<Menu> menu, RecyclerView recyclerView, Context context)
>>>>>>> origin/Anthony'sCode
        {
            mMenu = menu;
            mRecyclerView = recyclerView;
            mContext = context;
        }

        public class MyView : RecyclerView.ViewHolder
        {
            public View mMainView { get; set; }
            public TextView mName { get; set; }
            public TextView mCateglory { get; set; }
            public TextView mPrice { get; set; }

            public MyView(View view) : base(view)
            {
                mMainView = view;
            }
        }

        public class MyView2 : RecyclerView.ViewHolder
        {
            public View mMainView { get; set; }

            public MyView2(View view) : base(view)
            {
                mMainView = view;
            }
        }

        public override int GetItemViewType(int position)
        {
            if ((position % 2) == 0)
            {
                //Even number
<<<<<<< HEAD
                return Resource.Layout.EditMenu_Row1;
=======
                return Resource.Layout.row;
>>>>>>> origin/Anthony'sCode
            }

            else
            {
                //Odd number
<<<<<<< HEAD
                return Resource.Layout.EditMenu_Row2;
=======
                return Resource.Layout.row2;
>>>>>>> origin/Anthony'sCode
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
<<<<<<< HEAD
            if (viewType == Resource.Layout.EditMenu_Row1)
            {
                //First card view
                View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.EditMenu_Row1, parent, false);
=======
            if (viewType == Resource.Layout.row)
            {
                //First card view
                View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row, parent, false);
>>>>>>> origin/Anthony'sCode

                TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
                TextView txtCateglory = row.FindViewById<TextView>(Resource.Id.txtCateglory);
                TextView txtPrice = row.FindViewById<TextView>(Resource.Id.txtPrice);
                TextView txtSell_Price = row.FindViewById<TextView>(Resource.Id.txtSell_Price);

                MyView view = new MyView(row) { mName = txtName, mCateglory = txtCateglory, mPrice = txtPrice };
                return view;
            }

            else
            {
                //Second card view
<<<<<<< HEAD
                View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.EditMenu_Row2, parent, false);
=======
                View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.row2, parent, false);
>>>>>>> origin/Anthony'sCode
                MyView2 view = new MyView2(row);
                return view;
            }

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is MyView)
            {
                //First view
                MyView myHolder = holder as MyView;
                myHolder.mMainView.Click += mMainView_Click;
                myHolder.mName.Text = mMenu[position].Name;
                myHolder.mCateglory.Text = mMenu[position].Categlory;
                myHolder.mPrice.Text = mMenu[position].Price;

                if (position > mCurrentPosition)
                {
                    int currentAnim = Resource.Animation.slide_left_to_right;
                    SetAnimation(myHolder.mMainView, currentAnim);
                    mCurrentPosition = position;
                }
            }

            else
            {
                //Second View
                MyView2 myHolder = holder as MyView2;
                if (position > mCurrentPosition)
                {
                    int currentAnim = Resource.Animation.slide_right_to_left;
                    SetAnimation(myHolder.mMainView, currentAnim);
                    mCurrentPosition = position;
                }
            }

        }

        private void SetAnimation(View view, int currentAnim)
        {
            Animator animator = AnimatorInflater.LoadAnimator(mContext, Resource.Animation.flip);
            animator.SetTarget(view);
            animator.Start();
            //Animation anim = AnimationUtils.LoadAnimation(mContext, currentAnim);
            //view.StartAnimation(anim);
        }

        void mMainView_Click(object sender, EventArgs e)
        {
            int position = mRecyclerView.GetChildPosition((View)sender);
            Console.WriteLine(mMenu[position].Name);
        }

        public override int ItemCount
        {
            get { return mMenu.Count; }
        }
    }
}

