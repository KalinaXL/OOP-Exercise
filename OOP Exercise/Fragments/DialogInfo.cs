using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace OOP_Exercise.Fragments
{
    class DialogInfo:DialogFragment
    {
        ProgressBar progressbar;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.dialog_info, container, false);
            progressbar = view.FindViewById<ProgressBar>(Resource.Id.progress_bar_info);
            return view;
        }
  
    }
}