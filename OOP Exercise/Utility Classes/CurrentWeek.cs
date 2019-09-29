using SQLite;

namespace OOP_Exercise.Utility_Classes
{
    class CurrentWeek
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Week { get; set; }
    }
}