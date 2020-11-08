using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Blazing_Shop
{
    public class GlobalVar
    {
        public static HttpClient webApiClient = new HttpClient();

        static GlobalVar()
        {
            webApiClient.BaseAddress = new Uri("https://localhost:44342/api/");
            webApiClient.DefaultRequestHeaders.Accept.Clear();
            webApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}