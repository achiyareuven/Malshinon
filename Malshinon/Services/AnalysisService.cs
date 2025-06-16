using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Compiler;
using Malshinon.Dal;
using Malshinon.Models;

namespace Malshinon.Services
{
    public static class AnalysisService
    {
 
        public static People GetOrCreatePerson(string first, string last)
        {
                if (!PeopleDal.IsExists(first, last))
                    PeopleDal.AddPerso(first, last);

                    return PeopleDal.GetPersonByName(first, last);
        }
        public static string GetSracrtCodeByName(string firstname ,string lastname)
        {
            if (!PeopleDal.IsExists(firstname, lastname))
            {
                Console.WriteLine("user not exsit");
                return null;
            }
            return $"your seacret code is: {PeopleDal.GetPersonByName(firstname, lastname).SecretCode}";
        }
        public static List<People> GetMostDangerous(string firstname, string lastname)
        {
            People people = GetOrCreatePerson(firstname, lastname);
            if (people.NumReports < 40 && people.NumMentions == 0)
            {
                Console.WriteLine("You do not have permission.");
                return null;
            }
            return PeopleDal.GetAllDangerous();
        }
        public static List<People> RecruitCandidate(string firstname, string lastname)
        {
            People people = GetOrCreatePerson(firstname, lastname);
            if (people.NumReports < 5)
            {
                Console.WriteLine("You do not have permission.");
                return null;
            }
            return PeopleDal.GetAllRecruitCandidate();
        }





    }
}
