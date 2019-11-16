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
    class Profile
    {
        [PrimaryKey, AutoIncrement ]
        public string Name { get; set; }
        public string Id { get; set; }
        public string DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        
        public Profile(string ID, string Name, string DateOfBirth, string Gender, string Country)
        {
            this.Name = Name;
            this.Id = ID;
            this.DateOfBirth = DateOfBirth;
            this.Country = Country;
            this.Gender = Gender;
        }

        public Profile() { }
    }
}