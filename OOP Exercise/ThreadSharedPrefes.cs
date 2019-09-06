using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace OOP_Exercise
{
    class ThreadSharedPrefes:Thread
    {
        string username;
        string password;
        Context context;
        public ThreadSharedPrefes(Context context,string username, string password )
        {
            this.context = context;
            this.username = username;
            this.password = password;
        }
        void SaveInfo()
        {
            ISharedPreferences sharePrefs = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editor = sharePrefs.Edit();
            editor.PutString("username", username);
            editor.PutString("password", password);
            editor.Apply();
        }
        public override void Run()
        {
            SaveInfo();
        }
    }
}