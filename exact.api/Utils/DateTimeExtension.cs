using System;

namespace exact.api.Utils
{
    public static class DateTimeExtension
    {
        public static DateTime GetBrDateTime(this DateTime utc)
        {    
#if DEBUG
            return utc;
#endif
            
            var kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var horaBrasilia= TimeZoneInfo.ConvertTime(utc, kstZone);
            return horaBrasilia;
        }

        public static string Format(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}