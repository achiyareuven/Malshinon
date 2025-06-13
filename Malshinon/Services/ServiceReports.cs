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
        public static void SubmitReport(string firstNameTarget, string lastNameTarget, string firstNameReporter, string lastNameReporter, string text,DateTime? time = null)
        {
            if (string.IsNullOrEmpty(firstNameTarget)||string.IsNullOrEmpty(lastNameTarget) ||string.IsNullOrEmpty(firstNameReporter) || string.IsNullOrEmpty(lastNameReporter) || string.IsNullOrEmpty(text))
            {
                Console.WriteLine("invalid input please try again");
                return;
            }
            DateTime finalTime = time ?? DateTime.Now;
            People target = AnalysisService.GetOrCreatePerson(firstNameTarget, lastNameTarget);
            People reporter = AnalysisService.GetOrCreatePerson(firstNameReporter, lastNameReporter);

            ReprtsDal.AddReport(reporter.Id, target.Id, text, finalTime);

            reporter = PeopleDal.GetPersonByName(reporter.FirstName, reporter.LastName);
            target= PeopleDal.GetPersonByName(target.FirstName, target.LastName);

            IsHeCanBeAgant(reporter);
            CheckAlertAndStatuaTarget(target);




        }
        public static void IsHeCanBeAgant(People reporter)
        {
            if (reporter != null&&
                !reporter.IsRecruitCandidate&&
                reporter.NumReports>=10&&
                reporter.NumMentions==0 &&
                ReprtsDal.GetAvgReportsText(reporter.Id)>= 100)
            {
                PeopleDal.UpDateRecruitStatus(reporter.Id, true);
                Console.WriteLine($"{reporter.FirstName} {reporter.LastName} updated Recruit status ");

            }
        }
        private static void CheckAlertAndStatuaTarget(People target)
        {
            if (target != null && 
                !target.IsDangerous&&
                target.NumMentions >= 20)
            {
                PeopleDal.UpDateDangerousStatus(target.Id, true);
                Alert alert = new Alert
                {
                    TargetId = target.Id,
                    Reason = "Mentioned in more than 20 reports",
                    AlertTime = DateTime.Now
                };
                AlertDal.AddAlert(alert);
                Console.WriteLine($"ALERT: {target.FirstName} {target.LastName} became dangerous");
            }
            Alert burstAlert = ReprtsDal.IsAlertBurstReport(target.Id);
            if (burstAlert != null)
            {
                AlertDal.AddAlert(burstAlert);
                Console.WriteLine($"ALERT: burst alert created for {target.FirstName} {target.LastName}");
            }
        }

    }
}
