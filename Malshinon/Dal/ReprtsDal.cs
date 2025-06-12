using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Models;

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
                DBConnection1.ExecuteNonQuery(sqlUpdateReporter, parametersReporter);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                string sqlupdateTarget = @"UPDATE people SET num_mentions = num_mentions + 1  WHERE id = @id";
                var targetParameters = new Dictionary<string, object>
                {
                    {"@id",targetId }
                };
                DBConnection1.ExecuteNonQuery(sqlupdateTarget, targetParameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
                
           


        }
        public static double GetAvgReportsText(int reporterId)
        {
            try
            {
                string sql = @"SELECT AVG(CHAR_LENGTH(text)) FROM intelreports WHERE reporter_id = @id";
                var parameters = new Dictionary<string, object>
                {
                    {"@id",reporterId }

                };
                object result = DBConnection1.ExecuteScalar(sql, parameters);
                if (result == null || result == DBNull.Value)
                { return 0; }
                return Convert.ToDouble(result);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
            
        }

        public static Alert IsAlertBurstReport(int targetId)
        {
            string sql = @"SELECT timestamp FROM intelreports WHERE target_id  = @id AND timestamp >= NOW() - INTERVAL 15 MINUTE ORDER BY timestamp  ASC";
            var parameters = new Dictionary<string, object>
            {
                {"@id",targetId}
            };
            var rows = DBConnection1.ExecuteQuery(sql, parameters);
            if (rows.Count > 2)
            {
                DateTime windowstart = Convert.ToDateTime(rows[0]["timestamp"]);
                DateTime windowsend = Convert.ToDateTime(rows[rows.Count -1]["timestamp"]);
                return new Alert
                {
                    TargetId = targetId,
                    Reason = "Three or more reports in less than 15 minutes",
                    WindowStart = windowstart,
                    WindowEnd = windowsend,
                    AlertTime = DateTime.Now
                };
            }
            return null;
        }
        
    }

}

