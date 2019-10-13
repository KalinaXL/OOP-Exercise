using SQLite;

namespace DataOfUser
{
    public class Subject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Date { get; set; }
        public string Week { get; set; }
        public Subject(string name, string room, string timestart, string timeend, string date, string week)
        {
            this.Name = name;
            this.Room = room;
            this.TimeStart = timestart;
            this.TimeEnd = timeend;
            this.Date = date;
            this.Week = week;
        }
        public Subject() { }

    }
}