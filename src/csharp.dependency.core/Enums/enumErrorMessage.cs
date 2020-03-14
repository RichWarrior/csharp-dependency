using System.ComponentModel;

namespace csharp.dependency.core.Enums
{
    public enum enumErrorMessage
    {
        /// <summary>
        /// Herhangi bir işlemin başarılı olma durumudur.
        /// </summary>
        [Description("error.successful")]
        successful = 1,
        /// <summary>
        /// Validate Olmayan Entitylerde Dönecek Cevap
        /// </summary>
        [Description("error.invalidModel")]
        invalidModel = 2
    }
}
