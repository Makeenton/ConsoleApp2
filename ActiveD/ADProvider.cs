using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ActiveD
{
    public class ADProvider
    {
        /// <summary>
        /// Поиск всех пользователей в АД
        /// </summary>
        /// <returns>Список пользователей</returns>

        private PrincipalContext ctx;
        private UserPrincipal up;

        public ADProvider()
        {
            ctx = new PrincipalContext(ContextType.Domain);
            up = new UserPrincipal(ctx);
        }


        public List<ADUserProperties> GetAllUsers()
        {

            // find a user
            PrincipalSearcher srch = new PrincipalSearcher(up);
            List<ADUserProperties> users = new List<ADUserProperties>();
            

            using (PrincipalSearchResult<Principal> results = srch.FindAll())
            {
                    foreach (Principal found in results)
                    {
                        ADUserProperties user = new ADUserProperties();
                        user.Login = found.SamAccountName;
                        user.UserName = found.Name;
                        users.Add(user);
                    }
                    return users;
             }
        }

        public string GetUser(string login)
        {
            string result = null;

            if (login != null)
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain))
                {
                    result = UserPrincipal.FindByIdentity(ctx, login)?.ToString();
                }
            }

            return result;
        }
    }

    

}
