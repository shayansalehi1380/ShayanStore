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
        catch (Exception e)
        {
            return string.Empty;
        }
    }
}