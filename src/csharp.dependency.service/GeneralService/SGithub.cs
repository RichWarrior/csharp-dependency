using csharp.dependency.core.Interface;
using RestSharp;
using System.Net;

namespace csharp.dependency.service.GeneralService
{
    public class SGithub : IGithub
    {       

        /// <summary>
        /// API Isteklerinde Bulunmak İçin Kullanılır.
        /// </summary>
        /// <param name="path">İsteğin Yapılacağı Point</param>
        /// <returns></returns>
        private IRestResponse Execute(string path)
        {
            var client = new RestClient($"https://api.github.com/{path}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public bool Check_Github_User(string username)
        {
            string path = $"users/{username}";
            return Execute(path).StatusCode == HttpStatusCode.OK ? true : false;            
        }
    }
}
