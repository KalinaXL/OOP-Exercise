using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
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
    public class CustomExpandableListAdapter : BaseExpandableListAdapter
    {
        Context context;
        List<string> titleName;
        Dictionary<string, List<Subject>> Items;
        TextView titleHeaderName;
        TextView startTime;
        TextView endTime;
        TextView subjectName;
        TextView roomName;

        public CustomExpandableListAdapter(Context context, List<string> titleName,Dictionary<string,List<Subject>> items)
        {
            this.context = context;
            this.titleName = titleName;
            this.Items = items;
        }
        
        public override int GroupCount { get => titleName.Count; }

        public override bool HasStableIds { get => false; }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return Items[titleName[groupPosition]].Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var subjects = Items[titleName[groupPosition]][childPosition];

            /*Expand the view today
             *ExpandableListView mELV = parent as ExpandableListView;
             * mELV.ExpandGroup(groupPosition);
             * 
             */
            View view = convertView;
            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.listItemView, null, false);
            }
            

            var times = subjects.Time.Split('-');
            startTime = view.FindViewById<TextView>(Resource.Id.startTime);
            startTime.Text = times[0];

            endTime = view.FindViewById<TextView>(Resource.Id.endTime);
            endTime.Text = times[1];

            subjectName = view.FindViewById<TextView>(Resource.Id.subjectName);
            subjectName.Text = subjects.Name;

            roomName = view.FindViewById<TextView>(Resource.Id.roomName);
            roomName.Text = subjects.Room;
            return view;

        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return titleName[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.listHeaderView, null, false);
            }
            
            titleHeaderName = view.FindViewById<TextView>(Resource.Id.titleHeaderName);
            titleHeaderName.Text = titleName[groupPosition];
            return view;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
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