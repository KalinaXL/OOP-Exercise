using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OOP_Exercise
{
    [Activity(Label = "Login", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        Button btnLogin;
        ProgressBar progressBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar);
            btnLogin.Click += BtnLogin_Click;
            // Create your application here
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            btnLogin.Visibility = ViewStates.Invisible;
            
            Intent intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
            //this.OverridePendingTransition();
            this.Finish();
        }
    }
}