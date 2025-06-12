using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Win32.SafeHandles;
using Org.BouncyCastle.Crypto;
using Malshinon.Services;
using Malshinon.CSV;

namespace Malshinon.UI
{
    public static class Run
    {
        public static void Start()
        {

            while (true)
            {
                Input.ShowMenu();
                string chois = Console.ReadLine();

                switch (chois)
                {
                    case "1":
                        Input.SubmitReportInput();
                        break;

                    case "2":
                        ImportCSV.ReportByCsv();
                        break;
                    case "3":
                        Input.GetSracrtCodeByNameInpur();
                        break;
                    case "4":
                        Input.PrintMostRecruitCandidate();
                        break;
                    case "5":
                        Input.PrintMostDangerous();
                        break;
                    case "6":
                        Console.WriteLine("goodby");
                        return;
                    default:
                        Console.WriteLine("invalid input please Choose from stitches 1 to 6 ");
                        break;

                }
            }
            

        }
    }
}
