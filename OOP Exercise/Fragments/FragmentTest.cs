using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using System;

namespace OOP_Exercise.Fragments
{
    public class FragmentTest : Fragment
    {
        FragmentSelectTerm fragSelectTerm;
        FragmentSelectSubjects fragSelectSuject;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            fragSelectTerm = new FragmentSelectTerm();
            fragSelectSuject = new FragmentSelectSubjects();
            FragmentSelectTerm.ClickSelectTerm += FragSelectTerm_ClickSelectTerm;

            // Create your fragment here
        }

        private void FragSelectTerm_ClickSelectTerm(object sender, EventArgs e)
        {
            var trans = FragmentManager.BeginTransaction();
            if (fragSelectSuject.IsVisible)
                return;
            trans.Replace(Resource.Id.fragment_test, fragSelectSuject, "Select subject");
            trans.AddToBackStack(null);
            trans.Commit();
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_first_quiz, container, false);
            var trans = FragmentManager.BeginTransaction();
            if (FragmentManager.FindFragmentByTag("Select term") != null)
            {
                fragSelectTerm = new FragmentSelectTerm();
                trans.Replace(Resource.Id.fragment_test, fragSelectTerm);
                trans.AddToBackStack(null);
                trans.Commit();
                return view;
            }
            trans.Add(Resource.Id.fragment_test, fragSelectTerm, "Select term");
            trans.Commit();
            return view;
        }


    }
}