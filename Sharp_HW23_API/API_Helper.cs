using System.Collections.Generic;
using RestSharp;
using OpenQA.Selenium;

namespace Sharp_HW23_API
{
    public static class API_Helper
    {
        public static IRestResponse SendApiRequest(object body, Dictionary<string, string> headers, string link, Method type)
        {
            RestClient client = new RestClient(link)
            {
                Timeout = 300000
            };

            RestRequest request = new RestRequest(type);
            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            bool isBodyJson = false;
            foreach (var header in headers)
            {
                if (header.Value.Contains("application/json"))
                {
                    isBodyJson = true;
                    break;
                }
            }
            if (!isBodyJson)
            {
                foreach (var data in (Dictionary<string, string>)body)
                {
                    request.AddParameter(data.Key, data.Value);
                }
            }
            else
            {
                request.AddJsonBody(body);
                request.RequestFormat = DataFormat.Json;
            }

            IRestResponse response = client.Execute(request);

            return response;
        }

        public static Cookie ExtractCookie(IRestResponse response, string cookieName)
        {
            Cookie result = null;
            foreach (var cookie in response.Cookies)
                if (cookie.Name.Equals(cookieName))
                    result = new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null);

            return result;
        }

        public static List<Cookie> ExtractAllCookies(IRestResponse response)
        {
            List<Cookie> result = new List<Cookie>();
            foreach (var cookie in response.Cookies)
                result.Add(new Cookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path, null));

            return result;
        }

    }
}