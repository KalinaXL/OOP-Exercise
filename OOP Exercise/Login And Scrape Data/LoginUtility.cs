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
        public static bool CrawlData(string username, string password)
        {

            #region Initialize HttpRequest

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
            catch
            {
                return false;
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
            catch
            {
                return false;
            }
            #endregion

            #region Get Request & Token
            // Get Request
            try
            {
                response = request.Get(LoginManager.stinfoUrl);
            }
            catch
            {
                return false;
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

                //Get token
                token = string.Format("_token={0}", token);
                if (token.Length != 47)
                    return false;
                //Get Information of Student
                response = request.Post(LoginManager.scheduleUrl, token, LoginManager.ContentTypeToGetData);
                var json_tkb = response.ToString();

                // Get Exam Schedule
                response = request.Post(LoginManager.examUrl, token, LoginManager.ContentTypeToGetData);
                var json_exam = response.ToString();

                // Data is stored to response with Json type
                // Handle Json File 

                try
                {
                   new Thread(async () =>
                    {
                        LoginManager.Scheduler = JsonConvert.DeserializeObject<List<ThongtinSV>>(json_tkb);

                    }).Start();
                    new Thread(async () =>
                    {
                        LoginManager.Exam = JsonConvert.DeserializeObject<ExamScheduler>(json_exam);
 
                    }).Start();
                }

                catch (Exception ex)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            #endregion
            return true;
        }
    }
}