using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

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
            MyViewSubject myview = new MyViewSubject(view);
            myview.ClickSelectSubject += Myview_ClickSelectSubject;
            return myview;
        }

        private void Myview_ClickSelectSubject(object sender, EventArgs e)
        {
            Handler h = new Handler();
            Toast.MakeText(this.context, "Đang tải dữ liệu...", ToastLength.Long).Show();
            Action action = async () =>
            {
                Intent intent = new Intent(this.context, typeof(QuizActivity));
                Bundle bundle = new Bundle();
                bundle.PutString("SubjectName", (sender as TextView).Text);
                intent.PutExtras(bundle);
                this.context.StartActivity(intent);
            };
            h.Post(action);
        }

        public class MyViewSubject : RecyclerView.ViewHolder
        {
            public CardView cardSubject;
            public TextView subjectName;
            public event EventHandler ClickSelectSubject;

            public MyViewSubject(View itemView) : base(itemView)
            {
                cardSubject = itemView.FindViewById<CardView>(Resource.Id.card_subject);
                subjectName = itemView.FindViewById<TextView>(Resource.Id.txt_subject_name);
                // cardSubject.SetOnClickListener(new View.IOnClickListener);
                cardSubject.Click += CardSubject_Click;
            }

            private void CardSubject_Click(object sender, EventArgs e)
            {
                //subjectName.Visibility = ViewStates.Invisible;
                ClickSelectSubject(subjectName, e);

            }
        }



    }
}