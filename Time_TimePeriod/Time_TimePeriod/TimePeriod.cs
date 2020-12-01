using System;

namespace Time_TimePeriod
{
    public struct TimePeriod: IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private long seconds;

        private long Seconds => seconds;

        public TimePeriod(ulong hour, byte minute, byte second = 0)
        {
            if(minute > 59) throw new ArgumentException("Incorect argument!");
            if(second > 59) throw new ArgumentException("Incorect argument!");
            seconds = (long) (hour * 3600 + (ulong) (minute * 60) + second);
        }

        public TimePeriod(string timePeriod)
        {
            
        }

        public bool Equals(TimePeriod other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(TimePeriod other)
        {
            throw new NotImplementedException();
        }
    }
}