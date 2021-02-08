using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SalaryApp
{
    public static class Utils
    {
        //public static DateTime ConvertToDateTime(string str)
        //{
        //    string pattern = @"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})\.(\d{3})";
        //    if (Regex.IsMatch(str, pattern))
        //    {
        //        Match match = Regex.Match(str, pattern);
        //        int year = Convert.ToInt32(match.Groups[1].Value);
        //        int month = Convert.ToInt32(match.Groups[2].Value);
        //        int day = Convert.ToInt32(match.Groups[3].Value);
        //        int hour = Convert.ToInt32(match.Groups[4].Value);
        //        int minute = Convert.ToInt32(match.Groups[5].Value);
        //        int second = Convert.ToInt32(match.Groups[6].Value);
        //        int millisecond = Convert.ToInt32(match.Groups[7].Value);
        //        return new DateTime(year, month, day, hour, minute, second, millisecond);
        //    }
        //    else
        //    {
        //        throw new Exception("Unable to parse.");
        //    }
        //}

        public static void InactiveElem( WorkerF workerF ) // костылёк
        {
             workerF.label2.Visible = workerF.label3.Visible = workerF.label4.Visible =
                    workerF.textBox2.Visible = workerF.textBox3.Visible = workerF.textBox4.Visible = false;
        }

        public static decimal CalcFromBaseToMax( int years, decimal year_ratio, decimal max_ratio )
        {
            int max_years = (int)(max_ratio / year_ratio);
            if ( years >= max_years )
            {
                return year_ratio * max_years;
            }
            else
            {
                return year_ratio * years;
            }
        }

    }
}
