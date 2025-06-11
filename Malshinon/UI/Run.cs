using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Win32.SafeHandles;
using Org.BouncyCastle.Crypto;
using Malshinon.Services;

namespace Malshinon.UI
{
    public static class Run
    {
        public static void Start()
        {
            Console.WriteLine("-----reporting system------");
            do
            {
                Console.WriteLine("1: submiy report");
                Console.WriteLine("2: submit report by CSV");
                Console.WriteLine("3: get code by name");
                Console.WriteLine("4: get all candidates for recruitment");
                Console.WriteLine("5: get all the dangerous targets");
                Console.WriteLine("6: exit");
                Console.WriteLine("Choose from stitches 1 to 6");

                string chois = Console.ReadLine();
                switch(chois)
                {
                    case "1":
                        Console.WriteLine("enter first name of reporter:");
                        string firstnamereporter = Console.ReadLine();

                        Console.WriteLine("enter last name of reporter:");
                        string lastnamereporter = Console.ReadLine();

                        Console.WriteLine("enter first name of target");
                        string firstnametarget = Console.ReadLine();

                        Console.WriteLine("enter last name of target");
                        string lastnametarger = Console.ReadLine();

                        Console.WriteLine("enter your report");
                        string text = Console.ReadLine();

                        ServiceReports.SubmitReport(firstnametarget , lastnametarger,firstnamereporter, lastnamereporter, text);
                        break;

                    case "2":
                        break;
                    case "3":
                        Console.WriteLine("enter your first name:");
                        string firstname = Console.ReadLine();

                        Console.WriteLine("enter your last name:");
                        string lastname = Console.ReadLine();

                        Console.WriteLine(AnalysisService.GetSracrtCodeByName(firstname, lastname));
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        Console.WriteLine("goodby");
                        return;
                        
                }
            }while (true);

        }
    }
}
