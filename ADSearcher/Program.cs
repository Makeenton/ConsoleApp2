using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveD;


namespace ADSearcher
{
    class Program
    {
        static void Main()
        {

            ADProvider ctx = new ADProvider();
            Console.WriteLine("Введите логин пользователя:");
            string inputlogin = Console.ReadLine();
            string User = ctx.GetUser(inputlogin);
            Console.WriteLine($"{User}");
            Console.ReadKey();
     
        }
    }
}
