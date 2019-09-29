using System;
using System.Collections.Generic;
using System.Globalization;

namespace OOP_Exercise.Login_And_Scrape_Data
{
    class LoginManager
    {
        public static string loginUrl = "https://sso.hcmut.edu.vn/cas/login?service=https://mybk.hcmut.edu.vn/my/homeSSO.action";
        public static string scheduleUrl = "https://mybk.hcmut.edu.vn/stinfo/lichthi/ajax_lichhoc";
        public static string examUrl = "https://mybk.hcmut.edu.vn/stinfo/lichthi/ajax_lichthi";
        public static string stinfoUrl = "https://mybk.hcmut.edu.vn/stinfo/";


        public static string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36";
        public static string ContentTypeToLogin = "application/x-www-form-urlencoded";
        public static string ContentTypeToGetData = "application/x-www-form-urlencoded; charset=UTF-8";

        public static bool IsLoadData { get; set; }
        public static List<ThongtinSV> Scheduler { get; set; }
        public static ExamSchedule Exam { get; set; }
        public static int CurrentWeekOfYear { get; set; }
        public static int Year { get; set; }
       
        public static void GetWeekAndYear()
        {
            CultureInfo myCI = new CultureInfo("vi-VN");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            CurrentWeekOfYear = myCal.GetWeekOfYear(DateTime.Now, myCWR, DayOfWeek.Monday);
            Year = DateTime.Now.Year;
        }

    }
}