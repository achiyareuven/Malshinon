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
        public static void SubmitReport(string firstNameTarget, string lastNameTarget, string firstNameReporter, string lastNameReporter, string text)
        {
            if (string.IsNullOrEmpty(firstNameTarget)||string.IsNullOrEmpty(lastNameTarget) ||string.IsNullOrEmpty(firstNameReporter) || string.IsNullOrEmpty(lastNameReporter) || string.IsNullOrEmpty(text))
            {
                Console.WriteLine("invalid input please try again");
                return;
            }
            var target = AnalysisService.GetOrCreatePerson(firstNameTarget, lastNameTarget);
            var reporter = AnalysisService.GetOrCreatePerson(firstNameReporter, lastNameReporter);
            ReprtsDal.AddReport(reporter.Id, target.Id, text);
            if (IsHeCanBeAgant(reporter.FirstName, reporter.LastName, reporter.Id))
            {
                PeopleDal.UpDateRecruitStatus(reporter.Id, true);
            }
            if (target.NumReports >= 20)
            {
                PeopleDal.UpDateDangerousStatus(target.Id, true);
                Alert alert = new Alert
                {
                    TargetId = target.Id,
                    Reason = "Mentioned in more than 20 reports",
                    AlertTime = DateTime.Now

                };
                AlertDal.AddAlert(alert);
            }
            Alert burstAlert = ReprtsDal.IsAlertBurstReport(target.Id);
            if (burstAlert != null)
            {
                AlertDal.AddAlert(burstAlert);
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
