using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;

using Android.Util;
using Android.Views;
using Android.Widget;
using DataOfUser;
using Java.Lang;
using OOP_Exercise.Login_And_Scrape_Data;

namespace OOP_Exercise.Resources.Fragments
{
    public class FragmentScheduler : Fragment
    {
        Toolbar toolbar;
        ExpandableListView expandListView;
        List<string> titleNames;
        ImageView chevronLeft;
        ImageView chevronRight;
        Dictionary<string, List<Subject>> schedulerOfDay;
        CustomExpandableListAdapter adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // List<Subject> subs = new List<Subject>() { new Subject("OOP","H6-114","07:00-10:00") , new Subject("DSA","H2-206","07:00-10:00") };
            //schedulerOfDay.Add("Thứ 2", subs);
            //schedulerOfDay.Add("Thứ 3", subs);
            //schedulerOfDay.Add("Thứ 4", subs);

            GetData();

           
            // Create your fragment here
        }

        void GetData()
        {
            string week = LoginManager.CurrentWeekOfYear.ToString();
            schedulerOfDay = LoginManager.Scheduler[0].tkb
                                .Where(item => item.tuan_hoc.Contains(week))
                                .Select(item => new { K = "Thứ " + item.thu1, Subject = new Subject(item.ten_mh, item.phong1, item.giobd, item.giokt) })
                                .Distinct()
                                .GroupBy(item => item.K).ToDictionary(item => item.Key, item => item.Select(item2 => item2.Subject).ToList());
            titleNames = schedulerOfDay.Keys.ToList();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_scheduler, container, false);
            expandListView = view.FindViewById<ExpandableListView>(Resource.Id.expandableListView);
            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbarSheduler);
            chevronLeft = view.FindViewById<ImageView>(Resource.Id.chevron_left);
            chevronRight = view.FindViewById<ImageView>(Resource.Id.chevron_right);
            chevronLeft.Click += ChevronLeft_Click;
            chevronRight.Click += ChevronRight_Click;

            toolbar.Title = "Lịch học tuần "+LoginManager.CurrentWeekOfYear;
            toolbar.SetTitleTextColor(Color.White);
            adapter = new CustomExpandableListAdapter(this.Activity, titleNames, schedulerOfDay);
            expandListView.SetAdapter(adapter);
            return view;
          
           
        }

        private void ChevronRight_Click(object sender, EventArgs e)
        {
            ++LoginManager.CurrentWeekOfYear;
            GetData();
            toolbar.Title = "Lịch học tuần " + LoginManager.CurrentWeekOfYear;
            toolbar.SetTitleTextColor(Color.White);
            adapter = new CustomExpandableListAdapter(this.Activity, titleNames, schedulerOfDay);
            expandListView.SetAdapter(adapter);
        }

        private void ChevronLeft_Click(object sender, EventArgs e)
        {
            --LoginManager.CurrentWeekOfYear;
            GetData();
            toolbar.Title = "Lịch học tuần " + LoginManager.CurrentWeekOfYear;
            toolbar.SetTitleTextColor(Color.White);
            adapter = new CustomExpandableListAdapter(this.Activity, titleNames, schedulerOfDay);
            expandListView.SetAdapter(adapter);
            

        }
    }
}