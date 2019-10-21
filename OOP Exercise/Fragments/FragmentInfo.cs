using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Login_And_Scrape_Data;
using OOP_Exercise.Utility_Classes;
using Xamarin.Essentials;

namespace OOP_Exercise.Fragments
{
    public class FragmentInfo : Fragment
    {
        Button btnLogout;
        Button btnUpdateInfo;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_info, container, false);
            btnLogout = view.FindViewById<Button>(Resource.Id.btnLogout);
            btnUpdateInfo = view.FindViewById<Button>(Resource.Id.btnUpdateInfo);
            
            btnLogout.Click += BtnLogout_Click;
            btnUpdateInfo.Click += BtnUpdateInfo_ClickAsync;

            return view;
        }

        

        private async void BtnUpdateInfo_ClickAsync(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            DialogInfo dialog = new DialogInfo();
            dialog.Show(transaction: transaction, "dialog info");
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this.Activity);
            string username = prefs.GetString("username", "");
            string password = prefs.GetString("password", "");
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                Toast.MakeText(this.Activity, "Hãy thử đăng nhập lại !!!", ToastLength.Short).Show();
                //Show message failture
                return;
            }
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                dialog.Dismiss();
                Toast.MakeText(this.Activity, "Không thể kết nối với Wifi/3G/4G", ToastLength.Short).Show();
                return;
            }
            bool isLoginSuccess = await LoginUtility.CrawlData(username, password).ConfigureAwait(true);
            if (isLoginSuccess)
            {
                DatabaseUtility.GetDataScheduler();
                DatabaseUtility.GetDataExam();
                DatabaseUtility.SaveInfoDatabase();
                dialog.Dismiss();
                Toast.MakeText(this.Activity, "Cập nhật thành công !", ToastLength.Short).Show();
            }
            else
            {
                dialog.Dismiss();
                Toast.MakeText(this.Activity, "Cập nhật thất bại !", ToastLength.Short).Show();
            }

        }

        async Task ChangeState()
        {
            await Task.Run(new Action(() =>
            {
                ISharedPreferences sharePrefs = PreferenceManager.GetDefaultSharedPreferences(this.Activity);
                ISharedPreferencesEditor editor = sharePrefs.Edit();
                editor.Remove("IsSaveInfo");
                editor.Apply();
                LoginManager.IsLoadData = false;
                if (File.Exists(DatabaseUtility.dbInfoPath))
                    File.Delete(DatabaseUtility.dbInfoPath);
            }));
        }
        private async void BtnLogout_Click(object sender, EventArgs e)
        {
            await ChangeState().ConfigureAwait(true);
            Handler handler = new Handler();
            ISharedPreferences sharePrefs = PreferenceManager.GetDefaultSharedPreferences(this.Activity);
            ISharedPreferencesEditor editor = sharePrefs.Edit();
            editor.Remove("IsSaveInfo");
            editor.Remove("username");
            editor.Remove("password");
            editor.Remove("IsSaveSignUp");
            editor.Apply();
            if (File.Exists(DatabaseUtility.dbInfoPath))
                File.Delete(DatabaseUtility.dbInfoPath);
            if (File.Exists(DatabaseUtility.jsonPath))
                File.Delete(DatabaseUtility.jsonPath);
            handler.Post(new Action(()=>
            {
                Intent intent = new Intent(this.Activity, typeof(LoginActivity));
                StartActivity(intent);
            }));
        }
    }
}