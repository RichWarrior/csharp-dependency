using csharp.dependency.core.Entity;

namespace csharp.dependency.core.Interface
{
    public interface IUser
    {
        /// <summary>
        /// Sistemde Bu Github Adıyla Kayıtlı veya E-Postayla Kayıtlı Kullanıcı Var Mı?
        /// Kontrolünü Yapar.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="github_username"></param>
        /// <returns></returns>
        User Check_User(string email, string github_username);
        /// <summary>
        /// Kullanıcı Oluşturur.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        long Insert_User(User user);
    }
}
