using csharp.dependency.core.CustomEntity.Github;
using csharp.dependency.core.Entity;
using csharp.dependency.core.Enums;
using csharp.dependency.core.Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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

        public List<GithubFollowers> Get_Github_Followers(string username)
        {
            List<GithubFollowers> rtn = null;
            string path = $"users/{username}/followers";
            IRestResponse response = Execute(path);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<List<GithubFollowers>>(response.Content);
            return rtn;
        }

        public List<GithubFollowing> Get_Github_Following(string username)
        {
            List<GithubFollowing> rtn = null;
            string path = $"users/{username}/following";
            IRestResponse response = Execute(path);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<List<GithubFollowing>>(response.Content);
            return rtn;
        }

        public List<GithubStarredRepository> Get_Github_Starred_Repository(string username)
        {
            List<GithubStarredRepository> rtn = null;
            string path = $"users/{username}/starred";
            IRestResponse response = Execute(path);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<List<GithubStarredRepository>>(response.Content);            
            return rtn;
        }

        public List<GithubRepository> Get_Github_Repository(string username)
        {
            List<GithubRepository> rtn = null;
            string path = $"users/{username}/repos";
            IRestResponse response = Execute(path);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<List<GithubRepository>>(response.Content);
            return rtn;
        }

        public async Task<GithubUser> Get_Github_User(User user, IRedisService _SRedisService)
        {
            GithubUser rtn = null;
            string json =await _SRedisService.Get((int)enumRedis.users, user.id.ToString());
            if (json == "null")
            {
                rtn = Get_Github_User(user.github_username);
               bool isOk =  await _SRedisService.Set((int)enumRedis.users, user.id.ToString(), JsonConvert.SerializeObject(rtn), DateTime.Now.AddHours(1));
                if (!isOk)
                    throw new Exception("Redis'e İlgili Değer Yazılamadı");
            }
            else
            {
                rtn = JsonConvert.DeserializeObject<GithubUser>(json);
            }
            return rtn;
        }

        public async Task<List<GithubFollowers>> Get_Github_Followers(User user, IRedisService _SRedisService)
        {
            List<GithubFollowers> rtn = null;
            string json = await _SRedisService.Get((int)enumRedis.followers, user.id.ToString());
            if (json == "null")
            {
                rtn = Get_Github_Followers(user.github_username);
                bool isOk = await _SRedisService.Set((int)enumRedis.followers, user.id.ToString(), JsonConvert.SerializeObject(rtn), DateTime.Now.AddHours(1));
                if (!isOk)
                    throw new Exception("Redis'e İlgili Değer Yazılamadı");
            }
            else
            {
                rtn = JsonConvert.DeserializeObject<List<GithubFollowers>>(json);
            }
            return rtn;
        }

        public async Task<List<GithubFollowing>> Get_Github_Following(User user, IRedisService _SRedisService)
        {
            List<GithubFollowing> rtn = null;
            string json = await _SRedisService.Get((int)enumRedis.following, user.id.ToString());
            if (json == "null")
            {
                rtn = Get_Github_Following(user.github_username);
                bool isOk = await _SRedisService.Set((int)enumRedis.following, user.id.ToString(), JsonConvert.SerializeObject(rtn), DateTime.Now.AddHours(1));
                if (!isOk)
                    throw new Exception("Redis'e İlgili Değer Yazılamadı");
            }
            else
            {
                rtn = JsonConvert.DeserializeObject<List<GithubFollowing>>(json);
            }
            return rtn;
        }

        public async Task<List<GithubStarredRepository>> Get_Github_Starred_Repository(User user, IRedisService _SRedisService)
        {
            List<GithubStarredRepository> rtn = null;
            string json = await _SRedisService.Get((int)enumRedis.starred_repository, user.id.ToString());
            if (json == "null")
            {
                rtn = Get_Github_Starred_Repository(user.github_username);
                bool isOk = await _SRedisService.Set((int)enumRedis.starred_repository, user.id.ToString(), JsonConvert.SerializeObject(rtn), DateTime.Now.AddHours(1));
                if (!isOk)
                    throw new Exception("Redis'e İlgili Değer Yazılamadı");
            }
            else
            {
                rtn = JsonConvert.DeserializeObject<List<GithubStarredRepository>>(json);
            }
            return rtn;
        }

        public async Task<List<GithubRepository>> Get_Github_Repository(User user, IRedisService _SRedisService)
        {
            List<GithubRepository> rtn = null;
            string json = await _SRedisService.Get((int)enumRedis.repository, user.id.ToString());
            if (json == "null")
            {
                rtn = Get_Github_Repository(user.github_username);
                bool isOk = await _SRedisService.Set((int)enumRedis.repository, user.id.ToString(), JsonConvert.SerializeObject(rtn), DateTime.Now.AddHours(1));
                if (!isOk)
                    throw new Exception("Redis'e İlgili Değer Yazılamadı");
            }
            else
            {
                rtn = JsonConvert.DeserializeObject<List<GithubRepository>>(json);
            }
            return rtn;
        }
    }
}
