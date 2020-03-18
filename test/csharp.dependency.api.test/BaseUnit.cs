using csharp.dependency.core.CustomEntity.Request.User;
using csharp.dependency.core.CustomEntity.Response.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Moq;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Xunit;

namespace csharp.dependency.api.test
{
    public class BaseUnit
    {
        private TestServer _server;
        private HttpClient _client;
        private string _token;

        public long customer_id = 1;

        public TestServer server
        {
            get
            {
                return _server;
            }
        }

        public HttpClient client
        {
            get
            {
                if (!String.IsNullOrEmpty(_token))
                {
                    _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {_token}");
                }
                return _client;
            }
        }

        public string token
        {
            get
            {
                if (string.IsNullOrEmpty(_token))
                {
                    Login();
                }
                return _token;
            }
        }

        public string email { get; }

        public BaseUnit()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            email = "faruk_thecno@hotmail.com";
            _client = _server.CreateClient();
        }

        public async void Login()
        {
            RequestLogin userLogin = new RequestLogin()
            {
                email = email,
                password = "03102593",               
            };
            var content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/api/user/login", content).Result;
            string message = await response.Content.ReadAsStringAsync();
            RootItem<ResponseLogin> responseItem = JsonConvert.DeserializeObject<RootItem<ResponseLogin>>(message);
            if (String.IsNullOrEmpty(responseItem.data.token))
                throw new Exception("Token Alınırken Bir Hataya Rastlandı!");
            _token = responseItem.data.token;
        }

        public void ThrowNewException(HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    Assert.True(true);
                    break;
                case HttpStatusCode.Unauthorized:
                    if (String.IsNullOrEmpty(_token))
                        throw new Exception("Oturum Açılamadığı İçin İlgili URL'e Erişelemedi!");
                    throw new Exception("Oturum Açılmış Olmasına Rağmen İlgili URL'e Erişelemedi!");
                    break;
                case HttpStatusCode.NotFound:
                    string message = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    RootItem rootItem = JsonConvert.DeserializeObject<RootItem>(message);
                    if (!String.IsNullOrEmpty(rootItem.message))
                        throw new Exception($"Bir Hataya Rastlandı!\nHata Kodu={rootItem.message}");
                    throw new Exception($"Bir Hataya Rastlandı!\nHata Kodu={httpResponseMessage.StatusCode}");
                    break;
                default:
                    throw new Exception(
                        String.Format($"Beklenmeyen Bir Hata Durumu İle Karılaşıldı.\nHata Kodu={httpResponseMessage.StatusCode}")
                        );
                    break;
            }
        }

        public IFormFile ConvertIFormFile(string path)
        {
            FileInfo physicalFile = new FileInfo(path);
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(physicalFile.OpenRead());
            writer.Flush();
            ms.Position = 0;
            var fileName = physicalFile.Name;
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(m => m.OpenReadStream()).Returns(ms);
            fileMock.Setup(m => m.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));
            return fileMock.Object;
        }

        public MultipartFormDataContent ConvertMultipartFormDataContent<T>(T obj)
        {
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(IFormFile))
                {
                    var value = property.GetValue(obj, null);
                    if (value != null)
                    {
                        IFormFile item = (IFormFile)property.GetValue(obj, null);
                        multipartFormDataContent.Add(new StreamContent(item.OpenReadStream()), property.Name, item.FileName);
                    }
                }
                else
                {
                    object value = property.GetValue(obj, null);
                    if (value != null)
                    {
                        HttpContent content = new StringContent(property.GetValue(obj).ToString());
                        multipartFormDataContent.Add(content, property.Name);
                    }
                }
            }
            return multipartFormDataContent;
        }

        public StringContent ConvertStringContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public class RootItem
        {
            public int statusCode { get; set; }
            public string message { get; set; }
        }

        public class RootItem<T>
        {
            public T data { get; set; }
            public int statusCode { get; set; }
            public string message { get; set; }
        }
    }
}
