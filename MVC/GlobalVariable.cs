using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;

namespace MVC
{
    public class GlobalVariable
    {
         
        public static HttpClient webapiClient = new HttpClient();

        static GlobalVariable()
        {
            webapiClient.BaseAddress = new Uri("https://localhost:44307/api/");
            webapiClient.DefaultRequestHeaders.Clear();
            webapiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
