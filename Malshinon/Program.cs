using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Dal;

namespace Malshinon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine(PeopleDal.IsExists("o", "s"));
            var person = PeopleDal.GetPersonByName("o", "s");
            Console.WriteLine(person.Id);
            Console.WriteLine(person.FirstName);
            Console.WriteLine(person.LastName);
            Console.WriteLine(person.SecretCode);
            Console.WriteLine(person.NumMentions);
            Console.WriteLine(person.NumReports);
            ReprtsDal.AddReport(1, 2, "jhjjkhjkhjk");
        }
    }
}
