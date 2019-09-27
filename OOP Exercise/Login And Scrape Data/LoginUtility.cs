using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using xNet;

namespace OOP_Exercise.Login_And_Scrape_Data
{
    public static class LoginUtility
    {
        public async static Task<bool> CrawlData(string username, string password)
        {
            string json_tkb = "";
            string json_exam = "";
            try
            {
                #region Initialize HttpRequest
                Task task = new Task(new Action(() =>
                {
                    HttpRequest request = new HttpRequest();
                    request.Cookies = new CookieDictionary();
                    request.UserAgent = LoginManager.userAgent;

                #endregion

                #region Get LT, execution and cookies
                HttpResponse response;
                    try
                    {
                        response = request.Get(LoginManager.loginUrl);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    string sourceLoginPage = response.ToString();

                    string[] listLT = Regex.Match(sourceLoginPage, @"<input type=""hidden"" name=""lt"" value=""(.*?) /", RegexOptions.IgnoreCase).Value.Split('"');
                    string LT = listLT[listLT.Length - 2];
                    string[] listExe = Regex.Match(sourceLoginPage, @"<input type=""hidden"" name=""execution"" value=""(.*?) ", RegexOptions.IgnoreCase).Value.Split('"');
                    string execution = listExe[listExe.Length - 2];

                    var listCookies = response.Cookies.ToString().Split(';');
                    foreach (var item in listCookies)
                    {
                        var item2 = item.Split('=');
                        if (item2.Length == 2)
                            request.Cookies.TryAdd(item2[0], item2[1]);
                    }

                #endregion

                #region Login
                //Login 
                string dataPost = "username=" + username + "&password=" + password + "&lt=" + LT + "&execution=" + execution + "&_eventId=submit&submit=Login";
                    try
                    {
                        response = request.Post(LoginManager.loginUrl, dataPost, LoginManager.ContentTypeToLogin);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                #endregion

                #region Get Request & Token
                // Get Request
                try
                    {
                        response = request.Get(LoginManager.stinfoUrl);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    string page = response.ToString();

                //Get Token
                page = Regex.Match(page, @"<meta name=""_token"" content=""(.*?) ", RegexOptions.IgnoreCase).Value;
                    string[] arr = page.Split('"');
                #endregion

                #region Get Data

                try // check token
                {
                        string token = arr[arr.Length - 2]; // if login fail, there is no token is returned --> crash
                    if (token.Length != 40)
                            throw new Exception();
                    //Get token
                    token = string.Format("_token={0}", token);

                    //Get Information of Student
                    response = request.Post(LoginManager.scheduleUrl, token, LoginManager.ContentTypeToGetData);
                    json_tkb = response.ToString();

                    // Get Exam Schedule
                    response = request.Post(LoginManager.examUrl, token, LoginManager.ContentTypeToGetData);
                    json_exam = response.ToString();

                    // Data is stored to response with Json type
                    // Handle Json File 

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }));
                task.Start();
                await task;
                #endregion
            }
            catch (Exception e)
            {
                return false;
            }
            try
            {
                //new Thread(() =>
                //{
                //    LoginManager.Scheduler = JsonConvert.DeserializeObject<List<ThongtinSV>>(json_tkb);

                //}).Start();
                Task getScheduler = new Task(new Action(() => { LoginManager.Scheduler = JsonConvert.DeserializeObject<List<ThongtinSV>>(json_tkb); }));
                Task getExam = new Task(new Action(() => { LoginManager.Exam = JsonConvert.DeserializeObject<ExamSchedule>(json_exam); }));

                //new Thread(() =>
                //{
                //    LoginManager.Exam = JsonConvert.DeserializeObject<ExamScheduler>(json_exam);

                //}).Start();
                getScheduler.Start();
                getExam.Start();
                await getScheduler;
                await getExam;
            }

            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}