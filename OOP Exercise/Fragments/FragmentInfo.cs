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

namespace OOP_Exercise.Fragments
{
    public class FragmentInfo : Fragment
    {
        Button btnLogout;
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
            btnLogout.Click += BtnLogout_Click;
            return view;
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
           
            handler.Post(new Action(()=>
            {
                Intent intent = new Intent(this.Activity, typeof(LoginActivity));
                StartActivity(intent);
            }));
        }
    }
}