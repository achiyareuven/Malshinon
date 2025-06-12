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
            try
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
                Console.WriteLine("person Created successfully");
                return secret_code;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error in creating a person");
                return null;
            }

        }
        public static bool IsExists(string firstname, string lastname)
        {
            try
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
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error checking IsExists");
                return false;
            }

        }

        public static People GetPersonByName(string firstName, string lastName)
        {
            try
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
            }catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error accepting person to class");
                return null;
            }

        }

        public static void UpDateRecruitStatus(int reporterId, bool IsCandidate)
        {
            try
            {
                string sql = @"UPDATE people SET is_recruit_candidate = @status WHERE id = @id";
                var parameters = new Dictionary<string, object>
                {
                    {"@status", IsCandidate},
                    {"@id",reporterId }
                };
                DBConnection1.ExecuteNonQuery(sql, parameters);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error updating is recruit candidate");
            }
  
        }

        public static void UpDateDangerousStatus(int targetId, bool IsDangerous)
        {
            try
            {
                string sql = @"UPDATE people SET is_dangerous = @status WHERE id = @id";
                var parameters = new Dictionary<string, object>
                {
                    {"@status", IsDangerous},
                    {"@id",targetId }
                };
                DBConnection1.ExecuteNonQuery(sql, parameters);
            }catch(System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error updating is Dangerous ");
            }

        }

        public static List<People> GetAllDangerous()
        {
            try
            {
                string sql = @"SELECT * FROM people WHERE is_dangerous = TRUE ORDER BY num_mentions DESC LIMIT 10";
                var result = DBConnection1.ExecuteQuery(sql);
                List<People> listPeople = new List<People>();
                foreach (var item in result)
                {
                    listPeople.Add(new People
                    {
                        Id = Convert.ToInt32(item["id"]),
                        FirstName = item["first_name"].ToString(),
                        LastName = item["last_name"].ToString(),
                        SecretCode = item["secret_code"].ToString(),
                        NumReports = Convert.ToInt32(item["num_reports"]),
                        NumMentions = Convert.ToInt32(item["num_mentions"]),
                        IsDangerous = Convert.ToBoolean(item["is_dangerous"]),
                        IsRecruitCandidate = Convert.ToBoolean(item["is_recruit_candidate"])
                    });
                }
                return listPeople;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error accepting peoples to class");
                return null;
            }

        }
        public static List<People> GetAllRecruitCandidate()
        {
            try
            {
                string sql = @"SELECT * FROM people WHERE is_recruit_candidate = TRUE ORDER BY num_reports DESC LIMIT 10";
                var result = DBConnection1.ExecuteQuery(sql);
                List<People> listPeople = new List<People>();
                foreach (var item in result)
                {
                    listPeople.Add(new People
                    {
                        Id = Convert.ToInt32(item["id"]),
                        FirstName = item["first_name"].ToString(),
                        LastName = item["last_name"].ToString(),
                        SecretCode = item["secret_code"].ToString(),
                        NumReports = Convert.ToInt32(item["num_reports"]),
                        NumMentions = Convert.ToInt32(item["num_mentions"]),
                        IsDangerous = Convert.ToBoolean(item["is_dangerous"]),
                        IsRecruitCandidate = Convert.ToBoolean(item["is_recruit_candidate"])
                    });
                }
                return listPeople;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error accepting peoples to class");
                return null;
            }
        }

    }
        //    public static string GetSeacretCodOrCreateAndGet(string firstname, string lastname)
        //{
        //    string sqlcheck = @"SELECT secret_code FROM people 
        //                WHERE first_name = @first AND last_name = @last";
        //    var parameters = new Dictionary<string, object>
        //    {
        //        {"@first",firstname },
        //        {"@last",lastname }
        //    };
        //    object result = DBConnection1.ExecuteScalar(sqlcheck, parameters);
        //    if (result != null) { return result.ToString(); }

        //    string seacretcod = PeopleDal.AddPerso(firstname, lastname);
        //    return seacretcod;
        //}


    }

