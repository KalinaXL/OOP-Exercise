using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Adapters;
using OOP_Exercise.Utility_Classes;
using OOP_Exercise.Login_And_Scrape_Data;

namespace OOP_Exercise.Fragments
{
    public class FragmentExam : Fragment
    {
        RecyclerView rcViewMidTerm;
        RecyclerView rcViewFinalTerm;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_exam_scheduler, container, false);
            FindWidgetFromID(view);

            rcViewMidTerm.SetLayoutManager(new GridLayoutManager(this.Activity, 1));
            rcViewFinalTerm.SetLayoutManager(new GridLayoutManager(this.Activity, 1));

            List<ExamScheduler> examMidList = (from item in LoginManager.Exam._20191.lichthi select new ExamScheduler() { Hour = item.gio_kt, Date = item.ngaykt,SubjectName = item.ten_mh,Room = item.phong_ktra}).ToList();

            List<ExamScheduler> examFinalList = (from item in LoginManager.Exam._20191.lichthi select new ExamScheduler() {Hour = item.gio_thi, Date = item.ngaythi,SubjectName = item.ten_mh,Room = item.phong_thi }).ToList();



            rcViewMidTerm.SetAdapter(new ExamSchedulerAdapter(this.Activity, examMidList));
            rcViewFinalTerm.SetAdapter(new ExamSchedulerAdapter(this.Activity, examFinalList));


            rcViewMidTerm.AddItemDecoration(new SpaceDecoration(10));
            rcViewFinalTerm.AddItemDecoration(new SpaceDecoration(10));

            return view;
        }
        void FindWidgetFromID(View view)
        {
            rcViewMidTerm = view.FindViewById<RecyclerView>(Resource.Id.rcViewMidTerm);
            rcViewFinalTerm = view.FindViewById<RecyclerView>(Resource.Id.rcViewFinalTerm);

        }
    }
}