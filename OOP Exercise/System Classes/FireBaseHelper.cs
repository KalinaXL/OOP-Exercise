using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OOP_Exercise.Utility_Classes;
using xNet;

namespace OOP_Exercise.System_Classes
{
    /// <summary>
    /// Class for connecting to firebase to get data about questions, save into file as json, and read data again from saved file
    /// </summary>
    static partial class FireBaseHelper
    {
        static FirebaseClient firebase = new FirebaseClient("https://oop-exercise.firebaseio.com/");
        public static List<Question> Ques { get; private set; }
        public static List<string> Terms { get; private set; }
        public static byte Time { get; private set; }

        public async static Task<bool> GetSubExam(string typeOfExam, string subjectName, string year)
        {
            try
            {
                SubExam sub = (await firebase.Child(typeOfExam + "/" + subjectName).OnceAsync<SubExam>()).Where(item => item.Key == year).Select(item => new SubExam()
                {
                    Time = item.Object.Time,
                    Quiz = item.Object.Quiz,
                    HasKeyE = item.Object.HasKeyE
                }).First();
                Ques = sub.Quiz;
                Time = sub.Time;
                DataManager.HasKeyE = sub.HasKeyE;
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
            await Task.Run(new Action(() => {
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
                                        DataManager.HasKeyE = sub.HasKeyE;
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
        public static async Task<List<string>> LoadTermFromFile(string typeOfExam, string subjectName)
        {
            List<string> ls = null;
            Action action = (() =>
            {
                try
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
                                        var obj = JObject.Load(reader)[typeOfExam + "Term"][subjectName];
                                        ls = JsonConvert.DeserializeObject<TermTime>(obj.ToString()).Time.Select(item => item.Year).ToList();
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }
                catch
                {

                }

            });
            await Task.Run(action).ConfigureAwait(true);
            return ls;


        }

        public static async Task<bool> GetTermYear(string typeOfExam, string subjectName)
        {
            try
            {
                Terms = (await firebase.Child(typeOfExam + "Term").OnceAsync<TermTime>()).Where(item => item.Key == subjectName).Select(item => new TermTime() { Time = item.Object.Time }).First().Time.Select(item => item.Year).ToList();
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}