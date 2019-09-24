
using System;
using System.IO;
using System.Reflection;


using System.Threading;

using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using xNet;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


namespace Test
{
    public class Program
    {
        static void CrawlData()
        {
            #region Input from User

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            #endregion

            #region Initialize HttpRequest

            HttpRequest request = new HttpRequest();
            request.Cookies = new CookieDictionary();
            request.UserAgent = Config.userAgent;

            #endregion

            #region Get LT, execution and cookies

            HttpResponse response = request.Get(Config.loginUrl);
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
            response = request.Post(Config.loginUrl, dataPost, Config.ContentTypeToLogin);
            #endregion

            #region Get Request & Token
            // Get Request
            response = request.Get("https://mybk.hcmut.edu.vn/stinfo/");
            string page = response.ToString();

            //Get Token
            page = Regex.Match(page, @"<meta name=""_token"" content=""(.*?) ", RegexOptions.IgnoreCase).Value;
            string[] arr = page.Split('"');
            #endregion

            #region Get Data

            try // check token
            {
                string token = arr[arr.Length - 2]; // if login fail, there is no token is returned --> crash
                Console.WriteLine("Login success");

                //Get token
                token = String.Format("_token={0}", token);

                //Get Information of Student
                response = request.Post(Config.scheduleUrl, token, Config.ContentTypeToGetData);
                var json_tkb = response.ToString();

                // Get Exam Schedule
                response = request.Post(Config.examUrl, token, Config.ContentTypeToGetData);
                var json_exam = response.ToString();

                // Data is stored to response with Json type
                // Handle Json File

                try
                {
                    //Console.WriteLine(json_tkb.ToString());

                    var jss = new JavaScriptSerializer();
                    var subject_info = JsonConvert.DeserializeObject<List<ThongtinSV>>(json_tkb.ToString());
                    var exam = JsonConvert.DeserializeObject<ConsoleApp3.ExamSchedule>(json_exam.ToString());

                    Console.WriteLine("tên môn học: " + subject_info[0].tkb[0].ten_mh.ToString());
                    Console.WriteLine("Mon thi: " + exam._20191.lichthi[0].ten_mh.ToString() + "- phong thi: " + exam._20191.lichthi[0].phong_ktra.ToString());
                }

                catch (Exception ex)
                {
                    Console.WriteLine("We had a problem: " + ex.Message.ToString());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Fail");
                Console.WriteLine("We have a problem: " + ex.Message.ToString());
            }
        }
        #endregion

        static void Main(string[] args)
        {

            CrawlData();

        }

    }
}
