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

namespace OOP_Exercise.Fragments
{
    class CustomGridView : BaseAdapter<string>
    {
        Context context;
        List<string> subjects;
        TextView txtSubject;
        public CustomGridView(Context context, List<string> subjects)
        {
            this.context = context;
            this.subjects = subjects;
        }
        public override string this[int position] { get => subjects[position]; }

        public override int Count { get => subjects.Count; }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = LayoutInflater.From(context).Inflate(Resource.Layout.itemSubjectGridView, null, false);
            txtSubject = view.FindViewById<TextView>(Resource.Id.txtSubjectGridView);
            txtSubject.Text = subjects[position];
            return view;
        }
    }
    public class FragmentSelectSubjects : Fragment
    {
        GridView gridSubjects;
        List<string> subjects;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            subjects = new List<string>() { "A", "B", "C","D","E","F" };
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_select_subject, container, false);
            gridSubjects = view.FindViewById<GridView>(Resource.Id.gridViewSubjects);
            CustomGridView adapter = new CustomGridView(this.Activity, subjects);
            gridSubjects.Adapter = adapter;
            return view;
        }
    }
}