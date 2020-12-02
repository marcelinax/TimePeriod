using System;

namespace Time_TimePeriod
{
    /// <summary>
    /// Struktura typu TimePeriod reprezentująca długość odcinka w czasie
    /// </summary>
    public struct TimePeriod: IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long seconds;

        /// <summary>
        /// wewnętrzna realizacja czasu trwania w sekundach (typ long)
        /// </summary>
        public long Seconds => seconds;

        public TimePeriod(ulong hour, byte minute, byte second = 0)
        {
            if(minute > 59) throw new ArgumentException("Incorect argument!");
            if(second > 59) throw new ArgumentException("Incorect argument!");
            seconds = (long) (hour * 3600 + (ulong) (minute * 60) + second);
        }

        public TimePeriod(long s)
        {
            seconds = s;
        }

        public TimePeriod (Time t1, Time t2)
        {
            var time = ConvertToSeconds(t1);
            var time2 = ConvertToSeconds(t2);
            if (time2 - time < 0)
            {
                seconds = (time2 - time) + 24 * 3600;
            }
            else  seconds = time2 - time;
        }

        public TimePeriod TimePeriodPlus(TimePeriod timePeriod)
        {
            return new TimePeriod(seconds + timePeriod.seconds);
        }

        public TimePeriod(string timePeriod)
        {
            var timeTab = timePeriod.Split(':');
            
            var h = long.Parse(timeTab[0]) >= 0? long.Parse(timeTab[0]) : throw new ArgumentException("Incorect argument!") ;
            var m = long.Parse(timeTab[1]) < 60? long.Parse(timeTab[1]) : throw new ArgumentException("Incorect argument!") ;
            var s = long.Parse(timeTab[2]) < 60? long.Parse(timeTab[2]) : throw new ArgumentException("Incorect argument!") ;
            seconds = h * 3600 + m * 60 + s;
        }
        public override string ToString()
        {
            return $"{Seconds / 3600}:{(Seconds / 60) % 60:00}:{Seconds % 60:00}";
        }

        public bool Equals(TimePeriod other)
        {
            return seconds == other.seconds;
        }

        public override bool Equals(object? obj)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return seconds.GetHashCode();
        }

        public int CompareTo(TimePeriod other)
        {
            return Convert.ToInt32(seconds - other.seconds);
        }
        private static long ConvertToSeconds(Time time)
        {
            return time.Hours * 3600 + time.Minutes * 60 + time.Seconds;
        }
        
        public static bool operator == (TimePeriod t1, TimePeriod t2) => t1.Equals(t2);
        public static bool operator != (TimePeriod t1, TimePeriod t2) => !(t1 == t2);
        public static bool operator > (TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) > 0;
        public static bool operator >= (TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) >= 0;
        public static bool operator < (TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) < 0;
        public static bool operator <= (TimePeriod t1, TimePeriod t2) => t1.CompareTo(t2) <= 0;
        public static TimePeriod operator +(TimePeriod a , TimePeriod b) => a.TimePeriodPlus(b);
       
    }
}