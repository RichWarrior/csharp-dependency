using csharp.dependency.core.Interface;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace csharp.dependency.service.GeneralService
{
    public class SMethod : IMethod
    {
        public string Get_Enum_Description(Enum _enum)
        {
            FieldInfo fi = _enum.GetType().GetField(_enum.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return _enum.ToString();
        }
        public string Generate_MD5(string _value)
        {
            StringBuilder sb = new StringBuilder();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] _bytes = Encoding.UTF8.GetBytes(_value);
            _bytes = md5.ComputeHash(_bytes);
            foreach (byte _byte in _bytes)
            {
                sb.Append(_byte.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
}
