using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using OOP_Exercise.Login_And_Scrape_Data;
using OOP_Exercise.Utility_Classes;
using System;

namespace OOP_Exercise.Activities
{
    [Activity(Label = "BKStudent", Theme = "@style/SlashTheme", MainLauncher = true)]
    public class SlashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CheckSaveState();

            // Create your application here
        }
        async void CheckSaveState()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            bool isSaveInfo = prefs.GetBoolean("IsSaveInfo", false);
            Handler handler = new Handler();
            Action action;
            if (isSaveInfo)
            {
                LoginManager.IsLoadData = true;
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
                LoginManager.IsLoadData = false;
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