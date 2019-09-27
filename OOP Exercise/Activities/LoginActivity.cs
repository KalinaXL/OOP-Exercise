using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Login_And_Scrape_Data;
using OOP_Exercise.Utility_Classes;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace OOP_Exercise
{
    [Activity(Label = "BKStudent", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        Button btnLogin;
        ProgressBar progressBar;
        EditText txtUsername;
        EditText txtPassword;
        CheckBox checkBoxRememberMe;

       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            FindWidgetFromId();
            btnLogin.Click += BtnLogin_Click;
            GetAccountPrefers();
           
            // Create your application here
        }

        void SaveAccountPrefers()
        {
            if (!checkBoxRememberMe.Checked)
                return;
            new ThreadSharedPrefes(this, txtUsername.Text, txtPassword.Text).Run();

        }
        void GetAccountPrefers()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            if (prefs.Contains("username") && prefs.Contains("password"))
            {
                string username = prefs.GetString("username", "Tên đăng nhập");
                string password = prefs.GetString("password", "Mật khẩu");
                RunOnUiThread(() => 
                {
                    txtUsername.Text = username;
                    txtPassword.Text = password;
                });
              
            }
        }

        void FindWidgetFromId()
        {
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            checkBoxRememberMe = FindViewById<CheckBox>(Resource.Id.checkBoxRememberMe);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
        }

        private async void  BtnLogin_Click(object sender, EventArgs e)
        {
                     
            
            if (txtUsername.Text.Length == 0)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Thông báo");
                alert.SetMessage("Tên đăng nhập không được để trống");
                alert.SetButton("OK", (c, events) => { });
                alert.Show();
                return;
            }
            else if (txtPassword.Text.Length == 0)
            {
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Thông báo");
                alert.SetMessage("Mật khẩu không được để trống");
                alert.SetButton("OK", (c, events) => { });
                alert.Show();
                return;
            }
           

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                Toast.MakeText(this, "Không thể kết nối với Wifi/3G/4G", ToastLength.Short).Show();
                return;
            }
            new Thread(() =>
            {
                LoginManager.GetWeekAndYear();
            }).Start();
            RunOnUiThread(new Action(()=>
             {
                progressBar.Visibility = ViewStates.Visible;
                btnLogin.Visibility = ViewStates.Gone;
                txtUsername.Enabled = txtPassword.Enabled = false;
             }));
            bool isLoginSuccess =  await LoginUtility.CrawlData(txtUsername.Text, txtPassword.Text);
            
            if (isLoginSuccess)
            {
                Handler handler = new Handler();
                SaveAccountPrefers();
                Action action = new Action(() =>
                {
                    Intent intent = new Intent(this, typeof(MainActivity));
                    this.StartActivity(intent);
                });
                handler.Post(action);
                new Thread(async () =>
                {
                    DatabaseUtility.CloneExistingDatabase();
                }).Start();
                //this.OverridePendingTransition();
                this.Finish();
            }
            else
            {
                progressBar.Visibility = ViewStates.Gone;
                btnLogin.Visibility = ViewStates.Visible;
                txtUsername.Enabled = txtPassword.Enabled = true;
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Thông báo");
                alert.SetMessage("Đăng nhập thất bại! Vui lòng thử lại sau!");
                alert.SetButton("OK", (c, events) => { });
                alert.Show();
            }
            
            
        }

      
    }
}