using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.Dal
{
    public static class ReprtsDal
    {
        public static void AddReport(int reporterId, int targetId, string text)
        {
            try
            {
                string sql = @"INSERT INTO intelreports (reporter_id , target_id , text)
                           VALUES (@reporter_id, @target_id, @text)";

                var parameters = new Dictionary<string, object>
                {
                    {"@reporter_id",reporterId },
                    {"@target_id", targetId},
                    {"@text", text}
                };
                DBConnection1.ExecuteNonQuery(sql, parameters);

            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                string sqlUpdateReporter = @"UPDATE people SET num_reports = num_reports + 1  WHERE id = @id";
                var parametersReporter = new Dictionary<string, object>
                {
                     {"@id" ,reporterId }
                };
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                string updateTarget = @"UPDATE people SET num_mentions = num_mentions + 1  WHERE id = @id";
                var targetParameters = new Dictionary<string, object>
                {
                    {"@id",targetId }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
                
           
            


        }
    }
}

