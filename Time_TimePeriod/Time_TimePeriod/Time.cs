using System;

namespace Time_TimePeriod
{
    /// <summary>
    /// Struktura typu Time opisująca punkt w czasie
    /// </summary>
    public struct Time: IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        ///  wewnętrzna reprezentacja czasu w godzinach (typ byte)
        /// </summary>
        public byte Hours => _hours;

        private readonly byte _hours; 
        /// <summary>
        ///  wewnętrzna reprezentacja czasu w minutach (typ byte)
        /// </summary>
        public byte Minutes => _minutes;

        private readonly byte _minutes;
        /// <summary>
        ///  wewnętrzna reprezentacja czasu w sekundach (typ byte)
        /// </summary>
        public byte Seconds => _seconds;

        private readonly byte _seconds;

        public Time(byte hour, byte minute = 0, byte second = 0)
        {
            if(hour > 23) throw new ArgumentException("Incorrect argument!");
            _hours = hour;
            if(minute > 59) throw new ArgumentException("Incorrect argument!");
            _minutes = minute;
            if(second > 59) throw new ArgumentException("Incorrect argument!");
            _seconds = second;
        }

        public Time(string time)
        {
            var timeTab = time.Split(':');
            _hours = Convert.ToByte(timeTab[0]);
            _minutes = Convert.ToByte(timeTab[1]);
            _seconds = Convert.ToByte(timeTab[2]);
        }
        public override string ToString()
        {
            return $"{_hours:00}:{_minutes:00}:{_seconds:00}";
        }

        public bool Equals(Time other)
        {
            if (_hours != other._hours || _minutes != other._minutes || _seconds != other._seconds) return false;
            return true;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public int CompareTo(Time other)
        {
            if (_hours - other._hours != 0) return _hours - other._hours;
            if (_minutes - other._minutes != 0) return _minutes - other._minutes;
            if (_seconds - other._seconds != 0) return _seconds - other._seconds;
            return 0;
        }

        
        public override int GetHashCode()
        {
            return HashCode.Combine(_hours, _minutes, _seconds);
        }

        public static bool operator == (Time t1, Time t2) => t1.Equals(t2);
        public static bool operator != (Time t1, Time t2) => !(t1 == t2);
        public static bool operator > (Time t1, Time t2) => t1.CompareTo(t2) > 0;
        public static bool operator >= (Time t1, Time t2) => t1.CompareTo(t2) >= 0;
        public static bool operator < (Time t1, Time t2) => t1.CompareTo(t2) < 0;
        public static bool operator <= (Time t1, Time t2) => t1.CompareTo(t2) <= 0;
        public static Time operator +(Time a, TimePeriod b) => a.TimePlus(b);
        public static Time operator -(Time a, TimePeriod b) => a.TimeMinus(b);
        

        public Time TimePlus(TimePeriod timePeriod)
        {
            var seconds = _hours * 3600 + _minutes * 60 + _seconds + timePeriod._Seconds;
            byte h =(byte) (seconds / 3600 > 23 ? seconds / 3600 % 24 : seconds / 3600);
            byte m =(byte) (seconds / 60 % 60);
            byte s = (byte)(seconds % 60);
            return new Time(h, m, s);
        }
        public static Time TimePlus(TimePeriod timePeriod, Time time)
        {
            var seconds = ConvertToSeconds(time) + timePeriod._Seconds;
            byte h = ( byte) (seconds / 3600 > 23 ? seconds / 3600 % 24 : seconds / 3600);
            byte m = (byte) (seconds / 60 % 60);
            byte s = (byte) (seconds % 60);
            return new Time(h, m, s);
        }

        public static Time TimeMinus(TimePeriod timePeriod, Time time)
        {
            var seconds =  ConvertToSeconds(time)-timePeriod._Seconds;
            byte h =(byte) (seconds / 3600 >= 0 ? seconds / 3600 : seconds / 3600 + 24);
            byte m;
            byte s;
            if (seconds / 60 % 60 >= 0)
            {
                m = (byte) (seconds / 60 % 60);
            }
            else
            {
                m = (byte) (seconds / 60 % 60 + 60);
                h--;
            }

            if (seconds % 60 >= 0)
            {
                s = (byte) (seconds % 60);
            }
            else
            {
                s = (byte) (seconds % 60 + 60);
                m--;
            }
            return new Time(h, m, s);
          
        }
        public Time TimeMinus(TimePeriod timePeriod)
        {
            var seconds = _hours * 3600 + _minutes * 60 + _seconds - timePeriod._Seconds;
            byte h =(byte) (seconds / 3600 >= 0 ? seconds / 3600 : seconds / 3600 + 24);
            byte m;
            byte s;
            if (seconds / 60 % 60 >= 0)
            {
                m = (byte) (seconds / 60 % 60);
            }
            else
            {
                m = (byte) (seconds / 60 % 60 + 60);
                h--;
            }

            if (seconds % 60 >= 0)
            {
                s = (byte) (seconds % 60);
            }
            else
            {
                s = (byte) (seconds % 60 + 60);
                m--;
            }
            return new Time(h, m, s);
        }

        private static long ConvertToSeconds(Time time)
        {
            return time._hours * 3600 + time._minutes * 60 + time._seconds;
        }
    }
}