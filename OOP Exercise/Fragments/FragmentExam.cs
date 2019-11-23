using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using OOP_Exercise.Adapters;
using OOP_Exercise.Utility_Classes;

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



            rcViewMidTerm.SetAdapter(new ExamSchedulerAdapter(this.Activity, SaveInfo.examMidList));
            rcViewFinalTerm.SetAdapter(new ExamSchedulerAdapter(this.Activity, SaveInfo.examFinalList));


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