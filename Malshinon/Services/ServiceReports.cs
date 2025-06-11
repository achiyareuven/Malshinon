using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Dal;
using Malshinon.Models;
using MySql.Data.MySqlClient;

namespace Malshinon.Services
{
    public static class ServiceReports
    {
        public static void SubmitReport(string firstNameTarget, string lastNameTarget,string firstNameReporter,string lastNameReporter,string text)
        {
            var target = AnalysisService.GetOrCreatePerson(firstNameTarget, lastNameTarget);
            var reporter = AnalysisService.GetOrCreatePerson(firstNameReporter,lastNameReporter);
            ReprtsDal.AddReport(reporter.Id, target.Id, text);
            if (IsHeCanBeAgant(reporter.FirstName, reporter.LastName, reporter.Id))
            {
                PeopleDal.UpDateRecruitStatus(reporter.Id,true);
            }

        }
        public static bool IsHeCanBeAgant(string firstNameReporter, string lastNameReporter, int reporterid)
        {
            People reporter = (PeopleDal.GetPersonByName(firstNameReporter, lastNameReporter));
            if (reporter.NumMentions > 10 && reporter.NumReports < 1 &&
                ReprtsDal.GetAvgReportsText(reporter.Id) >= 100)
            {
                return true;
            }
            return false;
            
        }
    }
}
