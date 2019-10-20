using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Utility_Classes;
using System;

namespace OOP_Exercise.Fragments
{
    public class FragmentSelectTerm : Fragment
    {
        public static EventHandler ClickSelectTerm;
        LinearLayout midTerm;
        LinearLayout finalTerm;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_select_term, container, false);
            midTerm = view.FindViewById<LinearLayout>(Resource.Id.layout_mid_term);
            finalTerm = view.FindViewById<LinearLayout>(Resource.Id.layout_final_term);
            midTerm.Click += MidTerm_Click;
            finalTerm.Click += FinalTerm_Click;
            return view;
        }



        private void FinalTerm_Click(object sender, EventArgs e)
        {
            DataManager.IsMidTerm = false;
            ClickSelectTerm(sender, e);
        }

        private void MidTerm_Click(object sender, EventArgs e)
        {
            DataManager.IsMidTerm = true;
            ClickSelectTerm(sender, e);
        }
    }

}