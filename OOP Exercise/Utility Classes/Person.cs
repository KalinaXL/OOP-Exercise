using System.Collections.Generic;


namespace OOP_Exercise.Utility_Classes
{
    class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string DayOfBirth { get; set; }
        public Person() { }
        public Person(List<string> profile)
        {
            ID = profile[0];
            Name = profile[1];
            Address = profile[4];
            Gender = profile[3];
            DayOfBirth = profile[2];
        }
    }
}