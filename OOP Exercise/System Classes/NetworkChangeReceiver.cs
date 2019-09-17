using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OOP_Exercise
{
    class NetworkChangeReceiver : BroadcastReceiver
    {
        public bool IsActive;
        public bool isFinished;
        public NetworkChangeReceiver()
        {
            this.IsActive = true;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            NetworkInfo netInfo = connectivityManager.ActiveNetworkInfo;
            if (netInfo == null || netInfo.IsConnected == false)
            {
                Toast.MakeText(context, "Không thể kết nối với Wifi/3G/4G", ToastLength.Short).Show();
                IsActive = false;
            }
            else
                IsActive = true;
            isFinished = true;
        }
    }
}