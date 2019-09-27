using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Transitions;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Login_And_Scrape_Data;
using OOP_Exercise.Utility_Classes;

namespace OOP_Exercise.Adapters
{
    class ExamSchedulerAdapter : RecyclerView.Adapter
    {
        Context context;
        List<ExamScheduler> examSchedulers;
        public ExamSchedulerAdapter(Context context, List<ExamScheduler> examSchedulers)
        {
            this.context = context;
            this.examSchedulers = examSchedulers;
        }
        public override int ItemCount
        {
            get => examSchedulers.Count;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder view = holder as MyViewHolder;
            view.txtExSubjectName.Text = examSchedulers[position].SubjectName;
            view.txtExRoom.Text = examSchedulers[position].Room;
            if (examSchedulers[position].Hour != null)
            view.txtExHour.Text = examSchedulers[position].Hour.Replace('g',':');
            if (!string.IsNullOrEmpty(examSchedulers[position].Date))
            {
                var time = examSchedulers[position].Date.Split("/");
                view.txtExDate.Text = time[0];
                try
                {
                    view.txtExMonthYear.Text = time[1] + "/";
                    if (int.Parse(time[1]) < 2)
                        view.txtExMonthYear.Text += (LoginManager.Year + 1).ToString();
                    else
                        view.txtExMonthYear.Text += LoginManager.Year.ToString();
                }
                catch { }
            }
        }

       

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(context).Inflate(Resource.Layout.layout_exem_item,parent,false);
            TextView txtSubName = view.FindViewById<TextView>(Resource.Id.txtExSubjectName);
            TextView txtRoom = view.FindViewById<TextView>(Resource.Id.txtExRoom);
            TextView txtDate = view.FindViewById<TextView>(Resource.Id.txtExDate);
            TextView txtMonthYear = view.FindViewById<TextView>(Resource.Id.txtExMonth);
            TextView txtHour = view.FindViewById<TextView>(Resource.Id.txtExTime);
            return new MyViewHolder(view) { txtExSubjectName = txtSubName, txtExRoom = txtRoom,txtExHour = txtHour,txtExMonthYear = txtMonthYear,txtExDate = txtDate };
        }

        public class MyViewHolder : RecyclerView.ViewHolder
        {
            public TextView txtExSubjectName { get; set; }
           
            public TextView txtExRoom { get; set; }
            public TextView txtExHour { get; set; }
            public TextView txtExMonthYear { get; set;}
            public TextView txtExDate { get; set; }
            
            public MyViewHolder(View itemView) : base(itemView)
            {
                
            }
        }
    }


}