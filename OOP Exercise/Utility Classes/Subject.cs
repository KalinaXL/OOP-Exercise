namespace DataOfUser
{
    public class Subject
    {
        public string Name { get; set; }
        public string Room { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public Subject(string name, string room, string timestart, string timeend)
        {
            this.Name = name;
            this.Room = room;
            this.TimeStart = timestart;
            this.TimeEnd = timeend;
        }
        
    }
}