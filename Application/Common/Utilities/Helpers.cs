using System.Globalization;

namespace Application.Common.Utilities;

public static class Helpers
{
    public static string ToPersianTime(this DateTime calendar)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}", pc.GetYear(calendar), pc.GetMonth(calendar),
                pc.GetDayOfMonth(calendar));
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    
    public static int ToInt(this string? val)
    {
        return string.IsNullOrEmpty(val) ? 0 : Convert.ToInt32(val);
    }
    public static long ToLong(this string? val)
    {
        return string.IsNullOrEmpty(val) ? 0 : Convert.ToInt64(val);
    }
    public static double ToDouble(this string? val)
    {
        return string.IsNullOrEmpty(val) ? 0 : Convert.ToDouble(val);
    }
}