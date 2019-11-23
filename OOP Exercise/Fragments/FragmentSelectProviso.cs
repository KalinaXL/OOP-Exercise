using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;

namespace OOP_Exercise.Fragments
{
    public class FragmentSelectProviso : Fragment
    {
        public static EventHandler ClickSelectProviso;
        LinearLayout Intro;
        LinearLayout Rules;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_select_proviso, container, false);
            Intro = view.FindViewById<LinearLayout>(Resource.Id.layout_intro);
            Rules = view.FindViewById<LinearLayout>(Resource.Id.layout_rules);
            Intro.Click += Intro_Click;
            Rules.Click += Rules_Click;
            return view;
        }



        private void Rules_Click(object sender, EventArgs e)
        {
            FragmentRules fragRules;
            fragRules = new FragmentRules();
            var trans = FragmentManager.BeginTransaction();
            if (fragRules.IsVisible)
                return;
            trans.Replace(Resource.Id.fragment_proviso, fragRules, "Select Rules");
            trans.AddToBackStack(null);
            trans.CommitAllowingStateLoss();
        }

        private void Intro_Click(object sender, EventArgs e)
        {
            FragmentIntro fragIntro;
            fragIntro = new FragmentIntro();
            var trans = FragmentManager.BeginTransaction();
            if (fragIntro.IsVisible)
                return;
            trans.Replace(Resource.Id.fragment_proviso, fragIntro, "Select Intro");
            trans.AddToBackStack(null);
            trans.CommitAllowingStateLoss();

        }



    }
}