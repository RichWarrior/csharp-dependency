using csharp.dependency.core.Interface;
using System;
using System.ComponentModel;
using System.Reflection;

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
    }
}
