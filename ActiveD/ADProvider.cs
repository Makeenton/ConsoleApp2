using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace ActiveD
{
    public class ADProvider
    {
        /// <summary>
        /// Поиск всех пользователей в АД
        /// </summary>
        /// <returns>Список пользователей</returns>
        public List<ADUserProperties> GetAllUsers()
        {

            List<ADUserProperties> users = new List<ADUserProperties>();

            DirectoryEntry CurrentDomain = new DirectoryEntry();
            DirectorySearcher adSearcher = new DirectorySearcher(CurrentDomain);
            adSearcher.Filter = "(&(objectClass=user)(objectCategory=person))";
            SearchResultCollection resultCol = adSearcher.FindAll();
            
            foreach (SearchResult result in resultCol)
            {
                ADUserProperties user = new ADUserProperties();

                user.Login = (String)result.Properties["samaccountname"][0];
                user.UserName = (String)result.Properties["cn"][0];

                users.Add(user);

            }
            return users;

        }
    }
}
