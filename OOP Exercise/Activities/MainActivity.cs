using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Fragments;
using OOP_Exercise.Resources.Fragments;
using SupportFragment = Android.Support.V4.App.Fragment;
namespace OOP_Exercise
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        SupportFragment currentFragment;
        FragmentScheduler fragScheduler;
        FragmentTest fragTest;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            
            navigation.SetOnNavigationItemSelectedListener(this);
            fragScheduler = new FragmentScheduler();
            fragTest = new FragmentTest();

            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.frameLayout, fragScheduler, "Fragment Scheduler");
            trans.Commit();

            currentFragment = fragScheduler;
           



        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_scheduler:
                    ShowFragment(fragScheduler);
                    //textMessage.SetText(Resource.String.title_home);
                    return true;
                case Resource.Id.navigation_exam:
                   // textMessage.SetText(Resource.String.title_dashboard);
                    return true;
                case Resource.Id.navigation_test:
                    ShowFragment(fragTest);
                    //textMessage.SetText(Resource.String.title_notifications);
                    return true;
                case Resource.Id.navigation_info:
                    //textMessage.SetText(Resource.String.title_notifications);
                    return true;
            }
            return false;
        }

        void ShowFragment(SupportFragment fragment)
        {
            if (fragment.IsVisible)
                return;
            RunOnUiThread(()=>
            {
                var trans = SupportFragmentManager.BeginTransaction();
                trans.Replace(Resource.Id.frameLayout, fragment);
                trans.AddToBackStack(null);
                trans.Commit();
                 currentFragment = fragment;
            }
            );

           
        }
    }
}

