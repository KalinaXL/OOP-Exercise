using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Login_And_Scrape_Data;
using OOP_Exercise.Utility_Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Exercise.Resources.Fragments
{
    public class FragmentScheduler : Fragment
    {
        Toolbar toolbar;
        ExpandableListView expandListView;
        List<string> titleNames;
        ImageView chevronLeft;
        ImageView chevronRight;
        CustomExpandableListAdapter adapter;
        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // GetData();
            titleNames = SaveInfo.SchedulerOfDay.Keys.ToList();
  
            // Create your fragment here
        }

      
        void UpdateUI()
        {
            toolbar.Title = "Lịch học tuần " + LoginDataManager.CurrentWeekOfYear;
            toolbar.SetTitleTextColor(Color.White);
            adapter = new CustomExpandableListAdapter(this.Activity, titleNames, SaveInfo.SchedulerOfDay);
            expandListView.SetAdapter(adapter);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_scheduler, container, false);
            FindIdOfWidget(view);
            chevronLeft.Click += ChevronLeft_Click;
            chevronRight.Click += ChevronRight_Click;

            toolbar.Title = "Lịch học tuần " + LoginDataManager.CurrentWeekOfYear;
            toolbar.SetTitleTextColor(Color.White);
            adapter = new CustomExpandableListAdapter(this.Activity, titleNames, SaveInfo.SchedulerOfDay);
            expandListView.SetAdapter(adapter);
            return view;


        }
        void FindIdOfWidget(View view)
        {
            expandListView = view.FindViewById<ExpandableListView>(Resource.Id.expandableListView);
            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbarSheduler);
            chevronLeft = view.FindViewById<ImageView>(Resource.Id.chevron_left);
            chevronRight = view.FindViewById<ImageView>(Resource.Id.chevron_right);
            chevronLeft.Click += ChevronLeft_Click;
        }
        private void ChevronRight_Click(object sender, EventArgs e)
        {
            ++LoginDataManager.CurrentWeekOfYear;
            DatabaseUtility.GetDataScheduler();
            titleNames = SaveInfo.SchedulerOfDay.Keys.ToList();
            UpdateUI();
        }

        private void ChevronLeft_Click(object sender, EventArgs e)
        {
            --LoginDataManager.CurrentWeekOfYear;
            DatabaseUtility.GetDataScheduler();
            titleNames = SaveInfo.SchedulerOfDay.Keys.ToList();
            UpdateUI();
        }
    }
}