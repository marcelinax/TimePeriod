using System;

namespace Time_TimePeriod
{
    public struct Time: IEquatable<Time>, IComparable<Time>
    {
        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }

        public Time(byte hour, byte minute = 0, byte second = 0)
        {
            if(hour > 23) throw new ArgumentException("Incorrect argument!");
            Hours = hour;
            if(minute > 59) throw new ArgumentException("Incorrect argument!");
            Minutes = minute;
            if(second > 59) throw new ArgumentException("Incorrect argument!");
            Seconds = second;
        }

        public Time(string time)
        {
            var timeTab = time.Split(':');
            Hours = Convert.ToByte(timeTab[0]);
            Minutes = Convert.ToByte(timeTab[1]);
            Seconds = Convert.ToByte(timeTab[2]);
        }
        public override string ToString()
        {
            return $"{Hours:00}:{Minutes:00}:{Seconds:00}";
        }

        public bool Equals(Time other)
        {
            if (Hours != other.Hours || Minutes != other.Minutes || Seconds != other.Seconds) return false;
            return true;
        }

        public int CompareTo(Time other)
        {
            if (Hours - other.Hours != 0) return Hours - other.Hours;
            if (Minutes - other.Minutes != 0) return Minutes - other.Minutes;
            if (Seconds - other.Seconds != 0) return Seconds - other.Seconds;
            return 0;
        }

        public static bool operator == (Time t1, Time t2) => t1.Equals(t2);
        public static bool operator != (Time t1, Time t2) => !(t1 == t2);
        public static bool operator > (Time t1, Time t2) => t1.CompareTo(t2) > 0;
        public static bool operator >= (Time t1, Time t2) => t1.CompareTo(t2) >= 0;
        public static bool operator < (Time t1, Time t2) => t1.CompareTo(t2) < 0;
        public static bool operator <= (Time t1, Time t2) => t1.CompareTo(t2) <= 0;

        public Time TimePlus(TimePeriod timePeriod)
        {
            var 
        }
        public static Time TimePlus(TimePeriod timePeriod, Time time)
        {
            
        }
    }
}