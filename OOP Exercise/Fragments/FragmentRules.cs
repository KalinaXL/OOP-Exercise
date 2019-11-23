using Android.Content.Res;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System.IO;

namespace OOP_Exercise.Fragments
{
    class FragmentRules : Fragment
    {
        TextView txt_rules;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_rules, container, false);
            txt_rules = view.FindViewById<TextView>(Resource.Id.txt_rules);
            AssetManager assets = Activity.Assets;
            using (StreamReader sr = new StreamReader(assets.Open("Rules.txt")))
            {
                txt_rules.Text = sr.ReadToEnd();
            }
            return view;
        }



    }
}