using System;
using System.Collections.Generic;
using System.Text;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;

using Android.Util;
using Android.Views;
using Android.Widget;
using DataOfUser;
using Java.Lang;

namespace OOP_Exercise.Resources.Fragments
{
    public class FragmentScheduler : Fragment
    {
        ExpandableListView expandListView;
        List<string> titleNames;
        Dictionary<string, List<Subject>> schedulerOfDay;
       
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            titleNames = new List<string>() { "Thứ 2","Thứ 3","Thứ 4" };
            schedulerOfDay = new Dictionary<string, List<Subject>>();
            List<Subject> subs = new List<Subject>() { new Subject("OOP","H6-114","07:00-10:00") , new Subject("DSA","H2-206","07:00-10:00") };
            schedulerOfDay.Add("Thứ 2", subs);
            schedulerOfDay.Add("Thứ 3", subs);
            schedulerOfDay.Add("Thứ 4", subs);

           
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_scheduler, container, false);
            expandListView = view.FindViewById<ExpandableListView>(Resource.Id.expandableListView);
            CustomExpandableListAdapter adapter = new CustomExpandableListAdapter(this.Activity, titleNames, schedulerOfDay);
            expandListView.SetAdapter(adapter);
            return view;
          
           
        }
    }
}