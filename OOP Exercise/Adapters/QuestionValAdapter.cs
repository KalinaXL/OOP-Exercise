using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Utility_Classes;

namespace OOP_Exercise.Adapters
{
    class QuestionValAdapter : RecyclerView.Adapter
    {
        Context context;
        StateOfQuestion[] stateOfQues;
        public QuestionValAdapter(Context context, StateOfQuestion[] states)
        {
            this.context = context;
            stateOfQues = states;
        }
        public override int ItemCount
        {
            get => stateOfQues.Length;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewVal myView = holder as MyViewVal;
            myView.txtQues.Text = $"Câu hỏi {position + 1}";

            switch (stateOfQues[position])
            {
                case StateOfQuestion.RIGHT:
                    myView.txtQues.SetBackgroundResource(Resource.Drawable.right_question_style);
                    break;
                case StateOfQuestion.WRONG:
                    myView.txtQues.SetBackgroundResource(Resource.Drawable.wrong_question_style);
                    break;
                case StateOfQuestion.MISSED:
                    myView.txtQues.SetBackgroundResource(Resource.Drawable.missed_question_style);
                    break;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.layout_question_val, parent, false);
            MyViewVal myView = new MyViewVal(view);

            return myView;
        }



        public class MyViewVal : RecyclerView.ViewHolder
        {
            public TextView txtQues;

            public MyViewVal(View itemView) : base(itemView)
            {
                txtQues = itemView.FindViewById<TextView>(Resource.Id.txtQuesValId);
            }

        }
    }


}