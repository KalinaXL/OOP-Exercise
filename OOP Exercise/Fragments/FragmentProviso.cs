using Android.OS;
using Android.Support.V4.App;
using Android.Views;


namespace OOP_Exercise.Fragments
{
    class FragmentProviso : Fragment
    {
        FragmentSelectProviso fragSelectProviso;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            fragSelectProviso = new FragmentSelectProviso();


            // Create your fragment here
        }

 

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_proviso, container, false);
            var trans = FragmentManager.BeginTransaction();
            if (FragmentManager.FindFragmentByTag("Select proviso") != null)
            {
                fragSelectProviso = new FragmentSelectProviso();
                trans.Replace(Resource.Id.fragment_proviso, fragSelectProviso);
                trans.AddToBackStack(null);
                trans.Commit();
                return view;
            }
            trans.Add(Resource.Id.fragment_proviso, fragSelectProviso, "Select proviso");
            trans.Commit();
            return view;
        }



    }
}