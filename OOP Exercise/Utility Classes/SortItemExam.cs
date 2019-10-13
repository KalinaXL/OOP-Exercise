using System.Collections.Generic;

namespace OOP_Exercise.Utility_Classes
{
    class SortItemExam : IComparer<ExamScheduler>
    {
        public int Compare(ExamScheduler x, ExamScheduler y)
        {
            if (y == null)
                return -1;
            if (x == null)
                return 1;
            try
            {
                var timex = x.Date.Split("/");
                var timey = y.Date.Split("/");
                int monthx = int.Parse(timex[1]);
                int monthy = int.Parse(timey[1]);
                if (monthx != monthy)
                {
                    if (monthx >= 8 && monthy >= 8)
                    {
                        if (monthx > monthy)
                            return 1;
                        return -1;
                    }
                    if (monthx < 8)
                        return 1;
                    return -1;
                }
                else 
                {
                    int datex = int.Parse(timex[0]);
                    int datey = int.Parse(timey[0]);
                    if (datex > datey)
                        return 1;
                    else if (datex == datey)
                    {
                        int hourx = int.Parse(x.Hour.Replace("g", ""));
                        int houry = int.Parse(y.Hour.Replace("g", ""));
                        if (hourx > houry)
                            return 1;
                        else if (hourx < houry)
                            return -1;
                    }
                    else
                        return -1;

                }
         
            }
            catch { }
            return 0;
        }
    }
}