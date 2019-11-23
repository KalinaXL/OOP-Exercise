using DataOfUser;
using System.Collections.Generic;


namespace OOP_Exercise.Utility_Classes
{
    class SaveInfo
    {
        public static List<ExamScheduler> examMidList { get; set; }
        public static List<ExamScheduler> examFinalList { get; set; }
        public static Dictionary<string, List<Subject>> SchedulerOfDay { get; set; }
        public static List<Subject> subjectList { get; set; }
        public static Person PersonInfo { get; set; }

    }
}