namespace PineBot.Util;

public static class TimeConvert
{
    private static readonly DateTime _unixStartTime = new DateTime(1970, 1, 1, 8, 0, 0, 0);

    public static DateTime ToDateTime(long timeStamp)
    {
        return _unixStartTime.AddSeconds(timeStamp);
    }

    public static string GetTimeString(long timeStamp)
    {
        return _unixStartTime.AddSeconds(timeStamp).ToString("yyyy/MM/dd HH:mm:ss");
    }
}