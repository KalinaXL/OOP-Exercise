using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public static class Config
    {
        public static string loginUrl = "https://sso.hcmut.edu.vn/cas/login?service=https://mybk.hcmut.edu.vn/my/homeSSO.action";
        public static string scheduleUrl = "https://mybk.hcmut.edu.vn/stinfo/lichthi/ajax_lichhoc";
        public static string examUrl = "https://mybk.hcmut.edu.vn/stinfo/lichthi/ajax_lichthi";
        public static string stinfoUrl = "https://mybk.hcmut.edu.vn/stinfo/";
        public static string outputPath = "data.html";

        public static string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36";
        public static string ContentTypeToLogin = "application/x-www-form-urlencoded";
        public static string ContentTypeToGetData = "application/x-www-form-urlencoded; charset=UTF-8";
    }
}
