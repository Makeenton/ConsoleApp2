using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
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

                    bool accountDisable = ((int)adUser.Properties["userAccountControl"][0] & 0x0002) == 2;

                    string accountName = adUser.Properties.Contains("sAMAccountName")
                                                        ? adUser.Properties["sAMAccountName"][0].ToString()
                                                         : string.Empty;

                    string surname = adUser.Properties.Contains("sn")
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
                    DateTime? lastLogonReal = (new List<DateTime?>() { lastLogon, lastLogonTimestamp }).OrderByDescending(t => t).FirstOrDefault();

                    Console.WriteLine($"{surname} {name} {middleName} ({accountName})");
                    Console.WriteLine($"lastLogon={lastLogon}, lastLogonTimestamp={lastLogonTimestamp}, lastLogonReal={lastLogonReal}");
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }
    }
}
