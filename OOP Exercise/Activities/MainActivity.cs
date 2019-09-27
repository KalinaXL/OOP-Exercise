using Android.App;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DataOfUser;
using OOP_Exercise.Fragments;
using OOP_Exercise.Resources.Fragments;

using SupportFragment = Android.Support.V4.App.Fragment;
namespace OOP_Exercise
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        SupportFragment currentFragment;
        FragmentScheduler fragScheduler;
        FragmentTest fragTest;
        FragmentExam fragExam;
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            
            navigation.SetOnNavigationItemSelectedListener(this);
            InitFragment();

            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.frameLayout, fragScheduler, "Fragment Scheduler");
            trans.Commit();

            currentFragment = fragScheduler;

            

        }
        void InitFragment()
        {
            fragScheduler = new FragmentScheduler();
            fragTest = new FragmentTest();
            fragExam = new FragmentExam();
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
                    
                    return true;
                case Resource.Id.navigation_exam:
                    ShowFragment(fragExam);
                  
                    return true;
                case Resource.Id.navigation_test:
                    ShowFragment(fragTest);
                    
                    return true;
                case Resource.Id.navigation_info:
                   //TO DO
                    return true;
                case Resource.Id.navigation_others:
                    //TO DO
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

