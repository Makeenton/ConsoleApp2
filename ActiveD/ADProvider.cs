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
            List<ADUserProperties> users = new List<ADUserProperties>();


            string domainPath = "LDAP://OU=Альвента,DC=alventa,DC=ru";
            DirectoryEntry searchRoot = new DirectoryEntry(domainPath);

            DirectorySearcher search = new DirectorySearcher(searchRoot)
            {
                Filter = "(&(objectClass=user)(objectCategory=person))"
            };

            search.PropertiesToLoad.Add("sAMAccountName");  // Логин
            search.PropertiesToLoad.Add("sn");          // Фамилия
            search.PropertiesToLoad.Add("givenName");   // Имя
            search.PropertiesToLoad.Add("middleName");  // Отчество
            search.PropertiesToLoad.Add("telephoneNumber"); // Внутренний номер
            search.PropertiesToLoad.Add("mobile");      // Мобильный телефон
            search.PropertiesToLoad.Add("mail");        // Email
            search.PropertiesToLoad.Add("company");     // Отделение
            search.PropertiesToLoad.Add("department");  // Подразделение
            search.PropertiesToLoad.Add("title");       // Должность
            search.PropertiesToLoad.Add("info");        // ДР
            search.PropertiesToLoad.Add("userAccountControl"); // Состояние учетной записи в домене
            search.PropertiesToLoad.Add("lastLogon");     // Дата и время последнего входа в домен
            search.PropertiesToLoad.Add("lastLogonTimestamp");     // Дата и время последнего входа в домен 2

            SearchResultCollection resultCol = search.FindAll();

            if (resultCol != null)
            {
                foreach (SearchResult adUser in resultCol)
                {
                    //ADUser user = GetAdUser(foundAdUser);
                    ADUserProperties user = new ADUserProperties();

                    bool accountDisable = ((int)adUser.Properties["userAccountControl"][0] & 0x0002) == 2;

                    user.Login = adUser.Properties.Contains("sAMAccountName")
                                                        ? adUser.Properties["sAMAccountName"][0].ToString()
                                                         : string.Empty;

                    user.UserName = adUser.Properties.Contains("sn")
                                                         ? adUser.Properties["sn"][0].ToString()
                                                         : string.Empty;

                    string name = adUser.Properties.Contains("givenName")
                                                         ? adUser.Properties["givenName"][0].ToString()
                                                         : string.Empty;

                    string middleName = adUser.Properties.Contains("middleName")
                                                         ? adUser.Properties["middleName"][0].ToString()
                                                         : string.Empty;

                    // Здесь можно добавить еще поля...

                    DateTime? lastLogon = adUser.Properties.Contains("lastLogon")
                        ? (DateTime?)DateTime.FromFileTime((long)adUser.Properties["lastLogon"][0])
                        : null;

                    DateTime? lastLogonTimestamp = adUser.Properties.Contains("lastLogonTimestamp")
                        ? (DateTime?)DateTime.FromFileTime((long)adUser.Properties["lastLogonTimestamp"][0])
                        : null;

                    //DateTime? lastLogonReal;
                    //if (lastLogon != null && lastLogonTimestamp != null)
                    //{
                    //    lastLogonReal = lastLogonTimestamp > lastLogon ? lastLogonTimestamp : lastLogon;
                    //}
                    //else if (lastLogon != null && lastLogonTimestamp == null)
                    //{
                    //    lastLogonReal = lastLogon;
                    //}
                    //else if (lastLogon == null && lastLogonTimestamp != null)
                    //{
                    //    lastLogonReal = lastLogonTimestamp;
                    //}
                    //else
                    //{
                    //    lastLogonReal = null;
                    //}

                    // Вместо закомментированного кода выше:
                    user.LastLogon = (new List<DateTime?>() { lastLogon, lastLogonTimestamp }).OrderByDescending(t => t).FirstOrDefault();

                    users.Add(user);
                }
            }

            return users;
        }



        public List<ADUserProperties> GetAllUsersMYMYMYMYMY()
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
                    

                    DirectoryEntry de = found.GetUnderlyingObject() as DirectoryEntry;
                    DirectorySearcher desearcher = new DirectorySearcher(de);
                    
                    
                        desearcher.PropertiesToLoad.Add("LastLogon");
                        desearcher.PropertiesToLoad.Add("lastLogonTimestamp");
                        foreach (SearchResult searchrecord in desearcher.FindAll())
                        {
                            DateTime? lastLogon = searchrecord.Properties.Contains("lastLogon")
                            ? (DateTime?)DateTime.FromFileTime((long)searchrecord.Properties["lastLogon"][0])
                            : null;

                            DateTime? lastLogonTimestamp = searchrecord.Properties.Contains("lastLogonTimestamp")
                            ? (DateTime?)DateTime.FromFileTime((long)searchrecord.Properties["lastLogonTimestamp"][0])
                            : null;

                            user.LastLogon = (new List<DateTime?>() { lastLogon, lastLogonTimestamp }).OrderByDescending(t => t).FirstOrDefault();
                            

                        }
                     
                                           
                    users.Add(user);
                }
                return users;
            }

        }

        public List<ADUserProperties> GettimefilterUsers(double number)
        {

            List<ADUserProperties> oldusers = new List<ADUserProperties>();
            // find a user
            //PrincipalSearcher srch = new PrincipalSearcher(up);
            List<ADUserProperties> users = GetAllUsers();
            foreach (var user in users)
            {
                if (user.LastLogon != null)
                {
                    if (user.LastLogon<=DateTime.Now.AddDays(-number))
                    {
                        oldusers.Add(user);
                    }
                }
            }
            return oldusers;
            

           

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