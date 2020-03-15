using csharp.dependency.core.CustomEntity.Github;
using csharp.dependency.core.Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
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

        public Tuple<bool,GithubUser> Check_Github_User(string username)
        {
            Tuple<bool, GithubUser> result = null;
            string path = $"users/{username}";
            IRestResponse response = Execute(path);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                GithubUser githubUser = JsonConvert.DeserializeObject<GithubUser>(response.Content);
                result = new Tuple<bool, GithubUser>(true, githubUser);
            }
            else
                result = new Tuple<bool, GithubUser>(false,null);
            return result;
        }

        public GithubUser Get_Github_User(string username)
        {
            string path = $"users/{username}";
            IRestResponse response = Execute(path);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<GithubUser>(response.Content);
            return null;
        }
    }
}
