using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {


            DirectoryEntry CurrentDomain = new DirectoryEntry();

            DirectorySearcher adSearcher = new DirectorySearcher(CurrentDomain);

            adSearcher.Filter = "(&(objectClass=user)(objectCategory=person))";
        
            Console.WriteLine("Listing of users in the Active Directory");
            Console.WriteLine("========================================");
            SearchResultCollection resultCol = adSearcher.FindAll();

            foreach (SearchResult result in resultCol)
            {
                string Login = (String)result.Properties["samaccountname"][0];
                string UserName = (String)result.Properties["cn"][0];

                Console.WriteLine($"{UserName}({Login})" );
                                
            }

            Console.WriteLine("========================================");
            Console.ReadKey();
        }
    }
}
