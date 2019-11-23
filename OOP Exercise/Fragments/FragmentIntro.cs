using Android.Content.Res;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System.IO;

namespace OOP_Exercise.Fragments
{
    class FragmentIntro : Fragment
    {
        TextView txt_intro;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_intro, container, false);
            txt_intro = view.FindViewById<TextView>(Resource.Id.txt_intro);
            AssetManager assets = Activity.Assets;
            using (StreamReader sr = new StreamReader(assets.Open("Intro.txt")))
            {
                txt_intro.Text = sr.ReadToEnd();
            }
            return view;
        }

    }
}