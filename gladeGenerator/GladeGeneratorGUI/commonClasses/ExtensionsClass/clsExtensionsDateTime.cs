using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

public static class DateTimeExtensions {


    /// <summary>
    /// 日曜日始まりの場合
    /// </summary>
    public static DateTime _getThisWeek_sunday(this DateTime d) {
        return d.AddDays(DayOfWeek.Sunday - d.DayOfWeek);
    }

    /// <summary>
    /// 月曜日始まりの場合
    /// </summary>
    public static DateTime _getThisWeek_monday(this DateTime d) {
        int diff = DayOfWeek.Monday - d.DayOfWeek;
        if (diff > 0)
            diff -= 7;
        return d.AddDays(diff);

    }

    /// <summary>
    /// 時間を比較する
    /// </summary>
    public static int _compareTime(this DateTime time, DateTime nowTime) {

        int result = -1;
        switch (time.CompareTo(nowTime)) {
            case -1:
                // Console.WriteLine(" time は nowTime より古い -1");
                result = -1;
                break;
            case 0:
                //  Console.WriteLine(" time と nowTime は等しい 0");
                result = 0;
                break;
            case 1:
                //  Console.WriteLine(" time は nowTime より新しい 1");
                result = 1;
                break;
        }
        return result;

    }


    /// <summary>
    /// タイムスタンプをDateTimeに変換する
    /// </summary>
    public static DateTime _mkUnixTimeStampToDateTime(this long unixTimeStamp) {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
    
    /// <summary>
    /// タイムスタンプをDateTimeに変換する
    /// </summary>
    public static DateTime? _mkUnixTimeStampToDateTime(this string unixTimeStampStr) {

        if (long.TryParse(unixTimeStampStr , out long unixTimeStamp))
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        return null;
    }


    /// <summary>
    /// DateTimeをタイムスタンプに変換する
    /// </summary>
    public static long _mkUnixTimeStamp(this DateTime targetTime_t) {

        DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        // UTC時間に変換
        DateTime targetTime = targetTime_t.ToUniversalTime();

        // UNIXエポックからの経過時間を取得
        TimeSpan elapsedTime = targetTime - UNIX_EPOCH;

        //経過秒数に変換
        return (long)elapsedTime.TotalSeconds;
    }


    /// <summary>
    /// 何年後　+-
    /// </summary>
    public static DateTime _addYears(this DateTime date, int year) {
        return date.AddYears(year);
    }

    /// <summary>
    /// 何ヵ月後　+-
    /// </summary>
    public static DateTime _addMonths(this DateTime date, int months) {
        return date.AddMonths(months);
    }

    /// <summary>
    /// 何日後　+-
    /// </summary>
    public static DateTime _addDays(this DateTime date, int days) {
        return date.AddDays(days);
    }
    /// <summary>
    /// 何時間後　+-
    /// </summary>
    public static DateTime _addHours(this DateTime date, double hours) {
        return date.AddHours(hours);
    }
    /// <summary>
    /// 何分後　+-
    /// </summary>
    public static DateTime _addMinutes(this DateTime date, double minitus) {
        return date.AddMinutes(minitus);
    }
    
    /// <summary>
    /// 曜日を取得する
    /// </summary>
    public static string _getYoubi(this DateTime date) {
       
        DayOfWeek dow = date.DayOfWeek;
        string youbi = "";
        switch (dow)
        {
            case DayOfWeek.Sunday:
                youbi = "日曜日";
                break;
            case DayOfWeek.Monday:
                youbi = "月曜日";
                break;
            case DayOfWeek.Tuesday:
                youbi = "火曜日";
                break;
            case DayOfWeek.Wednesday:
                youbi = "水曜日";
                break;
            case DayOfWeek.Thursday:
                youbi = "木曜日";
                break;
            case DayOfWeek.Friday:
                youbi = "金曜日";
                break;
            case DayOfWeek.Saturday:
                youbi = "土曜日";
                break;
        }
        return youbi;
    }

    /// <summary>
    /// その日の時間のDateTimeを生成する
    /// </summary>
    public static DateTime _setNowDayTime(this DateTime date,int hour , int minute,int second)
    {
        DateTime date2 = new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
        return date2;
    }

    /// <summary>
    /// UTCに変換する
    /// </summary>
    public static DateTime _convertToUtc(this DateTime date)
    {
        DateTime utcTime = System.TimeZoneInfo.ConvertTimeToUtc(date);
        return utcTime;
    }
    
    /// <summary>
    /// localに変換する
    /// </summary>
    public static DateTime _convertToLocal(this DateTime date)
    {
        DateTime localTime =
            System.TimeZoneInfo.ConvertTimeFromUtc(date, System.TimeZoneInfo.Local);
        return localTime;
    }
    
    /// <summary>
    /// 今日のタイムスタンプ
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static long _getTodayTimeStamp(this DateTime date)
    {
        DateTime copyDateTime = new DateTime(date.Year,date.Month,date.Day,0,0,0);
        long timeStamp = copyDateTime._mkUnixTimeStamp();
        return timeStamp;
    }
    
    /// <summary>
    /// 今日の時間のタイムスタンプ
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static long _getTodayHourTimeStamp(this DateTime date)
    {
        DateTime copyDateTime = new DateTime(date.Year,date.Month,date.Day,date.Hour,0,
            0);
        long timeStamp = copyDateTime._mkUnixTimeStamp();
        return timeStamp;
    }

    /// <summary>
    /// 土日かどうか
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static bool _isSaturdaySunday(this DateTime date)
    {
        DayOfWeek dow = date.DayOfWeek;
        if (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Saturday
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static bool _isSaturday(this DateTime date)
    {
        DayOfWeek dow = date.DayOfWeek;
        if (dow == DayOfWeek.Saturday)
        {
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Sunday
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static bool _isSunday(this DateTime date)
    {
        DayOfWeek dow = date.DayOfWeek;
        if (dow == DayOfWeek.Sunday)
        {
            return true;
        }
        return false;
    }

    private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000;
    public static DateTime _RoundUpToDay(this DateTime value) => value._RoundUpCore(TimeSpan.TicksPerDay);
    public static DateTime _RoundUpToHour(this DateTime value) => value._RoundUpCore(TimeSpan.TicksPerHour);
    public static DateTime _RoundUpToMinute(this DateTime value) => value._RoundUpCore(TimeSpan.TicksPerMinute);
    public static DateTime _RoundUpToSecond(this DateTime value) => value._RoundUpCore(TimeSpan.TicksPerSecond);
    public static DateTime _RoundUpToMillisecond(this DateTime value) => value._RoundUpCore(TimeSpan.TicksPerMillisecond);
    public static DateTime _RoundUpToMicrosecond(this DateTime value) => value._RoundUpCore(TicksPerMicrosecond);
    public static DateTime _RoundUp(this DateTime value, TimeSpan interval) => value._RoundUpCore(interval.Ticks);

    public static DateTime _RoundDownToDay(this DateTime value) => value._RoundDownCore(TimeSpan.TicksPerDay);
    public static DateTime _RoundDownToHour(this DateTime value) => value._RoundDownCore(TimeSpan.TicksPerHour);
    public static DateTime _RoundDownToMinute(this DateTime value) => value._RoundDownCore(TimeSpan.TicksPerMinute);
    public static DateTime _RoundDownToSecond(this DateTime value) => value._RoundDownCore(TimeSpan.TicksPerSecond);
    public static DateTime _RoundDownToMillisecond(this DateTime value) => value._RoundDownCore(TimeSpan.TicksPerMillisecond);
    public static DateTime _RoundDownToMicrosecond(this DateTime value) => value._RoundDownCore(TicksPerMicrosecond);
    public static DateTime _RoundDown(this DateTime value, TimeSpan interval) => value._RoundDownCore(interval.Ticks);

    public static DateTime _RoundAwayFromZeroToDay(this DateTime value) => value._RoundAwayFromZeroCore(TimeSpan.TicksPerDay);
    public static DateTime _RoundAwayFromZeroToHour(this DateTime value) => value._RoundAwayFromZeroCore(TimeSpan.TicksPerHour);
    public static DateTime _RoundAwayFromZeroToMinute(this DateTime value) => value._RoundAwayFromZeroCore(TimeSpan.TicksPerMinute);
    public static DateTime _RoundAwayFromZeroToSecond(this DateTime value) => value._RoundAwayFromZeroCore(TimeSpan.TicksPerSecond);
    public static DateTime _RoundAwayFromZeroToMillisecond(this DateTime value) => value._RoundAwayFromZeroCore(TimeSpan.TicksPerMillisecond);
    public static DateTime _RoundAwayFromZeroToMicrosecond(this DateTime value) => value._RoundAwayFromZeroCore(TicksPerMicrosecond);
    public static DateTime _RoundAwayFromZero(this DateTime value, TimeSpan interval) => value._RoundAwayFromZeroCore(interval.Ticks);

    private static DateTime _RoundUpCore(this DateTime value, long interval)
    {
        return value.Ticks % interval is var overflow and > 0
            ? value.AddTicks(interval - overflow)
            : value;
    }

    private static DateTime _RoundDownCore(this DateTime value, long interval)
    {
        return value.AddTicks(-(value.Ticks % interval));
    }

    private static DateTime _RoundAwayFromZeroCore(this DateTime value, long interval)
    {
        var ticks = value.Ticks + interval >> 1;
        return new DateTime(ticks - ticks % interval, value.Kind);
    }

}