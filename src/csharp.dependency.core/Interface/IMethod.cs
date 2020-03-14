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
        /// <summary>
        /// Verilen Parametreyi MD5'e Dönüştürür.
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        string Generate_MD5(string _value);
    }
}
