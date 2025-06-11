using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Malshinon.Models;
using Google.Protobuf.Compiler;

namespace Malshinon.Dal
{
    public static class PeopleDal
    {

        public static string AddPerso(string firstname, string lastname)
        {
            string secret_code = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            string sql = @"INSERT INTO people (first_name, last_name, secret_code)
                           VALUES (@firstname, @lastname, @secretcode)";

            var parameters = new Dictionary<string, object>
            {
                {"@firstname" ,firstname},
                {"@lastname", lastname },
                {"@secretcode",secret_code }
            };
            DBConnection1.ExecuteNonQuery(sql, parameters);
            return secret_code;
        }
        public static bool IsExists(string firstname, string lastname)
        {
            string sql = @"SELECT COUNT(*) FROM people 
                   WHERE first_name = @first AND last_name = @last";
            var parameters = new Dictionary<string, object>
            {
                {"@first",firstname },
                {"@last",lastname}
            };
            object result = DBConnection1.ExecuteScalar(sql, parameters);
            if (result != null && int.TryParse(result.ToString(), out int count))
            {
                return count > 0;
            }
            return false;
        }
        public static string GetSeacretCodOrCreateAndGet(string firstname, string lastname)
        {
            string sqlcheck = @"SELECT secret_code FROM people 
                        WHERE first_name = @first AND last_name = @last";
            var parameters = new Dictionary<string, object>
            {
                {"@first",firstname },
                {"@last",lastname }
            };
            object result = DBConnection1.ExecuteScalar(sqlcheck, parameters);
            if (result!= null ) { return result.ToString(); }
            
            string seacretcod = PeopleDal.AddPerso(firstname, lastname);
            return seacretcod;
        }
        public static People GetPersonByName(string firstName, string lastName)
        {
            string sql = @"SELECT id, first_name, last_name, secret_code, num_reports, num_mentions, is_dangerous, is_recruit_candidate
                   FROM people
                   WHERE first_name = @first AND last_name = @last
                   LIMIT 1";

            var parameters = new Dictionary<string, object>
            {
                { "@first", firstName },
                { "@last", lastName }
            };
               
                  
              

            var results = DBConnection1.ExecuteQuery(sql, parameters);
            if (results.Count == 0) return null;

            var row = results[0];
            return new People
            {
                Id = Convert.ToInt32(row["id"]),
                FirstName = row["first_name"].ToString(),
                LastName = row["last_name"].ToString(),
                SecretCode = row["secret_code"].ToString(),
                NumReports = Convert.ToInt32(row["num_reports"]),
                NumMentions = Convert.ToInt32(row["num_mentions"]),
                IsDangerous = Convert.ToBoolean(row["is_dangerous"]),
                IsRecruitCandidate = Convert.ToBoolean(row["is_recruit_candidate"])

            };
        }

        public static void UpDateRecruitStatus(int reporterId, bool IsCandidate)
        {
            string sql = @"UPDATE people SET is_recruit_candidate = @status WHERE id = @id";
            var parameters = new Dictionary<string, object>
            {
                {"@status", IsCandidate},
                {"@id",reporterId }
            };
            DBConnection1.ExecuteNonQuery(sql, parameters);
        }

        public static void UpDateDangerousStatus(int targetId, bool IsDangerous)
        {
            string sql = @"UPDATE people SET is_dangerous = @status WHERE id = @id";
            var parameters = new Dictionary<string, object>
            {
                {"@status", IsDangerous},
                {"@id",targetId }
            };
            DBConnection1.ExecuteNonQuery(sql, parameters);
        }


    }


}
//public static void AddPerson(string first_name, string last_name, string secretCode)
//{
//    var sql = $"INSERT INTO people(first_name, last_name, secret_code )" +
//        $" VALUES ('{first_name}','{last_name}','{secretCode}')";
//    DBConnection.Execute(sql);
//}
