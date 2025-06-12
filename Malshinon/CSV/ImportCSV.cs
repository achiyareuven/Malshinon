using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Dal;
using Malshinon.Services;

namespace Malshinon.CSV
{
    public static class ImportCSV
    {
        public static void ReportByCsv()
        {
            int count = 0;

            Console.Write("CSV file path: ");
            var path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) { Console.WriteLine("File not found.\n"); return; }
            var reader = new StreamReader(path);
            string header = reader.ReadLine();
            if (header == null) { Console.WriteLine("CSV is empty.\n"); return; }
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                if (parts.Length < 6) continue;
                var reporterFirst = parts[0];
                var reporterLast = parts[1];
                var targetFirst = parts[2];
                var targetLast = parts[3];
                var text = parts[4];
                if (!DateTime.TryParse(parts[5], null, System.Globalization.DateTimeStyles.AssumeLocal, out var ts)) continue;
                if (string.IsNullOrWhiteSpace(reporterFirst) || 
                    string.IsNullOrWhiteSpace(reporterLast) ||
                    string.IsNullOrWhiteSpace(targetFirst) ||
                    string.IsNullOrWhiteSpace(targetLast) ||
                    string.IsNullOrWhiteSpace(text)) continue;

                ServiceReports.SubmitReport(targetFirst, targetLast, reporterFirst, reporterLast, text, ts);
                count++;
            }
            
        }
    }
}
