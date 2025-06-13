using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Models;
using Mysqlx;

namespace Malshinon.Dal
{
    public static class ReprtsDal
    {
        public static void AddReport(int reporterId, int targetId, string text,DateTime time)
        {
            try
            {
                string sql = @"INSERT INTO intelreports (reporter_id , target_id , text, timestamp)
                           VALUES (@reporter_id, @target_id, @text,@timestamp)";

                var parameters = new Dictionary<string, object>
                {
                    {"@reporter_id",reporterId },
                    {"@target_id", targetId},
                    {"@text", text},
                    {"@timestamp", time}
                };
                DBConnection1.ExecuteNonQuery(sql, parameters);
                Console.WriteLine("A report was created successfully");

            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error creating report");
            }
            try
            {
                string sqlUpdateReporter = @"UPDATE people SET num_reports = num_reports + 1  WHERE id = @id";
                var parametersReporter = new Dictionary<string, object>
                {
                     {"@id" ,reporterId }
                };
                DBConnection1.ExecuteNonQuery(sqlUpdateReporter, parametersReporter);
                Console.WriteLine("reporter update successfully ");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error update report");
            }
            try
            {
                string sqlupdateTarget = @"UPDATE people SET num_mentions = num_mentions + 1  WHERE id = @id";
                var targetParameters = new Dictionary<string, object>
                {
                    {"@id",targetId }
                };
                DBConnection1.ExecuteNonQuery(sqlupdateTarget, targetParameters);
                Console.WriteLine("target update successfully ");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error update target");

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
                Console.WriteLine("Average obtained successfully");
                if (result == null || result == DBNull.Value)
                { return 0; }
                Console.WriteLine(Convert.ToDouble(result));
                return Convert.ToDouble(result);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error in getting average");
                return 0;
            }
            
        }

        public static Alert IsAlertBurstReport(int targetId)
        {
            try
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
                    DateTime windowsend = Convert.ToDateTime(rows[rows.Count - 1]["timestamp"]);
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Error checking BURS reports");
                return null;
            }


        }
        
    }

}

