using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using MySql.Data.MySqlClient;

namespace Malshinon.Dal
{
    internal static class DBConnection1
    {
        private static string DefaultConnectionString =>
                 "server=127.0.0.1;uid=root;password=;database=nalshinon";

        public static List<Dictionary<string, object>> ExecuteQuery(string sql, Dictionary<string, object> parameters = null, string connectionString = null)
        {
            var results = new List<Dictionary<string, object>>();
            try
            {
                using (var conn = new MySqlConnection(
                   string.IsNullOrWhiteSpace(connectionString)
                   ? DefaultConnectionString
                   : connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand (sql,conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>(reader.FieldCount);
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var val = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                    row[reader.GetName(i)] = val;
                                }
                                results.Add(row);
                            }
                        }

                    }
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }

            return results;
        }

        public static int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null, string connectionString = null)
        {
            try
            {
                using (var conn = new MySqlConnection(
                    string.IsNullOrWhiteSpace(connectionString)
                        ? DefaultConnectionString
                        : connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }

                        return cmd.ExecuteNonQuery(); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
                return -1; 
            }
        }
        public static object ExecuteScalar(string sql, Dictionary<string, object> parameters = null, string connectionString = null)
        {
            try
            {
                using (var conn = new MySqlConnection(
                    string.IsNullOrWhiteSpace(connectionString)
                        ? DefaultConnectionString
                        : connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }

                        return cmd.ExecuteScalar(); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
                return null;
            }
        }
        public static void PrintResult(List<Dictionary<string, object>> results)
        {
            if (results == null || results.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }

            foreach (var row in results)
            {
                foreach (var kv in row)
                    Console.WriteLine($"{kv.Key}: {kv.Value}");
                Console.WriteLine("---");
            }
        }


    }

}
