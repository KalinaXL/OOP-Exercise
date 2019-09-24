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
using SQLite;

namespace OOP_Exercise.Utility_Classes
{
    class Category
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int IsMidTerm { get; set; }
        public string Name { get; set; }
        public byte TotalTime { get; set; }
    }
}