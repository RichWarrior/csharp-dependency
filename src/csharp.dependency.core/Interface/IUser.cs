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
        /// <summary>
        /// Kullanıcı Girişi Yapmak İçin Kullanılır.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Check_User_With_Password(string email, string password);
        /// <summary>
        /// Id'ye Göre Kullanıcıyı Getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User Get_By_Id(long id);
        /// <summary>
        /// Kullanıcı Güncellemek İçin Kullanılır.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Update_User(User user);
    }
}
