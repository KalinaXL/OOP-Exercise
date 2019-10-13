using DataOfUser;
using OOP_Exercise.Login_And_Scrape_Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Exercise.Utility_Classes
{
    public static class DatabaseUtility
    {
        public static string ExistingFile = "database.db";
        public static string NewFile = "data.db";
        public static string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), NewFile);
        public static string dbInfoPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "dbInfo.db");
        public static void CloneExistingDatabase()
        {
            if (File.Exists(dbPath))
                File.Delete(dbPath);

            using (BinaryReader br = new BinaryReader(Android.App.Application.Context.Assets.Open(ExistingFile)))
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                {
                    var buffer = new byte[2048];
                    int len = 0;
                    while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, len);
                    }
                }

            }
        }
        public static async void SaveInfoDatabase()
        {
            if (File.Exists(dbInfoPath))
                File.Delete(dbInfoPath);
            await Task.Run(new Action(() =>
            {
                SQLiteConnection sql = new SQLiteConnection(dbInfoPath);
                sql.CreateTable<ExamScheduler>();
                sql.InsertAll(SaveInfo.examMidList);
                sql.InsertAll(SaveInfo.examFinalList);
                sql.CreateTable<Subject>();
                List<Subject> listSub = LoginManager.Scheduler[0].tkb
                                        .Select(item => new Subject(item.ten_mh, item.phong1, item.giobd, item.giokt, item.thu1, item.tuan_hoc) )
                                        .ToList();
                sql.InsertAll(listSub);
                sql.CreateTable<CurrentWeek>();
                sql.Insert(new CurrentWeek() { Week = LoginManager.CurrentWeekOfYear });
                sql.Close();

            }
            ));

        }
        public static async Task<bool> GetInfoDatabase()
        {
            await Task.Run(new Action(() =>
            {
                SQLiteConnection sql = new SQLiteConnection(dbInfoPath);
                LoginManager.CurrentWeekOfYear = sql.Table<CurrentWeek>().Select(item => item).First().Week;
                SaveInfo.examMidList = sql.Table<ExamScheduler>().Where(item => item.IsMidTerm).ToList();
                SaveInfo.examFinalList = sql.Table<ExamScheduler>().Where(item => !item.IsMidTerm).ToList();
                SaveInfo.subjectList = sql.Table<Subject>().Select(item => item).ToList();
                string week = LoginManager.CurrentWeekOfYear.ToString();
                SaveInfo.SchedulerOfDay = SaveInfo.subjectList.Where(item => item.Week.Contains(week)).Select(item => new { item.Date, Subject = new Subject(item.Name, item.Room, item.TimeStart, item.TimeEnd, item.Date, item.Week) })
                    .GroupBy(item => item.Date).Distinct().ToDictionary(item => item.Key, item => item.Select(iter => iter.Subject).ToList());
                sql.Close();
            }));
            return true;
        }
        public static void GetDataScheduler()
        {
            string week = LoginManager.CurrentWeekOfYear.ToString();
            if (LoginManager.IsLoadData)
            {
                SaveInfo.SchedulerOfDay = SaveInfo.subjectList.Where(item => item.Week.Contains(week)).Select(item => new { item.Date, Subject = new Subject(item.Name, item.Room, item.TimeStart, item.TimeEnd, item.Date, item.Week) })
                    .GroupBy(item => item.Date).Distinct().ToDictionary(item => item.Key, item => item.Select(iter => iter.Subject).ToList());
                return;
            }

            SaveInfo.SchedulerOfDay = LoginManager.Scheduler[0].tkb
                                     .Where(item => item.tuan_hoc.Contains(week))
                                     .Select(item => new { K = item.thu1, Subject = new Subject(item.ten_mh, item.phong1, item.giobd, item.giokt, item.thu1, item.tuan_hoc) })
                                     .Distinct()
                                     .GroupBy(item => item.K).ToDictionary(item => item.Key, item => item.Select(item2 => item2.Subject).ToList());
        }
        public static void GetDataExam()
        {
            SaveInfo.examMidList = (from item in LoginManager.Exam._20191.lichthi select new ExamScheduler() { Hour = item.gio_kt, Date = item.ngaykt, SubjectName = item.ten_mh, Room = item.phong_ktra,IsMidTerm = true })
                                   .ToList();
            SaveInfo.examMidList.Sort(new SortItemExam());

            SaveInfo.examFinalList = (from item in LoginManager.Exam._20191.lichthi select new ExamScheduler() { Hour = item.gio_thi, Date = item.ngaythi, SubjectName = item.ten_mh, Room = item.phong_thi })
                                     .ToList();
            SaveInfo.examFinalList.Sort(new SortItemExam());

        }


    }

}