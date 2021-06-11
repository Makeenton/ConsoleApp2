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
            double number;
            ADProvider ctx = new ADProvider();
            Console.WriteLine("Срок в днях:");
            string inputdays = Console.ReadLine();
            Double.TryParse(inputdays, out number);
            List<ADUserProperties> oldusers = ctx.GettimefilterUsers(number);
            foreach (ADUserProperties user in oldusers)
            {
                Console.WriteLine($"{user.UserName}({user.Login})({user.LastLogon})");

            }

            Console.WriteLine("========================================");
            Console.ReadKey();
     
        }
    }
}
