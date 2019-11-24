using DataOfUser;
using OOP_Exercise.Login_And_Scrape_Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using xNet;

namespace OOP_Exercise.Utility_Classes
{
    public static class DatabaseUtility
    {
        public static string ExistingFile = "database.db";
        public static string jsonPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "data.json");
        public static string dbInfoPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "dbInfo.db");
        
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
                List<Subject> listSub = LoginDataManager.Scheduler[0].tkb
                                        .Select(item => new Subject(item.ten_mh, item.phong1, item.giobd, item.giokt, item.thu1, item.tuan_hoc) )
                                        .ToList();
                sql.InsertAll(listSub);

                sql.CreateTable<Person>();
                sql.Insert(SaveInfo.PersonInfo);

               

                sql.Close();

            }
            ));

        }
        public static async Task<bool> GetInfoDatabase()
        {

            await Task.Run(new Action(() =>
            {
                SQLiteConnection sql = new SQLiteConnection(dbInfoPath);
                LoginDataManager.GetWeekInYear();
                SaveInfo.examMidList = sql.Table<ExamScheduler>().Where(item => item.IsMidTerm).ToList();
                SaveInfo.examFinalList = sql.Table<ExamScheduler>().Where(item => !item.IsMidTerm).ToList();
                SaveInfo.subjectList = sql.Table<Subject>().Select(item => item).ToList();
                SaveInfo.PersonInfo = sql.Table<Person>().First();
            
                string week = LoginDataManager.CurrentWeekOfYear.ToString();
                SaveInfo.SchedulerOfDay = SaveInfo.subjectList.Where(item => item.Week.Contains(week)).Select(item => new { item.Date, Subject = new Subject(item.Name, item.Room, item.TimeStart, item.TimeEnd, item.Date, item.Week) })
                        .GroupBy(item => item.Date).Distinct().ToDictionary(item => item.Key, item => item.Select(iter => iter.Subject).ToList());
             
                    sql.Close();
              
            }));
            return true;
        }
        public static void GetDataScheduler()
        {
            string week = LoginDataManager.CurrentWeekOfYear.ToString();
            if (LoginConfig.IsLoadData)
            {
                SaveInfo.SchedulerOfDay = SaveInfo.subjectList.Where(item => item.Week.Contains(week)).Select(item => new { item.Date, Subject = new Subject(item.Name, item.Room, item.TimeStart, item.TimeEnd, item.Date, item.Week) })
                    .GroupBy(item => item.Date).Distinct().ToDictionary(item => item.Key, item => item.Select(iter => iter.Subject).ToList());
                return;
            }

            SaveInfo.SchedulerOfDay = LoginDataManager.Scheduler[0].tkb
                                     .Where(item => item.tuan_hoc.Contains(week))
                                     .Select(item => new { K = item.thu1, Subject = new Subject(item.ten_mh, item.phong1, item.giobd, item.giokt, item.thu1, item.tuan_hoc) })
                                     .Distinct()
                                     .GroupBy(item => item.K).ToDictionary(item => item.Key, item => item.Select(item2 => item2.Subject).ToList());
        }
        public static void GetDataExam()
        {
            SaveInfo.examMidList = (from item in LoginDataManager.Exam._20191.lichthi select new ExamScheduler() { Hour = item.gio_kt, Date = item.ngaykt, SubjectName = item.ten_mh, Room = item.phong_ktra,IsMidTerm = true })
                                   .ToList();
            SaveInfo.examMidList.Sort(new SortItemExam());

            SaveInfo.examFinalList = (from item in LoginDataManager.Exam._20191.lichthi select new ExamScheduler() { Hour = item.gio_thi, Date = item.ngaythi, SubjectName = item.ten_mh, Room = item.phong_thi })
                                     .ToList();
            SaveInfo.examFinalList.Sort(new SortItemExam());

        }
        public static void GetProfile()
        {
            if (!LoginConfig.IsLoadData)
                SaveInfo.PersonInfo = new Person(LoginDataManager.Profile);
        }
    }

}