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