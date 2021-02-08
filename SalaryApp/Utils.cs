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
