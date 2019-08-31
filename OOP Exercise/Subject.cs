namespace DataOfUser
{
    public class Subject
    {
        public string Name { get; set; }
        public string Room { get; set; }
        public string Time { get; set; }
        public Subject(string name, string room, string time)
        {
            this.Name = name;
            this.Room = room;
            this.Time = time;
        }
    }
}