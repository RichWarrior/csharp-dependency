using System;

namespace csharp.dependency.core.Interface
{
    public interface IMethod
    {
        /// <summary>
        /// İlgili Enumun Açıklamasını Verir.
        /// </summary>
        /// <param name="_enum">Açıklaması Alınmak İstenilen Enum</param>
        /// <returns></returns>
        string Get_Enum_Description(Enum _enum);
    }
}
