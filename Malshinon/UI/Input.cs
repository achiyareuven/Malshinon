using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Services;
using Malshinon.Models;
using MySqlX.XDevAPI.Common;

namespace Malshinon.UI
{
    public static class Input
    {
        public static void PrintMostDangerous()
        {
            Console.WriteLine("enter your first name");
            string firstname = Console.ReadLine();
            Console.WriteLine("enter your last name");
            string lastname = Console.ReadLine();
            List<People> PeopleList = AnalysisService.GetMostDangerous(firstname, lastname);
            if (PeopleList == null || PeopleList.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }

            foreach (People people in PeopleList)
            {
                Console.WriteLine($"first name: {people.FirstName}");
                Console.WriteLine($"last name: {people.LastName}");
                Console.WriteLine($"amount of mentions:{people.NumMentions}");
            }

        }
        public static void PrintMostRecruitCandidate()
        {
            Console.WriteLine("enter your first name");
            string firstname = Console.ReadLine();
            Console.WriteLine("enter your last name");
            string lastname = Console.ReadLine();
            List<People> PeopleList = AnalysisService.RecruitCandidate(firstname, lastname);
            if (PeopleList == null || PeopleList.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }

            foreach (People people in PeopleList)
            {
                Console.WriteLine($"first name: {people.FirstName}");
                Console.WriteLine($"last name: {people.LastName}");
                Console.WriteLine($"amount of mentions:{people.NumReports}");
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("                                                 -----reporting system------\n");
            Console.WriteLine("1: submiy report");
            Console.WriteLine("2: submit report by CSV");
            Console.WriteLine("3: get code by name");
            Console.WriteLine("4: get best candidates for recruitment");
            Console.WriteLine("5: get most the dangerous targets");
            Console.WriteLine("6: exit");
            Console.WriteLine("Choose from stitches 1 to 6");
        }
        public static void SubmitReportInput()
        {
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

            ServiceReports.SubmitReport(firstnametarget, lastnametarger, firstnamereporter, lastnamereporter, text);
        }
        public static void GetSracrtCodeByNameInpur()
        {
            Console.WriteLine("enter your first name:");
            string firstname = Console.ReadLine();

            Console.WriteLine("enter your last name:");
            string lastname = Console.ReadLine();

            Console.WriteLine(AnalysisService.GetSracrtCodeByName(firstname, lastname));
        }
    }
}
