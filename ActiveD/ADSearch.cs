using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveD
{
    public class ADSearch
    {
        /// <summary>
        /// Поиск пользователя по логину
        /// </summary>
        /// <returns>имя пользователя</returns>
        public string GetUser()
        {
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, login);
            
            
            if (user != null)
            {
                return user.ToString();
            }
            else
                return "Пользователь не найден";
                        

        }
    }
}
