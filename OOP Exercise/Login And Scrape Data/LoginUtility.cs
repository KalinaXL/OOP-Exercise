using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
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
            List<string> profile_Student = new List<string>();
            try
            {
                #region Initialize HttpRequest
                await Task.Run(new Action(() =>
                {
                    HttpRequest request = new HttpRequest();
                    request.Cookies = new CookieDictionary();
                    request.UserAgent = LoginConfig.userAgent;

                    #endregion

                    #region Get LT, execution and cookies
                    HttpResponse response;
                    try
                    {
                        response = request.Get(LoginConfig.loginUrl);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    string sourceLoginPage = response.ToString();

             
                    string LT = RegularEx(sourceLoginPage, @"<input type=""hidden"" name=""lt"" value=""(.*?) /");
                    string execution = RegularEx(sourceLoginPage, @"<input type=""hidden"" name=""execution"" value=""(.*?) ");

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
                    string dataPost = "username=" + username + "&password=" + password + "&lt=" + LT + "&execution=" + execution + "&_eventId=submit&submit=%C4%90%C4%83ng+nh%E1%BA%ADp";
                    try
                    {
                        response = request.Post(LoginConfig.loginUrl, dataPost, LoginConfig.ContentTypeToLogin);
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
                        response = request.Get(LoginConfig.stinfoUrl);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    string page = response.ToString();

                  
                    #endregion

                    #region Get Data

                    try // check token
                    {
                      
                        // if login fail, there is no token is returned --> crash
                        string token = RegularEx(page, @"<meta name=""_token"" content=""(.*?) ");
                        if (token.Length != 40)
                            throw new Exception();
                        //Get token
                        token = string.Format("_token={0}", token);

                        //Get Information of Student
                        response = request.Post(LoginConfig.scheduleUrl, token, LoginConfig.ContentTypeToGetData);
                        json_tkb = response.ToString();

                        // Get Exam Schedule
                        response = request.Post(LoginConfig.examUrl, token, LoginConfig.ContentTypeToGetData);


                       


                        // Data is stored to response with Json type
                       


                        json_exam = response.ToString();

                        // Data is stored to response with Json type
                        // Handle Json File 

                        

                        //Parse htmlfile
                        request.UserAgent = LoginConfig.userAgent;
                        // request.CharacterSet = Encoding.UTF8;
                        request.AddHeader("Accept", @"*/*");
                        request.AddHeader("Accept-Language", "vi,en;q=0.9");
                        request.AddHeader("X-Requested-With", "XMLHttpRequest");
                        request.Referer = LoginConfig.stinfoUrl;

                        // Get information of Student
                        response = request.Get(LoginConfig.profileUrl);
                        string htmlFile = response.ToString();


                        HtmlDocument document = new HtmlDocument();
                        document.LoadHtml(htmlFile);

                        // get data of student
                        
                        byte counter = 0;
                        foreach (HtmlNode node in document.DocumentNode.SelectNodes("//div[@class='info-acc-table']//td"))
                        {
                            if (++counter <= 5)
                            {
                                profile_Student.Add(WebUtility.HtmlDecode(node.InnerText.ToString()));

                            }
                            else
                                break;

                        }


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }));
                
                #endregion
            }
            catch (Exception e)
            {
                return false;
            }
            try
            {
               
                Task getScheduler = new Task(new Action(() => { LoginDataManager.Scheduler = JsonConvert.DeserializeObject<List<ThongtinSV>>(json_tkb); }));
                Task getExam = new Task(new Action(() => { LoginDataManager.Exam = JsonConvert.DeserializeObject<ExamSchedule>(json_exam); }));
                Task getProfile = new Task(new Action(() => { LoginDataManager.Profile = profile_Student; }));
                
                getScheduler.Start();
                getExam.Start();
                getProfile.Start();
                await getScheduler;
                await getExam;
                await getProfile;
            }

            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        static string RegularEx(string source, string rex)
        {
            var list = Regex.Match(source, @rex, RegexOptions.IgnoreCase).Value.Split('"');
            return list[list.Length - 2];
        }
    }
}