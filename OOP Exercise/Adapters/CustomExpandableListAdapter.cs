using System.Collections.Generic;
using System.Linq;


using Android.Content;
using Android.Views;
using Android.Widget;
using DataOfUser;

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

            //Expand the view today
             
            View view = convertView;
            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.listItemView, null, false);
            }
           
           

           
            startTime = view.FindViewById<TextView>(Resource.Id.startTime);
            startTime.Text = subjects.TimeStart;

            endTime = view.FindViewById<TextView>(Resource.Id.endTime);
            endTime.Text = subjects.TimeEnd;

            subjectName = view.FindViewById<TextView>(Resource.Id.subjectName);
            if (subjects.Name.Length > 30)
                subjectName.TextSize = 15;
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
}