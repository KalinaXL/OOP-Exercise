using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using OOP_Exercise.Login_And_Scrape_Data;
using OOP_Exercise.Utility_Classes;
using System;

namespace OOP_Exercise.Activities
{
    [Activity(Label = "BKStudent", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class SlashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.slash_screen);
            CheckSaveState();

            // Create your application here
        }
        async void CheckSaveState()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            bool isSaveInfo = prefs.GetBoolean("IsSaveInfo", false);
            //bool isSaveInfo = false;
            Handler handler = new Handler();
            Action action;
            if (isSaveInfo)
            {
                LoginConfig.IsLoadData = true;
                try
                {
                    bool isSuccess = await DatabaseUtility.GetInfoDatabase();
                    if (!isSuccess)
                        return;
                    action = new Action(() =>
                    {
                        Intent intent = new Intent(this, typeof(MainActivity));
                        StartActivity(intent);
                    }
                    );
                }
                catch
                {
                    return;
                }

            }
            else
            {
                LoginConfig.IsLoadData = false;
                action = new Action(() =>
                {
                    Intent intent = new Intent(this, typeof(LoginActivity));
                    StartActivity(intent);
                }
                );
            }
            handler.Post(action);
            Finish();
        }
    }
}