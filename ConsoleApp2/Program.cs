using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveD;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            ADProvider ad = new ADProvider();
            List<ADUserProperties> users = ad.GetAllUsers();

            foreach (ADUserProperties user in users)
            {
                Console.WriteLine($"{user.UserName}({user.Login})" );
                                
            }

            Console.WriteLine("========================================");
            Console.ReadKey();
        }
    }
}
