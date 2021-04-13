using System.DirectoryServices.AccountManagement;

namespace ActiveD
{
    public class ADSearch
    {
        /// <summary>
        /// Поиск пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>Имя пользователя, если не найдено - <see langword="null"/></returns>
        public string GetUser(string login)
        {
            string result = null;

            if (login != null)
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain))
                {
                    result = UserPrincipal.FindByIdentity(ctx, login).ToString();
                }
            }
            
            return result;
        }
    }
}
