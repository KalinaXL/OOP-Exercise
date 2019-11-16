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
        TextView txt_mssv;
        TextView txt_hovaten;
        TextView txt_quequan;
        TextView txt_gioitinh;
        TextView txt_ngaysinh;
        Button btnLogout;
        Button btnUpdate;

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
            Profile profile = new Profile(); 
            profile = SaveInfo.profileList[0];

            txt_hovaten = view.FindViewById<TextView>(Resource.Id.txt_hovaten);
            
            txt_hovaten.Text = "Họ và tên: " + profile.Name;

            txt_mssv = view.FindViewById<TextView>(Resource.Id.txt_mssv); 
            txt_mssv.Text =  "Mã số sinh viên: " + profile.Id;

            txt_gioitinh = view.FindViewById<TextView>(Resource.Id.txt_gioitinh);
            txt_gioitinh.Text = "Giới Tính: " + profile.Gender;

            txt_quequan = view.FindViewById<TextView>(Resource.Id.txt_quequan);
            txt_quequan.Text = "Quê Quán: " + profile.Country;

            txt_ngaysinh = view.FindViewById<TextView>(Resource.Id.txt_ngaysinh);
            txt_ngaysinh.Text = "Ngày Sinh: " + profile.DateOfBirth;

            btnLogout = view.FindViewById<Button>(Resource.Id.btnLogout);
            btnLogout.Click += BtnLogout_Click1;
            btnUpdate = view.FindViewById<Button>(Resource.Id.btnUpdateInfo);
            btnUpdate.Click += BtnUpdate_Click;


            return view;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            BtnUpdateInfo_ClickAsync(sender, e);
        }

        private void BtnLogout_Click1(object sender, EventArgs e)
        {
            BtnLogout_Click(sender, e);
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
            handler.Post(new Action(() =>
            {
                Intent intent = new Intent(this.Activity, typeof(LoginActivity));
                StartActivity(intent);
            }));
        }
    }
}