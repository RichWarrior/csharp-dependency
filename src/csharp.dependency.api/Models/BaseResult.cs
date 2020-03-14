using csharp.dependency.core.Enums;
using csharp.dependency.core.Interface;
using csharp.dependency.service.GeneralService;
using System;
using System.Net;

namespace csharp.dependency.api.Models
{
    public class BaseResult<T>
    {
        public T data { get; set; }
        public HttpStatusCode statusCode{ get; set; }
        public string message { get; set; }


        public BaseResult()
        {
            this.data = (T)Activator.CreateInstance(typeof(T));
            this.statusCode = HttpStatusCode.OK;

            IMethod _SMethod = new SMethod();

            this.message = _SMethod.Get_Enum_Description(enumErrorMessage.successful);
        }
    }
}
