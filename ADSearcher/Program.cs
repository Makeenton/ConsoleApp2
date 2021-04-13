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

            ADSearch ctx = new ADSearch();
            string User = ctx.GetUser().ToString();
            Console.WriteLine($"{User}");
            Console.ReadKey();
     
        }
    }
}
