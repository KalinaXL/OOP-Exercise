using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using OOP_Exercise.Login_And_Scrape_Data;
using OOP_Exercise.Utility_Classes;
using System.Collections.Generic;
using System.Linq;

namespace OOP_Exercise.Fragments
{
    public class FragmentSelectSubjects : Fragment
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        RecyclerView recyclerView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_select_subject, container, false);
            toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Chọn môn";

            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new GridLayoutManager(this.Activity, 2));

            //List<string> subjectList = (from item in LoginManager.Scheduler[0].tkb where (item.ten_mh[0] != 'P') select item.ten_mh).ToList();
            List<string> subjectList;
            if (LoginManager.IsLoadData)
            {
                subjectList = (from item in SaveInfo.subjectList where !item.Name.Contains("(mr") && !item.Name.Contains("(th") && !item.Name.Contains("(bt")  select item.Name).ToList();
            }
            else
            {
                subjectList = (from item in LoginManager.Scheduler[0].tkb  select item.ten_mh).ToList();
            }

            SubjectAdapter adapter = new SubjectAdapter(this.Activity, subjectList);
            recyclerView.AddItemDecoration(new SpaceDecoration(20));
            recyclerView.SetAdapter(adapter);
            return view;
        }
    }
}