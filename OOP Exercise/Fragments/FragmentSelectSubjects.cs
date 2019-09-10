using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;

using Android.Util;
using Android.Views;

using Android.Widget;

namespace OOP_Exercise.Fragments
{

    public class SubjectAdapter : RecyclerView.Adapter
    {
        Context context;
        List<string> subjects;
        public SubjectAdapter(Context context, List<string> subjects)
        {
            this.context = context;
            this.subjects = subjects;
        }
        public override int ItemCount
        {
            get => subjects.Count;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewSubject myView = holder as MyViewSubject;
            myView.subjectName.Text = subjects[position];
        }



        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.layout_subject_item, parent, false);
            return new MyViewSubject(view);
        }

        public class MyViewSubject : RecyclerView.ViewHolder
        {
            public CardView cardSubject;
            public TextView subjectName;

            public MyViewSubject(View itemView) : base(itemView)
            {
                cardSubject = itemView.FindViewById<CardView>(Resource.Id.card_subject);
                subjectName = itemView.FindViewById<TextView>(Resource.Id.txt_subject_name);
                // cardSubject.SetOnClickListener(new View.IOnClickListener);
                cardSubject.Click += CardSubject_Click;
            }

            private void CardSubject_Click(object sender, EventArgs e)
            {
                subjectName.Visibility = ViewStates.Invisible;
            }
        }


    }
    public class FragmentSelectSubjects : Fragment
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        RecyclerView recyclerView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_select_subject, container, false);
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Chọn môn";

            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new GridLayoutManager(this.Activity, 2));

            SubjectAdapter adapter = new SubjectAdapter(this.Activity, new List<string> { "Giải tích 1", "Hóa đại cương", "Vật lí 1", "Cấu trúc rời rạc","Hệ thống số","Nhập môn điện toán" });
            recyclerView.AddItemDecoration(new SpaceDecoration(20));
            recyclerView.SetAdapter(adapter);
            return view;
        }
    }
    public class SpaceDecoration : RecyclerView.ItemDecoration
    {
        int space;
        public SpaceDecoration(int space)
        {
            this.space = space;
        }
        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            outRect.Left = outRect.Right = outRect.Top = outRect.Bottom = space;
        }
    }
}