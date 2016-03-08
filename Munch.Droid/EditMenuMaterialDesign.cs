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

namespace Munch
{
    public class MainEditMenuMaterialDesign : Activity
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        private List<EditMenu> mMenu;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.EditMenuMaterialDesign);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mMenu = new List<EditMenu>();
            mMenu.Add(new EditMenu() { Name = "Pizza", Categlory = "Lunch", Price = "$8" });

            //Create our layout manager
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerAdapter(mMenu, mRecyclerView, this);
            mRecyclerView.SetAdapter(mAdapter);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.values.actionbar, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.add:
                    //Add button clicked
                    mMenu.Add(new EditMenu() { Name = "New Name", Categlory = "New Categlory", Price = "New Price" });
                    mAdapter.NotifyItemInserted(mMenu.Count - 1);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }

    public class RecyclerAdapter : RecyclerView.Adapter
    {
        private List<EditMenu> mMenu;
        private RecyclerView mRecyclerView;
        private Context mContext;
        private int mCurrentPosition = -1;

        public RecyclerAdapter(List<EditMenu> menu, RecyclerView recyclerView, Context context)
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
                return Resource.Layout.EditMenu_Row1;
            }

            else
            {
                //Odd number
                return Resource.Layout.EditMenu_Row2;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == Resource.Layout.EditMenu_Row1)
            {
                //First card view
                View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.EditMenu_Row1, parent, false);

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
                View row = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.EditMenu_Row2, parent, false);
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

