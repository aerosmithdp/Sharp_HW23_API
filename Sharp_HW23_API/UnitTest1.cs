using System.Collections.Generic;
using RestSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using System.IO;

namespace Sharp_HW23_API
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var body = new Dictionary<string, string>
            {
                { "ulogin", "art1613122"},
                { "upassword", "505558545"}
            };

            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/x-www-form-urlencoded"}
            };

            IRestResponse response = API_Helper.SendApiRequest(body, headers, "https://my.soyuz.in.ua", Method.POST);

            var cookie = API_Helper.ExtractCookie(response, "zbs_lang");
            var cookie2 = API_Helper.ExtractCookie(response, "ulogin");
            var cookie3 = API_Helper.ExtractCookie(response, "upassword");

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://my.soyuz.in.ua");
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.AddCookie(cookie);
            driver.Manage().Cookies.AddCookie(cookie2);
            driver.Manage().Cookies.AddCookie(cookie3);
            driver.Navigate().GoToUrl("https://my.soyuz.in.ua/index.php");

            var headers2 = new Dictionary<string, string>
            {
                { "Cookie", cookie.Name},
                { "Content-Type", "application/x-www-form-urlencoded"}
            };

            IRestResponse response2 = API_Helper.SendApiRequest(body, headers2, "https://my.soyuz.in.ua", Method.GET);

            driver.Quit();
        }

        [Fact]
        public void Test2()
        {
            var client = new RestClient("https://clip2net.com");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "image/jpeg");
            request.AddFile("content", "/pic.jpg");
            IRestResponse response = client.Execute(request);
            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public void Test3()
        {
            var client = new RestClient("http://clip2net.com/clip/m566236/1643363250-62a5a-6kb.png");
            var request = new RestRequest(Method.GET);
            byte[] downloaded = client.DownloadData(request);
            File.WriteAllBytes(Path.Combine("/Users/User/Desktop", "nyancat.png"), downloaded);
        }


    }
}
