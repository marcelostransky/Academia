using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace AcademiaDanca.IO.Compartilhado.Util
{
    public static class DateTimeExtension
    {
        public static string GetDayOfWeek(this DateTime uiDateTime, CultureInfo culture = null)
        {
            if (culture == null)
            {
                culture = Thread.CurrentThread.CurrentUICulture;
            }

            return culture.DateTimeFormat.GetDayName(uiDateTime.DayOfWeek);
        }
    }
}
