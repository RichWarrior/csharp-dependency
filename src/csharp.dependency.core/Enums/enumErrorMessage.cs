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
        invalidModel = 2,
        /// <summary>
        /// Hata Durumlarında Dönecek Cevap
        /// </summary>
        [Description("error.unSuccessful")]
        unSuccessful = 3,

        #region User Region
        /// <summary>
        /// Github Kullanıcısı Bulunamadıysa Dönecek Cevap.
        /// </summary>
        [Description("error.githubUserNotFound")]
        githubUserNotFound = 100,
        /// <summary>
        /// Sistemde Kayıtlı Kullanıcı Varsa Dönecek Cevap
        /// </summary>
        [Description("error.duplicatedUser")]
        duplicatedUser = 101,
        [Description("error.loginFailed")]
        loginFailed = 102
        #endregion
    }
}
