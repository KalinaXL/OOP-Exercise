using SQLite;

namespace OOP_Exercise.Utility_Classes
{
    public class ExamScheduler
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public string Room { get; set; }
        public string Hour { get; set; }
        public string Date { get; set; }
        public string MonthYear { get; set; }
    }
}