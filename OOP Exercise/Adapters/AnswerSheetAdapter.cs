using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace OOP_Exercise.Adapters
{
    class AnswerSheetAdapter : RecyclerView.Adapter
    {
        Context context;
        List<CurrentQuestion> currQuesList;

        public AnswerSheetAdapter(Context context, List<CurrentQuestion> currQues)
        {
            this.context = context;
            this.currQuesList = currQues;
        }

        public override int ItemCount
        {
            get => currQuesList.Count;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewAnswer myviewAns = holder as MyViewAnswer;
            if (currQuesList[position].IsAnswered)
            {
                myviewAns.ques_item.SetBackgroundResource(Resource.Drawable.ques_has_ans);
            }
            else
                myviewAns.ques_item.SetBackgroundResource(Resource.Drawable.ques_no_ans);

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(this.context).Inflate(Resource.Layout.layout_grid_ans_sheet_item, parent, false);
            return new MyViewAnswer(view);
        }

        public class MyViewAnswer : RecyclerView.ViewHolder
        {
            public View ques_item;
            public MyViewAnswer(View itemView) : base(itemView)
            {
                ques_item = itemView.FindViewById<View>(Resource.Id.question_item);
            }
        }
    }
}