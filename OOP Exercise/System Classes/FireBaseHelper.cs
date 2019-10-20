using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DataOfUser;
using Firebase.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OOP_Exercise.Utility_Classes;
using xNet;

namespace OOP_Exercise.System_Classes
{
    static class FireBaseHelper
    {
        static FirebaseClient firebase = new FirebaseClient("https://oop-exercise.firebaseio.com/");
        public static List<Question> Ques { get; private set; }
        public static byte Time { get; private set; }
        public static string YearOfExam { get; set; }
        public async static Task<bool> GetSubExam(string typeOfExam, string subjectName, string year = "2015-2016")
        {
            try
            {
                SubExam sub = (await firebase.Child(typeOfExam + "/" + subjectName).OnceAsync<SubExam>()).Where(item => item.Key == year).Select(item => new SubExam()
                {
                    Time = item.Object.Time,
                    Year = item.Key,
                    Quiz = item.Object.Quiz
                }).First();
                Ques = sub.Quiz;
                Time = sub.Time;
                YearOfExam = sub.Year;
                await SaveDataInfoFile();
                
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        async static Task SaveDataInfoFile()
        {
              await Task.Run(new Action(()=>{
                HttpRequest request = new HttpRequest();
                HttpResponse response = request.Get(@"https://oop-exercise.firebaseio.com/.json");
                  if (File.Exists(DatabaseUtility.jsonPath))
                      File.Delete(DatabaseUtility.jsonPath);
                File.WriteAllText(DatabaseUtility.jsonPath, response.ToString());
            }));   
        }
        public static async Task LoadDataFromFile(string typeOfExam, string subjectName, string year = "2015-2016")
        {  
            try
            {
                await Task.Run(new Action(() =>
                {
                    using (FileStream fs = File.Open(@String.Format("{0}", DatabaseUtility.jsonPath), FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            using (JsonReader reader = new JsonTextReader(sr))
                            {
                                while (reader.Read())
                                {
                                    if (reader.TokenType == JsonToken.StartObject)
                                    {
                                        var obj = JObject.Load(reader)[typeOfExam][subjectName][year];
                                        var sub = JsonConvert.DeserializeObject<SubExam>(obj.ToString());
                                        Ques = sub.Quiz;
                                        Time = sub.Time;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                })).ConfigureAwait(true);
            }
            catch { }
            
        }


    }
    class SubExam
    {
        public byte Time { get; set; }
        public string Year { get; set; }
        public List<Question> Quiz { get; set; }
    }
}