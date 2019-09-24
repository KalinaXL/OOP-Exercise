using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Android.App;
using Android.Content;


using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using DataOfUser;

using OOP_Exercise.Activities;
using OOP_Exercise.Adapters;
using OOP_Exercise.Fragments;
using OOP_Exercise.Utility_Classes;
using SQLite;
using Android.Net;
using Android.OS;
namespace OOP_Exercise.Utility_Classes
{
    public class DatabaseUtility
    {
        public static string ExistingFile = "database.db";
        public static string NewFile = "data.db";
        public static string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), NewFile);
        public static void CloneExistingDatabase()
        {
            if (!File.Exists(dbPath))
                File.Delete(dbPath);
           
                using (BinaryReader br = new BinaryReader(Android.App.Application.Context.Assets.Open(ExistingFile)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        var buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }

                }

            
        }

    }

}